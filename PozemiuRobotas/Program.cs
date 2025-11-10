using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PozemiuRobotas.Obstacles;

namespace PozemiuRobotas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo input;
            int botx = 50, boty = 50, mapx = 100, mapy = 100;
            int[,] map = new int[mapx, mapy];
            int roomNr = 0, moveCounter = 0;
            bool hitSaw = false, hitSpyke = false;



            List<Saw> saws = new List<Saw>();
            List<Spyke> spykes = new List<Spyke>();

            Map.generateMap(map, botx, boty, saws, spykes);

            Console.WriteLine("Press any button");
            Console.ReadKey();

            while (true)
            {
                Console.Clear();

                Map.drawMap(map, botx, boty, saws, spykes);

                input = Console.ReadKey();
                if (input.Key == ConsoleKey.UpArrow && boty > 0 && (map[boty - 1, botx] == 1 || map[boty - 1, botx] >= 20))
                    boty--;
                else if (input.Key == ConsoleKey.DownArrow && boty < mapx - 1 && (map[boty + 1, botx] == 1 || map[boty + 1, botx] >= 20))
                    boty++;
                else if (input.Key == ConsoleKey.LeftArrow && botx > 0 && (map[boty, botx - 1] == 1 || map[boty, botx - 1] >= 20))
                    botx--;
                else if (input.Key == ConsoleKey.RightArrow && botx < mapy - 1 && (map[boty, botx + 1] == 1 || map[boty, botx + 1] >= 20))
                    botx++;

                moveCounter++;
                if (moveCounter > 3)
                {
                    moveCounter = 0;
                    foreach (Spyke spyke in spykes)
                        spyke.Move();
                }

                foreach (Saw saw in saws)
                    saw.Move(map);

                hitSaw = false;
                foreach (Saw saw in saws)
                    if (botx == saw.x && boty == saw.y)
                        hitSaw = true;
                if (hitSaw)
                    break;

                if (spykes[0].up)
                {
                    hitSpyke = false;
                    foreach (Spyke spyke in spykes)
                        if (botx == spyke.x && boty == spyke.y)
                            hitSpyke = true;
                    if (hitSpyke)
                        break;
                }

                if (map[boty, botx] == 34)
                {
                    map[boty, botx] = 1;
                    roomNr++;
                }
                else if (map[boty, botx] >= 20 && map[boty, botx] <= 29)
                    break;
            }
            Console.Clear();
            Console.WriteLine("You died");

            if (map[boty, botx] == 20)
                Console.WriteLine("Fenll into spykes");
            else if (map[boty, botx] == 21)
                Console.WriteLine("Fell into a pit");
            else if (hitSaw)
                Console.WriteLine("Got hit by a saw");
            else if (hitSpyke)
                Console.WriteLine("Got hit by a hiden Spyke");

            Console.WriteLine("Press any button");
            Console.ReadKey();
            //Console.WriteLine(input.KeyChar);

            //drawMap(map);
        }
    }
}
