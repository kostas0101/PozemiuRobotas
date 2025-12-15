using static PozemiuRobotas2.ObstacleFactory;
using static PozemiuRobotas2.Obstacles;

namespace PozemiuRobotas2
{
    public static class Map
    {
        private static Random random = new Random();
        private static IObstacleFactory sawFactory = new SawFactory();
        private static IObstacleFactory spykeFactory = new SpykeFactory();
        private static IEnamyFactory enamyFactory = new EnamyFactory();

        public static void GenerateMap(int[,] map, int botx, int boty, List<Saw> saws, List<Spyke> spykes, ref Enamy enamy)
        {

            FillMap(map, MapConstants.empty);
            CreateStartingArea(map, botx, boty);
            CreateExitHallway(map, botx, boty, saws, ref enamy);
            CreateKeyHallway(map, botx, boty, saws, ref enamy);
            CreateSideHallways(map, botx, boty, saws, spykes);
        }

        private static void FillMap(int[,] map, int value)
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    map[i, j] = value;
        }

        private static void CreateStartingArea(int[,] map, int botx, int boty)
        {
            for (int i = botx - 5; i <= botx + 5; i++)
                for (int j = boty - 5; j <= boty + 5; j++)
                    map[i, j] = MapConstants.floor;
        }


        private static void CreateExitHallway(int[,] map, int botx, int boty, List<Saw> saws, ref Enamy enamy)
        {
            int hallwayLength = 10 + random.Next(0, 5);

            for (int i = boty; i > boty - hallwayLength; i--)
            {
                map[i, boty] = MapConstants.floor;
                map[i, boty + 1] = MapConstants.floor;
                map[i, boty - 1] = MapConstants.floor;
            }

            map[boty - hallwayLength, boty] = MapConstants.exit;
            map[boty - hallwayLength, boty + 1] = MapConstants.exit;
            map[boty - hallwayLength, boty - 1] = MapConstants.exit;

            map[boty - 6, boty] = MapConstants.exitGate;
            map[boty - 6, boty - 1] = MapConstants.exitGate;
            map[boty - 6, boty + 1] = MapConstants.exitGate;
        }

        private static void CreateKeyHallway(int[,] map, int botx, int boty, List<Saw> saws, ref Enamy enamy)
        {
            int hallwayLength = 10 + random.Next(0, 5);
            int x = 0, y = 0;
            bool found = false;

            hallwayLength = 15 + random.Next(0, 10);
            for (int i = boty; i < boty + hallwayLength; i++)
            {
                map[i, boty] = MapConstants.floor;
                map[i, boty + 1] = MapConstants.floor;
                map[i, boty - 1] = MapConstants.floor;
            }

            for (int traps = 0; traps < 10; traps++)
            {
                x = random.Next(-1, 2) + botx;
                y = random.Next(boty + 7, boty + hallwayLength);

                found = false;
                for (int i = y - 1; i <= y + 1; i++)
                    for (int j = x - 1; j <= x + 1; j++)
                        if (map[i, j] == GameConstants.spike)
                            found = true;
                if (!found)
                    map[y, x] = GameConstants.spike;
            }

            map[boty + 6, boty] = MapConstants.keyRoomGate;
            map[boty + 6, boty - 1] = MapConstants.keyRoomGate;
            map[boty + 6, boty + 1] = MapConstants.keyRoomGate;

            int tempy = random.Next(boty + hallwayLength + 1, boty + hallwayLength + 10) + 16;
            for (int i = botx + hallwayLength; i < tempy; i++)
                for (int j = boty - 5; j <= boty + 5; j++)
                    map[i, j] = MapConstants.floor;

            map[tempy - 3, boty] = MapConstants.gateKey;

            for (int i = botx + hallwayLength + 1; i < tempy - 5; i++)
            {
                if (i % 2 == 0)
                {
                    y = (random.Next(-5, 6) + boty);
                    if (random.Next(0, 2) == 0)
                        for (int j = random.Next(3, 10); j > 0; j--)
                        {
                            if (map[i, y] != MapConstants.floor)
                                break;
                            else
                            {
                                map[i, y] = GameConstants.spike;
                                y++;
                            }
                        }
                    else
                        for (int j = random.Next(3, 9); j > 0; j--)
                        {
                            if (map[i, y] != MapConstants.floor)
                                break;
                            else
                            {
                                map[i, y] = GameConstants.spike;
                                y--;
                            }
                        }
                }
            }

            enamy = enamyFactory.Create(boty, tempy - 1, false);
            EnamyUpdates e = new EnamyUpdates();
            enamy.StatusChange += e.StatusChange;
        }




