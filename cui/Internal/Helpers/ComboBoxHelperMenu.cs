using cui.Abstractions;
using cui.Controls;
using cui.Interfaces;
using System;

namespace cui.Internal.Helpers;

internal class ComboBoxHelperMenu : MenuBase
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

    private class ComboBoxItem : Label, IPressable
    {
        public ComboBoxItem(string name, IMenu parent) : base(name)
        {
            _parent = parent;
        }

        private readonly IMenu _parent;

        public void Pressed(ConsoleKeyInfo info)
        {
            _parent.Close();
        }
    }
}