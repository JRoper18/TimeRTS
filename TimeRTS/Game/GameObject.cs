using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRTS.Game
{
    /// <summary>
    /// A GameObject is something that is rendered onto the screen that is in the world (so not UI). 
    /// </summary>
    class GameObject
    {
        public Vector3 position;
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
