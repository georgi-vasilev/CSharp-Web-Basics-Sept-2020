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
        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();
            List<Task> tasks = new List<Task>();
            for (int i = 1; i <= 100; i++)
            {
                var task = Task.Run(() => DownloadAsync(i));
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine(sw.Elapsed);
        }

        private static async Task DownloadAsync(int i)
        {
            HttpClient httpClient = new HttpClient();
            var url = $"https://vicove.com/vic-{i}";
            var httpResponse = await httpClient.GetAsync(url);
            var vic = await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine(vic.Length);
        }

        private static void PrintPrimeCount()
        {
            Stopwatch sw = Stopwatch.StartNew();
            int n = 10000000;
            int count = 0;
            for (int i = 1; i <= n; i++)
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
                    count++;
                }
            }

            Console.WriteLine(count);
            Console.WriteLine(sw.Elapsed);
        }
    }
}
