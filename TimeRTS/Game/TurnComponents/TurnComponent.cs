using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRTS.Game.TurnComponents {
    class TurnComponent {
        public TurnComponent() {

        }
        public virtual void doTurn(GameObject obj, MapState map) {
            //Do nothing by default.
        }
    }
}
