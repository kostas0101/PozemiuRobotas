using static PozemiuRobotas2.Obstacles;
using static PozemiuRobotas2.Player;

namespace PozemiuRobotas2
{
    public static class GameLogic
    {

        public static void GetInput(Robot robot, int[,] map)
        {
            int x = robot.GetX();
            int y = robot.GetY();
            ConsoleKeyInfo input = Console.ReadKey();

            if (input.Key == ConsoleKey.UpArrow && CanMove(x, y - 1, map))
                robot.MoveUp();
            else if (input.Key == ConsoleKey.DownArrow && CanMove(x, y + 1, map))
                robot.MoveDown();
            else if (input.Key == ConsoleKey.LeftArrow && CanMove(x - 1, y, map))
                robot.MoveLeft();
            else if (input.Key == ConsoleKey.RightArrow && CanMove(x + 1, y, map))
                robot.MoveRight();

            robot.DecresBatteryLevel();
        }


        public static bool GameProcess(Robot robot, int[,] map, List<Obstacle> obstacles)
        {


            if (robot.GetBatteryLevel() <= 0)
                return EndGame(hitEnamy: false, hitSpyke: false, hitSaw: false, exited: false, map, robot, outOfBattery: true);

            robot.IncressMoveCounter();
            if (robot.GetMoveCounter() > 3)
            {
                robot.ResetMoveCounter();
                UpdateObstacles<Spyke>(obstacles, map, robot);
            }

            UpdateObstacles<Saw>(obstacles, map, robot);
            UpdateObstacles<Enamy>(obstacles, map, robot);

            if (Collisions(robot, obstacles, map, out bool hitEnamy, out bool hitSpyke, out bool hitSaw, out bool exited))
                return EndGame(hitEnamy, hitSpyke, hitSaw, exited, map, robot, outOfBattery: false);

            GateControls(robot, map);
            ReleaseEnemies(robot, obstacles);

            /*
            if (map[robot.GetY(), robot.GetX()] >= 20 && map[robot.GetY(), robot.GetX()] <= 29)
                return EndGame(hitEnamy, hitSpyke, hitSaw, exited, map, robot, outOfBattery);
            */

            return false;
        }

        private static void GateControls(Robot robot, int[,] map)
        {
            int cell = map[robot.GetY(), robot.GetX()];

            switch (cell)
            {
                case GameConstants.exit:
                    break;
                case GameConstants.gateKey:
                    map[robot.GetY(), robot.GetX()] = 1;
                    robot.GotGateKey();
                    robot.AddBattery();
                    break;
                case GameConstants.roomKey:
                    map[robot.GetY(), robot.GetX()] = 1;
                    robot.IncressRoomKeyNumber();
                    robot.AddBattery();
                    break;
            }

            if (robot.GetRoomKeyNumber() >= 2)
            {
                map[56, 50] = 1;
                map[56, 49] = 30;
                map[56, 51] = 31;
            }

            if (robot.HasGateKey() && Distance(robot.GetX(), robot.GetY(), 50, 44) <= 3)
            {
                map[44, 50] = 1;
                map[44, 49] = 32;
                map[44, 51] = 33;
            }
        }

        private static void ReleaseEnemies(Robot robot, List<Obstacle> obstacles)
        {
            foreach (Enamy enamy in obstacles.OfType<Enamy>())
            {
                if (robot.HasGateKey() && !enamy.Status)
                {
                    enamy.IncresReleasCounter();
                    if (enamy.GetReleasCounter() > 10)
                        enamy.ChangeState();
                }
            }
        }

        public static bool EndGame(bool hitEnamy, bool hitSpyke, bool hitSaw, bool exited, int[,] map, Robot robot, bool outOfBattery)
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

                int cell = map[robot.GetY(), robot.GetX()];

                if (cell == GameConstants.spike)
                    Console.WriteLine("Fenll into spykes");
                else if (cell == GameConstants.pit)
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


        private static void UpdateObstacles<T>(List<Obstacle> obstacles, int[,] map, Robot robot) where T : Obstacle
        {
            foreach (var obs in obstacles.OfType<T>())
            {
                if (obs is Enamy enamy && !enamy.Status)
                    continue;
                obs.Update(map, robot.GetX(), robot.GetY());
            }
        }

        private static bool Collisions(Robot robot, List<Obstacle> obstacles, int[,] map,
            out bool hitEnamy, out bool hitSpyke, out bool hitSaw, out bool exited)
        {
            hitEnamy = SamePosition<Enamy>(robot, obstacles);
            hitSpyke = SamePosition<Spyke>(robot, obstacles, checkStatus: true);
            hitSaw = SamePosition<Saw>(robot, obstacles);
            exited = map[robot.GetY(), robot.GetX()] == GameConstants.exit;

            if (hitEnamy || hitSpyke || hitSaw || exited || (map[robot.GetY(), robot.GetX()] >= 20 && map[robot.GetY(), robot.GetX()] <= 29))
                return true;

            return false;
        }

        private static bool SamePosition<T>(Robot robot, List<Obstacle> obstacles, bool checkStatus = false) where T : Obstacle
        {
            return obstacles.OfType<T>().Any(o => o.X == robot.GetX() && o.Y == robot.GetY() && (!checkStatus || o.Status));
        }

        private static double Distance(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }
        private static bool CanMove(int x, int y, int[,] map)
        {
            return y >= 0 && y < map.GetLength(0) && x >= 0 && x < map.GetLength(1) &&
                   (map[y, x] == 1 || map[y, x] >= 20);
        }




    }
    public static class GameConstants
    {
        public const int exit = 36;
        public const int roomKey = 34;
        public const int gateKey = 35;
        public const int spike = 20;
        public const int pit = 21;

        public const int exitGateY = 44;
        public const int exitGateX = 50;
        public const int keyGateY = 56;
        public const int keyGateX = 50;
    }
}
