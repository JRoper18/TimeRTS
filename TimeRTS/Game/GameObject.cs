using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRTS.Game.TurnComponents;

namespace TimeRTS.Game
{
    /// <summary>
    /// A generic container for all objects in-game. 
    /// </summary>
    class GameObject
    {
        public Vector3 position;
        public Direction direction;

        private TurnComponent turn;
        private GraphicsComponent graphics;
        public GameObject(GraphicsComponent graphics, TurnComponent turn, Vector3? position, Direction? direction = Direction.NORTHEAST)  {
            this.graphics = graphics;
            this.turn = turn;
            if (position.HasValue) {
                this.position = position.Value;
            }
            else {
                this.position = new Vector3(0, 0, 0);
            }
            if (direction.HasValue) {
                this.direction = direction.Value;
            }
            else {
                this.direction = Direction.NORTHEAST;
            }
        }
        public void doTurn(MapState map) {
            this.turn.doTurn(this, map);
        }
        public RenderData GetRenderData() {
            return this.graphics.GetRenderData(this);
        }
    }
}
