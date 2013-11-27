using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InssiParty.Engine
{
    class GameHelpers
    {
        /**
         * Get the count of games that are final.
         */
        static public int finalGameCount(List<GameBase> games)
        {
            int count = 0;

            for (int i = 0; i < games.Count; ++i)
            {
                if (games[i].FinalVersion == true)
                    ++count;
            }

            return count;
        }
    }
}
