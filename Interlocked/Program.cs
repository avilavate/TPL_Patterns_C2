using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interlocked
{
    class Program
    {
        static void Main(string[] args)
        {
            const string Search_Text = "Sir Arthur Conan Doyle";
            var tasks = new List<Task>();
            var hits = 0;

            for (int i = 0; i < 20; i++)
            {
                var fileName = $"./log{i}.txt";
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    Search s = new Search(new Input { Path = fileName, Text = Search_Text });
                    var local_hit = s.Search_and_Count();
                    System.Threading.Interlocked.Add(ref hits, local_hit);
                }));
            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Total counts: \n \t {Search_Text} : {hits}");
        }
    }
}
