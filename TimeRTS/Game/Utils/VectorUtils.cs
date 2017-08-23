using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRTS.Game.Utils {
    public static class VectorUtils {
        public static Vector2 RotateAroundOrigin(Vector2 point, float degrees, Vector2 origin) {
            Vector2 offsetPoint = point - origin;
            return (RotateAroundOrigin(offsetPoint, degrees) + origin);
        }
        public static Vector3 RotateAroundOrigin3D(Vector3 point, float degrees, Vector2 origin) {
            return To3(RotateAroundOrigin(To2(point), degrees, origin), point.Z);
        }
        public static Vector2 RotateAroundOrigin(Vector2 point, float degrees) {
            float rad = (float)(degrees * Math.PI / 180);
            float sin = (float) Math.Sin(rad);
            float cos = (float) Math.Cos(rad);
            int rem = (int) degrees % 90;
            if(rem == 0) {
                switch(degrees % 360) {
                    case 0:
                        return point;
                    case 90:
                        sin = 1;
                        cos = 0;
                        break;
                    case 180:
                        sin = 0;
                        cos = -1;
                        break;
                    case 270:
                        sin = -1;
                        cos = 0;
                        break;
                }
            }
            return new Vector2((point.X * cos) - (point.Y * sin), (point.Y * cos) + (point.X * sin));
        }
        public static Vector2 To2(Vector3 point) {
            return new Vector2(point.X, point.Y);
        }
        public static Vector3 To3(Vector2 point, float z = 0) {
            return new Vector3(point.X, point.Y, z);
        }
    }
}
