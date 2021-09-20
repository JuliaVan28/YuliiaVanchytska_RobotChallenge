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
      

        public static Position FindNearestFreeStation(Robot.Common.Robot currentRobot, Map map,
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


            // maybe very USEFUL
            /*if (nearest != null)
            {
                Position result = null;
                List<Position> positions = CalculationHelper.CalculateCellsAroundStation(nearest.Position,
               map);
                int minDistanceAroundStation = int.MaxValue;
                foreach (Position p in positions)
                {
                    int d = CalculationHelper.CalculateEnergyBetweenCells(p, currentRobot.Position);
                    if (d < minDistanceAroundStation)
                    {
                        minDistanceAroundStation = d;
                        result = p;
                    }
                }
                return result;
            }*/
            return nearest == null ? null : nearest.Position;
        }        public static List<Robot.Common.Robot> FindEnemies(IList<Robot.Common.Robot> robots, string
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
                if (((currentRobot.Energy - neededEnergy) >= 20/*energyToLeave*/))
                    return enemies[i];
            }
            return null;
        }



    }
}
