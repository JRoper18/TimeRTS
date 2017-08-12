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
        public int direction;

        private TurnComponent turn;
        private GraphicsComponent graphics;
        public GameObject(Vector3 position, GraphicsComponent graphics)  {
            this.position = position;
            this.graphics = graphics;
        }
        public RenderData GetRenderData() {
            return this.graphics.GetRenderData();
        }
    }
}
