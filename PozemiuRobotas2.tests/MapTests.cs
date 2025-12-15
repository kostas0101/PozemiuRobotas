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
        public void GenerateMap_PlacesExit()
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

            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    if (map[i, j] == MapConstants.exit) hasExit = true;

            Assert.That(hasExit, Is.True, "Map should contain an exit cell");
        }

    }
}
