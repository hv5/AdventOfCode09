using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode09
{
    class GuardRecordProcessor
    {
        private List<GuardRecord> GuardRecordList;
        private List<Guard> GuardList;
        private Guard MostSleepyGuard;

        public GuardRecordProcessor()
        {
            GuardRecordList = new List<GuardRecord>();
            GuardList = new List<Guard>();
        }

        private void CreateRecord(string str)
        {
            GuardRecord record = GuardRecord.CreateFromInputString(str);
            if (record != null)
                GuardRecordList.Add(record);
        }

        public void Read(StreamReader reader)
        {
            string str;
            while ((str = reader.ReadLine()) != null)
                CreateRecord(str);
            
            // Sort by date and time
            GuardRecordList = GuardRecordList.OrderBy(x => x.DateTime).ToList();
        }

        public void Process()
        {
            GuardShift currentGuardShift = null;
            int startSleepMinute = -1;

            foreach(GuardRecord rec in GuardRecordList)
            {
                if (rec.RecordType == GuardRecord.Type.StartShift) {
                    Guard guard = GetCreateGuard(rec);
                    currentGuardShift = new GuardShift();
                    currentGuardShift.DateTime = rec.DateTime;
                    guard.AddShift(currentGuardShift);
                    startSleepMinute = -1;
                }
                else if (rec.RecordType == GuardRecord.Type.StartSleep)
                {
                    if (rec.DateTime.Hour == 0)
                        startSleepMinute = rec.DateTime.Minute;
                    else
                        startSleepMinute = 0; // if guard falls asleep before midnight
                }
                else if (rec.RecordType == GuardRecord.Type.EndSleep)
                {
                    int endSleepMinute = rec.DateTime.Minute - 1; // -1 ref requirement

                    if (startSleepMinute >= 0 && startSleepMinute <= 59 &&
                        endSleepMinute >= 0 && endSleepMinute <= 59 &&
                        startSleepMinute <= endSleepMinute)
                    {
                        currentGuardShift.SetSleepInterval(startSleepMinute, endSleepMinute, true);
                    }
                    startSleepMinute = -1;
                }
            }

            MostSleepyGuard = null;
            int maxSleepMinutes = -1;

            foreach(Guard guard in GuardList)
            {
                int countSleepMinutes = guard.CountSleepMinutes();
                if (countSleepMinutes > maxSleepMinutes) 
                {
                    maxSleepMinutes = countSleepMinutes;
                    MostSleepyGuard = guard;
                }
            }
        }

        public void Output()
        {
            /*
            foreach(GuardRecord rec in GuardRecordList)
                Console.WriteLine(rec.InputLine);

            Console.WriteLine("Number of guard records: {0}", GuardRecordList.Count);
            return;
            */

            Console.WriteLine("Number of guards: {0}", GuardList.Count);

            foreach (Guard guard in GuardList)
            {
                int countShifts = guard.CountShifts();
                int countTotalMinutes = guard.CountTotalMinutes();
                int countSleepMinutes = guard.CountSleepMinutes();

                Console.WriteLine("Guard {0}", guard.Name);
                Console.WriteLine("  Number of shifts: {0}", countShifts);

                //int i=1;
                //foreach(GuardShift shift in guard.GetShifts())
                //    Console.WriteLine("    Shift {0} ({1})", i++, shift.CountSleepMinutes());

                Console.WriteLine("  Total time on shift (minutes): {0}", countTotalMinutes);
                Console.WriteLine("  Time on shift sleeping (minutes): {0}", countSleepMinutes);
            }

            if (MostSleepyGuard != null)
            {
                int mostFreqSleepMinute = MostSleepyGuard.FindMostFrequentSleepMinute();

                Console.WriteLine();
                Console.WriteLine("Most sleepy guard: {0} (ID={1})", MostSleepyGuard.Name, MostSleepyGuard.Id);
                Console.WriteLine("  Total time on shift (minutes): {0}", MostSleepyGuard.CountTotalMinutes());
                Console.WriteLine("  Time on shift sleeping (minutes): {0}", MostSleepyGuard.CountSleepMinutes());
                Console.WriteLine("  Minute most likely sleeping: {0}", mostFreqSleepMinute);
                Console.WriteLine("  Guard ID multiplied by minute: {0}", MostSleepyGuard.Id * mostFreqSleepMinute);
            }

        }

        private Guard GetCreateGuard(GuardRecord rec)
        {
            Guard guard = GuardList.FirstOrDefault(x => x.Id == rec.GuardId);
            if (guard == null)
            {
                guard = new Guard();
                guard.Id = rec.GuardId;
                guard.Name = rec.GuardName;
                GuardList.Add(guard);
            }
            return guard;
        }
    }
}