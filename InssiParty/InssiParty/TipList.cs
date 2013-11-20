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
        private static void tipList(TipManager tipManager)
        {
            tipManager.addTip("Olut on hyvää");
            tipManager.addTip("Alienteknologiaa!");
            tipManager.addTip("Vesi on märkää");
            tipManager.addTip("Lisää tippejä tiedostoon TipList.cs!");
            tipManager.addTip("                           D:");
            tipManager.addTip("Inssit > katit");
            tipManager.addTip("Tontzan hakkaaminen laudalla tuottaa mielihyvää.");
            tipManager.addTip("Right click to skip transition!");
            tipManager.addTip("XNA on vanhentunutta teknologiaa!");
            tipManager.addTip("} //MITÄ VITTUA!? If-lause renderissä aiheuttaa Avastin \nsekoamisen?");
            tipManager.addTip("Vastoin yleistä käsitystä, insinöörit eivät pohjimmiltaan\n ole kovinkaan fiksuja.");
            tipManager.addTip("Tutkimusten mukaan auringon massa on huomattavisti suurempi\n kuin omenan.");
            tipManager.addTip("Ohjelmoija on organismi joka muuntaa kofeiinin koodiksi.");
            tipManager.addTip("Insinööri korjaa ongelmia joita ei tiennyt olevan olemassakaan\n tavoilla joita hän ei itse ymmärrä.");
        }

        public static void InitTipList(TipManager tipManager)
        {
            tipList(tipManager);

            Console.WriteLine("[TipList] " + tipManager.getTipCount() + " tips loaded!");
        }

    }
}
