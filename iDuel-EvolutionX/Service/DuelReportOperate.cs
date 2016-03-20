using iDuel_EvolutionX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace iDuel_EvolutionX.Service
{
    class DuelReportOperate
    {
        private static MainWindow mainwindow;

        //mainwindow.children[cv_num].Name - [cv_children_num] - [string] - mainwindow.children[cv_num].children[cv_children_num] -> mainwindow.children[cv_aim_num]
        //
        //<原控件位置序号>              <操作的卡片在控件中的序号>
        //     参数1            参数3             参数2                 参数4           参数5                 参数6
        //<原位置控件名称> -   <动作1>   -  [被操作的卡片名字] -> <目标控件位置序号> - <动作2>              -<动作3>-
        //  <从手卡>       -     召唤    -      [cardname]     ->         有         - <>
        //  <从手卡>       -     盖伏    -          [?]        ->         有         - <>
        //  <从手卡>       -   特殊召唤  -      [cardname]     ->         有         - <>
        //  <从手卡>       -     发动    -      [cardname]     ->         有         - <>
        //  <从手卡>       -      把     -          [?]        ->         有         - <放回卡组顶端>
        //  <从手卡>       -      把     -      [cardname]     ->         有         - <送去墓地>
        //  <从手卡>       -      把     -      [cardname]     ->         有         - <除外>

        //  <从场上>       -      把     -      [cardname]     ->         有         - <送往墓地>
        //  <从场上>       -      把     -      [cardname]     ->         有         - <除外>
        //  <从场上>       -      把     -      [cardname]     ->         有         - <放回卡组顶端>
        //  <从场上>       -      把     -      [cardname]     ->         有         - <解放>
        //  <从场上>       -      把     -      [cardname]     ->         有         - <取回手卡>
        //  <从场上>       -      把     -         [?]         ->         有         - <取回手卡>
        //  <从场上>       -      把     -      [cardname]     ->         有         - <送往对手墓地> 
        //  <从场上>       -     转移    -      [cardname]     ->         有         - 
        //    <场上>       -             -      [cardname]     ->                    - <变更为 表侧防守>
        //    <场上>       -             -      [cardname]     ->                    - <变更为 里侧防守>
        //    <场上>       -             -      [cardname]     ->                    - <变更为 表侧攻击>
        //    <场上>       -             -      [cardname]     ->                    - <变更为 里侧攻击>
        //    <场上>       -             -      [cardname]     ->                    - <发动效果>   
        //    <场上>       -             -      [cardname]     ->                    - <发动效果>          - <对象为>
        //    <场上>       -             -      [cardname]     ->                    - <攻击>              - [cardname2]
        //    <场上>       -             -      [cardname]     ->                    - <指示物增加>
        //    <场上>       -             -      [cardname]     ->                    - <指示物减少>
        //    <场上>       -             -      [cardname]     ->                    - <指示物清空>
        //    <场上>       -             -      [cardname]     ->                    - <修改备注为->

        //    <墓地>       -             -      [cardname]     ->                    - <发动效果>
        //  <从墓地>       -     把      -      [cardname]     ->         有         - <取回手卡>
        //  <从墓地>       -     把      -      [cardname]     ->         有         - <放回卡组顶端>
        //  <从墓地>       -     把      -      [cardname]     ->         有         - <除外>
        //  <从墓地>       -   特殊召唤  -      [cardname]     ->         有         -

        //  <从卡组>       -     查看    -                     ->                    - <顶牌>  -
        //  <从卡组>       -     查看    -                     ->                    - <上方>  - X张 -
        //  <从卡组>       -     抽取    -                     ->                    - <上方>  - X张 -
        //  <从卡组>       -      把     -                     ->                    - <顶牌>  - 放回底部>
        //  <从卡组>       -      把     -      [cardname]     ->         有         - <送往墓地>
        //  <从卡组>       -      把     -      [cardname]     ->         有         - <除外>
        //  <从卡组>       -      把     -         [?]         ->         有         - <顶牌背面除外>    
        //  <从卡组>       -   特殊召唤  -      [cardname]     ->         有         - 
        //  <从卡组>       -      把     -      [cardname]     ->         有         - <取回手卡>
        //                 -             -                     ->                    - <洗切卡组>

        //    <除外>       -             -      [cardname]     ->                    - <发动效果>
        //  <从除外>       -   特殊召唤  -      [cardname]     ->         有         -
        //  <从除外>       -      把     -      [cardname]     ->         有         - <送往墓地>
        //  <从除外>       -      把     -      [cardname]     ->         有         - <取回手卡>

        //    <除外>       -             -      [cardname]     ->                    - <发动效果>
        //  <从除外>       -   特殊召唤  -      [cardname]     ->         有         -
        //  <从除外>       -      把     -      [cardname]     ->         有         - <送往墓地>
        //  <从除外>       -      把     -      [cardname]     ->         有         - <取回手卡>


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cv_num">哪个Canvas</param>
        /// <param name="cv_children_num">Canvas中的哪张卡</param>
        /// <param name="str"></param>
        /// <param name="cv_aim_num"></param>
        //public static void analyze(int cv_num,int cv_children_num,string action1,int cv_aim_num,string action2,string action3)
        //{
        //    if (action1.Equals("召唤") || action1.Equals("特殊召唤") ||action1.Equals("") )
        //    {
        //        Canvas cv_start = mainwindow.MySpace.Children[transform(cv_num)] as Canvas;
        //        Canvas cv_aim = mainwindow.MySpace.Children[cv_aim_num] as Canvas;
        //        Card cd = cv_start.Children[cv_children_num] as Card;

        //        OpponentOperate.Move(cv_children_num, action1, cv_start, cv_aim);
        //        Console.WriteLine(from(cv_num) + " - " + action1 + " - " + cd.cardName + " ->" + "[" + cv_aim_num + "]");
        //    }


           
        //   // int 
            
        //    //int cv_aim
            
        //}


        public static string action(int num)
        {
            switch (num)
            {
                case 1: return " 召唤 ";
                case 2: return "<手卡>";
                case 3: return "<手卡>";
                case 4: return "<手卡>";
                case 5: return "<手卡>";
                case 6: return "<手卡>";
                case 7: return "<手卡>";
                case 8: return "<手卡>";
                case 9: return "<手卡>";
                case 10: return "<手卡>";
                case 11: return "<手卡>";
                case 12: return "<手卡>";
                case 13: return "<手卡>";
                case 14: return "<手卡>";
                case 15: return "<手卡>";
                case 16: return "<手卡>";
                case 17: return "<手卡>";
                case 18: return "<手卡>";
                case 19: return "<手卡>";
                case 20: return "<手卡>";
                case 21: return "<手卡>";
                case 22: return "<手卡>";
                case 23: return "<手卡>";
                case 24: return "<手卡>";
                case 25: return "<手卡>";
                case 26: return "<手卡>";
                case 27: return "<手卡>";
                case 28: return "<手卡>";
                case 29: return "<手卡>";
                case 30: return "<手卡>";
                case 31: return "<手卡>";
                case 32: return "<手卡>";
                case 33: return "<手卡>";
                case 34: return "<手卡>";
                case 35: return "<手卡>";
                case 36: return "<手卡>";
                case 37: return "<手卡>";
                case 38: return "<手卡>";
                case 39: return "<手卡>";
                case 40: return "<手卡>";
                case 41: return "<手卡>";
                case 42: return "<手卡>";
                case 43: return "<手卡>";
                case 44: return "<手卡>";
                case 45: return "<手卡>";
                case 46: return "<手卡>";
                case 47: return "<手卡>";
                case 48: return "<手卡>";

            }
            return null;
        }


        public  static string from(int num)
        {
            switch (num)
            {
                case 35: return "<手卡>";
                case 11: return "<卡组>";
                case 5 : return "<额外>";
                case 9 : return "<墓地>";
                case 10: return "<右灵摆区>";
                case 4 : return "<左灵摆区>";
                case 23: return "<除外>";
                case 3 : return "<场地>";

                case 25: return "<场上 - 1>";
                case 26: return "<场上 - 2>";
                case 27: return "<场上 - 3>";
                case 28: return "<场上 - 4>";
                case 29: return "<场上 - 5>";
                case 30: return "<场上 - 6>";
                case 31: return "<场上 - 7>";
                case 32: return "<场上 - 8>";
                case 33: return "<场上 - 9>";
                case 34: return "<场上 - 10>";
            }

            return null;
        }

        /// <summary>
        /// 翻译场地控件的NUM
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int Analyze_canvas(string num)
        {
            switch (num)
            {
                case "card_1_hand": return 36;
                case "card_1_Deck": return 0;
                case "card_1_Extra": return 6;
                case "card_1_Graveyard": return 2;
                case "card_1_Right": return 1;
                case "card_1_Left": return 7;
                case "card_1_Outside": return 24;
                case "card_1_Area": return 8;

                case "card_1_1": return 13;
                case "card_1_2": return 14;
                case "card_1_3": return 15;
                case "card_1_4": return 16;
                case "card_1_5": return 17;
                case "card_1_6": return 18;
                case "card_1_7": return 19;
                case "card_1_8": return 20;
                case "card_1_9": return 21;
                case "card_1_10": return 22;
            }

            return -1;
        }


        public static string from(string num)
        {
            switch (num)
            {
                case "card_1_hand": return "<手卡>";
                case "card_1_Deck": return "<卡组>";
                case "card_1_Extra": return "<额外>";
                case "card_1_Graveyard": return "<墓地>";
                case "card_1_Right": return "<右灵摆区>";
                case "card_1_Left": return "<左灵摆区>";
                case "card_1_Outside": return "<除外>";
                case "card_1_Area": return "<场地>";

                case "card_1_1": return "<A - 1>";
                case "card_1_2": return "<A - 2>";
                case "card_1_3": return "<A - 3>";
                case "card_1_4": return "<A - 4>";
                case "card_1_5": return "<A - 5>";
                case "card_1_6": return "<A - 6>";
                case "card_1_7": return "<A - 7>";
                case "card_1_8": return "<A - 8>";
                case "card_1_9": return "<A - 9>";
                case "card_1_10": return "<A - 10>";
            }

            return null;
        }

        public static string analyze_state(Card card)
        {
            //string a;
            //string b;

            //a = card.isBack? "里侧" : "表侧";
            //b = card.isDef ? "防守" : "攻击";

            return (card.isBack ? "里侧" : "表侧") + " · " + (card.isDef ? "防守" : "攻击");
        }

        /* 11,36,6,
         第 35 个控件：card_1_hand
         第 36 个控件：card_2_hand
         第 11 个控件：card_1_Deck
         第 0  个控件：card_2_Deck  
         第 5  个控件：card_1_Extra
         第 6  个控件：card_2_Extra
         第 9  个控件：card_1_Graveyard
         第 2  个控件：card_2_Graveyard  
         第 10 个控件：card_1_Right
         第 1  个控件：card_2_Right
         第 4  个控件：card_1_Left
         第 7  个控件：card_2_Left 
         第 23 个控件：card_1_Outside
         第 24 个控件：card_2_Outside
         第 3  个控件：card_1_Area
         第 8  个控件：card_2_Area
                    
         第 25 个控件：card_1_1
         第 13 个控件：card_2_1        
         第 26 个控件：card_1_2
         第 14 个控件：card_2_2
         第 27 个控件：card_1_3
         第 15 个控件：card_2_3
         第 28 个控件：card_1_4
         第 16 个控件：card_2_4
         第 29 个控件：card_1_5
         第 17 个控件：card_2_5
         第 30 个控件：card_1_6
         第 18 个控件：card_2_6
         第 31 个控件：card_1_7
         第 19 个控件：card_2_7
         第 32 个控件：card_1_8
         第 20 个控件：card_2_8
         第 33 个控件：card_1_9
         第 21 个控件：card_2_9
         第 34 个控件：card_1_10
         第 22 个控件：card_2_10       
       */
        public static int transform(int num)
        {
            switch (num)
            {
                case 35: return 36;   //手卡
                case 11: return 0;    //卡组
                case 5:  return 6;    //额外
                case 9:  return 2;    //墓地
                case 10: return 1;    //右灵摆
                case 4:  return 7;    //左灵摆
                case 23: return 24;   //除外
                case 3:  return 8;    //场地

                case 25: return 13;
                case 26: return 14;
                case 27: return 15;
                case 28: return 16;
                case 29: return 17;
                case 30: return 18;
                case 31: return 19;
                case 32: return 20;
                case 33: return 21;
                case 34: return 22;
            }
            return -1;
 
        }

    }
}
