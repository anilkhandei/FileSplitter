using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FileSplitter
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Program...to extract 6_318000 scripts started at "+System.DateTime.Now.ToShortTimeString());
            //string FileToRead = "NonClaimsLines_Full_DELETE.SQL";
            string FileToRead = "NonClaimsLines_Full_DELETE.SQL";

            var lines = File.ReadAllLines(FileToRead);
            
            List<string> lsr = new List<string>();
            int Progress = 0;
            Console.WriteLine("Scanning file fr billing records started at " + System.DateTime.Now.ToShortTimeString());
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach(var line in lines)
            {
                //if (!line.Contains("N'CLAIMS'") && !line.Contains("'CLAIMS'"))
                //if (line.Contains("6.318000"))
                if (line.IndexOf("Billing")>0)
                {
                    lsr.Add(line.ToString());
                }
                Progress += 1;
                drawTextProgressBar(Progress, 752901);
            }
            stopWatch.Stop();
            Console.WriteLine("Scanning file for billing records ended at " + System.DateTime.Now.ToShortTimeString() +", time taken is "+stopWatch.ElapsedMilliseconds+" milli seconds.");

            string FileToWrite = "Billing_Scripts.SQL";

            if (!File.Exists(FileToWrite))
            {

                using(StreamWriter sw= File.CreateText(FileToWrite))
                {
                    Console.WriteLine("Writing to"+ FileToWrite + " file started.");
                    foreach(var line in lsr)
                    {
                        sw.WriteLine(line.ToString());
                    }
                    Console.WriteLine("Writing to" + FileToWrite + " file completed.");
                }
            }
            Console.WriteLine("Program...to extract Billing scripts Ended at " + System.DateTime.Now.ToShortTimeString());

            Console.ReadKey();
        }

        private static void drawTextProgressBar(int progress, int total)
        {
            //draw empty progress bar
            Console.CursorLeft = 0;
            Console.Write("["); //start
            Console.CursorLeft = 32;
            Console.Write("]"); //end
            Console.CursorLeft = 1;
            float onechunk = 30.0f / total;

            //draw filled part
            int position = 1;
            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw unfilled part
            for (int i = position; i <= 31; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw totals
            Console.CursorLeft = 35;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(progress.ToString() + " of " + total.ToString() + "    "); //blanks at the end remove any excess
        }
    }
}
