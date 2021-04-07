using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Threading.Tasks;

namespace primes
{
    class Program
    {

        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();

            Stopwatch totstopWatch = new Stopwatch();

            Stopwatch profiler = new Stopwatch();

            long limit;

            if (args.Length > 0 && long.TryParse(args[0], out limit) == true) { }
            else
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Please eneter limit");
                        limit = long.Parse(Console.ReadLine());
                        break;
                    }
                    catch (System.Exception)
                    {
                        Console.WriteLine("Please enter a valid number");
                    }

                }

            }

            totstopWatch.Start();

            var print = 1000;

            int printJump = 1000;

            //limit = 1000000;


            List<long> primeList = new List<long>();


            for (int i = 0; i < limit; i++)
            {


                profiler.Start();

                var isPrimeBool = isPrime(i, primeList);

                profiler.Stop();

                if (isPrimeBool)
                {
                    primeList.Add(i);

                    if (i > print)
                    {
                        if (stopWatch.IsRunning) stopWatch.Stop();
                        //Console.WriteLine(i + " is prime");
                        var percent = (((double)i / (double)limit) * 100);
                        Console.WriteLine(string.Format("{0:0.000}", percent) + "%");

                        if (stopWatch.Elapsed.TotalSeconds != 0)
                        {
                            printJump = Convert.ToInt32(printJump / (stopWatch.Elapsed.TotalSeconds));
                            Process proc = Process.GetCurrentProcess();
                            Console.WriteLine("Speed: " + printJump + " numbers/second searched");
                            Console.WriteLine((proc.PrivateMemorySize64 / 1000000) + "MB of memory used");
                            Console.WriteLine();
                            if (printJump > 60000) printJump = 60000;
                        }





                        print += printJump;

                        stopWatch.Reset();

                        stopWatch.Start();
                    }

                }
            }

            File.WriteAllText("primes0-" + limit + ".json", JsonSerializer.Serialize(primeList));

            totstopWatch.Stop();

            string time = "0";

            if (totstopWatch.Elapsed.TotalSeconds > 60)
            {
                time = totstopWatch.Elapsed.TotalMinutes.ToString() + " minutes";
            }
            else
            {
                time = totstopWatch.Elapsed.TotalSeconds.ToString() + " seconds";
            }
            Console.WriteLine("Took " + time + " (" + string.Format("{0:0.00}", (profiler.Elapsed.TotalSeconds / totstopWatch.Elapsed.TotalSeconds) * 100) + "% of time spent calculating primes)");
        }

        public static bool isPrime(long possibalePrime, List<long> primes)
        {

            foreach (long item in primes)
            {
                if (possibalePrime % item == 0)
                {
                    return false;
                }
            }

            if (possibalePrime < 2) return false;

            return true;


        }
    }
}
