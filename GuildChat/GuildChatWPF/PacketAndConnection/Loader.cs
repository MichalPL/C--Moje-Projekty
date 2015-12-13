using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GuildChatWPF
{
    class Loader
    {
        InfoBox ibox;
        Log log;
        MainWindow con;
        TcpClient tcpclnt;
        Stream stream;
        Label tsslInfo;
        String nick;

        public Loader(MainWindow c, TcpClient tcpc, Stream stm, Label tssl)
        {
            ibox = new InfoBox();
            log = new Log();
            con = c;
            tcpclnt = tcpc;
            stream = stm;
            tsslInfo = tssl;
        }

        public void login(CheckBox cbZapamietaj, TextBox tbLogin, PasswordBox tbHaslo)
        {
            try
            {
                if (cbZapamietaj.IsChecked == true)
                {
                    string[] lines = { tbLogin.Text, tbHaslo.Password };
                    System.IO.File.WriteAllLines("settings.ini", lines);
                }
                else
                {
                    if (File.Exists("settings.ini"))
                        File.Delete("settings.ini");
                }

                Dictionary<string, string> test = new Dictionary<string, string>();
                test.Add("typ", "1");
                test.Add("text1", tbLogin.Text);
                test.Add("text2", tbHaslo.Password);
                String str = JsonConvert.SerializeObject(test);
                stream = tcpclnt.GetStream();
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str);
                ibox.writeInfo(con, tsslInfo, "Wysłano żądanie logowania.....");
                stream.Write(ba, 0, ba.Length);
                nick = tbLogin.Text;
            }
            catch (Exception ex)
            {
                ibox.writeInfo(con, tsslInfo, "Error...");
                log.nextLog();
                log.write(ex.ToString());
            }
        }

        public String retNick()
        {
            return nick;
        }

        public void send(TextBox tbPisz, TextBox tbDo, TextBox tbWiad, ListBox lbWiadomosci, 
            NewMessage mess, NewTextMessage ntmess, String nick, String doKogo, String wiadomosc)
        {
            if (tbPisz.Text != "" && tbDo.Text != "")
            {
                try
                {
                    Dictionary<string, string> wiad = new Dictionary<string, string>();
                    wiad.Add("typ", "2");
                    wiad.Add("text1", tbDo.Text);
                    wiad.Add("text2", tbPisz.Text);
                    String str = JsonConvert.SerializeObject(wiad);
                    stream = tcpclnt.GetStream();
                    ASCIIEncoding asen = new ASCIIEncoding();
                    byte[] ba = asen.GetBytes(str);
                    ibox.writeInfo(con, tsslInfo, "Wysyłanie...");
                    if (tbDo.Text == nick)
                    {
                        ibox.writeInfo(con, tsslInfo, "Nie możesz wysłać wiadomości do siebie!");
                        mess.show(con, lbWiadomosci, "Uwaga", "Nie możesz wysłać wiadomości do siebie!", 3);
                    }
                    else
                    {
                        stream.Write(ba, 0, ba.Length);

                        doKogo = tbDo.Text;
                        wiadomosc = tbPisz.Text;
                        ntmess.write(con, tbWiad, nick + " do " + doKogo + ": " + wiadomosc);
                        ntmess.write(con, tbWiad, "--------------------------------------------------------------------------");
                        mess.show(con, lbWiadomosci, nick, wiadomosc, 1);
                        tbPisz.Text = "";
                        tbPisz.Focus();
                    }
                }
                catch (Exception ex)
                {
                    ibox.writeInfo(con, tsslInfo, "Error...");
                    log.write(ex.ToString());
                    ntmess.write(con, tbWiad, "=================================");
                    ntmess.write(con, tbWiad, "Wiadomość nie została wysłana...");
                    ntmess.write(con, tbWiad, "=================================");
                    mess.show(con, lbWiadomosci, "Uwaga", "Wiadomość nie została wysłana!", 3);
                }
            }
            else
            {
                ntmess.write(con, tbWiad, "=================================");
                ntmess.write(con, tbWiad, "Uzupełnij pola!");
                ntmess.write(con, tbWiad, "=================================");
                mess.show(con, lbWiadomosci, "Uwaga", "Uzupełnij pola!", 3);
            }
        }

        public void loadMessages()
        {
            try
            {
                Dictionary<string, string> test = new Dictionary<string, string>();
                test.Add("typ", "3");
                test.Add("text1", "Client");
                test.Add("text2", "zadanie o wiadomosci");
                String str = JsonConvert.SerializeObject(test);
                stream = tcpclnt.GetStream();
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str);
                ibox.writeInfo(con, tsslInfo, "Wysłano żądanie pobrania wiadomości.....");
                stream.Write(ba, 0, ba.Length);
            }
            catch (Exception ex)
            {
                ibox.writeInfo(con, tsslInfo, "Error...");
                log.nextLog();
                log.write(ex.ToString());
            }
        }

        public void loadFriends()
        {
            try
            {
                Dictionary<string, string> test = new Dictionary<string, string>();
                test.Add("typ", "4");
                test.Add("text1", "Client");
                test.Add("text2", "zadanie o ludzi");
                String str = JsonConvert.SerializeObject(test);
                stream = tcpclnt.GetStream();
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str);
                ibox.writeInfo(con, tsslInfo, "Wysłano żądanie pobrania znajomych.....");
                stream.Write(ba, 0, ba.Length);
            }
            catch (Exception ex)
            {
                ibox.writeInfo(con, tsslInfo, "Error...");
                log.nextLog();
                log.write(ex.ToString());
            }
        }

        public void isOnline()
        {
            try
            {
                Dictionary<string, string> test = new Dictionary<string, string>();
                test.Add("typ", "5");
                test.Add("text1", "Client");
                test.Add("text2", "zadanie o online");
                String str = JsonConvert.SerializeObject(test);
                stream = tcpclnt.GetStream();
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str);
                ibox.writeInfo(con, tsslInfo, "Wysłano żądanie, że jesteś online.....");
                stream.Write(ba, 0, ba.Length);
                ibox.writeInfo(con, tsslInfo, "Jesteś online!");
            }
            catch (Exception ex)
            {
                ibox.writeInfo(con, tsslInfo, "Error...");
                log.nextLog();
                log.write(ex.ToString());
            }
        }
    }
}
