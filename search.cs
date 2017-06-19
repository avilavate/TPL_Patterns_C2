using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace text_serach
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
