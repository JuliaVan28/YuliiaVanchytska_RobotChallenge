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
            var myRobot = robots[robotToMoveIndex];
            Position stationPosition = null;
            return new MoveCommand() { NewPosition = stationPosition };
        }
    }
}
