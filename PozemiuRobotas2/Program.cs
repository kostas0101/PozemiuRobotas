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
                int[,] map = new int[mapSizeX, mapSizeY];


                Robot robot = new Robot(startingBotX, startingBotY, startingBatteryLevel);
                List<Saw> saws = new List<Saw>();
                List<Spyke> spykes = new List<Spyke>();
                Enamy enamy = null;


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
                    Console.CursorVisible = false;
                    Console.SetCursorPosition(0, 0);

                    Map.DrawMap(map, robot.GetX(), robot.GetY(), saws, spykes, enamy);
                    robot.DrawBattery();

                    GameLogic.GetInput(robot, map);

                    if (GameLogic.GameProcess(robot, map, obstacles) == true)
                        break;

                }
            }


        }
        private const int mapSizeX = 100;
        private const int mapSizeY = 100;
        private const int startingBotX = 50;
        private const int startingBotY = 50;
        private const int startingBatteryLevel = 100;
    }
}
