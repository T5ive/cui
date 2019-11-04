using System;
using System.Collections.Generic;
using System.Linq;
using cui.Abstractions;
using cui.Interfaces;
using cui.Internal.Helpers;

namespace cui.Controls
{
    public class TextBox : ControlBase, IHasValue<string>, IHasIndex, ILeftRight, IOtherKey
    {
        public TextBox(string name) : base(name) { }

        public TextBox(string name, string value)
            : this(name)
        {
            Value = value;
        }
        
        public TextBox(string name, string value, bool hidden, char hiddenChar = '*')
            : this(name, value)
        {
            _hidden = hidden;
            _hiddenChar = hiddenChar;
        }

        public string Value
        {
            get => new string(_content.ToArray());
            set => _content = value.ToCharArray().ToList();
        }
        
        public int Index { get; set; }

        readonly bool _hidden;
        readonly char _hiddenChar;
        IList<char> _content = new List<char>();

        public void Left(ConsoleKeyInfo info) => Index--;
        public void Right(ConsoleKeyInfo info) => Index++;

        void NormaliseIndex()
        {
            if (Index > _content.Count)
                Index = _content.Count;
            else if (Index < 0)
                Index = 0;
        }
        
        public override void DrawControl(bool selected)
        {
            NormaliseIndex();
            Console.Write(Name + " ");
            ConsoleColorHelper.Write("[  ", ConsoleColor.Cyan);
            for (var i = 0; i < _content.Count; i++)
            {
                ConsoleColorHelper.Write(_hidden ? _hiddenChar : _content[i], ConsoleColor.Yellow, selected ?
                    Index == i ?
                        ConsoleColor.Blue : Console.BackgroundColor
                    : Console.BackgroundColor);
            }

            if (selected && Index == _content.Count)
            {
                ConsoleColorHelper.Write('|', ConsoleColor.Yellow, ConsoleColor.Blue);
                ConsoleColorHelper.WriteLine(" ]", ConsoleColor.Cyan);
            }
            else ConsoleColorHelper.WriteLine("  ]", ConsoleColor.Cyan);
        }

        public void OtherKey(ConsoleKeyInfo info)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (info.Key)
            {
                case ConsoleKey.Home:
                    Index = 0;
                    break;
                case ConsoleKey.End:
                    Index = _content.Count;
                    break;
                case ConsoleKey.Backspace:
                    if (Index > 0)
                    {
                        _content.RemoveAt(Index - 1);
                        Index--;
                    }
                    break;
                case ConsoleKey.Delete:
                    if (Index < _content.Count)
                        _content.RemoveAt(Index);
                    break;
                default:
                    _content.Insert(Index, info.KeyChar);
                    Index++;
                    break;
            }
        }
    }
}