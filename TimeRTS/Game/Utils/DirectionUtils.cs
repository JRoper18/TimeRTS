using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRTS.Game {
    enum Direction {
        NORTHEAST,
        SOUTHEAST,
        SOUTHWEST,
        NORTHWEST
    }
    static class DirectionUtils {
        public static Vector3 GetDirectionUnitVector(Direction dir) {
            switch (dir) {
                case Direction.NORTHEAST:
                    return new Vector3(0, -1, 0);
                case Direction.NORTHWEST:
                    return new Vector3(1, 0, 0);
                case Direction.SOUTHEAST:
                    return new Vector3(0, 1, 0);
                case Direction.SOUTHWEST:
                    return new Vector3(-1, 0, 0);
            }
            return new Vector3(0, 0, 0);
        }
        public static int GetAngle(Direction dir) {
            if(dir == Direction.NORTHEAST) {
                return 0;
            }
            return 360 - ((int)dir) * 90;
        }
    }
}
