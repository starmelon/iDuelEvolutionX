using iDuel_EvolutionX.Model;
using iDuel_EvolutionX.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace iDuel_EvolutionX.Service
{
    class SignOperate
    {
        public static void execute_Addsign(object sender, ExecutedRoutedEventArgs e)
        {
            CardControl card = e.OriginalSource as CardControl;
            StackPanel sp = (sender as MyCanvas).signs;
            SignTextBlock stb = new SignTextBlock();
            stb.Height = 25;
            stb.Width = 25;
            stb.Content = "1";
            switch ((e.Command as RoutedUICommand).Name)
            {
                case "addBlueSign":
                    stb.BorderBrush = Brushes.Blue;
                    break;
                case "addBlackSign":
                    stb.BorderBrush = Brushes.Black;
                    break;
                case "addRedSign":
                    stb.BorderBrush = Brushes.Red;
                    break;
                case "addGreenSign":
                    stb.BorderBrush = Brushes.Green;
                    break;
                default:
                    break;
            }
            card.signs.Add(stb);
            sp.Children.Add(stb);
            e.Handled = true;
            //addBlueSign = new RoutedUICommand("addBlueSign", "addBlueSign", typeof(CardCommands));
            //addBlackSign = new RoutedUICommand("addBlackSign", "addBlackSign", typeof(CardCommands));
            //addRedSign = new RoutedUICommand("addRedSign", "addRedSign", typeof(CardCommands));
            //addGreenSign = new RoutedUICommand("addGreenSign", "addGreenSign", typeof(CardCommands));
        }
    }
}
