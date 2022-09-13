using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardOptimizer
{
    class ProgressBar
    {
        public static void draw(int progress, int total)
        {
            Console.CursorVisible = false;

            //draw empty progress bar
            Console.CursorLeft = 0;
            Console.Write("["); //start
            Console.CursorLeft = 62;
            Console.Write("]"); //end
            Console.CursorLeft = 1;
            float onechunk = 60.0f / total;

            //draw filled part
            int position = 1;
            for (int i = 0; i < onechunk * progress; i++)
            {
                //Console.BackgroundColor = ConsoleColor.Green;
                Console.CursorLeft = position++;
                Console.Write("-");
            }

            //draw unfilled part
            for (int i = position; i <= 31; i++)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw totals
            Console.CursorLeft = 65;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(progress.ToString() + " of " + total.ToString() + "    ");
        }

        public static void cls()
        {
            Console.CursorLeft = 0;
            for(int q = 0; q < 100; q++)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" ");
            }
            Console.CursorLeft = 0;

            
        }

    }
}
