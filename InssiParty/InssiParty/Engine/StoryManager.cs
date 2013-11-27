using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InssiParty.Engine
{
    /**
     * Manages the story state!
     * 
     * Starts new games when needed, tells when the games should be stopped.
     * Keeps count what games have been played and whats the current score.
     */
    class StoryManager
    {
        public int hp { get; set; }
        public int points { get; set; }

        private List<GameBase> games;
        private List<GameBase> gamesPlayed;

        private Random random;

        public StoryManager()
        {
            //Games list is just a local list of the games played so same game won't come up twice.
            gamesPlayed = new List<GameBase>();

            //list of games that are "done" Separated from the list in the main menu.
            games = new List<GameBase>();

            random = new Random();

            Console.WriteLine("[StoryManager] Init ok!");
        }

        /**
         * Get pointers to all games that are "done".
         */
        public void LoadGames(List<GameBase> allGames)
        {
            for (int i = 0; i < allGames.Count; ++i)
            {
                if (allGames[i].FinalVersion)
                    games.Add(allGames[i]);
            }

            Console.WriteLine("[StoryManager] found " + games.Count + " games that are ready for the story mode.");
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

            StartNextGame();
        }

        public void StoryLogicUpdate()
        {
            //TODO: Move the game updating and drawing here ?!?
        }

        public void StoryLogicDraw()
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

        private void StartNextGame()
        {
            bool gameSelected = false;
            int id = 0;
            int missCount = -1;

            if(gamesPlayed.Count == games.Count)
            {
                //All games have been played! Give extra +5 points and clear the list.
                points += 5;

                gamesPlayed.Clear();
                Console.WriteLine("[StoryManager] All games played! +5 points and clearing list");
            }

            while (gameSelected == false)
            {
                id = random.Next(0, games.Count);
                gameSelected = true;

                //check if it has been already played
                for (int i = 0; i < gamesPlayed.Count; ++i)
                {
                    if (games[id] == gamesPlayed[i])
                        gameSelected = false; //The game was already played.
                }

                ++missCount;
            }

            Console.WriteLine("[StoryManager] " + games[id].Name + " chosen. (" + missCount + ") miss count on randoming.");

            gamesPlayed.Add(games[id]);
        }
    }
}
