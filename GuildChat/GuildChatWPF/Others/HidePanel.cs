using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GuildChatWPF
{
    class HidePanel
    {
        MainWindow con;
        Panel panel1;
        Panel panel2;
        public HidePanel(MainWindow c, Panel p1, Panel p2)
        {
            con = c;
            panel1 = p1;
            panel2 = p2;
        }
        public void hide(bool wart)
        {
            if (!panel1.CheckAccess() && !panel2.CheckAccess())
            {
                if (con == null) return;
                con.Dispatcher.Invoke(DispatcherPriority.Send,
                (Action)delegate
                {
                    Visibility stan;
                    if (wart == true)
                        stan = System.Windows.Visibility.Hidden;
                    else
                        stan = System.Windows.Visibility.Visible;
                    panel1.Visibility = stan;
                    if (wart == false)
                        stan = System.Windows.Visibility.Hidden;
                    else
                        stan = System.Windows.Visibility.Visible;
                    panel2.Visibility = stan;
                    //btnWyslij.IsDefault = true;
                });
            }
            else
            {

                Visibility stan;
                if (wart == true)
                    stan = System.Windows.Visibility.Hidden;
                else
                    stan = System.Windows.Visibility.Visible;
                panel1.Visibility = stan;
                if (wart == false)
                    stan = System.Windows.Visibility.Hidden;
                else
                    stan = System.Windows.Visibility.Visible;
                panel2.Visibility = stan;
                //btnWyslij.IsDefault = true;
            }
        }
    }
}
