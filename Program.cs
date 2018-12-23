using System;
using System.IO;

namespace AdventOfCode09
{
    class Program
    {
        public void Run(string[] args)
        {
            try 
            {
                StreamReader reader;
                if (args.Length > 0)
                    reader = new StreamReader(args[0]);
                else
                    reader = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding);

                GuardRecordProcessor proc = new GuardRecordProcessor();
                proc.Read(reader);
                proc.Process();
                proc.Output();
                
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        static void Main(string[] args)
        {         
            new Program().Run(args);
        }
    }
}
