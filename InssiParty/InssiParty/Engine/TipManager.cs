using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InssiParty.Engine
{
    class TipManager
    {
        Random random;
        private List<String> tipList;

        public TipManager()
        {
            random = new Random();
            tipList = new List<String>();

            Console.WriteLine("[TipManager] init ok!");
        }

        public int getTipCount()
        {
            return tipList.Count;
        }

        public void addTip(String tip)
        {
            tipList.Add(tip);
        }

        public String getRandomTip()
        {
            return tipList[random.Next(0, tipList.Count)];
        }
    }
}
