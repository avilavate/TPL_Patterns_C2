using System;
using System.Linq;
using System.IO;

namespace Interlocked
{
    class Search
    {
        public Input Inputs { get; set; }

        public Search(Input Input)
        {
            this.Inputs = Input;
        }
        public Search()
        {
            this.Get_Inputs();
        }

        public int Deep_Search_and_Cot()
        {
            var lines = File.ReadAllLines(Inputs.Path);
            var q1 = lines.Select(line =>
            {
                var words = line.Split(' ').ToList();
                var total = words.Where(w => w == Inputs.Text).Count();
                return total;
            });
            var sum = 0;
            q1.ToList().ForEach(i => { sum += i; });
            return sum;
        }
        public int Search_and_Count()
        {
            var lines = File.ReadAllLines(Inputs.Path);

            var q = from line in lines
                    where line.Contains(Inputs.Text)
                    select line;
           
            return q.Count();
        }

        private void Get_Inputs()
        {
            Console.WriteLine($"Folder Path?");
            var path = Console.ReadLine();
            Console.WriteLine($"Search Text?");
            var text = Console.ReadLine();
            Console.WriteLine($"Your entered: \n Path: {path} \t Search Tetx: {text} ");

            this.Inputs = new Input { Path = path, Text = text };
        }
    }
}
