using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GuildChatWPF
{
    class FriendsChanger
    {
        ListView lvZnajomi;
        MainWindow con;
        TcpClient tcpclnt;
        Stream stream;
        Label tsslInfo;
        Loader loader;

        public FriendsChanger(MainWindow c, ListView lv, TcpClient tcpc, Stream stm, Label tssl)
        {
            lvZnajomi = lv;
            con = c;
            tcpclnt = tcpc;
            stream = stm;
            tsslInfo = tssl;
            loader = new Loader(con, tcpclnt, stream, tsslInfo);
        }

        public void add(string text1, string text2)
        {
            if (!lvZnajomi.CheckAccess())
            {
                con.Dispatcher.Invoke(DispatcherPriority.Send,
                (Action)delegate
                {
                    if (text2 == "online")
                        text2 = text2.ToUpper();
                    lvZnajomi.Items.Add(new User() { Nick = text1, Dost = text2 });
                });
            }
            else
            {
                if (text2 == "online")
                    text2 = text2.ToUpper();
                lvZnajomi.Items.Add(new User() { Nick = text1, Dost = text2 });
            }
        }

        public void change(string text1, string online)
        {
            Log log = new Log();
            if (!lvZnajomi.CheckAccess())
            {
                con.Dispatcher.Invoke(DispatcherPriority.Send,
                (Action)delegate
                {
                    try
                    {
                        var list = lvZnajomi.Items.OfType<User>().Select(Item => Item.Nick.ToString()).ToList();
                        User item = (User)lvZnajomi.Items.GetItemAt(list.IndexOf(text1));
                        if (online == "online")
                            online = online.ToUpper();
                        item.Dost = online;
                        lvZnajomi.Items.Refresh();
                    }
                    catch (Exception ex)
                    {
                        log.write(ex.ToString());
                        loader.loadFriends();
                    }
                });
            }
            else
            {
                try
                {
                    var list = lvZnajomi.Items.OfType<User>().Select(Item => Item.Nick.ToString()).ToList();
                    User item = (User)lvZnajomi.Items.GetItemAt(list.IndexOf(text1));
                    if (online == "online")
                        online = online.ToUpper();
                    item.Dost = online;
                    lvZnajomi.Items.Refresh();
                }
                catch (Exception ex)
                {
                    log.write(ex.ToString());
                    loader.loadFriends();
                }
            }

        }
    }
}