        private static void CreateSideHallways(int[,] map, int botx, int boty, List<Saw> saws, List<Spyke> spykes)
        {
            int hallwayLength = 10 + random.Next(0, 5);
            int x = 0, y = 0;
            bool found = false;

            //LEFT
            hallwayLength = 20 + random.Next(0, 10);
            for (int i = botx; i > botx - hallwayLength; i--)
            {
                map[botx, i] = MapConstants.floor;
                map[botx + 1, i] = MapConstants.floor;
                map[botx - 1, i] = MapConstants.floor;
            }

            for (int traps = 0; traps < 20; traps++)
            {
                y = random.Next(-1, 2) + boty;
                x = random.Next(botx - hallwayLength + 1, botx - 7);

                found = false;
                for (int i = y - 1; i <= y + 1; i++)
                    for (int j = x - 1; j <= x + 1; j++)
                        if (map[i, j] == MapConstants.pit)
                            found = true;
                if (!found)
                    map[y, x] = MapConstants.pit;
            }

            x = random.Next(botx - hallwayLength - 10, botx - hallwayLength) - 10;
            for (int i = botx - hallwayLength; i > x; i--)
                for (int j = boty - 3; j <= boty + 3; j++)
                    map[j, i] = MapConstants.floor;

            for (int i = botx - hallwayLength - 1; i > x + 2; i--)
                if (random.Next(0, 2) == 0)
                    saws.Add((Saw)sawFactory.Create(i, (boty + random.Next(-3, 4)), random.Next(0, 2) != 0));
            map[boty, x + 1] = MapConstants.roomKey;


            //RIGHT
            hallwayLength = 20 + random.Next(0, 10);
            for (int i = botx; i < botx + hallwayLength; i++)
            {
                map[botx, i] = MapConstants.floor;
                map[botx + 1, i] = MapConstants.floor;
                map[botx - 1, i] = MapConstants.floor;
            }

            for (int traps = 0; traps < 10; traps++)
            {
                y = random.Next(-1, 2) + boty;
                x = random.Next(botx + 7, botx + hallwayLength - 1);

                found = false;
                for (int i = y - 1; i <= y + 1; i++)
                    for (int j = x - 1; j <= x + 1; j++)
                        if (map[i, j] == GameConstants.spike)
                            found = true;
                if (!found)
                    map[y, x] = GameConstants.spike;
            }

            int tempx = random.Next(botx + hallwayLength + 1, botx + hallwayLength + 10) + 10;
            for (int i = botx + hallwayLength; i < tempx; i++)
                for (int j = boty - 5; j <= boty + 5; j++)
                    map[j, i] = MapConstants.floor;

            map[(random.Next(-5, 6) + boty), (random.Next(botx + hallwayLength + 8, tempx))] = MapConstants.roomKey;

            for (int i = 0; i < 80; i++)
            {
                x = (random.Next(-5, 6) + boty);
                y = random.Next(botx + hallwayLength, tempx);
                if (map[x, y] == MapConstants.floor)
                    spykes.Add((Spyke)spykeFactory.Create(y, x, true));
            }
        }

        public static void DrawMap(int[,] map, int botx, int boty, List<Saw> saws, List<Spyke> spykes, Enamy enamy)
        {
            DrawBorders();

            int startY = Math.Max(0, boty - MapConstants.maxMapDrawSize);
            int endY = Math.Min(map.GetLength(0), boty + MapConstants.maxMapDrawSize);
            int startX = Math.Max(0, botx - MapConstants.maxMapDrawSize);
            int endX = Math.Min(map.GetLength(1), botx + MapConstants.maxMapDrawSize);

            for (int y = startY; y < endY; y++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("██");
                Console.ForegroundColor = ConsoleColor.White;

                for (int x = startX; x < endX; x++)
                {
                    double distance = Distance(x, y, botx, boty);

                    if (distance >= MapConstants.viewDistance)
                    {

                        Console.Write("██");
                        continue;
                    }

                    if (distance >= MapConstants.viewLimitation && map[y, x] != MapConstants.empty)
                    {
                        Console.Write("▓▓");
                        continue;
                    }

                    bool isSaw = saws.Exists(s => s.X == x && s.Y == y);
                    bool isSpyke = spykes.Exists(s => s.X == x && s.Y == y && s.Status);
                    bool isRetractedSpyke = spykes.Exists(s => s.X == x && s.Y == y && !s.Status);
                    bool isEnemy = (enamy.X == x && enamy.Y == y);

                    Console.Write(DrawCell(map[y, x], x == botx && y == boty, isEnemy, isSaw, isSpyke, isRetractedSpyke));
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("██");
                Console.ForegroundColor = ConsoleColor.White;
            }

            DrawBorders();
        }

        private static void DrawBorders()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            for (int i = 0; i < 22; i++)
                Console.Write("██");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static string DrawCell(int cell, bool isRobot, bool isEnemy, bool isSaw, bool isSpyke, bool isRetractedSpyke)
        {

            Console.ForegroundColor = ConsoleColor.Red;
            if (isEnemy) return "<>";
            if (isSaw) return "{}";
            if (isSpyke) return @"/\";
            if (cell == MapConstants.spike) return "^^";
            if (cell == MapConstants.pit) return "[]";

            Console.ForegroundColor = ConsoleColor.Yellow;
            if (cell == MapConstants.roomKey) return "++";
            if (cell == MapConstants.gateKey) return "@~";

            Console.ForegroundColor = ConsoleColor.White;
            if (isRobot) return "()";
            if (isRetractedSpyke) return "~~";
            if (cell == MapConstants.floor) return "  ";
            if (cell == MapConstants.exitGate) return "__";
            if (cell == MapConstants.keyRoomGate) return "--";
            if (cell == MapConstants.exit) return "_/";

            if (cell == MapConstants.exitGateLeft) return "_ ";
            if (cell == MapConstants.exitGateRight) return " _";
            if (cell == MapConstants.keyRoomGateLeft) return "- ";
            if (cell == MapConstants.keyRoomGateRight) return " -";


            return "██";
        }

        private static double Distance(int x, int y, int botx, int boty)
        {
            int dx = x - botx;
            int dy = y - boty;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }

    public static class MapConstants
    {
        public const int empty = 0;
        public const int floor = 1;
        public const int exit = 36;
        public const int roomKey = 34;
        public const int gateKey = 35;
        public const int spike = 20;
        public const int pit = 21;
        public const int exitGate = 11;
        public const int exitGateLeft = 32;
        public const int exitGateRight = 33;
        public const int keyRoomGate = 12;
        public const int keyRoomGateLeft = 30;
        public const int keyRoomGateRight = 31;

        public const int viewDistance = 8;
        public const int viewLimitation = 5;
        public const int maxMapDrawSize = 10;
    }
}
