using iDuel_EvolutionX.Model;
using iDuel_EvolutionX.Net;
using iDuel_EvolutionX.UI;
using NBX3.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace iDuel_EvolutionX.Service
{
    class OpponentOperate
    {
        public static MainWindow mainwindow;


        public static void ActionAnalyze(string action,bool send)
        {
            if (send)
            {
                DuelOperate.getInstance().sendMsg(action, "开发中");
            }
            else
            {
                //#region 执行动作

                ////命令格式：common=参数1,参数2....

                ////移动：
                ////参数1:卡片移动前所在控件的num
                ////参数2:卡片移动前在控件中的num
                ////参数3:目标控件的

                ////抽卡：
                ////参数1:抽取的数量
                ////参数2:卡片移动前所在控件中的num
                ////参数3:目标控件的num

                //string[] actions = action.Split(new char[] { '=' , ',' }, StringSplitOptions.None);

                //DuelOperate duelOperate = DuelOperate.getInstance();
                ////foreach (string str in actions)
                ////{
                ////    Console.WriteLine(str);
                ////}
                ////
                ////<参数1,参数2,参数3,参数4,参数5>



                //switch (actions[4])
                //{
                //    #region <-- 攻击 -->
                //    case "Atk":
                //        {
                //            string card_duelindex = actions[5];
                //            Card Move_card = Base.getGard(card_duelindex, "2");
                //            Canvas Move_cv = Base.getParerent(Move_card);
                //            Canvas Move_cv_aim = Base.getCanvasByName(actions[6], "1");

                //            Atk(Move_cv, Move_cv_aim);
                //        }
                //        break;

                //    #endregion

                //    #region <-- 接入 -->

                //    case "Connect":
                //        connect(actions[1]);
                //        break;

                //    #endregion

                //    #region <-- 开局 -->

                //    case "start":
                //        {
                //            #region

                //            string duelistname = actions[1];
                //            startgame(actions[0], actions[5], actions[6]);

                //            #endregion
                //        }
                //        break;

                //    #endregion

                //    #region <-- 先攻 -->

                //    case "FirstAtk":
                //        {
                //            List<Card> cards = new List<Card>();
                //            for (int i = 5; i < actions.Length; i++)
                //            {
                //                Card card = Base.getGard(actions[i], "2");
                //                cards.Add(card);
                //            }
                //            FirstAtk(5, cards, mainwindow.card_2_Deck, mainwindow.card_2_hand);

                //        }
                //        break;

                //    #endregion

                //    #region <-- 抽卡 -->

                //    case "Draw":
                //        {
                //            int draw_num = int.Parse(actions[5]);

                //            List<Card> cards = new List<Card>();
                //            for (int i = 6; i < actions.Length; i++)
                //            {
                //                Card card = Base.getGard(actions[i], "2");
                //                cards.Add(card);
                //            }
                //            Draw(draw_num, cards, mainwindow.card_2_Deck, mainwindow.card_2_hand); 

                //        }
                                      
                //        break;

                //    #endregion

                //    #region <-- 卡片移动 -->

                //    case "Move":
                //        {
                //            //参数1：召唤的卡片序号
                //            //参数2：目标控件名字
                //            //参数3：召唤还是素材添加
                //            //参数4：需要变更状态的卡片序号
                //            string card_duelindex = actions[5];
                //            Card Summon_card = Base.getGard(actions[5], "2");
                //            //Canvas Summon_cv = Base.getParerent(Summon_card);
                //            Canvas Summon_cv_aim = Base.getCanvasByName(actions[6], "2");
                //            bool add = actions[7].Equals("1") ? true : false;
                //            Card statechange_card = null;
                //            if (!actions[8].Equals(""))
                //            {
                //                statechange_card = Base.getGard(actions[8], "2");
                //            }
                //            //Summon(Summon_cv,Summon_card, Summon_cv_aim);
                //            Move2(Summon_card, Summon_cv_aim, add, statechange_card);



                //            ////参数1：卡片序号
                //            ////参数2：目标控件名字
                //            //string card_duelindex = actions[5];
                //            //Card Move_card = Base.getGard(card_duelindex, "2");
                //            //Canvas Move_cv = Base.getParerent(Move_card);
                //            //Canvas Move_cv_aim = Base.getCanvasByName(actions[6],"2");
          
                //            //int cv_num = mainwindow.MySpace.Children.IndexOf(Move_cv);
                //            //Move(Move_cv, Move_card, Move_cv_aim, cv_num);

                //        }
                //        break;

                //    #endregion

                //    #region <-- 卡片消除 -->

                //    case "Disappear":
                //        {
                //            string card_duelindex = actions[5];
                //            Card Disappear_card = Base.getGard(card_duelindex, "2");
                //            Canvas Disappear_cv = Base.getParerent(Disappear_card);
                //            Canvas Disappear_cv_aim = Base.getCanvasByName(actions[6], "2");

                         
                //            Disappear(Disappear_card, Disappear_cv_aim, false);

                //        }
                //        break;

                //    #endregion

                //    #region <-- 盖放卡片 -->

                //    case "Cover":
                //        {
                //            #region
                //            //盖放怪物
                //            //参数1：卡片序号
                //            //参数2：目标控件名字
                //            string card_duelindex = actions[5];
                //            Card Cover_card = Base.getGard(card_duelindex, "2");
                //            Canvas Cover_cv = Base.getParerent(Cover_card);
                //            Canvas Cover_cv_aim = Base.getCanvasByName(actions[6],"2");

                //            //int Cover_cv_num = int.Parse(actions[1]);
                //            //int Cover_card_num = int.Parse(actions[2]);
                //            //int Cover_cv_aim_num = int.Parse(actions[3]);

                //            //Canvas Cover_cv = mainwindow.MySpace.Children[Cover_cv_num] as Canvas;
                //            //Card Cover_card = Cover_cv.Children[Cover_card_num] as Card;
                //            //Canvas Cover_cv_aim = mainwindow.MySpace.Children[Cover_cv_aim_num] as Canvas;

                //            Cover(Cover_cv, Cover_card, Cover_cv_aim);

                //            #endregion
                //        }
                //        break;

                //    #endregion

                //    #region <-- 召唤卡片 -->

                //    case "Summon":
                //        {
                //            #region
                //            //参数1：召唤的卡片序号
                //            //参数2：目标控件名字
                //            //参数3：召唤还是素材添加
                //            //参数4：需要变更状态的卡片序号
                //            string card_duelindex = actions[5];
                //            Card Summon_card = Base.getGard(actions[5], "2");
                //            //Canvas Summon_cv = Base.getParerent(Summon_card);
                //            Canvas Summon_cv_aim = Base.getCanvasByName(actions[6],"2");
                //            bool add = actions[7].Equals("1") ? true : false;
                //            Card statechange_card = null;
                //            if (!actions[8].Equals(""))
                //            {
                //                statechange_card = Base.getGard(actions[8], "2");
                //            } 
                //            //Summon(Summon_cv,Summon_card, Summon_cv_aim);
                //            Summon2(Summon_card, Summon_cv_aim, add, statechange_card);
                //            #endregion
                //        }
                //        break;

                //    #endregion

                //    #region <-- 盖放卡片2 -->

                //    case "Cover2":
                //        {
                //            #region


                //            Card Cover2_card = Base.getGard(actions[5], "2");
                //            Canvas Cover2_cv = Base.getParerent(Cover2_card);

                //            Canvas Cover2_cv_aim = Base.getCanvasByName(actions[6], "2");

                //            Cover2(Cover2_cv, Cover2_card, Cover2_cv_aim);

                //            #endregion
                //        }
                //        break;

                //    #endregion

                //    #region <-- 回到卡组 -->

                //    case "Back2Deck":
                //        {
                //            string card_duelindex = actions[5];
                //            Card Back_card = Base.getGard(card_duelindex, "2");
                //            CardBackTo(Back_card, mainwindow.card_2_Deck);
                //        }
                //        break;

                //    #endregion

                //    #region <-- 回到手卡 -->

                //    case "Back2Hand":
                //        {

                //            #region

                //            string card_duelindex = actions[5];
                //            Card Back_card = Base.getGard(card_duelindex, "2");
                //            CardBackTo(Back_card, mainwindow.card_2_hand);

                //            #endregion
                //        }
                //        break;

                //    #endregion

                //    #region <-- 回到额外 -->

                //    case "Back2Extra":
                //        {

                //            #region

                //            string card_duelindex = actions[5];
                //            Card Back_card = Base.getGard(card_duelindex, "2");
                //            CardBackTo(Back_card, mainwindow.card_2_Extra);

                //            #endregion
                //        }
                //        break;

                //    #endregion

                //    #region <-- 状态改变 -->

                //    case "FormChange":
                //        {
                //            #region

                //            string card_duelindex = actions[5];                        
                //            Card FormChange_card = Base.getGard(card_duelindex,"2");
                //            Canvas FormChange_cv = Base.getParerent(FormChange_card);

                //            Def_or_Atk(FormChange_cv, FormChange_card);

                //            #endregion
                //        }
                //        break;

                //    #endregion

                //    #region <-- 改变状态2 -->

                //    case "FormChange2":
                //        {
                //            #region

                //            string card_duelindex = actions[5];
                //            Card FormChange2_card = Base.getGard(card_duelindex, "2");
                       
                //            Back_or_Front(FormChange2_card);

                //            #endregion
                //        }

                //        break;

                //    #endregion

                //    #region <-- 改变状态3 -->

                //    case "FormChange3":
                //        {
                //            #region
                //            string card_duelindex = actions[5];
                //            Card FormChange3_card = Base.getGard(card_duelindex, "2");
                //            Canvas FormChange3_cv = Base.getParerent(FormChange3_card);                          

                //            Atk2Back(FormChange3_cv, FormChange3_card);
                //            #endregion
                //        }
                //        break;

                //    #endregion

                //    #region <-- 返回卡组底部 -->

                //    case "Top2bottom":
                //        Top2bottom();
                //        break;

                //    #endregion

                //    #region <-- 送去墓地 -->

                //    case "Send2Graveyard":
                //        {
                //            string card_duelindex = actions[5];
                //            Card card = Base.getGard(card_duelindex, "2");
                //            Canvas cv_aim = Base.getCanvasByName(actions[6], "2");
                //            SendTo(card, cv_aim);
 
                //        }
                //        break;

                //    #endregion

                //    #region <-- 送入对手墓地 -->

                //    case "Send2OpGraveyard":
                //        {
                //            #region

                //            string card_duelindex = actions[5];
                //            Card Send2OpGraveyard_card = Base.getGard(card_duelindex, "2");
                //            Canvas Send2OpGraveyard_cv = Base.getParerent(Send2OpGraveyard_card);                          
                //            Send2OpGraveyard(Send2OpGraveyard_cv, Send2OpGraveyard_card);

                //            #endregion
                //        }
                //        break;

                //    #endregion

                //    #region <-- 转移控制权 -->

                //    case "ControlChange":
                //        {


                //            string card_duelindex = actions[5];
                //            Card ControlChange_card = new Card();
                //            //if (actions[5].Equals("2"))
                //            //{
                //            //   ControlChange_card = Base.getGard(card_duelindex, "2");
                //            //}
                //            //else if (actions[5].Equals("1"))
                //            //{
                //            //    ControlChange_card = Base.getGard(card_duelindex, "1");
                //            //}

                //            ControlChange_card = Base.getGard(card_duelindex, "1");

                //            Canvas ControlChange_cv = Base.getCanvasByName(actions[6], "1");



                //            ControlChange(ControlChange_card, ControlChange_cv);
                //        }
                //        break;

                //    #endregion

                //    #region <-- 生命值改变 -->

                //    case "LifeChange":
                //        {
                //            #region

                //            double remaining_life = double.Parse(actions[5]);
                //            LifeChange(remaining_life);

                //            #endregion
                //        }
                //        break;

                //    #endregion
                 
                //    #region <-- 多重卡片操作 -->

                //    case "Cards":
                //        {
                //            cardsHandle(actions[5], actions[6]);
                //        }
                //        break;

                //    #endregion
                
                //    #region <-- 清空指示物 -->

                //    case "Clearsign":
                //        {
                          
                //            StackPanel sp = mainwindow.FindName(actions[5].ToString().Replace("sign_1", "sign_2")) as StackPanel;
                //            Clearsign(sp);
                        
                //        }
                //        break;

                //    #endregion

                //    #region <-- 阶段改变 -->

                //    case "ChangePhase":
                //        {
                            
                //            Rectangle rta = mainwindow.FindName(actions[5]) as Rectangle;
                //            int press = Grid.GetColumn(rta);
                //            ChangePhase(rta, press);
                            
                //        }
                //        break;

                //    #endregion

                //    #region <-- 选择对象 -->

                //    case "SelectObject":
                //        {
                            
                //            FrameworkElement origin = mainwindow.FindName(actions[5].Replace("card_1","card_2").Replace("card", "bd")) as FrameworkElement;
                //            FrameworkElement aim = mainwindow.FindName(actions[6].Replace("card_2","card_1").Replace("card", "bd")) as FrameworkElement;

                //            SelectObject(origin, aim);
                            
                //        }
                //        break;

                //    #endregion

                //    #region <-- 效果发动 -->

                //    case "StartEffect":
                //        {
                //            if (actions[0].Equals("2"))
                //            {
                //                FrameworkElement origin = mainwindow.FindName(actions[5].Replace("card_1", "card_2").Replace("card", "bd")) as FrameworkElement;
                //                StartEffect(origin);
                //            }
                //        }
                //        break;

                //    #endregion

                //    #region <-- 放置指示物 -->

                //    case "Setsign":
                //        {                        
                //            StackPanel sp = mainwindow.FindName(actions[5].ToString().Replace("sign_1", "sign_2")) as StackPanel;
                //            Setsign(sp, actions[6]);                          
                //        }
                //        break;

                //    #endregion

                //    #region <-- 改变指示物个数 -->

                //    case "ChangeSign":
                //        {
                            
                //            StackPanel sp = mainwindow.FindName(actions[5].Replace("sign_1", "sign_2")) as StackPanel;
                //            foreach (Grid gd in sp.Children)
                //            {
                //                if (gd.Name.Equals(actions[6]))
                //                {
                //                    ChangeSign(gd.Children[1] as TextBlock, actions[7]);
                //                    break;
                //                }
                //            }
                         
                //        }
                //        break;

                //    #endregion

                //    #region <-- 切洗区域 -->

                //    case "Shuffle_zone":
                //        {

                //            //第一个参数是要集合的点
                //            Canvas aim = mainwindow.battle_zone_middle.FindName(actions[5].Replace("card_1", "card_2")) as Canvas;

                //            Dictionary<Card, Canvas> di = new Dictionary<Card, Canvas>();
                //            for (int i = 6; i < actions.Length ; i+=2)
			             //   {
                //                Card card = Base.getGard(actions[i], "2");
                //                Canvas aim2 = mainwindow.battle_zone_middle.FindName(actions[i+1].Replace("card_1","card_2")) as Canvas;
                //                di.Add(card, aim2);
			             //   }
                //            Shuffle_zone(aim,di);

                //        }
                //        break;

                //    #endregion

                //    #region <-- 选择区域 -->

                //    case "ChooseZone":
                //        {

                //            CheckBox cb = mainwindow.battle_zone_middle.FindName(actions[5].Replace("cb_1","cb_2")) as CheckBox;
                //            bool state = true;
                //            if (actions[6].Equals("0")) state = false;
                //            ChooseZone(cb, state);

                //        }
                //        break;

                //    #endregion

                //    #region <-- 清除区域选择 -->

                //    case "ClearChooseZone":
                //        {
                //            ClearChooseZone();
                //        }
                //        break;

                //    #endregion

                //    default:

                //        break;
                    
                //}

                //#endregion
            }
            
            

            
 
        }

        #region <-- 清除选择卡位 -->

        private static void ClearChooseZone()
        {
            CheckBox cb = new CheckBox();
            for (int i = 1; i < 11; i++)
            {
                cb = mainwindow.battle_zone_middle.FindName("cb_2_" + i) as CheckBox;
                cb.Checked -= DuelEvent.cb_Checked;
                cb.Unchecked -= DuelEvent.cb_Checked;
                CardAnimation.Opacity20(250).Begin(cb);
                cb.IsChecked = false;
                
            }
        }

        #endregion

        #region <-- 选择卡位 -->

        private static void ChooseZone(CheckBox cb, bool state)
        {
            cb.IsChecked = state;
            if (state) 
            {
                CardAnimation.Opacity21(100).Begin(cb);
            }
            else 
            {
                CardAnimation.Opacity20(100).Begin(cb);
            } 
        }

        #endregion

        #region <-- 洗切怪物 -->

        private static void Shuffle_zone(Canvas aim, Dictionary<Card, Canvas> di)
        {
            //Panel.SetZIndex(mainwindow.battle_zone_middle, 1);
            //Panel.SetZIndex(aim, 1);
            //List<Card> temp = new List<Card>(di.Keys);
            //for (int i = 0; i < temp.Count; i++)
            //{            
            //    //CardAnimation.setTransformGroup(temp[i]);
            //    Point start = temp[i].TranslatePoint(new Point(),aim);
            //    if (temp[i].isDef)
            //    {
            //        start.X = start.X + ((temp[i].ActualHeight - temp[i].ActualWidth) / 2.0);
            //        start.Y = start.Y - temp[i].ActualWidth - ((temp[i].ActualHeight - temp[i].ActualWidth) / 2.0);
            //        //start.Y = start.Y - ((temp[i].ActualHeight - temp[i].ActualWidth) / 2.0);
            //    }

            //    //2.获取卡片在卡框中的相对距离
            //    Point end = new Point((aim.ActualWidth - temp[i].ActualWidth) / 2.0, (aim.ActualHeight - temp[i].ActualHeight) / 2.0);
            //    //脱离原控件
            //    Base.getawayParerent(temp[i]);
            //    //利用1设置初始位置
            //    Canvas.SetTop(temp[i], start.Y);
            //    Canvas.SetLeft(temp[i], start.X);
            //    //加入目的地控件
            //    aim.Children.Add(temp[i]);
            //    MyStoryboard msb = CardAnimation.CanvasXY(end, 200);
            //    msb.card = temp[i];
            //    msb.Completed += (object sender_, EventArgs e_) =>
            //    {
            //        //清空属性和动画的关联绑定
            //        msb.card.BeginAnimation(Canvas.LeftProperty, null);
            //        msb.card.BeginAnimation(Canvas.TopProperty, null);

            //        Canvas.SetTop(msb.card, end.Y);
            //        Canvas.SetLeft(msb.card, end.X);

            //        Canvas aim2 = di[msb.card];
            //        Panel.SetZIndex(aim2, i);
            //        Point start2 = msb.card.TranslatePoint(new Point(), aim2);
            //        if (msb.card.isDef)
            //        {
            //            start2.X = start2.X + ((msb.card.ActualHeight - msb.card.ActualWidth) / 2.0);
            //            start2.Y = start2.Y - msb.card.ActualWidth - ((msb.card.ActualHeight - msb.card.ActualWidth) / 2.0);
            //        }

            //        //2.获取卡片在卡框中的相对距离
            //        Point end2 = new Point((aim2.ActualWidth - msb.card.ActualWidth) / 2.0, (aim2.ActualHeight - msb.card.ActualHeight) / 2.0);
            //        //脱离原控件
            //        Base.getawayParerent(msb.card);
            //        //利用1设置初始位置
            //        Canvas.SetTop(msb.card, start2.Y);
            //        Canvas.SetLeft(msb.card, start2.X);
            //        //加入目的地控件
            //        aim2.Children.Add(msb.card);
                    
            //        MyStoryboard msb2 = CardAnimation.CanvasXY(end2, 200);
            //        msb2.card = msb.card;
            //        msb2.Completed += (object a, EventArgs b) =>
            //        {
            //            msb2.card.BeginAnimation(Canvas.LeftProperty, null);
            //            msb2.card.BeginAnimation(Canvas.TopProperty, null);

            //            Canvas.SetTop(msb2.card, end2.Y);
            //            Canvas.SetLeft(msb2.card, end2.X);

            //            Panel.SetZIndex(di[msb2.card],0);

            //            //if (i == cvs.Count)
            //            //{
            //            //    foreach (var item in cvs)
            //            //    {
            //            //        Panel.SetZIndex(item, 0);
            //            //    }
            //            //}
            //            //Panel.SetZIndex(cvs[i], 0);
            //        };
            //        msb2.Begin(msb.card);
                    
            //    };
            //    msb.Begin(temp[i]);
            //}
            //foreach (var item in di)
            //{
                

            //}
        }

        #endregion 

        #region <-- 对手接入 -->

        /// <summary>
        /// 对手接入
        /// </summary>
        /// <param name="duelistName"></param>
        private static void connect(string duelistName)
        {
            DuelOperate duelOperate = DuelOperate.getInstance();
            duelOperate.opponentConnect(duelistName);

        }

        #endregion

        #region <-- 开局 -->

        /// <summary>
        /// 开局
        /// </summary>
        /// <param name="main_"></param>
        /// <param name="extra_"></param>
        private static void startgame(string opuserindex,string main_, string extra_)
        {
            DuelOperate duelOperate = DuelOperate.getInstance();

            List<string> main = new List<string>();
            //Console.WriteLine("主卡组：");
            for (int i = 0; i < main_.Length; i += 8)
            {
                Console.WriteLine(main_.Substring(i, 8));
                main.Add(main_.Substring(i, 8));

            }
            List<string> extra = new List<string>();
            //Console.WriteLine("额外：");
            for (int i = 0; i < extra_.Length; i += 8)
            {
                Console.WriteLine(extra_.Substring(i, 8));
                extra.Add(extra_.Substring(i, 8));
            }

            duelOperate.DuelStart(opuserindex,main, extra);
            //throw new NotImplementedException();
        }

        #endregion

        #region <-- 先攻 -->

        /// <summary>
        /// 先攻
        /// </summary>
        /// <param name="draw_num"></param>
        /// <param name="cv"></param>
        /// <param name="cv_aim"></param>
        private static void FirstAtk(int draw_num,List<Card> cards, Canvas cv, Canvas cv_aim)
        {
            mainwindow.bd_step1.SetValue(Border.BorderBrushProperty, Brushes.Red);
            mainwindow.bd_step1.Effect.SetValue(DropShadowEffect.ColorProperty, Colors.Red);
            mainwindow.bd_step2.SetValue(Border.BorderBrushProperty, Brushes.Red);
            mainwindow.bd_step2.Effect.SetValue(DropShadowEffect.ColorProperty, Colors.Red);
            UIAnimation.ChangePhase(1).Begin(mainwindow.bd_step1);
            Draw(draw_num,cards, cv, cv_aim);
        }

        #endregion

        #region <-- 阶段宣言 -->

        /// <summary>
        /// 阶段宣言
        /// </summary>
        /// <param name="rta"></param>
        /// <param name="press"></param>
        private static void ChangePhase(Rectangle rta, int press)
        {


            MyStoryboard msb2zero = UIAnimation.ChangePhase(0);
            MyStoryboard msb2one = UIAnimation.ChangePhase(1);
            int op = Convert.ToInt32(mainwindow.bd_step1.Opacity);
            //int op = Convert.ToInt32(mainwindow.bd_step1.Effect.GetValue(System.Windows.Media.Effects.DropShadowEffect.OpacityProperty));

            if (rta.Name.Equals("rta_dp"))
            {
                mainwindow.bd_step1.SetValue(Border.BorderBrushProperty, Brushes.Red);
                mainwindow.bd_step1.Effect.SetValue(DropShadowEffect.ColorProperty, Colors.Red);
                mainwindow.bd_step2.SetValue(Border.BorderBrushProperty, Brushes.Red);
                mainwindow.bd_step2.Effect.SetValue(DropShadowEffect.ColorProperty, Colors.Red);
            }

            if (rta.Name.Equals("rta_ep"))
            {


                if (mainwindow.tb_ep.Text.Equals("END"))
                {
                    msb2zero.Completed += (object c, EventArgs d) =>
                    {
                        mainwindow.bd_step1.SetValue(Border.BorderBrushProperty, Brushes.Black);
                        mainwindow.bd_step1.Effect.SetValue(DropShadowEffect.ColorProperty, Colors.Blue);
                        mainwindow.bd_step2.SetValue(Border.BorderBrushProperty, Brushes.Black);
                        mainwindow.bd_step2.Effect.SetValue(DropShadowEffect.ColorProperty, Colors.Blue);
                    };

                    if (op == 1)
                    {
                        msb2zero.Begin(mainwindow.bd_step1);
                    }
                    else
                    {
                        msb2zero.Begin(mainwindow.bd_step2);
                    }



                    return;
                }
                mainwindow.tb_ep.Text = "END";

            }
            else
            {
                mainwindow.tb_ep.Text = "EP";
            }


            if (op == 1)
            {

                Grid.SetColumn(mainwindow.bd_step2, press);
                msb2zero.Begin(mainwindow.bd_step1);
                msb2one.Begin(mainwindow.bd_step2);
            }
            else if (op == 0)
            {
                Grid.SetColumn(mainwindow.bd_step1, press);
                msb2one.Begin(mainwindow.bd_step1);
                msb2zero.Begin(mainwindow.bd_step2);
            }
        }

        #endregion

        #region <-- 发动效果 -->

        /// <summary>
        /// 发动效果
        /// </summary>
        /// <param name="origin"></param>
        private static void StartEffect(FrameworkElement origin)
        {
            MyStoryboard msb = CardAnimation.EffectOrigin();
            msb.Begin(origin);
        }

        #endregion

        #region <-- 选择对象 -->

        /// <summary>
        /// 选择对象
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="aim"></param>
        private static void SelectObject(FrameworkElement origin, FrameworkElement aim)
        {
            MyStoryboard msb = CardAnimation.EffectOrigin();
            msb.Begin(origin);
            MyStoryboard msb2 = CardAnimation.EffectAim();
            msb2.Begin(aim);
        }

        #endregion     

        #region <-- 改变指示物个数 -->

        /// <summary>
        /// 改变指示物个数
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="how"></param>
        private static void ChangeSign(TextBlock tb, string how)
        {
            if (how.Equals("+"))
            {
                tb.Text = (Convert.ToInt32(tb.Text) + 1).ToString();
            }
            if (how.Equals("-"))
            {
                tb.Text = (Convert.ToInt32(tb.Text) - 1).ToString();

                if (Convert.ToInt32(tb.Text) == 0)
                {
                    Grid gd = tb.Parent as Grid;
                    StackPanel sp = gd.Parent as StackPanel;
                    sp.Children.Remove(gd);
                    
                }
            }
        }

        #endregion

        #region <-- 添加指示物 -->

        /// <summary>
        /// 添加指示物
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="color"></param>
        private static void Setsign(StackPanel sp, string color)
        {
            foreach (FrameworkElement fe in sp.Children)
            {
                if (fe.Name.Equals("Black") && color.Equals("black"))
                {
                    //mainwindow.UnregisterName(sp.Name.Replace("sp", "tb") + "_black");
                    sp.Children.Remove(fe); return;
                }
                if (fe.Name.Equals("Blue") && color.Equals("blue"))
                {
                    //mainwindow.UnregisterName(sp.Name.Replace("sp", "tb") + "_blue");
                    sp.Children.Remove(fe); return;
                }
                if (fe.Name.Equals("Red") && color.Equals("red"))
                {
                    //mainwindow.UnregisterName(sp.Name.Replace("sp", "tb") + "_red");
                    sp.Children.Remove(fe); return;
                }

            }

            Grid gd = new Grid();
            gd.Width = 25;
            gd.Height = 25;

            TextBlock tb = new TextBlock();

            tb.Foreground = Brushes.White;
            tb.IsHitTestVisible = true;
            tb.Height = 21;
            tb.Style = Application.Current.Resources["tb_AtkDefStyle"] as Style;
            tb.Text = "1";
            //tb.MouseDown += new MouseButtonEventHandler(DuelEvent.ClikDouble2);

            Border bd = new Border();
            bd.Style = Application.Current.Resources["bd_style"] as Style;
            switch (color)
            {
                case "black":
                    gd.Name = "Black";
                    tb.Name = sp.Name.Replace("sp", "tb") + "_black";
                    //mainwindow.RegisterName(tb.Name, tb);
                    bd.BorderBrush = Brushes.Black;
                    break;
                case "blue":
                    gd.Name = "Blue";
                    tb.Name = sp.Name.Replace("sp", "tb") + "_blue";
                    //mainwindow.RegisterName(tb.Name, tb);
                    bd.BorderBrush = Brushes.Blue;
                    break;
                case "red":
                    gd.Name = "Red";
                    tb.Name = sp.Name.Replace("sp", "tb") + "_red";
                    //mainwindow.RegisterName(tb.Name, tb);
                    bd.BorderBrush = Brushes.Red;
                    break;
            }
            gd.Children.Add(bd);

            
            gd.Children.Add(tb);

            sp.Children.Add(gd);
        }

        #endregion

        #region <-- 清空指示物 -->

        /// <summary>
        /// 清空指示物
        /// </summary>
        /// <param name="sp"></param>
        private static void Clearsign(StackPanel sp)
        {
            string signName = sp.Name.Replace("sp", "tb");
            if (mainwindow.FindName(signName + "_black") != null)
            {
                mainwindow.UnregisterName(signName + "_black");
            }
            if (mainwindow.FindName(signName + "_blue") != null)
            {
                mainwindow.UnregisterName(signName + "_blue");
            }
            if (mainwindow.FindName(signName + "_red") != null)
            {
                mainwindow.UnregisterName(signName + "_red");
            }
            sp.Children.Clear();
        }

        #endregion

        #region <-- 攻击宣言处理 -->

        /// <summary>
        /// 攻击宣言处理
        /// </summary>
        /// <param name="Move_cv"></param>
        /// <param name="Move_cv_aim"></param>
        private static void Atk(Canvas Move_cv, Canvas Move_cv_aim)
        {
            TransLibrary.StoryboardChain tls = new TransLibrary.StoryboardChain();

            Point p1 = Move_cv.TranslatePoint(new Point(Move_cv.ActualWidth / 2, Move_cv.ActualHeight / 2), mainwindow.OpBattle);
            Point p2 = Move_cv_aim.TranslatePoint(new Point(Move_cv_aim.ActualWidth / 2, Move_cv_aim.ActualHeight / 2), mainwindow.OpBattle);
            double angle = Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) * (180 / Math.PI) + 90;


            MyStoryboard msb = CardAnimation.Atk(p1, p2, 800);
            msb.Completed += (object sender_, EventArgs e_) =>
            {
                mainwindow.OpBattle.Children.Remove(msb.sword);
                msb.sword = null;
                
            };  
            MyStoryboard msb2 = CardAnimation.Atk(p1, p2, 700);
            msb2.Completed += (object sender_, EventArgs e_) =>
            {
                mainwindow.OpBattle.Children.Remove(msb2.sword);
                msb2.sword = null;
            };  
            MyStoryboard msb3 = CardAnimation.Atk(p1, p2, 600);
            msb3.Completed += (object sender_, EventArgs e_) =>
            {
                mainwindow.OpBattle.Children.Remove(msb3.sword);
                msb3.sword = null;
                
            };
            MyStoryboard msb4 = CardAnimation.Atkline(p1, p2,800);
            msb4.Completed += (object sender_, EventArgs e_) =>
            {
                mainwindow.OpBattle.Children.Remove(msb4.sword);
                msb4.sword = null;
                

            };   
            

            tls.Animates.Add(msb);
            tls.Animates.Add(msb2);
            tls.Animates.Add(msb3);

            msb4.Begin();
            msb.Begin();
            msb2.Begin();
            msb3.Begin();
            
            
            //tls.Begin();

        }

        #endregion     

        #region <-- 多重卡片操作处理 -->

        private static void cardsHandle(string where ,string command)
        {
            //#region 多重卡片操作

            //if (command.Equals("全部送往墓地") || command.Contains("全部除外") || command.Equals("全部变为表侧 · 防守表示") || command.Equals("全部变为里侧 · 防守表示") || command.Equals("全部变为表侧 · 攻击表示") || command.Contains("全部返回手卡") || command.Contains("全部返回卡组顶端"))
            //{
            //    //MenuItem mi = sender as MenuItem;
            //    //MenuItem mi_par = mi.Parent as MenuItem;

            //    //怪物
            //    int n = 18;
            //    int m = 23;

            //    if (where.Equals("魔陷"))
            //    {
            //        n = 13;
            //        m = 18;
            //    }
            //    if (where.Equals("场上"))
            //    {
            //        n = 13;
            //        m = 23;
            //    }
            //    if (where.Equals("手卡"))
            //    {
            //        n = 36;
            //        m = 37;
            //    }

            //    List<Card> cards_1 = new List<Card>();
            //    List<Card> cards_2 = new List<Card>();
            //    List<Card> cards_3 = new List<Card>();//返回手卡时每个卡位的第一张XYZ怪
            //    List<Card> cards_4 = new List<Card>();//返回手卡时每个卡位的卡片素材

            //    for (int i = n; i < m; i++)
            //    {
            //        Canvas cv = mainwindow.MySpace.Children[i] as Canvas;

            //        if (cv.Children.Count > 0)
            //        {
            //            if (command.Equals("全部变为表侧 · 防守表示") || command.Equals("全部变为表侧 · 攻击表示"))
            //            {
            //                #region

            //                Card top = cv.Children[cv.Children.Count - 1] as Card;
            //                if (top.isBack)
            //                {
            //                    cards_1.Add(top);
            //                }
            //                cards_2.Add(top);

            //                #endregion
            //            }
            //            else if (command.Equals("全部变为里侧 · 防守表示"))
            //            {
            //                #region

            //                Card top = cv.Children[cv.Children.Count - 1] as Card;
            //                if (!top.isBack)
            //                {
            //                    cards_1.Add(top);
            //                }
            //                cards_2.Add(top);

            //                #endregion
            //            }
            //            else if (command.Contains("全部返回手卡"))
            //            {
            //                #region

            //                if (cv.Children.Count < 2)
            //                {
            //                    foreach (Card card in cv.Children)
            //                    {
            //                        if (card.sCardType.Equals("XYZ怪兽") || card.sCardType.Equals("同调怪兽") || card.sCardType.Equals("融合怪兽"))
            //                        {
            //                            if (!card.isBack)
            //                            {
            //                                cards_1.Add(card);
            //                            }
            //                            cards_3.Add(card);

            //                        }
            //                        else
            //                        {
            //                            if (!card.isBack)
            //                            {
            //                                cards_1.Add(card);
            //                            }
            //                            cards_2.Add(card);
            //                        }
            //                    }

            //                }
            //                else if (cv.Children.Count > 1)
            //                {
            //                    foreach (Card card in cv.Children)
            //                    {
            //                        if (card.isBack)
            //                        {
            //                            cards_1.Add(card);
            //                        }

            //                        if (card != cv.Children[cv.Children.Count - 1])
            //                        {
                                        
            //                            cards_4.Add(card);

            //                        }
            //                        else
            //                        {
            //                            if (!card.isBack)
            //                            {
            //                                cards_1.Add(card);
            //                            }
            //                            cards_3.Add(card);
            //                        }
            //                    }
            //                }

            //                #endregion
            //            }
            //            else if (command.Contains("全部返回卡组顶端"))
            //            {
            //                #region

            //                if (cv.Children.Count < 2)
            //                {
            //                    foreach (Card card in cv.Children)
            //                    {
            //                        if (card.sCardType.Equals("XYZ怪兽") || card.sCardType.Equals("同调怪兽") || card.sCardType.Equals("融合怪兽"))
            //                        {
            //                            if (!card.isBack)
            //                            {
            //                                cards_1.Add(card);
            //                            }
            //                            cards_3.Add(card);

            //                        }
            //                        else
            //                        {
            //                            if (!card.isBack)
            //                            {
            //                                cards_1.Add(card);
            //                            }
            //                            cards_2.Add(card);
            //                        }
            //                    }

            //                }
            //                else if (cv.Children.Count > 1)
            //                {
            //                    if (cv.Name.Equals("card_1_hand"))
            //                    {
            //                        foreach (Card card in cv.Children)
            //                        {

            //                            cards_1.Add(card);
            //                            cards_2.Insert(0, card);
            //                            //cards_2.Add(card);
            //                        }
            //                    }
            //                    else
            //                    {
            //                        foreach (Card card in cv.Children)
            //                        {


            //                            if (card != cv.Children[cv.Children.Count - 1])
            //                            {
            //                                cards_4.Add(card);

            //                            }
            //                            else
            //                            {
            //                                if (!card.isBack)
            //                                {
            //                                    cards_1.Add(card);
            //                                }
            //                                cards_3.Add(card);
            //                            }
            //                        }
            //                    }


            //                }

            //                #endregion
            //            }
            //            else if (command.Contains("全部除外"))
            //            {
            //                #region


            //                if (cv.Children.Count < 2)
            //                {
            //                    foreach (Card card in cv.Children)
            //                    {
            //                        cards_2.Add(card);
            //                    }
            //                }
            //                else if (cv.Children.Count > 1)
            //                {

            //                    if (command.Substring(command.Length - 4, 4).Equals("素材除外"))
            //                    {
            //                        foreach (Card card in cv.Children)
            //                        {
            //                            cards_2.Add(card);
            //                        }
            //                    }
            //                    else if (command.Substring(command.Length - 4, 4).Equals("素材送墓"))
            //                    {
            //                        foreach (Card card in cv.Children)
            //                        {


            //                            if (card != cv.Children[cv.Children.Count - 1])
            //                            {
            //                                cards_4.Add(card);

            //                            }
            //                            else
            //                            {
            //                                cards_2.Add(card);
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        //手卡除外的情况
            //                        foreach (Card card in cv.Children)
            //                        {
            //                            cards_2.Add(card);
            //                        }
            //                    }

            //                }

            //                #endregion
            //            }
            //            else
            //            {
            //                foreach (Card card in cv.Children)
            //                {

            //                    if (command.Equals("全部送往墓地"))
            //                    {
            //                        if (card.isBack)
            //                        {
            //                            cards_1.Add(card);
            //                        }

            //                    }
            //                    cards_2.Add(card);
            //                }
            //            }


            //        }
            //    }

            //    TransLibrary.StoryboardChain tls = new TransLibrary.StoryboardChain();

            //    #region 处理所有需要翻转的卡的前期翻转

            //    if (cards_1.Count > 0)
            //    {
            //        if (command.Equals("全部送往墓地") || command.Equals("全部变为表侧 · 防守表示") || command.Equals("全部变为表侧 · 攻击表示"))
            //        {
            //            MyStoryboard msb1 = CardAnimation.Cards_scalX_120(cards_1, 100);
            //            msb1.Completed += (object sender_, EventArgs e_) =>
            //            {
            //                foreach (Card card in msb1.cards)
            //                {
            //                    //card.isDef = false;
            //                    card.isBack = false;
            //                    card.SetPic();
            //                }

            //            };
            //            tls.Animates.Add(msb1);
            //        }
            //        else if (command.Equals("全部变为里侧 · 防守表示"))
            //        {
            //            MyStoryboard msb6 = CardAnimation.Cards_formchange2DefDown(cards_1, 100);
            //            msb6.Completed += (object sender_, EventArgs e_) =>
            //            {
            //                foreach (Card card in msb6.cards)
            //                {
            //                    if (!card.isDef)
            //                    {
            //                        card.isDef = true;
            //                    }
            //                    card.isBack = true;
            //                    card.SetPic();
            //                }

            //            };
            //            tls.Animates.Add(msb6);
            //        }
            //        else if (command.Substring(0, 6).Equals("全部返回手卡"))
            //        {
            //            MyStoryboard msb9 = CardAnimation.Cards_scalX_120(cards_1, 100);
            //            msb9.Completed += (object sender_, EventArgs e_) =>
            //            {
            //                foreach (Card card in msb9.cards)
            //                {
            //                    //card.isDef = false;
            //                    card.isBack = card.isBack ? false : true;
            //                    //{

            //                    //}
            //                    //card.isBack = false;
            //                    card.SetPic();
            //                }

            //            };
            //            tls.Animates.Add(msb9);
            //        }
            //        else if (command.Substring(0, 8).Equals("全部返回卡组顶端"))
            //        {
            //            MyStoryboard msb15 = CardAnimation.Cards_scalX_120(cards_1, 100);
            //            msb15.Completed += (object sender_, EventArgs e_) =>
            //            {
            //                foreach (Card card in msb15.cards)
            //                {
            //                    card.isBack = true;
            //                    card.SetPic();
            //                }

            //            };
            //            tls.Animates.Add(msb15);
            //        }

            //    }

            //    #endregion

            //    #region 处理所有送往命令目的地的卡

            //    if (cards_2.Count > 0)
            //    {
            //        if (command.Equals("全部送往墓地"))
            //        {
            //            #region

            //            Point end = mainwindow.card_1_Graveyard.TranslatePoint(new Point(), mainwindow.MySpace);
            //            MyStoryboard msb2 = CardAnimation.Cards_move(cards_2, end, 150, "2");
            //            msb2.Completed += (object sender_, EventArgs e_) =>
            //            {
            //                foreach (Card card in msb2.cards)
            //                {
            //                    card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //                    card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);

            //                    Base.getawayParerent(card);
            //                    //mainwindow.MySpace.Children.Remove(card);

            //                    //清空属性和动画的关联绑定
            //                    card.BeginAnimation(Canvas.LeftProperty, null);
            //                    card.BeginAnimation(Canvas.TopProperty, null);

            //                    Canvas.SetTop(card, 0);
            //                    Canvas.SetLeft(card, 0);

            //                    card.isDef = false;
            //                    mainwindow.card_2_Graveyard.Children.Add(card);
            //                    CardOperate.sort_SingleCard(card);

            //                }
            //            };

            //            tls.Animates.Add(msb2);

            //            #endregion
            //        }
            //        else if (command.Contains("全部除外"))
            //        {
            //            #region

            //            MyStoryboard msb3 = CardAnimation.Cards_disappear(cards_2, 150);
            //            msb3.Completed += (object sender_, EventArgs e_) =>
            //            {
            //                foreach (Card card in msb3.cards)
            //                {
            //                    Base.getawayParerent(card);
            //                    card.RenderTransform = new RotateTransform();
            //                    //card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);

            //                    card.isBack = false;
            //                    card.isDef = false;
            //                    card.SetPic();
            //                    mainwindow.card_2_Outside.Children.Add(card);
            //                    CardOperate.sort_SingleCard(card);
            //                }

            //                foreach (Card card in cards_4)
            //                {
            //                    card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //                    card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);

            //                    Base.getawayParerent(card);
            //                    //mainwindow.MySpace.Children.Remove(card);

            //                    //清空属性和动画的关联绑定
            //                    card.BeginAnimation(Canvas.LeftProperty, null);
            //                    card.BeginAnimation(Canvas.TopProperty, null);

            //                    Canvas.SetTop(card, 0);
            //                    Canvas.SetLeft(card, 0);

            //                    card.isBack = false;
            //                    card.isDef = false;
            //                    mainwindow.card_2_Graveyard.Children.Add(card);
            //                    CardOperate.sort_SingleCard(card);

            //                }
            //            };
            //            msb3.Name = "msb3";
            //            tls.Animates.Add(msb3);
            //            MyStoryboard msb4 = CardAnimation.Cards_appear(cards_2, 150);
            //            msb4.Completed += (object sender_, EventArgs e_) =>
            //            {
            //                foreach (Card card in msb4.cards)
            //                {
            //                    card.BeginAnimation(Card.OpacityProperty, null);
            //                }
            //            };
            //            tls.Animates.Add(msb4);

            //            #endregion
            //        }
            //        else if (command.Equals("全部变为表侧 · 防守表示"))
            //        {
            //            #region

            //            MyStoryboard msb5 = CardAnimation.Cards_formchange2DefUp(cards_2, 150);
            //            msb5.Completed += (object sender_, EventArgs e_) =>
            //            {
            //                foreach (Card card in msb5.cards)
            //                {
            //                    card.isDef = true;
            //                    card.BeginAnimation(Card.OpacityProperty, null);
            //                    if (card.sCardType.Equals("XYZ怪兽"))
            //                    {
            //                        Canvas cv = card.Parent as Canvas;
            //                        CardOperate.sort_Canvas(cv);
            //                    }
            //                }
            //            };
            //            tls.Animates.Add(msb5);

            //            #endregion
            //        }
            //        else if (command.Equals("全部变为里侧 · 防守表示"))
            //        {
            //            #region

            //            MyStoryboard msb7 = CardAnimation.Cards_scalX_021(cards_2, 150);
            //            msb7.Completed += (object sender_, EventArgs e_) =>
            //            {
            //                foreach (Card card in msb7.cards)
            //                {
            //                    card.isDef = true;
            //                    card.BeginAnimation(Card.OpacityProperty, null);
            //                    if (card.sCardType.Equals("XYZ怪兽"))
            //                    {
            //                        Canvas cv = card.Parent as Canvas;
            //                        CardOperate.sort_Canvas(cv);
            //                    }
            //                }
            //            };
            //            tls.Animates.Add(msb7);

            //            #endregion
            //        }
            //        else if (command.Equals("全部变为表侧 · 攻击表示"))
            //        {
            //            #region

            //            MyStoryboard msb8 = CardAnimation.Cards_formchange2AtkUp(cards_2, 150);
            //            msb8.Completed += (object sender_, EventArgs e_) =>
            //            {
            //                foreach (Card card in msb8.cards)
            //                {
            //                    card.isDef = false;
            //                    card.BeginAnimation(Card.OpacityProperty, null);
            //                    if (card.sCardType.Equals("XYZ怪兽"))
            //                    {
            //                        Canvas cv = card.Parent as Canvas;
            //                        CardOperate.sort_Canvas(cv);
            //                    }
            //                }
            //            };
            //            tls.Animates.Add(msb8);

            //            #endregion
            //        }
            //        else if (command.Substring(0, 6).Equals("全部返回手卡"))
            //        {
            //            #region

            //            Point end = mainwindow.card_2_hand.TranslatePoint(new Point(), mainwindow.OpBattle);
            //            if (mainwindow.card_2_hand.Children.Count < 1)
            //            {
            //                end.X = end.X + ((mainwindow.card_2_hand.Width - 56) / 2);
            //            }
            //            else
            //            {
            //                end.X = end.X + Canvas.GetLeft(mainwindow.card_2_hand.Children[mainwindow.card_2_hand.Children.Count - 1]);
            //            }
            //            MyStoryboard msb10 = CardAnimation.Cards_move2(cards_2, end, 150, "2");
            //            msb10.Completed += (object sender_, EventArgs e_) =>
            //            {
            //                foreach (Card card in msb10.cards)
            //                {
            //                    card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //                    card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);

            //                    Base.getawayParerent(card);
            //                    //mainwindow.MySpace.Children.Remove(card);

            //                    //清空属性和动画的关联绑定
            //                    card.BeginAnimation(Canvas.LeftProperty, null);
            //                    card.BeginAnimation(Canvas.TopProperty, null);

            //                    Canvas.SetTop(card, 0);
            //                    Canvas.SetLeft(card, 0);

            //                    card.isDef = false;
            //                    mainwindow.card_2_hand.Children.Add(card);

            //                }

            //                CardOperate.sort_HandCard(mainwindow.card_2_hand);

            //                foreach (Card card in cards_3)
            //                {
            //                    card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //                    card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);

            //                    Base.getawayParerent(card);
            //                    //mainwindow.MySpace.Children.Remove(card);

            //                    //清空属性和动画的关联绑定
            //                    card.BeginAnimation(Canvas.LeftProperty, null);
            //                    card.BeginAnimation(Canvas.TopProperty, null);

            //                    Canvas.SetTop(card, 0);
            //                    Canvas.SetLeft(card, 0);

            //                    card.isDef = false;
            //                    mainwindow.card_2_Extra.Children.Add(card);
                                
            //                    CardOperate.sort_SingleCard(card);
            //                }

            //                foreach (Card card in cards_4)
            //                {
            //                    if (command.Substring(command.Length - 4, 4).Equals("素材送墓"))
            //                    {
            //                        card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //                        card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);

            //                        Base.getawayParerent(card);
            //                        //mainwindow.MySpace.Children.Remove(card);

            //                        //清空属性和动画的关联绑定
            //                        card.BeginAnimation(Canvas.LeftProperty, null);
            //                        card.BeginAnimation(Canvas.TopProperty, null);

            //                        Canvas.SetTop(card, 0);
            //                        Canvas.SetLeft(card, 0);

            //                        card.isDef = false;
            //                        mainwindow.card_2_Graveyard.Children.Add(card);
            //                        CardOperate.sort_SingleCard(card);
            //                    }
            //                    else if (command.Substring(command.Length - 4, 4).Equals("素材除外"))
            //                    {

            //                        Base.getawayParerent(card);
            //                        card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);
            //                        card.isBack = false;
            //                        card.isDef = false;
            //                        card.SetPic();
            //                        mainwindow.card_1_Outside.Children.Add(card);
            //                        CardOperate.sort_SingleCard(card);
            //                        MyStoryboard msb14 = CardAnimation.Cards_appear(cards_4, 100);
            //                        msb14.Completed += (object sender__, EventArgs e__) =>
            //                        {
            //                            foreach (Card card_ in msb14.cards)
            //                            {
            //                                card.BeginAnimation(Card.OpacityProperty, null);
            //                            }
            //                        };
            //                        msb14.Begin();

            //                        //card.BeginAnimation(Card.OpacityProperty, null);
            //                    }


            //                }
            //            };

            //            msb10.Name = "msb10";
            //            tls.Animates.Add(msb10);

            //            #endregion
            //        }
            //        else if (command.Substring(0, 8).Equals("全部返回卡组顶端"))
            //        {
            //            #region

            //            Point end = mainwindow.card_1_Deck.TranslatePoint(new Point(), mainwindow.MySpace);

            //            MyStoryboard msb16 = CardAnimation.Cards_move2(cards_2, end, 150, "2");
            //            msb16.Completed += (object sender_, EventArgs e_) =>
            //            {
            //                foreach (Card card in cards_2)
            //                {
            //                    card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //                    card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);

            //                    Base.getawayParerent(card);
            //                    //mainwindow.MySpace.Children.Remove(card);

            //                    //清空属性和动画的关联绑定
            //                    card.BeginAnimation(Canvas.LeftProperty, null);
            //                    card.BeginAnimation(Canvas.TopProperty, null);

            //                    Canvas.SetTop(card, 0);
            //                    Canvas.SetLeft(card, 0);

            //                    card.isDef = false;
            //                    card.isBack = true;
            //                    mainwindow.card_2_Deck.Children.Add(card);
            //                    CardOperate.sort_SingleCard(card);
            //                }

            //                foreach (Card card in cards_3)
            //                {
            //                    card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //                    card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);

            //                    mainwindow.MySpace.Children.Remove(card);

            //                    //清空属性和动画的关联绑定
            //                    card.BeginAnimation(Canvas.LeftProperty, null);
            //                    card.BeginAnimation(Canvas.TopProperty, null);

            //                    Canvas.SetTop(card, 0);
            //                    Canvas.SetLeft(card, 0);

            //                    card.isDef = false;
            //                    card.isBack = true;
            //                    mainwindow.card_2_Extra.Children.Add(card);
            //                    CardOperate.sort_SingleCard(card);
            //                }

            //                foreach (Card card in cards_4)
            //                {
            //                    if (command.Substring(command.Length - 4, 4).Equals("素材送墓"))
            //                    {
            //                        card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //                        card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);

            //                        Base.getawayParerent(card);
            //                        //mainwindow.MySpace.Children.Remove(card);

            //                        //清空属性和动画的关联绑定
            //                        card.BeginAnimation(Canvas.LeftProperty, null);
            //                        card.BeginAnimation(Canvas.TopProperty, null);

            //                        Canvas.SetTop(card, 0);
            //                        Canvas.SetLeft(card, 0);

            //                        card.isDef = false;
            //                        mainwindow.card_2_Graveyard.Children.Add(card);
            //                        CardOperate.sort_SingleCard(card);
            //                    }
            //                    else if (command.Substring(command.Length - 4, 4).Equals("素材除外"))
            //                    {

            //                        Base.getawayParerent(card);
            //                        card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);
            //                        card.isBack = false;
            //                        card.isDef = false;
            //                        card.SetPic();
            //                        mainwindow.card_2_Outside.Children.Add(card);
            //                        CardOperate.sort_SingleCard(card);
            //                        MyStoryboard msb14 = CardAnimation.Cards_appear(cards_4, 100);
            //                        msb14.Completed += (object sender__, EventArgs e__) =>
            //                        {
            //                            foreach (Card card_ in msb14.cards)
            //                            {
            //                                card.BeginAnimation(Card.OpacityProperty, null);
            //                            }
            //                        };
            //                        msb14.Begin();

            //                        //card.BeginAnimation(Card.OpacityProperty, null);
            //                    }


            //                }

            //            };
            //            msb16.Name = "msb16";
            //            tls.Animates.Add(msb16);

            //            #endregion
            //        }



            //    }

            //    #endregion

            //    #region 当处理回手卡,回卡组命令时，XYZ需回到额外

            //    if (cards_3.Count > 0)
            //    {


            //        Point end = mainwindow.card_2_Extra.TranslatePoint(new Point(), mainwindow.OpBattle);

            //        //设置XYZ的动画
            //        MyStoryboard msb11 = CardAnimation.Cards_move2(cards_3, end, 150, "2");

            //        if (cards_2.Count < 1)
            //        {
            //            //若msb10不存在，则不存在回手卡的卡，需要启动自身动画，即msb11
            //            msb11.Completed += (object sender_, EventArgs e_) =>
            //            {
            //                foreach (Card card in cards_3)
            //                {
            //                    card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //                    card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);

            //                    Base.getawayParerent(card);
            //                    //mainwindow.MySpace.Children.Remove(card);

            //                    //清空属性和动画的关联绑定
            //                    card.BeginAnimation(Canvas.LeftProperty, null);
            //                    card.BeginAnimation(Canvas.TopProperty, null);

            //                    Canvas.SetTop(card, 0);
            //                    Canvas.SetLeft(card, 0);

            //                    card.isDef = false;
            //                    card.isBack = true;
            //                    mainwindow.card_2_Extra.Children.Insert(0,card);
            //                    CardOperate.sort_SingleCard(card);
            //                }
            //            };
            //            tls.Animates.Add(msb11);
            //        }
            //        else
            //        {
            //            //若msb10存在，即存在回手卡的卡，则添加到其动画时间线中
            //            foreach (MyStoryboard msb in tls.Animates)
            //            {
            //                if (msb.Name != null)
            //                {
            //                    if (msb.Name.Equals("msb10"))
            //                    {
            //                        foreach (DoubleAnimation dba in msb11.Children)
            //                            msb.Children.Add(dba);
            //                    }
            //                    else if (msb.Name.Equals("msb16"))
            //                    {
            //                        foreach (DoubleAnimation dba in msb11.Children)
            //                            msb.Children.Add(dba);
            //                    }
            //                }
            //            }
            //        }

            //    }

            //    #endregion

            //    #region 当处理回手卡命，回卡组命令时，素材需要送去墓地或者除外

            //    if (cards_4.Count > 0)
            //    {


            //        if (command.Substring(command.Length - 4, 4).Equals("素材送墓"))
            //        {
            //            #region 素材送墓地

            //            Point end = mainwindow.card_2_Graveyard.TranslatePoint(new Point(), mainwindow.OpBattle);
            //            MyStoryboard msb12 = CardAnimation.Cards_move2(cards_4, end, 150, "2");
            //            if (cards_2.Count < 1)
            //            {
            //                //若msb10不存在，则不存在回手卡的卡，需要启动自身动画，即msb11
            //                msb12.Completed += (object sender_, EventArgs e_) =>
            //                {
            //                    foreach (Card card in cards_4)
            //                    {
            //                        card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //                        card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);

            //                        Base.getawayParerent(card);
            //                        //mainwindow.MySpace.Children.Remove(card);

            //                        //清空属性和动画的关联绑定
            //                        card.BeginAnimation(Canvas.LeftProperty, null);
            //                        card.BeginAnimation(Canvas.TopProperty, null);

            //                        Canvas.SetTop(card, 0);
            //                        Canvas.SetLeft(card, 0);

            //                        card.isDef = false;
            //                        mainwindow.card_2_Graveyard.Children.Add(card);
            //                        CardOperate.sort_SingleCard(card);
            //                    }
            //                };
            //                tls.Animates.Add(msb12);
            //            }
            //            else
            //            {
            //                //若msb10存在，即存在回手卡的卡，则添加到其动画时间线中
            //                foreach (MyStoryboard msb in tls.Animates)
            //                {
            //                    if (msb.Name != null)
            //                    {

            //                        if (msb.Name.Equals("msb10"))
            //                        {
            //                            foreach (DoubleAnimation dba in msb12.Children)
            //                            {
            //                                msb.Children.Add(dba);
            //                            }

            //                        }
            //                        else if (msb.Name.Equals("msb16"))
            //                        {
            //                            foreach (DoubleAnimation dba in msb12.Children)
            //                            {
            //                                msb.Children.Add(dba);
            //                            }

            //                        }
            //                        else if (msb.Name.Equals("msb3"))
            //                        {
            //                            foreach (DoubleAnimation dba in msb12.Children)
            //                            {
            //                                msb.Children.Add(dba);
            //                            }
            //                        }

            //                    }
            //                }
            //            }

            //            #endregion 素材送墓地
            //        }
            //        else if (command.Substring(command.Length - 4, 4).Equals("素材除外"))
            //        {
            //            #region 素材除外

            //            MyStoryboard msb13 = CardAnimation.Cards_disappear(cards_4, 150);
            //            MyStoryboard msb14 = CardAnimation.Cards_appear(cards_4, 100);
            //            if (cards_2.Count < 1)
            //            {
            //                //若msb10不存在，则不存在回手卡的卡，需要启动自身动画，即msb11
            //                msb13.Completed += (object sender_, EventArgs e_) =>
            //                {
            //                    foreach (Card card in msb13.cards)
            //                    {
            //                        Base.getawayParerent(card);
            //                        card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);
            //                        card.isBack = false;
            //                        card.isDef = false;
            //                        card.SetPic();
            //                        mainwindow.card_2_Outside.Children.Add(card);
            //                        CardOperate.sort_SingleCard(card);
            //                    }
            //                };
            //                tls.Animates.Add(msb13);
            //                msb14.Completed += (object sender_, EventArgs e_) =>
            //                {
            //                    foreach (Card card in msb14.cards)
            //                    {
            //                        card.BeginAnimation(Card.OpacityProperty, null);
            //                    }
            //                };
            //                tls.Animates.Add(msb14);
            //            }
            //            else
            //            {
            //                //若msb10存在，即存在回手卡的卡，则添加到其动画时间线中
            //                foreach (MyStoryboard msb in tls.Animates)
            //                {
            //                    if (msb.Name != null)
            //                    {
            //                        if (msb.Name.Equals("msb10"))
            //                        {
            //                            foreach (DoubleAnimationUsingKeyFrames dba in msb13.Children)
            //                            {
            //                                msb.Children.Add(dba);
            //                            }
            //                        }
            //                        else if (msb.Name.Equals("msb16"))
            //                        {
            //                            foreach (DoubleAnimationUsingKeyFrames dba in msb13.Children)
            //                            {
            //                                msb.Children.Add(dba);
            //                            }
            //                        }

            //                    }


            //                }
            //            }
            //            #endregion
            //        }




            //    }

            //    tls.Begin();

            //    #endregion
            //}

            //#endregion
        }

        #endregion

        #region <-- 攻/守形式转换 -->

        /// <summary>
        /// 攻←→守
        /// </summary>
        /// <param name="cv"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        public static bool Def_or_Atk(Canvas cv,Card card)
        {
            //#region 表侧变为里侧，消除指示物。清除攻守数据或显示攻守数据

            ////Canvas cv = card.Parent as Canvas;

            ////if (card.isBack)
            ////{
            ////    TextBlock tb = mainwindow.FindName(cv.Name.Replace("card", "atk")) as TextBlock;
            ////    tb.SetCurrentValue(TextBlock.TextProperty, card.cardAtk + "/" + card.cardDef);
            ////}

            //#endregion

            //CardAnimation.setTransformGroup(card);

            //#region 里守→表攻

            //if (card.isDef && card.isBack)
            //{                
            //    TransLibrary.StoryboardChain tls = new TransLibrary.StoryboardChain();
            //    MyStoryboard msb1 = CardAnimation.ScaleX_120_Rotate(-90, 0, 100, 200);
            //    msb1.Completed += (object sender, EventArgs e) =>
            //    {
            //        card.isDef = false;
            //        card.isBack = false;
            //        card.SetPic(); 
            //    };
            //    MyStoryboard msb2 = CardAnimation.ScaleX_021(200);
            //    tls.Animates.Add(msb1);
            //    tls.Animates.Add(msb2);
            //    tls.Begin(card);
            //    CardOperate.sort_Canvas(cv);

            //    TextBlock tb = mainwindow.FindName(cv.Name.Replace("card", "atk")) as TextBlock;
            //    if(tb != null) tb.SetCurrentValue(TextBlock.TextProperty, card.atk + "/" + card.def);

            //    return true;
            //}

            //#endregion

            //#region 表攻，里攻→里守，表守

            //if (card.isDef == false)
            //{              
            //    if (cv.Children.Count > 1)
            //    {
            //        CardOperate.sort_Canvas(cv);
            //    }

            //    MyStoryboard msb = CardAnimation.Rotate_A2D(250);
            //    msb.Begin(card);
            //    card.isDef = true;
            //    CardOperate.sort_Canvas(cv);
            //    return true;
            //}

            //#endregion

            //#region 表守→表攻

            //if (card.isDef && card.isBack == false)
            //{
            //    MyStoryboard msb = CardAnimation.Rotate_D2A(250);
            //    msb.Begin(card);           
            //    card.isDef = false;
            //    card.isBack = false;
            //    CardOperate.sort_Canvas(cv);
            //    return true;
            //}

            //#endregion

            return false;
        }

        #endregion

        #region <-- 里/表形式转换 -->

        /// <summary>
        /// 里侧←→表侧
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static bool Back_or_Front(Card card)
        {

            //#region 表侧变为里侧，消除指示物。清除攻守数据或显示攻守数据
            
            //Canvas cv = card.Parent as Canvas;

            //if (!card.isBack)
            //{
            //    StackPanel sp = mainwindow.FindName(cv.Name.Replace("card", "sp_sign")) as StackPanel;
            //    if (sp!=null) Clearsign(sp);

            //    TextBlock tb = mainwindow.FindName(cv.Name.Replace("card", "atk")) as TextBlock;
            //    if (tb != null) tb.SetCurrentValue(TextBlock.TextProperty, null);
            //}
            //else if (card.isBack)
            //{
            //    TextBlock tb = mainwindow.FindName(cv.Name.Replace("card", "atk")) as TextBlock;
            //    if( tb != null ) tb.SetCurrentValue(TextBlock.TextProperty, card.atk + "/" + card.def);
            //}

            //#endregion

            //CardAnimation.setTransformGroup(card);
            //TransLibrary.StoryboardChain tls = new TransLibrary.StoryboardChain();

            //#region 表守或者里守

            //if (card.isDef)
            //{
                
            //    MyStoryboard msb1 = CardAnimation.ScaleX_120(250);
            //    msb1.Completed += (object sender, EventArgs e) =>
            //    {
            //        card.isBack = card.isBack ? false : true;
            //        card.SetPic();
            //    };
            //    MyStoryboard msb2 = CardAnimation.ScaleX_021(250);
            //    msb2.card = card;
            //    msb2.Completed += (object sender, EventArgs e) =>
            //    {
            //        msb2.card.BeginAnimation(ScaleTransform.ScaleXProperty, null);
            //        msb2.card.BeginAnimation(ScaleTransform.ScaleYProperty, null);
            //        msb2.card.SetCurrentValue(ScaleTransform.ScaleYProperty, 0.0);
            //        msb2.card.SetCurrentValue(RotateTransform.AngleProperty, (double)-90.0);

            //    };
            //    tls.Animates.Add(msb1);
            //    tls.Animates.Add(msb2);
                
            //}

            //#endregion

            //#region 表攻或者里攻

            //if (!card.isDef)
            //{
            //    MyStoryboard msb1 = CardAnimation.ScaleX_120(250);
            //    msb1.Completed += (object sender, EventArgs e) =>
            //    {
            //        card.isBack = card.isBack ? false : true;
            //        card.SetPic();
            //    };
            //    MyStoryboard msb2 = CardAnimation.ScaleX_021(250);
            //    tls.Animates.Add(msb1);
            //    tls.Animates.Add(msb2);
            //}

            //#endregion

            //tls.Begin(card);

            return true;
        }

        #endregion

        #region <-- 转为里侧守备 -->

        /// <summary>
        /// 表攻→里守
        /// </summary>
        /// <param name="cv"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        public static bool Atk2Back(Canvas cv,Card card)
        {
            //if (card.isBack && card.isDef)
            //{
            //    return true;
            //}

            //#region 表侧变为里侧，消除指示物。清除攻守数据或显示攻守数据

            ////Canvas cv = card.Parent as Canvas;

            //if (!card.isBack)
            //{
            //    StackPanel sp = mainwindow.FindName(cv.Name.Replace("card", "sp_sign")) as StackPanel;
            //    Clearsign(sp);

            //    TextBlock tb = mainwindow.FindName(cv.Name.Replace("card", "atk")) as TextBlock;
            //    tb.SetCurrentValue(TextBlock.TextProperty, null);
            //}
            

            //#endregion

            //CardAnimation.setTransformGroup(card);

            //if (!card.isBack)
            //{
            //    #region 表守→里守

            //    if (card.isDef)
            //    {
            //        Back_or_Front(card);
            //        return true;
            //    }

            //    #endregion

            //    #region  表攻→里守

            //    if (!card.isDef)
            //    {
            //        #region 清除指示物

            //        StackPanel sp = mainwindow.FindName(cv.Name.Replace("card", "sp_sign")) as StackPanel;
            //        if (sp != null) sp.Children.Clear();

            //        #endregion

            //        TransLibrary.StoryboardChain tls = new TransLibrary.StoryboardChain();
            //        MyStoryboard msb1 = CardAnimation.ScaleX_120_Rotate(0, -90, 250, 250);
            //        msb1.Completed += (object sender, EventArgs e) =>
            //        {

            //            card.isDef = true;
            //            card.isBack = true;
            //            card.SetPic();
            //        };
            //        MyStoryboard msb2 = CardAnimation.ScaleX_021(250);
            //        tls.Animates.Add(msb1);
            //        tls.Animates.Add(msb2);
            //        tls.Begin(card);
            //        if (cv.Children.Count > 1)
            //        {
            //            CardOperate.sort_Canvas(cv);
            //        }
            //        return true;
            //    }

            //    #endregion
            //}

            //#region 里攻→里守

            //if (card.isBack && !card.isDef)
            //{
            //    MyStoryboard msb = CardAnimation.Rotate_A2D(250);
            //    msb.Begin(card);
            //    card.isDef = true;
            //    CardOperate.sort_Canvas(cv);
            //}
            

            //#endregion

            return false ;
        }

        #endregion

        #region <-- 抽卡命令 -->

        /// <summary>
        /// 抽卡命令
        /// </summary>
        /// <param name="num"></param>
        /// <param name="cv"></param>
        /// <param name="cv_aim"></param>
        /// <returns></returns>
        public static bool Draw(int draw_num,List<Card> cards,Canvas cv,Canvas cv_aim)
        {

            ////Random rand = new Random();
            ////List<int> randNum = new List<int>()5;
            ////while (randNum.Count < 5)
            ////{
            ////    int addNum = rand.Next(0, 50); 
            ////    if (!randNum.Contains(addNum)) randNum.Add(addNum);            
            ////}


            //if ( !(cv.Children.Count < draw_num))
            //{

            //    TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();
            //    //List<FrameworkElement> cards = new List<FrameworkElement>();
            //    for (int i = 0; i < cards.Count; i++)
            //    {
            //        //Card card_main = cv.Children[cv.Children.Count - 1] as Card;
            //        //1.获取卡片相对于目的地的距离
            //        Point start = cards[i].TranslatePoint(new Point(), cv_aim);
            //        //2.获取卡片在卡框中的相对距离
            //        //Card card_handlast = cv_aim.Children[cv_aim.Children.Count - 1] as Card;
            //        Point end;
            //        if (cv_aim.Children.Count == 0 || draw_num > 1)
            //        {
            //            end = new Point(((cv_aim.ActualWidth - cards[i].ActualWidth) / 2), ((cv_aim.ActualHeight - cards[i].ActualHeight) / 2));
            //        }
            //        else
            //        {
            //            Card card_hand_R1 = cv_aim.Children[cv_aim.Children.Count - 1] as Card;
            //            end = card_hand_R1.TranslatePoint(new Point(), cv_aim);
            //        }
                    
            //        //脱离原控件
            //        Base.getawayParerent(cards[i]);
            //        //利用1设置初始位置
            //        Canvas.SetTop(cards[i], start.Y);
            //        Canvas.SetLeft(cards[i], start.X);
            //        //加入目的地控件
            //        cv_aim.Children.Add(cards[i]);

            //        MyStoryboard msb = CardAnimation.CanvasXY(start, end, 150);
            //        msb.card = cards[i];
            //        msb.Completed += (object c, EventArgs d) =>
            //        {

            //            msb.card.BeginAnimation(Canvas.LeftProperty, null);
            //            msb.card.BeginAnimation(Canvas.TopProperty, null);

            //            Canvas.SetTop(msb.card, end.Y);
            //            Canvas.SetLeft(msb.card, end.X);
                        
            //        };
            //        //CardOperate.sort_HandCard("2");
            //        animator.Animates.Add(msb);
            //        //cards.Add(cards[i]);
            //        //msb.Begin(card_main);
            //    }
            //    animator.Animates[animator.Animates.Count - 1].Completed += (object c, EventArgs d) =>
            //    {
            //        CardOperate.sort_HandCard(cv_aim);
            //    };
                
            //    animator.Begin(cards.ToList<FrameworkElement>());

            //    return true;
            //}

            return false;
                      
        }

        #endregion

        #region <-- 卡片召唤-->


        public static bool Summon2(Card card,Canvas cv_aim,bool add,Card card2)
        {
            //Grid gd = cv_aim.Parent as Grid;
            //if (gd != null)
            //{
            //    Panel.SetZIndex(gd, 1);
            //}
            //Panel.SetZIndex(cv_aim, 11);

            ////动画栈
            

            //Canvas cv = Base.getParerent(card);

            //CardAnimation.setTransformGroup(card);

            ////1.获取卡片起始位置相对于目的地的坐标
            //Point start = card.TranslatePoint(new Point(), cv_aim);

            ////2.获取卡片终止位置相对于目的地的坐标
            //Point end = new Point((cv_aim.ActualWidth - card.ActualWidth) / 2.0, (cv_aim.ActualHeight - card.ActualHeight) / 2.0);
            ////脱离原控件
            //getawayParerent(card);
            ////利用1设置初始位置
            //Canvas.SetTop(card, start.Y);
            //Canvas.SetLeft(card, start.X);

            //if (cv.Equals(mainwindow.card_1_Outside) || cv.Equals(mainwindow.card_2_Outside))
            //{
            //    //如果是从除外离开
            //    card.isDef = true;
            //    CardAnimation.setTransformGroup(card);
            //}
            //if (card.isDef)
            //{
            //    //防守需要重新设定起始位置
            //    Canvas.SetTop(card, start.Y - card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2.0));
            //    Canvas.SetLeft(card, start.X + ((card.ActualHeight - card.ActualWidth) / 2.0));
            //}
            ////加入目的地控件
            //if (add)
            //{
            //    #region <-- 叠放操作需要清除指示物 -->

            //    if (cv_aim.Children.Count > 0 )
            //    {
            //        Card top = cv_aim.Children[cv_aim.Children.Count - 1] as Card;
            //        if (!top.CardDType.Equals("XYZ"))
            //        {
            //            #region 清除指示物

            //            StackPanel sp = mainwindow.FindName(cv_aim.Name.Replace("card", "sp_sign")) as StackPanel;
            //            if (sp != null) sp.Children.Clear();

            //            #endregion
            //        }
            //    }

            //    #endregion

            //    cv_aim.Children.Add(card);
            //    TextBlock tb = mainwindow.FindName(cv_aim.Name.Replace("card", "atk")) as TextBlock;
            //    if(tb != null && !card.sCardType.Contains("魔法") && !card.sCardType.Contains("陷阱")) tb.SetCurrentValue(TextBlock.TextProperty, card.atk + "/" + card.def);
            //}
            //else if (!add)
            //{
            //    cv_aim.Children.Insert(0, card);
            //}

       
            //CardAnimation.setTransformGroup(card);

            //TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();

            //if (card2 != null)
            //{
            //    CardAnimation.setTransformGroup(card2);
            //    TextBlock tb = mainwindow.FindName(cv_aim.Name.Replace("card", "atk")) as TextBlock;
            //    tb.SetCurrentValue(TextBlock.TextProperty, card.atk + "/" + card.def);

            //    TransLibrary.StoryboardChain animator0 = new TransLibrary.StoryboardChain();
            //    if (card2.isBack)
            //    {

            //        MyStoryboard msb = CardAnimation.ScaleX_120_Rotate(-90, 0, 150, 200);
            //        msb.card = card2;
            //        msb.Completed += (object sender, EventArgs e) =>
            //        {
            //            //卡片切换为背面
            //            msb.card.isBack = false;
            //            msb.card.isDef = false;
            //            msb.card.SetPic();
            //        };
            //        animator0.Animates.Add(msb);
            //        MyStoryboard msb2 = CardAnimation.ScaleX_021(150);
            //        animator0.Animates.Add(msb2);
            //    }
            //    else if (!card2.isBack)
            //    {
            //        MyStoryboard msb = CardAnimation.Rotate_D2A(150);
            //        msb.card = card2;
            //        msb.Completed += (object sender_, EventArgs e_) =>
            //        {
            //            msb.card.isDef = false;
            //        };
            //        animator0.Animates.Add(msb);
            //    }
            //    animator0.Begin(card2);

                
            //}
            //else if ( card2 == null )
            //{
               
            //}


            //MyStoryboard msb1 = new MyStoryboard();

            //#region 里侧

            //if (card.isBack)
            //{
            //    if (!card.isDef)
            //    {
            //        //里攻 （先翻旋至消失） （旋转并移动到目的地）
            //        msb1 = CardAnimation.ScaleX_120(150);
            //    }
            //    else if (card.isDef)
            //    {
            //        //里守 （移动到目的地）
            //        //msb1 = CardAnimation.CanvasXY(end, 200);
            //    }

            //    msb1.card = card;
            //    msb1.Completed += (object sender, EventArgs e) =>
            //    {
            //        //卡片切换为正面
            //        msb1.card.isBack = false;
            //        msb1.card.isDef = false;
            //        msb1.card.SetPic();
            //        Canvas.SetBottom(msb1.card, end.Y);
            //        Canvas.SetLeft(msb1.card, end.X);
            //    };
            //    animator.Animates.Add(msb1);

            //    //翻转至显现并移动到目的地
            //    MyStoryboard msb2 = CardAnimation.ScaleX_021_CanvasXY(start,end, 100, 250);
            //    //msb2.FillBehavior = FillBehavior.Stop;
            //    msb2.card = card;
            //    msb2.Completed += (object sender2, EventArgs e) =>
            //    {
            //        msb2.card.BeginAnimation(Canvas.LeftProperty, null);
            //        msb2.card.BeginAnimation(Canvas.TopProperty, null);
            //        msb2.card.SetValue(Canvas.LeftProperty, end.X);
            //        msb2.card.SetValue(Canvas.TopProperty, end.Y);
            //        msb2.card.SetCurrentValue(Canvas.LeftProperty, end.X);
            //        msb2.card.SetCurrentValue(Canvas.TopProperty, end.Y);
                    
            //    };
            //    animator.Animates.Add(msb2);

                

            //}

            //#endregion

            //#region 表侧

            //if (!card.isBack)
            //{

            //    if (!card.isDef)
            //    {
            //        //表攻 （旋转至0度，移动）,只有从 
            //        msb1 = CardAnimation.CanvasXY(end,200);
            //    }
            //    else if (card.isDef)
            //    {
            //        //表攻 （先翻转至消失）
            //        //msb1 = CardAnimation.ScaleX_120(150);
            //        msb1 = CardAnimation.CanvasXY(end, 200);
            //    }

            //    msb1.card = card;
            //    msb1.Completed += (object sender, EventArgs e) =>
            //    {
            //        //msb1.card.isDef = false;
            //        msb1.card.BeginAnimation(Canvas.LeftProperty, null);
            //        msb1.card.BeginAnimation(Canvas.TopProperty, null);
            //        msb1.card.SetValue(Canvas.LeftProperty, end.X);
            //        msb1.card.SetValue(Canvas.TopProperty, end.Y);
            //    };
            //    animator.Animates.Add(msb1);
              
            //}

            //#endregion

            //animator.Begin(card);
            //animator.Animates[animator.Animates.Count - 1].Completed += (object sender, EventArgs e) =>
            //{


            //    Panel.SetZIndex(gd, 0);
            //    Panel.SetZIndex(cv_aim, 0);
            //    if (cv_aim.Equals(mainwindow.card_1_hand) || cv_aim.Equals(mainwindow.card_2_hand))
            //    {
            //        CardOperate.sort_HandCard(cv_aim);
            //    }
            //    else
            //    {
            //        CardOperate.sort(cv_aim, null);
            //    }

            //};

            return false;
        }

        #endregion

        #region <-- 卡片在场地中的移动 -->

        public static bool Move2(Card card,Canvas cv_aim,bool add,Card card2)
        {
            //Grid gd = cv_aim.Parent as Grid;
            //if (gd != null)
            //{
            //    Panel.SetZIndex(gd, 1);
            //}
            //Panel.SetZIndex(cv_aim, 11);

            ////动画栈


            //Canvas cv = Base.getParerent(card);

            //CardAnimation.setTransformGroup(card);

            ////1.获取卡片起始位置相对于目的地的坐标
            //Point start = card.TranslatePoint(new Point(), cv_aim); 

            ////2.获取卡片终止位置相对于目的地的坐标
            //Point end = new Point((cv_aim.ActualWidth - card.ActualWidth) / 2.0, (cv_aim.ActualHeight - card.ActualHeight) / 2.0);
            ////脱离原控件
            //Base.getawayParerent(card);
            //if (cv.Children.Count > 0)
            //{
            //    CardOperate.sort(cv, card);
            //}
            ////getawayParerent(card);
            ////利用1设置初始位置
            //Canvas.SetTop(card, start.Y);
            //Canvas.SetLeft(card, start.X);

            //if (cv.Equals(mainwindow.card_1_Outside) || cv.Equals(mainwindow.card_2_Outside))
            //{
            //    //如果是从除外离开
            //    card.isDef = true;
            //    CardAnimation.setTransformGroup(card);
            //}
            //if (card.isDef)
            //{
            //    //防守需要重新设定起始位置
            //    Canvas.SetTop(card, start.Y - card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2.0));
            //    Canvas.SetLeft(card, start.X + ((card.ActualHeight - card.ActualWidth) / 2.0));
            //}
            ////加入目的地控件
            //if (add)
            //{
            //    cv_aim.Children.Add(card);
            //    TextBlock tb = mainwindow.FindName(cv_aim.Name.Replace("card", "atk")) as TextBlock;
            //    if (tb != null && !card.isBack && !card.sCardType.Contains("魔法") && !card.sCardType.Contains("陷阱") ) tb.SetCurrentValue(TextBlock.TextProperty, card.atk + "/" + card.def);
            //}
            //else if (!add)
            //{
            //    cv_aim.Children.Insert(0, card);
            //}


            //CardAnimation.setTransformGroup(card);

            //#region 处理目标卡位的第一张卡

            //if (card2 != null)
            //{
            //    TextBlock tb = mainwindow.FindName(cv_aim.Name.Replace("card", "atk")) as TextBlock;
            //    tb.SetCurrentValue(TextBlock.TextProperty, card.atk + "/" + card.def);

            //    TransLibrary.StoryboardChain animator0 = new TransLibrary.StoryboardChain();
            //    if (card2.isBack)
            //    {

            //        MyStoryboard msb = CardAnimation.ScaleX_120_Rotate(-90, 0, 150, 200);
            //        msb.card = card2;
            //        msb.Completed += (object sender, EventArgs e) =>
            //        {
            //            //卡片切换为背面
            //            msb.card.isBack = false;
            //            msb.card.isDef = false;
            //            msb.card.SetPic();
            //        };
            //        animator0.Animates.Add(msb);
            //        MyStoryboard msb2 = CardAnimation.ScaleX_021(150);
            //        animator0.Animates.Add(msb2);
            //    }
            //    else if (!card2.isBack)
            //    {
            //        MyStoryboard msb = CardAnimation.Rotate_D2A(150);
            //        msb.card = card2;
            //        msb.Completed += (object sender_, EventArgs e_) =>
            //        {
            //            msb.card.isDef = false;
            //        };
            //        animator0.Animates.Add(msb);
            //    }
            //    animator0.Begin(card2);


            //}
            //else if (card2 == null)
            //{

            //}

            //#endregion

            //TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();

            //#region <-- 目的地为魔陷区时，应变更为攻击形式 -->

            //if ((CardOperate.cv_magictraps_1.Contains(cv_aim) || CardOperate.cv_magictraps_2.Contains(cv_aim)) && card.isDef)
            //{
            //    MyStoryboard msb0 = CardAnimation.Rotate_D2A(200);
            //    card.isDef = false;
            //    msb0.Begin(card);
            //}

            //#endregion

            //#region <-- 当目标区域卡片超过1时，默认识别为叠放操作 -->

            //if (cv_aim.Children.Count > 1 )
            //{
            //    if (card.isBack)
            //    {
            //        if (card.isDef)
            //        {
            //            MyStoryboard msb = CardAnimation.ScaleX_120_Rotate(-90, 0, 150, 200);
            //            msb.card = card;
            //            msb.Completed += (object sender_, EventArgs e_) =>
            //            {
            //                msb.card.isBack = false;
            //                msb.card.isDef = false;
            //                msb.card.SetPic();
            //            };
            //            animator.Animates.Add(msb);

            //        }
            //        else if (!card.isDef)
            //        {
            //            MyStoryboard msb = CardAnimation.ScaleX_120(150);
            //            msb.card = card;
            //            msb.Completed += (object sender_, EventArgs e_) =>
            //            {
            //                msb.card.isBack = false;
            //                msb.card.isDef = false;
            //                msb.card.SetPic();
            //            };
            //            animator.Animates.Add(msb);
            //        }
            //        MyStoryboard msb1 = CardAnimation.ScaleX_021(150);
            //        animator.Animates.Add(msb1);
            //    }
            //    else if (!card.isBack)
            //    {
            //        if (card.isDef)
            //        {
            //            MyStoryboard msb = CardAnimation.Rotate_D2A(200);
            //            msb.card = card;
            //            msb.Completed += (object sender_, EventArgs e_) =>
            //            {
            //                msb.card.isBack = false;
            //                msb.card.isDef = false;
            //            };
            //            animator.Animates.Add(msb);
            //        }
            //    }
                
                

            //}

            //#endregion 

            //#region <-- 处理指示物 -->

            //if (CardOperate.cv_monsters_1.Contains(cv_aim) || CardOperate.cv_monsters_2.Contains(cv_aim))
            //{
            //    if (card.sCardType.Equals("XYZ怪兽") || cv.Children.Count == 0)
            //    {
                            
            //        StackPanel sp = mainwindow.FindName(cv.Name.Replace("card", "sp_sign")) as StackPanel;
            //        if (sp != null && sp.Children.Count > 0)
            //        {
            //            StackPanel sp_aim = mainwindow.FindName(cv_aim.Name.Replace("card", "sp_sign")) as StackPanel;
            //            int count = sp.Children.Count;
            //            for (int i = 0; i < count; i++)
            //            {
            //                Grid gd_ = sp.Children[0] as Grid;
            //                sp.Children.Remove(gd_);
            //                sp_aim.Children.Add(gd_);
            //            }
            //        }

                    
            //    }
            //}

            //#endregion

            //MyStoryboard msb3 = CardAnimation.CanvasXY(end, 200);
            //msb3.card = card;
            //msb3.Completed += (object sender, EventArgs e) =>
            //{
                
            //    Panel.SetZIndex(gd, 0);
            //    Panel.SetZIndex(cv_aim, 0);
            //    msb3.card.BeginAnimation(Canvas.LeftProperty, null);
            //    msb3.card.BeginAnimation(Canvas.TopProperty, null);
            //    msb3.card.SetValue(Canvas.LeftProperty, end.X);
            //    msb3.card.SetValue(Canvas.TopProperty, end.Y);
            //    TextBlock tb = mainwindow.FindName(cv_aim.Name.Replace("card", "atk")) as TextBlock;
            //    if (tb != null && !card.sCardType.Contains("魔法") && !card.sCardType.Contains("陷阱")) tb.SetCurrentValue(TextBlock.TextProperty, msb3.card.atk + "/" + card.def);
            //    if (cv_aim.Children.Count > 1)
            //    {
            //        CardOperate.sort(cv_aim, null);
            //    }
            //};
            //animator.Animates.Add(msb3);
            //animator.Begin(card);
            ////msb3.Begin(card);

            return false;


        }

        #endregion

        #region <-- 送入墓地， -->

        public static bool SendTo(Card card, Canvas cv_aim)
        {
            //Grid gd = cv_aim.Parent as Grid;
            //if (gd != null)
            //{
            //    Panel.SetZIndex(gd, 1);
            //}
            //Panel.SetZIndex(cv_aim, 11);

            ////动画栈

            //Canvas cv = Base.getParerent(card);

            //CardAnimation.setTransformGroup(card);

            ////1.获取卡片起始位置相对于目的地的坐标
            //Point start = card.TranslatePoint(new Point(), cv_aim);

            ////2.获取卡片终止位置相对于目的地的坐标
            //Point end = new Point((cv_aim.ActualWidth - card.ActualWidth) / 2.0, (cv_aim.ActualHeight - card.ActualHeight) / 2.0);
            ////脱离原控件
            //getawayParerent(card);
            ////利用1设置初始位置
            //Canvas.SetTop(card, start.Y);
            //Canvas.SetLeft(card, start.X);

            //if (cv.Equals(mainwindow.card_1_Outside) || cv.Equals(mainwindow.card_2_Outside))
            //{
            //    //如果是从除外离开
            //    card.isDef = true;
            //    CardAnimation.setTransformGroup(card);
            //}
            //if (card.isDef)
            //{
            //    //防守需要重新设定起始位置
            //    Canvas.SetTop(card, start.Y - card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2.0));
            //    Canvas.SetLeft(card, start.X + ((card.ActualHeight - card.ActualWidth) / 2.0));
            //}
            //cv_aim.Children.Add(card);

            //TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();

            //if(card.isBack)
            //{
            //    if (card.isDef)
            //    {
            //        MyStoryboard msb1 = CardAnimation.ScaleX_120_Rotate(-90, 0, 150, 200);
            //        msb1.card = card;
            //        msb1.Completed += (object sender, EventArgs e) =>
            //        {
            //            //卡片切换为正面
            //            msb1.card.isBack = false;
            //            msb1.card.isDef = false;
            //            msb1.card.SetPic();
            //        };
            //        animator.Animates.Add(msb1);
            //    }
            //    else if (!card.isDef)
            //    {
            //        MyStoryboard msb1 = CardAnimation.ScaleX_120(150);
            //        msb1.card = card;
            //        msb1.Completed += (object sender, EventArgs e) =>
            //        {
            //            //卡片切换为背面
            //            msb1.card.isBack = false;
            //            msb1.card.isDef = false;
            //            msb1.card.SetPic();
            //        };
            //        animator.Animates.Add(msb1);                   
            //    }
            //    MyStoryboard msb2 = CardAnimation.ScaleX_021(150);
            //    animator.Animates.Add(msb2);
            //}
            //else if (!card.isBack)
            //{
            //    if (card.isDef)
            //    {
            //        MyStoryboard msb1 = CardAnimation.Rotate_D2A(200);
            //        card.isDef = false;
            //        animator.Animates.Add(msb1);

            //    }
            //}

            //MyStoryboard msb = CardAnimation.CanvasXY(end, 200);
            //animator.Animates.Add(msb);
            //animator.Animates[animator.Animates.Count - 1].Completed += (object sender, EventArgs e) =>
            //{
            //    Panel.SetZIndex(gd, 0);
            //    Panel.SetZIndex(cv_aim, 0);
            //    card.BeginAnimation(Canvas.LeftProperty, null);
            //    card.BeginAnimation(Canvas.TopProperty, null);
            //    card.SetValue(Canvas.LeftProperty, end.X);
            //    card.SetValue(Canvas.TopProperty, end.Y);
            //    if (cv_aim.Equals(mainwindow.card_1_hand) || cv_aim.Equals(mainwindow.card_2_hand))
            //    {
            //        CardOperate.sort_HandCard(cv_aim);
            //    }
            //    else
            //    {
            //        CardOperate.sort(cv_aim, null);
            //    }

            //};
            //animator.Begin(card);

            return false;
        }

        #endregion

        #region <-- 卡片返回 -->

        /*  动作：返回卡组，返回手牌，返回额外
         *相似点：都只能以里攻形式返回
         *  处理：可能的返回情况
         *        1.里守（旋转为0度，移动）
         *        2.里攻（移动）
         *        3.表守（翻旋至0度，切换为背面，移动）
         *        4.表攻（翻转，切换为背面，移动）
         */

        public static bool CardBackTo(Card card,Canvas cv_aim)
        {
            ////初始化动画栈


            //Grid gd = cv_aim.Parent as Grid;
            //if (gd != null)
            //{
            //    Panel.SetZIndex(gd, 1);
            //}
            //Panel.SetZIndex(cv_aim, 1);

            //Canvas cv = Base.getParerent(card);

            ////1.获取卡片起始位置相对于目的地的坐标
            //Point start = card.TranslatePoint(new Point(), cv_aim);
            
            ////2.获取卡片终止位置相对于目的地的坐标
            //Point end = new Point((cv_aim.ActualWidth - card.ActualWidth) / 2.0, (cv_aim.ActualHeight - card.ActualHeight) / 2.0);
            ////如果目的地是手卡,且手卡不为0（优先回到靠近卡组第一张的位置）
            //if (cv_aim.Equals(mainwindow.card_1_hand) || cv_aim.Equals(mainwindow.card_2_hand))
            //{
            //    if (cv_aim.Children.Count > 0)
            //    {
            //        Card card_last = cv_aim.Children[cv_aim.Children.Count - 1] as Card;
            //        end = new Point(Canvas.GetLeft(card_last), Canvas.GetTop(card_last));
            //    }
            //}
            ////脱离原控件
            //getawayParerent(card);
            ////利用1设置初始位置
            //Canvas.SetTop(card, start.Y);
            //Canvas.SetLeft(card, start.X);
            //if (cv.Equals(mainwindow.card_1_Outside) || cv.Equals(mainwindow.card_2_Outside))
            //{
            //    //如果是从除外离开
            //    card.isDef = true;
            //    CardAnimation.setTransformGroup(card);
            //}
            //if (card.isDef)
            //{
            //    //防守需要重新设定起始位置
            //    Canvas.SetTop(card, start.Y - card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2.0));
            //    Canvas.SetLeft(card, start.X + ((card.ActualHeight - card.ActualWidth) / 2.0));
            //}
            ////加入目的地控件
            //cv_aim.Children.Add(card);

            //CardAnimation.setTransformGroup(card);

            //TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();

            //MyStoryboard msb1 = new MyStoryboard();

            //#region 里侧

            //if (card.isBack)
            //{              
            //    if (card.isDef)
            //    {   
            //        //里守 （旋转并移动到目的地）
            //        msb1 = CardAnimation.Rotate_CanvasXY(-90, 0, start,end, 150, 200);                   
            //    }
            //    else if (!card.isDef)
            //    {
            //        //里攻 （移动到目的地）
            //        msb1 = CardAnimation.CanvasXY(end, 200);
            //    }

            //    msb1.card = card;
            //    msb1.Completed += (object sender, EventArgs e) =>
            //    {
            //        msb1.card.isDef = false;
            //        msb1.card.SetCurrentValue(Canvas.LeftProperty, end.X);
            //        msb1.card.SetCurrentValue(Canvas.TopProperty, end.Y);
            //    };
            //    animator.Animates.Add(msb1);
                
            //}

            //#endregion

            //#region 表侧

            //if (!card.isBack)
            //{
                
            //    if (card.isDef)
            //    {
            //        //表防 （先翻旋至消失） 
            //        msb1 = CardAnimation.ScaleX_120_Rotate(-90, 0, 150, 200);
            //    }
            //    else if (!card.isDef)
            //    {
            //        //表攻 （先翻转至消失）
            //        msb1 = CardAnimation.ScaleX_120(150);
            //    }

            //    msb1.card = card;
            //    msb1.Completed += (object sender, EventArgs e) =>
            //    {
            //        //卡片切换为背面
            //        msb1.card.isBack = true;
            //        msb1.card.isDef = false;
            //        msb1.card.SetPic();
            //    };
            //    animator.Animates.Add(msb1);

            //    //翻转至显现并移动到目的地
            //    MyStoryboard msb2 = CardAnimation.ScaleX_021_CanvasXY(end, 150, 200);
            //    msb2.card = card;
            //    msb2.Completed += (object sender2, EventArgs e) =>
            //    {
                   
            //        msb2.card.SetCurrentValue(Canvas.LeftProperty, end.X);
            //        msb2.card.SetCurrentValue(Canvas.TopProperty, end.Y);
            //        //Canvas.SetBottom(msb1.card, end.Y);
            //        //Canvas.SetLeft(msb1.card, end.X);
            //    };
            //    animator.Animates.Add(msb2);
            //}

            //#endregion
          
            //animator.Begin(card);
            //animator.Animates[animator.Animates.Count - 1].Completed += (object sender, EventArgs e) =>
            //{
            //    Panel.SetZIndex(gd, 0);
            //    Panel.SetZIndex(cv_aim, 0);
            //    card.BeginAnimation(Canvas.LeftProperty, null);
            //    card.BeginAnimation(Canvas.TopProperty, null);
            //    card.SetValue(Canvas.LeftProperty, end.X);
            //    card.SetValue(Canvas.TopProperty, end.Y);
            //    if ( cv_aim.Equals(mainwindow.card_1_hand) || cv_aim.Equals(mainwindow.card_2_hand))
            //    {
            //        CardOperate.sort_HandCard(cv_aim);
            //    }
            //};
            
            return false;
 
        }

        #endregion

        #region <-- 返回手卡（可废弃） -->

        /// <summary>
        /// 卡片回手
        /// </summary>
        /// <param name="cv"></param>
        /// <param name="card"></param>
        /// <param name="cv_aim"></param>
        /// <returns></returns>
        public static bool Back2Hand(Canvas cv, Card card, Canvas cv_aim)
        {
            #region 清除指示物

            StackPanel sp = mainwindow.FindName(cv.Name.Replace("card", "sp_sign")) as StackPanel;
            if (sp != null) sp.Children.Clear();

            #endregion

            if (card.isDef && card.isBack)
            {
                return Back2Hand_1(card, cv_aim);
            }
            else if (card.isDef && !card.isBack)
            {
                return Back2Hand_2(card,cv_aim);
            }
            else if (!card.isDef && card.isBack)
            {
                Back2Hand_3(cv,card, cv_aim);
            }
            else if (!card.isDef && !card.isBack)
            {
                return Back2Hand_4(cv,card, cv_aim ,"2");
            }

            return false;
 
        }

        #endregion

        #region <-- 里守回手 -->

        public static bool Back2Hand_1(Card card, Canvas cv_aim)
        {
            //Grid gd = cv_aim.Parent as Grid;
            //if (gd != null)
            //{
            //    Panel.SetZIndex(gd, 1);
            //}
            //Panel.SetZIndex(cv_aim, 1);

            //Canvas cv = Base.getParerent(card);

            ////1.获取卡片相对于目的地的距离
            //Point start = card.TranslatePoint(new Point(), cv_aim);
            //Point end;
            ////2.获取卡片在卡框中的相对距离
            //if (cv_aim.Children.Count>0)
            //{
            //    Card card_last = cv_aim.Children[cv_aim.Children.Count - 1] as Card;
            //    end = new Point(Canvas.GetLeft(card_last), Canvas.GetTop(card_last));
            //}
            //else
            //{
            //    end = new Point((cv_aim.ActualWidth - card.ActualWidth) / 2.0, (cv_aim.ActualHeight - card.ActualHeight) / 2.0);
            //}
            ////脱离原控件
            //Base.getawayParerent(card);
            ////利用1设置初始位置
            //Canvas.SetTop(card, start.Y - card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2.0));
            //Canvas.SetLeft(card, start.X + ((card.ActualHeight - card.ActualWidth)/2.0));
            ////- card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2.0)
            ////加入目的地控件
            //cv_aim.Children.Add(card);
       
            //MyStoryboard msb = CardAnimation.Rotate_CanvasXY(-90, 0, end, 150, 200);
            //msb.Completed += (object sender, EventArgs e) =>
            //{
            //    if (gd != null)
            //    {
            //        Panel.SetZIndex(gd, 0);
            //    }
            //    Panel.SetZIndex(cv_aim, 0);

            //    //清空属性和动画的关联绑定
            //    card.BeginAnimation(Canvas.LeftProperty, null);
            //    card.BeginAnimation(Canvas.TopProperty, null);

            //    Canvas.SetTop(card, end.Y);
            //    Canvas.SetLeft(card, end.X);

            //    card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //    card.RenderTransform.SetValue(RotateTransform.AngleProperty, (double)0);

            //    card.isDef = false;

            //    CardOperate.sort_HandCard(cv_aim);
            //};

            //msb.Begin(card);

            return true;
        }

        #endregion

        #region <-- 表守回手 -->

        public static bool Back2Hand_2(Card card, Canvas cv_aim)
        {
            //Grid gd = cv_aim.Parent as Grid;
            //if (gd != null)
            //{
            //    Panel.SetZIndex(gd, 2);
            //}
            //Panel.SetZIndex(cv_aim, 2);

            //Canvas cv = Base.getParerent(card);

            ////1.获取卡片相对于目的地的距离
            //Point start = card.TranslatePoint(new Point(), cv_aim);
            //Point end;
            ////2.获取卡片在卡框中的相对距离
            //if (cv_aim.Children.Count>0)
            //{
            //    Card card_last = cv_aim.Children[cv_aim.Children.Count - 1] as Card;
            //    end = new Point(Canvas.GetLeft(card_last), Canvas.GetTop(card_last));
            //}
            //else
            //{
            //    end = new Point((cv_aim.ActualWidth - card.ActualWidth) / 2.0, (cv_aim.ActualHeight - card.ActualHeight) / 2.0);
            //}
            ////脱离原控件
            //Base.getawayParerent(card);
            ////利用1设置初始位置
            //Canvas.SetTop(card, start.Y - card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2.0));
            //Canvas.SetLeft(card, start.X + ((card.ActualHeight - card.ActualWidth)/2.0));
            ////- card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2.0)
            ////加入目的地控件
            //cv_aim.Children.Add(card);

            //TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();

            //MyStoryboard msb = CardAnimation.ScaleX_120_Rotate(-90, 0, 500, 1000);

            ////MyStoryboard msb = CardAnimation.ScaleY_120(5000);
            ////MyStoryboard msb2 = CardAnimation.scalX_120(card,100);
            //msb.Completed += (object c, EventArgs d) =>
            //{
            //    card.isBack = true;
            //    card.SetPic();
            //};
            //animator.Animates.Add(msb);

            //MyStoryboard msb2 = CardAnimation.ScaleX_021_CanvasXY(end, 500, 1000);
            //msb2.Completed += (object sender, EventArgs e) =>
            //{
            //    if (gd != null)
            //    {
            //        Panel.SetZIndex(gd, 0);
            //    }
            //    Panel.SetZIndex(cv_aim, 0);

            //    card.BeginAnimation(Canvas.LeftProperty, null);
            //    card.BeginAnimation(Canvas.TopProperty, null);

            //    Canvas.SetTop(card, end.Y);
            //    Canvas.SetLeft(card, end.X);

            //    CardOperate.sort_HandCard(cv_aim);
 
            //};
            //animator.Animates.Add(msb2);

            //animator.Begin(card);

            return true;
        }

        #endregion 

        #region <-- 里攻回手 -->

        public static bool Back2Hand_3(Canvas cv,Card card, Canvas cv_aim)
        {
            //Grid gd = cv_aim.Parent as Grid;
            //if (gd != null)
            //{
            //    Panel.SetZIndex(gd, 2);
            //}
            //Panel.SetZIndex(cv_aim, 2);

            ////1.获取卡片相对于目的地的距离
            //Point start = card.TranslatePoint(new Point(), cv_aim);
            //Point end;
            ////2.获取卡片在卡框中的相对距离
            //if (cv_aim.Children.Count > 0)
            //{
            //    Card card_last = cv_aim.Children[cv_aim.Children.Count - 1] as Card;
            //    end = new Point(Canvas.GetLeft(card_last), Canvas.GetTop(card_last));
            //}
            //else
            //{
            //    end = new Point((cv_aim.ActualWidth - card.ActualWidth) / 2.0, (cv_aim.ActualHeight - card.ActualHeight) / 2.0);
            //}
            ////脱离原控件
            //Base.getawayParerent(card);
            ////利用1设置初始位置
            //Canvas.SetTop(card, start.Y);
            //Canvas.SetLeft(card, start.X);
            ////加入目的地控件
            //cv_aim.Children.Add(card);
            
            //if (!cv.Name.Equals("card_2_Deck") && !cv.Name.Equals("card_2_Gravyard") && !cv.Name.Equals("card_2_Outside"))
            //{
            //    CardOperate.sort_Canvas(cv);
            //}

            //MyStoryboard msb = CardAnimation.CanvasXY(end,200);
            //msb.Completed += (object sender, EventArgs e) =>
            //{
            //    if (gd != null)
            //    {
            //        Panel.SetZIndex(gd, 0);
            //    }
            //    Panel.SetZIndex(cv_aim, 0);

            //    card.BeginAnimation(Canvas.LeftProperty, null);
            //    card.BeginAnimation(Canvas.TopProperty, null);

            //    Canvas.SetTop(card, end.Y);
            //    Canvas.SetLeft(card, end.X);

            //    CardOperate.sort_HandCard(cv_aim);
            //};

            //msb.Begin(card);

            return true;
        }

        #endregion

        #region <-- 表攻回手 -->

        public static bool Back2Hand_4(Canvas cv,Card card, Canvas cv_aim , string field)
        {
            //CardAnimation.setTransformGroup(card);

            //Grid gd = cv_aim.Parent as Grid;
            //if (gd != null)
            //{
            //    Panel.SetZIndex(gd, 2);
            //}
            //Panel.SetZIndex(cv_aim, 2);

            ////1.获取卡片相对于目的地的距离
            //Point start = card.TranslatePoint(new Point(), cv_aim);
            //Point end;
            ////2.获取卡片在卡框中的相对距离
            //if (cv_aim.Children.Count > 0)
            //{
            //    Card card_last = cv_aim.Children[cv_aim.Children.Count - 1] as Card;
            //    end = new Point(Canvas.GetLeft(card_last), Canvas.GetTop(card_last));
            //}
            //else
            //{
            //    end = new Point((cv_aim.ActualWidth - card.ActualWidth) / 2.0, (cv_aim.ActualHeight - card.ActualHeight) / 2.0);
            //}
            ////脱离原控件
            //Base.getawayParerent(card);
            ////利用1设置初始位置
            //Canvas.SetTop(card, start.Y);
            //Canvas.SetLeft(card, start.X);
            ////加入目的地控件
            //cv_aim.Children.Add(card);

            //TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();

            //MyStoryboard msb = CardAnimation.ScaleX_120(100);
            //msb.Completed += (object sender, EventArgs e) =>
            //{
            //    card.isBack = true;
            //    card.SetPic();
            //};
            //animator.Animates.Add(msb);

            //if (!cv.Name.Equals("card_2_Deck") && !cv.Name.Equals("card_2_Graveyard") && !cv.Name.Equals("card_2_Outside"))
            //{
            //    CardOperate.sort_Canvas(cv);
            //}  
       
            //MyStoryboard msb2 = CardAnimation.ScaleX_021_CanvasXY(end,150,200);
            //msb2.Completed += (object sender, EventArgs e) =>
            //{
            //    if (gd != null)
            //    {
            //        Panel.SetZIndex(gd, 0);
            //    }
            //    Panel.SetZIndex(cv_aim, 0);

            //    card.BeginAnimation(Canvas.LeftProperty, null);
            //    card.BeginAnimation(Canvas.TopProperty, null);

            //    Canvas.SetTop(card, end.Y);
            //    Canvas.SetLeft(card, end.X);

            //    CardOperate.sort_HandCard(cv_aim);
            //};

            //animator.Animates.Add(msb2);

            //animator.Begin(card);

            return true;
 
        }

        #endregion

        #region <-- 卡片移动 -->

        /// <summary>
        /// 卡片移动
        /// </summary>
        /// <param name="cv"></param>
        /// <param name="card"></param>
        /// <param name="cv_aim"></param>
        /// <param name="cv_num"></param>
        /// <returns></returns>
        public static bool Move(Canvas cv,Card card, Canvas cv_aim,int cv_num)
        {
            //Panel.SetZIndex(mainwindow.battle_zone_left, 1);
            //Panel.SetZIndex(cv_aim, 1);

            ////1.获取卡片相对于目的地的距离
            //Point start = card.TranslatePoint(new Point(), cv_aim);
            //Console.WriteLine(start.X);
            ////2.获取卡片在卡框中的相对距离
            //Point end = new Point((cv_aim.ActualWidth - card.ActualWidth) / 2.0, (cv_aim.ActualHeight - card.ActualHeight) / 2.0);
            ////脱离原控件
            //Base.getawayParerent(card);
            ////利用1设置初始位置
            //Canvas.SetTop(card, start.Y);
            //Canvas.SetLeft(card, start.X);
            ////加入目的地控件
            //FrameworkElement fe = cv_aim.Parent as FrameworkElement;
            //if (fe != null && fe.Name.Equals("battle_zone_middle"))
            //{
            //    if (cv_aim.Children.Count > 0)
            //    {
            //        Card card2 = cv_aim.Children[cv_aim.Children.Count - 1] as Card;
            //        if (card2.sCardType.Equals("XYZ怪兽"))
            //        {
            //            cv_aim.Children.Insert(0, card);
            //        }
            //        else
            //        {
            //            if (card2.isDef && card2.isBack)
            //            {
            //                CardAnimation.Rotate2Atk(card2);
            //                card2.isBack = false;
            //                card2.isDef = false;
            //            }
            //            else if (card2.isDef)
            //            {
            //                card2.RenderTransform = new RotateTransform(-90);
            //                CardAnimation.Def_or_Atk(card2);
            //                card2.isDef = false;
            //            }

            //            //if (card.isDef && card.isBack)
            //            //{
            //            //    CardAnimation.Rotate2Atk(card);
            //            //    card.isBack = false;
            //            //    card.isDef = false;
            //            //}
            //            //else if (card.isDef)
            //            //{
            //            //    card.RenderTransform = new RotateTransform(-90);
            //            //    CardAnimation.Def_or_Atk(card);
            //            //    card.isDef = false;
            //            //}
            //            cv_aim.Children.Add(card);
            //        }

            //    }
            //    else
            //    {
            //        cv_aim.Children.Add(card);
            //    }
            //}
            //else
            //{
            //    cv_aim.Children.Add(card);
            //}
            


            //TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();

            //if (cv.Name.Length == 8 || cv.Name.Length == 9)
            //{
            //    int witch;
            //    if (int.TryParse(cv.Name.Substring(7), out witch))
            //    {
            //        if (witch > 5 && witch < 11)
            //        {

            //        }
            //    }
            //}

            //if (cv_aim.Name.Contains("Graveyard") || cv_aim.Name.Contains("Outside"))
            //{
            //    #region 清除指示物

            //    StackPanel sp = mainwindow.FindName(cv.Name.Replace("card", "sp_sign")) as StackPanel;
            //    if (sp != null) sp.Children.Clear();

            //    #endregion
            //}

            //if (cv_aim.Name.Substring(7).Equals("Graveyard"))
            //{              
            //    #region 送去墓地的场合
                                             
            //    if (cv.Name.Equals("card_2_hand"))
            //    {
            //        RotateTransform rotate = new RotateTransform(0);
            //        ScaleTransform scale = new ScaleTransform();
            //        TransformGroup group = new TransformGroup();
            //        group.Children.Add(scale);
            //        group.Children.Add(rotate);
            //        card.RenderTransform = group;

            //        Canvas.SetTop(card, start.Y);
            //        Canvas.SetLeft(card, start.X);

            //        MyStoryboard msb1 = CardAnimation.scalX_120(card,100);
            //        msb1.Completed += (object sender, EventArgs e) =>
            //        {
            //            msb1.card.isBack = false;
            //            msb1.card.isDef = false;
            //            card.SetPic();
            //        };

            //        animator.Animates.Add(msb1);

            //        MyStoryboard msb2 = CardAnimation.Send2OpGraveyard1(card, start, end, 0, 0, 150);
            //        msb2.Completed += (object sender, EventArgs e) =>
            //        {

            //            msb2.card.BeginAnimation(Canvas.LeftProperty, null);
            //            msb2.card.BeginAnimation(Canvas.TopProperty, null);

            //            Canvas.SetTop(msb2.card, end.Y);
            //            Canvas.SetLeft(msb2.card, end.X);

            //            //CardOperate.sort_SingleCard(card);

            //            //需要修改的地方
            //            CardOperate.sort_HandCard(mainwindow.card_1_hand);
            //            CardOperate.sort_HandCard(mainwindow.card_2_hand);
            //        };

            //        animator.Animates.Add(msb2);

            //        animator.Begin();
            //         //MyStoryboard msb = CardAnimation.MoveAnimation2

            //        return true;
            //    }

            //    if (cv.Children.Count>0)
            //    {
            //        Card temp = cv.Children[cv.Children.Count - 1] as Card;
            //        if (temp.sCardType.Equals("XYZ怪兽") && temp.isDef)
            //        {
            //            CardOperate.sort_Canvas(cv);
            //        }
            //        else
            //        {
            //            CardOperate.sort_Canvas(cv);
            //        }
            //    }

                

            //    if (card.isDef)
            //    {                    
            //        #region 防守

            //        RotateTransform rotate = new RotateTransform(-90);
            //        ScaleTransform scale = new ScaleTransform();
            //        TransformGroup group = new TransformGroup();
            //        group.Children.Add(scale);
            //        group.Children.Add(rotate);
            //        card.RenderTransform = group;

            //        start.X = start.X + ((card.ActualHeight - card.ActualWidth)/2.0);
            //        start.Y = start.Y - card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2.0);

            //        Canvas.SetTop(card, start.Y);
            //        Canvas.SetLeft(card, start.X);

            //        if (card.isBack)
            //        {
            //            #region 里守

            //            //翻开中
            //            MyStoryboard msb1 = CardAnimation.scalX_120(card, 1000);
            //            msb1.Completed += (object sender, EventArgs e) =>
            //            {
            //                msb1.card.isBack = false;
            //                msb1.card.isDef = false;
            //                card.SetPic();
            //            };

            //            animator.Animates.Add(msb1);

            //            //边翻开，边旋转，边移动
            //            MyStoryboard msb2 = CardAnimation.Send2OpGraveyard1(card, start, end, 90, 0, 1000);
            //            msb2.Completed += (object sender, EventArgs e) =>
            //            {
            //                //从移动面板Canvas分离
            //                //mainwindow.MySpace.Children.Remove(msb2.card);

            //                //清空属性和动画的关联绑定
            //                msb2.card.BeginAnimation(Canvas.LeftProperty, null);
            //                msb2.card.BeginAnimation(Canvas.TopProperty, null);

            //                Canvas.SetTop(msb2.card, 0);
            //                Canvas.SetLeft(msb2.card, 0);

            //                msb2.card.RenderTransform = null;

            //                //cv_aim.Children.Add(card);

            //                CardOperate.sort_SingleCard(card);
            //            };

            //            animator.Animates.Add(msb2);

            //            #endregion
            //        }
            //        else if (!card.isBack)
            //        {

            //            #region 表守

            //            //边旋转，边移动
            //            MyStoryboard msb1 = CardAnimation.Send2OpGraveyard2(card, start, end,90,180, 300);
            //            msb1.Completed += (object sender, EventArgs e) =>
            //            {
            //                //清空属性和动画的关联绑定
            //                msb1.card.BeginAnimation(Canvas.LeftProperty, null);
            //                msb1.card.BeginAnimation(Canvas.TopProperty, null);

            //                Canvas.SetTop(msb1.card, 0);
            //                Canvas.SetLeft(msb1.card, 0);

            //                msb1.card.RenderTransform = null;                         
            //                CardOperate.sort_SingleCard(card);

                    
            //            };
            //            animator.Animates.Add(msb1);

            //            #endregion
            //        }
               
            //        #endregion
            //    }
            //    else if (!card.isDef)
            //    {
            //        #region 攻击

            //        if (!card.isBack)
            //        {
            //            #region 表攻
            //            RotateTransform rotate = new RotateTransform(0);
            //            ScaleTransform scale = new ScaleTransform();
            //            TransformGroup group = new TransformGroup();
            //            group.Children.Add(scale);
            //            group.Children.Add(rotate);
            //            card.RenderTransform = group;

            //            MyStoryboard msb = CardAnimation.MoveAnimation2(card, start, end, 200);
            //            msb.Completed += (object sender, EventArgs e) =>
            //            {                

            //                //清空属性和动画的关联绑定
            //                msb.card.BeginAnimation(Canvas.LeftProperty, null);
            //                msb.card.BeginAnimation(Canvas.TopProperty, null);

            //                Canvas.SetTop(msb.card, 0);
            //                Canvas.SetLeft(msb.card, 0);

            //                msb.card.RenderTransform = null;

            //                CardOperate.sort_SingleCard(card);
            //            };

            //            animator.Animates.Add(msb);

            //            #endregion
            //        }
            //        else if (card.isBack)
            //        {
            //            #region 里攻

            //            #endregion
            //        }

            //        #endregion                 
            //    }

            //    #endregion
            //}
            //else
            //{
            //    //RotateTransform rotate = new RotateTransform(0);
            //    //ScaleTransform scale = new ScaleTransform();
            //    //TransformGroup group = new TransformGroup();
            //    //group.Children.Add(scale);
            //    //group.Children.Add(rotate);
            //    //card.RenderTransform = group;

            //    #region 场地中的移动
                
            //    #region 处理原位置
            //    if (   
            //           !cv.Name.Substring(7).Equals("Graveyard") 
            //        && !cv.Name.Substring(7).Equals("Outside") 
            //        && !cv.Name.Substring(7).Equals("Graveyard") 
            //        && !cv.Name.Substring(7).Equals("Left") 
            //        && !cv.Name.Substring(7).Equals("Right")
            //        && !cv.Name.Substring(7).Equals("Area"))
            //    {
                    

            //    }
            //    #endregion


            //    if (cv_aim.Children.Count > 1)
            //    {
            //        #region 目标区域存在卡
            //        if (cv_aim.Name.Contains("Left")||cv_aim.Name.Contains("Right"))
            //        {
                        
            //        }
            //        if (card.isDef && card.isBack)
            //        {
            //            //RotateTransform rotate1 = group.Children[1] as RotateTransform;
            //            //rotate1.Angle = 180;
            //            MyStoryboard msb1 = CardAnimation.ScaleX_120_Rotate(-90, 0, 150, 150);
            //            msb1.card = card;
            //            //MyStoryboard msb1 = CardAnimation.scalX_120_rotate_2702180(card);
            //            msb1.Completed += (object c, EventArgs d) =>
            //            {
            //                msb1.card.isBack = false;
            //                msb1.card.isDef = false;
            //                msb1.card.SetPic();
            //            };
            //            animator.Animates.Add(msb1);



            //            MyStoryboard msb2 = CardAnimation.ScaleX_021(150);

            //            animator.Animates.Add(msb2);
            //            card.isBack = false;
            //            card.isDef = false;

            //            start.X = start.X + ((card.ActualHeight - card.ActualWidth) / 2.0);
            //            start.Y = start.Y - card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2.0);
            //        }
            //        else if (card.isDef)
            //        {
            //            MyStoryboard msb3 = CardAnimation.Rotate(card, -270, -180);
            //            //CardAnimation.Def_or_Atk(card);
            //            animator.Animates.Add(msb3);
            //            card.isDef = false;

            //            start.X = start.X + ((card.ActualHeight - card.ActualWidth) / 2.0);
            //            start.Y = start.Y - card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2.0);
            //        }

            //        #endregion

            //    }
            //    else
            //    {

            //        if (card.isDef)
            //        {
            //            start.X = start.X + ((card.ActualHeight - card.ActualWidth) / 2.0);
            //            start.Y = start.Y - card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2.0);
            //        }

            //        CardAnimation.setTransformGroup(card);

            //        //if (card.isDef && card.isBack)
            //        //{
            //        //    card.RenderTransform = new RotateTransform(-90);
            //        //    CardAnimation.setTransformGroup(card);
            //        //}
            //        //else if (card.isDef)
            //        //{

            //        //    card.RenderTransform = new RotateTransform(-90);

            //        //}
                    

            //        #region 
                    
            //        if (cv.Name.Length == 8 || cv.Name.Length == 9)
            //        {

            //            int witch;
            //            if (int.TryParse(cv_aim.Name.Substring(7), out witch))
            //            {
            //                #region 来自怪物区
            //                if (witch > 0 && witch < 6)
            //                {
                                
            //                }
            //                if (witch > 5 && witch < 11)
            //                {
            //                    if (cv.Children.Count > 0)
            //                    {
            //                        int count = cv.Children.Count;
            //                        for (int i = 0; i < count; i++)
            //                        {
            //                            Card card2 = cv.Children[0] as Card;
            //                            RotateTransform rotate2 = new RotateTransform(0);
            //                            ScaleTransform scale2 = new ScaleTransform();
            //                            TransformGroup group2 = new TransformGroup();
            //                            group2.Children.Add(rotate2);
            //                            group2.Children.Add(scale2);
            //                            card2.RenderTransform = group2;

            //                            //1.获取卡片相对于目的地的距离
            //                            Point start2 = card2.TranslatePoint(new Point(), cv_aim);
            //                            //2.获取卡片在卡框中的相对距离
            //                            Point end2 = new Point((cv_aim.ActualWidth - card2.Width) / 2.0, (cv_aim.ActualHeight - card2.Height) / 2.0);
            //                            //脱离原控件
            //                            Base.getawayParerent(card2);
            //                            //利用1设置初始位置
            //                            Canvas.SetTop(card2, start2.Y);
            //                            Canvas.SetLeft(card2, start2.X);
            //                            //加入目的地控件
            //                            cv_aim.Children.Add(card2);

            //                            MyStoryboard sb2 = CardAnimation.MoveAnimation2(card2, start2, end2,1000);
            //                            sb2.Completed += (object sender, EventArgs e) =>
            //                            {
            //                                //从移动面板Canvas分离
            //                                //Base.getawayParerent(sb2.card);
            //                                //mainwindow.MySpace.Children.Remove(sb2.card);

            //                                //清空属性和动画的关联绑定
            //                                sb2.card.BeginAnimation(Canvas.LeftProperty, null);
            //                                sb2.card.BeginAnimation(Canvas.TopProperty, null);


            //                                //cv_aim.Children.Add(sb2.card);

            //                                //Canvas.SetTop(sb2.card, 0);
            //                                //Canvas.SetLeft(sb2.card, 0);

            //                                CardOperate.sort(cv_aim, null);

            //                            };
            //                            animator.Animates.Add(sb2);
            //                        }
            //                    }
            //                }

            //                #endregion
            //            }
            //        }

                    
 

            //        #endregion
            //    }


            //    Canvas.SetTop(card, start.Y);
            //    Canvas.SetLeft(card, start.X);

            //    //mainwindow.MySpace.Children.Add(card);

            //    MyStoryboard sb = CardAnimation.MoveAnimation2(card, start, end, 200);
            //    sb.Completed += (object sender, EventArgs e) =>
            //    {
            //        #region 移动完成后

            //        #region 转移指示物

            //        if (CardOperate.cv_monsters_1.Contains(cv_aim) || CardOperate.cv_monsters_2.Contains(cv_aim))
            //        {
            //            if (sb.card.sCardType.Equals("XYZ怪兽") || cv.Children.Count == 0)
            //            {
                            
            //                StackPanel sp = mainwindow.FindName(cv.Name.Replace("card", "sp_sign")) as StackPanel;
            //                if (sp != null && sp.Children.Count > 0)
            //                {
            //                    StackPanel sp_aim = mainwindow.FindName(cv_aim.Name.Replace("card", "sp_sign")) as StackPanel;
            //                    int count = sp.Children.Count;
            //                    for (int i = 0; i < count; i++)
            //                    {
            //                        Grid gd = sp.Children[0] as Grid;
            //                        sp.Children.Remove(gd);
            //                        sp_aim.Children.Add(gd);
            //                    }
            //                }

            //                sp.Children.Clear();
            //            }
            //        }

            //        if (cv.Name.Length == 8 || cv.Name.Length == 9)
            //        {

            //            int witch;
            //            if (int.TryParse(cv_aim.Name.Substring(7), out witch))
            //            {
            //                #region 来自怪物区
            //                if (witch > 0 && witch < 6)
            //                {
                                
            //                }
            //                if (witch > 5 && witch < 11)
            //                {
            //                    StackPanel sp = mainwindow.FindName(cv.Name.Replace("card", "sp_sign")) as StackPanel;
            //                    if (sp != null && sp.Children.Count > 0)
            //                    {
            //                        StackPanel sp_aim = mainwindow.FindName(cv_aim.Name.Replace("card", "sp_sign")) as StackPanel;
            //                        int count = sp.Children.Count;
            //                        for (int i = 0; i < count; i++)
            //                        {
            //                            Grid gd = sp.Children[0] as Grid;
            //                            sp.Children.Remove(gd);
            //                            sp_aim.Children.Add(gd);
            //                        }
            //                    }
            //                }

            //                #endregion
            //            }
            //        }

                    

            //        #endregion

            //        ////从移动面板Canvas分离
            //        //mainwindow.MySpace.Children.Remove(sb.card);

            //        //清空属性和动画的关联绑定
            //        sb.card.BeginAnimation(Canvas.LeftProperty, null);
            //        sb.card.BeginAnimation(Canvas.TopProperty, null);

            //        //if (card.isDef)
            //        //{
            //        //    card.RenderTransform = new RotateTransform(-90);
            //        //}
            //        //else
            //        //{
            //        //    card.RenderTransform = null;
            //        //}




            //        Canvas.SetTop(sb.card, end.Y);
            //        Canvas.SetLeft(sb.card, end.X);

            //        CardOperate.sort(cv_aim, null);
            //        //cv_aim.Children.Add(sb.card);


            //        switch (cv_aim.Name)
            //        {
            //            case "card_2_Graveyard":
            //                //CardOperate.card_FrontAtk(card);
            //                CardOperate.sort_SingleCard(card);
            //                break;
            //            default:
            //                //CardOperate.sort_Canvas(cv_aim);
            //                break;
            //        }
                    

                    

            //        //CardOperate.sort_HandCard("2");
            //        CardOperate.sort_HandCard(mainwindow.card_2_hand);

            //        sb.Remove(sb.card);
            //        sb = null;
            //        GC.Collect();
            //        //Console.WriteLine("清理内存");

            //        #endregion
            //    };

                
            //    animator.Animates.Add(sb);
            //    animator.Animates[animator.Animates.Count - 1].Completed += (object sender, EventArgs e) =>
            //    {
            //        Panel.SetZIndex(mainwindow.battle_zone_left, 0);
            //        Panel.SetZIndex(cv_aim, 0);
            //    };
            //    #endregion
            //}
            
            ////sb.Begin();

            //animator.Begin(card);
            return true;
        }

        #endregion

        #region <-- 怪物卡放置 -->

        /// <summary>
        /// 怪物卡盖放
        /// </summary>
        /// <param name="cv">出发控件</param>
        /// <param name="card">卡片</param>
        /// <param name="cv_aim">目标控制</param>
        /// <returns></returns>
        public static bool Cover(Canvas cv, Card card, Canvas cv_aim)
        {
            //Grid gd = cv.Parent as Grid;
            //if (gd != null)
            //{
            //    Panel.SetZIndex(gd, 1);
            //}
            //Panel.SetZIndex(cv_aim, 1);

            ////1.获取卡片相对于目的地的距离
            //Point start = card.TranslatePoint(new Point(), cv_aim);
            ////2.获取卡片在卡框中的相对距离
            //Point end = new Point((cv_aim.ActualWidth - card.ActualWidth) / 2.0, (cv_aim.ActualHeight - card.ActualHeight) / 2.0);
            ////脱离原控件
            //Base.getawayParerent(card);
            ////利用1设置初始位置
            //Canvas.SetTop(card, start.Y);
            //Canvas.SetLeft(card, start.X);
            ////加入目的地控件
            //cv_aim.Children.Add(card);

            //CardAnimation.setTransformGroup(card);
            //MyStoryboard msb = CardAnimation.Rotate_CanvasXY(0, -90, end, 150, 200);
            //msb.card = card;    
            //msb.Completed += (object sender, EventArgs e) =>
            //{
            //    if (gd != null)
            //    {
            //        Panel.SetZIndex(gd, 0);
            //    }
            //    Panel.SetZIndex(cv_aim, 0);

            //    //清空属性和动画的关联绑定
            //    msb.card.BeginAnimation(Canvas.LeftProperty, null);
            //    msb.card.BeginAnimation(Canvas.TopProperty, null);
            //    Canvas.SetTop(card, end.Y);
            //    Canvas.SetLeft(card, end.X);

            //    //card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //    //card.RenderTransform.SetValue(RotateTransform.AngleProperty, (double)-90);

            //    card.isDef = true;
            //    //CardOperate.sort_SingleCard(card);
            //    CardOperate.sort_HandCard(cv);
            //    //storyboard = null;
            //};

            //msb.Begin(card);

            return true;
            //CardAnimation.MoveAnimation2Def_whole(card, start, end, 500);
 
        }

        #endregion <-- 怪物卡放置 -->

        #region <-- 魔陷卡盖放 -->

        /// <summary>
        /// 魔陷卡盖放
        /// </summary>
        /// <param name="cv"></param>
        /// <param name="card"></param>
        /// <param name="cv_aim"></param>
        /// <returns></returns>
        public static bool Cover2(Canvas cv, Card card, Canvas cv_aim)
        {
            //Grid gd = cv_aim.Parent as Grid;
            //if (gd != null) Panel.SetZIndex(gd, 1);           
            //Panel.SetZIndex(cv_aim, 1);

            ////1.获取卡片相对于目的地的距离
            //Point start = card.TranslatePoint(new Point(), cv_aim);
            //Console.WriteLine(start.X);
            ////2.获取卡片在卡框中的相对距离
            //Point end = new Point((cv_aim.ActualWidth - card.ActualWidth) / 2.0, (cv_aim.ActualHeight - card.ActualHeight) / 2.0);
            ////脱离原控件
            //Base.getawayParerent(card);
            ////利用1设置初始位置
            //Canvas.SetTop(card, start.Y);
            //Canvas.SetLeft(card, start.X);
            ////加入目的地控件
            //cv_aim.Children.Add(card);

            //MyStoryboard msb = CardAnimation.CanvasXY(end,200);
            //msb.Completed += (object sender, EventArgs e) =>
            //{
            //    card.BeginAnimation(Canvas.LeftProperty, null);
            //    card.BeginAnimation(Canvas.TopProperty, null);

            //    Canvas.SetTop(card, end.Y);
            //    Canvas.SetLeft(card, end.X);

            //    CardOperate.sort_HandCard(cv);

            //    Panel.SetZIndex(gd, 0);
            //    Panel.SetZIndex(cv_aim, 0);
            //};
            //msb.Begin(card);
            return true;
        }

        #endregion <-- 魔陷卡盖放 -->

        #region <-- 卡片消除 -->

        /// <summary>
        /// 卡片消除
        /// </summary>
        /// <param name="card"></param>
        /// <param name="aim_controls"></param>
        /// <param name="isback"></param>
        /// <returns></returns>
        public static bool Disappear(Card card, Canvas cv_aim, bool isback)
        {
            //Canvas cv = card.Parent as Canvas;

            //#region 清除指示物

            //StackPanel sp = mainwindow.FindName(cv.Name.Replace("card", "sp_sign")) as StackPanel;
            //if (sp != null) sp.Children.Clear();

            //#endregion

            

            //TransLibrary.StoryboardChain tls = new TransLibrary.StoryboardChain();

            //MyStoryboard msb = CardAnimation.Opacity20(250);
            //msb.card = card;
            ////MyStoryboard msb = CardAnimation.FadeOut(card,250);
            //msb.Completed += (object c, EventArgs d) =>
            //{
            //    Base.getawayParerent(msb.card);
            //    CardOperate.sort(cv,msb.card);
            //    cv_aim.Children.Add(msb.card);
            //    if (!isback) CardOperate.card_FrontAtk(card);              
            //    CardOperate.sort(cv_aim,msb.card);
            //};

            //tls.Animates.Add(msb);

            //MyStoryboard msb2 = CardAnimation.Opacity21(250);
            //msb2.card = card;
            ////MyStoryboard msb2 = CardAnimation.FadeIn(card, 500);
            //msb2.Completed += (object c, EventArgs d) =>
            //{
            //    msb2.card.BeginAnimation(Card.OpacityProperty, null);
            //};
            //tls.Animates.Add(msb2);

            //tls.Begin(card);
            ////CardAnimation.FadeOut(card, aim_controls, isback);
            
            return true;
 
        }

        #endregion

        #region <-- 卡组顶牌回到底部 -->

        /// <summary>
        /// 卡组顶牌回到底部
        /// </summary>
        /// <returns></returns>
        public static bool Top2bottom() 
        {
            Card card = (Card)mainwindow.card_2_Deck.Children[mainwindow.card_2_Deck.Children.Count - 1];
            mainwindow.card_2_Deck.Children.Remove(card);
            mainwindow.card_2_Deck.Children.Insert(0, card);

            return true;
        }

        #endregion

        #region <-- 送入对手墓地 -->

        /// <summary>
        /// 送入对手墓地
        /// </summary>
        /// <param name="cv"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        public static bool Send2OpGraveyard(Canvas cv,Card card)
        {
            ////Canvas cv = card.Parent as Canvas;

            //Point start = card.TranslatePoint(new Point(), mainwindow.MySpace);
            //Point end = mainwindow.card_1_Graveyard.TranslatePoint(new Point(), mainwindow.MySpace);

            //Base.getawayParerent(card);
            //mainwindow.MySpace.Children.Add(card);



            //RotateTransform rotate = new RotateTransform();
            //ScaleTransform scale = new ScaleTransform();
            //TranslateTransform translate = new TranslateTransform();
            //TransformGroup group = new TransformGroup();

            //TransLibrary.StoryboardChain tls = new TransLibrary.StoryboardChain();

            ////里守
            //if (card.isDef && card.isBack)
            //{
            //    start.X = start.X - card.ActualWidth  - ((cv.ActualWidth - card.ActualWidth) / 2) + ((cv.ActualWidth - card.ActualHeight)/2);
            //    start.Y = start.Y  - ((card.ActualHeight - card.ActualWidth) / 2);

            //    //end.X = (cv.ActualWidth - card.ActualWidth) / 2;
            //    //end.Y = end.Y - card.ActualHeight;

            //    Canvas.SetTop(card, start.Y);
            //    Canvas.SetLeft(card, start.X);

            //    rotate = new RotateTransform(90);

            //    MyStoryboard msb1 = CardAnimation.scalX_120(card, 100);
            //    msb1.Completed += (object sender, EventArgs e) =>
            //    {
            //        msb1.card.isBack = false;
            //        msb1.card.isDef = false;
            //        card.SetPic();
            //    };

            //    tls.Animates.Add(msb1);

            //    MyStoryboard msb2 = CardAnimation.Send2OpGraveyard1(card, start, end,90,0 ,200);
            //    msb2.Completed += (object sender, EventArgs e) =>
            //    {
            //        //从移动面板Canvas分离
            //        mainwindow.MySpace.Children.Remove(msb2.card);

            //        //清空属性和动画的关联绑定
            //        msb2.card.BeginAnimation(Canvas.LeftProperty, null);
            //        msb2.card.BeginAnimation(Canvas.TopProperty, null);

            //        Canvas.SetTop(msb2.card, 0);
            //        Canvas.SetLeft(msb2.card, 0);

            //        msb2.card.RenderTransform = null;

            //        mainwindow.card_1_Graveyard.Children.Add(card);

            //        CardOperate.sort_SingleCard(card);
            //    };

            //    tls.Animates.Add(msb2);
            //}
            ////表守
            //else if (card.isDef && !card.isBack)
            //{
            //    start.X = start.X - card.ActualWidth - ((cv.ActualWidth - card.ActualWidth) / 2) + ((cv.ActualWidth - card.ActualHeight) / 2);
            //    start.Y = start.Y - ((card.ActualHeight - card.ActualWidth) / 2);

            //    //end.X = (cv.ActualWidth - card.ActualWidth) / 2;
            //    //end.Y = end.Y - card.ActualHeight;

            //    Canvas.SetTop(card, start.Y);
            //    Canvas.SetLeft(card, start.X);

            //    rotate = new RotateTransform(90);

            //    MyStoryboard msb1 = CardAnimation.Send2OpGraveyard2(card, start, end, 90, 0, 200);
            //    msb1.Completed += (object sender, EventArgs e) =>
            //    {
            //        //从移动面板Canvas分离
            //        mainwindow.MySpace.Children.Remove(msb1.card);

            //        //清空属性和动画的关联绑定
            //        msb1.card.BeginAnimation(Canvas.LeftProperty, null);
            //        msb1.card.BeginAnimation(Canvas.TopProperty, null);

            //        Canvas.SetTop(msb1.card, 0);
            //        Canvas.SetLeft(msb1.card, 0);

            //        msb1.card.RenderTransform = null;

            //        mainwindow.card_1_Graveyard.Children.Add(msb1.card);

            //        CardOperate.sort_SingleCard(card);


            //    };
            //    tls.Animates.Add(msb1);

            //}
            ////表攻
            //else if (!card.isDef && !card.isBack)
            //{
            //    start.X = start.X - card.ActualWidth - ((cv.ActualWidth - card.ActualWidth) / 2) + ((cv.ActualWidth - card.ActualHeight) / 2);
            //    start.Y = start.Y - ((card.ActualHeight - card.ActualWidth) / 2);

            //    //end.X = (cv.ActualWidth - card.ActualWidth) / 2;
            //    //end.Y = end.Y - card.ActualHeight;

            //    Canvas.SetTop(card, start.Y);
            //    Canvas.SetLeft(card, start.X);

            //    rotate = new RotateTransform(-180);

            //    MyStoryboard msb1 = CardAnimation.Send2OpGraveyard2(card, start, end, -180, 0, 200);
            //    msb1.Completed += (object sender, EventArgs e) =>
            //    {
            //        //从移动面板Canvas分离
            //        mainwindow.MySpace.Children.Remove(msb1.card);

            //        //清空属性和动画的关联绑定
            //        msb1.card.BeginAnimation(Canvas.LeftProperty, null);
            //        msb1.card.BeginAnimation(Canvas.TopProperty, null);

            //        Canvas.SetTop(msb1.card, 0);
            //        Canvas.SetLeft(msb1.card, 0);

            //        msb1.card.RenderTransform = null;

            //        mainwindow.card_1_Graveyard.Children.Add(msb1.card);

            //        CardOperate.sort_SingleCard(card);


            //    };
            //    tls.Animates.Add(msb1);
            //    //rotate = new RotateTransform(-90);
            //}

            //group.Children.Add(scale);
            //group.Children.Add(rotate);
            //group.Children.Add(translate);
            //card.RenderTransform = group;

            //tls.Begin();

            return true;
        }

        #endregion

        #region <-- 转移控制权,成为对方素材 -->

        /// <summary>
        /// 转移控制权,成为对方素材
        /// </summary>
        /// <param name="card"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static bool ControlChange(Card card, Canvas cv_aim)
        {
            //if (DuelOperate.getInstance().myself.deck.Main.Contains(card) || DuelOperate.getInstance().myself.deck.Extra.Contains(card))
            //{
            //    card.ToolTip = null;
            //}
            //else
            //{
            //    card.ToolTip = "对手的卡";
            //    card.ContextMenu = AllMenu.cm_monster;
            //}

            //Canvas cv = Base.getParerent(card);

            //TransLibrary.StoryboardChain tls = new TransLibrary.StoryboardChain();

            //if (cv_aim.Children.Count == 0)
            //{

            //    MyStoryboard msb1 = CardAnimation.Card_2Opponent(card, cv_aim, 150, "2");
            //    msb1.Completed += (object sender, EventArgs e) =>
            //    {
            //        #region 转移指示物

            //        StackPanel sp = mainwindow.FindName(cv.Name.Replace("card", "sp_sign")) as StackPanel;
                    
            //        if ( sp!=null && sp.Children.Count > 0)
            //        {
            //            StackPanel sp_aim = mainwindow.FindName(cv_aim.Name.Replace("card", "sp_sign")) as StackPanel;
            //            int count = sp.Children.Count;
            //            for (int i = 0; i < count; i++)
            //            {
            //                Grid gd = sp.Children[0] as Grid;
            //                TextBlock tb = gd.Children[1] as TextBlock;
            //                tb.MouseDown += new MouseButtonEventHandler(DuelEvent.ClikDouble2);
            //                sp.Children.Remove(gd);
            //                sp_aim.Children.Add(gd);
            //            }
            //        }

            //        #endregion

            //        //msb1.card.isMine = msb1.card.isMine ? false : true;
            //        Base.getawayParerent(msb1.card);

            //        //清空属性和动画的关联绑定
            //        msb1.card.BeginAnimation(Canvas.LeftProperty, null);
            //        msb1.card.BeginAnimation(Canvas.TopProperty, null);

            //        if (msb1.card.isDef)
            //        {
            //            msb1.card.RenderTransform = new RotateTransform(-90);
            //        }
            //        else
            //        {
            //            msb1.card.RenderTransform = new RotateTransform();
            //        }
                    
            //        Canvas.SetTop(msb1.card, 0);
            //        Canvas.SetLeft(msb1.card, 0);


            //        cv_aim.Children.Add(msb1.card);
            //        msb1.card.SetPic();
            //        CardOperate.sort(cv_aim, msb1.card);
            //        CardOperate.sort(cv, msb1.card);
            //    };
            //    tls.Animates.Add(msb1);
            //    if (cv.Children.Count > 1 && card.sCardType.Equals("XYZ怪兽"))
            //    {
            //        int ControlChangeNum = cv.Children.Count;
            //        for (int i = 0; i < ControlChangeNum ; i++)
            //        {
            //            Card card2 = cv.Children[0] as Card;
            //            MyStoryboard msb4 = CardAnimation.Card_2Opponent(card2, cv_aim, 150, "2");
            //            msb4.Completed += (object sender, EventArgs e) =>
            //            {
            //                //msb4.card.isMine = msb4.card.isMine ? false : true;
            //                Base.getawayParerent(msb4.card);

            //                //清空属性和动画的关联绑定
            //                msb4.card.BeginAnimation(Canvas.LeftProperty, null);
            //                msb4.card.BeginAnimation(Canvas.TopProperty, null);

            //                if (msb4.card.isDef)
            //                {
            //                    msb4.card.RenderTransform = new RotateTransform(-90);
            //                }
            //                else
            //                {
            //                    msb4.card.RenderTransform = new RotateTransform();
            //                }

            //                Canvas.SetTop(msb4.card, 0);
            //                Canvas.SetLeft(msb4.card, 0);

            //                cv_aim.Children.Insert(0, msb4.card);
            //                msb4.card.SetPic();
            //                CardOperate.sort(cv_aim, msb4.card);
            //                CardOperate.sort(cv, msb4.card);

            //                msb4.card.PreviewMouseMove += new MouseEventHandler(DuelEvent.CardDragStart);
            //                msb4.card.MouseDown += new MouseButtonEventHandler(DuelEvent.ClikDouble);

            //                msb4.card.QueryContinueDrag += new QueryContinueDragEventHandler(DuelEvent.card_DragContinue);
            //                msb4.card.MouseEnter += new MouseEventHandler(DuelEvent.card_picture_MouseEnter);
            //            };
            //            tls.Animates.Add(msb4);
            //        }

                    
            //    }
                
            //}
            //else
            //{

            //    if (card.isBack)
            //    {
            //        MyStoryboard msb2 = CardAnimation.scalX_120(card,100);
            //        msb2.Completed += (object sender, EventArgs e) =>
            //        {
            //            card.isBack = false;
            //            card.SetPic();
            //        };
            //        tls.Animates.Add(msb2);
            //    }

            //    MyStoryboard msb3 = CardAnimation.Card_2OpponentXYZmaterial(card, cv_aim, 150, "2");
            //    msb3.Completed += (object sender, EventArgs e) =>
            //    {
            //        Base.getawayParerent(msb3.card);

            //        //清空属性和动画的关联绑定
            //        msb3.card.BeginAnimation(Canvas.LeftProperty, null);
            //        msb3.card.BeginAnimation(Canvas.TopProperty, null);


            //        msb3.card.RenderTransform = new RotateTransform();


            //        Canvas.SetTop(msb3.card, 0);
            //        Canvas.SetLeft(msb3.card, 0);

            //        cv_aim.Children.Insert(0, msb3.card);
            //        msb3.card.SetPic();
            //        CardOperate.sort(cv_aim, msb3.card);
            //        CardOperate.sort(cv, msb3.card);
            //    };

            //    tls.Animates.Add(msb3);
            //}
            //tls.Animates[tls.Animates.Count - 1].Completed += (object sender, EventArgs e) =>
            //{
            //    Panel.SetZIndex(mainwindow.battle_zone_left, 0);
            //    Panel.SetZIndex(cv_aim, 0);
            //};
            //tls.Begin();

            //#region 注册拖拽

            //card.PreviewMouseMove += new MouseEventHandler(DuelEvent.CardDragStart);
            //card.MouseDown += new MouseButtonEventHandler(DuelEvent.ClikDouble);

            //card.QueryContinueDrag += new QueryContinueDragEventHandler(DuelEvent.card_DragContinue);
            //card.MouseEnter += new MouseEventHandler(DuelEvent.card_picture_MouseEnter);

            //#endregion

            return true;
        }

        #endregion

        #region <-- 生命值变化 -->

        /// <summary>
        /// 生命值变化
        /// </summary>
        /// <param name="remaining_life"></param>
        /// <returns></returns>
        public static bool LifeChange(double remaining_life)
        {
            double rt_remaining_life;
            mainwindow.tbk_life_P2.Text = remaining_life.ToString();
            rt_remaining_life = 200 * (remaining_life / 8000.0);
            MyStoryboard msb = CardAnimation.LifeChange(mainwindow.rt_life_P2, rt_remaining_life, 1000);
            msb.Begin();
            return true;
        }

        #endregion

        #region <-- 脱离父控件 -->

        /// <summary>
        /// 脱离父控件
        /// </summary>
        /// <param name="card"></param>
        private static void getawayParerent(Card card)
        {
            ////获得卡片所在父控件并解离
            //Canvas cv = card.Parent as Canvas;
            //if (cv != null)
            //{             
            //    cv.Children.Remove(card);
            //    Canvas.SetLeft(card, 0);
            //    Canvas.SetTop(card, 0);

            //    Grid gd = cv.Parent as Grid;
            //    if (gd != null)
            //    {

            //        #region 清除指示物

            //        StackPanel sp = mainwindow.FindName(cv.Name.Replace("card", "sp_sign")) as StackPanel;
            //        if (sp != null) sp.Children.Clear();

            //        #endregion

            //        if (CardOperate.cv_monsters_1.Contains(cv) || CardOperate.cv_monsters_2.Contains(cv))
            //        {

            //            if (cv.Children.Count > 0)
            //            {
            //                CardOperate.sort(cv, card);
            //                return;
            //            }
            //        }

            //        if (gd.Equals(mainwindow.battle_zone_middle))
            //        {
            //            //int num = 0;
            //            //if(int.TryParse(cv.Name.Substring(cv.Name.Length - 1),out num))
            //            //{
            //            //    if (num > 0 && num < 11)
            //            //    {
                                

            //            //        if ( num > 5)
            //            //        {
            //            //            //前场
            //            //            if (cv.Children.Count > 0)
            //            //            {
            //            //                CardOperate.sort(cv, card);
            //            //            }
            //            //        }
            //            //        else if ( num < 6 )
            //            //        {
            //            //            //后场

            //            //        }
                                

            //            //    }

            //            //}

            //            if (cv.Name.Contains("hand"))
            //            {
            //                //CardOperate.sort(cv)
            //            }

            //            if ( !cv.Equals(mainwindow.card_1_Outside) && !cv.Equals(mainwindow.card_2_Outside))
            //            {
            //                CardOperate.sort(cv, card);
            //            }
                        
            //        }

            //        if (gd.Equals(mainwindow.battle_zone_left))
            //        {

            //        }

            //        if (gd.Equals(mainwindow.battle_zone_right))
            //        {

            //        }

                    
            //    }
                
            //}

        }

        #endregion

        #region <-- 暂时无用的 -->

        #region 1

        public static int transform(string cv_name)
        {
            switch (cv_name)
            {
                case "card_hand": return 36;   //手卡
                case "card_1_Deck": return 0;    //卡组
                case "card_1_Extra": return 6;    //额外
                case "card_1_Graveyard": return 2;    //墓地
                case "card_1_Right": return 1;    //右灵摆
                case "card_1_Left": return 7;    //左灵摆
                case "card_1_Outside": return 24;   //除外
                case "card_1_Area": return 8;    //场地

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

        #endregion

        #region <-- 召唤相关命令 -->

        /// <summary>
        /// 从手卡召唤，特殊召唤，P召唤，叠放，发动魔法卡
        /// </summary>
        /// <param name="cardname"></param>
        /// <param name="Type"></param>
        /// <param name="cv"></param>
        /// <param name="cv_aim"></param>
        /// <returns></returns>
        public static bool Summon(Canvas cv, Card card, Canvas cv_aim)
        {
            //Grid gd = cv.Parent as Grid;
            //if (gd != null)
            //{
            //    Panel.SetZIndex(gd, 1);
            //}
            //Panel.SetZIndex(cv_aim, 11);

            //RotateTransform rotate = new RotateTransform();
            //ScaleTransform scale = new ScaleTransform();
            //TranslateTransform translate = new TranslateTransform();
            //TransformGroup group = new TransformGroup();

            //CardAnimation.setTransformGroup(card);

            ////1.获取卡片相对于目的地的距离
            //Point start = card.TranslatePoint(new Point(), cv_aim);
            ////2.获取卡片在卡框中的相对距离
            //Point end = new Point((cv_aim.ActualWidth - card.ActualWidth) / 2.0, (cv_aim.ActualHeight - card.ActualHeight) / 2.0);
            ////脱离原控件
            //Base.getawayParerent(card);
            ////利用1设置初始位置
            //Canvas.SetTop(card, start.Y);
            //Canvas.SetLeft(card, start.X);
            ////加入目的地控件
            //cv_aim.Children.Add(card);


            //if (cv.Name.Contains("Outside"))
            //{

            //    //card.RenderTransformOrigin = new Point(0, 0);
            //    rotate = new RotateTransform(-90);
            //    //start.X = 64;

            //    //start.Y = start.Y + card.ActualWidth; 

            //}

            //group.Children.Add(scale);
            //group.Children.Add(translate);
            //group.Children.Add(rotate);
            //card.RenderTransform = group;

            //TransLibrary.StoryboardChain tls = new TransLibrary.StoryboardChain();

            //if (cv.Name.Equals("card_2_hand") || cv.Name.Equals("card_2_Extra"))
            //{
            //    MyStoryboard msb1 = CardAnimation.scalX_120(card, 100);
            //    msb1.Completed += (object sender, EventArgs e) =>
            //    {
            //        //CardOperate.card_FrontAtk(msb1.card);
            //        msb1.card.isBack = false;
            //        msb1.card.SetPic();
            //        msb1.card.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, null);
            //    };
            //    tls.Animates.Add(msb1);
            //}



            //MyStoryboard msb2 = CardAnimation.MoveAnimation2Summon(card, start, end, 150);
            //msb2.Completed += (object sender, EventArgs e) =>
            //{
            //    if (gd != null)
            //    {
            //        Panel.SetZIndex(gd, 0);
            //    }
            //    Panel.SetZIndex(cv_aim, 0);

            //    #region
            //    CardOperate.sort(cv, msb2.card);

            //    //从移动面板Canvas分离
            //    Base.getawayParerent(msb2.card);

            //    //清空属性和动画的关联绑定
            //    msb2.card.BeginAnimation(Canvas.LeftProperty, null);
            //    msb2.card.BeginAnimation(Canvas.TopProperty, null);
            //    msb2.card.BeginAnimation(RotateTransform.AngleProperty, null);
            //    //msb2.card.RenderTransform.SetValue(RotateTransform.AngleProperty, (double)0);
            //    msb2.card.RenderTransform = null;
            //    Canvas.SetTop(msb2.card, 0);
            //    Canvas.SetLeft(msb2.card, 0);

            //    msb2.card.isBack = false;
            //    //cv_aim.Children.Add(msb2.card);

            //    if (cv_aim.Children.Count > 0)
            //    {
            //        Card top = cv_aim.Children[cv_aim.Children.Count - 1] as Card;

            //        if (msb2.card.sCardType.Equals("XYZ怪兽"))
            //        {
            //            if (top.isDef && top.isBack)
            //            {
            //                CardAnimation.Rotate2Atk(top);
            //                top.isBack = false;
            //                card.isDef = false;
            //            }
            //            else if (top.isDef)
            //            {
            //                top.RenderTransform = new RotateTransform(-90);
            //                CardAnimation.Def_or_Atk(top);
            //                card.isDef = false;
            //            }
            //            cv_aim.Children.Add(msb2.card);
            //        }
            //        else
            //        {
            //            if (top.sCardType.Equals("XYZ怪兽"))
            //            {
            //                cv_aim.Children.Insert(0, msb2.card);
            //                if (top.isDef)
            //                {
            //                    CardOperate.sort_Canvas(cv_aim);
            //                }
            //                else
            //                {
            //                    CardOperate.sort_Canvas(cv_aim);
            //                }
            //            }
            //            else
            //            {
            //                cv_aim.Children.Add(msb2.card);
            //                CardOperate.sort_Canvas(cv_aim);
            //                if (top.isDef && top.isBack)
            //                {
            //                    CardAnimation.Rotate2Atk(top);
            //                    top.isBack = false;
            //                    card.isDef = false;
            //                }
            //                else if (top.isDef)
            //                {
            //                    top.RenderTransform = new RotateTransform(-90);
            //                    CardAnimation.Def_or_Atk(top);
            //                    card.isDef = false;
            //                }

            //            }
            //        }


            //    }
            //    else
            //    {
            //        //Base.getawayParerent(msb2.card);
            //        cv_aim.Children.Add(msb2.card);
            //    }

            //    CardOperate.sort(cv_aim, msb2.card);
            //    //CardOperate.sort_Canvas(cv_aim);
            //    ////CardOperate.CardSortsingle(cv_aim, card, 56, 81);
            //    //CardOperate.sort_HandCard("2");

            //    msb2.Remove(msb2.card);
            //    msb2 = null;
            //    GC.Collect();
            //    Console.WriteLine("清理内存");

            //    #endregion
            //};



            //tls.Animates.Add(msb2);

            //tls.Begin();

            return true;
        }

        #endregion

        #endregion

    }
}
