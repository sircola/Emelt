using System;

namespace SZTF1Zh1
{
    class Program
    {
        static bool CheckNeighbours(char[,] forest, int i, int j, int soughtValue)
        {
            for (int x = (i>0?i-1:0); x < (i+1<forest.GetLength(0)?i+1:forest.GetLength(0)); x++)
            {
                for (int y = (j > 0 ? j - 1 : 0); y < (j + 1 < forest.GetLength(1) ? j + 1 : forest.GetLength(1)); y++)
                {
                    if (x == i && y == j)
                        continue;
                    if (forest[x, y] == soughtValue)
                        return true;
                }
            }
            return false;
        }

        static char[,] GenerateForest()
        {
            char[,] erdő = new char[20, 20];
            var rng = new Random();

            for (int i = 0; i < erdő.GetLength(0); i++)
            {
                for (int j = 0; j < erdő.GetLength(1); j++)
                {
                    int n = rng.Next(1, 101);
                    if( n <= 35)
                    {
                        erdő[i, j] = 'T';
                    }
                    else
                    if( n <= 60)
                    {
                        erdő[i, j] = (char)('1' + rng.Next(0, 5));
                    }
                    else
                    if( n<= 80 )
                    {
                        if (CheckNeighbours(erdő, i, j, 'C'))
                        {
                            if (rng.Next(1, 101) <= 5)
                                erdő[i, j] = 'C';
                        }
                        else
                            erdő[i,j] = 'C';
                    }
                }
            }
            return erdő;
        }

        static void Show( char[,] forest)
        {
            for (int i = 0; i < forest.GetLength(0); i++)
            {
                for (int j = 0; j < forest.GetLength(1); j++)
                {
                    Console.Write(""+(forest[i, j]==0?' ': forest[i, j]) + ' ');
                }
                Console.WriteLine();
            }
            for (int i = 0; i < forest.GetLength(1); i++)
            {
                Console.Write("- ");
            }
            Console.WriteLine();
        }

        static int NbrOfEntries(char[,] forest, char target )
        {
            int n = 0;
            for (int i = 0; i < forest.GetLength(0); i++)
            {
                for (int j = 0; j < forest.GetLength(1); j++)
                {
                    if (forest[i, j] == target)
                        ++n;
                }
            }
            return n;
        }

        static bool DistributedEntries( char[,] forest, char target )
        {
            for (int i = 0; i < forest.GetLength(0); i++)
            {
                bool volt = false;
                for (int j = 0; j < forest.GetLength(1); j++)
                    if (forest[i, j] == target)
                    {
                        volt = true;
                        continue;
                    }
                if (volt == false)
                    return false;
            }
            return true;
        }

        static void SpawnGhost( char [,] forest )
        {
            var rng = new Random();

            for (int i = 0; i < forest.GetLength(0); i++)
                for (int j = 0; j < forest.GetLength(1); j++)
                    if( forest[i,j] == 0 && (rng.Next(1,101) <= 15) )
                        forest[i, j] = '*';
        }

        static void RemoveCandies( char[,] forest)
        {
            for (int i = 0; i < forest.GetLength(0); i++)
                for (int j = 0; j < forest.GetLength(1); j++)
                    if (forest[i, j] == '*')
                    {
                        for (int x = 0; x < forest.GetLength(0); x++)
                            if (forest[x, j] >= '1' && forest[x, j] <= '5')
                                forest[x, j] = (char)0;
                        for (int y = 0; y < forest.GetLength(1); y++)
                            if (forest[i, y] >= '1' && forest[i, y] <= '5')
                                forest[i, y] = (char)0;
                    }
        }

        static int NbrOfCandies( char[,] forest )
        {
            int cukorka = 0;
            for (int i = 0; i < forest.GetLength(0); i++)
                for (int j = 0; j < forest.GetLength(1); j++)
                    if (forest[i, j] >= '1' && forest[i, j] <= '5')
                        ++cukorka;
            return cukorka;
        }

        static char[] GetAllEntries( char[,] forest )
        {
            int n = 0;
            for (int i = 0; i < forest.GetLength(0); i++)
                for (int j = 0; j < forest.GetLength(1); j++)
                    if (forest[i, j] != 0)
                        ++n;

            char[] T = new char[n];
            n = 0;

            for (int i = 0; i < forest.GetLength(0); i++)
                for (int j = 0; j < forest.GetLength(1); j++)
                    if (forest[i, j] != 0)
                        T[n++] = forest[i, j];

            return T;
        }

        static void Main(string[] args)
        {
            char[,] erdő = GenerateForest();
            Show(erdő);
            Console.WriteLine("kölök: " + NbrOfEntries(erdő, 'C'));
            Console.WriteLine("fák: " + DistributedEntries(erdő, 'T'));

            int n = NbrOfCandies(erdő);
            SpawnGhost(erdő);
            RemoveCandies(erdő);
            Console.WriteLine("" + (n - NbrOfCandies(erdő)) + " cukorkát ettek meg.");
            Show(erdő);
            
            char[] t = GetAllEntries(erdő);

            Console.ReadLine();
        }
    }
}
