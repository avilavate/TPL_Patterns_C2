using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_serach
{
    class Task_PassbyValue
    {
        public void Execute()
        {
            string testString = "before";

            var t = Task.Factory.StartNew(() =>
            {
                //  var value = args as string;
                //   value = "after";
                testString = "after";
                Console.WriteLine($" inner value: {testString}");
            });

            t.Wait();
            Console.WriteLine($"outer value: {testString}");
        }
    }
}
