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
                int StartMapx = 100, StartMapy = 100, StartBotx = 50, StartBoty = 50, batteryLevel = 100;
                int[,] map = new int[StartMapx, StartMapy];

                Robot robot = new Robot(StartBotx, StartBoty, batteryLevel);
                List<Saw> saws = new List<Saw>();
                List<Spyke> spykes = new List<Spyke>();
                Enamy enamy = new Enamy(0, 0, false);

                Console.Clear();
                Console.WriteLine("=====================");
                Console.WriteLine("      Start run");
                Console.WriteLine("=====================");
                Console.ReadKey();

                Map.generateMap(map, robot.botx, robot.boty, saws, spykes, enamy);

                while (true)
                {
                    Console.Clear();
                    Map.drawMap(map, robot.botx, robot.boty, saws, spykes, enamy);
                    robot.drawBattery();

                    GameLogic.getInput(robot, map);

                    if (GameLogic.gameProcess(robot, map, spykes, saws, enamy) == true)
                        break;

                }
            }
        }
    }
}
