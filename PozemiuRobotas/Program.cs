using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PozemiuRobotas.Obstacles;
using static PozemiuRobotas.Player;

namespace PozemiuRobotas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int runNo = 0;
            while (true)
            {
                runNo++;
                int StartMapx = 100, StartMapy = 100, StartBotx = 50, StartBoty = 50;
                int[,] map = new int[StartMapx, StartMapy];

                Robot robot = new Robot(StartBotx, StartBoty);
                List<Saw> saws = new List<Saw>();
                List<Spyke> spykes = new List<Spyke>();
                Enamy enamy = new Enamy(0, 0, false);

                Console.Clear();
                Console.WriteLine("Start run: " + runNo);
                Console.ReadKey();

                Map.generateMap(map, robot.botx, robot.boty, saws, spykes, enamy);

                while (true)
                {
                    Console.Clear();

                    Map.drawMap(map, robot.botx, robot.boty, saws, spykes, enamy);

                    GameLogic.getInput(robot, map);

                    if (GameLogic.gameProcess(robot, map, spykes, saws, enamy) == true)
                        break;

                }
            }
        }
    }
}
