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
            fillRanges(ranges, prevPoint, 5, 10, 10, 200, 200, 1000, 1000, 5000, 80000, 170000, 10000, 12000, 
                10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 16000, 14000, 15000, 10000, 10000, 15000, 20000);
            //g90, g80, g80a, g75, g70a, g70, g65a, gold ingot, gold coin, silver coin, exp stone, wealth stone, scrolls x8,
            //abs, water, solublizier, silvery carp, silvery eel, diamond stone, toe
            sumOfRanges = rangesSum(ranges);

            //FAKE
            FAKEprevPoint = new List<int>();
            FAKEranges = new List<int>();
            FAKEprevPoint.Add(0);
            fillRanges(FAKEranges, FAKEprevPoint, 25, 40, 40, 200, 200, 1000, 1000, 5000, 8000, 17000, 1000, 1200,
                1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1600, 1400, 1500, 1000, 1000, 1500, 2000);
            FAKEsumOfRanges = rangesSum(FAKEranges);
        }

        private void fillRanges(List<int> rangeList, List<int> pointList, params int[] intList)
        {
            for (int i = 0; i < intList.Length; i++ )
            {
                rangeList.Add(intList[i]);
            }
            for (int i = 0; i < rangeList.Count(); i++)
                switchRange(rangeList[i], pointList);
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
