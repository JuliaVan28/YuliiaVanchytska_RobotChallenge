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
        public YuliiaVanchytskaAlgorithm()
        {
            Robot.Common.Logger.OnLogRound += Logger;
        }
        private void Logger(object sender, LogRoundEventArgs e)
        {
            RoundCounter++;
        }
        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map)
        {
            try
            {
                var currentRobot = robots[robotToMoveIndex] ?? throw new ArgumentOutOfRangeException();
                if (RoundCounter == 51 || currentRobot.Energy == 0)
                    return new CollectEnergyCommand();

                int myRobotsCount = ((IEnumerable<Robot.Common.Robot>)robots).Where(robot => robot.OwnerName == Author).Count();

                if (currentRobot.Energy >= 300 && myRobotsCount < 100 && myRobotsCount < map.Stations.Count)
                {
                    return new CreateNewRobotCommand();
                }

                Position stationPosition = Finder.FindNearestFreeStation(robots[robotToMoveIndex], map, robots);
                if (stationPosition == null)
                    return null;

                if (stationPosition == currentRobot.Position)
                    return new CollectEnergyCommand();

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
