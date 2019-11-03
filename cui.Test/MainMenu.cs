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
            AddControl(new Label("Test label"));
            AddControl(new Button("Async test button", async sender =>
            {
                await Task.Delay(5000);
                Controls[0].Name = "Async label";
            }));
            AddControl(new Button("Non-async test button", sender =>
            {
                Thread.Sleep(5000);
                Controls[0].Name = "Non-async label";
            }));
            AddControl(new Checkbox("Test checkbox", true));
            AddControl(new Slider<int>("Test slider<int>", 10, 1, 0, 100));
            AddControl(new Slider<BigInteger>("Test slider<BigInteger>", 10, 1, 0, 100));
            AddControl(new TextBox("Test textbox", "Some text"));
            AddControl(new TextBox("Test password field", "password", true, '?'));
            AddControl(new Submenu());
        }

        class Submenu : MenuBase
        {
            public Submenu() : base("Temp")
            {
                AddControls(new ControlBase[]
                {
                    new Checkbox("Temporary checkbox"),
                    new Slider<double>("Temporary slider", 0, 1, 0, 100),
                    new Label("Or wait, do they keep their value?"), 
                });
            }
        }
    }
}