using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    /// <summary>
    /// A structure that represents moving a piece from (startCoord1, startCoord2) to (endCoord1, endCoord2).
    /// (Used only when performing simulations.)
    /// </summary>
    public struct SimMove
    {
        public readonly int startCoord1, startCoord2, endCoord1, endCoord2;

        public SimMove(int sc1, int sc2, int ec1, int ec2)
        {
            startCoord1 = sc1;
            startCoord2 = sc2;
            endCoord1 = ec1;
            endCoord2 = ec2;
        }
    }
}
