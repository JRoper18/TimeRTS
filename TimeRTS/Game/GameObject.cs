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
    /// A GameObject is something that is rendered onto the screen that is in the world (so not UI). It is isometrically textured. 
    /// </summary>
    abstract class GameObject
    {
        public Vector3 position;
        private GameObjectTexture texture;
        public GameObject(Vector3 position, String textureName) {
            this.position = position;
            this.texture = GameRenderer.textures[textureName];
        }
        virtual public Texture2D GetCurrentTexture(int cameraDir) {
            return this.texture.spriteSheet;
        }
    }
}
