using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace iDuel_EvolutionX.Service
{
    class UICommands
    {
        private static RoutedUICommand viewAreaCards;


        static UICommands()
        {
            viewAreaCards = new RoutedUICommand("viewAreaCards", "viewAreaCards", typeof(UICommands));
           
        }

        public static RoutedUICommand ViewAreaCards
        {
            get
            {
                return viewAreaCards;
            }

        }
    }
}
