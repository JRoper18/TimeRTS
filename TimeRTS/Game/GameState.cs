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

        private MapState[] mapsOverTime;

        private GameState()
        {

        }

        public MapState getMapAtTime(int time)
        {
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
