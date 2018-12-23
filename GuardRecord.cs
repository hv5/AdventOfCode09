using System;
using System.Text.RegularExpressions;

namespace AdventOfCode09
{
    class GuardRecord
    {
      
        public enum Type {StartShift, StartSleep, EndSleep, Undef};
        public string InputLine {get;set;}
        public string Message {get;set;}
        public DateTime DateTime {get;set;}
        public int GuardId {get;set;} // only for StartShift type
        public string GuardName {get;set;} // only for StartShift type
        public Type RecordType;
        
        public GuardRecord()
        {
            RecordType = Type.Undef;
        }

        public void Initialize(string str)
        {
            str = str.Trim();
            
            if (string.IsNullOrEmpty(str))
                return;
            
            // Example:
            // [1518-05-08 00:00] Guard #563 begins shift
            // [1518-05-08 00:29] falls asleep
            // [1518-05-08 00:51] wakes up

            Match match = Regex.Match(str, @"\[(.+)\]\s+(.+)"); // capture date and message
            if (match.Success && match.Groups.Count >= 3)
            {
                string dateStr = match.Groups[1].Value ;
                string message = match.Groups[2].Value ;

                var date = DateTime.ParseExact(dateStr, "yyyy-MM-dd HH:mm", null);

                this.InputLine = str;
                this.Message = message;
                this.DateTime = date;

                this.RecordType = Type.Undef;

                // Categorize record
                if (this.RecordType == Type.Undef) {
                    Match m = Regex.Match(message, @"Guard #(\d+) begins shift");
                    if (m.Success && m.Groups.Count >= 2)
                    {
                        this.GuardId = Int32.Parse(m.Groups[1].Value);
                        this.GuardName = "#" + m.Groups[1].Value;
                        this.RecordType = Type.StartShift;
                    }
                }
                if (this.RecordType == Type.Undef) {
                    if (Regex.Match(message, @"falls asleep").Success)
                    {
                        this.RecordType = Type.StartSleep;
                    }
                }
                if (this.RecordType == Type.Undef) {
                    if (Regex.Match(message, @"wakes up").Success)
                    {
                        this.RecordType = Type.EndSleep;
                    }
                }
            }
        }

        public static GuardRecord CreateFromInputString(string str)
        {
            GuardRecord record = new GuardRecord();
            record.Initialize(str);
            return record;
        }

    }
}

