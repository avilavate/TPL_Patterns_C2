using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_serach
{
    class AsOP : IOne, ITwo
    {
        public void Display1()
        {
            Console.WriteLine("Display1");
        }

        public void Display2()
        {
            Console.WriteLine("Display2");
        }
    }

    interface IOne
    { 
        void Display1();
    }

    interface ITwo
    {
        void Display2();
    }
}
