using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GuildChatWPF
{
    class FillTo
    {
        public void fill(MainWindow con, TextBox tdo, string str)
        {
            if (!con.CheckAccess())
            {
                con.Dispatcher.Invoke(DispatcherPriority.Send,
                (Action)delegate
                {
                    if (tdo.Text == "")
                    {
                        tdo.Text = str;
                    }
                });
            }
            else
            {
                if (tdo.Text == "")
                {
                    tdo.Text = str;
                }
            }
        }

    }
}
