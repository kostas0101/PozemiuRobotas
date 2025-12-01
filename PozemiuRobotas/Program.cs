using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PozemiuRobotas.Obstacles;
using static PozemiuRobotas.GameLogic;
using static PozemiuRobotas.Player;

namespace PozemiuRobotas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int startMapX = 100, startMapY = 100, startBotX = 50, startBotY = 50, startBatteryLevel = 100;
                int[,] map = new int[startMapX, startMapY];

                Robot robot = new Robot(startBotX, startBotY, startBatteryLevel);
                List<Saw> saws = new List<Saw>();
                List<Spyke> spykes = new List<Spyke>();
                Enamy enamy = new Enamy(0, 0, false);

                Console.Clear();
                Console.WriteLine("=====================");
                Console.WriteLine("      Start run");
                Console.WriteLine("=====================");
                Console.ReadKey();

                Map.generateMap(map, robot.GetX(), robot.GetY(), saws, spykes, enamy);

                while (true)
                {
                    Console.Clear();
                    Map.drawMap(map, robot.GetX(), robot.GetY(), saws, spykes, enamy);
                    robot.drawBattery();

                    GameLogic.getInput(robot, map);

                    if (GameLogic.gameProcess(robot, map, spykes, saws, enamy) == true)
                        break;

                }
            }
        }
    }
}
