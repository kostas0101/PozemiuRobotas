using static PozemiuRobotas2.Obstacles;
using static PozemiuRobotas2.Player;

namespace PozemiuRobotas2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int[,] map = new int[startMapX, startMapY];


                Robot robot = new Robot(startBotX, startBotY, startBatteryLevel);
                List<Saw> saws = new List<Saw>();
                List<Spyke> spykes = new List<Spyke>();
                Enamy enamy = ObstacleFactory.CreateEnamy(0, 0, false);

                //test

                Console.Clear();
                Console.WriteLine("=====================");
                Console.WriteLine("      Start run");
                Console.WriteLine("=====================");
                Console.ReadKey();

                Map.GenerateMap(map, robot.GetX(), robot.GetY(), saws, spykes, ref enamy);

                List<Obstacle> obstacles = new List<Obstacle>();
                obstacles.AddRange(saws);
                obstacles.AddRange(spykes);
                obstacles.Add(enamy);

                while (true)
                {
                    Console.Clear();
                    Map.DrawMap(map, robot.GetX(), robot.GetY(), saws, spykes, enamy);
                    robot.DrawBattery();

                    GameLogic.GetInput(robot, map);

                    if (GameLogic.GameProcess(robot, map, obstacles) == true)
                        break;

                }
            }


        }
        private const int startMapX = 100;
        private const int startMapY = 100;
        private const int startBotX = 50;
        private const int startBotY = 50;
        private const int startBatteryLevel = 100;
    }
}
