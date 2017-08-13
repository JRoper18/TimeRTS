using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRTS.Game.TurnComponents;

namespace TimeRTS.Game {
    enum GameObjectType {
        TILE_GRASS,
        TILE_STAIR_GRASS,
        TILE_STONE,
        UNIT_CAR
    }
    static class GameObjectFactory {
        public static GameObject CreateGameObject(GameObjectType type, Vector3 position, Direction? direction = null) {
            switch (type) {
                case GameObjectType.TILE_GRASS:
                    return new GameObject(new IsometricGraphicsComponent("GrassTile"), new TurnComponent(), position);
                case GameObjectType.TILE_STAIR_GRASS:
                    return new GameObject(new IsometricGraphicsComponent("GrassTileStair"), new TurnComponent(), position, direction);
                case GameObjectType.TILE_STONE:
                    return new GameObject(new IsometricGraphicsComponent("StoneTile"), new TurnComponent(), position);
                case GameObjectType.UNIT_CAR:
                    return new GameObject(new IsometricGraphicsComponent("CarUnit"), new MoveForwardTurnComponent(), position, direction);
                default:
                    return new GameObject(new IsometricGraphicsComponent("GrassTile"), new TurnComponent(), position);
            }
        }
    }
}
