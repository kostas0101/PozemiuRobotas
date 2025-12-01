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
            if (input.Key == ConsoleKey.UpArrow && robot.GetY() > 0 && (map[robot.GetY() - 1, robot.GetX()] == 1 || map[robot.GetY() - 1, robot.GetX()] >= 20))
                robot.MoveUp();
            else if (input.Key == ConsoleKey.DownArrow && robot.GetY() < map.GetLength(0) - 1 && (map[robot.GetY() + 1, robot.GetX()] == 1 || map[robot.GetY() + 1, robot.GetX()] >= 20))
                robot.MoveDown();
            else if (input.Key == ConsoleKey.LeftArrow && robot.GetX() > 0 && (map[robot.GetY(), robot.GetX() - 1] == 1 || map[robot.GetY(), robot.GetX() - 1] >= 20))
                robot.MoveLeft();
            else if (input.Key == ConsoleKey.RightArrow && robot.GetX() < map.GetLength(1) - 1 && (map[robot.GetY(), robot.GetX() + 1] == 1 || map[robot.GetY(), robot.GetX() + 1] >= 20))
                robot.MoveRight();

            robot.DecresBatteryLevel();
        }


        public static bool gameProcess(Robot robot, int[,] map, List<Spyke> spykes, List<Saw> saws, Enamy enamy)
        {
            bool hitSaw = false, hitSpyke = false, exited = false, hitEnamy = false, outOfBattery = false;

            if (robot.GetBatteryLevel() <= 0)
            {
                outOfBattery = true;
                return endGame(hitEnamy, hitSpyke, hitSaw, exited, map, robot, outOfBattery);
            }

            robot.IncressMoveCounter();
            if (robot.GetMoveCounter() > 3)
            {
                robot.REsetMoveCounter();
                foreach (Spyke spyke in spykes)
                    spyke.Move();
            }

            foreach (Saw saw in saws)
                saw.Move(map);
            if (enamy.GetStatus())
                enamy.Chase(robot.GetX(), robot.GetY());

            hitSaw = false;
            foreach (Saw saw in saws)
                if (robot.GetX() == saw.GetX() && robot.GetY() == saw.GetY())
                    hitSaw = true;
            if (hitSaw)
                return endGame(hitEnamy, hitSpyke, hitSaw, exited, map, robot, outOfBattery);

            if (robot.GetX() == enamy.GetX() && robot.GetY() == enamy.GetY())
            {
                hitEnamy = true;
                return endGame(hitEnamy, hitSpyke, hitSaw, exited, map, robot, outOfBattery);
            }

            if (spykes[0].GetStatus())
            {
                hitSpyke = false;
                foreach (Spyke spyke in spykes)
                    if (robot.GetX() == spyke.GetX() && robot.GetY() == spyke.GetY())
                        hitSpyke = true;
                if (hitSpyke)
                    return endGame(hitEnamy, hitSpyke, hitSaw, exited, map, robot, outOfBattery);
            }

            if (map[robot.GetY(), robot.GetX()] == 36)
            {
                exited = true;
                return endGame(hitEnamy, hitSpyke, hitSaw, exited, map, robot, outOfBattery);
            }
            else if (map[robot.GetY(), robot.GetX()] == 35)
            {
                map[robot.GetY(), robot.GetX()] = 1;
                robot.GotGateKey();
                robot.addBattery();

            }
            else if (map[robot.GetY(), robot.GetX()] == 34)
            {
                map[robot.GetY(), robot.GetX()] = 1;
                robot.IncressRoomKeyNumber();
                robot.addBattery();
            }
            else if (map[robot.GetY(), robot.GetX()] >= 20 && map[robot.GetY(), robot.GetX()] <= 29)
                return endGame(hitEnamy, hitSpyke, hitSaw, exited, map, robot, outOfBattery);

            if (robot.HasGateKey() && !enamy.GetStatus())
            {
                enamy.IncresReleasCounter();
                if (enamy.GetReleasCounter() > 10)
                    enamy.SetStatus(true);
            }

            if (robot.GetRoomKeyNumber() >= 2)
            {
                map[56, 50] = 1;
                map[56, 49] = 30;
                map[56, 51] = 31;
            }

            if (robot.HasGateKey() && Math.Sqrt(Math.Pow(44 - robot.GetY(), 2) + Math.Pow(50 - robot.GetX(), 2)) <= 3)
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

                if (map[robot.GetY(), robot.GetX()] == 20)
                    Console.WriteLine("Fenll into spykes");
                else if (map[robot.GetY(), robot.GetX()] == 21)
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
