using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeNumbersCounter
{
    public class Program
    {
        static int Count = 0;
        static object lockObj = new object();
        static void Main(string[] args)
        {
            //Stopwatch sw = Stopwatch.StartNew();
            PrintPrimeCount(1, 10_000_000);
            //List<Task> tasks = new List<Task>();
            //for (int i = 1; i <= 100; i++)
            //{
            //    var task = Task.Run(() => DownloadAsync(i));
            //    tasks.Add(task);
            //}

            //Task.WaitAll(tasks.ToArray());
            //Console.WriteLine(sw.Elapsed);
        }

        private static async Task DownloadAsync(int i)
        {
            HttpClient httpClient = new HttpClient();
            var url = $"https://vicove.com/vic-{i}";
            var httpResponse = await httpClient.GetAsync(url);
            var vic = await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine(vic.Length);
        }

        private static void PrintPrimeCount(int min, int max)
        {
            Stopwatch sw = Stopwatch.StartNew();
            //for (int i = min; i <= max; i++)
            Parallel.For(min, max + 1, i => 
             {
                 bool isPrime = true;
                 for (int j = 2; j <= Math.Sqrt(i); j++)
                 {
                     if (i % j == 0)
                     {
                         isPrime = false;
                         break;
                     }
                 }
                 if (isPrime)
                 {
                     lock (lockObj)
                     {
                         Count++;
                     }
                 }
             });

            Console.WriteLine(Count);
            Console.WriteLine(sw.Elapsed);
        }
    }
}
