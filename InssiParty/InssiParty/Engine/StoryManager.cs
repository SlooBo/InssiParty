using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InssiParty.Engine
{
    /**
     * Manages the story state!
     */
    class StoryManager
    {
        public int hp { get; set; }
        public int points { get; set; }

        private List<GameBase> games;

        public StoryManager(List<GameBase> games)
        {
            //Get the pointer to the game list.
            this.games = games;
            Console.WriteLine("[StoryManager] Init ok!");
        }

        public void StopStory()
        {
            Console.WriteLine("[StoryManager] Story stop forced.");
        }

        public void StartStory()
        {
            Console.WriteLine("[StoryManager] Starting story...");
            points = 0;
            hp = 0;
        }

        public void StoryLogicUpdate()
        {
        }

        public void StoryNextGame(bool victoryState)
        {
            if (victoryState == true)
            {
                ++points;
                Console.WriteLine("[StoryManager] +1 point for victory");
            }
            else
            {
                --hp;
                Console.WriteLine("[StoryManager] -1 hp for failure");
            }

            //TODO: Randomize next game.
        }
    }
}
