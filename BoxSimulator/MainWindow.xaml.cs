using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BoxSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread thread1;
        Anim anim;
        RandomImage ri;
        List<string> list = new List<string>();
        ButtonController bc;
        public MainWindow()
        {
            InitializeComponent();
            thread1 = new Thread(new ThreadStart(myThread));
            bc = new ButtonController(this, thread1);
            anim = new Anim();
            box.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/Images/BOX.gif"));
        }

        private int currentElement = 0;

        private void fill()
        {
            if (!this.CheckAccess())
            {
                this.Dispatcher.Invoke(DispatcherPriority.Send,
                (Action)delegate
                {
                    ri = new RandomImage();
                    list = ri.fill();
                    for (int i = 0; i <= 34; i++)
                        img(i).Source = new BitmapImage(new Uri(list[i]));
                });
            }
            else
            {
                ri = new RandomImage();
                list = ri.fill();
                for (int i = 0; i <= list.Count; i++)
                    img(i).Source = new BitmapImage(new Uri(list[i]));
            }
        }

        private Image img (int i)
        {
            for (int a = 1; a <= 35; a++ )
                if(a==i)
                    return this.FindName("i" + a.ToString()) as Image;
            return i1;
        }

        private void myThread()
        {
            fill();
            Random rnd = new Random();
            int losowa = rnd.Next(3, 47);
            for (int i = 0; i < 24; i++)
            {
                    currentElement++;
                    anim.animate(this, -50 * (currentElement) + 25, 0.8);
                Thread.Sleep(i * i);
            }
            anim.animate(this, -50 * (currentElement) + 25 - losowa, 0.8 * losowa/47); //surprice anim :D
            Thread.Sleep(800);
            wynik();
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
             bc.open();
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            bc.reset(myThread);
            currentElement = 0;
            anim.animate(this, 0, 0);
        }

        private void wynik()
        {
            if (!this.CheckAccess())
                this.Dispatcher.Invoke(DispatcherPriority.Send,
                (Action)delegate
                {
                    dispatch1();
                });
            else
                dispatch1();
        }

        private void dispatch1()
        {
            lbl1.Content = ri.getItem(currentElement + 1);
            otworz.Visibility = Visibility.Hidden;
            reset.Visibility = Visibility.Visible;
            otworz.IsEnabled = true;
        }
    }
}
