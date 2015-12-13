using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GuildChatWPF
{
    class NewTextMessage
    {
        public void write(MainWindow con, TextBox tbWiad, string text)
        {
            if (!tbWiad.CheckAccess())
            {
                con.Dispatcher.Invoke(DispatcherPriority.Send,
                (Action)delegate
                {
                    tbWiad.Text += text + Environment.NewLine;
                });
            }
            else
                tbWiad.Text += text + Environment.NewLine;
        }
    }
}
