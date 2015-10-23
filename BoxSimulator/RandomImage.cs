using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BoxSimulator
{
    class RandomImage
    {
        List<string> list = new List<string>();
        List<string> list2 = new List<string>();
        ListWhithNames lwn = new ListWhithNames();
        List<int> prevPoint;
        List<int> ranges;
        int los;
        int sumOfRanges;
        Random rnd = new Random();

        public RandomImage()
        {
            prevPoint = new List<int>();
            ranges = new List<int>();
            prevPoint.Add(0);
            fillRanges();
            sumOfRanges = rangesSum();
        }

        private void fillRanges()
        {
            ranges.Add(5); //g90
            ranges.Add(10); //g80
            ranges.Add(10); //g80 armor
            ranges.Add(200); //g75
            ranges.Add(200); //g70 armor
            ranges.Add(1000); //g70
            ranges.Add(1000); //g65 armor
            ranges.Add(5000); // gold ingot
            ranges.Add(80000); // gold coin
            ranges.Add(170000); //silver coin
            ranges.Add(10000); //exp stone
            ranges.Add(12000); //wealth stone
            for (int i = 0; i < 8; i++)
                ranges.Add(10000); //scrolls,
            ranges.Add(16000); //abs
            ranges.Add(14000); //water of 8 trigrams
            ranges.Add(15000); // solubilizer
            ranges.Add(10000); //Silvery Carp
            ranges.Add(10000); //Silvery Eel
            ranges.Add(15000); //Diamond Stone
            ranges.Add(20000); //TOE
            for (int i = 0; i < ranges.Count(); i++)
                switchRange(ranges[i]);
        }

        public List<string> fill()
        {
            int wylosowane = 1;
            for (int i = 0; i < 35; i++)
            {
                wylosowane = randImg();
                list.Add(Directory.GetCurrentDirectory() + "/Images/" + wylosowane + ".gif");
                list2.Add(lwn.get(wylosowane - 1, 1));
            }
            return list;
        }

        public string getItem(int i)
        {
            return list2[i];
        }

        private bool zakres(int los, int zakresOd, int zakresDo)
        {
            if (los <= zakresDo && los > zakresOd)
            {
                return true;
            }
            return false;
        }

        private int randImg()
        {
            los = rnd.Next(1, sumOfRanges);
            for (int i = 1; i <= ranges.Count; i++)
                if (inRange(i))
                    return i;
            return 10;
        }

        private void switchRange(int range)
        {
            List<int> ret = new List<int>();
            int nextPoint = range + prevPoint[prevPoint.Count() - 1];
            prevPoint.Add(nextPoint);
        }

        private bool inRange(int index)
        {
            if (los > prevPoint[index - 1] && los <= prevPoint[index])
                return true;
            return false;
        }

        private int rangesSum()
        {
            int sum = 0;
            for (int i = 0; i < ranges.Count(); i++)
                sum += ranges[i];
            return sum;
        }
    }
}
