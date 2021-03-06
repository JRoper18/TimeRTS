﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRTS.Game {
    class IsometricGraphicsComponent : GraphicsComponent {
        private const int TILE_CONTENT_WIDTH = 887; //The actual image is this wide
        private const int TILE_CONTENT_HEIGHT = 1024; //Each cube is this tall in the given content. 
        public IsometricGraphicsComponent(string textureName) : base(textureName) {
        }
        public override RenderData GetRenderData(GameObject obj) {
            Rectangle sourceRect = new Rectangle(0, (((int) GameRenderer.GetDirection() + (int) obj.direction) % 4) * TILE_CONTENT_HEIGHT, TILE_CONTENT_WIDTH, TILE_CONTENT_HEIGHT);
            return new RenderData(this.texture, sourceRect);
        }
    }
}
