using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRTS.Game {
    class GraphicsComponent {
        public String textureName;
        public Texture2D texture;
        protected static int cameraDir = 0;
        public GraphicsComponent(String textureName) {
            this.textureName = textureName;
            this.texture = GameRenderer.textures[this.textureName].spriteSheet;
        }
        public virtual RenderData GetRenderData() {
            return new RenderData(this.texture);
        }
        public static void RotateClockwise() {
            if(cameraDir == 3) {
                cameraDir = 0;
            }
            else {
                cameraDir++;
            }
            Debug.WriteLine(cameraDir);
        }
        public static void RotateCounterClockwise() {
            if(cameraDir == 0) {
                cameraDir = 3;
            }
            else {
                cameraDir--;
            }
            Debug.WriteLine(cameraDir);
        }
        public static int GetCameraDirection() {
            return cameraDir;
        }
    }
}
