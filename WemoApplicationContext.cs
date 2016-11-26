using System;
using System.Windows.Forms;

namespace Karpach.Wemo.Switcher
{
    public class WemoApplicationContext: ApplicationContext
    {
        private readonly NotifyIcon _trayIcon;
        private readonly WeMoSwitch _switch;
        private readonly KeyboardHook _hookOn;
        private readonly KeyboardHook _hookOff;

        public WemoApplicationContext()
        {
            var notifyContextMenu = new ContextMenuStrip();
            var lightOn = new ToolStripMenuItem("Light On")
            {
                Image = Resources.Red.ToBitmap()
            };
            lightOn.Click += SwitchOn;
            lightOn.ShortcutKeyDisplayString = "Ctrl+Alt+Shift+Win+Up";            
            notifyContextMenu.Items.Add(lightOn);

            var lightOff = new ToolStripMenuItem("Light Off")
            {
                Image = Resources.Grey.ToBitmap()
            };
            lightOff.Click += SwitchOff;
            lightOff.ShortcutKeyDisplayString = "Ctrl+Alt+Shift+Win+Down";
            notifyContextMenu.Items.Add(lightOff);

            notifyContextMenu.Items.Add("-");

            var exit = new ToolStripMenuItem("Exit")
            {
                Image = Resources.Exit.ToBitmap()
            };
            exit.Click += Exit;            
            notifyContextMenu.Items.Add(exit);


            // Initialize Tray Icon            
            _trayIcon = new NotifyIcon
            {
                Icon = Resources.AppIcon,
                ContextMenuStrip = notifyContextMenu,
                Visible = true
            };            
            _switch = WeMoSwitch.ConnectTo("WeMo Switch");

            _hookOn = new KeyboardHook();
            _hookOn.KeyPressed += SwitchOn;
            _hookOn.RegisterHotKey(ModifierKeys.Win | ModifierKeys.Alt | ModifierKeys.Control | ModifierKeys.Shift, Keys.Up);

            _hookOff = new KeyboardHook();
            _hookOff.KeyPressed += SwitchOff;
            _hookOff.RegisterHotKey(ModifierKeys.Win | ModifierKeys.Alt | ModifierKeys.Control | ModifierKeys.Shift, Keys.Down);
        }

        void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            _trayIcon.Visible = false;

            Application.Exit();
        }

        void SwitchOn(object sender, EventArgs e)
        {
            _switch?.TurnOn();
        }

        void SwitchOff(object sender, EventArgs e)
        {
            _switch?.TurnOff();
        }
    }
}