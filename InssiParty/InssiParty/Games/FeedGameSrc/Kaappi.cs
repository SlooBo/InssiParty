using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;

namespace InssiParty.Games.FeedGameSrc
{
    class Kaappi
    {
        public bool auki { get; set; }

        public int tavara_id { get; set; }

        public Vector2 sijainti { get; set; }
        public Vector2 koko { get; set; }

        public Kaappi()
        {
            auki = false;
        }


    }
}
