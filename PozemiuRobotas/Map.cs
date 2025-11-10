using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PozemiuRobotas.Obstacles;

namespace PozemiuRobotas
{
    public static class Map
    {
        public static void generateMap(int[,] map, int botx, int boty, List<Saw> saws, List<Spyke> spykes)
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    map[i, j] = 0;

            for (int i = botx - 5; i <= botx + 5; i++)
                for (int j = boty - 5; j <= boty + 5; j++)
                    map[i, j] = 1;

            Random random = new Random();
            int hallwayLength = 10 + random.Next(0, 5);
            int x, y;
            bool found;

            //EXIT
            for (int i = boty; i > boty - hallwayLength; i--)
            {
                map[i, boty] = 1;
                map[i, boty + 1] = 1;
                map[i, boty - 1] = 1;
            }

            map[boty - hallwayLength, boty] = 10;
            map[boty - hallwayLength, boty + 1] = 10;
            map[boty - hallwayLength, boty - 1] = 10;

            map[boty - 6, boty] = 11;
            map[boty - 6, boty - 1] = 11;
            map[boty - 6, boty + 1] = 11;


            //KEY
            hallwayLength = 15 + random.Next(0, 10);
            for (int i = boty; i < boty + hallwayLength; i++)
            {
                map[i, boty] = 1;
                map[i, boty + 1] = 1;
                map[i, boty - 1] = 1;
            }
            //lines of spykes, affter the keay, something chases you


            for (int traps = 0; traps < 10; traps++)
            {
                x = random.Next(-1, 2) + botx;
                y = random.Next(boty + 7, boty + hallwayLength);

                found = false;
                for (int i = y - 1; i <= y + 1; i++)
                    for (int j = x - 1; j <= x + 1; j++)
                        if (map[i, j] == 20)
                            found = true;
                if (!found)
                    map[y, x] = 20;
            }

            map[boty + 6, boty] = 12;
            map[boty + 6, boty - 1] = 12;
            map[boty + 6, boty + 1] = 12;


            //LEFT
            hallwayLength = 20 + random.Next(0, 10);
            for (int i = botx; i > botx - hallwayLength; i--)
            {
                map[botx, i] = 1;
                map[botx + 1, i] = 1;
                map[botx - 1, i] = 1;
            }


            for (int traps = 0; traps < 20; traps++)
            {
                y = random.Next(-1, 2) + boty;
                x = random.Next(botx - hallwayLength + 1, botx - 7);

                found = false;
                for (int i = y - 1; i <= y + 1; i++)
                    for (int j = x - 1; j <= x + 1; j++)
                        if (map[i, j] == 21)
                            found = true;
                if (!found)
                    map[y, x] = 21;
            }

            //mooving saws
            x = random.Next(botx - hallwayLength - 10, botx - hallwayLength) - 10;
            for (int i = botx - hallwayLength; i > x; i--)
                for (int j = boty - 3; j <= boty + 3; j++)
                    map[j, i] = 1;

            for (int i = botx - hallwayLength - 1; i > x + 2; i--)
                if (random.Next(0, 2) == 0)
                    saws.Add(new Saw(i, (boty + random.Next(-3, 4)), random.Next(0, 2) != 0));

            map[boty, x + 1] = 34;


            //RIGHT
            hallwayLength = 20 + random.Next(0, 10);
            for (int i = botx; i < botx + hallwayLength; i++)
            {
                map[botx, i] = 1;
                map[botx + 1, i] = 1;
                map[botx - 1, i] = 1;
            }
            //map[botx, botx + hallwayLength - 1] = 0;
            //map[botx, botx + 7] = 0;
            for (int traps = 0; traps < 10; traps++)
            {
                y = random.Next(-1, 2) + boty;
                x = random.Next(botx + 7, botx + hallwayLength - 1);

                found = false;
                for (int i = y - 1; i <= y + 1; i++)
                    for (int j = x - 1; j <= x + 1; j++)
                        if (map[i, j] == 20)
                            found = true;
                if (!found)
                    map[y, x] = 20;
            }

            //spykes going up and down
            int tempx = random.Next(botx + hallwayLength + 1, botx + hallwayLength + 10) + 10;
            for (int i = botx + hallwayLength; i < tempx; i++)
                for (int j = boty - 5; j <= boty + 5; j++)
                    map[j, i] = 1;

            map[(random.Next(-5, 6) + boty), (random.Next(botx + hallwayLength + 8, tempx))] = 34;

            for (int i = 0; i < 80; i++)
            {
                x = (random.Next(-5, 6) + boty);
                y = random.Next(botx + hallwayLength, tempx);
                if (map[x, y] == 1)
                    spykes.Add(new Spyke(y, x, true));
            }

        }

        public static void drawMap(int[,] map, int botx, int boty, List<Saw> saws, List<Spyke> spykes)
        {
            for (int i = 0; i < 22; i++)
                Console.Write("//");
            Console.WriteLine();

            for (int i = (boty - 10 < 0 ? 0 : (boty < map.GetLength(0) - 10 ? boty - 10 : boty - 10 - (10 - (map.GetLength(0) - boty)))); i < map.GetLength(0) && (boty > 10 ? i < boty + 10 : i < boty + 10 + (10 - boty)); i++)
            {
                Console.Write("//");

                for (int j = (botx - 10 < 0 ? 0 : (botx < map.GetLength(1) - 10 ? botx - 10 : botx - 10 - (10 - (map.GetLength(1) - botx)))); j < map.GetLength(1) && (botx > 10 ? j < botx + 10 : j < botx + 10 + (10 - botx)); j++)
                {
                    bool isSaw = false;
                    foreach (var saw in saws)
                        if (saw.x == j && saw.y == i)
                            isSaw = true;

                    bool isSpyke = false;
                    foreach (var spyke in spykes)
                        if (spyke.x == j && spyke.y == i)
                            isSpyke = true;



                    if (i == boty && j == botx)
                        Console.Write("()");
                    else if (Math.Sqrt(Math.Pow(i - boty, 2) + Math.Pow(j - botx, 2)) >= 9)
                        Console.Write("██");
                    else if (Math.Sqrt(Math.Pow(i - boty, 2) + Math.Pow(j - botx, 2)) >= 6 && map[i, j] != 0)
                        Console.Write("▓▓");
                    else if (map[i, j] == 0)
                        Console.Write("██");
                    else if (isSaw)
                        Console.Write("{}");
                    else if (isSpyke && spykes[0].up)
                        Console.Write(@"/\");
                    else if(isSpyke)
                        Console.Write("~~");
                    else if (map[i, j] == 1)
                        Console.Write("  ");
                    else if (map[i, j] == 10)
                        Console.Write("EE");
                    else if (map[i, j] == 11)
                        Console.Write("__");
                    else if (map[i, j] == 12)
                        Console.Write("--");
                    else if (map[i, j] == 20)
                        Console.Write("^^");
                    else if (map[i, j] == 21)
                        Console.Write("[]");
                    else if (map[i, j] == 30)
                        Console.Write("- ");
                    else if (map[i, j] == 31)
                        Console.Write(" -");
                    else if (map[i, j] == 32)
                        Console.Write("_ ");
                    else if (map[i, j] == 33)
                        Console.Write(" _");
                    else if (map[i, j] == 34)
                        Console.Write("++");

                    //░▒▓
                }

                Console.Write("//");
                Console.WriteLine();
            }
            for (int i = 0; i < 22; i++)
                Console.Write("//");

        }
    }
}
