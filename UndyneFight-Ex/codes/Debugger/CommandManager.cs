using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace UndyneFight_Ex.Debugging
{
    internal partial class DebugWindow
    {
        internal static Vector2 CursorPosition { get; private set; }
        internal partial class StringBlock
        {
            public StringBlock(string data)
            {
                _data = data;
                _type = WordType.Unknown;
                CheckType();
            }
            private string _data;
            private WordType _type;

            private Color DrawingColor => StyleManager.ColorStyleManager.GetColor(_type);

            public void Insert(int pos, string imf)
            {
                _data = _data.Insert(pos, imf);
                CheckType();
            }
            public void DeleteFront(int pos, int length)
            {
                if (length == 0) return;
                _data = _data.Remove(pos, length);
                CheckType();
            }
            public void DeleteBack(int pos, int length)
            {
                if (_data.Length > 0)
                    _data = _data.Remove(pos - length, length);
                CheckType();
            }

            public float Draw(Vector2 start)
            {
                debugFont.Draw(_data, start, DrawingColor, instance._spriteBatch);
                return debugFont.SFX.MeasureString(_data).X;
            }
            public float DrawWith2Cursor(Vector2 start, int position1, int position2)
            {
                string part1 = _data[..position1], part2 = _data[position1..position2], part3 = _data[position2..];
                Vector2 pos1 = start;
                Vector2 pos2 = pos1; pos2.X += debugFont.SFX.MeasureString(part1).X;
                Vector2 pos3 = pos2; pos3.X += debugFont.SFX.MeasureString(part2).X;

                debugFont.Draw(part1, pos1, DrawingColor, instance._spriteBatch);
                debugFont.Draw(part2, pos2, Color.Pink, instance._spriteBatch);
                debugFont.Draw(part3, pos3, DrawingColor, instance._spriteBatch);
                return debugFont.SFX.MeasureString(_data).X;
            }
            public float DrawWithCursor(Vector2 start, int position, CursorPositionType cursorPositionType)
            {
                string part1 = _data[..position], part2 = _data[position..];
                Vector2 pos1 = start, pos2 = pos1; pos2.X += debugFont.SFX.MeasureString(part1).X;
                Vector2 cursorPos = pos2 + new Vector2(-2, 2);
                debugFont.Draw(part1, pos1,
                    (cursorPositionType is CursorPositionType.Full or CursorPositionType.Left)
                    ? Color.LightPink : DrawingColor, instance._spriteBatch);
                debugFont.Draw(part2, pos2,
                    (cursorPositionType is CursorPositionType.Full or CursorPositionType.Right)
                    ? Color.LightPink : DrawingColor, instance._spriteBatch);
                if (cursorPositionType == CursorPositionType.Point && DrawCursor && CommandManager.HasCommand)
                    instance._spriteBatch.Draw(GlobalResources.Sprites.cursor, cursorPos, Color.LightSteelBlue);
                return debugFont.SFX.MeasureString(_data).X;
            }

            public float GetCursorPosition(int position)
            {
                string part = _data[..position];
                return debugFont.SFX.MeasureString(part).X;
            }

            public static implicit operator string(StringBlock _string)
            {
                return _string._data;
            }

            internal enum CursorPositionType
            {
                Point = 1,
                Left = 2,
                Right = 3,
                Full = 4
            }
            public int Length => _data.Length;
        }
        internal static class CommandManager
        {
            private static readonly LinkedList<StringBlock> allBlocks = new LinkedList<StringBlock>();
            private static StringBlock CurrentBlock { get => blockEnumerator1.Value; }
            private static LinkedListNode<StringBlock> blockEnumerator1, blockEnumerator2;
            private static int positionInBlock1, positionInBlock2;
            public static LinkedListNode<StringBlock> MainCommand { get; private set; }
            public static bool HasCommand => allBlocks.First.Value.Length > 0;

            public static void Initialize()
            {
                allBlocks.AddLast(new LinkedListNode<StringBlock>(new("")));
                blockEnumerator1 = allBlocks.First;
                blockEnumerator2 = allBlocks.First;
                MainCommand = blockEnumerator1;
            }
            public static void Insert(string val)
            {
                if (string.IsNullOrEmpty(val)) return;
                InternalCommandSystem.IsNewCommand = true;
                DeleteInCursor();
                CurrentBlock.Insert(positionInBlock1, val);
                positionInBlock1 += val.Length;
                positionInBlock2 = positionInBlock1;
                bool res = CurrentBlock.CheckMixed();
                if (res)
                {
                    StringBlock[] blocks = CurrentBlock.Split();
                    for (int i = blocks.Length - 1; i >= 0; i--)
                        allBlocks.AddAfter(blockEnumerator1, new LinkedListNode<StringBlock>(blocks[i]));
                    var tmp = blockEnumerator1.Next;
                    if (blockEnumerator1 == MainCommand) MainCommand = tmp;
                    allBlocks.Remove(blockEnumerator1);
                    blockEnumerator1 = tmp;
                    while (positionInBlock1 > blockEnumerator1.Value.Length)
                    {
                        positionInBlock1 -= blockEnumerator1.Value.Length;
                        blockEnumerator1 = blockEnumerator1.Next;
                        if (blockEnumerator1 == null) break;
                    }
                    positionInBlock2 = positionInBlock1;
                    blockEnumerator2 = blockEnumerator1;
                }
                if (blockEnumerator1.Next != null)
                    TryMerge(blockEnumerator1, blockEnumerator1.Next);
            }
            public static void DeleteInCursor()
            {
                if (blockEnumerator1 == blockEnumerator2)
                {
                    if (positionInBlock1 == positionInBlock2) return;
                    int min = Math.Min(positionInBlock1, positionInBlock2), max = Math.Max(positionInBlock1, positionInBlock2);
                    positionInBlock1 = min;
                    CurrentBlock.DeleteFront(positionInBlock1, max - min);
                }
                else
                {
                    throw new NotImplementedException();
                }
                positionInBlock2 = positionInBlock1;
                blockEnumerator2 = blockEnumerator1;
            }
            public static void DeleteBack()
            {
                if (positionInBlock1 != positionInBlock2 || blockEnumerator1 != blockEnumerator2)
                {
                    InternalCommandSystem.IsNewCommand = true;
                    DeleteInCursor();
                    return;
                }
                if (positionInBlock1 == 0)
                {
                    if (blockEnumerator1.Previous == null) return;
                    CursorLeft();
                    CurrentBlock.DeleteFront(positionInBlock1, 1);
                    InternalCommandSystem.IsNewCommand = true;
                    return;
                }
                InternalCommandSystem.IsNewCommand = true;
                CurrentBlock.DeleteBack(positionInBlock1, 1);
                CursorLeft();
                if (positionInBlock1 == blockEnumerator1.Value.Length && blockEnumerator1.Next != null)
                    TryMerge(blockEnumerator1, blockEnumerator1.Next);
            }
            /// <summary>
            /// 将两个块合并。其中node1将会被保留，将node2的字符串加入，而node2将会被删除。
            /// </summary>
            /// <param name="node1"></param>
            /// <param name="node2"></param>
            public static void TryMerge(LinkedListNode<StringBlock> node1, LinkedListNode<StringBlock> node2)
            {
                if (node1.Value.IsMergeAvailable(node2.Value))
                {
                    node1.Value.Merge(node2.Value);
                    if (blockEnumerator1 == node2)
                    {
                        blockEnumerator1 = blockEnumerator2 = node1;
                        positionInBlock1 += node1.Value.Length;
                        positionInBlock2 = positionInBlock1;
                    }
                    allBlocks.Remove(node2);
                }
            }
            public static void CursorLeft()
            {
                positionInBlock1--;
                if (positionInBlock1 <= 0)
                {
                    if (blockEnumerator1.Previous == null)
                    {
                        positionInBlock1 = 0;
                        if (!KeyInputManager.ShiftPressed)
                        {
                            blockEnumerator2 = blockEnumerator1;
                            positionInBlock2 = positionInBlock1;
                        }
                        return;
                    }
                    blockEnumerator1 = blockEnumerator1.Previous;
                    positionInBlock1 = CurrentBlock.Length;
                }
                if (!KeyInputManager.ShiftPressed)
                {
                    blockEnumerator2 = blockEnumerator1;
                    positionInBlock2 = positionInBlock1;
                }
            }
            public static void CursorRight()
            {
                positionInBlock1++;
                if (positionInBlock1 > blockEnumerator1.Value.Length)
                {
                    if (blockEnumerator1.Next == null)
                    {
                        positionInBlock1--;
                        if (!KeyInputManager.ShiftPressed)
                        {
                            positionInBlock2 = positionInBlock1;
                            blockEnumerator2 = blockEnumerator1;
                        }
                        return;
                    }
                    blockEnumerator1 = blockEnumerator1.Next;
                    positionInBlock1 = 1;
                }
                if (!KeyInputManager.ShiftPressed)
                {
                    blockEnumerator2 = blockEnumerator1;
                    positionInBlock2 = positionInBlock1;
                }
            }
            public static void MouseClick(Vector2 pos)
            {

            }
            public static void MouseRelease(Vector2 pos)
            {

            }
            internal static void RenderCommand(Vector2 startPosition, LinkedListNode<StringBlock> command)
            {
                LinkedListNode<StringBlock> current = command;
                Vector2 pos = new(startPosition.X - InternalCommandSystem.RenderOffset, startPosition.Y);
                Vector2 realCursorPos = startPosition;
                bool inCursor = false;
                float adapt = 0;
                bool isFixed = false;
                bool cursor1Found = false;
                while (current != null)
                {
                    if (current == blockEnumerator1 || current == blockEnumerator2)
                    {
                        if (blockEnumerator1 == blockEnumerator2)
                        {
                            adapt = positionInBlock1 == positionInBlock2
                                ? current.Value.DrawWithCursor(pos, positionInBlock1, StringBlock.CursorPositionType.Point)
                                : current.Value.DrawWith2Cursor(
                                    pos,
                                    Math.Min(positionInBlock1, positionInBlock2),
                                    Math.Max(positionInBlock1, positionInBlock2)
                                );
                        }
                        else
                        {
                            inCursor = !inCursor;
                            adapt =
                                current.Value.DrawWithCursor(
                                    pos,
                                    current == blockEnumerator1 ? positionInBlock1 : positionInBlock2,
                                    inCursor ? StringBlock.CursorPositionType.Right : StringBlock.CursorPositionType.Left
                                );
                        }
                    }
                    else
                    {
                        adapt = inCursor ? current.Value.DrawWithCursor(pos, 0, StringBlock.CursorPositionType.Full) : current.Value.Draw(pos);
                    }

                    if (current == blockEnumerator1)
                    {
                        realCursorPos.X += current.Value.GetCursorPosition(positionInBlock1);
                        cursor1Found = true;
                    }
                    current = current.Next;
                    pos.X += adapt;
                    if (!cursor1Found)
                        realCursorPos.X += adapt;
                    else
                    {
                        if (isFixed) continue;
                        isFixed = true;
                        if (realCursorPos.X + 5 > InternalCommandSystem.RenderOffset + ScreenSize.X)
                            InternalCommandSystem.RenderOffset = (realCursorPos.X + 5 - ScreenSize.X) * 0.1f + InternalCommandSystem.RenderOffset * 0.9f;
                        if (realCursorPos.X < InternalCommandSystem.RenderOffset + 24)
                            InternalCommandSystem.RenderOffset = (realCursorPos.X - 24) * 0.1f + InternalCommandSystem.RenderOffset * 0.9f;
                    }
                }
            }

            private static class InternalCommandSystem
            {
                public static bool IsNewCommand { get; set; } = false;
                public static float RenderOffset { get; set; } = 0;
            }

            public static void Update()
            {

            }
        }
    }
}
