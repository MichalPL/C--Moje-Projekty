using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json;
using System.Windows.Threading;

namespace GuildChatWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TcpClient tcpclnt;
        Stream stm;
        string data;
        string nick = null;
        string doKogo = null, wiadomosc = null;
        //Thread thread = null;
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        string wersja;
        string IP_ADRES = "136.243.44.221";//"127.0.0.1"; //
        TextMode tmode;
        NewMessage mess;
        HidePanel panel;
        Log log;
        InfoBox ibox;
        NewTextMessage ntmess;
        StartClient client;
        Loader loader;
        PacketSwitcher packetSwitch;

        public MainWindow()
        {
            IniReader ireader = new IniReader();
            try
            {
                InitializeComponent();
                client = new StartClient(this, panel1, panel2);
                client.start(IP_ADRES, nasluch, tsslInfo, btnLoguj);
                tcpclnt = client.retClient();
                tmode = new TextMode();
                mess = new NewMessage();
                log = new Log();
                ibox = new InfoBox();
                ntmess = new NewTextMessage();
                packetSwitch = new PacketSwitcher();
                player.SoundLocation = "sound.wav";
                ireader.loadFromIni(tbLogin, tbHaslo, cbZapamietaj);
                lblWersja.Content = "2105";
                wersja = lblWersja.Content.ToString();
                panel = new HidePanel(this, panel1, panel2);
                panel.hide(false);
            }
            catch (Exception e)
            {
                log.nextLog();
                log.write(e.ToString());
            }
        }

        private void nasluch()
        {
            try
            {

                stm = tcpclnt.GetStream();
                FriendsChanger fchanger = new FriendsChanger(this, lvZnajomi, tcpclnt, stm, tsslInfo);
                loader = new Loader(this, tcpclnt, stm, tsslInfo);

                byte[] bb = new byte[2048];
                int k;
                while (tcpclnt.Connected)
                {
                    k = stm.Read(bb, 0, 2048);

                    char[] fla = new char[k];
                    for (int i = 0; i < k; i++)
                    {
                        fla[i] = Convert.ToChar(bb[i]);
                    }

                    data = new string(fla);
                    Dictionary<string, dynamic> values = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(data);
                    string typ = values["typ"].ToString();
                    packetSwitch.switch_(this, typ, tsslInfo, values, wersja, panel, loader, client, IP_ADRES, nasluch,
                        btnLoguj, lbWiadomosci, tbDo, tbWiad, lvZnajomi, ntmess, mess, fchanger, player);
                }
            }
            catch (Exception ex)
            {
                ibox.writeInfo(this, tsslInfo, "Błąd odpowiedzi servera");

                log.nextLog();
                log.write("Dane: " + data + Environment.NewLine + ex.ToString());
                panel.hide(false);
                tcpclnt.Close();
                client.start(IP_ADRES, nasluch, tsslInfo, btnLoguj);
                tcpclnt = client.retClient();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loader.login(cbZapamietaj, tbLogin, tbHaslo);
            nick = loader.retNick();
            label5.Content = nick;
        }

        private void btnWyslij_Click(object sender, RoutedEventArgs e)
        {
            loader.send(tbPisz, tbDo, tbWiad, lbWiadomosci, mess, ntmess, nick, doKogo, wiadomosc);
        }

        private void lvZnajomi_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int zaznaczony = lvZnajomi.SelectedIndex;
            User item = (User)lvZnajomi.Items.GetItemAt(zaznaczony);
            tbDo.Text = item.Nick;
            tbPisz.Focus();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            tmode.text(this, tbWiad, lbWiadomosci, true);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            tmode.text(this, tbWiad, lbWiadomosci, false);
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.None && e.Key == Key.Enter)
            {
                e.Handled = true;
                loader.send(tbPisz, tbDo, tbWiad, lbWiadomosci, mess, ntmess, nick, doKogo, wiadomosc);
            }
        }

        private void tbHaslo_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.Tab))
                tbHaslo.SelectAll();
        }

    }
}
