﻿using iDuel_EvolutionX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace iDuel_EvolutionX.UI
{
    class CardAreaEvent
    {
        #region 墓地区控件事件

        /// <summary>
        /// 卡片离开墓地时，墓地控件操作
        /// </summary>
        /// <param name="cv">墓地控件</param>
        /// <param name="card">卡片</param>
        public static void romoveFromGraveyard(MyCanvas cv , CardControl card)
        {

        }

        /// <summary>
        /// 卡片进入墓地时，墓地控件的操作
        /// </summary>
        /// <param name="cv">墓地控件</param>
        /// <param name="card">卡片</param>
        public static void add2Graveyrad(MyCanvas cv, CardControl card)
        {
            card.centerAtVerticalInParent();
            card.set2FrontAtk();
            card.ContextMenu = AllMenu.cm_graveyard;
        }

        #endregion

        #region P卡区控件事件

        #endregion

        #region 额外区控件事件

        #endregion

        #region 卡组区控件事件

        #endregion

        #region 场地控件事件

        #endregion

        #region 怪物区控件事件

        /// <summary>
        /// 卡片以覆盖方式进入怪物区时，怪物区控件的操作
        /// </summary>
        /// <param name="cv">怪物区控件</param>
        /// <param name="card">卡片</param>
        public static void add2Monster(MyCanvas cv, CardControl card)
        {
            int count = cv.Children.Count;
            if (count == 1)
            {

                if (card.Status == Status.BACK_ATK || card.Status == Status.FRONT_ATK)
                {
                    card.centerAtVerticalInParent();
                }
                else
                {
                    card.centerAtHorizontalInParent();
                }
                card.ContextMenu = AllMenu.cm_monster;
            }
            else
            {
                CardControl second = cv.Children[count - 2] as CardControl;

                /*
                判断加入前最顶层的卡的状态，若是只要是存在背面或防守，则应先启动相关动画
                */

                CardAnimation.setTransformGroup(second);
                TransLibrary.StoryboardChain animator0 = new TransLibrary.StoryboardChain();

                if (second.Status == Status.BACK_DEF)
                {
                    
                    MyStoryboard msb1 = CardAnimation.ScaleX_120_Rotate(-90, 0, 150, 200);
                    msb1.card = second;
                    msb1.Completed += (object sender_, EventArgs e_) =>
                    {
                        //卡片切换为背面
                        msb1.card.set2FrontAtk();
                        //msb1.card.showImg();
                    };
                    animator0.Animates.Add(msb1);
                    MyStoryboard msb2 = CardAnimation.ScaleX_021(150);
                    animator0.Animates.Add(msb2); 
                }
                if (second.Status == Status.FRONT_DEF)
                {
                    MyStoryboard msb = CardAnimation.Rotate_D2A(150);
                    msb.card = second;
                    msb.Completed += (object sender_, EventArgs e_) =>
                    {
                        msb.card.set2FrontAtk();
                    };
                    animator0.Animates.Add(msb);
                }

                animator0.Begin(second);
                Canvas.SetTop(card, (cv.ActualHeight - card.ActualHeight) / 2.0);
                Canvas.SetLeft(card, cv.ActualWidth - card.ActualWidth);
                Service.CardOperate.sort_XYZ_atk(cv);
            }
            
        }

        /// <summary>
        /// 卡片以插入方式进入怪物区时，怪物区控件的操作
        /// </summary>
        /// <param name="cv">怪物区控件</param>
        /// <param name="card">卡片</param>
        public static void insert2Monster(MyCanvas cv, CardControl card)
        {
            if (cv.Children.Count == 0)
            {

                if (card.Status == Status.BACK_ATK || card.Status == Status.FRONT_ATK)
                {
                    card.centerAtVerticalInParent();
                }
                else
                {
                    card.centerAtHorizontalInParent();
                }
                card.ContextMenu = AllMenu.cm_monster;
            }

            if (cv.Children.Count > 1)
            {
                int count = cv.Children.Count;
                CardControl top = cv.Children[count - 1] as CardControl;
                if (top.Status == Status.FRONT_ATK || top.Status == Status.BACK_ATK)
                {
                    Canvas.SetTop(card, (cv.ActualHeight - card.ActualHeight) / 2.0);
                    Canvas.SetLeft(card, 0);
                    Service.CardOperate.sort_XYZ_atk(cv);
                }
                else
                {
                    if (count == 2)
                    {
                        card.centerAtVerticalInParent();
                    }
                    else
                    {
                        Service.CardOperate.sort_XYZ_def2(cv);
                    }
                    
                }
                
            }
            

            
        }

        /// <summary>
        /// 卡片离开怪物区时，怪物区控件的操作
        /// </summary>
        /// <param name="cv">怪物区控件</param>
        /// <param name="card">卡片</param>
        public static void romoveFromMonster(MyCanvas cv, CardControl card)
        {
            int count = cv.Children.Count;
            CardControl top = cv.Children[count - 1] as CardControl;
            if (top.Status == Status.BACK_ATK || top.Status == Status.FRONT_ATK)
            {
                Service.CardOperate.sort_XYZ_atk(cv);
            }
            else
            {

            }
        }

        #endregion

        #region 魔陷区控件事件

        #endregion
    }
}
