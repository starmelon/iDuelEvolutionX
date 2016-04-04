using iDuel_EvolutionX.Model;
using NBX3.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;


namespace iDuel_EvolutionX.UI
{
    public enum Area
    {
        NON_VALUE,

        GRAVEYARD,
        MAINDECK,
        BANISH,
        SPACE,
        EXTRA,
        HAND,
        MONSTER_1,
        MONSTER_2,
        MONSTER_3,
        MONSTER_4,
        MONSTER_5,
        MAGICTRAP_1,
        MAGICTRAP_2,
        MAGICTRAP_3,
        MAGICTRAP_4,
        MAGICTRAP_5,
        PENDULUM_LEFT,
        PENDULUM_RIGHT,

        GRAVEYARD_OP,
        MAINDECK_OP,
        BANISH_OP,
        SPACE_OP,
        EXTRA_OP,
        HAND_OP,
        MONSTER_1_OP,
        MONSTER_2_OP,
        MONSTER_3_OP,
        MONSTER_4_OP,
        MONSTER_5_OP,
        MAGICTRAP_1_OP,
        MAGICTRAP_2_OP,
        MAGICTRAP_3_OP,
        MAGICTRAP_4_OP,
        MAGICTRAP_5_OP,
        PENDULUM_LEFT_OP,
        PENDULUM_RIGHT_OP

    }

    class Base
    {
        public static MainWindow mainwindow;

        /// <summary>
        /// 卡片脱离父控件
        /// </summary>
        /// <param name="card"></param>
        public static void getawayParerent(CardUI card)
        {
            //获得卡片所在父控件并解离
            if (card.Parent != null)
            {
                Canvas cv = card.Parent as Canvas;
                cv.Children.Remove(card);
                Canvas.SetLeft(card, 0);
                Canvas.SetTop(card, 0);
            }
            
        }

        /// <summary>
        /// 获取父控件
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static Canvas getParerent(CardUI card)
        {
            if (card.Parent != null)
            {
                return card.Parent as Canvas;
            }

            return null;
        }

        /// <summary>
        /// 获得卡片引用
        /// </summary>
        /// <param name="card_duelindex"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static Card getGard(string card_duelindex,string num)
        {
            Card card = null;

            //card = DuelOperate.getInstance().myself.deck.Main.Find(cd =>
            //{
            //    //cc为card_all中的对象，此处是lamda表达式写的委托
            //    return cd.duelindex.Equals(card_duelindex);
            //});
            //if (card == null)
            //{
            //    card = DuelOperate.getInstance().myself.deck.Extra.Find(cd =>
            //    {
            //        //cc为card_all中的对象，此处是lamda表达式写的委托
            //        return cd.duelindex.Equals(card_duelindex);
            //    });
            //}
            //if (card == null)
            //{
            //    card = DuelOperate.getInstance().opponent.deck.Main.Find(cd =>
            //    {
            //        //cc为card_all中的对象，此处是lamda表达式写的委托
            //        return cd.duelindex.Equals(card_duelindex);
            //    });
            //}
            //if (card == null)
            //{
            //    card = DuelOperate.getInstance().opponent.deck.Extra.Find(cd =>
            //    {
            //        //cc为card_all中的对象，此处是lamda表达式写的委托
            //        return cd.duelindex.Equals(card_duelindex);
            //    });
            //}

            //if (num.Equals("1"))
            //{
            //    card = DuelOperate.getInstance().myself.main.Find(cd =>
            //    {
            //        //cc为card_all中的对象，此处是lamda表达式写的委托
            //        return cd.duelindex.Equals(card_duelindex);
            //    });
            //    if (card == null)
            //    {
            //        card = DuelOperate.getInstance().myself.extra.Find(cd =>
            //        {
            //            //cc为card_all中的对象，此处是lamda表达式写的委托
            //            return cd.duelindex.Equals(card_duelindex);
            //        });
            //    }
            //}
            //else if (num.Equals("2"))
            //{
            //    card = DuelOperate.getInstance().opponent.main.Find(cd =>
            //    {
            //        //cc为card_all中的对象，此处是lamda表达式写的委托
            //        return cd.duelindex.Equals(card_duelindex);
            //    });
            //    if (card == null)
            //    {
            //        card = DuelOperate.getInstance().opponent.extra.Find(cd =>
            //        {
            //            //cc为card_all中的对象，此处是lamda表达式写的委托
            //            return cd.duelindex.Equals(card_duelindex);
            //        });
            //    }
            //}

            

         //   if (card == null)
	        //{
         //       MessageBox.Show("程序内部错误，卡片(" + card_duelindex + ")获取失败！");
	        //}
            
            return card;


        }
       



        /// <summary>
        /// 根据Canvas名字获得控件引用
        /// </summary>
        /// <param name="cv_aim"></param>
        /// <returns></returns>
        public static Canvas getCanvasByName(string cv_aim_, string field)
        {
            string cv_aim = cv_aim_;

            if (field.Equals("2"))
            {
                cv_aim = cv_aim.Remove(5, 1);
                cv_aim = cv_aim.Insert(5, "2");
                
            }
            if (field.Equals("1"))
            {
                cv_aim = cv_aim.Remove(5, 1);
                cv_aim = cv_aim.Insert(5, "1");
                
            }
            return mainwindow.FindName(cv_aim) as Canvas;
        }


        
        public static void EmptyJudge(string who, string where)
        {
           // if
        }
        
    }
}
