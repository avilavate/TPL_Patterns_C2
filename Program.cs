using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_serach
{
    class Program
    {
        static void Main(string[] args)
        {
            const string Search_Text = " The ";
            DirectoryInfo dir = new DirectoryInfo("./files");
            IEnumerable<FileInfo> files = dir.EnumerateFiles();
            int hits = 0, hitSeq = 0;

            //Sequential Version:
            foreach (var file in files)
            {
                Search s = new Search(new Input { Path = file.Name, Text = Search_Text });
                hitSeq += s.Search_and_Count();
            }
            Console.WriteLine($"Total counts: \n \t {Search_Text} : {hitSeq}");

            List<Task<int>> tasks = new List<Task<int>>();
            var obj = new Object();
            foreach (var file in files)
            {
                tasks.Add(Task<int>.Factory.StartNew((arg) =>
                {
                    var fileItem = (FileInfo)arg;
                    var localHits = 0;
                    Search s = new Search(new Input { Path = fileItem.Name, Text = Search_Text });
                    lock (obj)
                    {
                        localHits = s.Search_and_Count();
                    }
                    return localHits;
                }, file));
            }

            while (tasks.Count() > 0)
            {

                try
                {
                    var t = Task<int>.WaitAny(tasks.ToArray());
                    var t_current = tasks[t];
                    hits += t_current.Result;
                    tasks.RemoveAt(t);

                }
                catch (AggregateException AE)
                {
                    Console.WriteLine(AE.Flatten().InnerExceptions[0].Message);
                }
            }

            Console.WriteLine($"Total counts: \n \t {Search_Text} : {hits}");

        }
    }
}
