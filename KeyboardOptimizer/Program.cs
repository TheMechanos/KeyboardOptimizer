using System;
using System.IO;
using System.Linq;

namespace KeyboardOptimizer
{
    class Program
    {
        static readonly int POPULATION_SIZE = 40;
        static readonly int CROSSOVER_PAIRS = 4;
        static readonly int CROSSOVER_PAIRS_MULTIPLY = 4;

        static readonly int GENERATIONS_NUMBER = 60;

        static readonly int MAX_MUTATION = 3;
        static readonly int MUTATION_CHANCE = 50;

        static void Main(string[] args)
        {

            string text = File.ReadAllText(@"C:\OneDrive\STUDIA\ATH\2_sem\MSI --- Metody\inputData.txt").ToUpper();
           
            double firstFitness;

            //Generate starting population
            KeyboardGeneration firstGen = new KeyboardGeneration();

            firstGen.generateRandom(POPULATION_SIZE);
            firstGen.calculateFitness(text);
            firstGen.sort();
            firstFitness = firstGen.population[0].lastFitness;

            firstGen.printBest(0, firstFitness);


            KeyboardGeneration lastGen = firstGen;

            for (int p = 0; p < GENERATIONS_NUMBER; p++)
            {
                KeyboardGeneration gen = new KeyboardGeneration();

                for(int m = 0; m < CROSSOVER_PAIRS_MULTIPLY; m++)
                {
                    for (int q = 0; q < CROSSOVER_PAIRS; q++)
                    {
                        gen.population.Add(Keyboard.crossOverOX(lastGen.population[(q * 2) + 0], lastGen.population[(q * 2) + 1]));
                        gen.population.Add(Keyboard.crossOverOX(lastGen.population[(q * 2) + 1], lastGen.population[(q * 2) + 0]));
                    }
                }
                gen.population.Add(lastGen.population[0]);
                gen.population.Add(lastGen.population[1]);

                gen.mutate(MAX_MUTATION, MUTATION_CHANCE);
                gen.calculateFitness(text);
                gen.sort();
                gen.printBest(p + 1, firstFitness);
                gen.population[0].printDesign();

                lastGen = gen;

            }         


        }
    }
}
