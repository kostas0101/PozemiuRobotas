namespace PozemiuRobotas2.tests
{
    [TestFixture]
    public class RobotTests
    {
        [Test]
        public void MoveUp_DecreasesY()
        {
            var robot = new Player.Robot(2, 2, 100);
            robot.MoveUp();
            Assert.AreEqual(1, robot.GetY());
        }

        [Test]
        public void MoveDown_IncreasesY()
        {
            var robot = new Player.Robot(2, 2, 100);
            robot.MoveDown();
            Assert.AreEqual(3, robot.GetY());
        }

        [Test]
        public void MoveLeft_DecreasesX()
        {
            var robot = new Player.Robot(2, 2, 100);
            robot.MoveLeft();
            Assert.AreEqual(1, robot.GetX());
        }

        [Test]
        public void MoveRight_IncreasesX()
        {
            var robot = new Player.Robot(2, 2, 100);
            robot.MoveRight();
            Assert.AreEqual(3, robot.GetX());
        }

        [Test]
        public void AddBattery_DoesNotExceedMax()
        {
            var robot = new Player.Robot(2, 2, 150);
            robot.AddBattery();
            Assert.AreEqual(200, robot.GetBatteryLevel());
        }

        [Test]
        public void DecresBatteryLevel_DecreasesBattery()
        {
            var robot = new Player.Robot(2, 2, 100);
            robot.DecresBatteryLevel();
            Assert.AreEqual(99, robot.GetBatteryLevel());
        }

        [Test]
        public void GotGateKey_SetsGateKeyTrue()
        {
            var robot = new Player.Robot(2, 2, 100);
            Assert.IsFalse(robot.HasGateKey());
            robot.GotGateKey();
            Assert.IsTrue(robot.HasGateKey());
        }

        [Test]
        public void IncressRoomKeyNumber_IncreasesRoomKey()
        {
            var robot = new Player.Robot(2, 2, 100);
            int before = robot.GetRoomKeyNumber();
            robot.IncressRoomKeyNumber();
            Assert.AreEqual(before + 1, robot.GetRoomKeyNumber());
        }

        [Test]
        public void IncressMoveCounter_IncreasesMoveCounter()
        {
            var robot = new Player.Robot(2, 2, 100);
            robot.IncressMoveCounter();
            Assert.AreEqual(1, robot.GetMoveCounter());
        }

        [Test]
        public void ResetMoveCounter_SetsMoveCounterToZero()
        {
            var robot = new Player.Robot(2, 2, 100);
            robot.IncressMoveCounter();
            robot.ResetMoveCounter();
            Assert.AreEqual(0, robot.GetMoveCounter());
        }
    }
}