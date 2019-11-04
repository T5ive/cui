using System;
using cui.Abstractions;
using cui.Controls;
using cui.Interfaces;

namespace cui.Internal.Helpers
{
    class ComboBoxHelperMenu : MenuBase
    {
        internal ComboBoxHelperMenu(ComboBox box)
            : base("Press enter/escape to go back")
        {
            foreach (var value in box.Value)
            {
                Controls.Add(new ComboBoxItem(value, this));
            }

            Index = box.Index;
        }

        internal int GetNewIndex()
        {
            DrawMenu();
            return Index;
        }

        class ComboBoxItem : Label, IPressable
        {
            public ComboBoxItem(string name, IMenu parent) : base(name)
            {
                _parent = parent;
            }

            readonly IMenu _parent;
            
            public void Pressed(ConsoleKeyInfo info)
            {
                _parent.Close();
            }
        }
    }
}