using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KeyboardOptimizer
{
    class KeyboardGeneration
    {
        public List<Keyboard> population = new List<Keyboard>();

        public KeyboardGeneration()
        {
            population.Clear();
        }

        public void generateRandom(int len)
        {
            population.Clear();

            for(int q = 0; q < len; q++)
            {
                Keyboard kb = new Keyboard();
                kb.designRandom();
                population.Add(kb);
            }
        }

        public void calculateFitness(string text)
        {
            for(int q = 0; q < population.Count; q++)
            {
                ProgressBar.draw(q, population.Count);
                population[q].calcFitness(text);
            }
            ProgressBar.cls();
           
        }

        public void mutate(int maxMutation, int mutationPercentageChance)
        {
            Random rnd = new Random();

            foreach (Keyboard keyb in population)
            {
                for (int q = 0; q < maxMutation; q++)
                {
                   
                    int randomPercentage = rnd.Next(0, 101);

                    if (randomPercentage < mutationPercentageChance)
                        keyb.mutate(1);
                    
                }
            }
        }

        public void sort()
        {
            population = population.OrderBy(a => a.lastFitness).ToList();
        }

        public void print()
        {
            for (int q = 0; q < population.Count; q++)
            {
                Console.WriteLine(q+" => Fitness: "+ population[q].lastFitness);
            }
                
        }

        public void printBest(int idx, double firstFitness)
        {
            int percent = (int)((firstFitness * 100) / population[0].lastFitness);

            Console.Write("Gen " + idx+" "+ percent+"%");
            Console.CursorLeft = 17;
            Console.WriteLine("BEST= " + population[0].lastFitness);
        }

    }
}
