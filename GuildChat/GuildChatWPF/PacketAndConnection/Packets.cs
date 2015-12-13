using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GuildChatWPF
{
    class Packets
    {
        Label tsslInfo;
        MainWindow con;
        InfoBox ibox;
        Dictionary<string, dynamic> values;
        String wersja;

        public Packets(Label tssl, MainWindow c,  Dictionary<string, dynamic> val, String ver)
        {
            ibox = new InfoBox();
            tsslInfo = tssl;
            con = c;
            values = val;
            wersja = ver;
        }

        public void serv1()
        {
            ibox.writeInfo(con, tsslInfo, "Połączono");
            CheckVersion sprawdz = new CheckVersion();
            sprawdz.check(values["text1"].ToString(), wersja, con);
        }

        public void wiad(NewTextMessage ntmess, NewMessage mess, System.Media.SoundPlayer player, 
            TextBox tbWiad, ListBox lbWiadomosci, TextBox tbDo)
        {
            ntmess.write(con, tbWiad, values["text1"] + " do " + values["text3"] + ": " + values["text2"]);
            ntmess.write(con, tbWiad, "--------------------------------------------------------------------------");
            mess.show(con, lbWiadomosci, values["text1"].ToString(), values["text2"].ToString(), 2);
            FillTo uzupelnij = new FillTo();
            uzupelnij.fill(con, tbDo, values["text1"]);
            player.Play();
        }

        public void wiad2(NewTextMessage ntmess, NewMessage mess, TextBox tbWiad, ListBox lbWiadomosci)
        {
            int ilosc2 = 0;
            foreach (string ele in values["text1"])
            {
                ilosc2++;
            }
            for (int i = 0; i < ilosc2; i++)
            {
                ntmess.write(con, tbWiad, values["text1"][i].ToString() + ": " + values["text2"][i].ToString());
                ntmess.write(con, tbWiad, "--------------------------------------------------------------------------");
                mess.show(con, lbWiadomosci, values["text1"][i].ToString(), values["text2"][i].ToString(), 2);
            }
        }

        public void info1(NewTextMessage ntmess, NewMessage mess, TextBox tbWiad, ListBox lbWiadomosci)
        {
            ibox.writeInfo(con, tsslInfo, "Brak użytkownika!");
            ntmess.write(con, tbWiad, "Wiadomość nie została wysłana...");
            mess.show(con, lbWiadomosci, "Uwaga", "Wiadomosc nie została wysłana!", 3);
        }

        public void info3a(Loader loader, NewTextMessage ntmess, NewMessage mess, TextBox tbWiad, ListBox lbWiadomosci)
        {
            ibox.writeInfo(con, tsslInfo, "Wiadomości odebrane!");
            ntmess.write(con, tbWiad, "OSTATNIE WIADOMOŚCI DO CIEBIE");
            ntmess.write(con, tbWiad, "(zostaną automatycznie usunięte z bazy danych)");
            ntmess.write(con, tbWiad, "====================================");
            mess.show(con, lbWiadomosci, "OSTATNIE WIADOMOSCI DO CIEBIE", "Zostaną automatycznie usunięte z bazy danych", 3);
            loader.loadFriends();
        }

        public void znajomi(ListView lvZnajomi, FriendsChanger fchanger, Loader loader)
        {
            ListViewCleaner lvCleaner = new ListViewCleaner();
            lvCleaner.clear(con, lvZnajomi);
            ibox.writeInfo(con, tsslInfo, "Pobrano listę znajomych");
            int ilosc = 0;
            foreach (string ele in values["text1"])
            {
                ilosc++;
            }
            for (int i = 0; i < ilosc; i++)
            {
                fchanger.add(values["text1"][i].ToString(), values["text2"][i].ToString());
            }
            loader.isOnline();
        }

        public void online(NewTextMessage ntmess, NewMessage mess, TextBox tbWiad, ListBox lbWiadomosci, FriendsChanger fchanger)
        {
            fchanger.change(values["text1"], values["text2"]);
            ntmess.write(con, tbWiad, "=====================================");
            if (values["text2"] == "online")
            {
                ntmess.write(con, tbWiad, values["text1"] + " jest teraz " + values["text2"].ToUpper());
                mess.show(con, lbWiadomosci, values["text1"], "Jest teraz " + values["text2"].ToUpper(), 3);
            }
            else
            {
                ntmess.write(con, tbWiad, values["text1"] + " jest teraz " + values["text2"]);
                mess.show(con, lbWiadomosci, values["text1"], "Jest teraz " + values["text2"], 3);
            }
            ntmess.write(con, tbWiad, "====================================");
        }
    }
}
