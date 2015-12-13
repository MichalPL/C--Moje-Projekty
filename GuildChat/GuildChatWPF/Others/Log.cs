using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GuildChatWPF
{
    class Log
    {
        public void write(string text)
        {
            try
            {
                TextWriter tsw = new StreamWriter("Logs.txt", true);
                tsw.WriteLine(text);
                tsw.Close();
            }
            catch (Exception) 
            {
                MessageBox.Show("Wystąpił błąd podczas zapisu do Logs.txt");
            }
        }

        public void nextLog()
        {
            write("======= " + DateTime.Now + " =======");
        }
    }
}
