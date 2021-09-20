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

        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map)
        {
            try
            {
                var currentRobot = robots[robotToMoveIndex] ?? throw new ArgumentOutOfRangeException();
                if (currentRobot.Energy == 0)
                    return new CollectEnergyCommand();

                if (currentRobot.Energy >= 500)
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
