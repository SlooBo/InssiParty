using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InssiParty.Engine;

namespace InssiParty
{
    /**
     * This class has list of all the tips that the game will have.
     */
    static class TipList
    {
        public static void InitTipList(TipManager tipManager)
        {
            tipManager.addTip("Olut on hyvää");
            tipManager.addTip("Alienteknologiaa!");
            tipManager.addTip("Vesi on märkää");
            tipManager.addTip("Lisää tippejä tiedostoon TipList.cs!");
            tipManager.addTip("                           D:");
            tipManager.addTip("Inssit > katit");
            tipManager.addTip("Tontzan hakkaaminen laudalla tuottaa mielihyvää");

            Console.WriteLine("[TipList] " + tipManager.getTipCount() + " tips loaded!");
        }
    }
}
