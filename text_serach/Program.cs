using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace text_serach
{
    class Program
    {
        static void Main(string[] args)
        {
            //const string Search_Text = "Sir Arthur Conan Doyle";
            DirectoryInfo dir = new DirectoryInfo("./files");
            IEnumerable<FileInfo> files = dir.EnumerateFiles();
            int hits = 0, hitSeq = 0;

            //Sequential Version:

            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here
          
            foreach (var file in files)
            {
                const string Search_Text = "Sir Arthur Conan Doyle";
                Search s = new Search(new Input { Path = "./files/" + file.Name, Text = Search_Text });
                hitSeq += s.Search_and_Count();
            }
            Console.WriteLine($"Total counts: \n \t Sir Arthur Conan Doyle : {hitSeq}");

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Seqential Algo time: {elapsedMs/1000}");
            List<Task<int>> tasks = new List<Task<int>>();
            var obj = new Object();

            foreach (var file in files)
            {
                tasks.Add(Task<int>.Factory.StartNew((arg) =>
                {
                    const string Search_Text = "Sir Arthur Conan Doyle";
                    var fileItem = (FileInfo)arg;
                    var localHits = 0;
                    Search s = new Search(new Input { Path = fileItem.Name, Text = Search_Text });
                    //lock (obj)
                    //{
                        localHits = s.Search_and_Count();
                    //}
                    return localHits;
                }, file));
            }

            var bag = new System.Collections.Concurrent.ConcurrentBag<Task<int>>();

            var watch1 = System.Diagnostics.Stopwatch.StartNew();
            Parallel.ForEach(files, file =>
            {
                bag.Add(Task<int>.Factory.StartNew((arg) =>
                {
                    const string Search_Text = "Sir Arthur Conan Doyle";
                    var fileItem = (FileInfo)arg;
                    var localHits = 0;
                    Search s = new Search(new Input { Path = "./files/" + file.Name, Text = Search_Text });
                    localHits = s.Search_and_Count();
                    return localHits;
                }, file));
            });

            var tasks1 = bag.ToList<Task<int>>();
            while (tasks1.Count() > 0)
            {
                try
                {
                    var t = Task<int>.WaitAny(tasks1.ToArray());
                    var t_current = tasks[t];
                    hits += t_current.Result;
                    tasks1.RemoveAt(t);
                }
                catch (AggregateException AE)
                {
                    Console.WriteLine(AE.Flatten().InnerExceptions[0].Message);
                }
            }

            Console.WriteLine($"Total counts: \n \t Sir Arthur Conan Doyle : {hits}");
            watch1.Stop();
            var elapsedMs1 = watch1.ElapsedMilliseconds;
            Console.WriteLine($"Parallel Algo time: {elapsedMs1/1000d}");

            Task_PassbyValue tp = new Task_PassbyValue();
            tp.Execute();

            IOne obj1 = new AsOP();
            ITwo obj2 = new AsOP();
            var lst = new List<int>();

            Console.WriteLine($"{obj1 is IOne} + {obj2 is ITwo} +{obj1 is AsOP} + {"fg" is string}");
            var total = 0;
            foreach (var file1 in files)
            {
                Search s1 = new Search(new Input { Path = file1.Name, Text = "Sir Arthur Conan Doyle" });
                total += s1.Search_and_Count();
            }
            Console.WriteLine($"Deep Search: {total}");

            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict.Add(@"avil's", "Avate");
            Console.WriteLine(dict[@"avil's"]);
        }
    }
}
