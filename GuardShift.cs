using System;

namespace AdventOfCode09
{
    class GuardShift
    {
        private const int MaxIndex = 60; // 60 minutes
        public DateTime DateTime {get;set;}
        private bool[] MinutesArray; 

        public GuardShift()
        {
            MinutesArray = new bool[MaxIndex];
            DateTime = DateTime.MinValue;
        }

        public void SetSleep(int index, bool val)
        {
            if (index < 0 || index >= MinutesArray.Length)
                throw new IndexOutOfRangeException("Guard shift index outside of range");

            MinutesArray[index] = val;
        }

        public void SetSleepInterval(int minIndex, int maxIndex, bool val)
        {
            for (int i=minIndex; i<=maxIndex; i++)
                SetSleep(i, val);
        }

        public bool IsSleep(int index)
        {
            if (index < 0 || index >= MinutesArray.Length)
                throw new IndexOutOfRangeException("Guard shift index outside of range");

            return MinutesArray[index];
        }

        public int CountSleepMinutes()
        {
            int count = 0;
            for (int i=0; i<MinutesArray.Length; i++)
            {
                if (MinutesArray[i])
                    count++;
            }
            return count;
        }

        public int CountTotalMinutes()
        {
            return MinutesArray.Length;
        }

        public static int GetMaxIndex()
        {
            return MaxIndex;
        }
    }
}
