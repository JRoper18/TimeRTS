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
        public GameObject(Vector3 position, GraphicsComponent graphics, Direction? direction = Direction.NORTHEAST)  {
            this.position = position;
            this.graphics = graphics;
            if (direction.HasValue) {
                this.direction = direction.Value;
            }
            else {
                this.direction = Direction.NORTHEAST;
            }
        }
        public RenderData GetRenderData() {
            return this.graphics.GetRenderData(this);
        }
    }
}
