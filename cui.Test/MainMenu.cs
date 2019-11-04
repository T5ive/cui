using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using cui.Abstractions;
using cui.Controls;

namespace cui.Test
{
    public class MainMenu : MenuBase
    {
        public MainMenu() : base("Main Menu")
        {
            Controls.Add(new Label("Test label"));
            Controls.Add(new Button("Async test button", async sender =>
            {
                await Task.Delay(5000);
                Controls[0].Name = "Async label";
            }));
            Controls.Add(new Button("Non-async test button", sender =>
            {
                Thread.Sleep(5000);
                Controls[0].Name = "Non-async label";
            }));
            Controls.Add(new Checkbox("Test checkbox", true));
            Controls.Add(new Slider("Test slider", 0, 1, 0, 100));
            Controls.Add(new TextBox("Test textbox", "Some text"));
            Controls.Add(new TextBox("Test password field", "password", true));
            Controls.Add(new ComboBox("Test combobox", new List<string>
            {
                "Item1",
                "Item2",
                "Item3",
                "Item4",
                "Item5"
            }));
            Controls.Add(new Submenu());
        }

        class Submenu : MenuBase
        {
            readonly ProgressBar _bar = new ProgressBar("Ayylmao");
            
            public Submenu() : base("Just a submenu")
            {
                Controls.AddRange(new ControlBase[]
                {
                    new Checkbox("Temporary checkbox"),
                    new Label("Or wait, do they keep their value?"),
                    new Button("You can go back also, using this button", sender => Close()),
                    new Button("Increment progressbar to 100", async sender =>
                    {
                        for (var i = 0; i < 100; i++)
                        {
                            await Task.Delay(50);
                            _bar.Value++;
                        }
                    }),
                    new Button("Decrement progressbar to 0", async sender =>
                    {
                        for (var i = 0; i < 100; i++)
                        {
                            await Task.Delay(50);
                            _bar.Value--;
                        }
                    }),
                    _bar
                });
            }
        }
    }
}