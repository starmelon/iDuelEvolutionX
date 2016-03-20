using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iDuel_EvolutionX.Service
{
    class DeckManergerOperatre
    {
        private static DeckManergerOperatre deckManerger;
        private static MainWindow mainwindow;

        public DeckManergerOperatre()
        {

        }

        public static DeckManergerOperatre getInstance()
        {
            if (deckManerger == null)
            {
                deckManerger = new DeckManergerOperatre();


            }
            return deckManerger;
        }

        public static DeckManergerOperatre getInstance(MainWindow mw)
        {
            if (deckManerger == null)
            {
                deckManerger = new DeckManergerOperatre();
                mainwindow = mw;

                
            }
            return deckManerger;
        }



    }
}
