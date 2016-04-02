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

        private static RoutedUICommand setCardRemark;
        private static RoutedUICommand set2AtkOrDef;
        private static RoutedUICommand set2FrontOrBack;
        private static RoutedUICommand set2BackDef;

        private static RoutedUICommand release2Graveyard;
        private static RoutedUICommand release2Banish;

        private static RoutedUICommand back2MainDeck;

        static CardCommands()
        {
            addBlueSign = new RoutedUICommand("addBlueSign", "addBlueSign", typeof(CardCommands));
            addBlackSign = new RoutedUICommand("addBlackSign", "addBlackSign", typeof(CardCommands));
            addRedSign = new RoutedUICommand("addRedSign", "addRedSign", typeof(CardCommands));
            addGreenSign = new RoutedUICommand("addGreenSign", "addGreenSign", typeof(CardCommands));

            setCardRemark = new RoutedUICommand("setCardRemark", "setCardRemark", typeof(CardCommands));
            set2AtkOrDef = new RoutedUICommand("set2AtkOrDef", "set2AtkOrDef", typeof(CardCommands));
            set2FrontOrBack = new RoutedUICommand("set2FrontOrBack", "set2FrontOrBack", typeof(CardCommands));
            set2BackDef = new RoutedUICommand("set2BackDef", "set2BackDef", typeof(CardCommands));

            release2Graveyard = new RoutedUICommand("release2Graveyard", "release2Graveyard", typeof(CardCommands));
            release2Banish = new RoutedUICommand("release2Banish", "release2Banish", typeof(CardCommands));

            back2MainDeck = new RoutedUICommand("back2MainDeck", "back2MainDeck", typeof(CardCommands));
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

        public static RoutedUICommand Set2AtkOrDef
        {
            get
            {
                return set2AtkOrDef;
            }

        }

        public static RoutedUICommand Set2FrontOrBack
        {
            get
            {
                return set2FrontOrBack;
            }

        }

        public static RoutedUICommand Set2BackDef
        {
            get
            {
                return set2BackDef;
            }

        }

        public static RoutedUICommand Release2Graveyard
        {
            get
            {
                return release2Graveyard;
            }

        }

        public static RoutedUICommand Release2Banish
        {
            get
            {
                return release2Banish;
            }

        }

        public static RoutedUICommand Back2MainDeck
        {
            get
            {
                return back2MainDeck;
            }

        }
    }
}
