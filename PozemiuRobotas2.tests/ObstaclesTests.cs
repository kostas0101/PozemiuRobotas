using NUnit.Framework;
using PozemiuRobotas2;
using static PozemiuRobotas2.Obstacles;
using static PozemiuRobotas2.Player;

namespace PozemiuRobotas2.tests
{
    [TestFixture]
    public class ObstaclesTests
    {
        [Test]
        public void Saw_MovesDownThenUp()
        {
            int[,] map = new int[10, 10];
            var saw = new Saw(5, 5, movingDown: true);
            saw.Update(map, robotX: 0, robotY: 0);
            Assert.That(saw.Y, Is.EqualTo(4));

            saw.Update(map, robotX: 0, robotY: 0);
            Assert.That(saw.Y, Is.EqualTo(5));
        }

        [Test]
        public void Spyke_TogglesStatus_OnUpdate()
        {
            var spyke = new Spyke(2, 2, extended: false);
            bool before = spyke.Status;
            spyke.Update(new int[3,3], 0, 0);
            Assert.That(spyke.Status, Is.EqualTo(!before));
        }

        [Test]
        public void Enamy_Inactive_DoesNotMove()
        {
            var enamy = new Enamy(4, 4, active: false);
            enamy.Update(new int[10,10], robotX: 0, robotY: 0);
            Assert.That(enamy.X, Is.EqualTo(4));
            Assert.That(enamy.Y, Is.EqualTo(4));
        }

        [Test]
        public void Enamy_Active_Moves_TowardsRobot()
        {
            var enamy = new Enamy(4, 4, active: true);
            int[,] map = new int[10,10];
            enamy.Update(map, robotX: 2, robotY: 2);
            Assert.That(enamy.X, Is.LessThan(4));
            Assert.That(enamy.Y, Is.LessThan(4));
        }
    }
}
