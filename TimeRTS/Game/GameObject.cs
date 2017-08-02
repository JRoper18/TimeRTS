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
        private const Texture2D cubeTexture = new Texture2D(); //Replace with a default texture
        abstract public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content);
        abstract public void Draw();
        public Texture2D getTextureFromDirection(int dir)
        {
            
        }
    }
}
