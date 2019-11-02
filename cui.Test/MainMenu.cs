using System;
using cui.Abstractions;
using cui.Controls;

namespace cui.Test
{
    public class MainMenu : MenuBase
    {
        public MainMenu() : base("Main Menu")
        {
            AddControl(new Label("Test label"));
            AddControl(new Button("Test button", sender =>
            {
                Console.Beep();
            }));
            AddControl(new Checkbox("Test checkbox"));
            AddControl(new Slider<int>("Test slider<int>", 10, 1, 0, 100));
            AddControl(new Temp());
        }

        class Temp : MenuBase
        {
            public Temp() : base("Temp") { }
        }
    }
}