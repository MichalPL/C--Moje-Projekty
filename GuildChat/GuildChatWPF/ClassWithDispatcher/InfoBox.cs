using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GuildChatWPF
{
    class InfoBox
    {
        public void writeInfo(MainWindow con, Label tsslInfo, string text)
        {
            if (!tsslInfo.CheckAccess())
            {
                con.Dispatcher.Invoke(DispatcherPriority.Send,
                (Action)delegate
                {
                    tsslInfo.Content = text;
                });
            }
            else
                tsslInfo.Content = text;
        }
    }
}
