using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardOptimizer
{
    class Keyboard
    {
        public static readonly char[] charDefines = { 'Q', 'W', 'E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L',':','Z','X','C','V','B','N','M',',','.','?'};
        public static readonly double[] charEffort = { 4,2,2,3,4,5,3,2,2,4,1.5,1,1,1,3,3,1,1,1,1.5,4,4,3,2,5,3,2,3,4,4 }; //http://kordos.com/images/keyboard_effort1.png

        public int[] design;
        public double lastFitness;

        public Keyboard()
        {
            design = new int[30];
            Array.Fill(design, -1);
            lastFitness = int.MaxValue;
        }
        public Keyboard(int[] startDesign)
        {
            if(startDesign.Length == 30)
                design = startDesign;

            lastFitness = int.MaxValue;
        }

        public void designDef()
        {
            for(int q = 0; q < 30; q++)
            {
                design[q] = q;
            }
            lastFitness = int.MaxValue;
        }
        public void designRandom()
        {
            var rnd = new Random();
            var numbers = Enumerable.Range(0, 30).OrderBy(x => rnd.Next()).Take(30).ToList();
            design = numbers.ToArray();

            lastFitness = int.MaxValue;
        }

        public void mutate(int iteartes)
        {
            for(int q = 0; q < iteartes; q++)
            {
                var rnd = new Random();
                var pair = Enumerable.Range(0, 30).OrderBy(x => rnd.Next()).Take(2).ToList();

                int tmp = design[pair[0]];

                design[pair[0]] = design[pair[1]];
                design[pair[1]] = tmp;
            }

            lastFitness = int.MaxValue;
        }

        public static Keyboard crossOverOX(Keyboard par1, Keyboard par2, bool print=false)
        {
            int[] childDesign = new int[30];
            Array.Fill(childDesign, -1);

            int[] p1 = par1.design;
            int[] p2 = par2.design;

            var rnd = new Random();
            int cut1 = rnd.Next(1, 28);
            int cut2 = rnd.Next(cut1 + 1, 29);

            //COPY SECTION
            for (int q = cut1 + 1; q <= cut2; q++)
            {
                childDesign[q] = p1[q];
            }


            for (int q = 0; q < 30; q++)
            {
                if (q <= cut1 || q > cut2)
                {
                    for (int x = 0; x < 30; x++)
                    {
                        if (!childDesign.Contains(p2[x])) {
                            childDesign[q] = p2[x];
                            break;
                        }
                    }

                }
            }

            if (print) { 
                Console.WriteLine("");
                for (int q = 0; q < 30; q++)
                {
                    Console.Write(p1[q] + " ");
                    if (p1[q] < 10) Console.Write(" ");

                    if (q == cut1 || q == cut2) Console.Write("| ");
                }
                Console.WriteLine("");
                for (int q = 0; q < 30; q++)
                {
                    Console.Write(p2[q] + " ");
                    if (p2[q] < 10) Console.Write(" ");

                    if (q == cut1 || q == cut2) Console.Write("| ");
                }
                Console.Write("\n");
                for (int q = 0; q < 93; q++) Console.Write("-");
                Console.Write("\n");
                for (int q = 0; q < 30; q++)
                {
                    Console.Write(childDesign[q] + " ");
                    if (childDesign[q] < 10) Console.Write(" ");

                    if (q == cut1 || q == cut2) Console.Write("| ");
                }
                Console.WriteLine("");
            }


            return new Keyboard(childDesign);
        }


        public void printDesign()
        {
            Console.WriteLine("-----------------------");

            Console.Write("|");
            for (int q = 0; q < 10; q++)
            {
                Console.Write(charDefines[design[q]]);
                Console.Write("|");
            }

            Console.Write("\n |");
            for (int q = 10; q < 20; q++)
            {
                Console.Write(charDefines[design[q]]);
                Console.Write("|");
            }

            Console.Write("\n  |");
            for (int q = 20; q < 30; q++)
            {
                Console.Write(charDefines[design[q]]);
                Console.Write("|");
            }
            Console.WriteLine("\n-----------------------");
            Console.WriteLine("");
        }



        public double calcFitness(string text)
        {
            double f1=0, f2=0;

            //F1 -----------------------------------
            int[] speeches = new int[30];

            foreach (var actChar in text.ToCharArray())
            {
                for (int q = 0; q < 30; q++)
                {
                    if (actChar == charDefines[design[q]])
                    {
                        speeches[q]++;
                        break;
                    }
                }
            }

            for (int q = 0; q < 30; q++)
            {
                f1 += speeches[q] * charEffort[q];
            }
            //F1 -----------------------------------

            //F2 -----------------------------------

            int lastPositionInDesign = -1;

            foreach (var actChar in text.ToCharArray())
            {
                int actualPositionInDesign = -1;

                for (int q = 0; q < 30; q++)
                {
                    if (actChar == charDefines[design[q]])
                    {
                        actualPositionInDesign = q;
                    }
                }
                //Console.WriteLine(lastPositionInDesign + " " + actualPositionInDesign);

                if(lastPositionInDesign >= 0 && actualPositionInDesign >= 0
                    && lastPositionInDesign % 10 == actualPositionInDesign % 10
                    && lastPositionInDesign != actualPositionInDesign)
                {
                    int row1 = lastPositionInDesign / 10;
                    int row2 = actualPositionInDesign / 10;

                    int rowDif = Math.Abs(row1 - row2);

                    //Console.WriteLine(lastPositionInDesign + " " + actualPositionInDesign+ " "+rowDif);
                    //break;

                    if(rowDif == 1)
                    {
                        f2 += 3;
                    } 
                    else if(rowDif == 2)
                    {
                        f2 += 6;
                    }
                }

                lastPositionInDesign = actualPositionInDesign;
            }
            //F2 -----------------------------------


            lastFitness = f1 + f2;
            return lastFitness;
        }


    }
}
