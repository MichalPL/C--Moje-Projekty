using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GuildChatWPF
{
    class NewMessage
    {
        public void show(MainWindow con, ListBox lb, string kto, string text, int tem)
        {
            if (!lb.CheckAccess())
            {
                con.Dispatcher.Invoke(DispatcherPriority.Send,
                (Action)delegate
                {
                    UserWiad itm = new UserWiad();
                    itm.Nick = kto;
                    itm.Wiad = text;
                    itm.Typ = tem;
                    lb.Items.Add(itm);
                    lb.ScrollIntoView(itm);
                });
            }
            else
            {
                UserWiad itm = new UserWiad();
                itm.Nick = kto;
                itm.Wiad = text;
                itm.Typ = tem;
                lb.Items.Add(itm);

                lb.ScrollIntoView(itm);
            }
        }
    }
}
