using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PozemiuRobotas.Obstacles;
using static PozemiuRobotas.Player;

namespace PozemiuRobotas
{
    public static class GameLogic
    {

        public static void getInput(Robot robot, int[,] map)
        {
            ConsoleKeyInfo input;

            input = Console.ReadKey();
            if (input.Key == ConsoleKey.UpArrow && robot.boty > 0 && (map[robot.boty - 1, robot.botx] == 1 || map[robot.boty - 1, robot.botx] >= 20))
                robot.boty--;
            else if (input.Key == ConsoleKey.DownArrow && robot.boty < map.GetLength(0) - 1 && (map[robot.boty + 1, robot.botx] == 1 || map[robot.boty + 1, robot.botx] >= 20))
                robot.boty++;
            else if (input.Key == ConsoleKey.LeftArrow && robot.botx > 0 && (map[robot.boty, robot.botx - 1] == 1 || map[robot.boty, robot.botx - 1] >= 20))
                robot.botx--;
            else if (input.Key == ConsoleKey.RightArrow && robot.botx < map.GetLength(1) - 1 && (map[robot.boty, robot.botx + 1] == 1 || map[robot.boty, robot.botx + 1] >= 20))
                robot.botx++;

            robot.battery--;
        }


        public static bool gameProcess(Robot robot, int[,] map, List<Spyke> spykes, List<Saw> saws, Enamy enamy)
        {
            bool hitSaw = false, hitSpyke = false, exited = false, hitEnamy = false, outOfBattery = false;

            if (robot.battery <= 0)
            {
                outOfBattery = true;
                return endGame(hitEnamy, hitSpyke, hitSaw, exited, map, robot, outOfBattery);
            }

            robot.moveCounter++;
            if (robot.moveCounter > 3)
            {
                robot.moveCounter = 0;
                foreach (Spyke spyke in spykes)
                    spyke.Move();
            }

            foreach (Saw saw in saws)
                saw.Move(map);
            if (enamy.active)
                enamy.Chase(robot.botx, robot.boty);

            hitSaw = false;
            foreach (Saw saw in saws)
                if (robot.botx == saw.x && robot.boty == saw.y)
                    hitSaw = true;
            if (hitSaw)
                return endGame(hitEnamy, hitSpyke, hitSaw, exited, map, robot, outOfBattery);

            if (robot.botx == enamy.x && robot.boty == enamy.y)
            {
                hitEnamy = true;
                return endGame(hitEnamy, hitSpyke, hitSaw, exited, map, robot, outOfBattery);
            }

            if (spykes[0].up)
            {
                hitSpyke = false;
                foreach (Spyke spyke in spykes)
                    if (robot.botx == spyke.x && robot.boty == spyke.y)
                        hitSpyke = true;
                if (hitSpyke)
                    return endGame(hitEnamy, hitSpyke, hitSaw, exited, map, robot, outOfBattery);
            }

            if (map[robot.boty, robot.botx] == 36)
            {
                exited = true;
                return endGame(hitEnamy, hitSpyke, hitSaw, exited, map, robot, outOfBattery);
            }
            else if (map[robot.boty, robot.botx] == 35)
            {
                map[robot.boty, robot.botx] = 1;
                robot.gotKey = true;
                robot.addBattery();

            }
            else if (map[robot.boty, robot.botx] == 34)
            {
                map[robot.boty, robot.botx] = 1;
                robot.roomNr++;
                robot.addBattery();
            }
            else if (map[robot.boty, robot.botx] >= 20 && map[robot.boty, robot.botx] <= 29)
                return endGame(hitEnamy, hitSpyke, hitSaw, exited, map, robot, outOfBattery);

            if (robot.gotKey)
            {
                enamy.releasCounter++;
                if (enamy.releasCounter > 10)
                    enamy.active = true;
            }

            if (robot.roomNr >= 2)
            {
                map[56, 50] = 1;
                map[56, 49] = 30;
                map[56, 51] = 31;
            }

            if (robot.gotKey && Math.Sqrt(Math.Pow(44 - robot.boty, 2) + Math.Pow(50 - robot.botx, 2)) <= 3)
            {
                map[44, 50] = 1;
                map[44, 49] = 32;
                map[44, 51] = 33;
            }
            return false;
        }

        public static bool endGame(bool hitEnamy, bool hitSpyke, bool hitSaw, bool exited, int[,] map, Robot robot, bool outOfBattery)
        {
            Console.Clear();
            if (exited)
            {
                Console.WriteLine("====================");
                Console.WriteLine("    You survived");
                Console.WriteLine("====================");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("====================");
                Console.WriteLine("      You died");
                Console.WriteLine("====================");

                if (map[robot.boty, robot.botx] == 20)
                    Console.WriteLine("Fenll into spykes");
                else if (map[robot.boty, robot.botx] == 21)
                    Console.WriteLine("Fell into a pit");
                else if (hitSaw)
                    Console.WriteLine("Got hit by a saw");
                else if (hitSpyke)
                    Console.WriteLine("Got hit by a hiden spyke");
                else if (hitEnamy)
                    Console.WriteLine("Got hit by an enamy");
                else if (outOfBattery)
                    Console.WriteLine("You ran out of battery");

                Console.ReadKey();
            }

            return true;
        }
    }

}
