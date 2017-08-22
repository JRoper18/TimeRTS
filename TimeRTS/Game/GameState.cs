using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TimeRTS.Game
{
    class GameState
    {
        public static GameState instance;
        private static readonly Object lockObj = new Object();
        private static int currentViewedTurn = 0;
        public const int MAX_TURNS = 6;
        private MapState[] mapsOverTime = new MapState[MAX_TURNS];
        public const int WINDOW_HEIGHT = 900;
        public const int WINDOW_WIDTH = 1600;
        public static readonly Vector3 mapSize = new Vector3(10, 10, 2);
        private GameState(){
            this.mapsOverTime[0] = new MapState(mapSize);
        }
        public MapState GetCurrentViewedMap() {
            return this.mapsOverTime[currentViewedTurn];
        }
        public MapState GetMapAtTime(int time){
            return this.mapsOverTime[time];
        }
        public void SimulateTurns() {
            for(int i = 1; i<MAX_TURNS; i++) {
                this.mapsOverTime[i] = SimNextTurn(this.mapsOverTime[i - 1]);
            }
        }
        private MapState SimNextTurn(MapState oldMap) {
            Vector3 size = oldMap.getSize();
            List<GameObject> objects = new List<GameObject>();
            GameObject[,,] newMapArray = new GameObject[(int)size.X, (int)size.Y, (int)size.Z];
            for (int x = 0; x<size.X; x++) {
                for(int y = 0; y<size.Y; y++) {
                    for(int z = 0; z<size.Z; z++) {
                        GameObject currentObj = oldMap.getTileAtPosition(new Vector3(x, y, z));
                        if(currentObj != null) {
                            objects.Add(currentObj);
                        }
                    }
                }
            }
            for(int i = 0; i<objects.Count; i++) {
                GameObject currentObj = objects[i];
                currentObj.doTurn(oldMap);
                Vector3 pos = currentObj.position;
                newMapArray[(int) pos.X, (int) pos.Y, (int) pos.Z] = currentObj;
            }

            return new MapState(newMapArray);
        }
        public void Update(GameTime time) {
            if (InputHandler.WasPressed(Keys.E)) {
                GameRenderer.RotateCameraClockwise();
            }
            else if (InputHandler.WasPressed(Keys.Q)) {
                GameRenderer.RotateCameraCounterClockwise();
            }

            if (InputHandler.WasPressed(Keys.Up) && currentViewedTurn < MAX_TURNS-1) {
                currentViewedTurn++;
            }
            else if (InputHandler.WasPressed(Keys.Down) && currentViewedTurn != 0) {
                currentViewedTurn--;
            }

            if (InputHandler.WasPressed(Keys.OemPlus)) {
                GameRenderer.scale *= 1.1f;
            }
            else if (InputHandler.WasPressed(Keys.OemMinus)) {
                GameRenderer.scale *= 0.9f;
            }
            const float SCROLL_SENSITIVITY = .01f;
            const int MOVE_SENSITIVITY = 30;
            float scrollDiff = InputHandler.ScrollDifference();
            if(scrollDiff == 0) {
                //Nothing
            }
            else if(scrollDiff < 0) {
                GameRenderer.scale *= (1 / (-1 * scrollDiff * SCROLL_SENSITIVITY));
            }
            else {
                GameRenderer.scale *= (1 * scrollDiff * SCROLL_SENSITIVITY);
            }
            if (InputHandler.IsMouseDown()) {
                Debug.WriteLine(GameRenderer.ScreenToIsometric(InputHandler.MousePosition().ToVector2(), this.GetCurrentViewedMap()));
            }
            if (InputHandler.WasPressed(Keys.R)) {
                GameRenderer.cameraOffset = new Vector2(0, 0);
            }
            if (InputHandler.IsPressed(Keys.S)) {
                GameRenderer.cameraOffset.Y -= MOVE_SENSITIVITY;
            }
            else if (InputHandler.IsPressed(Keys.W)) {
                GameRenderer.cameraOffset.Y += MOVE_SENSITIVITY;
            }
            if (InputHandler.IsPressed(Keys.D)) {
                GameRenderer.cameraOffset.X -= MOVE_SENSITIVITY;
            }
            else if (InputHandler.IsPressed(Keys.A)) {
                GameRenderer.cameraOffset.X += MOVE_SENSITIVITY;
            }
        }

        /// <summary>
        /// Returns the only instance of GameState. SINGLETON and thread-safe. 
        /// </summary>
        public static GameState Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                            instance = new GameState();
                    }
                }

                return instance;
            }
        }
    }

}
