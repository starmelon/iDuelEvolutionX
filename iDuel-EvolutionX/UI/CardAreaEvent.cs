using iDuel_EvolutionX.EventJson;
using iDuel_EvolutionX.Model;
using iDuel_EvolutionX.Service;
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

namespace iDuel_EvolutionX.UI
{
    class CardAreaEvent
    {
        #region 己方区域

        #region 墓地区控件事件

        /// <summary>
        /// 卡片进入墓地时，墓地控件的操作
        /// </summary>
        /// <param name="cv">墓地控件</param>
        /// <param name="card">卡片</param>
        public static void add2Graveyrad(MyCanvas cv, CardUI card)
        {

            card.reSetAtk();
            card.centerAtVerticalInParent();
            card.clearSigns();
            if (card.Status == Status.BACK_ATK)
            {
                CardAnimation.turn(card);
            }
            else
            {
                card.set2FrontAtk();
            }

            #region 指令发送

            MoveInfo moveInfo = new MoveInfo();
            moveInfo.cardID = CardOperate.getCardID(card);
            moveInfo.isAdd = true;
            moveInfo.aimArea = cv.area;
            moveInfo.aimStatus = Status.FRONT_ATK;
            String contentJson = JsonConvert.SerializeObject(moveInfo);

            BaseJson bj = new BaseJson();
            bj.guid = DuelOperate.getInstance().myself.userindex;
            bj.cid = "";
            bj.action = ActionCommand.CARD_MOVE;
            bj.json = contentJson;
            String json = JsonConvert.SerializeObject(bj);
            DuelOperate.getInstance().sendMsg(json);

            #endregion
            //card.set2FrontAtk();

            card.ContextMenu = AllMenu.Instance.cm_graveyard;
        }

        /// <summary>
        /// 卡片离开墓地时，墓地控件操作
        /// </summary>
        /// <param name="cv">墓地控件</param>
        /// <param name="card">卡片</param>
        public static void romoveFromGraveyard(MyCanvas cv , CardUI card)
        {

        }  

        #endregion

        #region 除外区控件事件

        /// <summary>
        /// 卡片以覆盖方式进入手卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void add2Banish(MyCanvas cv, CardUI card)
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
        public static void add2Pendulum(MyCanvas cv, CardUI card)
        {
            card.reSetAtk();
            card.centerAtVerticalInParent();
            card.ContextMenu = AllMenu.Instance.cm_pendulum;
        }

        /// <summary>
        /// 卡片离开P卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void removeFromPendulum(MyCanvas cv, CardUI card)
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
        public static void add2Extra(MyCanvas cv, CardUI card)
        {
            card.reSetAtk();
            if (card.info.CardDType.Contains("灵摆"))
            {
                card.set2FrontAtk();
            }
            else
            {
                card.set2BackAtk();
            }
            card.centerAtVerticalInParent();
            card.clearSigns();
            
        }

        /// <summary>
        /// 卡片以覆盖方式进入手卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void insert2Extra(MyCanvas cv, CardUI card)
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
        public static void add2Deck(MyCanvas cv, CardUI card)
        {
            card.reSetAtk();
            card.centerAtVerticalInParent();
            card.clearSigns();
            if (card.Status == Status.FRONT_ATK)
            {
                CardAnimation.turn2Back(card);
            }
            else
            {
                card.set2BackAtk();
            }
            card.ContextMenu = AllMenu.Instance.cm_deck;
        }

        /// <summary>
        /// 卡片以插入方式进入手卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void insert2Deck(MyCanvas cv, CardUI card)
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
        public static void add2Monster(MyCanvas cv, CardUI card)
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
                CardUI second = cv.Children[count - 2] as CardUI;

                second.reSetAtk();//当被叠放时要重置攻击力
                second.clearSigns();//当被叠放时要清除卡片指示物

                /*
                判断加入前最顶层的卡的状态，若是只要是存在背面或防守，则应先启动相关动画
                */

