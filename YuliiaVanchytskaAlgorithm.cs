using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robot.Common;

namespace YuliiaVanchytska.RobotChallenge
{
    public class YuliiaVanchytskaAlgorithm : IRobotAlgorithm
    {
        public string Author
        {
            get { return "Yuliia Vanchytska"; }
        }
        public int RoundCounter { get; set; }
        const int AttackingRound = 30;
        public YuliiaVanchytskaAlgorithm()
        {
            Robot.Common.Logger.OnLogRound += Logger;
        }
        private void Logger(object sender, LogRoundEventArgs e)
        {
            RoundCounter++;
        }
        delegate bool delegateTest(Robot.Common.Robot enemy, Robot.Common.Robot currentRobot, int energy);
        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map)
        {
            try
            {
                delegateTest deleg;
                var currentRobot = robots[robotToMoveIndex] ?? throw new ArgumentOutOfRangeException();
                if (RoundCounter == 51 || currentRobot.Energy == 0)
                    return new CollectEnergyCommand();

                int myRobotsCount = ((IEnumerable<Robot.Common.Robot>)robots).Where(robot => robot.OwnerName == Author).Count();

                if (currentRobot.Energy >= 300 && myRobotsCount < 100 && myRobotsCount < map.Stations.Count)
                {
                    return new CreateNewRobotCommand();
                }

                
                EnergyStation nearestStation = Finder.FindNearestFreeStation(robots[robotToMoveIndex], map, robots);
                Position stationPosition = nearestStation.Position;
                Position bestDistance = Finder.FindCurrentBestDistance(currentRobot, robots, stationPosition);

                List<Robot.Common.Robot> enemies = Finder.FindEnemies(robots, Author);
                Robot.Common.Robot bestEnemy = Finder.FindBestEnemyToAttack(enemies,currentRobot);
                deleg = Checker.isAbleToAtack;
                if (stationPosition == null)
                {
                    if (RoundCounter > AttackingRound )
                    {
                        if (bestEnemy != null)
                        {
                            if ((deleg(bestEnemy, currentRobot, 25)))
                            {
                                if (Checker.IsAbleToMove(currentRobot, bestEnemy.Position, 1))
                                    return new MoveCommand() { NewPosition = bestEnemy.Position };
                            }
                        }
                    }
                    return null;
                }
                   

                 if ((stationPosition == currentRobot.Position) && nearestStation.Energy > 0)
                    return new CollectEnergyCommand();

                if (bestDistance != null && !Checker.IsNearTheStation(currentRobot, map, robots))
                    return new MoveCommand() { NewPosition = bestDistance };

                return new MoveCommand() { NewPosition = stationPosition };
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
                throw;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
