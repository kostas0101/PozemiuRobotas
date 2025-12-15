using NUnit.Framework;
using System.Collections.Generic;
using PozemiuRobotas2;
using static PozemiuRobotas2.Obstacles;

namespace PozemiuRobotas2.tests
{
    [TestFixture]
    public class MapTests
    {
        [Test]
        public void GenerateMap_CreatesStartingAreaFloor()
        {
            int size = 100;
            int[,] map = new int[size, size];
            int botx = 50;
            int boty = 50;
            var saws = new List<Saw>();
            var spykes = new List<Spyke>();
            Enamy enamy = null;

            Map.GenerateMap(map, botx, boty, saws, spykes, ref enamy);

            for (int i = botx - 5; i <= botx + 5; i++)
            {
                for (int j = boty - 5; j <= boty + 5; j++)
                {
                    Assert.That(map[i, j], Is.EqualTo(MapConstants.floor), $"Expected floor at [{i},{j}]");
                }
            }
        }

        [Test]
        public void GenerateMap_PlacesExitAndGateKey()
        {
            int size = 100;
            int[,] map = new int[size, size];
            int botx = 50;
            int boty = 50;
            var saws = new List<Saw>();
            var spykes = new List<Spyke>();
            Enamy enamy = null;

            Map.GenerateMap(map, botx, boty, saws, spykes, ref enamy);

            bool hasExit = false;
            bool hasGateKey = false;

            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == MapConstants.exit) hasExit = true;
                    if (map[i, j] == MapConstants.gateKey) hasGateKey = true;
                }

            Assert.That(hasExit, Is.True, "Map should contain an exit cell");
            Assert.That(hasGateKey, Is.True, "Map should contain a gate key cell");
        }

        [Test]
        public void DrawMap_DoesNotThrow()
        {
            int size = 100;
            int[,] map = new int[size, size];
            int botx = 50;
            int boty = 50;
            var saws = new List<Saw>();
            var spykes = new List<Spyke>();
            Enamy enamy = ObstacleFactory.CreateEnamy(botx, boty, false);

            Map.GenerateMap(map, botx, boty, saws, spykes, ref enamy);

            Assert.DoesNotThrow(() => Map.DrawMap(map, botx, boty, saws, spykes, enamy));
        }
    }
}
