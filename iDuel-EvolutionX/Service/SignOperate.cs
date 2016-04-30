using iDuel_EvolutionX.EventJson;
using iDuel_EvolutionX.Model;
using iDuel_EvolutionX.UI;
using NBX3.Service;
using Newtonsoft.Json;
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
            CardUI card = e.OriginalSource as CardUI;
            StackPanel sp = (sender as MyCanvas).signs;
            SignTextBlock stb = new SignTextBlock(true);
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
            stb.Tag = card;
            sp.Children.Add(stb);
            e.Handled = true;

            #region 指令发送

            SignInfo signInfo = new SignInfo();
            int cardid = CardOperate.getCardID(card);
            signInfo.cardID = cardid;
            foreach (SignTextBlock item in sp.Children)
            {
                //Dictionary<string, string> content = new Dictionary<string, string>();
                //content.Add(item.Content.ToString(), item.ToolTip.ToString());
                signInfo.signs.Add(new SignInfo.SignMessage(item.BorderBrush, item.Content.ToString(), item.ToolTip == null?null:item.ToolTip.ToString()));
        
            }
            String contentJson = JsonConvert.SerializeObject(signInfo);

            BaseJson bj = new BaseJson();
            bj.guid = DuelOperate.getInstance().myself.userindex;
            bj.cid = "";
            bj.action = ActionCommand.CARD_SIGN_ACTION;
            bj.json = contentJson;
            String json = JsonConvert.SerializeObject(bj);
            DuelOperate.getInstance().sendMsg(json);

            #endregion
            //addBlueSign = new RoutedUICommand("addBlueSign", "addBlueSign", typeof(CardCommands));
            //addBlackSign = new RoutedUICommand("addBlackSign", "addBlackSign", typeof(CardCommands));
            //addRedSign = new RoutedUICommand("addRedSign", "addRedSign", typeof(CardCommands));
            //addGreenSign = new RoutedUICommand("addGreenSign", "addGreenSign", typeof(CardCommands));
        }
    }
}
