using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRTS.Game.TurnComponents {
    class MoveForwardTurnComponent : TurnComponent {
        public MoveForwardTurnComponent() {

        }
        public override void doTurn(GameObject obj, MapState map) {
            Vector3 newPos = obj.position + DirectionUtils.GetDirectionUnitVector(obj.direction);
            if (map.getTileAtPosition(obj.position + DirectionUtils.GetDirectionUnitVector(obj.direction)) == null) {
                map.moveGameObject(obj.position, newPos);
            }
        }
    }
}
