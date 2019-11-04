using System;
using System.Collections.Generic;
using cui.Abstractions;
using cui.Interfaces;
using cui.Internal.Helpers;

namespace cui.Controls
{
    public class ComboBox : ControlBase, IHasValue<IList<string>>, IHasIndex, ILeftRight, IPressable, IOtherKey
    {
        public ComboBox(string name, IList<string> items)
            : base(name)
        {
            Value = items;
        }
        
        public IList<string> Value { get; set; }
        public int Index { get; set; }
        public string SelectedItem => Value[Index];
        
        public void Left(ConsoleKeyInfo info)
        {
            if (Index > 0) Index--;
        }

        public void Right(ConsoleKeyInfo info)
        {
            if (Index < Value.Count - 1) Index++;
        }

        public void Pressed(ConsoleKeyInfo info)
        {
            var helper = new ComboBoxHelperMenu(this);
            Index = helper.GetNewIndex();
        }

        public void OtherKey(ConsoleKeyInfo info)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (info.Key)
            {
                case ConsoleKey.Spacebar:
                case ConsoleKey.Enter:
                    Pressed(info);
                    break;
            }
        }

        public override void DrawControl(bool selected)
        {
            Console.Write(Name + " ");
            ConsoleColorHelper.Write("/ ", ConsoleColor.Cyan);
            ConsoleColorHelper.Write(SelectedItem, ConsoleColor.Yellow);
            ConsoleColorHelper.Write(" /", ConsoleColor.Cyan);
            Console.WriteLine();
        }

        protected override int GetHash()
        {
            return 31 * Name.GetHashCode() + Value.GetHashCode() + Index.GetHashCode();
        }
    }
}