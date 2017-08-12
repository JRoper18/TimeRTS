using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRTS.Game {
    enum GameObjectType {
        TILE_GRASS,
        TILE_STAIR_GRASS,
        TILE_STONE,
        UNIT_CAR
    }
    static class GameObjectFactory {
        public static GameObject CreateGameObject(GameObjectType type, Vector3 position) {
            switch (type) {
                case GameObjectType.TILE_GRASS:
                    return new GameObject(position, new IsometricGraphicsComponent("GrassTile"));
                case GameObjectType.TILE_STAIR_GRASS:
                    return new GameObject(position, new IsometricGraphicsComponent("GrassTileStair"));
                case GameObjectType.TILE_STONE:
                    return new GameObject(position, new IsometricGraphicsComponent("StoneTile"));
                case GameObjectType.UNIT_CAR:
                    return new GameObject(position, new IsometricGraphicsComponent("CarUnit"));
                default:
                    return new GameObject(position, new IsometricGraphicsComponent("GrassTile"));
            }
        }
    }
}
