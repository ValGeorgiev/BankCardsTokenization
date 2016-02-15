using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankCardsServerTokenization
{
    public static class StartApp
    {
        public static void Main()
        {
            //start application
            Application.EnableVisualStyles();
            Application.Run(new LoginPage());
        }
    }
}
