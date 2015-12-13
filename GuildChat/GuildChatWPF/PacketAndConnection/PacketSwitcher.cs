using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GuildChatWPF
{
    class PacketSwitcher
    {
        InfoBox ibox = new InfoBox();
        public void switch_(MainWindow con, String typ, Label tsslInfo, Dictionary<String, dynamic> values,
            String wersja, HidePanel panel, Loader loader, StartClient client, String ip_adress, Action nasluch,
            Button btnLoguj, ListBox lbWiadomosci, TextBox tbDo, TextBox tbWiad, ListView lvZnajomi,
            NewTextMessage ntmess, NewMessage mess, FriendsChanger fchanger, System.Media.SoundPlayer player)
        {
            Packets packet = new Packets(tsslInfo, con, values, wersja);
            switch (typ)
            {
                //POLACZENIE I ZAPYTANIE O LOGOWANIE
                case "serv1":
                    packet.serv1();
                    break;
                //LOGOWANIE
                case "l1":
                    ibox.writeInfo(con, tsslInfo, "Zalogowano");
                    panel.hide(true);
                    loader.loadMessages();
                    break;
                case "l2":
                    MessageBox.Show("Ktoś był zalogowany! " + Environment.NewLine + " Trwa zamykanie poprzedniej sesji "
                        + Environment.NewLine + "i wykonywanie ponownego połączenia do serwera.");
                    client.reconnect(ip_adress, nasluch, tsslInfo, btnLoguj);
                    //tcpclnt1 = client.retClient();
                    break;
                case "l3":
                    ibox.writeInfo(con, tsslInfo, "Nieprawidłowe dane lub login nie istnieje");
                    break;
                case "l4":
                    ibox.writeInfo(con, tsslInfo, "Jesteś już zalogowany!");
                    break;
                //WIADOMOSCI
                case "wiad":
                    packet.wiad(ntmess, mess, player, tbWiad, lbWiadomosci, tbDo);
                    //client.reconnect(ip_adress, nasluch, tsslInfo, btnLoguj);
                    //tcpclnt1 = client.retClient();
                    break;
                case "wiad2":
                    packet.wiad2(ntmess, mess, tbWiad, lbWiadomosci);
                    break;
                //INFO
                case "info1":
                    packet.info1(ntmess, mess, tbWiad, lbWiadomosci);
                    break;
                case "info2":
                    ibox.writeInfo(con, tsslInfo, "Wiadomość wysłana!");
                    break;
                case "info3a":
                    packet.info3a(loader, ntmess, mess, tbWiad, lbWiadomosci);
                    break;
                case "info3b":
                    ibox.writeInfo(con, tsslInfo, "Brak nowych wiadomości!");
                    loader.loadFriends();
                    break;
                case "znajomi":
                    packet.znajomi(lvZnajomi, fchanger, loader);
                    break;
                case "online":
                    packet.online(ntmess, mess, tbWiad, lbWiadomosci, fchanger);
                    break;
                case "wylacz":
                    client.reconnect(ip_adress, nasluch, tsslInfo, btnLoguj);
                    //tcpclnt1 = client.retClient();
                    break;
                default:
                    ibox.writeInfo(con, tsslInfo, "Nieznany pakiet z serwera!" + typ);
                    break;
            }
        }

    }
}
