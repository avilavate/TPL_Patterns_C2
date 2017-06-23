using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interlocked
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var tasks = new List<Task>();
            var hits = 0;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < 20; i++)
            {
                var fileName = $"./log{i}.txt";
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    const string Search_Text = "Sir Arthur Conan Doyle";
                    Search s = new Search(new Input { Path = fileName, Text = Search_Text });
                    var local_hit = s.Search_and_Count();
                    System.Threading.Interlocked.Add(ref hits, local_hit);
                }));
            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Total counts: \n \t Sir Arthur Conan Doyle : {hits}");
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Seqential Algo time: {elapsedMs / 1000d}");
        }
    }
}
