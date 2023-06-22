using System;
using System.Collections.Generic;

namespace UndyneFight_Ex
{
    public struct Protected<T>
    {
        bool hacked;
        public bool Hacked => hacked;
        private T value;
        public T Value
        {
            get
            {
                if (value.GetHashCode() != hash)
                    hacked = true;
                return value;
            }
            set
            {
                this.value = value;
                hash = value.GetHashCode();
            }
        }
        private int hash;
        public static implicit operator T(Protected<T> val)
        {
            return val.Value;
        }
    }
    public static class DataStruct
    {
        private class RedBlackTree<T>
        {
            private RedBlackTreeNode<T> mRoot;
            private readonly Comparer<T> mComparer;
            private const bool RED = true;
            private const bool BLACK = false;

            public RedBlackTree()
            {
                mRoot = null;
                mComparer = Comparer<T>.Default;
            }

            public bool Contain(T value)
            {
                if (value == null)
                    throw new ArgumentNullException();
                RedBlackTreeNode<T> node = mRoot;
                while (node != null)
                {
                    int comparer = mComparer.Compare(value, node.Data);
                    if (comparer > 0)
                        node = node.RightChild;
                    else if (comparer < 0)
                        node = node.LeftChild;
                    else return true;
                }
                return false;
            }

            public void Add(T value)
            {
                count++;
                if (mRoot == null)
                {
                    // 根节点是黑色的
                    mRoot = new RedBlackTreeNode<T>(value, BLACK);
                }
                else
                {
                    // 新插入节点是红色的
                    Insert1(new RedBlackTreeNode<T>(value, RED), value);
                }
            }

            private void Insert1(RedBlackTreeNode<T> newNode, T value)
            {
                RedBlackTreeNode<T> node = mRoot;
                RedBlackTreeNode<T> parent = null;
                while (node != null)
                {
                    parent = node;
                    int comparer = mComparer.Compare(value, node.Data);
                    if (comparer > 0)
                        node = node.RightChild;
                    else if (comparer < 0)
                        node = node.LeftChild;
                    else
                    {
                        node.Count++;
                        return;
                    }
                }
                newNode.Parent = parent;
                int comparer1 = mComparer.Compare(value, parent.Data);
                if (comparer1 > 0)
                    parent.RightChild = newNode;
                else if (comparer1 < 0)
                    parent.LeftChild = newNode;
                InsertFixUp(newNode);
            }

            private void InsertFixUp(RedBlackTreeNode<T> newNode)
            {
                RedBlackTreeNode<T> parent = newNode.Parent;
                RedBlackTreeNode<T> gParent;
                while (IsRed(parent) && parent != null)
                {
                    gParent = parent.Parent;
                    if (parent == gParent.LeftChild)
                    {
                        RedBlackTreeNode<T> uncle = gParent.RightChild;

                        if (uncle != null && IsRed(uncle))
                        {
                            parent.Color = BLACK;
                            uncle.Color = BLACK;
                            gParent.Color = RED;
                            newNode = gParent;
                            parent = newNode.Parent;
                            continue;
                        }

                        if (newNode == parent.RightChild)
                        {
                            RotateLeft(parent);
                            RedBlackTreeNode<T> tmp = parent;
                            parent = newNode;
                            newNode = tmp;
                        }

                        parent.Color = BLACK;
                        gParent.Color = RED;
                        RotateRight(gParent);
                    }
                    else
                    {
                        RedBlackTreeNode<T> uncle = gParent.LeftChild;

                        if (uncle != null & IsRed(uncle))
                        {
                            parent.Color = BLACK;
                            uncle.Color = BLACK;
                            gParent.Color = RED;
                            newNode = gParent;
                            parent = newNode.Parent;
                            continue;
                        }

                        if (newNode == parent.LeftChild)
                        {
                            RotateRight(parent);
                            RedBlackTreeNode<T> tmp = parent;
                            parent = newNode;
                            newNode = tmp;
                        }

                        parent.Color = BLACK;
                        gParent.Color = RED;
                        RotateLeft(gParent);
                    }
                }
                mRoot.Color = BLACK;
            }

            private static bool IsRed(RedBlackTreeNode<T> node)
            {
                return node != null && node.Color == RED;
            }

