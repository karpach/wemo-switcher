using System;
using System.Threading;
using System.Windows.Forms;

namespace Karpach.Wemo.Switcher
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WemoApplicationContext());
        }        
    }
}
