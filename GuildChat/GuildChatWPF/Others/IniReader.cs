using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GuildChatWPF
{
    class IniReader
    {
        public void loadFromIni(TextBox tbLogin, PasswordBox tbHaslo, CheckBox cbZapamietaj)
        {
            Log log = new Log();
            if (File.Exists("settings.ini"))
            {
                try
                {
                    tbLogin.Text = File.ReadLines("settings.ini").Skip(0).Take(1).First();
                    tbHaslo.Password = File.ReadLines("settings.ini").Skip(1).Take(1).First();
                    cbZapamietaj.IsChecked = true;
                }
                catch (Exception e)
                {
                    log.nextLog();
                    log.write(e.ToString());
                }
            }
        }
    }
}
