﻿using iDuel_EvolutionX;
using iDuel_EvolutionX.EventJson;
using iDuel_EvolutionX.Model;
using iDuel_EvolutionX.Net;
using iDuel_EvolutionX.Service;
using iDuel_EvolutionX.Tools;
using iDuel_EvolutionX.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NBX3.Service
{
    class DuelOperate:IDisposable
    {
        private static DuelOperate duelOperate;
        private static MainWindow mainwindow;
        private static int card_duelindex = 0;
        private Dictionary<int, string> sendstack = new Dictionary<int, string>();
        //private List<string> sendstack = new List<string>();
        private static int msgNum = 0;
        //private NetworkStream ns;

        public Duelist myself = new Duelist(); 
        public Duelist opponent = new Duelist();
        public Deck temp_deck;
        public int main = -1;
        public int extra = -1;
        public Phase curPhase = Phase.STOP;

        

        private DuelOperate()
        {
            myself = new Duelist();
            opponent = new Duelist();
        }

        public static bool isExist()
        {
            if (duelOperate ==  null)
	        {
                return false;
	        }
            return true;
        }

        public static DuelOperate getInstance()
        {
            if (duelOperate == null)
            {
                duelOperate = new DuelOperate();
                

            }
            return duelOperate;
        }

        public static DuelOperate getInstance(MainWindow mw)
        {
            if (duelOperate == null)
            {
                duelOperate = new DuelOperate();
                mainwindow = mw;
                duelOperate.setMyinformation();
                duelOperate.opponent.deck = new Deck();

            }
            return duelOperate;
        }

        #region <-- 初始化己方信息 -->

        private void setMyinformation()
        {
            myself.userindex = Guid.NewGuid() ;
            myself.name = AppConfigOperate.getInstance().Duelist;
            myself.cardback = BitmapImagehandle.GetBitmapImage(AppConfigOperate.getInstance().Custom_path + "\\cardback0.jpg");
            mainwindow.tbk_myname.Text = myself.name;
            mainwindow.mi_setting.DataContext = AppConfigOperate.getInstance();
        }

        #endregion

        #region <-- 对手接入 -->

        /// <summary>
        /// 对手接入
        /// </summary>
        /// <param name="duelistName"></param>
        public void opponentConnect(string duelistName)
        {
            UIAnimation.getInstance().opacity21.Stop();
            UIAnimation.getInstance().opactiy20.Begin();

            mainwindow.tbk_opname.Text = duelistName;
            mainwindow.report.Text += "与" + duelistName[1] + "成功建立连接" + Environment.NewLine;
        }

        #endregion

        #region <-- 发送己方信息 -->

        public string sendmyself()
        {
            string send = "[SetP2="; //设定玩家2信息
            send += DuelOperate.getInstance().myself.userindex; //玩家昵称
            send += "," + DuelOperate.getInstance().myself.name + "]";//玩家ID
            return send;
 
        }

        

        #endregion

        #region <-- 打开卡组管理器 -->

        /// <summary>
        /// 打开卡组管理器
        /// </summary>
        public void ShowDeck()
        {


            //sendMsg("MSG=", "更换卡组");
            mainwindow.btn_start.IsEnabled = false;
            mainwindow.btn_deck.Content = "确认选择";
            mainwindow.bsb_menu_hide.Actions.RemoveAt(0);
            Grid gd = mainwindow.btn_choosedeckCancel.Parent as Grid;
            gd.ColumnDefinitions[1].Style = mainwindow.Resources["star"] as Style;
            mainwindow.gd_decksManerger.Visibility = System.Windows.Visibility.Visible;

            #region 清理己方场地

            mainwindow.card_1_1.Children.Clear();
            mainwindow.card_1_2.Children.Clear();
            mainwindow.card_1_3.Children.Clear();
            mainwindow.card_1_4.Children.Clear();
            mainwindow.card_1_5.Children.Clear();
            mainwindow.card_1_6.Children.Clear();
            mainwindow.card_1_7.Children.Clear();
            mainwindow.card_1_8.Children.Clear();
            mainwindow.card_1_9.Children.Clear();
            mainwindow.card_1_10.Children.Clear();

            mainwindow.card_1_Deck.Children.Clear();
            mainwindow.card_1_Extra.Children.Clear();
            mainwindow.card_1_Area.Children.Clear();
            mainwindow.card_1_Graveyard.Children.Clear();
            mainwindow.card_1_Outside.Children.Clear();
            mainwindow.card_1_Left.Children.Clear();
            mainwindow.card_1_Right.Children.Clear();
            mainwindow.card_1_hand.Children.Clear();

            #endregion


           
            MyStoryboard msb = UIAnimation.SPmove(mainwindow.sp_main, new Thickness(0, -340, 0, 0), new Thickness(0), 600);
            MyStoryboard msb2 = UIAnimation.SPmove(mainwindow.sp_extra, new Thickness(0, -430, 0, 0), new Thickness(0), 600);
            MyStoryboard msb3 = UIAnimation.SPmove(mainwindow.sp_side, new Thickness(0, -520, 0, 0), new Thickness(0), 600);
            MyStoryboard msb4 = UIAnimation.SPmove(mainwindow.tbc_DeckDocument, new Thickness(0, 102, 0, 0), new Thickness(0), 600);
            MyStoryboard msb5 = UIAnimation.showDecksManerger(mainwindow.gd_decksManerger, 0, 1, 300);


            msb5.Begin();
            msb.Begin();
            msb2.Begin();
            msb3.Begin();
            msb4.Begin();


        }

        #endregion

        #region <-- 选择卡组 -->

        /// <summary>
        /// 选择卡组
        /// </summary>
        /// <param name="deck"></param>
        public void ChooseDeck(object deck)
        {
            Deck deckChoose = deck as Deck;
            if (deckChoose != null)
            {
                mainwindow.cv_main.Children.Clear();
                mainwindow.cv_extra.Children.Clear();
                mainwindow.cv_side.Children.Clear();

                for (int i = 0; i < deckChoose.Main.Count; i++)
                {
                    CardUI card = deckChoose.Main[i] as CardUI;
                    card.set2FrontAtk();
                    mainwindow.cv_main.Children.Add(card);
                    card.showImg();
                    //card.SetPic();

                    #region 注册卡片信息

                    card.PreviewMouseMove += new MouseEventHandler(DuelEvent.CardDragStart);
                    //card.MouseDown += new MouseButtonEventHandler(DuelEvent.ClikDouble);

                    card.QueryContinueDrag += new QueryContinueDragEventHandler(DuelEvent.card_DragContinue);
                    card.MouseEnter += new MouseEventHandler(DuelEvent.card_picture_MouseEnter);

                    #endregion
                }

                for (int i = 0; i < deckChoose.Extra.Count; i++)
                {
                    CardUI card = deckChoose.Extra[i] as CardUI;
                    card.set2FrontAtk();     
                    card.showImg();
                    mainwindow.cv_extra.Children.Add(card);

                    #region 注册卡片信息

                    card.PreviewMouseMove += new MouseEventHandler(DuelEvent.CardDragStart);
                    //card.MouseDown += new MouseButtonEventHandler(DuelEvent.ClikDouble);

                    card.QueryContinueDrag += new QueryContinueDragEventHandler(DuelEvent.card_DragContinue);
                    card.MouseEnter += new MouseEventHandler(DuelEvent.card_picture_MouseEnter);

                    #endregion
                }
                for (int i = 0; i < deckChoose.Side.Count; i++)
                {
                    CardUI card = deckChoose.Side[i] as CardUI;
                    card.set2FrontAtk();
                    card.showImg();
                    mainwindow.cv_side.Children.Add(card);

                    #region 注册卡片信息

                    card.PreviewMouseMove += new MouseEventHandler(DuelEvent.CardDragStart);
                    //card.MouseDown += new MouseButtonEventHandler(DuelEvent.ClikDouble);

                    card.QueryContinueDrag += new QueryContinueDragEventHandler(DuelEvent.card_DragContinue);
                    card.MouseEnter += new MouseEventHandler(DuelEvent.card_picture_MouseEnter);

                    #endregion
                }

                CardOperate.sort(mainwindow.cv_main, null);
                CardOperate.sort(mainwindow.cv_extra, null);
                CardOperate.sort(mainwindow.cv_side, null);

            }

            temp_deck = deckChoose;

        }

        #endregion

        #region <-- 取消选择卡组或取消更换side -->

        internal void DecksetCancel()
        {
            //sendMsg("MSG=", "更换卡组结束");

            temp_deck = null;

            mainwindow.btn_start.IsEnabled = true;
            mainwindow.btn_deck.Content = "卡组管理";
            Grid gd = mainwindow.btn_choosedeckCancel.Parent as Grid;
            Grid gd2 = mainwindow.btn_sideMode.Parent as Grid;
            Style style = mainwindow.Resources["zero"] as Style;
            gd.ColumnDefinitions[1].Style = style;
            gd2.ColumnDefinitions[1].Style = style;
            TriggerAction ta = mainwindow.Resources["hide_menu"] as TriggerAction;
            mainwindow.bsb_menu_hide.Actions.Add(ta);

            MyStoryboard msb1 = UIAnimation.SPmove(mainwindow.sp_main, new Thickness(0), new Thickness(0, -340, 0, 0), 500);
            MyStoryboard msb2 = UIAnimation.SPmove(mainwindow.sp_extra, new Thickness(0), new Thickness(0, -430, 0, 0), 500);
            MyStoryboard msb3 = UIAnimation.SPmove(mainwindow.sp_side, new Thickness(0), new Thickness(0, -520, 0, 0), 500);
            MyStoryboard msb4 = UIAnimation.SPmove(mainwindow.tbc_DeckDocument, new Thickness(0), new Thickness(0, 102, 0, 0), 500);
            MyStoryboard msb5 = UIAnimation.showDecksManerger(mainwindow.gd_decksManerger, 1, 0, 300);
            msb5.Completed += (object c, EventArgs d) =>
            {
                mainwindow.gd_decksManerger.Visibility = System.Windows.Visibility.Hidden;
                mainwindow.cv_main.Children.Clear();
                mainwindow.cv_extra.Children.Clear();
                mainwindow.cv_side.Children.Clear();
                //DuelStart();
            };

            msb5.Begin();
            msb1.Begin();
            msb2.Begin();
            msb3.Begin();
            msb4.Begin();

            return;
        }

        #endregion

        #region <-- 确认选择的卡组 -->

        /// <summary>
        /// 确认选择的卡组
        /// </summary>
        public void SetDeck(Button btn)
        {
            if (temp_deck == null)
            {
                MessageBox.Show("请选择卡组！");
                return;
            }

            //sendMsg("MSG=", "更换卡组结束");

            myself.deck = temp_deck;
            temp_deck = null;
            mainwindow.btn_start.IsEnabled = true;
            mainwindow.btn_deck.Content = "卡组管理";
            mainwindow.btn_sideMode.IsEnabled = true;

            Grid gd = mainwindow.btn_deck.Parent as Grid;
            gd.ColumnDefinitions[1].Style = mainwindow.Resources["zero"] as Style;

            TriggerAction ta = mainwindow.Resources["hide_menu"] as TriggerAction;
            mainwindow.bsb_menu_hide.Actions.Add(ta);

            slideUp();
            
        }

        #endregion

        #region <-- 己方开局 -->

        /// <summary>
        /// 己方开局（清场，重新放置卡片）
        /// </summary>
        /// <param name="card_all"></param>
        public void DuelStart()
        {


            if (myself.deck == null)
            {
                //读取上一次使用的卡组
                return;
            }
            else
            {
                #region 清理己方场地

                mainwindow.card_1_1.Children.Clear();
                mainwindow.card_1_2.Children.Clear();
                mainwindow.card_1_3.Children.Clear();
                mainwindow.card_1_4.Children.Clear();
                mainwindow.card_1_5.Children.Clear();
                mainwindow.card_1_6.Children.Clear();
                mainwindow.card_1_7.Children.Clear();
                mainwindow.card_1_8.Children.Clear();
                mainwindow.card_1_9.Children.Clear();
                mainwindow.card_1_10.Children.Clear();

                mainwindow.card_1_Deck.Children.Clear();
                mainwindow.card_1_Extra.Children.Clear();
                mainwindow.card_1_Area.Children.Clear();
                mainwindow.card_1_Graveyard.Children.Clear();
                mainwindow.card_1_Outside.Children.Clear();
                mainwindow.card_1_Left.Children.Clear();
                mainwindow.card_1_Right.Children.Clear();
                mainwindow.card_1_hand.Children.Clear();

                #endregion

            }

            sendstack.Clear();
            card_duelindex = 0;

            

            //放置卡组
            CardUI card;
            string msg = "start=";
            for (int i = 0; i < myself.deck.Main.Count; i++)
            {
                card = myself.deck.Main[i];
                card.getAwayFromParents();
                //card.duelindex = myself.userindex + card_duelindex.ToString();
                card_duelindex ++;
                

                #region 注册卡片信息

                card.PreviewMouseMove += new MouseEventHandler(DuelEvent.CardDragStart);
                card.MouseDown += new MouseButtonEventHandler(DuelEvent.ClikDouble);

                card.QueryContinueDrag += new QueryContinueDragEventHandler(DuelEvent.card_DragContinue);
                card.MouseEnter += new MouseEventHandler(DuelEvent.card_picture_MouseEnter);

                #endregion

                //CardOperate.CardSortsingle(mainwindow.card_1_Deck, main_1[i], 56, 81);
                card.set2BackAtk();
                mainwindow.card_1_Deck.Children.Insert(0, card);
                card.centerAtVerticalInParent();
                card.CurLocation = new Location(mainwindow.card_1_Deck.area, mainwindow.card_1_Deck.Children.IndexOf(card));
                card.outputChange();

                //msg += card.cheatcode;
            }

            

            //放置额外
            for (int i = 0; i < myself.deck.Extra.Count; i++)
            {
                card = myself.deck.Extra[i];
                card.getAwayFromParents();
                //card.duelindex = myself.userindex + card_duelindex.ToString();
                card_duelindex++;
                

                #region 注册卡片信息

                card.PreviewMouseMove += new MouseEventHandler(DuelEvent.CardDragStart);
                card.MouseDown += new MouseButtonEventHandler(DuelEvent.ClikDouble);

                card.QueryContinueDrag += new QueryContinueDragEventHandler(DuelEvent.card_DragContinue);
                card.MouseEnter += new MouseEventHandler(DuelEvent.card_picture_MouseEnter);

                #endregion
                //CardOperate.CardSortsingle(mainwindow.card_1_Extra, extra_1[i], 56, 81);

                card.set2BackAtk();
                mainwindow.card_1_Extra.Children.Insert(0, card);
                card.centerAtVerticalInParent();
                //card.CurLocation = new Location(mainwindow.card_1_Extra.area, mainwindow.card_1_Extra.Children.IndexOf(card));
                //card.outputChange();
                
                //msg += card.cheatcode;
            }


            DeckInfo deckInfo = new DeckInfo();
            foreach (CardUI c in myself.deck.Main)
            {
                deckInfo.main.Add(c.info.cheatcode);
            }
            foreach (CardUI c2 in myself.deck.Extra)
            {
                deckInfo.extra.Add(c2.info.cheatcode);
            }
            String contentJson = JsonConvert.SerializeObject(deckInfo);

            BaseJson bj = new BaseJson();
            bj.guid = myself.userindex;
            bj.cid = "";
            bj.action = ActionCommand.GAME_START;
            bj.json = contentJson;
            String json = JsonConvert.SerializeObject(bj);
            sendMsg(json);

            #region 血条恢复

            mainwindow.tbk_life_P1.Text = "8000";
            MyStoryboard msb6 = CardAnimation.LifeChange(mainwindow.rt_life_P1, mainwindow.img_head_i.ActualWidth, 800);
            msb6.Completed += (object c, EventArgs d) =>
            {
                #region 先攻后攻变为可按状态

                mainwindow.btn_firstAtk.IsEnabled = true;
                mainwindow.btn_secondAtk.IsEnabled = true;

                #endregion
            };
            msb6.FillBehavior = FillBehavior.HoldEnd;
            msb6.Begin();

            #endregion

            #region 阶段按钮重置为不可点状态

            MyStoryboard msb7 = UIAnimation.ChangePhase(0);
            msb7.Completed += (object c, EventArgs d) =>
            {
                mainwindow.bd_step1.Effect.SetCurrentValue(DropShadowEffect.ColorProperty, Colors.White);
            };
            MyStoryboard msb8 = UIAnimation.ChangePhase(0);
            msb8.Completed += (object c, EventArgs d) =>
            {
                mainwindow.bd_step2.Effect.SetCurrentValue(DropShadowEffect.ColorProperty, Colors.White);
            };
            msb7.Begin(mainwindow.bd_step1);
            msb8.Begin(mainwindow.bd_step2);


            #endregion

            
        }

        #endregion

        #region <-- 对方开局 -->

        /// <summary>
        /// 对方开局（清场，重新放置卡片）
        /// </summary>
        /// <param name="main"></param>
        /// <param name="extra"></param>
        public void DuelStart(String userindex,List<string> main, List<string> extra)
        {
            bool success = CardOperate.readDeckBynet(main, extra, opponent.deck);

            #region 清理对手场

            mainwindow.card_2_1.Children.Clear();
            mainwindow.card_2_2.Children.Clear();
            mainwindow.card_2_3.Children.Clear();
            mainwindow.card_2_4.Children.Clear();
            mainwindow.card_2_5.Children.Clear();
            mainwindow.card_2_6.Children.Clear();
            mainwindow.card_2_7.Children.Clear();
            mainwindow.card_2_8.Children.Clear();
            mainwindow.card_2_9.Children.Clear();
            mainwindow.card_2_10.Children.Clear();


            mainwindow.card_2_Deck.Children.Clear();
            mainwindow.card_2_Extra.Children.Clear();
            mainwindow.card_2_Area.Children.Clear();
            mainwindow.card_2_Graveyard.Children.Clear();
            mainwindow.card_2_Outside.Children.Clear();
            mainwindow.card_2_Left.Children.Clear();
            mainwindow.card_2_Right.Children.Clear();
            mainwindow.card_2_hand.Children.Clear();

            #endregion


            //int duelindex = 0;
            //CardUI card;
            //for (int i = 0; i < opponent.deck.Main.Count; i++)
            //{
            //    card = opponent.deck.Main[i];
            //    card.duelindex = userindex + duelindex.ToString();
            //    duelindex++;

            //    mainwindow.card_2_Deck.Children.Insert(0, card);
            //    CardOperate.card_BackAtk(card);
            //    card.MouseEnter += new MouseEventHandler(DuelEvent.card_picture_MouseEnter);
            //    CardOperate.sort_SingleCard(card);
            //}


            //for (int i = 0; i < opponent.deck.Extra.Count; i++)
            //{
            //    card = opponent.deck.Extra[i];
            //    card.duelindex = userindex + duelindex.ToString();
            //    duelindex++;

            //    mainwindow.card_2_Extra.Children.Add(card);
            //    CardOperate.card_BackAtk(card);
            //    card.MouseEnter += new MouseEventHandler(DuelEvent.card_picture_MouseEnter);
            //    CardOperate.sort_SingleCard(card);

            //}

            #region 恢复血条

            mainwindow.tbk_life_P2.Text = "8000";
            MyStoryboard msb = CardAnimation.LifeChange(mainwindow.rt_life_P2, mainwindow.img_head_i.ActualWidth, 1000);
            msb.Begin();

            #endregion
        }

        #endregion

        #region <-- 先攻 -->

        /// <summary>
        /// 先攻
        /// </summary>
        public void FirstAtk()
        {
            mainwindow.btn_firstAtk.IsEnabled = false;
            mainwindow.btn_secondAtk.IsEnabled = false;
            List<CardUI> cards = CardOperate.card_Draw(5, 200);
            //string command = "FirstAtk="+cards[0].duelindex;
            for (int i = 1; i < cards.Count; i++)
            {
                //command += "," + cards[i].duelindex;
            }
            //DuelOperate.getInstance().sendMsg(command, "先攻！");
            //OpponentOperate.Draw(5, mainwindow.card_1_Deck, mainwindow.card_1_hand);           
            Grid.SetColumn(mainwindow.bd_step1, 1);
            Grid.SetColumn(mainwindow.bd_step2, 1);
            mainwindow.bd_step1.BorderBrush = Brushes.Black;
            mainwindow.bd_step2.BorderBrush = Brushes.Black;
            mainwindow.bd_step1.Effect.SetValue(DropShadowEffect.ColorProperty, Colors.Blue);
            mainwindow.bd_step2.Effect.SetValue(DropShadowEffect.ColorProperty, Colors.Blue);
            UIAnimation.ChangePhase(1).Begin(mainwindow.bd_step1);

            OrderInfo orderInfo = new OrderInfo();
            orderInfo.isFirst = true;
            foreach (CardUI card in cards)
            {
                orderInfo.cardsIDs.Add(myself.deck.Main.IndexOf(card));
            }
            String contentJson = JsonConvert.SerializeObject(orderInfo);

            BaseJson bj = new BaseJson();
            bj.guid = myself.userindex;
            bj.cid = "";
            bj.action = ActionCommand.GAME_ORDER;
            bj.json = contentJson;
            String json = JsonConvert.SerializeObject(bj);
            sendMsg(json);

        }

        #endregion

        #region <-- 后攻 -->

        /// <summary>
        /// 后攻
        /// </summary>
        public void SecondAtk()
        {
            mainwindow.btn_firstAtk.IsEnabled = false;
            mainwindow.btn_secondAtk.IsEnabled = false;
            List<CardUI> cards = CardOperate.card_Draw(5, 150);
            //string command = "Draw=5," + cards[0].duelindex;
            for (int i = 1; i < cards.Count; i++)
            {
                //command += "," + cards[i].duelindex;
            }
            //DuelOperate.getInstance().sendMsg(command, "后攻！");

        }

        #endregion

        #region <-- 阶段宣言 -->

        /// <summary>
        /// 阶段宣言
        /// </summary>
        /// <param name="rta"></param>
        public void ChangePhase(Rectangle rta)
        {
            //判断可否按阶段按钮
            Color bs = (Color)mainwindow.bd_step1.Effect.GetValue(DropShadowEffect.ColorProperty);
            Color bs2 = (Color)mainwindow.bd_step2.Effect.GetValue(DropShadowEffect.ColorProperty);
            if (bs == Colors.Blue && bs2 == Colors.Blue)
            {
                switch (rta.Name)
                {
                    case "rta_dp":
                        curPhase = Phase.DP;
                        break;
                    case "rta_sp":
                        curPhase = Phase.SP;
                        break;
                    case "rta_m1":
                        curPhase = Phase.M1;
                        break;
                    case "rta_bp":
                        curPhase = Phase.BP;
                        break;
                    case "rta_m2":
                        curPhase = Phase.M2;
                        break;
                    case "rta_ep":
                        curPhase = Phase.EP;
                        break;
                    default:
                        curPhase = Phase.STOP;
                        break;
                }

                #region 指令发送

                PhaseInfo phaseInfo = new PhaseInfo();
                phaseInfo.phase = curPhase;
                String contentJson = JsonConvert.SerializeObject(phaseInfo);

                BaseJson bj = new BaseJson();
                bj.guid = DuelOperate.getInstance().myself.userindex;
                bj.cid = "";
                bj.action = ActionCommand.GAME_SET_PHASE;
                bj.json = contentJson;
                String json = JsonConvert.SerializeObject(bj);
                DuelOperate.getInstance().sendMsg(json);

                #endregion
                //DuelOperate.getInstance().sendMsg("ChangePhase=" + rta.Name, rta.Name.Replace("rta_", "").ToUpper() + " 阶段");

                int press = Grid.GetColumn(rta);


                MyStoryboard msb2zero = UIAnimation.ChangePhase(0);
                MyStoryboard msb2one = UIAnimation.ChangePhase(1);
                int op = Convert.ToInt32(mainwindow.bd_step1.Opacity);
                //int op = Convert.ToInt32(mainwindow.bd_step1.Effect.GetValue(System.Windows.Media.Effects.DropShadowEffect.OpacityProperty));

                if (rta.Name.Equals("rta_ep"))
                {
                    if (mainwindow.tb_ep.Text.Equals("END"))
                    {
                        msb2zero.Completed += (object c, EventArgs d) =>
                        {
                            mainwindow.bd_step1.SetValue(Border.BorderBrushProperty, Brushes.Red);
                            mainwindow.bd_step1.Effect.SetValue(DropShadowEffect.ColorProperty, Colors.Red);
                            mainwindow.bd_step2.SetValue(Border.BorderBrushProperty, Brushes.Red);
                            mainwindow.bd_step2.Effect.SetValue(DropShadowEffect.ColorProperty, Colors.Red);
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
        }

        #endregion

        #region <-- 聊天 & 修改生命值 -->

        /// <summary>
        /// 聊天，改变生命值
        /// </summary>
        public void chatOrLife()
        {
            if (!mainwindow.tb_chat_send.Text.Trim().Equals(""))
            {
                if (mainwindow.tb_chat_send.Text.Length > 1)
                {
                    switch (mainwindow.tb_chat_send.Text.Substring(0, 2))
                    {
                        case "--":
                            {
                                #region 削减生命值

                                double before;
                                double lose_life;
                                double remaining_life;
                                double rt_remaining_life;
                                double.TryParse(mainwindow.tb_chat_send.Text.Substring(2, mainwindow.tb_chat_send.Text.Length - 2), out lose_life);
                                if (lose_life != 0)
                                {
                                    before = Convert.ToDouble(mainwindow.tbk_life_P1.Text);
                                    remaining_life = before - lose_life;
                                    if (remaining_life < 0) remaining_life = 0;
                                    rt_remaining_life = 200 * (remaining_life / 8000.0);
                                    mainwindow.tbk_life_P1.Text = remaining_life.ToString();
                                    MyStoryboard msb = CardAnimation.LifeChange(mainwindow.rt_life_P1, rt_remaining_life, 1000);
                                    msb.Begin();
                                    string report = "生命值减少" + lose_life + " -> 剩余" + remaining_life;
                                    //DuelOperate.getInstance().sendMsg("LifeChange=" + remaining_life, report);

                                    mainwindow.tb_chat_send.Clear();
                                }

                                #endregion
                            }
                            break;
                        case "++":
                            {
                                #region 增加生命值

                                double add_life;
                                double remaining_life;
                                double rt_remaining_life;
                                double.TryParse(mainwindow.tb_chat_send.Text.Substring(2, mainwindow.tb_chat_send.Text.Length - 2), out add_life);
                                if (add_life != 0)
                                {
                                    remaining_life = Convert.ToDouble(mainwindow.tbk_life_P1.Text) + add_life;
                                    //OpponentOperate.ActionAnalyze("LifeChange=" + remaining_life,true);
                                    mainwindow.tbk_life_P1.Text = remaining_life.ToString();
                                    rt_remaining_life = 200 * (remaining_life / 8000.0);
                                    MyStoryboard msb = CardAnimation.LifeChange(mainwindow.rt_life_P1, rt_remaining_life, 1000);
                                    msb.Begin();
                                    string report = "生命值增加" + add_life + " -> 剩余" + remaining_life;
                                    //DuelOperate.getInstance().sendMsg("LifeChange=" + remaining_life, report);
                                    mainwindow.tb_chat_send.Clear();
                                }

                                #endregion
                            }
                            break;
                        case "//":
                            {
                                #region 倍减生命值

                                double lose_life;
                                double remaining_life;
                                double rt_remaining_life;
                                double.TryParse(mainwindow.tb_chat_send.Text.Substring(2, mainwindow.tb_chat_send.Text.Length - 2), out lose_life);
                                if (lose_life > 1)
                                {
                                    remaining_life = Convert.ToDouble(mainwindow.tbk_life_P1.Text) / lose_life;
                                    //OpponentOperate.ActionAnalyze("LifeChange=" + remaining_life,true);
                                    mainwindow.tbk_life_P1.Text = remaining_life.ToString();
                                    rt_remaining_life = 200 * (remaining_life / 8000.0);
                                    MyStoryboard msb = CardAnimation.LifeChange(mainwindow.rt_life_P1, rt_remaining_life, 1000);
                                    msb.Begin();
                                    string report = "生命值减少" + (lose_life - 1) + "倍" + " -> 剩余" + remaining_life;
                                    //DuelOperate.getInstance().sendMsg("LifeChange=" + remaining_life, report);
                                    mainwindow.tb_chat_send.Clear();
                                }

                                #endregion
                            }
                            break;
                        case "**":
                            {
                                #region 倍增生命值

                                double add_life;
                                double remaining_life;
                                double rt_remaining_life;
                                double.TryParse(mainwindow.tb_chat_send.Text.Substring(2, mainwindow.tb_chat_send.Text.Length - 2), out add_life);
                                if (add_life > 1)
                                {
                                    remaining_life = Convert.ToDouble(mainwindow.tbk_life_P1.Text) * add_life;
                                    //OpponentOperate.ActionAnalyze("LifeChange=" + remaining_life, true);
                                    mainwindow.tbk_life_P1.Text = remaining_life.ToString();
                                    rt_remaining_life = 200 * (remaining_life / 8000.0);
                                    MyStoryboard msb = CardAnimation.LifeChange(mainwindow.rt_life_P1, rt_remaining_life, 1000);
                                    msb.Begin();
                                    //add_life = Convert.ToInt32(life_P1.Text) * add_life - Convert.ToInt32(life_P1.Text); ;
                                    string report = "生命值增加" + (add_life - 1) + "倍" + " -> 剩余" + remaining_life;
                                    //DuelOperate.getInstance().sendMsg("LifeChange=" + remaining_life, report);

                                    mainwindow.tb_chat_send.Clear();
                                }

                                #endregion
                            }
                            break;
                        case "\\\\":
                            {
                                #region 重置生命值

                                int reset_life;
                                double rt_remaining_life;
                                int.TryParse(mainwindow.tb_chat_send.Text.Substring(2, mainwindow.tb_chat_send.Text.Length - 2), out reset_life);
                                if (reset_life >= 0)
                                {
                                    mainwindow.tbk_life_P1.Text = reset_life.ToString();                       
                                    rt_remaining_life = 200 * (reset_life / 8000.0);
                                    MyStoryboard msb = CardAnimation.LifeChange(mainwindow.rt_life_P1, rt_remaining_life, 1000);
                                    msb.Begin();
                                    //add_life = Convert.ToInt32(life_P1.Text) * add_life - Convert.ToInt32(life_P1.Text); ;
                                    string report = "生命值重置为" + reset_life;
                                    //DuelOperate.getInstance().sendMsg("LifeChange=" + reset_life, report);

                                    mainwindow.tb_chat_send.Clear();
                                }

                                #endregion
                            }
                            break;
                        default:
                            //DuelOperate.getInstance().sendMsg("Chat=" + mainwindow.tb_chat_send.Text, "");
                            var p = new Paragraph(); // Paragraph 类似于 html 的 P 标签  
                            var r = new Run(mainwindow.tb_chat_send.Text); // Run 是一个 Inline 的标签  
                            p.Inlines.Add(r);  
                            p.Foreground = Brushes.Blue;//设置字体颜色  
                            //Doc.Blocks.Add(p);
                            mainwindow.tb_chat_view.Document.Blocks.Add(p);  
                            //mainwindow.tb_chat_view.AppendText("我：" + mainwindow.tb_chat_send.Text);
                            mainwindow.tb_chat_view.ScrollToEnd();
                            mainwindow.tb_chat_send.Clear();
                            if (!mainwindow.btn_viewreport.Content.Equals("R"))
                            {
                                MyStoryboard msb = CardAnimation.Opacity20(200);
                                msb.Completed += (object sender, EventArgs e) =>
                                {
                                    mainwindow.report.Visibility = Visibility.Hidden;
                                };
                                msb.Begin(mainwindow.report);
                                mainwindow.btn_viewreport.Content = "R";
                            }
                            break;
                    }
                }
                else
                {                   
                    mainwindow.tb_chat_send.Clear();
                }



            }
        }

        #endregion

        #region <-- 查看战报 -->

        /// <summary>
        /// 查看战报详情
        /// </summary>
        internal void ViewReport()
        {
            if (mainwindow.btn_viewreport.Content.Equals("R"))
            {
                mainwindow.report.Visibility = Visibility.Visible;
                CardAnimation.Opacity21(200).Begin(mainwindow.report);
                mainwindow.btn_viewreport.Content = "0";
            }
            else
            {
                MyStoryboard msb = CardAnimation.Opacity20(200);
                msb.Completed += (object sender, EventArgs e) =>
                {
                    mainwindow.report.Visibility = Visibility.Hidden;
                };
                msb.Begin(mainwindow.report);
                mainwindow.btn_viewreport.Content = "R";
            }

        }

        #endregion

        #region <-- 显示己方区位选择控件 -->

        public void ShowZone_cb()
        {
            mainwindow.btn_choosezone.Content = mainwindow.btn_choosezone.Content.Equals("C-OFF") ? "C-ON" : "C-OFF";
            
            CheckBox cb = new CheckBox();
            //for (int i = 1; i < 11; i++)
            //{
            //   cb = mainwindow.battle_zone_middle.FindName("cb_1_" + i) as CheckBox;
            //   cb.Checked -= DuelEvent.cb_Checked;
            //   cb.Unchecked -= DuelEvent.cb_Checked;
            //}

            for (int i = 1; i < 11; i++)
            {
                cb = mainwindow.battle_zone_middle.FindName("cb_1_" + i) as CheckBox;
                cb.Checked -= DuelEvent.cb_Checked;
                cb.Unchecked -= DuelEvent.cb_Checked;
                cb.IsChecked = false;
                cb.Visibility = cb.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible ;
            }

            if (cb.Visibility == Visibility.Hidden)
            {
                //DuelOperate.getInstance().sendMsg("ClearChooseZone=", "");
            }

            for (int i = 1; i < 11; i++)
            {
                cb = mainwindow.battle_zone_middle.FindName("cb_1_" + i) as CheckBox;
                cb.Checked += new RoutedEventHandler(DuelEvent.cb_Checked);
                cb.Unchecked += new RoutedEventHandler(DuelEvent.cb_Checked);
            }

        }

        #endregion

        #region <-- 选中卡区 -->

        internal void ChooseZone(object sender)
        {
            CheckBox cb = sender as CheckBox;
            if (cb != null)
            {
                //if ((bool)cb.IsChecked) DuelOperate.getInstance().sendMsg("ChooseZone=" + cb.Name + "," + 1, "");
                //else DuelOperate.getInstance().sendMsg("ChooseZone=" + cb.Name + "," + 0, "");
                
                
            }

        }

        #endregion

        #region <-- 发送信息 -->

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="command"></param>
        /// <param name="report"></param>
        public void sendMsg(string msg)
        {
            //return;

            sendstack.Add(sendstack.Count, msg);

            receiveMsg(msg);

            //if (Server.check())
            //{
            //    Server sr = Server.getInstance(mainwindow);
            //    sr.sendMsg(msg);
            //}
            //else if (Client.check())
            //{
            //    Client cl = Client.getInstance(mainwindow);
            //    cl.sendMsg(msg);
            //}
            //else
            //{
            //    mainwindow.report.AppendText("system：未建立连接" + Environment.NewLine);
            //}

        }

        #endregion

        #region <-- 信息接收 -->

        /// <summary>
        /// 信息接收
        /// </summary>
        /// <param name="msg"></param>
        public void receiveMsg(string msg)
        {
            Console.WriteLine(msg);
            
            BaseJson bj = JsonConvert.DeserializeObject<BaseJson>(msg);
            switch (bj.action)
            {
                case ActionCommand.GAME_SET_DUELST_INFO:
                    DuelistInfo dinfo = JsonConvert.DeserializeObject<DuelistInfo>(bj.json);

                    //mainwindow.Dispatcher.Invoke(handleCardbackCallBack, dinfo.cardBack);
                    break;
                case ActionCommand.GAME_START:
                    {
                        DeckInfo deck = JsonConvert.DeserializeObject<DeckInfo>(bj.json);
                        foreach (CardUI card in opponent.deck.Main)
                        {
                            card.getAwayFromParents();
                            card.signs.Clear();
                        }
                        foreach (CardUI card in opponent.deck.Extra)
                        {
                            card.getAwayFromParents();
                            card.signs.Clear();
                        }
                        opponent.deck.Main.Clear();
                        opponent.deck.Extra.Clear();
                        bool isReadSuccessed = CardOperate.readDeckBynet(deck.main, deck.extra, opponent.deck);
                        if (isReadSuccessed)
                        {
                            foreach (CardUI card in opponent.deck.Main)
                            {
                                mainwindow.card_2_Deck.Children.Add(card);
                            }

                            foreach (CardUI card in opponent.deck.Extra)
                            {
                                mainwindow.card_2_Extra.Children.Add(card);
                            }
                        }
                        break;
                    }  
                   
                case ActionCommand.GAME_RPS:
                    break;
                case ActionCommand.GAME_ORDER:
                    {
                        OrderInfo starOrder = JsonConvert.DeserializeObject<OrderInfo>(bj.json);
                        List<CardUI> cards = new List<CardUI>();
                        foreach (var card in starOrder.cardsIDs)
                        {
                            cards.Add(opponent.deck.Main[card]);
                        }
                        MyCanvas hand = (Application.Current.MainWindow as MainWindow).card_2_hand;
                        hand.WhenAddChildren -= CardAreaEvent.add2HandOP;
                        OpponentOperate.FirstAtk(5, cards, mainwindow.card_2_Deck, mainwindow.card_2_hand);
                        hand.WhenAddChildren += CardAreaEvent.add2HandOP;
                    }

                    break;
                case ActionCommand.GAME_SET_PHASE:
                    {
                        PhaseInfo phase = JsonConvert.DeserializeObject<PhaseInfo>(bj.json);
                        Rectangle rta = null;
                        switch (phase.phase)
                        {
                            case Phase.STOP:
                                break;
                            case Phase.DP:
                                rta = mainwindow.FindName("rta_dp") as Rectangle;
                                break;
                            case Phase.SP:
                                rta = mainwindow.FindName("rta_sp") as Rectangle;
                                break;
                            case Phase.M1:
                                rta = mainwindow.FindName("rta_m1") as Rectangle;
                                break;
                            case Phase.BP:
                                rta = mainwindow.FindName("rta_bp") as Rectangle;
                                break;
                            case Phase.M2:
                                rta = mainwindow.FindName("rta_m2") as Rectangle;
                                break;
                            case Phase.EP:
                                rta = mainwindow.FindName("rta_ep") as Rectangle;
                                break;
                        }



                        int press = Grid.GetColumn(rta);
                        OpponentOperate.ChangePhase(rta, press);
                        
                    }
                    break;
                case ActionCommand.GAME_DRAW:
                    {
                        DrawInfo drawInfo = JsonConvert.DeserializeObject<DrawInfo>(bj.json);
                        if (drawInfo.isBack)
                        {
                            CardUI card = getCardIDOP(drawInfo.cardID);
                            MyCanvas hand = (Application.Current.MainWindow as MainWindow).card_2_hand;
                            hand.WhenAddChildren -= CardAreaEvent.add2HandOP;
                            OpponentOperate.DrawCard(card, mainwindow.card_2_Deck, mainwindow.card_2_hand);
                            hand.WhenAddChildren += CardAreaEvent.add2HandOP;
                        }
                    }
                    
                    break;
                case ActionCommand.GAME_LIFE_CHANGE:
                    break;
                case ActionCommand.CARD_ACTIVE:
                    {
                        ActiveInfo activeInfo = JsonConvert.DeserializeObject<ActiveInfo>(bj.json);
                        CardUI card = getCardIDOP(activeInfo.cardID);
                        card.active();
                    }   
                    break;
                case ActionCommand.CARR_SELECT_AIM:
                    {
                        Aim2CardInfo aimInfo = JsonConvert.DeserializeObject<Aim2CardInfo>(bj.json);
                        CardUI card = getCardIDOP(aimInfo.cardID);
                        card.beAim();
                    }
                    break;
                case ActionCommand.CARD_MOVE:
                    {
                        MoveInfo moveInfo = JsonConvert.DeserializeObject<MoveInfo>(bj.json);
                        CardUI card = null;
                        card = getCardIDOP(moveInfo.cardID);

                        MyCanvas mcv_orgin = card.Parent as MyCanvas;
                        MyCanvas mcv_aim = getCanvasByArea(moveInfo.aimArea);
                        Point start = card.TranslatePoint(new Point(), mainwindow.OpBattle);
                        Point end = mcv_aim.TranslatePoint(new Point(), mainwindow.OpBattle);
                        end = new Point(end.X + (mcv_aim.ActualWidth - card.Width) / 2, end.Y + (mcv_aim.ActualHeight - card.Height) / 2);

                        #region 怪物区→魔陷，清除指示物

                        switch (mcv_aim.area)
                        {
                            case Area.MAGICTRAP_1_OP:
                            case Area.MAGICTRAP_2_OP:
                            case Area.MAGICTRAP_3_OP:
                            case Area.MAGICTRAP_4_OP:
                            case Area.MAGICTRAP_5_OP:
                                {
                                    switch (mcv_orgin.area)
                                    {
                                        case Area.MONSTER_1_OP:
                                        case Area.MONSTER_2_OP:
                                        case Area.MONSTER_3_OP:
                                        case Area.MONSTER_4_OP:
                                        case Area.MONSTER_5_OP:
                                            card.clearSigns();
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                break;
                            default:
                                break;
                        }

                        #endregion

                        #region 魔陷→怪物区，清除指示物

                        switch (mcv_aim.area)
                        {
                            case Area.MONSTER_1_OP:
                            case Area.MONSTER_2_OP:
                            case Area.MONSTER_3_OP:
                            case Area.MONSTER_4_OP:
                            case Area.MONSTER_5_OP:
                                {
                                    switch (mcv_orgin.area)
                                    {
                                        case Area.MAGICTRAP_1_OP:
                                        case Area.MAGICTRAP_2_OP:
                                        case Area.MAGICTRAP_3_OP:
                                        case Area.MAGICTRAP_4_OP:
                                        case Area.MAGICTRAP_5_OP:
                                            card.clearSigns();
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                break;
                            default:
                                break;
                        }

                        #endregion

                        card.getAwayFromParents();


                        #region 纯移动

                        if (card.Status == moveInfo.aimStatus)
                        {
                            card.Status = moveInfo.aimStatus;
                            (Application.Current.MainWindow as MainWindow).OpBattle.Children.Add(card);
                            Canvas.SetLeft(card, start.X);
                            Canvas.SetTop(card, start.Y);

                            if (card.Status == Status.BACK_DEF || card.Status == Status.FRONT_DEF)
                            {
                                start.X += (mcv_orgin.ActualHeight - card.Width) / 2 - (mcv_orgin.ActualWidth - card.Height) / 2;
                                start.Y += -card.Width - (mcv_orgin.ActualHeight - card.Width) / 2 + (mcv_orgin.ActualWidth - card.Height) / 2;
                                Canvas.SetLeft(card, start.X);
                                Canvas.SetTop(card, start.Y);
                            }
                            switch (mcv_aim.area)
                            {
                                case Area.HAND_OP:
                                    switch (mcv_orgin.area)
                                    {
                                        case Area.MAINDECK_OP:
                                            
                                            CardAnimation.CanvasXY_Scale_Rotate(card,mcv_aim);
                                            return;
                                        case Area.BANISH_OP:
                                            card.setAngle290();
                                            Canvas.SetLeft(card, start.X + (mcv_orgin.ActualHeight - card.Width) / 2.0 - (mcv_orgin.ActualHeight - card.Height) / 2.0);
                                            Canvas.SetTop(card, start.Y - (mcv_orgin.ActualWidth - card.Width) * 2 - card.Width);
                                            CardAnimation.move_rotate(card, end, mcv_aim, moveInfo.isAdd);
                                            return;
                                        default:
                                            break;
                                    }
                                    
                                    break;
                                case Area.MONSTER_1_OP:
                                case Area.MONSTER_2_OP:
                                case Area.MONSTER_3_OP:
                                case Area.MONSTER_4_OP:
                                case Area.MONSTER_5_OP:
                                    {
                                        if (mcv_orgin.area == Area.BANISH_OP)
                                        {
                                            card.setAngle290();
                                            Canvas.SetLeft(card, start.X + (mcv_orgin.ActualHeight - card.Width) / 2.0 - (mcv_orgin.ActualHeight - card.Height) / 2.0);
                                            Canvas.SetTop(card, start.Y - (mcv_orgin.ActualWidth - card.Width) * 2 - card.Width);
                                            CardAnimation.move_rotate(card, end, mcv_aim, moveInfo.isAdd);
                                            CardAnimation.commonSummon(mcv_aim);
                                            return;
                                        }
                                        if (moveInfo.isAdd)
                                        {
                                            if (mcv_aim.Children.Count > 0)
                                            {
                                                end.X += (mcv_aim.ActualWidth - card.Width) / 2;
                                            }
                                            
                                        }
                                        else
                                        {
                                            end.X -= (mcv_aim.ActualWidth - card.Width) / 2 + card.Width;
                                        }

                                        

                                    }
                                    
                                    break;
                                default:
                                    {
                                    
                                        
                                    }
                                    break;
                            }

                            if (mcv_orgin.area == Area.BANISH_OP)
                            {
                                card.setAngle290();
                                Canvas.SetLeft(card, start.X + (mcv_orgin.ActualHeight - card.Width) / 2.0 - (mcv_orgin.ActualHeight - card.Height) / 2.0);
                                Canvas.SetTop(card, start.Y - (mcv_orgin.ActualWidth - card.Width) * 2 - card.Width);
                                CardAnimation.move_rotate(card, end, mcv_aim, moveInfo.isAdd);
                                CardAnimation.commonSummon(mcv_aim);
                                return;
                            }

                            MyStoryboard msb = CardAnimation.CanvasXY(end);
                            msb.card = card;
                            msb.Completed += (sender, e) =>
                            {
                                msb.card.BeginAnimation(Canvas.LeftProperty, null);
                                msb.card.BeginAnimation(Canvas.TopProperty, null);

                                msb.card.getAwayFromParents();
                                card.Status = moveInfo.aimStatus;

                                if (moveInfo.isAdd)
                                {
                                    mcv_aim.Children.Add(msb.card);
                                    if (mcv_orgin.area == Area.EXTRA_OP && card.info.effect.Contains("灵摆效果"))
                                    {
                                        CardAnimation.commonSummon(mcv_aim);
                                    }
                                }
                                else
                                {
                                    mcv_aim.Children.Insert(0, card);
                                }

                                if (mcv_aim.Children.Count == 1)
                                {

                                    switch (moveInfo.aimStatus)
                                    {
                                        case Status.FRONT_ATK:
                                        case Status.BACK_ATK:
                                            msb.card.centerAtVerticalInParent();
                                            break;
                                        case Status.FRONT_DEF:
                                        case Status.BACK_DEF:
                                            msb.card.centerAtHorizontalInParent();
                                            break;
                                        default:
                                            break;
                                    }

                                    switch (mcv_aim.area)
                                    {
                                        case Area.MONSTER_1_OP:
                                        case Area.MONSTER_2_OP:
                                        case Area.MONSTER_3_OP:
                                        case Area.MONSTER_4_OP:
                                        case Area.MONSTER_5_OP:
                                            {
                                                switch (mcv_orgin.area)
                                                {
                                                    case Area.MONSTER_1_OP:
                                                    case Area.MONSTER_2_OP:
                                                    case Area.MONSTER_3_OP:
                                                    case Area.MONSTER_4_OP:
                                                    case Area.MONSTER_5_OP:
                                                        {
                                                            mcv_aim.WhenAddChildren -= CardAreaEvent.add2MonsterOP;
                                                            mcv_orgin.WhenRemoveChildren -= CardAreaEvent.removeFromMonsterOP;
                                                            while (mcv_orgin.Children.Count > 0)
                                                            {

                                                                CardUI c = mcv_orgin.Children[mcv_orgin.Children.Count - 1] as CardUI;
                                                                Point start2 = c.TranslatePoint(new Point(), mainwindow.OpBattle);
                                                                c.getAwayFromParents();
                                                                (Application.Current.MainWindow as MainWindow).OpBattle.Children.Add(c);
                                                                Canvas.SetLeft(c, start2.X);
                                                                Canvas.SetTop(c, start2.Y);
                                                                Point end2 = mcv_aim.TranslatePoint(new Point(), mainwindow.OpBattle);
                                                                end2.X = end2.X - c.Width;
                                                                end2.Y = end2.Y + (mcv_aim.ActualHeight - c.Height) / 2.0;
                                                                MyStoryboard msb2 = CardAnimation.CanvasXY(end2);
                                                                msb2.card = c;
                                                                msb2.Completed += (sender2, e2) =>
                                                                {
                                                                    msb2.card.BeginAnimation(Canvas.LeftProperty, null);
                                                                    msb2.card.BeginAnimation(Canvas.TopProperty, null);

                                                                    Canvas.SetLeft(msb2.card, -c.Width);
                                                                    Canvas.SetTop(msb2.card, (mcv_aim.ActualHeight - c.Height) / 2.0);

                                                                    msb2.card.getAwayFromParents();
                                                                    mcv_aim.Children.Insert(0, c);

                                                                };

                                                                msb2.Begin(c);
                                                            }

                                                            #region 消除攻守显示

                                                            Binding bind = new Binding();
                                                            BindingOperations.ClearBinding(mcv_orgin.tb_atkDef, TextBlock.TextProperty);
                                                            mcv_orgin.tb_atkDef.IsHitTestVisible = false;

                                                            #endregion

                                                            mcv_aim.WhenAddChildren += CardAreaEvent.add2MonsterOP;
                                                            mcv_orgin.WhenRemoveChildren += CardAreaEvent.removeFromMonsterOP;
                                                        }
                                                        break;
                                                    default:
                                                        break;
                                                }
                                            }
                                            break;
                                    }

                                    

                                }
                                else
                                {
                                    msb.card.centerAtVerticalInParent();
                                }

                                
                            };
                            msb.Begin(card);

                            return;
                        }

                        #endregion

                        #region 背攻→背防

                        if (card.Status == Status.BACK_ATK && moveInfo.aimStatus == Status.BACK_DEF)
                        {
                            (Application.Current.MainWindow as MainWindow).OpBattle.Children.Add(card);
                            Canvas.SetLeft(card, start.X);
                            Canvas.SetTop(card, start.Y);

                            MyStoryboard msb = CardAnimation.CanvasXY_Rotate_0290(end);
                            msb.card = card;
                            msb.Completed += (sender, e) =>
                            {

                                msb.card.getAwayFromParents();
                                msb.card.BeginAnimation(Canvas.LeftProperty, null);
                                msb.card.BeginAnimation(Canvas.TopProperty, null);
                                if (moveInfo.isAdd)
                                {
                                    mcv_aim.Children.Add(msb.card);
                                    msb.card.set2BackDef();
                                    msb.card.centerAtHorizontalInParent();

                                }
                            };
                            msb.FillBehavior = FillBehavior.Stop;
                            msb.Begin(card);
                            break;
                        }

                        #endregion

                        #region 背攻→表攻

                        if (card.Status == Status.BACK_ATK && moveInfo.aimStatus == Status.FRONT_ATK)
                        {
                            

                            if (mcv_aim.Children.Count > 0)
                            {
                                if (moveInfo.isAdd)
                                {
                                    end.X += (mcv_aim.ActualWidth - card.Width) / 2;
                                }
                                else
                                {
                                    end.X -= (mcv_aim.ActualWidth - card.Width) / 2 + card.Width;
                                }
                            }

                            switch (mcv_orgin.area)
                            {
                                case Area.GRAVEYARD_OP:
                                    break;
                                
                                case Area.BANISH_OP:
                                    break;
                                case Area.SPACE_OP:
                                    break;
                                case Area.EXTRA_OP:
                                    {
                                        card.set2FrontAtk2();
                                        (Application.Current.MainWindow as MainWindow).OpBattle.Children.Add(card);
                                        Canvas.SetLeft(card, start.X);
                                        Canvas.SetTop(card, start.Y);

                                        MyStoryboard msb0 = CardAnimation.CanvasXY(end);
                                        msb0.card = card;
                                        msb0.Completed += (sender, e) =>
                                        {
                                            msb0.card.BeginAnimation(Canvas.LeftProperty, null);
                                            msb0.card.BeginAnimation(Canvas.TopProperty, null);

                                            msb0.card.getAwayFromParents();
                                            msb0.card.set2FrontAtk2();

                                            if (moveInfo.isAdd)
                                            {
                                                switch (mcv_aim.area)
                                                {

                                                    case Area.MONSTER_1_OP:
                                                    case Area.MONSTER_2_OP:
                                                    case Area.MONSTER_3_OP:
                                                    case Area.MONSTER_4_OP:
                                                    case Area.MONSTER_5_OP:
                                                        if (mcv_aim.Children.Count > 0)
                                                        {
                                                            if (card.info.sCardType.Equals("XYZ怪兽"))
                                                            {
                                                                CardAnimation.overlaySummon(mcv_aim);
                                                            }
                                                            
                                                        }
                                                        else
                                                        {
                                                            if (card.info.sCardType.Equals("同调怪兽"))
                                                            {
                                                                CardAnimation.synchroSummon(mcv_aim);
                                                            }
                                                            else
                                                            {
                                                                CardAnimation.commonSummon(mcv_aim);
                                                            }
                                                        }
                                                        break;
                                                    default:
                                                        break;
                                                }
                                                
                                                mcv_aim.Children.Add(msb0.card);
                                                

                                            }
                                            else
                                            {
                                                mcv_aim.Children.Insert(0, msb0.card);
                                            }

                                        };
                                        msb0.Begin(card);
                                    }
                                    break;
                                case Area.MAINDECK_OP:
                                case Area.HAND_OP:
                                    {
                                        
                                        card.set2FrontAtk2();
                                        (Application.Current.MainWindow as MainWindow).OpBattle.Children.Add(card);
                                        Canvas.SetLeft(card, start.X);
                                        Canvas.SetTop(card, start.Y);

                                        MyStoryboard msb0 = CardAnimation.CanvasXY(end);
                                        msb0.card = card;
                                        msb0.Completed += (sender, e) =>
                                        {
                                            msb0.card.BeginAnimation(Canvas.LeftProperty, null);
                                            msb0.card.BeginAnimation(Canvas.TopProperty, null);

                                            msb0.card.getAwayFromParents();
                                            msb0.card.set2FrontAtk2();

                                            if (moveInfo.isAdd)
                                            {
                                                mcv_aim.Children.Add(msb0.card);
                                                CardAnimation.commonSummon(mcv_aim);
                                            }
                                            else
                                            {
                                                mcv_aim.Children.Insert(0, msb0.card);

                                            }

                                        };
                                        msb0.Begin(card);
                                        
                                    }
                                    break;
                                case Area.MONSTER_1_OP:
                                case Area.MONSTER_2_OP:
                                case Area.MONSTER_3_OP:
                                case Area.MONSTER_4_OP:
                                case Area.MONSTER_5_OP:
                                case Area.MAGICTRAP_1_OP:
                                case Area.MAGICTRAP_2_OP:
                                case Area.MAGICTRAP_3_OP:
                                case Area.MAGICTRAP_4_OP:
                                case Area.MAGICTRAP_5_OP:
                                case Area.PENDULUM_LEFT_OP:
                                case Area.PENDULUM_RIGHT_OP:
                                    {
                                        card.set2FrontAtk2();
                                        (Application.Current.MainWindow as MainWindow).OpBattle.Children.Add(card);
                                        Canvas.SetLeft(card, start.X);
                                        Canvas.SetTop(card, start.Y);

                                        MyStoryboard msb = CardAnimation.CanvasXY(end);
                                        msb.card = card;
                                        msb.Completed += (sender, e) =>
                                        {
                                            msb.card.BeginAnimation(Canvas.LeftProperty, null);
                                            msb.card.BeginAnimation(Canvas.TopProperty, null);
                                            msb.card.getAwayFromParents();
                                            msb.card.set2FrontAtk();
                                            if (moveInfo.isAdd)
                                            {
                                                mcv_aim.Children.Add(msb.card);
                                            }
                                            else
                                            {
                                                mcv_aim.Children.Insert(0,msb.card);
                                            }
                                            
                                        };
                                        msb.Begin(card);
                                    }
                                    break;
                                default:
                                    break;
                            }

                            


                        }

                        #endregion

                        #region 背防→表攻

                        if ((card.Status == Status.BACK_DEF) && moveInfo.aimStatus == Status.FRONT_ATK)
                        {
                            //card.set2FrontAtk2();
                            //(Application.Current.MainWindow as MainWindow).OpBattle.Children.Add(card);
                            //Canvas.SetLeft(card, start.X);
                            //Canvas.SetTop(card, start.Y);

                            switch (mcv_aim.area)
                            {
                                case Area.PENDULUM_LEFT_OP:
                                case Area.PENDULUM_RIGHT_OP:
                                case Area.MAGICTRAP_1_OP:
                                case Area.MAGICTRAP_2_OP:
                                case Area.MAGICTRAP_3_OP:
                                case Area.MAGICTRAP_4_OP:
                                case Area.MAGICTRAP_5_OP:
                                case Area.EXTRA_OP:
                                case Area.GRAVEYARD_OP:
                                    {
                                        (Application.Current.MainWindow as MainWindow).OpBattle.Children.Add(card);
                                        //Canvas.SetLeft(card, start.X);
                                        //Canvas.SetTop(card, start.Y);
                                        start.X += (mcv_orgin.ActualHeight - card.Width) / 2 - (mcv_orgin.ActualWidth - card.Height) / 2;
                                        start.Y += -card.Width - (mcv_orgin.ActualHeight - card.Width) / 2 + (mcv_orgin.ActualWidth - card.Height) / 2;

                                        Canvas.SetLeft(card, start.X);
                                        Canvas.SetTop(card, start.Y);
                                        card.set2FrontAtk2();
                                        CardAnimation.move_rotate(card, end, mcv_aim,moveInfo.isAdd);
                                    }
                                    break;
                                case Area.MONSTER_1_OP:
                                case Area.MONSTER_2_OP:
                                case Area.MONSTER_3_OP:
                                case Area.MONSTER_4_OP:
                                case Area.MONSTER_5_OP:
                                    {
                                        card.set2FrontAtk2();
                                        (Application.Current.MainWindow as MainWindow).OpBattle.Children.Add(card);
                                        Canvas.SetLeft(card, start.X);
                                        Canvas.SetTop(card, start.Y);
                                        if (mcv_aim.Children.Count > 0)
                                        {
                                            if (moveInfo.isAdd)
                                            {
                                                end.X += (mcv_aim.ActualWidth - card.Width) / 2;
                                            }
                                            else
                                            {
                                                end.X -= (mcv_aim.ActualWidth - card.Width) / 2 + card.Width;
                                            }
                                        }

                                        start.X += (mcv_aim.ActualHeight - card.Width) / 2 - (mcv_aim.ActualWidth - card.Height) / 2;
                                        start.Y += -card.Width - (mcv_aim.ActualHeight - card.Width) / 2 + (mcv_aim.ActualWidth - card.Height) / 2;
                                        //card.getAwayFromParents();
                                        Canvas.SetLeft(card, start.X);
                                        Canvas.SetTop(card, start.Y);

                                        MyStoryboard msb1 = CardAnimation.CanvasXY_Rotate_9020(end);

                                        msb1.card = card;
                                        msb1.Completed += (sender, e) =>
                                        {
                                            msb1.card.BeginAnimation(Canvas.LeftProperty, null);
                                            msb1.card.BeginAnimation(Canvas.TopProperty, null);

                                            msb1.card.getAwayFromParents();
                                            msb1.card.set2FrontAtk();
                                            if (moveInfo.isAdd)
                                            {
                                                mcv_aim.Children.Add(msb1.card);

                                            }
                                            else
                                            {
                                                mcv_aim.Children.Insert(0, msb1.card);

                                            }

                                        };
                                        msb1.Begin(card);
                                    }
                                    break;
                                default:
                                    break;
                            }

                            break;
                        }

                        #endregion

                        #region →背攻

                        if (moveInfo.aimStatus == Status.BACK_ATK)
                        {
                            

                            switch (card.Status)
                            {
                                case Status.FRONT_ATK:
                                    {
                                        switch (mcv_orgin.area)
                                        {
                                            case Area.BANISH_OP:
                                                card.set2FrontAtk2();
                                                (Application.Current.MainWindow as MainWindow).OpBattle.Children.Add(card);
                                                card.setAngle290();
                                                Canvas.SetLeft(card, start.X + (mcv_orgin.ActualHeight - card.Width)/2.0 - (mcv_orgin.ActualHeight - card.Height) / 2.0);
                                                Canvas.SetTop(card, start.Y - (mcv_orgin.ActualWidth - card.Width)*2 - card.Width);
                                                CardAnimation.CanvasXY_Scale_Rotate3(card, mcv_aim, moveInfo.isAdd);
                                                break;
                                            //case Area.GRAVEYARD_OP:
                                            //    (Application.Current.MainWindow as MainWindow).OpBattle.Children.Add(card);
                                            //    Canvas.SetLeft(card, start.X );
                                            //    Canvas.SetTop(card, start.Y);
                                            //    CardAnimation.CanvasXY_Scale_Rotate2(card, mcv_aim,moveInfo.isAdd);
                                            //    break;
                                            case Area.EXTRA_OP:
                                                break;
                                            case Area.MONSTER_1_OP:
                                            case Area.MONSTER_2_OP:
                                            case Area.MONSTER_3_OP:
                                            case Area.MONSTER_4_OP:
                                            case Area.MONSTER_5_OP:
                                            case Area.GRAVEYARD_OP:
                                            default:
                                                {
                                                    //card.set2BackAtk2();
                                                    MyCanvas cv = (Application.Current.MainWindow as MainWindow).OpBattle as MyCanvas;
                                                    cv.WhenAddChildren -= CardAreaEvent.add2OPBattle;
                                                    cv.Children.Add(card);
                                                    Canvas.SetLeft(card, start.X);
                                                    Canvas.SetTop(card, start.Y);
                                                    CardAnimation.CanvasXY_Scale_Rotate2(card, mcv_aim, moveInfo.isAdd);
                                                    cv.WhenAddChildren += CardAreaEvent.add2OPBattle;//CardAnimation.move(card, end, mcv_aim,moveInfo.isAdd);
                                                }
                                                break;
                                        }
                                        
                                    }
                                    break;
                                case Status.FRONT_DEF:
                                    {
                                        //card.set2BackAtk2();

                                        (Application.Current.MainWindow as MainWindow).OpBattle.Children.Add(card);
                                        start.X += (mcv_orgin.ActualHeight - card.Width) / 2 - (mcv_orgin.ActualWidth - card.Height) / 2;
                                        start.Y += -card.Width - (mcv_orgin.ActualHeight - card.Width) / 2 + (mcv_orgin.ActualWidth - card.Height) / 2;

                                        Canvas.SetLeft(card, start.X);
                                        Canvas.SetTop(card, start.Y);

                                        CardAnimation.CanvasXY_Scale_Rotate2(card, mcv_aim, moveInfo.isAdd);
                                        //CardAnimation.move_rotate(card, end, mcv_aim,moveInfo.isAdd);

                                    }
                                    break;
                                case Status.BACK_DEF:
                                    {
                                        card.set2BackAtk2();

                                        (Application.Current.MainWindow as MainWindow).OpBattle.Children.Add(card);
                                        start.X += (mcv_orgin.ActualHeight - card.Width) / 2 - (mcv_orgin.ActualWidth - card.Height) / 2;
                                        start.Y += -card.Width - (mcv_orgin.ActualHeight - card.Width) / 2 + (mcv_orgin.ActualWidth - card.Height) / 2;
                                        Canvas.SetLeft(card, start.X);
                                        Canvas.SetTop(card, start.Y);

                                        CardAnimation.move_rotate(card, end, mcv_aim,moveInfo.isAdd);

                                    }

                                    break;
                                default:
                                    break;
                            }
                        }

                        #endregion

                        #region 正防→表攻

                        if (card.Status == Status.FRONT_DEF && moveInfo.aimStatus == Status.FRONT_ATK)
                        {
                            card.set2FrontAtk2();

                            (Application.Current.MainWindow as MainWindow).OpBattle.Children.Add(card);

                            start.X += (mcv_aim.ActualHeight - card.Width) / 2 - (mcv_aim.ActualWidth - card.Height) / 2;
                            start.Y += -card.Width - (mcv_aim.ActualHeight - card.Width) / 2 + (mcv_aim.ActualWidth - card.Height) / 2;
                            Canvas.SetLeft(card, start.X);
                            Canvas.SetTop(card, start.Y);

                            switch (mcv_aim.area)
                            {
                                case Area.MONSTER_1_OP:
                                case Area.MONSTER_2_OP:
                                case Area.MONSTER_3_OP:
                                case Area.MONSTER_4_OP:
                                case Area.MONSTER_5_OP:
                                    if (mcv_aim.Children.Count > 0)
                                    {
                                        end.X = end.X - (mcv_aim.ActualWidth - card.Width) / 2 - card.Width;
                                    }
                                    break;
                            }
                            

                            CardAnimation.move_rotate(card, end, mcv_aim,moveInfo.isAdd);

                        }

                        #endregion

                            
                        }
                    break;
                case ActionCommand.CARD_DISAPPEAR:
                    {
                        DisappearInfo disappearInfo = JsonConvert.DeserializeObject<DisappearInfo>(bj.json);
                        CardUI card = getCardIDOP(disappearInfo.cardID);
                        MyStoryboard msb = CardAnimation.Opacity20(100);
                        msb.card = card;
                        msb.Completed += (sender, e) =>
                        {
                            msb.card.getAwayFromParents();
                            msb.card.Status = disappearInfo.aimStatus;
                            card.showImg();
                            (Application.Current.MainWindow as MainWindow).card_2_Outside.Children.Add(card);
                            
                        };
                        MyStoryboard msb2 = CardAnimation.Opacity21(100);
                        TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();
                        animator.addAnime(msb).addAnime(msb2).Begin(card);
                    }   
                    break;
                case ActionCommand.CARD_SIGN_ACTION:
                    {
                        SignInfo signInfo = JsonConvert.DeserializeObject<SignInfo>(bj.json);
                        CardUI card = getCardIDOP(signInfo.cardID);
                        
                        MyCanvas mcv = card.Parent as MyCanvas;
                        StackPanel sp = mcv.signs;
                        sp.Children.Clear();
                        card.clearSigns();
                        foreach (var item in signInfo.signs)
                        {
                            if (!item.count.Equals("0"))
                            {
                                SignTextBlock stb = new SignTextBlock(false);
                                stb.Height = 25;
                                stb.Width = 25;
                                stb.BorderBrush = item.brush;
                                stb.Content = item.count;
                                card.signs.Add(stb);
                                stb.ToolTip = item.remark;
                                stb.Tag = card;
                                sp.Children.Add(stb);
                            }       
                        }                                      
                    }
                    break;
                case ActionCommand.CARD_ATK:
                    {
                        AtkInfo atkInfo = JsonConvert.DeserializeObject<AtkInfo>(bj.json);
                        CardUI card = getCardIDOP(atkInfo.cardID);
                        CardUI aim = getCardID(atkInfo.aimID);
                        OpponentOperate.Atk(card, aim);
                    }
                    break;
                case ActionCommand.CARD_STATUS_CHANGE:
                    {
                        StatusChangeInfo statuschangeInfo = JsonConvert.DeserializeObject<StatusChangeInfo>(bj.json);
                        CardUI card = getCardIDOP(statuschangeInfo.cardID);
                        MyCanvas mcv = card.Parent as MyCanvas;
                        switch (card.Status)
                        {
                            case Status.FRONT_ATK:
                                if (statuschangeInfo.aimStatus == Status.FRONT_DEF)
                                {
                                    
                                    CardAnimation.Rotate2FrontDef(card);
                                }
                                else if (statuschangeInfo.aimStatus == Status.BACK_ATK)
                                {
                                    card.set2BackAtk2();
                                    CardAnimation.turn(card);
                                    cleanTbAtk(mcv);
                                }
                                else if (statuschangeInfo.aimStatus == Status.BACK_DEF)
                                {
                                    CardAnimation.turn2BackDef(card);
                                    cleanTbAtk(mcv);
                                }     
                                break;
                            case Status.FRONT_DEF:
                                if (statuschangeInfo.aimStatus == Status.FRONT_ATK)
                                {
                                    CardAnimation.Rotate2FrontAtk(card);
                                }
                                else if (statuschangeInfo.aimStatus == Status.BACK_DEF)
                                {
                                    card.set2BackDef2();
                                    CardAnimation.turn(card);
                                    cleanTbAtk(mcv);
                                }                         
                                break;
                            case Status.BACK_ATK:
                                if (statuschangeInfo.aimStatus == Status.BACK_DEF)
                                {
                                    CardAnimation.Rotate2BackDef(card);
                                    cleanTbAtk(mcv);
                                }
                                else if (statuschangeInfo.aimStatus == Status.FRONT_ATK)
                                {
                                    card.set2FrontAtk2();
                                    CardAnimation.turn(card);
                                    CardOperate.sort_XYZ_atk(mcv);
                                    CardAreaEvent.bindingAtk(mcv, card);
                                }                            
                                break;
                            case Status.BACK_DEF:
                                if (statuschangeInfo.aimStatus == Status.FRONT_ATK)
                                {
                                    card.set2FrontAtk2();
                                    CardAnimation.rotate_turn(card);
                                    //CardAnimation.Rotate2FrontAtk2(card);
                                    //CardAreaEvent.bindingAtk(mcv, card);
                                }
                                else if (statuschangeInfo.aimStatus == Status.FRONT_DEF)
                                {
                                    card.set2FrontDef2();
                                    CardAnimation.turn(card);
                                    CardAreaEvent.bindingAtk(mcv, card);
                                }
                                else if (statuschangeInfo.aimStatus == Status.BACK_DEF)
                                {
                                    CardAnimation.turn2BackDef(card);
                                    cleanTbAtk(mcv);
                                }              
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ActionCommand.CARD_MESSAGE:
                    {
                        CardMessage cardMessage = JsonConvert.DeserializeObject<CardMessage>(bj.json);
                        CardUI card = getCardIDOP(cardMessage.cardID);
                        card.CurAtk = cardMessage.curAtk;
                        card.CurDef = cardMessage.curDef;
                        card.ToolTip = cardMessage.remark;
                    }
                    break;
                default:
                    break;
            }

            //try
            //{
            //    if ( msg.StartsWith("[") && msg.EndsWith("]"))
            //    {
            //        msg = msg.TrimStart('[').TrimEnd(']');
            //        string[] msgs = msg.Split(new string[] { "]^[" }, System.StringSplitOptions.None);
            //        if (msgs.Length == 1)
            //        {
            //            msgs = msgs[0].Split(new char[] { '=', ',' }, StringSplitOptions.None);
            //            switch (msgs[0])
            //            {
            //                case "SetP1":
            //                    {
            //                        DuelOperate.getInstance().myself.userindex = msgs[1];
            //                        DuelOperate.getInstance().myself.name = msgs[2];
            //                        mainwindow.tbk_opname.Text = DuelOperate.getInstance().myself.name;
            //                    }
            //                    break;
            //                case "SetP2":
            //                    {
            //                        DuelOperate.getInstance().opponent.userindex = msgs[1];
            //                        DuelOperate.getInstance().opponent.name = msgs[2];
            //                        mainwindow.tbk_opname.Text = DuelOperate.getInstance().opponent.name;
            //                        UIAnimation.getInstance().rotateAnimation.Stop();
            //                        UIAnimation.getInstance().opactiy20.Begin(mainwindow.img_serchP2);
            //                        mainwindow.img_head_op.Source = new BitmapImage(new Uri("Image\\head3.jpg", UriKind.RelativeOrAbsolute));
            //                        UIAnimation.getInstance().opacity21.Begin(mainwindow.img_head_op);
                                    

            //                        //UIAnimation.opacityChange(0).Begin();
            //                    }
            //                    break;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        string[] msgs = msg.Split(new char[] { '=', ',' }, StringSplitOptions.None);

            //        foreach (string msg_ in msgs)
            //        {
            //            Console.WriteLine(msg);
            //        }

            //        if (msgs[2].Equals("-1"))
            //        {
            //            mainwindow.report.AppendText("System：" + Environment.NewLine + "与玩家 - " + msgs[1] + " - 成功建立连接" + Environment.NewLine);
            //            //DuelOperate.getInstance().Opponent
            //            DuelOperate.getInstance().opponent.userindex = msgs[0];
            //            DuelOperate.getInstance().opponent.name = msgs[1];
            //            mainwindow.tbk_opname.Text = DuelOperate.getInstance().opponent.name;

            //        }
            //        else if (msgs[4].Equals("MSG"))
            //        {
            //            mainwindow.report.AppendText(opponent.name + "：" + Environment.NewLine);
            //            mainwindow.report.AppendText("[" + msgs[2] + "] " + msgs[3] + Environment.NewLine);
            //        }
            //        else if (msgs[4].Equals("Chat"))
            //        {
            //            var p = new Paragraph(); // Paragraph 类似于 html 的 P 标签                  
            //            string opsay = "";
            //            for (int i = 5; i < msgs.Length; i++)
            //            {
            //                opsay += msgs[i];
            //            }
            //            var r = new Run(opsay); // Run 是一个 Inline 的标签  
            //            p.Inlines.Add(r);
            //            p.Foreground = Brushes.Red;//设置字体颜色  
            //            mainwindow.tb_chat_view.Document.Blocks.Add(p);
            //            mainwindow.tb_chat_view.ScrollToEnd();
            //            if (!mainwindow.btn_viewreport.Content.Equals("R"))
            //            {
            //                mainwindow.btn_viewreport.Content = Convert.ToInt32(mainwindow.btn_viewreport.Content) + 1;
            //            }

            //        }
            //        else
            //        {
            //            mainwindow.report.AppendText(opponent.name + "：" + Environment.NewLine);
            //            mainwindow.report.AppendText("[" + msgs[2] + "] " + msgs[3] + Environment.NewLine);


            //            OpponentOperate.ActionAnalyze(msg, false);
            //        }
            //    }


                
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Receive:[" + msg + "]" + ex);

            //}

        }

        private static void synchroAnime(MyCanvas mcv_aim)
        {
            Point summon2 = mcv_aim.TranslatePoint(new Point(0.5, 0.5), mainwindow.OpBattle);
            Canvas.SetLeft(mainwindow.img_synchro_op, summon2.X - ((mainwindow.img_synchro_op.Width - mcv_aim.ActualWidth) / 2));
            Canvas.SetTop(mainwindow.img_synchro_op, summon2.Y - ((mainwindow.img_synchro_op.Height - mcv_aim.ActualHeight) / 2));
            CardAnimation.Rotate_Scale_FadeInAndOut(mainwindow.img_synchro_op);
        }

        private static void summonAnime(MyCanvas mcv_aim)
        {
            Point summon2 = mcv_aim.TranslatePoint(new Point(0.5, 0.5), mainwindow.OpBattle);
            Canvas.SetLeft(mainwindow.img_summon_op, summon2.X - ((mainwindow.img_summon_op.Width - mcv_aim.ActualWidth) / 2));
            Canvas.SetTop(mainwindow.img_summon_op, summon2.Y - ((mainwindow.img_summon_op.Height - mcv_aim.ActualHeight) / 2));
            CardAnimation.Rotate_Scale_FadeInAndOut(mainwindow.img_summon_op);
        }

        private static void overlayAnime(MyCanvas mcv_aim)
        {
            Point summon2 = mcv_aim.TranslatePoint(new Point(0.5, 0.5), mainwindow.OpBattle);
            Canvas.SetLeft(mainwindow.img_overlay_op, summon2.X - ((mainwindow.img_overlay_op.Width - mcv_aim.ActualWidth) / 2));
            Canvas.SetTop(mainwindow.img_overlay_op, summon2.Y - ((mainwindow.img_overlay_op.Height - mcv_aim.ActualHeight) / 2));
            CardAnimation.Rotate_Scale_FadeInAndOut(mainwindow.img_overlay_op);
        }

        public CardUI getCardIDOP(int cardid)
        {
            CardUI card;
            if (cardid < 0)
            {
                card = opponent.deck.Extra[-cardid - 1];
            }
            else
            {
                card = opponent.deck.Main[cardid];
            }

            return card;
        }

        public CardUI getCardID(int cardid)
        {
            CardUI card;
            if (cardid < 0)
            {
                card = myself.deck.Extra[-cardid - 1];
            }
            else
            {
                card = myself.deck.Main[cardid];
            }

            return card;
        }

        private static void cleanTbAtk(MyCanvas mcv)
        {
            if (mcv.tb_atkDef == null)
            {
                return;
            }
            Binding bind = new Binding();
            BindingOperations.ClearBinding(mcv.tb_atkDef, TextBlock.TextProperty);
            mcv.tb_atkDef.IsHitTestVisible = false;
        }

        #region 根据Area获取Canvas

        private MyCanvas getCanvasByArea (Area area)
        {
            switch (area)
            {
                case Area.NON_VALUE:
                    return null;
                case Area.GRAVEYARD:
                    return (Application.Current.MainWindow as MainWindow).card_2_Graveyard;
                case Area.MAINDECK:
                    return (Application.Current.MainWindow as MainWindow).card_2_Deck;
                case Area.BANISH:
                    return null;
                case Area.SPACE:
                    return null;
                case Area.EXTRA:
                    return (Application.Current.MainWindow as MainWindow).card_2_Extra;
                case Area.HAND:
                    return (Application.Current.MainWindow as MainWindow).card_2_hand;
                case Area.MONSTER_1:
                    return (Application.Current.MainWindow as MainWindow).card_2_6;
                case Area.MONSTER_2:
                    return (Application.Current.MainWindow as MainWindow).card_2_7;
                case Area.MONSTER_3:
                    return (Application.Current.MainWindow as MainWindow).card_2_8;
                case Area.MONSTER_4:
                    return (Application.Current.MainWindow as MainWindow).card_2_9;
                case Area.MONSTER_5:
                    return (Application.Current.MainWindow as MainWindow).card_2_10;
                case Area.MAGICTRAP_1:
                    return (Application.Current.MainWindow as MainWindow).card_2_1;
                case Area.MAGICTRAP_2:
                    return (Application.Current.MainWindow as MainWindow).card_2_2;
                case Area.MAGICTRAP_3:
                    return (Application.Current.MainWindow as MainWindow).card_2_3;
                case Area.MAGICTRAP_4:
                    return (Application.Current.MainWindow as MainWindow).card_2_4;
                case Area.MAGICTRAP_5:
                    return (Application.Current.MainWindow as MainWindow).card_2_5;
                case Area.PENDULUM_LEFT:
                    return (Application.Current.MainWindow as MainWindow).card_2_Left;
                case Area.PENDULUM_RIGHT:
                    return (Application.Current.MainWindow as MainWindow).card_2_Right;
                case Area.GRAVEYARD_OP:
                    return null;
                case Area.MAINDECK_OP:
                    return null;
                case Area.BANISH_OP:
                    return null;
                case Area.SPACE_OP:
                    return null;
                case Area.EXTRA_OP:
                    return null;
                case Area.HAND_OP:
                    return null;
                case Area.MONSTER_1_OP:
                    return (Application.Current.MainWindow as MainWindow).card_2_6;
                case Area.MONSTER_2_OP:
                    return (Application.Current.MainWindow as MainWindow).card_2_7;
                case Area.MONSTER_3_OP:
                    return (Application.Current.MainWindow as MainWindow).card_2_8;
                case Area.MONSTER_4_OP:
                    return (Application.Current.MainWindow as MainWindow).card_2_9;
                case Area.MONSTER_5_OP:
                    return (Application.Current.MainWindow as MainWindow).card_2_10;
                case Area.MAGICTRAP_1_OP:
                    return null;
                case Area.MAGICTRAP_2_OP:
                    return null;
                case Area.MAGICTRAP_3_OP:
                    return null;
                case Area.MAGICTRAP_4_OP:
                    return null;
                case Area.MAGICTRAP_5_OP:
                    return null;
                case Area.PENDULUM_LEFT_OP:
                    return null;
                case Area.PENDULUM_RIGHT_OP:
                    return null;
                default:
                    return null;
            }
        }

        #endregion

        #endregion

        #region <-- 释放资源 -->

        public void Dispose()
        {



            //deck_1  = null;  //己方主卡组
            //extra_1 = null; //己方额外卡组
            //side_1 = null;

            //deck_2 = null;
            //extra_2 = null;
            //side_2 = null;

            //GC.Collect();


            //throw new NotImplementedException();
        }

        #endregion

        #region <-- 更换side -->

        internal void sideMode()
        {
            
            //sendMsg("MSG=", "更换SIDE");
            temp_deck = new Deck();
            temp_deck.Main.AddRange(myself.deck.Main);
            temp_deck.Extra.AddRange(myself.deck.Extra);
            temp_deck.Side.AddRange(myself.deck.Side);
            
            mainwindow.bsb_menu_hide.Actions.RemoveAt(0);

            Grid gd = mainwindow.btn_sideMode.Parent as Grid;
            gd.ColumnDefinitions[1].Style = mainwindow.Resources["star"] as Style;

            mainwindow.btn_sideMode.Content = "确认更换";
            mainwindow.btn_deck.IsEnabled = false;
            mainwindow.btn_start.IsEnabled = false;
            mainwindow.btn_firstAtk.IsEnabled = false;
            mainwindow.btn_secondAtk.IsEnabled = false;
            mainwindow.tbc_DeckDocument.Visibility = Visibility.Hidden;  //隐藏卡组选择
            mainwindow.gd_decksManerger.Visibility = System.Windows.Visibility.Visible;

            #region 血条恢复

            mainwindow.tbk_life_P1.Text = "0";
            MyStoryboard msb6 = CardAnimation.LifeChange(mainwindow.rt_life_P1, 0, 800);
            msb6.Begin();

            #endregion
            
           // CardOperate.sort(mainwindow.cv_side, null);

            MyStoryboard msb = UIAnimation.SPmove(mainwindow.sp_main, new Thickness(0, -340, 0, 0), new Thickness(0), 600);
            msb.Completed += (object c, EventArgs d) =>
            {
                for (int i = 0; i < temp_deck.Main.Count; i++)
                {
                    CardUI card = temp_deck.Main[i];
                    Base.getawayParerent(card);
                    mainwindow.cv_main.Children.Add(card);
                }
                CardOperate.sort(mainwindow.cv_main, null);
            };
            MyStoryboard msb2 = UIAnimation.SPmove(mainwindow.sp_extra, new Thickness(0, -430, 0, 0), new Thickness(0), 600);
            msb2.Completed += (object c, EventArgs d) =>
            {
                for (int i = 0; i < temp_deck.Extra.Count; i++)
                {
                    CardUI card = temp_deck.Extra[i];
                    Base.getawayParerent(card);
                    mainwindow.cv_extra.Children.Add(card);
                }
                CardOperate.sort(mainwindow.cv_extra, null);
            };
            MyStoryboard msb3 = UIAnimation.SPmove(mainwindow.sp_side, new Thickness(0, -520, 0, 0), new Thickness(0), 600);
            msb2.Completed += (object c, EventArgs d) =>
            {
                for (int i = 0; i < temp_deck.Side.Count; i++)
                {
                    CardUI card = temp_deck.Side[i];
                    Base.getawayParerent(card);
                    mainwindow.cv_side.Children.Add(card);
                }
                CardOperate.sort(mainwindow.cv_side, null);
            };
            MyStoryboard msb4 = UIAnimation.SPmove(mainwindow.tbc_DeckDocument, new Thickness(0, 102, 0, 0), new Thickness(0), 600);
            MyStoryboard msb5 = UIAnimation.showDecksManerger(mainwindow.gd_decksManerger, 0, 1, 300);


            msb5.Begin();
            msb.Begin();
            msb2.Begin();
            msb3.Begin();
            msb4.Begin();


            
            
        }


        #endregion
        
        #region <-- side确认更改 -->

        internal void sideModeEnd()
        {
            if (temp_deck.Main.Count != myself.deck.Main.Count)
            {
                MessageBox.Show("主卡组数量不能改变！应为" + myself.deck.Main.Count + "张");
                return;
            }
            if (temp_deck.Extra.Count != myself.deck.Extra.Count)
            {
                MessageBox.Show("额外数量不能改变！应为" + myself.deck.Extra.Count + "张");
                return;
            }

            mainwindow.btn_sideMode.Content = "更换SIDE";
            Grid gd = mainwindow.btn_sideMode.Parent as Grid;
            gd.ColumnDefinitions[1].Style = mainwindow.Resources["zero"] as Style;

            TriggerAction ta = mainwindow.Resources["hide_menu"] as TriggerAction;
            mainwindow.bsb_menu_hide.Actions.Add(ta);

            mainwindow.btn_deck.IsEnabled = true;
            mainwindow.btn_start.IsEnabled = true;

            slideUp();



            //sendMsg("MSG=", "SIDE更换结束");
        }

        #endregion

        #region <-- side更改撤销 -->

        internal void sideModeCancel()
        {
            mainwindow.cv_main.Children.Clear();
            mainwindow.cv_extra.Children.Clear();
            mainwindow.cv_side.Children.Clear();
            temp_deck = new Deck();
            temp_deck.Main.AddRange(myself.deck.Main);
            temp_deck.Extra.AddRange(myself.deck.Extra);
            temp_deck.Side.AddRange(myself.deck.Side);

            for (int i = 0; i < temp_deck.Main.Count; i++)
            {
                CardUI card = temp_deck.Main[i];
                Base.getawayParerent(card);
                mainwindow.cv_main.Children.Add(card);
            }
            CardOperate.sort(mainwindow.cv_main, null);

            for (int i = 0; i < temp_deck.Extra.Count; i++)
            {
                CardUI card = temp_deck.Extra[i];
                Base.getawayParerent(card);
                mainwindow.cv_extra.Children.Add(card);
            }
            CardOperate.sort(mainwindow.cv_extra, null);


            for (int i = 0; i < temp_deck.Side.Count; i++)
            {
                CardUI card = temp_deck.Side[i];
                Base.getawayParerent(card);
                mainwindow.cv_side.Children.Add(card);
            }
            CardOperate.sort(mainwindow.cv_side, null);

            
        }

        #endregion

        #region

        private void slideUp()
        {
            MyStoryboard msb = UIAnimation.SPmove(mainwindow.sp_main, new Thickness(0), new Thickness(0, -340, 0, 0), 500);
            MyStoryboard msb2 = UIAnimation.SPmove(mainwindow.sp_extra, new Thickness(0), new Thickness(0, -430, 0, 0), 500);
            MyStoryboard msb3 = UIAnimation.SPmove(mainwindow.sp_side, new Thickness(0), new Thickness(0, -520, 0, 0), 500);
            MyStoryboard msb4 = UIAnimation.SPmove(mainwindow.tbc_DeckDocument, new Thickness(0), new Thickness(0, 102, 0, 0), 500);
            MyStoryboard msb5 = UIAnimation.showDecksManerger(mainwindow.gd_decksManerger, 1, 0, 300);
            msb5.Completed += (object c, EventArgs d) =>
            {
                mainwindow.gd_decksManerger.Visibility = System.Windows.Visibility.Hidden;
                mainwindow.cv_main.Children.Clear();
                mainwindow.cv_extra.Children.Clear();
                mainwindow.cv_side.Children.Clear();
                //DuelStart();
            };

           
            msb5.Begin();
            msb.Begin();
            msb2.Begin();
            msb3.Begin();
            msb4.Begin();
        }

        #endregion


        #region <-- 掷骰子 -->

        public void roll()
        {
            Random roll = new Random();
            int result = roll.Next(1, 6);
            var p = new Paragraph(); // Paragraph 类似于 html 的 P 标签  
            var r = new Run("Roll -> " + result); // Run 是一个 Inline 的标签  
            p.Inlines.Add(r);
            p.Foreground = Brushes.Blue;//设置字体颜色  
            mainwindow.tb_chat_view.Document.Blocks.Add(p);
            //mainwindow.tb_chat_view.AppendText("我：" + mainwindow.tb_chat_send.Text);
            mainwindow.tb_chat_view.ScrollToEnd();
            //DuelOperate.getInstance().sendMsg("Chat=" + "Roll -> " + result, "");
            Thread.Sleep(10);
            //mainwindow.tb_chat_view.AppendText("[己方掷骰子 -> "+ result + " ]" );
        }

        #endregion

        #region <-- 抛硬币 -->

        internal void coin()
        {
            Random roll = new Random();
            int result = roll.Next(1, 3);
            string result_ = result == 1 ? "Coin -> 正面" : "Coin -> 反面";
            var p = new Paragraph(); // Paragraph 类似于 html 的 P 标签             
            var r = new Run(result_); // Run 是一个 Inline 的标签  
            p.Inlines.Add(r);
            p.Foreground = Brushes.Blue;//设置字体颜色  
            mainwindow.tb_chat_view.Document.Blocks.Add(p);
            //mainwindow.tb_chat_view.AppendText("我：" + mainwindow.tb_chat_send.Text);
            mainwindow.tb_chat_view.ScrollToEnd();
            //DuelOperate.getInstance().sendMsg("Chat=" + result_, "");
            Thread.Sleep(10);
        }

        #endregion
    }
}
