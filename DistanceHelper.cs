using System;
using Robot.Common;


namespace YuliiaVanchytska.RobotChallenge
{
    class DistanceHelper
    {
        
        public static int FindDistance(Position a, Position b)
        {
            return (int)(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
        public static int FindDistanceAbs(Position a, Position b)
        {
            return (Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y));
        }

        public static Position FindPossibleBestDistance(Position start, Position end, int divider)
        {
            if ((start.X < end.X) && (start.Y < end.Y))
                return new Position(start.X + (end.X - start.X) / divider, start.Y + (end.Y - start.Y) / divider);
            else if ((start.X > end.X) && (start.Y > end.Y))
                return new Position(start.X - (start.X - end.X) / divider, start.Y - (start.Y - end.Y) / divider);
            else if ((start.X < end.X) && (start.Y > end.Y))
                return new Position(start.X + (end.X - start.X) / divider, start.Y - (start.Y - end.Y) / divider);
            else if ((start.X > end.X) && (start.Y < end.Y))
                return new Position(start.X - (start.X - end.X) / divider, start.Y + (end.Y - start.Y) / divider);
            else if (start.X == end.X)
            {
                if (start.Y > end.Y)
                    return new Position(start.X, start.Y - (start.Y - end.Y) / divider);
                else if (start.Y < end.Y)
                    return new Position(start.X, start.Y + (end.Y - start.Y) / divider);
            }
            else if (start.Y == end.Y)
            {
                if (start.X > end.X)
                    return new Position(start.X - (start.X - end.X) / divider, start.Y);
                else if (start.X < end.X)
                    return new Position(start.X + (end.X - start.X) / divider, start.Y);
            }
            return new Position(start.X, end.Y);

        }
    }
}
