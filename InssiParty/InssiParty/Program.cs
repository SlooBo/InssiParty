using System;

namespace InssiParty
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (InssiGame game = new InssiGame())
            {
                game.Run();
            }
        }
    }
#endif
}