                //CardAnimation.setTransformGroup(second);
                TransLibrary.StoryboardChain animator0 = new TransLibrary.StoryboardChain();

                if (second.Status == Status.BACK_DEF)
                {

                    //MyStoryboard msb1 = CardAnimation.ScaleX_120_Rotate(-90, 0, 150, 200);
                    MyStoryboard msb1 = CardAnimation.scalX_120_rotate_9020();
                    msb1.card = second;
                    msb1.Completed += (object sender_, EventArgs e_) =>
                    {
                        //卡片切换为背面
                        msb1.card.set2FrontAtk();
                        //msb1.card.showImg();
                    };
                    animator0.Animates.Add(msb1);
                    MyStoryboard msb2 = CardAnimation.scalX_021();
                    animator0.Animates.Add(msb2);
                }
                if (second.Status == Status.FRONT_DEF)
                {
                    MyStoryboard msb = CardAnimation.Rotate_D2A();
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
                card.set2FrontAtk();
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
        public static void insert2Monster(MyCanvas cv, CardUI card)
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
                card.set2FrontAtk();
                int count = cv.Children.Count;
                CardUI top = cv.Children[count - 1] as CardUI;
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
        public static void removeFromMonster(MyCanvas cv, CardUI card)
        {
            
            int count = cv.Children.Count;
            if (count == 0)
            {
                
                Binding bind = new Binding();
                BindingOperations.ClearBinding(cv.tb_atkDef, TextBlock.TextProperty);
                cv.tb_atkDef.IsHitTestVisible = false;
                return;
            }
            CardUI top = cv.Children[count - 1] as CardUI;
            if (top.Status == Status.BACK_ATK || top.Status == Status.FRONT_ATK)
            {
                Service.CardOperate.sort_XYZ_atk(cv);
            }
            else
            {
                Service.CardOperate.sort_XYZ_def(cv);
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
        public static void add2MagicTrap(MyCanvas cv, CardUI card)
        {
            card.reSetAtk();
            switch (card.Status)
            {
                case Status.FRONT_ATK:
                case Status.FRONT_DEF:
                    card.set2FrontAtk();
                    break;
                case Status.BACK_ATK:
                case Status.BACK_DEF:
                    card.set2BackAtk();
                    break;
                default:
                    break;
            }
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
        public static void removeFromMagicTrap(MyCanvas cv, CardUI card)
        {
            int count = cv.Children.Count;
            if (count == 0)
            {
                return;
            }
            
        }

        #endregion

        #region 手卡区控件事件

        /// <summary>
        /// 卡片以覆盖方式进入手卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void add2Hand(MyCanvas cv, CardUI card)
        {
            card.clearSigns();
            card.reSetAtk();
            card.set2FrontAtk();

            card.CurLocation = new Location(cv.area, cv.Children.IndexOf(card));
            card.outputChange();

            int count = cv.Children.Count;

            Service.CardOperate.sort_HandCard(cv);
            card.ContextMenu = AllMenu.Instance.cm_hand;
        }

        /// <summary>
        /// 卡片离开手卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void removeFromHand(MyCanvas cv, CardUI card)
        {
            int count = cv.Children.Count;
            if (count == 0)
            {
                return;
            }
            Service.CardOperate.sort_HandCard(cv);
        }

        #endregion


        #endregion

        #region 敌方区域

        #region
        /// <summary>
        /// 卡片进入墓地时，墓地控件的操作
        /// </summary>
        /// <param name="cv">墓地控件</param>
        /// <param name="card">卡片</param>
        public static void add2OPBattle(MyCanvas cv, CardUI card)
        {

            if ((card.StatusLast == Status.BACK_DEF || card.StatusLast == Status.BACK_ATK) && card.Status == Status.FRONT_ATK)
            {
                CardAnimation.turn(card);
            }

            if ((card.StatusLast == Status.FRONT_ATK || card.StatusLast == Status.FRONT_DEF) && card.Status == Status.BACK_ATK)
            {
                CardAnimation.turn(card);
            }

        }
        #endregion

        #region 墓地区控件事件

        /// <summary>
        /// 卡片进入墓地时，墓地控件的操作
        /// </summary>
        /// <param name="cv">墓地控件</param>
        /// <param name="card">卡片</param>
        public static void add2GraveyradOP(MyCanvas cv, CardUI card)
        {

            card.reSetAtk();
            card.centerAtVerticalInParent();
            card.clearSigns();
            if ((card.StatusLast == Status.BACK_DEF || card.StatusLast == Status.BACK_ATK ) && card.Status == Status.FRONT_ATK)
            {
                CardAnimation.turn(card);
            }

            //card.set2FrontAtk();

            
        }

        /// <summary>
        /// 卡片离开墓地时，墓地控件操作
        /// </summary>
        /// <param name="cv">墓地控件</param>
        /// <param name="card">卡片</param>
        public static void romoveFromGraveyardOP(MyCanvas cv, CardUI card)
        {

        }

        #endregion

        #region 除外区控件事件

        /// <summary>
        /// 卡片以覆盖方式进入手卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void add2BanishOP(MyCanvas cv, CardUI card)
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
        public static void add2PendulumOP(MyCanvas cv, CardUI card)
        {
            card.reSetAtk();
            card.centerAtVerticalInParent();
            card.ContextMenu = AllMenu.Instance.cm_pendulum;
        }

        /// <summary>
        /// 卡片离开P卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void removeFromPendulumOP(MyCanvas cv, CardUI card)
        {
            card.clearSigns();
        }

        #endregion

        #region 卡组区控件事件

        /// <summary>
        /// 卡片以覆盖方式进入手卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void add2DeckOP(MyCanvas cv, CardUI card)
        {
            card.reSetAtk();
            card.centerAtVerticalInParent();
            card.clearSigns();
            if (card.Status == Status.FRONT_ATK)
            {
                CardAnimation.turn2Back(card);
            }
            else
            {
                card.set2BackAtk();
            }
            card.ContextMenu = AllMenu.Instance.cm_deck;
        }

        /// <summary>
        /// 卡片以插入方式进入手卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void insert2DeckOP(MyCanvas cv, CardUI card)
        {
            add2Deck(cv, card);

        }

        #endregion

        #region 额外区控件事件

        /// <summary>
        /// 卡片以覆盖方式进入手卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void add2ExtraOP(MyCanvas cv, CardUI card)
        {
            card.reSetAtk();
            if (card.info.CardDType.Contains("灵摆"))
            {
                card.set2FrontAtk();
            }
            else
            {
                card.set2BackAtk();
            }
            card.centerAtVerticalInParent();
            card.clearSigns();

        }

        /// <summary>
        /// 卡片以覆盖方式进入手卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void insert2ExtraOP(MyCanvas cv, CardUI card)
        {
            add2Extra(cv, card);

        }

        #endregion

        #region 怪物区控件事件

        /// <summary>
        /// 卡片以覆盖方式进入怪物区时，怪物区控件的操作
        /// </summary>
        /// <param name="mcv">怪物区控件</param>
        /// <param name="card">卡片</param>
        public static void add2MonsterOP(MyCanvas mcv, CardUI card)
        {


            int count = mcv.Children.Count;
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


                if (card.Status == Status.FRONT_ATK || card.Status == Status.FRONT_DEF)
                {
                    MainWindow mainwin = Application.Current.MainWindow as MainWindow;
                    switch (card.info.sCardType)
                    {


                        case "同调怪兽":
                            {
                                Point summon2 = mcv.TranslatePoint(new Point(0.5, 0.5), mainwin.OpBattle);
                                Canvas.SetLeft(mainwin.img_synchro_op, summon2.X - ((mainwin.img_synchro_op.Width - mcv.ActualWidth) / 2));
                                Canvas.SetTop(mainwin.img_synchro_op, summon2.Y - ((mainwin.img_synchro_op.Height - mcv.ActualHeight) / 2));
                                CardAnimation.Rotate_Scale_FadeInAndOut(mainwin.img_synchro_op);
                            }
                            break;
                        default:
                            {

                                Point summon2 = mcv.TranslatePoint(new Point(0.5, 0.5), mainwin.OpBattle);
                                Canvas.SetLeft(mainwin.img_summon_op, summon2.X - ((mainwin.img_summon_op.Width - mcv.ActualWidth) / 2));
                                Canvas.SetTop(mainwin.img_summon_op, summon2.Y - ((mainwin.img_summon_op.Height - mcv.ActualHeight) / 2));
                                CardAnimation.Rotate_Scale_FadeInAndOut(mainwin.img_summon_op);
                            }
                            break;
                    }
                }
                
            }
            else
            {
                Canvas.SetLeft(card, mcv.ActualWidth - card.Width);
                Canvas.SetTop(card, (mcv.ActualHeight - card.Height) / 2);
                card.ContextMenuOpening += (sender, e) =>
                {

                    card.ContextMenu.DataContext = card;
                };
                CardUI second = mcv.Children[count - 2] as CardUI;

                second.reSetAtk();//当被叠放时要重置攻击力
                second.clearSigns();//当被叠放时要清除卡片指示物

                /*
                判断加入前最顶层的卡的状态，若是只要是存在背面或防守，则应先启动相关动画
                */

                //CardAnimation.setTransformGroup(second);
                TransLibrary.StoryboardChain animator0 = new TransLibrary.StoryboardChain();

                if (second.Status == Status.BACK_DEF)
                {

                    //MyStoryboard msb1 = CardAnimation.ScaleX_120_Rotate(-90, 0, 150, 200);
                    MyStoryboard msb1 = CardAnimation.scalX_120_rotate_9020();
                    msb1.card = second;
                    msb1.Completed += (object sender_, EventArgs e_) =>
                    {
                        //卡片切换为背面
                        msb1.card.set2FrontAtk();
                        //msb1.card.showImg();
                    };
                    animator0.Animates.Add(msb1);
                    MyStoryboard msb2 = CardAnimation.scalX_021();
                    animator0.Animates.Add(msb2);
                }
                if (second.Status == Status.FRONT_DEF)
                {
                    MyStoryboard msb = CardAnimation.Rotate_D2A();
                    msb.card = second;
                    msb.Completed += (object sender_, EventArgs e_) =>
                    {
                        msb.card.set2FrontAtk();
                    };
                    animator0.Animates.Add(msb);
                }

                animator0.Begin(second);
                Canvas.SetTop(card, (mcv.ActualHeight - card.ActualHeight) / 2.0);
                Canvas.SetLeft(card, mcv.ActualWidth - card.ActualWidth);
                Service.CardOperate.sort_XYZ_atk(mcv);

                MainWindow mainwin = Application.Current.MainWindow as MainWindow;
                Point summon2 = mcv.TranslatePoint(new Point(0.5, 0.5), mainwin.OpBattle);
                Canvas.SetLeft(mainwin.img_overlay_op, summon2.X - ((mainwin.img_overlay_op.Width - mcv.ActualWidth) / 2));
                Canvas.SetTop(mainwin.img_overlay_op, summon2.Y - ((mainwin.img_overlay_op.Height - mcv.ActualHeight) / 2));
                CardAnimation.Rotate_Scale_FadeInAndOut(mainwin.img_overlay_op);
            }

            //MainWindow mainwin = Application.Current.MainWindow as MainWindow;

            #region 攻守显示绑定卡片

            if (card.Status != Status.BACK_ATK && card.Status != Status.BACK_DEF)
            {
                bindingAtk(mcv, card);
            }
            

            #endregion

            //添加指示物
            showSigns(mcv, card);
        }

        public static void insert2MonsterOP(MyCanvas mcv, CardUI card)
        {
            if (mcv.Children.Count == 0)
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

            if (mcv.Children.Count > 1)
            {
                card.reSetAtk();
                int count = mcv.Children.Count;
                CardUI top = mcv.Children[count - 1] as CardUI;
                if (top.Status == Status.FRONT_ATK || top.Status == Status.BACK_ATK)
                {
                    //Canvas.SetTop(card, (cv.ActualHeight - card.ActualHeight) / 2.0);
                    //Canvas.SetLeft(card, 0);
                    Canvas.SetLeft(card, 0 - card.Width);
                    Canvas.SetTop(card, (mcv.ActualHeight - card.Height) / 2);
                    Service.CardOperate.sort_XYZ_atk(mcv);
                }
                else
                {
                    if (count == 2)
                    {
                        card.centerAtVerticalInParent();
                    }
                    else
                    {
                        Canvas.SetTop(card, (mcv.ActualHeight - card.ActualHeight) / 2.0);
                        Canvas.SetLeft(card, 0);
                        Service.CardOperate.sort_XYZ_def2(mcv);
                    }

                }

            }



        }

        /// <summary>
        /// 卡片离开怪物区时，怪物区控件的操作
        /// </summary>
        /// <param name="cv">怪物区控件</param>
        /// <param name="card">卡片</param>
        public static void removeFromMonsterOP(MyCanvas cv, CardUI card)
        {

            int count = cv.Children.Count;
            if (count == 0)
            {

                Binding bind = new Binding();
                BindingOperations.ClearBinding(cv.tb_atkDef, TextBlock.TextProperty);
                cv.tb_atkDef.IsHitTestVisible = false;
                return;
            }
            CardUI top = cv.Children[count - 1] as CardUI;
            if (top.Status == Status.BACK_ATK || top.Status == Status.FRONT_ATK)
            {
                Service.CardOperate.sort_XYZ_atk(cv);
            }
            else
            {
                Service.CardOperate.sort_XYZ_def(cv);
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
        public static void add2MagicTrapOP(MyCanvas cv, CardUI card)
        {
            card.reSetAtk();
            switch (card.Status)
            {
                case Status.FRONT_ATK:
                case Status.FRONT_DEF:
                    card.set2FrontAtk();
                    break;
                case Status.BACK_ATK:
                case Status.BACK_DEF:
                    card.set2BackAtk();
                    break;
                default:
                    break;
            }
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
        public static void removeFromMagicTrapOP(MyCanvas cv, CardUI card)
        {


        }

        #endregion

        #region 手卡区控件事件

        /// <summary>
        /// 卡片以覆盖方式进入手卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void add2HandOP(MyCanvas cv, CardUI card)
        {
            card.clearSigns();
            card.reSetAtk();
            card.set2BackAtk();

            Service.CardOperate.sort_HandCard(cv);

        }

        /// <summary>
        /// 卡片离开手卡区时，手卡区控件的操作
        /// </summary>
        /// <param name="cv">手卡区控件</param>
        /// <param name="card">卡片</param>
        public static void removeFromHandOP(MyCanvas cv, CardUI card)
        {
            int count = cv.Children.Count;
            if (count == 0)
            {
                return;
            }
            Service.CardOperate.sort_HandCard(cv);
        }

        #endregion

        #endregion

        #region 公用函数

        /// <summary>
        /// 更新攻击防守显示控件的绑定源
        /// </summary>
        /// <param name="cv"></param>
        /// <param name="card"></param>
        public static void bindingAtk(MyCanvas cv, CardUI card)
        {
            if (cv.tb_atkDef == null)
            {
                return;
            }
            Binding bind = new Binding();
            bind.Source = card;
            bind.Path = new PropertyPath("CurAtk");
            bind.NotifyOnTargetUpdated = true;
            cv.tb_atkDef.IsHitTestVisible = true;
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
        private static void showSigns(MyCanvas cv, CardUI card)
        {
            foreach (SignTextBlock item in card.signs)
            {
                cv.signs.Children.Add(item);
            }
        }

        #endregion
    }
}
