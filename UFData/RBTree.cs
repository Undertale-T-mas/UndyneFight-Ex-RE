using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace UFData
{
    [SkipLocalsInit, Serializable]
    public unsafe class RBTree<T> : IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection<T>, IList<T>, IEnumerable<T> where T : unmanaged, IComparable<T>
    {
        int elementSize;
        public RBTree(int allocateSize = 512, params T[] values)
        {
            allocateSize = Math.Max(allocateSize, values.Length);
            int checker = allocateSize & -allocateSize;
            if (checker != allocateSize)
            {
                while (true)
                {
                    allocateSize -= checker;
                    int temp = checker;
                    if (allocateSize == 0)
                    {
                        allocateSize = temp << 1;
                        break;
                    }
                    checker = allocateSize & -allocateSize;
                }
            }
            elementSize = sizeof(RBTreeNode<T>);
            this.allocateSize = allocateSize;
            if (values != null && values.Length > 0)
            {
                this.UnBalancedBuild(values);
            }
        }

        private enum Color
        {
            BLACK = 0,
            RED = 1
        }
        Queue<IntPtr> recycled = new(16);
        [SkipLocalsInit]
        private unsafe struct RBTreeNode<S> where S : unmanaged, IComparable<S>
        {
            public RBTreeNode(S data, Color color)
            {
                this.data = data;
                this.color = color;
            }
            public S data;
            public Color color;
            public int treeSize = 1;
            public int count = 1;

            public RBTreeNode<S>* parent = null;
            public RBTreeNode<S>* left = null, right = null;

            public int CompareTo(RBTreeNode<S> other)
            {
                return this.data.CompareTo(other.data);
            }
            public bool IsLeaf()
            {
                return this.left != null && this.right != null;
            }

            public static bool operator >(RBTreeNode<S> operand1, RBTreeNode<S> operand2)
            {
                return operand1.CompareTo(operand2) > 0;
            }
            public static bool operator <(RBTreeNode<S> operand1, RBTreeNode<S> operand2)
            {
                return operand1.CompareTo(operand2) < 0;
            }

            public static bool operator >=(RBTreeNode<S> operand1, RBTreeNode<S> operand2)
            {
                return operand1.CompareTo(operand2) >= 0;
            }
            public static bool operator <=(RBTreeNode<S> operand1, RBTreeNode<S> operand2)
            {
                return operand1.CompareTo(operand2) <= 0;
            }

            public static bool operator ==(RBTreeNode<S> l, RBTreeNode<S> r)
            {
                return l.data.CompareTo(r.data) == 0;
            }
            public static bool operator !=(RBTreeNode<S> l, RBTreeNode<S> r)
            {
                return l.data.CompareTo(r.data) != 0;
            }

            public void Maintain()
            {
                this.treeSize = count;
                if (left != null) this.treeSize += left->treeSize;
                if (right != null) this.treeSize += right->treeSize;
                if (parent != null) this.parent->Maintain();
            }

            public void FullMaintain()
            {
                if (left != null) this.left->FullMaintain();
                if (right != null) this.right->FullMaintain();
                this.Maintain();
            }

            internal void UploadMaintain()
            {
                this.Maintain();
                if (parent != null) parent->Maintain();
            }

            public override bool Equals(object? obj)
            {
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return data.GetHashCode();
            }
        }

        private RBTreeNode<T>* _root;
        public int Count
        {
            get;
            private set;
        } = 0;

        public bool IsReadOnly => throw new NotImplementedException();

        private void RotateL(RBTreeNode<T>* parent)
        {
            RBTreeNode<T>* subR = parent->right, subRL = subR->left;
            RBTreeNode<T>* grandpa = parent->parent;

            parent->right = subRL;
            if (subRL != null) subRL->parent = parent;

            subR->left = parent;
            parent->parent = subR;

            if (grandpa == null)
            {
                _root = subR;
                _root->parent = null;
            }
            else
            {
                if (parent == grandpa->left) grandpa->left = subR;
                else grandpa->right = subR;
                subR->parent = grandpa;
            }
            parent->Maintain();
            subR->Maintain();
        }
        private void RotateR(RBTreeNode<T>* parent)
        {
            RBTreeNode<T>* subL = parent->left, subLR = subL->right;
            RBTreeNode<T>* grandpa = parent->parent;

            parent->left = subLR;
            if (subLR != null) subLR->parent = parent;

            subL->right = parent;
            parent->parent = subL;

            if (grandpa == null)
            {
                _root = subL;
                _root->parent = null;
            }
            else
            {
                if (parent == grandpa->left) grandpa->left = subL;
                else grandpa->right = subL;
                subL->parent = grandpa;
            }
            parent->Maintain();
            subL->Maintain();
        }
        private void RotateLR(RBTreeNode<T>* parent)
        {
            RotateL(parent->left);
            RotateR(parent);
        }
        private void RotateRL(RBTreeNode<T>* parent)
        {
            RotateR(parent->right);
            RotateL(parent);
        }

        ~RBTree()
        {
            if (allocateCount > 0)
                Marshal.FreeHGlobal(init);
        }

        RBTreeNode<T>* last = null;
        IntPtr init;
        int restAllocated = 0;
        private int allocateSize;
        private int allocateCount = 0;
        private RBTreeNode<T>* CreateNode(T data, Color color = Color.RED)
        {
            if (recycled.Count > 0)
            {
                RBTreeNode<T>* pos = (RBTreeNode<T>*)recycled.Dequeue();
                *pos = new(data, color);
                return pos;
            }
            if (restAllocated == 0)
            {
                if (allocateCount == 0)
                {
                    IntPtr ptr = Marshal.AllocHGlobal(elementSize * allocateSize); init = ptr;
                    last = (RBTreeNode<T>*)ptr;
                }
                else
                {
                    throw new NotImplementedException();
                    IntPtr ptr = Marshal.ReAllocHGlobal(init, (IntPtr)(elementSize * allocateSize * (allocateCount + 1)));
                    last = (RBTreeNode<T>*)ptr;
                    last += allocateSize * allocateCount;
                }
                allocateCount++;
                *last = new(data, color);
                restAllocated = allocateSize - 1;
                return last;
            }
            else
            {
                last++;
                *last = new(data, color);
                restAllocated--;
                return last;
            }
        }
        private bool TryInsert(T data)
        {
            if (_root == null)
            {
                RBTreeNode<T>* node = CreateNode(data);
                _root = node;
                _root->color = Color.BLACK;
                return true;
            }
            RBTreeNode<T>* cur = _root;
            RBTreeNode<T>* parent = null;
            T insert = data;
            while (cur != null)
            {
                cur->treeSize++;
                int u = insert.CompareTo(cur->data);
                if (u < 0)
                {
                    parent = cur;
                    cur = cur->left;
                }
                else if (u > 0)
                {
                    parent = cur;
                    cur = cur->right;
                }
                else
                {
                    while (cur != null)
                    {
                        cur->treeSize--;
                        cur = cur->parent;
                    }
                    return false;
                }
            }
            cur = CreateNode(insert);
            if (*cur < *parent)
            {
                parent->left = cur;
                cur->parent = parent;
            }
            else
            {
                parent->right = cur;
                cur->parent = parent;
            }
            ;
            while (parent != null && parent->color == Color.RED)
            {
                RBTreeNode<T>* grandpa = parent->parent;
                if (parent == grandpa->left)
                {
                    RBTreeNode<T>* uncle = grandpa->right;
                    if (uncle != null && uncle->color == Color.RED)
                    {
                        parent->color = uncle->color = Color.BLACK;
                        grandpa->color = Color.RED;

                        cur = grandpa;
                        parent = cur->parent;
                    }
                    else
                    {
                        if (cur == parent->left)
                        {
                            RotateR(grandpa);
                            grandpa->color = Color.RED;
                            parent->color = Color.BLACK;
                        }
                        else // cur == parent->right
                        {
                            RotateLR(grandpa);
                            grandpa->color = Color.RED;
                            cur->color = Color.BLACK;
                        }
                        break; // The root of sapling is black
                    }
                }
                else
                {
                    RBTreeNode<T>* uncle = grandpa->left;
                    if (uncle != null && uncle->color == Color.RED)
                    {
                        uncle->color = parent->color = Color.BLACK;
                        grandpa->color = Color.RED;

                        cur = grandpa;
                        parent = cur->parent;
                    }
                    else
                    {
                        if (cur == parent->left)
                        {
                            RotateRL(grandpa);

                            grandpa->color = Color.RED;
                            cur->color = Color.BLACK;
                        }
                        else
                        {
                            RotateL(grandpa);

                            grandpa->color = Color.RED;
                            parent->color = Color.BLACK;
                        }
                        break;
                    }
                }
            }
            _root->color = Color.BLACK;
            return true;
        }

#if DEBUG
        public void ISRBTree()
        {
            if (_root == null) return;
            if (_root->color == Color.RED)
            {
                throw new Exception();
            }
            int bcount = 0;
            RBTreeNode<T>* cur = _root;
            while (cur != null)
            {
                if (cur->color == Color.BLACK) bcount++;
                cur = cur->left;
            }
            int count = 0;
            ISRBTree(_root, count, bcount);
        }

        void ISRBTree(RBTreeNode<T>* root, int count, int bcount)
        {
            if (root == null)
            {
                if (count != bcount) throw new Exception();
                return;
            }
            if (root->parent != null)
                if (root->color == Color.RED && root->parent->color == Color.RED) throw new Exception();
            if (root->color == Color.BLACK) count++;
            ISRBTree(root->left, count, bcount);
            ISRBTree(root->right, count, bcount);
        }

        private void ShowInfo(RBTreeNode<T>* node)
        {
            if (node == null) return;
            Console.WriteLine($"address = {(IntPtr)node} value = {node->data}, count = {node->treeSize}, (&l, &r) = " +
                $"({(IntPtr)node->left}, {(IntPtr)node->right}), color = {node->color}");
            ShowInfo(node->left);
            ShowInfo(node->right);
        }
        public void ShowInfo()
        {
            Console.WriteLine("Showing info:");
            ShowInfo(_root);
        }
#endif

        public int IndexOf(T item)
        {
            if (_root == null) return -1;
            RBTreeNode<T>* cur = _root;
            int size = 0;
            while (cur != null)
            {
                int u = item.CompareTo(cur->data);
                if (u < 0) { cur = cur->left; }
                else if (u > 0) { if (cur->left != null) cur += cur->left->treeSize; cur = cur->right; }
                else return size;
            }
            return -1;
        }

        [SkipLocalsInit]
        public void Insert(int index, T item)
        {
            throw new NotSupportedException();
        }

        private void Rebuild()
        {
            if (this.Count < 20000)
            {
                Span<T> data = stackalloc T[this.Count];
                GainData(data, _root, 0);
                this.Clear();
                this.allocateSize *= 2;
                this.Build(data);
            }
            else
            {
                T[] data = new T[this.Count];
                this.CopyTo(data, 0);
                this.Clear();
                this.allocateSize *= 2;
                fixed (T* t = data) _root = Build(t, data.Length, 0, 0);
                this.Count = data.Length;
            }
        }

        private void GainData(Span<T> data, RBTreeNode<T>* cur, int index)
        {
            int lts = 0;
            if (cur->left != null)
            {
                lts += cur->left->treeSize;
                GainData(data, cur->left, index);
            }
            data[lts + index] = cur->data;
            if (cur->right != null)
                GainData(data, cur->right, lts + index + 1);
        }

        private RBTreeNode<T>* Build(T* array, int size, int index, int color)
        {
            if (size == 1) return CreateNode(*(array + index), (Color)color);
            int s = size >> 1;
            RBTreeNode<T>* l = Build(array, s, index, color ^ 1);
            RBTreeNode<T>* r = Build(array, s, index + s + 1, color ^ 1);
            RBTreeNode<T>* cur = CreateNode(*(array + index + s), (Color)color);

            l->parent = cur; r->parent = cur;
            cur->left = l; cur->right = r;
            cur->treeSize = size;

            return cur;
        }
        private void Build(Span<T> array)
        {
            fixed (T* t = array) _root = Build(t, array.Length, 0, 0);
            this.Count = array.Length;
        }
        private void UnBalancedBuild(T[] values)
        {
            Array.Sort(values);
            int len = values.Length + 1;
            int checker = len & -len;
            if (checker != len)
            {
                int size2 = len;
                while (true)
                {
                    size2 -= checker;
                    int temp = checker;
                    if (size2 == 0)
                    {
                        size2 = temp;
                        break;
                    }
                    checker = size2 & -size2;
                }
                size2 -= 1; len -= 1;
                fixed (T* t = values) _root = Build(t, size2, 0, 0);
                for (int i = size2; i < len; i++)
                {
                    this.TryInsert(values[i]);
                }
                this.Count = len;
            }
            else
            {
                fixed (T* t = values) _root = Build(t, values.Length, 0, 0);
                this.Count = values.Length;
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException();
                if (_root == null) throw new InvalidOperationException();
                RBTreeNode<T>* cur = _root;
                while (cur != null)
                {
                    int lTreeSize = cur->left == null ? 0 : cur->left->treeSize;
                    if (index == lTreeSize) return cur->data;
                    else if (index < lTreeSize) cur = cur->left;
                    else { index = index - lTreeSize - 1; cur = cur->right; }
                }
                throw new Exception();
            }
            set
            {

            }
        }

        public void RemoveAt(int index)
        {
            this.Remove(this[index]);
        }

        public void Add(T item)
        {
            if (Count == allocateSize - 1)
            {
                Rebuild();
            }
            if (this.TryInsert(item)) Count++;
        }

        public void Clear()
        {
            if (allocateCount > 0)
            {
                restAllocated = 0;
                allocateCount = 0;

                Marshal.FreeHGlobal(init);
            }
            if (recycled != null) recycled.Clear();
            Count = 0;
            _root = null;
        }

        public bool Contains(T item)
        {
            if (_root == null) return false;
            RBTreeNode<T>* cur = _root;
            while (cur != null)
            {
                int u = item.CompareTo(cur->data);
                if (u < 0) { cur = cur->left; }
                else if (u > 0) { cur = cur->right; }
                else return true;
            }
            return false;
        }

        private void GetSpan(RBTreeNode<T>* cur, Span<T> array, int index)
        {
            int lTreeSize = 0;
            if (cur->left != null)
            {
                lTreeSize = cur->left->treeSize;
                GetSpan(cur->left, array, index);
            }
            array[index + lTreeSize] = cur->data;
            if (cur->right != null) GetSpan(cur->right, array, index + lTreeSize + 1);
        }
        private void CopyTo(RBTreeNode<T>* cur, T[] array, int index)
        {
            int lTreeSize = 0;
            if (cur->left != null)
            {
                lTreeSize = cur->left->treeSize;
                CopyTo(cur->left, array, index);
            }
            array[index + lTreeSize] = cur->data;
            if (cur->right != null) CopyTo(cur->right, array, index + lTreeSize + 1);
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (_root == null) throw new InvalidOperationException();
            CopyTo(_root, array, arrayIndex);
        }
        private void ICopy(RBTreeNode<T>* cur, T[] arr, int left, int right, int index)
        {
            int lTreeSize = 0;
            if (cur->left != null)
            {
                lTreeSize = cur->left->treeSize;
                if (lTreeSize + index >= 0)
                    ICopy(cur->left, arr, left, right, index);
            }
            int curPos = index + lTreeSize;
            if (curPos >= 0)
                arr[curPos] = cur->data;
            if (cur->right != null)
            {
                curPos++;
                if (curPos < arr.Length)
                    ICopy(cur->right, arr, left, right, index + lTreeSize + 1);
            }
        }
        /// <summary>
        /// copy a part of the sorted array. 
        /// </summary>
        /// <param name="left">Left bound. Included</param>
        /// <param name="right">Right bound. Included</param>
        /// <returns>The array of the interval copied from the sorted array</returns>
        /// <exception cref="InvalidOperationException">When the set is empty</exception>
        /// <exception cref="ArgumentOutOfRangeException">when the left or right bound given is out the range of data</exception>
        public T[] IntervalCopy(int left, int right)
        {
            if (_root == null) throw new InvalidOperationException();

            if (left < 0 || right < left || right >= Count) throw new ArgumentOutOfRangeException();

            T[] arr = new T[right - left + 1];
            ICopy(_root, arr, left, right, -left);

            return arr;
        }

        [SkipLocalsInit]
        public bool Remove(T item)
        {
            bool t = TryRemove(item);
            if (t) Count--;
            return t;
        }
        [SkipLocalsInit]
        private bool TryRemove(T item)
        {
            RBTreeNode<T>* parent = null, cur = _root, delParentPos = null, delPos = null;
            while (cur != null)
            {
                int u = item.CompareTo(cur->data);
                if (u == -1)
                {
                    parent = cur;
                    cur = cur->left;
                }
                else if (u == 1)
                {
                    parent = cur;
                    cur = cur->right;
                }
                else // the node to be removed
                {
                    if (cur->left == null)
                    {
                        if (cur == _root)
                        {
                            _root = _root->right;
                            if (_root != null)
                            {
                                _root->parent = null;
                                _root->color = Color.BLACK;
                            }
                            ReleaseMemory(cur);
                            return true;
                        }
                        else
                        {
                            cur->count = 0;
                            delParentPos = parent;
                            delPos = cur;
                        }
                        break;
                    }
                    else if (cur->right == null)
                    {
                        if (cur == _root)
                        {
                            _root = _root->left;
                            if (_root != null)
                            {
                                _root->parent = null;
                                _root->color = Color.BLACK;
                            }
                            ReleaseMemory(cur);
                            return true;
                        }
                        else
                        {
                            cur->count = 0;
                            delParentPos = parent;
                            delPos = cur;
                        }
                        break;
                    }
                    else
                    {
                        RBTreeNode<T>* minParent = cur, minRight = cur->right;
                        while (minRight->left != null)
                        {
                            minParent = minRight;
                            minRight = minRight->left;
                        }
                        cur->data = minRight->data;
                        delParentPos = minParent;
                        delPos = minRight;
                        break;
                    }
                }
            }
            if (delPos == null)
            {
                return false;
            }

            RBTreeNode<T>* del = delPos, delP = delParentPos;

            if (delPos->color == Color.BLACK)
            {
                if (delPos->left != null)
                {
                    delPos->left->color = Color.BLACK;
                }
                else if (delPos->right != null)
                {
                    delPos->right->color = Color.BLACK;
                }
                else
                {
                    while (delPos != _root)
                    {
                        if (delPos == delParentPos->left)
                        {
                            RBTreeNode<T>* brother = delParentPos->right;
                            if (brother->color == Color.RED)
                            {
                                delParentPos->color = Color.RED;
                                brother->color = Color.BLACK;
                                RotateL(delParentPos);
                                brother = delParentPos->right;
                            }
                            if (((brother->left == null) || (brother->left->color == Color.BLACK))
                            && ((brother->right == null) || (brother->right->color == Color.BLACK)))
                            {
                                brother->color = Color.RED;
                                if (delParentPos->color == Color.RED)
                                {
                                    delParentPos->color = Color.BLACK;
                                    break;
                                }
                                delPos = delParentPos;
                                delParentPos = delPos->parent;
                            }
                            else
                            {
                                if ((brother->right == null) || (brother->right->color == Color.BLACK))
                                {
                                    brother->left->color = Color.BLACK;
                                    brother->color = Color.RED;
                                    RotateR(brother);
                                    brother = delParentPos->right;
                                }
                                brother->color = delParentPos->color;
                                delParentPos->color = Color.BLACK;
                                brother->right->color = Color.BLACK;
                                RotateL(delParentPos);
                                break;
                            }
                        }
                        else //delPos == delParentPos->right  
                        {
                            RBTreeNode<T>* brother = delParentPos->left;
                            if (brother->color == Color.RED)
                            {
                                delParentPos->color = Color.RED;
                                brother->color = Color.BLACK;
                                RotateR(delParentPos);
                                //需要继续处理
                                brother = delParentPos->left;
                            }
                            if (((brother->left == null) || (brother->left->color == Color.BLACK))
                                && ((brother->right == null) || (brother->right->color == Color.BLACK)))
                            {
                                brother->color = Color.RED;
                                if (delParentPos->color == Color.RED)
                                {
                                    delParentPos->color = Color.BLACK;
                                    break;
                                }
                                delPos = delParentPos;
                                delParentPos = delPos->parent;
                            }
                            else
                            {
                                if ((brother->left == null) || (brother->left->color == Color.BLACK))
                                {
                                    brother->right->color = Color.BLACK;
                                    brother->color = Color.RED;
                                    RotateL(brother);
                                    brother = delParentPos->left;
                                }
                                brother->color = delParentPos->color;
                                delParentPos->color = Color.BLACK;
                                brother->left->color = Color.BLACK;
                                RotateR(delParentPos);
                                break;
                            }
                        }
                    }
                }
            }

            if (del->left == null)
            {
                if (del == delP->left)
                {
                    delP->left = del->right;
                    if (del->right != null)
                        del->right->parent = delP;
                }
                else
                {
                    delP->right = del->right;
                    if (del->right != null)
                        del->right->parent = delP;
                }
            }
            else
            {
                if (del == delP->left)
                {
                    delP->left = del->left;
                    if (del->left != null)
                        del->left->parent = delP;
                }
                else
                {
                    delP->right = del->left;
                    if (del->left != null)
                        del->left->parent = delP;
                }
            }
            delP->UploadMaintain();
            ReleaseMemory(del);
            return true;
        }

        private void ReleaseMemory(RBTree<T>.RBTreeNode<T>* root)
        {
            recycled.Enqueue((IntPtr)root);
        }


        public class RBTreeEnumerator : IEnumerator<T>, IDisposable
        {
            private T[] elements;
            public RBTreeEnumerator(RBTree<T> tree)
            {
                elements = tree.ToArray();
                this.len = elements.Length;
            }

            int len;
            int index = -1;
            public T Current => elements[index];

            object IEnumerator.Current => elements[index];

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                index++;
                if (index >= elements.Length) return false;
                return true;
            }

            public void Reset()
            {
                index = -1;
            }
        }
        /// <summary>
        /// Get an enumerator to enumerate all the elements. It's not in sorted order!
        /// </summary>
        /// <returns>The enumerator</returns> 
        public IEnumerator<T> GetEnumerator()
        {
            return new RBTreeEnumerator(this);
        }

        /// <summary>
        /// Get an enumerator to enumerate all the elements. It's not in sorted order!
        /// </summary>
        /// <returns>The enumerator</returns> 
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new RBTreeEnumerator(this);
        }
    }
}