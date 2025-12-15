namespace PozemiuRobotas2
{
    public static class ObstacleFactory
    {
        public static Obstacles.Saw CreateSaw(int x, int y, bool movingDown)
        {
            return new Obstacles.Saw(x, y, movingDown);
        }

        public static Obstacles.Spyke CreateSpyke(int x, int y, bool extended)
        {
            return new Obstacles.Spyke(x, y, extended);
        }

        public static Obstacles.Enamy CreateEnamy(int x, int y, bool active)
        {
            return new Obstacles.Enamy(x, y, active);
        }
    }
}
