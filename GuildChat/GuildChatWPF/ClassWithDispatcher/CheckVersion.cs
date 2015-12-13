using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace GuildChatWPF
{
    class CheckVersion
    {
        public void check(string str, string wersja, MainWindow con)
        {
            if (str != wersja)
            {
                if (!con.CheckAccess())
                {
                    con.Dispatcher.Invoke(DispatcherPriority.Send,
                    (Action)delegate
                    {
                        con.Hide();
                    });
                }
                else
                    con.Hide();
                MessageBox.Show("Wersja chatu jest nieaktualna!", "Wymagany update");
                //tcpclnt.Close();
                Environment.Exit(0);
            }
        }

    }
}