            private static bool IsBlack(RedBlackTreeNode<T> node)
            {
                return node != null && node.Color == BLACK;
            }

            private void RotateLeft(RedBlackTreeNode<T> x)
            {
                RedBlackTreeNode<T> y = x.RightChild;
                x.RightChild = y.LeftChild;

                if (y.LeftChild != null)
                    y.LeftChild.Parent = x;

                if (x.Parent != null)
                    y.Parent = x.Parent;

                if (x.Parent == null)
                {
                    mRoot = y;
                    y.Parent = null;
                }
                else
                {
                    if (x == x.Parent.LeftChild)
                        x.Parent.LeftChild = y;
                    else x.Parent.RightChild = y;
                }

                y.LeftChild = x;
                x.Parent = y;
            }

            private void RotateRight(RedBlackTreeNode<T> y)
            {
                RedBlackTreeNode<T> x = y.LeftChild;
                y.LeftChild = x.RightChild;

                if (x.RightChild != null)
                    x.RightChild.Parent = y;

                if (y.Parent != null)
                    x.Parent = y.Parent;
                if (y.Parent == null)
                {
                    mRoot = x;
                    x.Parent = null;
                }
                else
                {
                    if (y == y.Parent.RightChild)
                        y.Parent.RightChild = x;
                    else y.Parent.LeftChild = x;
                }

                x.RightChild = y;
                y.Parent = x;
            }

            public int Count
            {
                get
                {
                    return count;
                }
            }

            private int count = 0;

            public int TimesOf(T value)
            {
                if (value == null)
                    throw new ArgumentNullException();
                RedBlackTreeNode<T> node = mRoot;
                while (node != null)
                {
                    int comparer = mComparer.Compare(value, node.Data);
                    if (comparer > 0)
                        node = node.RightChild;
                    else if (comparer < 0)
                        node = node.LeftChild;
                    else return node.Count;
                }
                return 0;
            }

            public int Depth
            {
                get
                {
                    return GetHeight(mRoot);
                }
            }

            private int GetHeight(RedBlackTreeNode<T> root)
            {
                if (root == null) return 0;
                int leftHight = GetHeight(root.LeftChild);
                int rightHight = GetHeight(root.RightChild);
                return leftHight > rightHight ? leftHight + 1 : rightHight + 1;
            }

            public T Max
            {
                get
                {
                    RedBlackTreeNode<T> node = mRoot;
                    while (node.RightChild != null)
                        node = node.RightChild;
                    return node.Data;
                }
            }

            public T Min
            {
                get
                {
                    if (mRoot != null)
                    {
                        RedBlackTreeNode<T> node = GetMinNode(mRoot);
                        return node.Data;
                    }
                    else return default(T);
                }
            }

            public void DelMin()
            {
                count--;
                mRoot = DelMin(mRoot);
            }

            private RedBlackTreeNode<T> DelMin(RedBlackTreeNode<T> node)
            {
                if (node.LeftChild == null)
                    return node.RightChild;
                node.LeftChild = DelMin(node.LeftChild);
                return node;
            }

            public void Remove(T value)
            {
                bool type;
                mRoot = Delete(mRoot, value, out type);
                if (type) count--;
            }

            private RedBlackTreeNode<T> Delete(RedBlackTreeNode<T> node, T value, out bool type)
            {
                type = false;
                if (node == null)
                    return null;
                int comparer = mComparer.Compare(value, node.Data);
                if (comparer > 0)
                    node.RightChild = Delete(node.RightChild, value, out type);
                else if (comparer < 0)
                    node.LeftChild = Delete(node.LeftChild, value, out type);
                else
                {
                    type = true;
                    if (node.Count > 1)
                    {
                        node.Count--;
                        return node;
                    }
                    if (node.LeftChild == null)
                    {
                        if (node.RightChild != null)
                            node.RightChild.Parent = node.Parent;
                        return node.RightChild;
                    }
                    else if (node.RightChild == null)
                    {
                        if (node.LeftChild != null)
                            node.LeftChild.Parent = node.Parent;
                        return node.LeftChild;
                    }
                    else
                    {
                        RedBlackTreeNode<T> child;
                        RedBlackTreeNode<T> parent;
                        bool color;
                        RedBlackTreeNode<T> replace = node;
                        replace = GetMinNode(replace.RightChild);

                        if (node.Parent != null)
                        {
                            if (node == node.Parent.LeftChild)
                                node.Parent.LeftChild = replace;
                            else node.Parent.RightChild = replace;
                        }
                        else mRoot = replace;

                        child = replace.RightChild;
                        parent = replace.Parent;
                        color = replace.Color;

                        if (parent == node)
                            parent = replace;
                        else
                        {
                            if (child != null)
                                child.Parent = parent;
                            parent.LeftChild = child;
                            replace.RightChild = node.RightChild;
                            node.RightChild.Parent = replace;
                        }
                        replace.Parent = node.Parent;
                        replace.Color = node.Color;
                        replace.LeftChild = node.LeftChild;
                        node.LeftChild.Parent = replace;

                        if (color == BLACK)
                            RemoveFixUp(child, parent);
                        return replace;
                    }
                }
                return node;
            }

