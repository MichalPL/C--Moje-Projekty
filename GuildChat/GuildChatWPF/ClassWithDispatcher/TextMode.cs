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
    class TextMode
    {
        public void text(MainWindow con, TextBox tbw, ListBox lb, bool wart)
        {

            if (!tbw.CheckAccess() && !lb.CheckAccess())
            {
                con.Dispatcher.Invoke(DispatcherPriority.Send,
                (Action)delegate
                {
                    Visibility stan;
                    if (wart == true)
                        stan = System.Windows.Visibility.Hidden;
                    else
                        stan = System.Windows.Visibility.Visible;
                    lb.Visibility = stan;
                    if (wart == false)
                        stan = System.Windows.Visibility.Hidden;
                    else
                        stan = System.Windows.Visibility.Visible;
                    tbw.Visibility = stan;
                });
            }
            else
            {
                Visibility stan;
                if (wart == true)
                    stan = System.Windows.Visibility.Hidden;
                else
                    stan = System.Windows.Visibility.Visible;
                lb.Visibility = stan;
                if (wart == false)
                    stan = System.Windows.Visibility.Hidden;
                else
                    stan = System.Windows.Visibility.Visible;
                tbw.Visibility = stan;
            }
        }
    }
}
