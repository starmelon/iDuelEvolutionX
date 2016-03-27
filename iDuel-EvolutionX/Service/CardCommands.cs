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
        private static RoutedUICommand addBlueSign;
        private static RoutedUICommand addBlackSign;
        private static RoutedUICommand addRedSign;
        private static RoutedUICommand addGreenSign;
        //private static RoutedUICommand add
        private static RoutedUICommand setCardRemark;

        static CardCommands()
        {
            addBlueSign = new RoutedUICommand("addBlueSign", "addBlueSign", typeof(CardCommands));
            addBlackSign = new RoutedUICommand("addBlackSign", "addBlackSign", typeof(CardCommands));
            addRedSign = new RoutedUICommand("addRedSign", "addRedSign", typeof(CardCommands));
            addGreenSign = new RoutedUICommand("addGreenSign", "addGreenSign", typeof(CardCommands));

            setCardRemark = new RoutedUICommand("setCardRemark", "setCardRemark", typeof(CardCommands));
        }

        public static RoutedUICommand AddBlueSign
        {
            get
            {
                return addBlueSign;
            }

        }

        public static RoutedUICommand AddBlackSign
        {
            get
            {
                return addBlackSign;
            }

        }


        public static RoutedUICommand AddGreenSign
        {
            get
            {
                return addGreenSign;
            }

        }

        public static RoutedUICommand AddRedSign
        {
            get
            {
                return addRedSign;
            }

        }

        public static RoutedUICommand SetCardRemark
        {
            get
            {
                return setCardRemark;
            }

        }
    }
}
