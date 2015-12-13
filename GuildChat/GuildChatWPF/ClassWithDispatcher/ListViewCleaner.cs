using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GuildChatWPF
{
    class ListViewCleaner
    {
        public void clear(MainWindow con, ListView lv)
        {
            if (!lv.CheckAccess())
            {
                con.Dispatcher.Invoke(DispatcherPriority.Send,
                (Action)delegate
                {
                    lv.Items.Clear();
                });
            }
            else
            {
                lv.Items.Clear();
            }
        }
    }
}
