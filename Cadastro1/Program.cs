using System;
using System.Windows.Forms;

namespace Cadastro1
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMenuPrincipal());
        }
    }
}
