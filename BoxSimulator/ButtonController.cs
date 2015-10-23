using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BoxSimulator
{
    class ButtonController
    {
        MainWindow con;
        Thread thread;
        public ButtonController(MainWindow c, Thread th)
        {
            con = c;
            thread = th;
        }

        public void reset(Action func)
        {
            con.otworz.Visibility = Visibility.Visible;
            con.reset.Visibility = Visibility.Hidden;
            thread.Abort();
            thread = new Thread(new ThreadStart(func));
            con.Carousel.Visibility = Visibility.Hidden;
            con.Wsk.Visibility = Visibility.Hidden;
            con.box.Visibility = Visibility.Visible;
            con.lbl1.Content = "";
        }

        public void open()
        {
            con.Carousel.Visibility = Visibility.Visible;
            con.Wsk.Visibility = Visibility.Visible;
            con.box.Visibility = Visibility.Hidden;
            con.otworz.IsEnabled = false;
            thread.Start();
        }
    }
}
