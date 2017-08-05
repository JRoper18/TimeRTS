using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRTS.Game {
    class RenderData {
        public Texture2D texture;
        public Rectangle? sourceRectangle;
        public RenderData(Texture2D texture, Rectangle sourceRect) {
            this.texture = texture;
            this.sourceRectangle = sourceRect;
        }
        public RenderData(Texture2D texture) {
            this.texture = texture;
            this.sourceRectangle = null;
        }
    }
}
