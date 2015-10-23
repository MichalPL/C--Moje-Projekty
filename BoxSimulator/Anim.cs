using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace BoxSimulator
{
    class Anim
    {
        public void animate(MainWindow con, int gdzie, double czas)
        {
            if (!con.CheckAccess())
            {
                con.Dispatcher.Invoke(DispatcherPriority.Send,
                (Action)delegate
                {
                    Storyboard storyboard = (con.Resources["CarouselStoryboard"] as Storyboard);
                    DoubleAnimation animation = storyboard.Children.First() as DoubleAnimation;
                    animation.Duration = new Duration(TimeSpan.FromSeconds(czas));
                    animation.To = gdzie;
                    storyboard.Begin();
                });
            }
            else
            {
                Storyboard storyboard = (con.Resources["CarouselStoryboard"] as Storyboard);
                DoubleAnimation animation = storyboard.Children.First() as DoubleAnimation;
                animation.Duration = new Duration(TimeSpan.FromSeconds(czas));
                animation.To = gdzie;
                storyboard.Begin();
            }
        }
    }
}
