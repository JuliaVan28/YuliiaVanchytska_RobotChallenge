using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robot.Common;


namespace YuliiaVanchytska.RobotChallenge
{
    public class Checker
    {
        /*public static bool IsCellFreeFromOthers(Position position, Robot.Common.Robot currentRobot, IList<Robot.Common.Robot> robots)
        {
            foreach (var robot in robots)
            {
                if (robot != currentRobot)
                {
                    if (robot.Position == position)
                        return false;
                }
            }
            return true;
        }*/
        public static bool isStationFree(Position stationPosition, Robot.Common.Robot currentRobot, IList<Robot.Common.Robot> robots)
        {
            IList<Robot.Common.Robot> others = new List<Robot.Common.Robot>();
            foreach (var robot in robots)
            {
                if (DistanceHelper.FindDistance(stationPosition, robot.Position) <= 2)
                {
                    if (robot != currentRobot)
                        others.Add(robot);
                }
            }
            if (others.Count != 0)
                return false;
            return true;

        }

        public static bool isAbleToAtack(Robot.Common.Robot enemy, Robot.Common.Robot currentRobot, int energy)
        {
            int distance = DistanceHelper.FindDistance(enemy.Position, currentRobot.Position);
            if ((enemy.Energy * 0.1 - distance - 30 <= energy) || (enemy.OwnerName == currentRobot.OwnerName))
                return false;
            return enemy == null ? false : true;
        }
        public class MyComparer : IComparer<Robot.Common.Robot>
        {
            public Robot.Common.Robot currentRobot { get; set; }
            public int Compare(Robot.Common.Robot x, Robot.Common.Robot y)
            {
                int energy1 = (int)(x.Energy * 0.1) - (DistanceHelper.FindDistance(x.Position,currentRobot.Position) + 30);
                int energy2 = (int)(y.Energy * 0.1) - (DistanceHelper.FindDistance(y.Position,currentRobot.Position) + 30);
                if (energy1 > energy2)
                    return 1;
                else if (energy1 < energy2)
                    return -1;
                else
                    return 0;
            }
        }

        public static bool IsAbleToMove(Robot.Common.Robot currentRobot, Position positionTo, int additionalEnergy)
        {
            int neededEnergy = DistanceHelper.FindDistance(currentRobot.Position, positionTo);
            if ((currentRobot.Energy - neededEnergy) >= 10)
                return true;
            return false;
        }


    }
}
