using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robot.Common;


namespace YuliiaVanchytska.RobotChallenge
{
    public class Finder
    {
        public static EnergyStation FindNearestFreeStation(Robot.Common.Robot currentRobot, Map map,
IList<Robot.Common.Robot> robots)
        {
            EnergyStation nearest = null;
            int minDistance = int.MaxValue;
            foreach (var station in map.Stations)
            {
                if (Checker.isStationFree(station.Position, currentRobot, robots))
                {
                    int d = DistanceHelper.FindDistance(station.Position,currentRobot.Position);
                    if (d < minDistance)
                    {
                        minDistance = d;
                        nearest = station;
                    }
                }
            }
            return nearest == null ? null : nearest;
        }

        public static Position FindCurrentBestDistance(Robot.Common.Robot currentRobot, IList<Robot.Common.Robot> robots, Position nearestStationPos)
        {
            int distance = DistanceHelper.FindDistanceAbs(currentRobot.Position, nearestStationPos);
            Position possibleDistance = nearestStationPos;
            bool isAbletoMove = Checker.IsAbleToMove(currentRobot, nearestStationPos, 10);
            if (distance < 10 && isAbletoMove)
            {
                return nearestStationPos;
            }
            for (int i = 2; i < 4; i++)
            {
                possibleDistance = DistanceHelper.FindPossibleBestDistance(currentRobot.Position, possibleDistance, 2);
                bool isPosFree = Checker.IsCellFreeFromOthers(possibleDistance, currentRobot, robots);
                isAbletoMove = Checker.IsAbleToMove(currentRobot, possibleDistance, 10);

                if (isPosFree && isAbletoMove) return possibleDistance;
            }
            return null;
        }
        public static List<Robot.Common.Robot> FindEnemies(IList<Robot.Common.Robot> robots, string
ownerName)
        {
            List<Robot.Common.Robot> enemies = new List<Robot.Common.Robot>();
            foreach ( var robot in robots)
            {
                if (robot.OwnerName != ownerName)
                    enemies.Add(robot);
            }
            return enemies;
        }
        public static Robot.Common.Robot FindBestEnemyToAttack(List<Robot.Common.Robot> enemies,
 Robot.Common.Robot currentRobot)
        {
            enemies.Sort(new Checker.MyComparer() { currentRobot = currentRobot });
            enemies.Reverse();
            for (int i = 0; i < enemies.Count; i++)
            {   
                int neededEnergy = DistanceHelper.FindDistance(currentRobot.Position, enemies[i].Position);
                if (((currentRobot.Energy - neededEnergy) >= 20))
                    return enemies[i];
            }
            return null;
        }




    }
}
