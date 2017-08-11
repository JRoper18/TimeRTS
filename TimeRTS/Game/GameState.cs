using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRTS.Game
{
    class GameState
    {
        public static GameState instance;
        private static readonly Object lockObj = new Object();

        private MapState[] mapsOverTime = new MapState[] {new MapState(new Microsoft.Xna.Framework.Vector3(8,8,2))};
        public const int WINDOW_HEIGHT = 900;
        public const int WINDOW_WIDTH = 1600;
        private GameState(){
            
        }

        public MapState getMapAtTime(int time){
            return this.mapsOverTime[time];
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
