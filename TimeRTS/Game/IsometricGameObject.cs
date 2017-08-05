using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace TimeRTS.Game {
    class IsometricGameObject : GameObject{
        public IsometricGameObject(Vector3 position, String textureName): base(position, textureName) {

        }
        public override RenderData GetRenderData(int cameraDir) {
            Rectangle sourceRect = new Rectangle(0, cameraDir * 128, 128, 128);
            return new RenderData(this.texture.spriteSheet, sourceRect);
        }
    }
}
