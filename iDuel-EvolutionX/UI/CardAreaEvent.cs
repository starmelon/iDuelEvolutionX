using iDuel_EvolutionX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace iDuel_EvolutionX.UI
{
    class CardAreaEvent
    {
        #region 墓地区控件事件

        /// <summary>
        /// 卡片进入墓地时，墓地控件的操作
        /// </summary>
        /// <param name="cv">墓地控件</param>
        /// <param name="card">卡片</param>
        public static void add2Graveyrad(MyCanvas cv, CardControl card)
        {
            card.reSetAtk();
            card.centerAtVerticalInParent();
            card.set2FrontAtk();
            card.clearSigns();
            card.ContextMenu = AllMenu.Instance.cm_graveyard;
        }

        /// <summary>
        /// 卡片离开墓地时，墓地控件操作
        /// </summary>
        /// <param name="cv">墓地控件</param>
        /// <param name="card">卡片</param>
        public static void romoveFromGraveyard(MyCanvas cv , CardControl card)
        {

        }  

        #endregion

        #region 除外区控件事件

        /// <summary>
        /// 卡片以覆盖方式进入手卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void add2Banish(MyCanvas cv, CardControl card)
        {
            card.reSetAtk();
            card.centerAtVerticalInParent();
            card.clearSigns();
            card.ContextMenu = AllMenu.Instance.cm_outside;
        }

        #endregion

        #region P卡区控件事件

        /// <summary>
        /// 卡片以顶层覆盖方式进入P卡区时，P卡区控件的操作
        /// </summary>
        /// <param name="cv">P卡区控件</param>
        /// <param name="card">卡片</param>
        public static void add2Pendulum(MyCanvas cv, CardControl card)
        {
            card.reSetAtk();
            card.centerAtVerticalInParent();
            card.ContextMenu = AllMenu.Instance.cm_pendulum;
        }

        /// <summary>
        /// 卡片离开手卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void removeFromPendulum(MyCanvas cv, CardControl card)
        {
            card.clearSigns();
        }

        #endregion

        #region 额外区控件事件

        /// <summary>
        /// 卡片以覆盖方式进入手卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void add2Extra(MyCanvas cv, CardControl card)
        {
            card.reSetAtk();
            card.set2BackAtk();
            card.centerAtVerticalInParent();
            card.clearSigns();
            
        }

        /// <summary>
        /// 卡片以覆盖方式进入手卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void insert2Extra(MyCanvas cv, CardControl card)
        {
            add2Extra(cv, card);
           
        }

        #endregion

        #region 卡组区控件事件

        /// <summary>
        /// 卡片以覆盖方式进入手卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void add2Deck(MyCanvas cv, CardControl card)
        {
            card.reSetAtk();
            card.set2BackAtk();
            card.centerAtVerticalInParent();
            card.clearSigns();
            card.ContextMenu = AllMenu.Instance.cm_deck;
        }

        /// <summary>
        /// 卡片以插入方式进入手卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void insert2Deck(MyCanvas cv, CardControl card)
        {
            add2Deck(cv, card);

        }

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
                card.ContextMenu = AllMenu.Instance.cm_monster;

            }
            else
            {
                card.ContextMenuOpening += (sender, e) =>
                {

                    card.ContextMenu.DataContext = card;
                };
                CardControl second = cv.Children[count - 2] as CardControl;

                second.reSetAtk();//当被叠放时要重置攻击力
                second.clearSigns();//当被叠放时要清除卡片指示物

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

            //MainWindow mainwin = Application.Current.MainWindow as MainWindow;

            #region 攻守显示绑定卡片

            bindingAtk(cv, card);

            #endregion

            //添加指示物
            showSigns(cv, card);
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
                card.ContextMenu = AllMenu.Instance.cm_monster;
            }

            if (cv.Children.Count > 1)
            {
                card.reSetAtk();
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
                        Canvas.SetTop(card, (cv.ActualHeight - card.ActualHeight) / 2.0);
                        Canvas.SetLeft(card, 0);
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
        public static void removeFromMonster(MyCanvas cv, CardControl card)
        {
            
            int count = cv.Children.Count;
            if (count == 0)
            {
                
                Binding bind = new Binding();
                BindingOperations.ClearBinding(cv.tb_atkDef, TextBlock.TextProperty);

                return;
            }
            CardControl top = cv.Children[count - 1] as CardControl;
            if (top.Status == Status.BACK_ATK || top.Status == Status.FRONT_ATK)
            {
                Service.CardOperate.sort_XYZ_atk(cv);
            }
            else
            {

            }
            bindingAtk(cv, top);//绑定顶层卡片攻击力
        }

        #endregion

        #region 魔陷区控件事件

        /// <summary>
        /// 卡片以覆盖方式进入怪物区时，怪物区控件的操作
        /// </summary>
        /// <param name="cv">怪物区控件</param>
        /// <param name="card">卡片</param>
        public static void add2MagicTrap(MyCanvas cv, CardControl card)
        {
            card.reSetAtk();
            int count = cv.Children.Count;
            if (count == 1)
            {
                card.centerAtVerticalInParent();
                card.ContextMenu = AllMenu.Instance.cm_magictrap;
                //添加指示物
                showSigns(cv, card);
            }
        }

        /// <summary>
        /// 卡片离开怪物区时，怪物区控件的操作
        /// </summary>
        /// <param name="cv">怪物区控件</param>
        /// <param name="card">卡片</param>
        public static void removeFromMagicTrap(MyCanvas cv, CardControl card)
        {
            int count = cv.Children.Count;
            if (count == 0)
            {
                return;
            }
            Service.CardOperate.sort_HandCard(cv);
        }

        #endregion

        #region 手卡区控件事件

        /// <summary>
        /// 卡片以覆盖方式进入手卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void add2Hand(MyCanvas cv, CardControl card)
        {
            card.clearSigns();
            card.reSetAtk();
            int count = cv.Children.Count;

            Service.CardOperate.sort_HandCard(cv);
            card.ContextMenu = AllMenu.Instance.cm_hand;
        }

        /// <summary>
        /// 卡片离开手卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void removeFromHand(MyCanvas cv, CardControl card)
        {
            int count = cv.Children.Count;
            if (count == 0)
            {
                return;
            }
            Service.CardOperate.sort_HandCard(cv);
        }

        #endregion


        /// <summary>
        /// 更新攻击防守显示控件的绑定源
        /// </summary>
        /// <param name="cv"></param>
        /// <param name="card"></param>
        private static void bindingAtk(MyCanvas cv, CardControl card)
        {
            Binding bind = new Binding();
            bind.Source = card;
            bind.Path = new PropertyPath("CurAtk");
            bind.NotifyOnTargetUpdated = true;
            cv.tb_atkDef.SetBinding(TextBlock.TextProperty, bind);
            cv.tb_atkDef.TargetUpdated += new EventHandler<DataTransferEventArgs>((o, e) =>
            {
                if (card.CurAtk == null)
                {
                    return;
                }
                if (!card.CurAtk.Equals(card.info.atk + "/" + card.info.def))
                {
                    cv.tb_atkDef.Style = Application.Current.TryFindResource("tb_AtkDefStyleChanged") as Style;
                }
                else
                {
                    cv.tb_atkDef.Style = Application.Current.TryFindResource("tb_AtkDefStyle") as Style;
                }

                //MessageBox.Show("修改了攻守");

            });
        }

        /// <summary>
        /// 显示指示物
        /// </summary>
        /// <param name="cv"></param>
        /// <param name="card"></param>
        private static void showSigns(MyCanvas cv, CardControl card)
        {
            foreach (SignTextBlock item in card.signs)
            {
                cv.signs.Children.Add(item);
            }
        }
    }
}
