using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace iDuel_EvolutionX.Service
{
    class CardCommands
    {
        private static RoutedUICommand addSign;

        static CardCommands()
        {
            addSign = new RoutedUICommand("addSign", "addSign", typeof(CardCommands));
        }

        public static RoutedUICommand AddSign
        {
            get
            {
                return addSign;
            }

        }

        
    }
}
