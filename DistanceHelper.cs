using System;
using Robot.Common;


namespace YuliiaVanchytska.RobotChallenge
{
    class DistanceHelper
    {
        public static int FindSpentEnergy(Position a, Position b)
        {
            return (int)(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
        public static int FindDistance(Position a, Position b)
        {
            return (int)(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
            //return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

    }
}
