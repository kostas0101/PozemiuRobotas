namespace PozemiuRobotas2
{

    public static class Obstacles
    {
        public abstract class Obstacle
        {
            private int _X;
            private int _Y;
            private bool _Status;

            public int X => _X;
            public int Y => _Y;
            public bool Status
            {
                get => _Status;
                protected set => _Status = value;
            }

            protected void SetPosition(int x, int y)
            {
                _X = x;
                _Y = y;
            }

            protected void MoveUp() => _Y--;
            protected void MoveDown() => _Y++;
            protected void MoveLeft() => _X--;
            protected void MoveRight() => _X++;

            public abstract void Update(int[,] map, int robotX, int robotY);
        }

        public class Saw : Obstacle
        {
            public Saw(int x, int y, bool movingDown)
            {
                SetPosition(x, y);
                Status = movingDown;
            }

            public override void Update(int[,] map, int robotX, int robotY)
            {
                if (Status && map[Y + 1, X] != 1)
                    Status = false;
                else if (!Status && map[Y - 1, X] != 1)
                    Status = true;

                if (Status)
                    MoveDown();
                else
                    MoveUp();
            }
        }

        public class Spyke : Obstacle
        {
            public Spyke(int x, int y, bool extended)
            {
                SetPosition(x, y);
                Status = extended;
            }

            public override void Update(int[,] map, int robotX, int robotY)
            {
                Status = !Status;
            }

        }

        public class Enamy : Obstacle
        {
            public delegate void StatusChangeDelegate();
            public event StatusChangeDelegate StatusChange;

            private int _ReleasCounter;

            public Enamy(int x, int y, bool active)
            {
                SetPosition(x, y);
                Status = active;
                _ReleasCounter = 0;
            }

            public void IncresReleasCounter() => _ReleasCounter++;
            public int GetReleasCounter() => _ReleasCounter;
            public void ChangeState()
            {
                Status = !Status;
                StatusChange?.Invoke();
            }

            public override void Update(int[,] map, int robotX, int robotY)
            {
                if (!Status)
                    return;

                if (robotX < X)
                    MoveLeft();
                if (robotX > X)
                    MoveRight();
                if (robotY < Y)
                    MoveUp();
                if (robotY > Y)
                    MoveDown();
            }

        }

    }
    public class EnamyUpdates
    {
        public void StatusChange()
        {
            Console.Write("\nThe Enamy Has Awaken!");
            ConsoleKeyInfo input = Console.ReadKey();
        }
    }
}
