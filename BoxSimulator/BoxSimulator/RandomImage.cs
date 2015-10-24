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

        //FAKE - all slots except 24 (that's where stops animation) have more luck :)
        List<int> FAKEprevPoint;
        List<int> FAKEranges;
        int FAKEsumOfRanges;

        public RandomImage()
        {
            prevPoint = new List<int>();
            ranges = new List<int>();
            prevPoint.Add(0);
            fillRanges();
            sumOfRanges = rangesSum(ranges);

            //FAKE
            FAKEprevPoint = new List<int>();
            FAKEranges = new List<int>();
            FAKEprevPoint.Add(0);
            FAKEfillRanges();
            FAKEsumOfRanges = rangesSum(FAKEranges);
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
                switchRange(ranges[i], prevPoint);
        }

        private void FAKEfillRanges()
        {
            FAKEranges.Add(25); //g90
            FAKEranges.Add(40); //g80
            FAKEranges.Add(40); //g80 armor
            FAKEranges.Add(200); //g75
            FAKEranges.Add(200); //g70 armor
            FAKEranges.Add(1000); //g70
            FAKEranges.Add(1000); //g65 armor
            FAKEranges.Add(5000); // gold ingot
            FAKEranges.Add(8000); // gold coin
            FAKEranges.Add(17000); //silver coin
            FAKEranges.Add(1000); //exp stone
            FAKEranges.Add(1200); //wealth stone
            for (int i = 0; i < 8; i++)
                FAKEranges.Add(1000); //scrolls,
            FAKEranges.Add(1600); //abs
            FAKEranges.Add(1400); //water of 8 trigrams
            FAKEranges.Add(1500); // solubilizer
            FAKEranges.Add(1000); //Silvery Carp
            FAKEranges.Add(1000); //Silvery Eel
            FAKEranges.Add(1500); //Diamond Stone
            FAKEranges.Add(2000); //TOE
            for (int i = 0; i < FAKEranges.Count(); i++)
                switchRange(FAKEranges[i], FAKEprevPoint);
        }

        public List<string> fill() 
        {
            int wylosowane = 1;
            for (int i = 0; i < 35; i++)
            {
                if (i != 25)
                {
                    wylosowane = randImg(FAKEprevPoint, FAKEranges, FAKEsumOfRanges);
                    list.Add(Directory.GetCurrentDirectory() + "/Images/" + wylosowane + ".gif");
                    list2.Add(lwn.get(wylosowane - 1, 1));
                }
                else
                {
                    wylosowane = randImg(prevPoint, ranges, sumOfRanges);
                    list.Add(Directory.GetCurrentDirectory() + "/Images/" + wylosowane + ".gif");
                    list2.Add(lwn.get(wylosowane - 1, 1));
                }
            }
            return list;
        }

        public string getItem(int i)
        {
            return list2[i];
        }

        private int randImg(List<int> pointList, List<int> rangeList, int sum)
        {
            los = rnd.Next(1, sum);
            for (int i = 1; i <= rangeList.Count; i++)
                if (inRange(i, pointList))
                    return i;
            return 10;
        }

        private void switchRange(int range, List<int> pointList)
        {
            List<int> ret = new List<int>();
            int nextPoint = range + pointList[pointList.Count() - 1];
            pointList.Add(nextPoint);
        }

        private bool inRange(int index, List<int> pointList)
        {
            if (los > pointList[index - 1] && los <= pointList[index])
                return true;
            return false;
        }

        private int rangesSum(List<int> rangeList)
        {
            int sum = 0;
            for (int i = 0; i < rangeList.Count(); i++)
                sum += rangeList[i];
            return sum;
        }
    }
}
