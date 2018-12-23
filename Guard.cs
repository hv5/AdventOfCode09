using System;
using System.Collections.Generic;

namespace AdventOfCode09
{
    class Guard
    {
        public int Id {get;set;}
        public string Name {get;set;}

        private List<GuardShift> GuardShiftList;

        public Guard()
        {
            GuardShiftList = new List<GuardShift>();
        }

        public void AddShift(GuardShift shift)
        {
            GuardShiftList.Add(shift);
        }

        public IEnumerable<GuardShift> GetShifts()
        {
            return GuardShiftList;
        }

        public int CountShifts()
        {
            return GuardShiftList.Count;
        }

        public int CountTotalMinutes()
        {
            int count = 0;
            foreach(GuardShift shift in GuardShiftList)
                count += shift.CountTotalMinutes();
            
            return count;
        }

        public int CountSleepMinutes()
        {
            int count = 0;
            foreach(GuardShift shift in GuardShiftList)
                count += shift.CountSleepMinutes();
            
            return count;
        }

        public int FindMostFrequentSleepMinute()
        {
            int maxIndex = GuardShift.GetMaxIndex();

            int[] sleepCountArray = new int[maxIndex];

            foreach(GuardShift shift in GuardShiftList)
            {
                 for (int i=0; i<maxIndex; i++)
                 {
                     if (shift.IsSleep(i))
                        sleepCountArray[i]++;
                 }
            }

            int index = -1;
            int maxSleepCount = -1;

            // Find index with max sleep
            for (int i=0; i<maxIndex; i++)
            {
                if (sleepCountArray[i] > maxSleepCount)
                {
                    maxSleepCount = sleepCountArray[i];
                    index = i;
                }
            }
            
            return index;
        }
    }
}
