using static PozemiuRobotas2.Obstacles;

namespace PozemiuRobotas2
{
    public static class ObstacleFactory
    {
        public interface IObstacleFactory
        {
            Obstacles.Obstacle Create(int x, int y, bool state);
        }

        public interface IEnamyFactory
        {
            Enamy Create(int x, int y, bool active);
        }

        public class SawFactory : IObstacleFactory
        {
            public Obstacles.Obstacle Create(int x, int y, bool movingDown)
            {
                return new Obstacles.Saw(x, y, movingDown);
            }
        }

        public class SpykeFactory : IObstacleFactory
        {
            public Obstacles.Obstacle Create(int x, int y, bool extended)
            {
                return new Obstacles.Spyke(x, y, extended);
            }
        }

        public class EnamyFactory : IEnamyFactory
        {
            public Enamy Create(int x, int y, bool active)
            {
                return new Obstacles.Enamy(x, y, active);
            }
        }
    }
}