            private void RemoveFixUp(RedBlackTreeNode<T> node, RedBlackTreeNode<T> parent)
            {
                RedBlackTreeNode<T> brother;
                while ((node == null || IsBlack(node)) && (node != mRoot))
                {
                    if (parent.LeftChild == node)
                    {
                        brother = parent.RightChild;
                        if (IsRed(brother))
                        {
                            brother.Color = BLACK;
                            parent.Color = RED;
                            RotateLeft(parent);
                            brother = parent.RightChild;
                        }

                        if ((brother.LeftChild == null || IsBlack(brother.LeftChild)) &&
                            (brother.RightChild == null || IsBlack(brother.RightChild)))
                        {
                            brother.Color = RED;
                            node = parent;
                            parent = node.Parent;
                        }
                        else
                        {
                            if (brother.RightChild == null || IsBlack(brother.RightChild))
                            {
                                brother.LeftChild.Color = BLACK;
                                brother.Color = RED;
                                RotateRight(brother);
                                brother = parent.RightChild;
                            }

                            brother.Color = parent.Color;
                            parent.Color = BLACK;
                            brother.RightChild.Color = BLACK;
                            RotateLeft(parent);
                            node = mRoot;
                            break;
                        }
                    }
                    else
                    {
                        brother = parent.LeftChild;
                        if (IsRed(brother))
                        {
                            brother.Color = BLACK;
                            parent.Color = RED;
                            RotateRight(parent);
                            brother = parent.LeftChild;
                        }
                        if ((brother.LeftChild == null || IsBlack(brother.LeftChild)) &&
                            (brother.RightChild == null || IsBlack(brother.RightChild)))
                        {
                            brother.Color = RED;
                            node = parent;
                            parent = node.Parent;
                        }
                        else
                        {
                            if (brother.LeftChild == null || IsBlack(brother.LeftChild))
                            {
                                brother.RightChild.Color = BLACK;
                                brother.Color = RED;
                                RotateLeft(brother);
                                brother = parent.LeftChild;
                            }

                            brother.Color = parent.Color;
                            parent.Color = BLACK;
                            brother.LeftChild.Color = BLACK;
                            RotateRight(parent);
                            node = mRoot;
                            break;
                        }
                    }
                }
                if (node != null) node.Color = BLACK;
            }

            private RedBlackTreeNode<T> GetMinNode(RedBlackTreeNode<T> node)
            {
                while (node.LeftChild != null)
                    node = node.LeftChild;
                return node;
            }

            public class RedBlackTreeNode<TKey>
            {
                public TKey Data { get; init; }
                public RedBlackTreeNode<TKey> LeftChild { get => leftChild; set { leftChild = value; } }
                public RedBlackTreeNode<TKey> RightChild { get => rightChild; set { rightChild = value; } }

                private RedBlackTreeNode<TKey> leftChild;
                private RedBlackTreeNode<TKey> rightChild;

                public RedBlackTreeNode<TKey> Parent { get; set; }
                public bool Color { get; set; }
                public int Count { get => count; set { count = value; } }

                private int count = 1;

                public RedBlackTreeNode(TKey value, bool color)
                {
                    Data = value;
                    LeftChild = null;
                    RightChild = null;
                    Color = color;
                }
            }
        }
    }
}