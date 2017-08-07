using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRTS.Game {
    enum GameObjectType {
        TILE_GRASS
    }
    static class GameObjectFactory {
        public static GameObject CreateGameObject(GameObjectType type, Vector3 position) {
            switch (type) {
                case GameObjectType.TILE_GRASS:
                    return new GameObject(position, new IsometricGraphicsComponent("GrassTile"));
                default:
                    return new GameObject(position, new IsometricGraphicsComponent("GrassTile"));
            }
        }
    }
}
