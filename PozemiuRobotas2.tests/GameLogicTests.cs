using NUnit.Framework;
using System.Collections.Generic;
using PozemiuRobotas2;
using static PozemiuRobotas2.Obstacles;
using static PozemiuRobotas2.Player;

namespace PozemiuRobotas2.tests
{
    [TestFixture]
    public class GameLogicTests
    {
        [Test]
        public void GameProcess_IncrementsMoveCounter_AndDoesNotEndGame()
        {
            var robot = new Robot(0, 0, battery: 100);
            int[,] map = new int[60, 60];
            var obstacles = new List<Obstacle>();

            bool ended = GameLogic.GameProcess(robot, map, obstacles);

            Assert.That(ended, Is.False);
            Assert.That(robot.GetMoveCounter(), Is.EqualTo(1));
        }

        [Test]
        public void GameProcess_TogglesSpyke_AfterFourMoves()
        {
            var robot = new Robot(0, 0, battery: 100);
            int[,] map = new int[60, 60];
            var spyke = new Spyke(2, 2, extended: false);
            var obstacles = new List<Obstacle> { spyke };

            for (int i = 0; i < 4; i++)
            {
                bool ended = GameLogic.GameProcess(robot, map, obstacles);
                Assert.That(ended, Is.False);
            }

            Assert.That(spyke.Status, Is.True);
        }

        [Test]
        public void GameProcess_ReleasesEnamy_WhenRobotHasGateKey()
        {
            var robot = new Robot(0, 0, battery: 100);
            int[,] map = new int[60, 60];
            var enamy = new Enamy(10, 10, active: false);
            var obstacles = new List<Obstacle> { enamy };

            robot.GotGateKey();

            for (int i = 0; i < 12; i++)
            {
                bool ended = GameLogic.GameProcess(robot, map, obstacles);
                Assert.That(ended, Is.False);
            }

            Assert.That(enamy.Status, Is.True);
        }
        //testa
    }
}
