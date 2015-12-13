using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GuildChatWPF
{
    class StartClient
    {
        HidePanel panel;
        Log log;
        MainWindow con;
        TcpClient tcpclnt;
        Thread thread;

        public StartClient(MainWindow c,
            Panel panel1, Panel panel2)
        {
            panel = new HidePanel(con, panel1, panel2);
            log = new Log();
            con = c;
            //thread = th;
        }

        public void start(string ip_adress, Action nasluch,
            Label tsslInfo, Button btnLoguj)
        {
            if (!tsslInfo.CheckAccess() && !btnLoguj.CheckAccess())
            {
                con.Dispatcher.Invoke(DispatcherPriority.Send,
                (Action)delegate
                {
                    btnLoguj.IsDefault = true;
                    tsslInfo.Content = "Trwa łączenie.....";
                    try
                    {
                        tcpclnt = new TcpClient();
                        tcpclnt.Connect(IPAddress.Parse(ip_adress), 9999);
                        //tcpclnt.Connect("127.0.0.1", 9999);
                        thread = new Thread(new ThreadStart(nasluch)) { IsBackground = true };
                        thread.Start();
                    }

                    catch (Exception e)
                    {
                        tsslInfo.Content = "Error ";
                        log.nextLog();
                        log.write(e.ToString());
                    }
                });
            }
            else
            {
                btnLoguj.IsDefault = true;
                tsslInfo.Content = "Trwa łączenie.....";
                try
                {
                    tcpclnt = new TcpClient();
                    tcpclnt.Connect(IPAddress.Parse(ip_adress), 9999);
                    //tcpclnt.Connect("127.0.0.1", 9999);
                    
                    thread = new Thread(new ThreadStart(nasluch)) { IsBackground = true };
                    thread.Start();
                }

                catch (Exception e)
                {
                    tsslInfo.Content = "Error ";
                    log.nextLog();
                    log.write(e.ToString());
                }
            }
        }

        public TcpClient retClient()
        {
            return tcpclnt;
        }

        public void reconnect(string ip_adress, Action nasluch, Label tsslInfo, Button btnLoguj)
        {
            if (tcpclnt.Connected)
            {
                tcpclnt.Close();
                tcpclnt = new TcpClient();
                thread.Abort();
            }
            panel.hide(false);
            start(ip_adress, nasluch, tsslInfo, btnLoguj);
        }
    }
}
