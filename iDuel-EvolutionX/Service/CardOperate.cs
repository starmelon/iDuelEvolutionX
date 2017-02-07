using iDuel_EvolutionX.ADO;
using iDuel_EvolutionX.EventJson;
using iDuel_EvolutionX.Model;
using iDuel_EvolutionX.UI;
using NBX3.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace iDuel_EvolutionX.Service
{
    /// <summary>
    /// 这里主要是关于卡片的非菜单命令操作
    /// </summary>
    class CardOperate
    {
        public static MainWindow mainwindow; //主窗口的引用
        public static List<FrameworkElement> cv_monsters_1 =  new List<FrameworkElement>();  //己方怪物区Canvas的控件引用
        public static List<FrameworkElement> cv_magictraps_1 = new List<FrameworkElement>(); //己方魔陷区Canvas的控件引用
        public static List<FrameworkElement> cv_others_1 = new List<FrameworkElement>();     //己方除外，额外，卡组，P卡位，场地Canvas的控件引用
        public static List<FrameworkElement> cv_monsters_2 = new List<FrameworkElement>();   //对方怪物区Canvas的控件引用
        public static List<FrameworkElement> cv_magictraps_2 = new List<FrameworkElement>(); //对方魔陷区Canvas的控件引用
        public static List<FrameworkElement> cv_others_2 = new List<FrameworkElement>();     //对方除外，额外，卡组，P卡位，场地Canvas的控件引用
        public static CardView cardview; //
        private static string mb_right;  //用于保存双击的按键源
        /// <summary>
        /// 用于显示鼠标跟随效果的装饰器
        /// </summary>
        private static DragAdorner _adorner;
        /// <summary>
        /// 用于呈现DragAdorner的图画
        /// </summary>
        private static AdornerLayer _layer;

      

        #region 读取卡组

        public static bool readDeckBynet(List<string> main_,List<string> extra_,Deck deck)
        {
            deck.Main.Clear();
            deck.Extra.Clear();

            //deck.Main = CardDataOperate.getCardsByCheatcode(main_);
            deck.Main = CardDataOperate.getCardsWithInfoByCheatcode(main_);
            deck.Extra = CardDataOperate.getCardsWithInfoByCheatcode(extra_);

            //main.Clear();
            //extra.Clear();

            //for (int i = 0;i < main_.Count; i++)
            //{
            //    Card temp = new Card(
            //                card_all.Find(cc =>
            //                {
            //                    //cc为card_all中的对象，此处是lamda表达式写的委托
            //                    return cc.cheatcode.Equals(main_[i]);
            //                }));
            //    if (temp != null)
            //    {
            //        main.Add(temp);
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}

            //for (int i = 0; i < extra_.Count; i++)
            //{
            //    Card temp = new Card(
            //                card_all.Find(cc =>
            //                {
            //                    //cc为card_all中的对象，此处是lamda表达式写的委托
            //                    return cc.cheatcode.Equals(extra_[i]);
            //                }));
            //    if (temp != null)
            //    {
            //        extra.Add(temp);
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}

            return true;
        }

        

        

        #endregion

        #region <-- 卡片信息显示 -->

        /// <summary>
        /// 卡图显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Card_showpic(object sender, MouseEventArgs e)
        {
            CardUI card = sender as CardUI;
            Canvas cv = card.Parent as Canvas;
            

            //if ( (cv_monsters_2.Contains(cv) || cv_magictraps_2.Contains(cv)) && card.isBack)
            //{
            //    return;
            //}
            //else if (cv.Equals(mainwindow.card_2_Deck) || cv.Equals(mainwindow.card_2_Extra) || cv.Equals(mainwindow.card_2_hand))
            //{
            //    return;
            //}
            //else if ( ( cv.Equals( mainwindow.card_1_Deck ) || cv.Equals( mainwindow.card_1_Extra )) && card.isBack )
            //{
            //    return;
            //}

            //显示卡片
            mainwindow.card_picture.Source = card.originalImage ;

            //显示卡片信息
            mainwindow.card_effect.Clear();
            mainwindow.card_effect.Text = "卡名："+card.info.name + Environment.NewLine;
            mainwindow.card_effect.Text += "密码：" + card.info.cheatcode + Environment.NewLine;
            mainwindow.card_effect.Text += "----------------------------"+Environment.NewLine;
            mainwindow.card_effect.Text += "效果：" + card.info.effect;

            //显示调整
            mainwindow.card_adjust.Clear();
            mainwindow.card_adjust.Text = card.info.adjust;
           
            //Console.WriteLine((sender as Card).cardName);
        }

        #endregion

        #region <-- 获取右键菜单时卡片对象引用 -->

        /// <summary>
        /// 获取卡片对象引用
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static UIElement getCard(object sender)
        {
            /* 右键菜单中无法直接获得菜单源,即卡片 */
            return ContextMenuService.GetPlacementTarget(
            LogicalTreeHelper.GetParent(sender as MenuItem));

        }

        public static CardUI getCardFromMenuItem(MenuItem mi)
        {    
            CardUI card = null;
            DependencyObject uie = mi;
            
            while (card == null)
            {
                uie = LogicalTreeHelper.GetParent(uie);
                card = ContextMenuService.GetPlacementTarget(uie) as CardUI ;
            }
            return card;
        }

        #endregion

        #region <-- 卡片的双击操作 -->

        /// <summary>
        /// 卡片的双击操作
        /// </summary>
        /// <param name="card"></param>
        public static void Card_DoubleClick(CardUI card,MouseButtonEventArgs e)
        {

            MyCanvas mcv = card.Parent as MyCanvas;

            StatusChangeInfo statuschangeInfo = new StatusChangeInfo();
            int cardid = CardOperate.getCardID(card);
            statuschangeInfo.cardID = cardid;

            switch (mcv.area)
            {

                case Area.MONSTER_1:
                case Area.MONSTER_2:
                case Area.MONSTER_3:
                case Area.MONSTER_4:
                case Area.MONSTER_5:
                    {
                        #region 指令发送

                        //StatusChangeInfo statuschangeInfo = new StatusChangeInfo();
                        //int cardid = CardOperate.getCardID(card);
                        //statuschangeInfo.cardID = cardid;

                        switch (card.Status)
                        {
                            case Status.FRONT_ATK:
                                CardAnimation.Rotate2FrontDef(card);
                                statuschangeInfo.aimStatus = Status.FRONT_DEF;
                                break;
                            case Status.FRONT_DEF:

                                CardAnimation.Rotate2FrontAtk(card);
                                statuschangeInfo.aimStatus = Status.FRONT_ATK;
                                break;
                            case Status.BACK_ATK:
                                CardAnimation.Rotate2BackDef(card);
                                statuschangeInfo.aimStatus = Status.BACK_DEF;
                                break;
                            case Status.BACK_DEF:
                                card.set2FrontAtk2();
                                CardAnimation.rotate_turn(card);
                                Service.CardOperate.sort_XYZ_atk(mcv);
                                statuschangeInfo.aimStatus = Status.FRONT_ATK;
                                break;
                            default:
                                break;
                        }




                        String contentJson = JsonConvert.SerializeObject(statuschangeInfo);

                        BaseJson bj = new BaseJson();
                        bj.guid = DuelOperate.getInstance().myself.userindex;
                        bj.cid = "";
                        bj.action = ActionCommand.CARD_STATUS_CHANGE;
                        bj.json = contentJson;
                        String json = JsonConvert.SerializeObject(bj);
                        DuelOperate.getInstance().sendMsg(json);

                        #endregion
                    }
                    break;
                case Area.MAGICTRAP_1:
                case Area.MAGICTRAP_2:
                case Area.MAGICTRAP_3:
                case Area.MAGICTRAP_4:
                case Area.MAGICTRAP_5:
                    {
                        switch (card.Status)
                        {
                            case Status.FRONT_ATK:
                                card.set2BackAtk2();
                                CardAnimation.turn(card);
                                statuschangeInfo.aimStatus = Status.BACK_ATK;
                                break;
                            case Status.BACK_ATK:
                                card.set2FrontAtk2();
                                CardAnimation.turn(card);
                                statuschangeInfo.aimStatus = Status.FRONT_ATK;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }

            
            //cardview = CardView.getInstance(mainwindow); ;
            //Canvas cv = card.Parent as Canvas;



            //   #region <-- 检索卡片回手 -->

            //   if (cv.Equals(cardview.card_1_Deck))
            //{
            //       cardview.card_1_Deck.Children.Remove(card);
            //       sort_CardView(cardview.card_1_Deck, 10);
            //       mainwindow.card_1_hand.Children.Add(card);
            //       card_FrontAtk(card);
            //       card.ContextMenu = AllMenu.Instance.cm_hand;
            //       sort_HandCard(mainwindow.card_1_hand);
            //       string report = ("[" + card.info.name + "] From " + "[" + DuelReportOperate.from(cv.Name) + "] To [<手卡>]");              
            //       //DuelOperate.getInstance().sendMsg("Back2Hand=" + card.duelindex, report);

            //} 

            //   #endregion

            //#region <-- 查看卡片素材 -->

            //if ( cv_monsters_1.Contains(cv) || cv_magictraps_2.Contains(cv) )
            //{
            //    if (mb_right.Equals("Pressed")) return;
            //    if (cv.Children.Count < 2) return;
            //    if (cv.Children.IndexOf(card) == cv.Children.Count - 1)
            //    {
            //        int n = cv.Children.Count;
            //        //if (card.scCardType.Equals("XYZ怪兽"))
            //        //{
            //        //    //Canvas cv = card.Parent as Canvas;
            //        //    n = n - 1;
            //        //}

            //        XYZmaterialView xyz = XYZmaterialView.getInstance(mainwindow, cv);

            //        for (int i = 0; i < n - 1; i++)
            //        {
            //            Card temp = cv.Children[0] as Card;
            //            cv.Children.RemoveAt(0);
            //            xyz.materials.Children.Insert(0, temp);
            //        }
            //        sort_CardView(xyz.materials, 7);
            //        sort_SingleCard(card);
            //        xyz.Show();
            //        xyz.Activate();


            //    }
            //}


            //#endregion

        }

        #endregion

        #region <-- 指示物的添加&减少 -->

        /// <summary>
        /// 指示物的双击操作
        /// </summary>
        /// <param name="card"></param>
        /// <param name="e"></param>
        public static void Sign_DoubleClick(TextBlock tb, string ChangedButton)
        {
            //FrameworkElement fe = tb.Parent as FrameworkElement;
            //FrameworkElement fe_par = fe.Parent as FrameworkElement;
            //Canvas cv = mainwindow.FindName(fe_par.Name.Replace("sp_sign", "card")) as Canvas;
            //Card card = cv.Children[0] as Card;
            //if (card != null)
            //{
            //    if (ChangedButton.Equals("Left"))
            //    {
            //        tb.Text = (Convert.ToInt32(tb.Text) + 1).ToString();
            //        string report = "[" + card.name + "] -> ";
            //        if (fe.Name.Equals("Black")) report += "[黑色指示物]+1";
            //        if (fe.Name.Equals("Blue")) report += "[蓝色指示物]+1";
            //        if (fe.Name.Equals("Red")) report += "[蓝色指示物]+1";
            //        report += " -> 现为 [" + tb.Text + "]";
            //        DuelOperate.getInstance().sendMsg("ChangeSign=" + fe_par.Name + "," + fe.Name + ",+", report);
                    

            //    }
            //    if (ChangedButton.Equals("Right"))
            //    {
                    
            //        tb.Text = (Convert.ToInt32(tb.Text) - 1).ToString();
            //        string report = "[" + card.name + "] -> ";
            //        if (fe.Name.Equals("Black")) report += "[黑色指示物]-1";
            //        if (fe.Name.Equals("Blue")) report += "[蓝色指示物]-1";
            //        if (fe.Name.Equals("Red")) report += "[蓝色指示物]-1";
            //        report += " -> 现为 [" + tb.Text + "]";
            //        DuelOperate.getInstance().sendMsg("ChangeSign=" + fe_par.Name + "," + fe.Name + ",-", report);
            //        if (Convert.ToInt32(tb.Text) == 0)
            //        {
            //            Grid gd = tb.Parent as Grid;
            //            StackPanel sp = gd.Parent as StackPanel;
            //            sp.Children.Remove(gd);
            //        }
            //    }
            //}
            
            
        }

        #endregion

        #region <-- 拖放操作 -->

        

        #region <-- 拖拽开始 -->

        /// <summary>
        /// 拖拽开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void CardDragStart(object sender, MouseEventArgs e)
        {
            CardUI card = sender as CardUI;
            if (e.LeftButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
            {
                CardUI card_ = card as CardUI;
                Canvas cv = card_.Parent as Canvas;
                //if (e.RightButton == MouseButtonState.Pressed && Base.getParerent(card_).Name.Equals("card_1_Deck"))
                //{
                //    return;
                //}
                mb_right = e.RightButton.ToString();

                DragEventHandler draghandler = new DragEventHandler(DragScope_PreviewDragOver);

                if (cv.Equals(mainwindow.cv_main) || cv.Equals(mainwindow.cv_extra) || cv.Equals(mainwindow.cv_side))
                {
                    mainwindow.gd_decksManerger.PreviewDragOver += draghandler;

                    if (cv != null && cv.Equals(mainwindow.card_1_hand) && e.RightButton == MouseButtonState.Pressed)
                    {
                        _adorner = new DragAdorner(mainwindow.battle, (UIElement)card_, 0.8,Status.BACK_DEF);
                    }
                    else
                    {
                        _adorner = new DragAdorner(mainwindow.battle, (UIElement)card_, 0.8, card_.Status);

                    }

                    _layer = AdornerLayer.GetAdornerLayer(mainwindow.battle as Visual);
                    _layer.Add(_adorner);

                    DataObject data = new DataObject(typeof(BitmapImage), card);
                    DragDrop.DoDragDrop(card, data, DragDropEffects.Move);

                    AdornerLayer.GetAdornerLayer(mainwindow.battle).Remove(_adorner);
                    _adorner = null;

                    mainwindow.battle.PreviewDragOver -= draghandler;

                }
                else
                {
                    mainwindow.battle.PreviewDragOver += draghandler;
                    if (cv != null && cv.Equals(mainwindow.card_1_hand) && e.RightButton == MouseButtonState.Pressed)
                    {
                        _adorner = new DragAdorner(mainwindow.gd_decksManerger, (UIElement)card_, 0.8,Status.BACK_ATK);
                    }
                    else
                    {
                        _adorner = new DragAdorner(mainwindow.gd_decksManerger, (UIElement)card_, 0.8, card_.Status);

                    }           
                    _layer = AdornerLayer.GetAdornerLayer(mainwindow.gd_decksManerger as Visual);
                    _layer.Add(_adorner);

                    DataObject data = new DataObject(typeof(BitmapImage), card);
                    DragDrop.DoDragDrop(card, data, DragDropEffects.Move);

                    AdornerLayer.GetAdornerLayer(mainwindow.gd_decksManerger).Remove(_adorner);
                    _adorner = null;

                    mainwindow.gd_decksManerger.PreviewDragOver -= draghandler;
                }

            }
            
        }

        #endregion

        public static void DragScope_PreviewDragOver(object sender, DragEventArgs args)
        {
            if (_adorner != null)
            {
                _adorner.LeftOffset = args.GetPosition(mainwindow.battle).X;
                _adorner.TopOffset = args.GetPosition(mainwindow.battle).Y;
            }
        }

        #region <-- 卡组管理器中的释放 -->

        internal static void sideMode(object sender, DragEventArgs e)
        {
            IDataObject data = e.Data;

            if (data.GetDataPresent(typeof(CardUI)))
            {
                //获得卡片对象
                CardUI card = data.GetData(typeof(CardUI)) as CardUI;
                //MessageBox.Show(rect.Parent.GetType().ToString().Contains("Canvas"));

                //判断卡片原有位置的父容器类型
                Canvas cv = card.Parent as Canvas;

                //获取放置容器
                Canvas cv_aim = sender as Canvas;

                //判断目标位置是否是原位置
                if (cv.Name.Equals(cv_aim.Name)) return;



                if (card.info.CardDType.Contains("同调") || card.info.sCardType.Contains("XYZ") || card.info.sCardType.Contains("融合"))
                {
                    if (cv_aim.Equals(mainwindow.cv_main)) return;
                }
                else
                {
                    if (cv_aim.Equals(mainwindow.cv_extra)) return;
                }
                
                Base.getawayParerent(card);
                if (cv.Equals(mainwindow.cv_main))
                {
                    DuelOperate.getInstance().temp_deck.Main.Remove(card);
                }
                else if (cv.Equals(mainwindow.cv_extra)) DuelOperate.getInstance().temp_deck.Extra.Remove(card);
                else if (cv.Equals(mainwindow.cv_side)) DuelOperate.getInstance().temp_deck.Side.Remove(card);
                
                cv_aim.Children.Add(card);
                if (cv_aim.Equals(mainwindow.cv_main)) DuelOperate.getInstance().temp_deck.Main.Add(card);
                else if (cv_aim.Equals(mainwindow.cv_extra)) DuelOperate.getInstance().temp_deck.Extra.Add(card);
                else if (cv_aim.Equals(mainwindow.cv_side)) DuelOperate.getInstance().temp_deck.Side.Add(card);


                CardOperate.sort(cv, null);
                CardOperate.sort(cv_aim, null);
            }
        }

        #endregion

        /// <summary>
        /// 通用释放
        /// </summary>
        public static void card_drop(object sender, DragEventArgs e)
        {
            //获取释放数据
            IDataObject data = e.Data;

            //判断是否为卡片类型
            if (!data.GetDataPresent(typeof(BitmapImage)))
            {
                return;
            }

            //获得卡片对象
            CardUI card = data.GetData(typeof(BitmapImage)) as CardUI;



            //脱离父控件
            card.getAwayFromParents();



        }

        #region <-- 魔陷区释放 -->

        /// <summary>
        /// 魔法陷阱区释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop_Magic(object sender, DragEventArgs e)
        {
            cardview = CardView.getInstance(mainwindow);

            IDataObject data = e.Data;

            if (data.GetDataPresent(typeof(BitmapImage)))
            {
                //获得卡片对象
                CardUI card = data.GetData(typeof(BitmapImage)) as CardUI;
                //MessageBox.Show(rect.Parent.GetType().ToString().Contains("Canvas"));

                //判断卡片原有位置的父容器类型
                MyCanvas cv = card.Parent as MyCanvas;

                //获取放置容器
                MyCanvas cv_aim = sender as MyCanvas;

                //如果源目标是怪物区，则应去除指示物
                switch (cv.Name)
                {
                    case "card_1_6":
                    case "card_1_7": 
                    case "card_1_8": 
                    case "card_1_9": 
                    case "card_1_10":
                        card.clearSigns();
                        break;
                    default:
                        break;
                }

                //判断目标位置是否是原位置
                if (cv.Name.Equals(cv_aim.Name)) return;

                //只允许存在一张卡
                if (cv_aim.Children.Count > 0) return;

                //脱离父控件
                card.getAwayFromParents();

                if (mb_right.Equals("Pressed"))
                {
                    card.Status = Status.BACK_ATK;
                    cv_aim.Children.Add(card);

                    #region 指令发送

                    MoveInfo moveInfo = new MoveInfo();
                    int cardid = getCardID(card);
                    moveInfo.cardID = cardid;
                    moveInfo.isAdd = true;
                    moveInfo.aimArea = cv_aim.area;
                    moveInfo.aimStatus = Status.BACK_ATK;
                    String contentJson = JsonConvert.SerializeObject(moveInfo);

                    BaseJson bj = new BaseJson();
                    bj.guid = DuelOperate.getInstance().myself.userindex;
                    bj.cid = "";
                    bj.action = ActionCommand.CARD_MOVE;
                    bj.json = contentJson;
                    String json = JsonConvert.SerializeObject(bj);
                    DuelOperate.getInstance().sendMsg(json);

                    #endregion
                }
                else
                {
                    cv_aim.Children.Add(card);

                    #region 指令发送

                    MoveInfo moveInfo = new MoveInfo();
                    int cardid = getCardID(card);
                    moveInfo.cardID = cardid;
                    moveInfo.isAdd = true;
                    moveInfo.aimArea = cv_aim.area;
                    switch (cv.area)
                    {
                        case Area.MONSTER_1:
                        case Area.MONSTER_2:
                        case Area.MONSTER_3:
                        case Area.MONSTER_4:
                        case Area.MONSTER_5:
                            switch (card.Status)
                            {
                                case Status.FRONT_ATK:
                                case Status.FRONT_DEF:
                                    moveInfo.aimStatus = Status.FRONT_ATK;
                                    break;
                                case Status.BACK_ATK:
                                case Status.BACK_DEF:
                                    moveInfo.aimStatus = Status.BACK_ATK;
                                    break;
                            }
                            
                            break;
                        case Area.MAGICTRAP_1:
                        case Area.MAGICTRAP_2:
                        case Area.MAGICTRAP_3:
                        case Area.MAGICTRAP_4:
                        case Area.MAGICTRAP_5:
                            moveInfo.aimStatus = card.Status;
                            break;
                        default:
                            moveInfo.aimStatus = Status.FRONT_ATK;
                            break;
                    }
                    
                    String contentJson = JsonConvert.SerializeObject(moveInfo);

                    BaseJson bj = new BaseJson();
                    bj.guid = DuelOperate.getInstance().myself.userindex;
                    bj.cid = "";
                    bj.action = ActionCommand.CARD_MOVE;
                    bj.json = contentJson;
                    String json = JsonConvert.SerializeObject(bj);
                    DuelOperate.getInstance().sendMsg(json);

                    #endregion
                }

                


            }
        }

        #endregion

        #region <-- 怪物区释放 -->

        /// <summary>
        /// 怪物区释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop_Monster(object sender, DragEventArgs e)
        {
            cardview = CardView.getInstance(mainwindow);

            //判断放置数据的有效性
            IDataObject data = e.Data;
            if (!data.GetDataPresent(typeof(BitmapImage)))
            {
                return;
            }
           
            //获得卡片对象
            CardUI card = data.GetData(typeof(BitmapImage)) as CardUI;

            //获取父控件
            MyCanvas cv = card.Parent as MyCanvas;

            //获取目标控件
            MyCanvas cv_aim = sender as MyCanvas;

            //判断目标位置是否是原位置
            if (cv.Equals(cv_aim)) return;

            //if (cv.Equals(mainwindow.card_1_Extra) && !card.info.effect.Contains("灵摆效果"))
            //{
            //    return;
            //}           
                       
            if (cv_magictraps_1.Contains(cv) && !mb_right.Equals("Pressed") && e.KeyStates == DragDropKeyStates.AltKey)
            {
                   

                MyStoryboard msb = CardAnimation.EffectOrigin();
                msb.Begin((mainwindow.FindName(cv.Name.Replace("card", "bd")) as FrameworkElement));
                MyStoryboard msb2 = CardAnimation.EffectAim();
                msb2.Begin((mainwindow.FindName(cv_aim.Name.Replace("card", "bd")) as FrameworkElement));
                return;
            }

            //如果源目标是魔法陷阱区，则应该先清除指示物
            switch (cv.Name)
            {
                case "card_1_1":
                case "card_1_2":
                case "card_1_3":
                case "card_1_4":
                case "card_1_5":
                    card.clearSigns();
                    break;
                default:
                    break;
            }

               
            card.ContextMenu = AllMenu.Instance.cm_monster;

            #region 目标卡区存在卡

            if (cv_aim.Children.Count > 0)
            {
                card.getAwayFromParents();
                if (cv.Equals(mainwindow.card_1_hand))
                {
                    CardOperate.sort(cv, null);
                }

                if (card.info.sCardType.Equals("XYZ怪兽"))
                {
                    Drop2MonsterWin oi = new Drop2MonsterWin();
                    oi.Owner = mainwindow;
                    oi.sendResult += new Drop2MonsterDelegate(result => {


                        switch (result)
                        {
                            case Drop2MonsterWinResult.INSERT:
                                {
                                    cv_aim.Children.Insert(0, card);

                                    #region 指令发送

                                    MoveInfo moveInfo = new MoveInfo();
                                    int cardid = getCardID(card);
                                    moveInfo.cardID = cardid;
                                    moveInfo.isAdd = false;
                                    moveInfo.aimArea = cv_aim.area;
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
                                }


                                break;
                            case Drop2MonsterWinResult.OVERLAY:
                                {
                                    cv_aim.Children.Add(card);

                                    if (cv.area == Area.EXTRA)
                                    {
                                        CardAnimation.overlaySummon(cv_aim);
                                    }

                                    //#region 召唤动画

                                    //Point summon2 = cv_aim.TranslatePoint(new Point(0.5, 0.5), mainwindow.Battle);
                                    //Canvas.SetLeft(mainwindow.img_overlay, summon2.X - ((mainwindow.img_overlay.Width - cv_aim.ActualWidth) / 2));
                                    //Canvas.SetTop(mainwindow.img_overlay, summon2.Y - ((mainwindow.img_overlay.Height - cv_aim.ActualHeight) / 2));
                                    //CardAnimation.Rotate_Scale_FadeInAndOut(mainwindow.img_overlay);

                                    //#endregion

                                    #region 指令发送

                                    MoveInfo moveInfo = new MoveInfo();
                                    int cardid = getCardID(card);
                                    moveInfo.cardID = cardid;
                                    moveInfo.isAdd = true;
                                    moveInfo.aimArea = cv_aim.area;
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
                                }
                                break;
                            default:
                                break;
                        }
                    });

                    //Point p = new Point();
                    Point p = cv_aim.PointToScreen(new Point(0, 0));
                    oi.Top = p.Y - oi.Height;
                    oi.Left = p.X - ((oi.Width - cv_aim.ActualWidth) / 2);
                    oi.ShowDialog();
                }
                else
                {
                    cv_aim.Children.Insert(0, card);

                    #region 指令发送

                    MoveInfo moveInfo = new MoveInfo();
                    int cardid = getCardID(card);
                    moveInfo.cardID = cardid;
                    moveInfo.isAdd = false;
                    moveInfo.aimArea = cv_aim.area;
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
                }


                    
            }

            #endregion

            #region 目标卡区为空时


            if (cv_aim.Children.Count == 0)
            {

                    
                switch (cv.area)
                {

                    case Area.GRAVEYARD:
                    case Area.MAINDECK:
                    case Area.BANISH:
                    case Area.HAND:
                        {
                            card.getAwayFromParents();
                            CardOperate.sort(cv, null);

                            #region 右键盖放

                            if (mb_right.Equals("Pressed"))
                            {
                                card.set2BackDef();
                                    
                                cv_aim.Children.Add(card);

                                #region 指令发送
                                {
                                    MoveInfo moveInfo1 = new MoveInfo();
                                    moveInfo1.cardID = getCardID(card);
                                    moveInfo1.isAdd = true;
                                    moveInfo1.aimArea = cv_aim.area;
                                    moveInfo1.aimStatus = Status.BACK_DEF;
                                    String contentJson1 = JsonConvert.SerializeObject(moveInfo1);

                                    BaseJson bj1 = new BaseJson();
                                    bj1.guid = DuelOperate.getInstance().myself.userindex;
                                    bj1.cid = "";
                                    bj1.action = ActionCommand.CARD_MOVE;
                                    bj1.json = contentJson1;
                                    String json1 = JsonConvert.SerializeObject(bj1);
                                    DuelOperate.getInstance().sendMsg(json1);
                                }
                                #endregion

                                return;
                            }

                            #endregion
                            cv_aim.Children.Add(card);
                            CardAnimation.commonSummon(cv_aim);
                        }
                        break;
                    case Area.EXTRA:
                        card.getAwayFromParents();
                        cv_aim.Children.Add(card);
                        switch (card.info.sCardType)
                        {
                            case "同调怪兽":
                                {
                                    #region 召唤动画

                                    CardAnimation.synchroSummon(cv_aim);

                                    #endregion
                                }
                                break;
                            default:
                                {
                                    #region 召唤动画

                                    CardAnimation.commonSummon(cv_aim);

                                    #endregion
                                }

                                break;
                        }
                        break;
                    case Area.MONSTER_1:
                    case Area.MONSTER_2:
                    case Area.MONSTER_3:
                    case Area.MONSTER_4:
                    case Area.MONSTER_5:
                        cv.WhenRemoveChildren -= CardAreaEvent.removeFromMonster;
                        card.getAwayFromParents();
                        while (cv.Children.Count > 0)
                        {
                            CardUI c = cv.Children[0] as CardUI;
                            c.getAwayFromParents();
                            cv_aim.Children.Add(c);

                            //#region 指令发送

                            //MoveInfo moveInfo1 = new MoveInfo();
                            //moveInfo1.cardID =  getCardID(c);
                            //moveInfo1.isAdd = true;
                            //moveInfo1.aimArea = cv_aim.area;
                            //moveInfo1.aimStatus = card.Status;
                            //String contentJson1 = JsonConvert.SerializeObject(moveInfo1);

                            //BaseJson bj1 = new BaseJson();
                            //bj1.guid = DuelOperate.getInstance().myself.userindex;
                            //bj1.cid = "";
                            //bj1.action = ActionCommand.CARD_MOVE;
                            //bj1.json = contentJson1;
                            //String json1 = JsonConvert.SerializeObject(bj1);
                            //DuelOperate.getInstance().sendMsg(json1);

                            //#endregion
                        }
                        cv.WhenRemoveChildren += CardAreaEvent.removeFromMonster;
                        cv_aim.Children.Add(card);

                        #region 消除攻守显示

                        Binding bind = new Binding();
                        BindingOperations.ClearBinding(cv.tb_atkDef, TextBlock.TextProperty);
                        cv.tb_atkDef.IsHitTestVisible = false;

                        #endregion

                        //CardAreaEvent.showSigns(cv_aim, card);

                        break;
                    case Area.MAGICTRAP_1:
                    case Area.MAGICTRAP_2:
                    case Area.MAGICTRAP_3:
                    case Area.MAGICTRAP_4:
                    case Area.MAGICTRAP_5:
                        card.getAwayFromParents();
                        cv_aim.Children.Add(card);

                        break;
                    default:
                        //cv_aim.Children.Add(card);
                        break;
                }
                                 

                #region 指令发送

                MoveInfo moveInfo = new MoveInfo();
                int cardid = getCardID(card);
                moveInfo.cardID = cardid;
                moveInfo.isAdd = true;
                moveInfo.aimArea = cv_aim.area;
                moveInfo.aimStatus = card.Status;
                String contentJson = JsonConvert.SerializeObject(moveInfo);

                BaseJson bj = new BaseJson();
                bj.guid = DuelOperate.getInstance().myself.userindex;
                bj.cid = "";
                bj.action = ActionCommand.CARD_MOVE;
                bj.json = contentJson;
                String json = JsonConvert.SerializeObject(bj);
                DuelOperate.getInstance().sendMsg(json);

                #endregion

                return;
            }

            #endregion


            
        }

        public static int getCardID(CardUI card)
        {
            int cardid = DuelOperate.getInstance().myself.deck.Main.IndexOf(card);
            if (cardid == -1)
            {
                cardid = -DuelOperate.getInstance().myself.deck.Extra.IndexOf(card) - 1;
            }

            return cardid;
        }

        public static int getCardIDOP(CardUI card)
        {
            int cardid = DuelOperate.getInstance().opponent.deck.Main.IndexOf(card);
            if (cardid == -1)
            {
                cardid = -DuelOperate.getInstance().opponent.deck.Extra.IndexOf(card) - 1;
            }

            return cardid;
        }

        #endregion

        #region <-- 卡组区释放 -->

        /// <summary>
        /// 卡组区释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop_Main(object sender, DragEventArgs e)
        {
            cardview = CardView.getInstance(mainwindow);

            IDataObject data = e.Data;

            if (data.GetDataPresent(typeof(BitmapImage)))
            {
                //获得卡片对象
                CardUI card = data.GetData(typeof(BitmapImage)) as CardUI;

                //判断卡片原有位置的父容器类型
                MyCanvas cv = card.Parent as MyCanvas;

                //获取放置容器
                MyCanvas cv_aim = sender as MyCanvas;

                //判断目标位置是否是原位置
                if (cv.Name.Equals(cv_aim.Name)) return;

                //对出发地的判断处理
                if (cv.Name.Equals("card_1_Extra") && !card.info.CardDType.Equals("效果·摇摆"))
                {
                    return;
                }
                if (card.info.sCardType.Equals("XYZ怪兽") || card.info.sCardType.Equals("融合怪兽") || card.info.sCardType.Equals("同调怪兽") )
                {
                    return;
                }

                Drag2MainDeckWin oi = new Drag2MainDeckWin();
                oi.Owner = mainwindow;
                oi.sendResult += new Drop2MainDeckDelegate(result => {

                    card.getAwayFromParents();
                    card.set2BackAtk();

                    #region 指令发送

                    MoveInfo moveInfo = new MoveInfo();
                    int cardid = getCardID(card);
                    moveInfo.cardID = cardid;

                    switch (result)
                    {
                        case Drop2MainDeckResult.TOP:
                            cv_aim.Children.Add(card);       
                            moveInfo.isAdd = true;        
                            break;
                        case Drop2MainDeckResult.MIDDLE:
                            break;
                        case Drop2MainDeckResult.BOTTOM:
                            cv_aim.Children.Insert(0, card);
                            moveInfo.isAdd = false;
                            break;
                        default:
                            break;
                    }

                    moveInfo.aimArea = cv_aim.area;
                    moveInfo.aimStatus = Status.BACK_ATK;
                    String contentJson = JsonConvert.SerializeObject(moveInfo);

                    BaseJson bj = new BaseJson();
                    bj.guid = DuelOperate.getInstance().myself.userindex;
                    bj.cid = "";
                    bj.action = ActionCommand.CARD_MOVE;
                    bj.json = contentJson;
                    String json = JsonConvert.SerializeObject(bj);
                    DuelOperate.getInstance().sendMsg(json);

                    #endregion

                });

                //Point p = new Point();
                Point p = cv_aim.PointToScreen(new Point(0, 0));
                oi.Top = p.Y - oi.Height;
                oi.Left = p.X - ((oi.Width - cv_aim.ActualWidth) / 2);
                oi.ShowDialog();

               
     
            }
        }

        #endregion

        #region <-- 额外区释放 -->

        /// <summary>
        /// 额外区释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop_Extra(object sender, DragEventArgs e)
        {
            cardview = CardView.getInstance(mainwindow);

            IDataObject data = e.Data;

            if (data.GetDataPresent(typeof(BitmapImage)))
            {
                //获得卡片对象
                CardUI card = data.GetData(typeof(BitmapImage)) as CardUI;
                //MessageBox.Show(rect.Parent.GetType().ToString().Contains("Canvas"));

                //判断卡片原有位置的父容器类型
                MyCanvas cv = card.Parent as MyCanvas;

                //获取放置容器
                MyCanvas cv_aim = sender as MyCanvas;

                //判断目标位置是否是原位置
                if (cv.area == cv_aim.area) return;

                //判断卡片类型
                if (!card.info.sCardType.Equals("XYZ怪兽") && !card.info.sCardType.Equals("融合怪兽") && !card.info.sCardType.Equals("同调怪兽") && !card.info.CardDType.Contains("灵摆"))
                {
                    return;
                }

                card.getAwayFromParents();
                

                #region 指令发送

                MoveInfo moveInfo = new MoveInfo();
                int cardid = getCardID(card);
                moveInfo.cardID = cardid;
                
                moveInfo.aimArea = cv_aim.area;
                if (card.info.CardDType.Contains("灵摆"))
                {
                    cv_aim.Children.Add(card);
                    moveInfo.isAdd = true;
                    moveInfo.aimStatus = Status.FRONT_ATK;
                }
                else
                {
                    cv_aim.Children.Insert(0,card);
                    moveInfo.isAdd = false;
                    moveInfo.aimStatus = Status.BACK_ATK;
                }
                
                String contentJson = JsonConvert.SerializeObject(moveInfo);

                BaseJson bj = new BaseJson();
                bj.guid = DuelOperate.getInstance().myself.userindex;
                bj.cid = "";
                bj.action = ActionCommand.CARD_MOVE;
                bj.json = contentJson;
                String json = JsonConvert.SerializeObject(bj);
                DuelOperate.getInstance().sendMsg(json);

                #endregion

            }
        }

        #endregion

        #region <-- 手卡区释放 -->

        /// <summary>
        /// 手卡区释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop_Hand(object sender, DragEventArgs e)
        {
            cardview = CardView.getInstance(mainwindow);

            IDataObject data = e.Data;

            if (data.GetDataPresent(typeof(BitmapImage)))
            {
                //获得卡片对象
                CardUI card = data.GetData(typeof(BitmapImage)) as CardUI;
                //MessageBox.Show(rect.Parent.GetType().ToString().Contains("Canvas"));

                //判断卡片原有位置的父容器类型
                MyCanvas cv = card.Parent as MyCanvas;

                //获取放置容器
                MyCanvas cv_aim = sender as MyCanvas;

                //判断目标位置是否是原位置
                if (cv.Name.Equals(cv_aim.Name)) return;

                //对出发地的判断处理
                if ( card.info.sCardType.Equals("XYZ怪兽") || card.info.sCardType.Equals("融合怪兽") || card.info.sCardType.Equals("同调怪兽") )
                {
                    return;
                }


                //对目标地的处理
                //if (cv_aim.Children.Count > 0) return;
                #region 清除指示物

                StackPanel sp = mainwindow.FindName(cv.Name.Replace("card", "sp_sign")) as StackPanel;
                if( sp != null ) sp.Children.Clear();

                #endregion
                


                card.getAwayFromParents();
                card.set2FrontAtk();

                if (cv_aim.Children.Count > 0)
                {
                    CardUI card_last = cv_aim.Children[cv_aim.Children.Count - 1] as CardUI;
                    Canvas.SetLeft(card, Canvas.GetLeft(card_last));
                    Canvas.SetTop(card, Canvas.GetTop(card_last));
                }
                else
                {
                    Canvas.SetLeft(card, (cv_aim.ActualWidth - card.ActualWidth) / 2.0);
                    Canvas.SetTop(card, (cv_aim.ActualHeight - card.ActualHeight) / 2.0);
                }
                mainwindow.card_1_hand.Children.Add(card);

                if (cv == mainwindow.card_1_Deck)
                {
                    DrawInfo orderInfo = new DrawInfo();

                    int cardid = getCardID(card);
                    orderInfo.cardID = cardid;
                    String contentJson = JsonConvert.SerializeObject(orderInfo);

                    BaseJson bj = new BaseJson();
                    bj.guid = DuelOperate.getInstance().myself.userindex;
                    bj.cid = "";
                    bj.action = ActionCommand.GAME_DRAW;
                    bj.json = contentJson;
                    String json = JsonConvert.SerializeObject(bj);
                    DuelOperate.getInstance().sendMsg(json);
                }
                else
                {
                    #region 指令发送

                    MoveInfo moveInfo = new MoveInfo();
                    int cardid = getCardID(card);
                    moveInfo.cardID = cardid;
                    moveInfo.isAdd = true;
                    moveInfo.aimArea = cv_aim.area;
                    moveInfo.aimStatus = Status.BACK_ATK;
                    String contentJson = JsonConvert.SerializeObject(moveInfo);

                    BaseJson bj = new BaseJson();
                    bj.guid = DuelOperate.getInstance().myself.userindex;
                    bj.cid = "";
                    bj.action = ActionCommand.CARD_MOVE;
                    bj.json = contentJson;
                    String json = JsonConvert.SerializeObject(bj);
                    DuelOperate.getInstance().sendMsg(json);

                    #endregion
                }



                //sort_HandCard(cv_aim);
                //card.ContextMenu = AllMenu.cm_hand;


                //if (cv.Name.Equals("materials"))
                //{
                //    report = ("将" + DuelReportOperate.from(XYZmaterialView.xyz_cv.Name) + " [" + card.name + "] 取回 <手卡>" + Environment.NewLine);
                //    DuelOperate.getInstance().sendMsg("Back2Hand=" + card.duelindex + "," + cv_aim.Name, report);
                //    return;
                //}

                //if (cv.Equals(mainwindow.card_1_Deck))
                //{
                //    report = "抽牌" + Environment.NewLine;
                //    DuelOperate.getInstance().sendMsg("Draw=1,"+card.duelindex, report);
                //    return;
                //}

                //DuelOperate.getInstance().sendMsg("Back2Hand=" + card.duelindex + "," + cv_aim.Name, report);


            }
        }

        #endregion

        #region <-- P卡区释放 -->

        /// <summary>
        /// 左右摇摆区释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop_Pendulum(object sender, DragEventArgs e)
        {
            cardview = CardView.getInstance(mainwindow);

            IDataObject data = e.Data;

            if (data.GetDataPresent(typeof(BitmapImage)))
            {
                //获得卡片对象
                CardUI card = data.GetData(typeof(BitmapImage)) as CardUI;

                //判断卡片原有位置的父容器类型
                MyCanvas cv = card.Parent as MyCanvas;

                //获取放置容器
                MyCanvas cv_aim = sender as MyCanvas;

                //判断目标位置是否是原位置
                //if (cv.Name.Equals(cv_aim.Name)) return;


                //对目标地的处理
                if (cv_aim.Children.Count > 0) return;

                card.getAwayFromParents();
                card.set2FrontAtk();
                cv_aim.Children.Add(card);

                #region 指令发送

                MoveInfo moveInfo = new MoveInfo();
                int cardid = getCardID(card);
                moveInfo.cardID = cardid;
                moveInfo.isAdd = true;
                moveInfo.aimArea = cv_aim.area;
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
            }
        }

        #endregion

        #region <-- 除外区释放 -->

        /// <summary>
        /// 除外区释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop_Outside(object sender, DragEventArgs e)
        {
            cardview = CardView.getInstance(mainwindow);

            IDataObject data = e.Data;

            if (data.GetDataPresent(typeof(BitmapImage)))
            {
                //获得卡片对象
                CardUI card = data.GetData(typeof(BitmapImage)) as CardUI;
                //MessageBox.Show(rect.Parent.GetType().ToString().Contains("Canvas"));

                //判断卡片原有位置的父容器类型
                MyCanvas cv = card.Parent as MyCanvas;

                //获取放置容器
                MyCanvas cv_aim = sender as MyCanvas;

                //判断目标位置是否是原位置
                if (cv.Name.Equals(cv_aim.Name)) return;

                //对出发地的判断处理
                #region 清除指示物

                StackPanel sp = mainwindow.FindName(cv.Name.Replace("card", "sp_sign")) as StackPanel;
                if (sp != null) sp.Children.Clear();

                #endregion
                //对目标地的处理

                card.getAwayFromParents();
                if (mb_right.Equals("Pressed"))
                {
                    card.set2BackAtk();
                }
                else
                {
                    card.set2FrontAtk();
                }
                cv_aim.Children.Add(card);

                #region 指令发送

                DisappearInfo moveInfo = new DisappearInfo();
                int cardid = getCardID(card);
                moveInfo.cardID = cardid;
                moveInfo.aimStatus = card.Status;
                String contentJson = JsonConvert.SerializeObject(moveInfo);

                BaseJson bj = new BaseJson();
                bj.guid = DuelOperate.getInstance().myself.userindex;
                bj.cid = "";
                bj.action = ActionCommand.CARD_DISAPPEAR;
                bj.json = contentJson;
                String json = JsonConvert.SerializeObject(bj);
                DuelOperate.getInstance().sendMsg(json);

                #endregion
                //cv.Children.Remove(card);
                //if (!cv.Name.Equals("card_1_Graveyard") && !cv.Name.Equals("card_1_Outside") && !cv.Equals(mainwindow.card_1_Deck) && !cv.Equals(mainwindow.card_1_Left) && !cv.Equals(mainwindow.card_1_Right))
                //{
                //    CardOperate.sort(cv, card);
                //}

                //card_FrontAtk(card);
                //
                //card.ContextMenu = AllMenu.cm_outside;
                //CardOperate.sort_SingleCard(card);

                //string report = ("将 " + DuelReportOperate.from(cv.Name) + " [" + card.name + "] 从游戏中除外" + Environment.NewLine);
                //DuelOperate.getInstance().sendMsg("Disappear=" + card.duelindex + "," + cv_aim.Name, report);
            }
        }

        #endregion

        #region <-- 墓地释放 -->

        /// <summary>
        /// 墓地释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop_Graveyard(object sender, DragEventArgs e)
        {
            cardview = CardView.getInstance(mainwindow);

            IDataObject data = e.Data;

            if (data.GetDataPresent(typeof(BitmapImage)))
            {
                //获得卡片对象
                CardUI card = data.GetData(typeof(BitmapImage)) as CardUI;
                //MessageBox.Show(rect.Parent.GetType().ToString().Contains("Canvas"));

                //判断卡片原有位置的父容器类型
                MyCanvas cv = card.Parent as MyCanvas;

                //获取放置容器
                MyCanvas cv_aim = sender as MyCanvas;

                //判断目标位置是否是原位置
                if (cv.Name.Equals(cv_aim.Name)) return;

                //对出发地的判断处理
                #region 清除指示物

                StackPanel sp = mainwindow.FindName(cv.Name.Replace("card", "sp_sign")) as StackPanel;
                if (sp != null) sp.Children.Clear();

                #endregion

                //脱离父控件
                card.getAwayFromParents();


                //对目标地的处理
                card.set2FrontAtk();
                cv_aim.Children.Add(card);

                
                
            }
        }

        #endregion

        #region <-- 对手怪物区释放 -->

        /// <summary>
        /// 对手怪物区地释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop_OpMonster(object sender, DragEventArgs e)
        {
            cardview = CardView.getInstance(mainwindow);

            IDataObject data = e.Data;

            if (data.GetDataPresent(typeof(BitmapImage)))
            {
                //获得卡片对象
                CardUI card = data.GetData(typeof(BitmapImage)) as CardUI;
                //MessageBox.Show(rect.Parent.GetType().ToString().Contains("Canvas"));

                //判断卡片原有位置的父容器类型
                MyCanvas cv = card.Parent as MyCanvas;

                //获取放置容器
                MyCanvas cv_aim = sender as MyCanvas;

                if (DuelOperate.getInstance().curPhase == Phase.BP)
                {
                    if (cv_aim.Children.Count > 0)
                    {

                        #region 指令发送

                        AtkInfo atkInfo = new AtkInfo();
                        CardUI aim = cv_aim.Children[0] as CardUI;
                        atkInfo.cardID = getCardID(card);
                        atkInfo.aimID = getCardIDOP(aim);
                        String contentJson = JsonConvert.SerializeObject(atkInfo);

                        BaseJson bj = new BaseJson();
                        bj.guid = DuelOperate.getInstance().myself.userindex;
                        bj.cid = "";
                        bj.action = ActionCommand.CARD_ATK;
                        bj.json = contentJson;
                        String json = JsonConvert.SerializeObject(bj);
                        DuelOperate.getInstance().sendMsg(json);

                        #endregion


                        if (e.KeyStates == DragDropKeyStates.None && !mb_right.Equals("Pressed"))
                        {
                            #region 攻击动画

                            Point p1 = card.TranslatePoint(new Point(card.ActualWidth / 2, card.ActualHeight / 2), mainwindow.OpBattle);
                            Point p2 = aim.TranslatePoint(new Point(aim.ActualWidth / 2, aim.ActualHeight / 2), mainwindow.OpBattle);
                            //double angle = Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) * (180 / Math.PI) + 90;


                            MyStoryboard msb = CardAnimation.Atk(p1, p2, 600);
                            msb.Completed += (object sender_, EventArgs e_) =>
                            {
                                mainwindow.OpBattle.Children.Remove(msb.sword);
                                msb.sword = null;

                            };
                            MyStoryboard msb2 = CardAnimation.Atk(p1, p2, 500);
                            msb2.Completed += (object sender_, EventArgs e_) =>
                            {
                                mainwindow.OpBattle.Children.Remove(msb2.sword);
                                msb2.sword = null;
                            };
                            MyStoryboard msb3 = CardAnimation.Atk(p1, p2, 400);
                            msb3.Completed += (object sender_, EventArgs e_) =>
                            {
                                mainwindow.OpBattle.Children.Remove(msb3.sword);
                                msb3.sword = null;

                            };
                            MyStoryboard msb4 = CardAnimation.Atkline(p1, p2, 600);
                            msb4.Completed += (object sender_, EventArgs e_) =>
                            {
                                mainwindow.OpBattle.Children.Remove(msb4.sword);
                                msb4.sword = null;
                                //mainwindow.OpBattle.Children.Remove(msb4.sword);
                                //msb4.sword = null;

                            };


                            msb4.Begin();
                            msb.Begin();
                            msb2.Begin();
                            msb3.Begin();

                            #endregion
                        }
                    }
                    
                }

                    ////判断目标位置是否是原位置
                    //if (cv.Name.Equals(cv_aim.Name)) return;

                    ////对出发地的判断处理

                    //if (cv_magictraps_1.Contains(cv))
                    //{
                    //    if (cv_aim.Children.Count > 0)
                    //    {
                    //        #region 选取对象

                    //        if (e.KeyStates == DragDropKeyStates.AltKey && !mb_right.Equals("Pressed"))
                    //        {
                    //            DuelOperate.getInstance().sendMsg("SelectObject=" + cv.Name + "," + cv_aim.Name, "选择对象");

                    //            MyStoryboard msb = CardAnimation.EffectOrigin();
                    //            msb.Begin((mainwindow.FindName(cv.Name.Replace("card", "bd")) as FrameworkElement));
                    //            MyStoryboard msb2 = CardAnimation.EffectAim();
                    //            msb2.Begin((mainwindow.FindName(cv_aim.Name.Replace("card", "bd")) as FrameworkElement));
                    //            return;
                    //        }

                    //        #endregion
                    //    }
                    //}

                    //if (cv_monsters_1.Contains(cv))
                    //{
                    //    if (cv_aim.Children.Count > 0)
                    //    {
                    //        #region 选取对象

                    //        if (e.KeyStates == DragDropKeyStates.AltKey && !mb_right.Equals("Pressed"))
                    //        {
                    //            DuelOperate.getInstance().sendMsg("SelectObject=" + cv.Name + "," + cv_aim.Name, "选择对象");

                    //            MyStoryboard msb = CardAnimation.EffectOrigin();
                    //            msb.Begin((mainwindow.FindName(cv.Name.Replace("card", "bd")) as FrameworkElement));
                    //            MyStoryboard msb2 = CardAnimation.EffectAim();
                    //            msb2.Begin((mainwindow.FindName(cv_aim.Name.Replace("card", "bd")) as FrameworkElement));
                    //            return;
                    //        }

                    //        #endregion

                    //        #region 攻击宣言

                    //        if (e.KeyStates == DragDropKeyStates.None && !mb_right.Equals("Pressed"))
                    //        {
                    //            #region 攻击动画

                    //            Point p1 = cv.TranslatePoint(new Point(cv.ActualWidth / 2, cv.ActualHeight / 2), mainwindow.OpBattle);
                    //            Point p2 = cv_aim.TranslatePoint(new Point(cv_aim.ActualWidth / 2, cv_aim.ActualHeight / 2), mainwindow.OpBattle);
                    //            //double angle = Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) * (180 / Math.PI) + 90;


                    //            MyStoryboard msb = CardAnimation.Atk(p1, p2, 800);
                    //            msb.Completed += (object sender_, EventArgs e_) =>
                    //            {
                    //                mainwindow.OpBattle.Children.Remove(msb.sword);
                    //                msb.sword = null;

                    //            };
                    //            MyStoryboard msb2 = CardAnimation.Atk(p1, p2, 700);
                    //            msb2.Completed += (object sender_, EventArgs e_) =>
                    //            {
                    //                mainwindow.OpBattle.Children.Remove(msb2.sword);
                    //                msb2.sword = null;
                    //            };
                    //            MyStoryboard msb3 = CardAnimation.Atk(p1, p2, 600);
                    //            msb3.Completed += (object sender_, EventArgs e_) =>
                    //            {
                    //                mainwindow.OpBattle.Children.Remove(msb3.sword);
                    //                msb3.sword = null;

                    //            };
                    //            MyStoryboard msb4 = CardAnimation.Atkline(p1, p2, 800);
                    //            msb4.Completed += (object sender_, EventArgs e_) =>
                    //            {
                    //                mainwindow.OpBattle.Children.Remove(msb4.sword);
                    //                msb4.sword = null;
                    //                //mainwindow.OpBattle.Children.Remove(msb4.sword);
                    //                //msb4.sword = null;

                    //            };


                    //            msb4.Begin();
                    //            msb.Begin();
                    //            msb2.Begin();
                    //            msb3.Begin();

                    //            #endregion

                    //            DuelOperate.getInstance().sendMsg("Atk=" + card.duelindex + "," + cv_aim.Name, "攻击宣言");
                    //            return;
                    //        }

                    //        #endregion





                    //    }

                    //    #region 转移控制权

                    //    //if (e.KeyStates == DragDropKeyStates.ShiftKey)
                    //    //{
                    //    //    if (DuelOperate.getInstance().opponent.deck.Main.Contains(card) || DuelOperate.getInstance().opponent.deck.Extra.Contains(card) )
                    //    //    {
                    //    //        card.ToolTip = null;
                    //    //        card.ContextMenu = null;
                    //    //    }

                    //    //    cv.Children.Remove(card);
                    //    //    CardOperate.sort(cv, card);

                    //    //    #region 取消拖拽的事件注册

                    //    //    card.PreviewMouseMove -= new MouseEventHandler(DuelEvent.CardDragStart);
                    //    //    card.MouseDown -= new MouseButtonEventHandler(DuelEvent.ClikDouble);

                    //    //    card.QueryContinueDrag -= new QueryContinueDragEventHandler(DuelEvent.card_DragContinue);
                    //    //    card.MouseEnter -= new MouseEventHandler(DuelEvent.card_picture_MouseEnter);

                    //    //    #endregion

                    //    //    #region 转移指示物

                    //    //    StackPanel sp = mainwindow.FindName(cv.Name.Replace("card", "sp_sign")) as StackPanel;
                    //    //    if (sp.Children.Count > 0)
                    //    //    {
                    //    //        StackPanel sp_aim = mainwindow.FindName(cv_aim.Name.Replace("card", "sp_sign")) as StackPanel;
                    //    //        int count = sp.Children.Count;
                    //    //        for (int i = 0; i < count; i++)
                    //    //        {
                    //    //            Grid gd = sp.Children[0] as Grid;
                    //    //            TextBlock tb = gd.Children[1] as TextBlock;
                    //    //            tb.MouseDown -= new MouseButtonEventHandler(DuelEvent.ClikDouble2);
                    //    //            sp.Children.Remove(gd);
                    //    //            sp_aim.Children.Add(gd);
                    //    //        }
                    //    //    }

                    //    //    #endregion

                    //    //    //if (!card.isMine)
                    //    //    //{
                    //    //    //    DuelOperate.getInstance().sendMsg("ControlChange=" + "1," + card.duelindex + "," + cv_aim.Name, "转移控制权");
                    //    //    //    card.isMine = true;
                    //    //    //}
                    //    //    //else if (card.isMine)
                    //    //    //{
                    //    //    //    DuelOperate.getInstance().sendMsg("ControlChange=" + "2," + card.duelindex + "," + cv_aim.Name, "转移控制权");
                    //    //    //    card.isMine = false;
                    //    //    //}

                    //    //    DuelOperate.getInstance().sendMsg("ControlChange="  + card.duelindex + "," + cv_aim.Name, "转移控制权");
                    //    //    if (cv_aim.Children.Count > 0)
                    //    //    {
                    //    //        if (card.isDef)
                    //    //        {
                    //    //            card_FrontAtk(card);
                    //    //        }
                    //    //        cv_aim.Children.Insert(0, card);
                    //    //    }
                    //    //    else
                    //    //    {
                    //    //        cv_aim.Children.Add(card);
                    //    //        if (cv.Children.Count > 1 && card.sCardType.Equals("XYZ怪兽"))
                    //    //        {
                    //    //            int ControlChangeNum = cv.Children.Count;
                    //    //            for (int i = 0; i < ControlChangeNum; i++)
                    //    //            {
                    //    //                Card card2 = cv.Children[0] as Card;
                    //    //                Base.getawayParerent(card2);
                    //    //                CardOperate.sort(cv_aim, card2);
                    //    //                //if (!card2.isMine)
                    //    //                //{
                    //    //                //    //DuelOperate.getInstance().sendMsg("ControlChange=" + "1," + card2.duelindex + "," + cv_aim.Name, "转移控制权");
                    //    //                //    card2.isMine = true;
                    //    //                //}
                    //    //                //else if (card2.isMine)
                    //    //                //{
                    //    //                //    //DuelOperate.getInstance().sendMsg("ControlChange=" + "2," + card2.duelindex + "," + cv_aim.Name, "转移控制权");
                    //    //                //    card2.isMine = false;
                    //    //                //}

                    //    //                cv_aim.Children.Insert(0, card2);

                    //    //                #region 取消拖拽的事件注册

                    //    //                card2.PreviewMouseMove -= new MouseEventHandler(DuelEvent.CardDragStart);
                    //    //                card2.MouseDown -= new MouseButtonEventHandler(DuelEvent.ClikDouble);

                    //    //                card2.QueryContinueDrag -= new QueryContinueDragEventHandler(DuelEvent.card_DragContinue);
                    //    //                card2.MouseEnter -= new MouseEventHandler(DuelEvent.card_picture_MouseEnter);

                    //    //                #endregion

                    //    //            }

                    //    //        }
                    //    //    }
                    //    //    card.SetPic();
                    //    //    CardOperate.sort(cv_aim, card);



                    //    //    return;
                    //    //}

                    //    #endregion
                    //}



                    //对目标地的处理

                }
        }

        #endregion

        #region <-- 对手魔陷区释放 -->

        /// <summary>
        /// 对手魔陷区释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop_OpMagic(object sender, DragEventArgs e)
        {
            cardview = CardView.getInstance(mainwindow);

            IDataObject data = e.Data;

            if (data.GetDataPresent(typeof(BitmapImage)))
            {
                //获得卡片对象
                Card card = data.GetData(typeof(BitmapImage)) as Card;
                //MessageBox.Show(rect.Parent.GetType().ToString().Contains("Canvas"));

                //判断卡片原有位置的父容器类型
                Canvas cv = card.Parent as Canvas;

                //获取放置容器
                Canvas cv_aim = sender as Canvas;

                //判断目标位置是否是原位置
                if (cv.Name.Equals(cv_aim.Name)) return;

                //对出发地的判断处理



                if (cv.Name.Length == 8 || cv.Name.Length == 9)
                {

                    int witch;
                    if (int.TryParse(cv.Name.Substring(7), out witch))
                    {
                        #region 选对象

                        if (witch > 0 && witch < 11)
                        {
                            if (cv_aim.Children.Count > 0)
                            {
                                #region 选取对象

                                if (e.KeyStates == DragDropKeyStates.AltKey && !mb_right.Equals("Pressed"))
                                {
                                    //DuelOperate.getInstance().sendMsg("SelectObject=" + cv.Name + "," + cv_aim.Name, "选择对象");

                                    MyStoryboard msb = CardAnimation.EffectOrigin();
                                    msb.Begin((mainwindow.FindName(cv.Name.Replace("card", "bd")) as FrameworkElement));
                                    MyStoryboard msb2 = CardAnimation.EffectAim();
                                    msb2.Begin((mainwindow.FindName(cv_aim.Name.Replace("card", "bd")) as FrameworkElement));
                                    return;
                                }

                                #endregion
                            }


                        }

                        #endregion


                    }
                }

                //对目标地的处理

            }
        }

        #endregion

        #region <-- 对手手卡区的释放 -->

        internal static void card_Drop_Hand2(object sender, DragEventArgs e)
        {
            cardview = CardView.getInstance(mainwindow);

            IDataObject data = e.Data;

            if (data.GetDataPresent(typeof(BitmapImage)))
            {
                //获得卡片对象
                Card card = data.GetData(typeof(BitmapImage)) as Card;
                //MessageBox.Show(rect.Parent.GetType().ToString().Contains("Canvas"));

                //判断卡片原有位置的父容器类型
                Canvas cv = card.Parent as Canvas;

                //获取放置容器
                Canvas cv_aim = sender as Canvas;

                //判断目标位置是否是原位置
                if (cv.Name.Equals(cv_aim.Name)) return;

                if (!cv_monsters_1.Contains(cv))
                {
                    return;
                }

                #region 攻击宣言



                if (e.KeyStates == DragDropKeyStates.None && !mb_right.Equals("Pressed"))
                {
                    #region 攻击动画

                    Point p1 = cv.TranslatePoint(new Point(cv.ActualWidth / 2, cv.ActualHeight / 2), mainwindow.OpBattle);
                    Point p2 = cv_aim.TranslatePoint(new Point(cv_aim.ActualWidth / 2, cv_aim.ActualHeight / 2), mainwindow.OpBattle);
                    //double angle = Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) * (180 / Math.PI) + 90;


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
                    MyStoryboard msb4 = CardAnimation.Atkline(p1, p2, 800);
                    msb4.Completed += (object sender_, EventArgs e_) =>
                    {
                        mainwindow.OpBattle.Children.Remove(msb4.sword);
                        msb4.sword = null;
                        //mainwindow.OpBattle.Children.Remove(msb4.sword);
                        //msb4.sword = null;

                    };


                    msb4.Begin();
                    msb.Begin();
                    msb2.Begin();
                    msb3.Begin();

                    #endregion

                    //DuelOperate.getInstance().sendMsg("Atk=" + card.duelindex + "," + cv_aim.Name, "攻击宣言");
                    return;
                }

                #endregion


            }


            
        }

        #endregion

        #region <-- CardView区的释放 -->

        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop(object sender, DragEventArgs e)
        {
           

            cardview = CardView.getInstance(mainwindow);

            //获得拖放对象
            IDataObject data = e.Data;

            //MessageBox.Show( LogicalTreeHelper.GetParent(sender as Canvas).ToString());

            //判断拖放对象是否为卡片
            if (data.GetDataPresent(typeof(BitmapImage)))
            {
                ////获得卡片对象
                //Card card = data.GetData(typeof(BitmapImage)) as Card;
                ////MessageBox.Show(rect.Parent.GetType().ToString().Contains("Canvas"));

                ////判断卡片原有位置的父容器类型
                //Canvas cv = card.Parent as Canvas;

                ////获取放置容器
                //Canvas cv_aim = sender as Canvas;

                ////判断目标位置是否是原位置
                //if (cv.Name.Equals(cv_aim.Name)) return;

                

                ////获取原卡片所在原控件的位置
                //int cv_num = mainwindow.MySpace.Children.IndexOf(cv);
                //int cv_childre_num = cv.Children.IndexOf(card);
                //int cv_aim_num = mainwindow.MySpace.Children.IndexOf(cv_aim);

                ////选取效果对象
                //if (e.KeyStates == DragDropKeyStates.AltKey)
                //{
                //    if (cv_aim.Children.Count < 1) return;
                //    MyStoryboard msb = CardAnimation.EffectOrigin();
                //    msb.Begin((mainwindow.FindName(cv.Name.Replace("card", "bd")) as FrameworkElement));
                //    MyStoryboard msb2 = CardAnimation.EffectAim();
                //    msb2.Begin((mainwindow.FindName(cv_aim.Name.Replace("card", "bd")) as FrameworkElement));
                //    return;
                //}

                //if ( mb_right.Equals("Pressed") && cv_num > 24 && cv_num < 35 && cv_aim_num > 29 && cv_aim_num < 35)
                //{
                //    if (cv_aim.Children.Count<1)
                //    {
                //        //string path = Application.Current.StartupUri + @"/Data/Token/";
                //        for (int i = 0; ; i++)
                //        {
                //            string img = System.IO.Directory.GetCurrentDirectory() + @"\Data\Token\" + card.cheatcode + Convert.ToChar('A' + i) + ".jpg";
                //            if (File.Exists(img)) 
                //            {
                //                Card token = new Card();
                //                token.Width = 59;
                //                token.Height = 81;
                //                switch (card.Name)
                //                {
                //                    default:
                //                        {
                //                            token.atk = "0";
                //                            token.def = "0";
 
                //                        }
                //                        break;
                //                }
                //                token.name = card.name + "[Token - " + (i+1) + " ]";
                //                token.cheatcode = card.cheatcode + Convert.ToChar('A' + i);
                //                BitmapImage image = new BitmapImage(new Uri(img, UriKind.Absolute));
                //                token.Source = image;
                //                if (i == 0)
                //                {
                //                    cv_aim.Children.Add(token);
                //                    sort(cv_aim, token);
                //                }
                //                else
                //                {
                //                    for (int j = 6; j < 11; j++)
                //                    {

                //                        Canvas cv_token = mainwindow.FindName("card_1_" + j) as Canvas;
                //                        if (cv_token.Children.Count < 1)
                //                        {
                //                            cv_token.Children.Add(token);
                //                            sort(cv_token, token);
                //                            break;
                //                        }
                //                        else if (j == 10)
                //                        {
                //                            return;
                //                        }

                //                    }
                //                }

                                
                                
                //            }
                //            else 
                //            {
                //                return;
                //            }

                //        }

                        
                //    }
                //    return;
 
                //}

                ////禁止从场地额外区取出
                ////Canvas cv_check = cv.Parent as Canvas;
                //if (cv.Name.Equals("card_1_Extra") && !(cv.Parent is System.Windows.Controls.Grid))
                //{
                //    MessageBox.Show("额外区卡片不允许向外拖拽！");
                //    return;
                //}



                ////判断目标是否可以叠加
                //if (cv_aim.Name.Equals("card_1_Right") || cv_aim.Name.Equals("card_1_Left")||cv_aim.Name.Equals("card_1_Area") || (cv_aim_num>24 && cv_aim_num<30))
                //{
                //    if (cv_aim.Children.Count > 0)
                //    {
                //        MessageBox.Show("此区不允许叠加！");
                //        return;
                //    }
                //    else if ((cv_aim.Name.Equals("card_1_Right") || cv_aim.Name.Equals("card_1_Left")) && !card.CardDType.Contains("灵摆"))
                //    {
                //        MessageBox.Show("灵摆区只允许放置[灵摆怪兽]！");
                //        return;
                //    }
                //}
                ////else if (cv_aim_num > 29 && cv_aim_num < 35)
                ////{
                ////    if (cv.Parent is System.Windows.Controls.Grid)
                ////    {
                ////        if (cv_aim.Children.Count > 0 && !card.scCardType.Equals("XYZ怪兽"))
                ////        {
                ////            MessageBox.Show("此区有卡，非[XYZ怪兽]不允许叠放！");
                ////            return;
                ////        }
                ////    }

                ////}
                //else if (cv_aim.Name.Substring(7).Equals("Extra"))
                //{
                //    if (!card.sCardType.Equals("XYZ怪兽") && !card.sCardType.Equals("融合怪兽") && !card.sCardType.Equals("同调怪兽") && !card.CardDType.Equals("效果·摇摆"))
	               // {
                //        MessageBox.Show("额外区只允许放置[灵摆怪兽]，[融合怪兽]，[同调怪兽],[XYZ怪兽]！");
                //        return;	 
	               // }
                    
                //}
                //else if (cv_aim_num > 17 && cv_aim_num < 23)
                //{
                //    if (e.KeyStates == DragDropKeyStates.None)
                //    {

                //        #region 攻击动画

                //        Point p1 = cv.TranslatePoint(new Point(cv.ActualWidth / 2, cv.ActualHeight / 2), mainwindow.OpBattle);
                //        Point p2 = cv_aim.TranslatePoint(new Point(cv_aim.ActualWidth / 2, cv_aim.ActualHeight / 2), mainwindow.OpBattle);
                //        double angle = Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) * (180 / Math.PI) + 90;


                //        MyStoryboard msb = CardAnimation.Atk(p1, p2, 800);
                //        msb.Completed += (object sender_, EventArgs e_) =>
                //        {
                //            mainwindow.OpBattle.Children.Remove(msb.sword);
                //            msb.sword = null;

                //        };
                //        MyStoryboard msb2 = CardAnimation.Atk(p1, p2, 700);
                //        msb2.Completed += (object sender_, EventArgs e_) =>
                //        {
                //            mainwindow.OpBattle.Children.Remove(msb2.sword);
                //            msb2.sword = null;
                //        };
                //        MyStoryboard msb3 = CardAnimation.Atk(p1, p2, 600);
                //        msb3.Completed += (object sender_, EventArgs e_) =>
                //        {
                //            mainwindow.OpBattle.Children.Remove(msb3.sword);
                //            msb3.sword = null;

                //        };
                //        MyStoryboard msb4 = CardAnimation.Atkline(p1, p2, 800);
                //        msb4.Completed += (object sender_, EventArgs e_) =>
                //        {
                //            mainwindow.OpBattle.Children.Remove(msb4.sword);
                //            msb4.sword = null;
                //            //mainwindow.OpBattle.Children.Remove(msb4.sword);
                //            //msb4.sword = null;

                //        };


                //        msb4.Begin();
                //        msb.Begin();
                //        msb2.Begin();
                //        msb3.Begin();

                //        #endregion

                //        DuelOperate.getInstance().sendMsg("Atk=" + card.duelindex + "," + cv_aim.Name, "攻击宣言");
                //        return;
                //    }

                //}

                ////脱离原来位置
                //cv.Children.Remove(card);
                //if (cv.Parent is System.Windows.Controls.Grid)
                //{
                //    sort_CardView(cv,10);
                //}
                //if(cv.Name.Equals("card_1_hand"))
                //{
                //    sort_HandCard(mainwindow.card_1_hand);
                //}
                //if (cv_num > 29 && cv_num < 35)
                //{
                //    sort_Canvas(cv);
                //}
                ////else if (cv.Name.Equals("card_1_Deck") || cv.Name.Equals("card_1_Extra") || cv.Name.Equals("card_1_Outside") || cv.Name.Equals("card_1_Graveyard"))
                ////{
                ////    //get_Firstcard2Battle(6);
                ////    //CardSort(cv, 56, 81, 0);
                ////}
                ////else if (cv.Children.Count > 0)
                ////{
                ////    CardSort(cv, 56, 81, 2);
                ////}

                //Canvas.SetLeft(card, 0);
                //Canvas.SetTop(card, 0);

                ////开始放置
                //if (cv_aim != null)
                //{
                    

                //    //如果拖拽出发地是卡片预览区则直接添加
                //    if (cv_aim.Parent is System.Windows.Controls.Grid)
                //    {
                //        #region 如果出发控件为预览区时

                //        //如果是额外区，还需要排序
                //        if (cv_aim.Name.Equals("card_1_Extra"))
                //        {
                //            card_ExtraSort(card, cardview.card_1_Extra);
                //        }
                //        else
                //        {
                //            cv_aim.Children.Add(card);
                //            sort_CardView(cv_aim,10);                   
                //        }

                //        //战报
                //        switch (cv_aim.Name)
                //        {
                //            case "card_1_Deck":
                //                mainwindow.report.Text += ("从 " + DuelReportOperate.from(cv.Name) + " 把 [" + card.name + "] 放回卡组底部" + Environment.NewLine);
                //                break;
                //            case "card_1_Graveyard":
                //                mainwindow.report.Text += ("从 " + DuelReportOperate.from(cv.Name) + " 把 [" + card.name + "] 送入墓地" + Environment.NewLine);
                //                break;
                //            case "card_1_Extra":
                //                mainwindow.report.Text += (DuelReportOperate.from(cv.Name) + " [" + card.name + "] 返回额外" + Environment.NewLine);
                //                break;
                //            case "card_1_Outside":
                //                mainwindow.report.Text += ("从 " + DuelReportOperate.from(cv.Name) + " 把 [" + card.name + "] 除外" + Environment.NewLine);
                //                break;

                //        }

                //       // OpponentOperate.ActionAnalyze("Draw=1,0,36");
                        
                //        //动作
                //        //OpponentOperate.ActionAnalyze("Move=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name));
                //        return;

                        

                //        #endregion

                //    }


                //    if (cv_aim.Name.Equals("card_1_Extra") || cv_aim.Name.Equals("card_1_Deck"))
                //    {
                //        #region 卡组，额外区放置

                //        card_BackAtk(card);
                //        cv_aim.Children.Add(card);
                //        if (cv_aim.Name.Equals("card_1_Extra")) card.ContextMenu = null;
                //        if (cv_aim.Name.Equals("card_1_Deck")) card.ContextMenu = AllMenu.cm_deck;
                //        sort_SingleCard(card);

                //        //OpponentOperate.ActionAnalyze("Move=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name));
                //        //战报
                //        if (cv_aim.Name.Equals("card_1_Deck"))
                //        {
                //            if (mb_right.Equals("Pressed"))
                //            {
                //                mainwindow.report.Text += ("将 " + DuelReportOperate.from(cv.Name) + " [?] 放回<卡组>顶端" + Environment.NewLine);
                //            }
                //            else
                //            {
                //                mainwindow.report.Text += ("将 " + DuelReportOperate.from(cv.Name) + " [" + card.name + "] 放回<卡组>顶端" + Environment.NewLine);
 
                //            }
                //        }
                //        else if (cv_aim.Name.Equals("card_1_Extra"))
                //        {
                //            mainwindow.report.Text += (DuelReportOperate.from(cv.Name) + " [" + card.name + "] 返回额外" + Environment.NewLine);
                            
                //        }
                //        //动作
                //        DuelOperate.getInstance().sendMsg("Back2Hand=" + card.duelindex + "," + cv_aim.Name, "返回");

                //        #endregion

                //    }
                //    else if (cv_aim_num > 29 && cv_aim_num < 35)
                //    {
                //        #region 怪物区放置
                //        card.ContextMenu = AllMenu.cm_monster;

                //        if (cv.Parent is System.Windows.Controls.Grid)
                //        {
                //            #region 当卡片来自预览区

                //            if (cv.Name.Substring(7).Equals("Extra"))
                //            {
                //                #region 若卡片来自额外区

                //                cv_aim.Children.Add(card);

                //                sort_Canvas(cv_aim);


                //                if (card.sCardType.Equals("XYZ怪兽"))
                //                {
                //                    if (cv_aim.Children.Count > 1)
                //                    {
                //                        #region 以一只怪兽叠放

                //                        Card top2 = cv_aim.Children[cv_aim.Children.Count - 2] as Card;
                //                        if (top2.isDef == true && top2.isBack == true)
                //                        {
                //                            //里防
                //                            CardAnimation.Rotate2Atk(top2);
                //                            top2.isBack = false;
                //                            top2.isDef = false;
                //                        }
                //                        else if (top2.isDef == true)
                //                        {
                //                            //表防
                //                            CardAnimation.Def_or_Atk(top2);
                //                            top2.isDef = false;
                //                        }
                //                        //top2.ContextMenu = CardMenu.cm_XYZmeterial;

                //                        #endregion
                //                    }

                //                    #region XYZ召唤

                //                    //XYZ召唤
                //                    //card.ContextMenu = CardMenu.cm_XYZ;

                //                    //动作
                //                    //OpponentOperate.ActionAnalyze("Summon=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                    DuelOperate.getInstance().sendMsg("Summon=" + card.duelindex + "," + cv_aim.Name, "XYZ召唤");
                //                    //战报
                //                    mainwindow.report.Text += ("从 <额外> XYZ召唤 [" + card.name + "] -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine);

                //                    #endregion
                //                }
                //                else
                //                {
                //                    #region 同调，融合等特殊召唤

                //                    card.ContextMenu = AllMenu.cm_monster;

                //                    //动作
                //                    //OpponentOperate.ActionAnalyze("Summon=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                    DuelOperate.getInstance().sendMsg("Summon=" + card.duelindex + "," + cv_aim.Name, "特殊召唤");
                //                    //战报
                //                    mainwindow.report.Text += ("从 <额外> 特殊召唤 [" + card.name + "] -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine);

                //                    #endregion
                //                }

                //                #endregion

                //            }
                //            else
                //            {
                //                cv_aim.Children.Add(card);
                //                CardOperate.sort_Canvas(cv_aim);
                //                mainwindow.report.Text += ("从 "+ DuelReportOperate.from(cv.Name) +" 特殊召唤 [" + card.name + "] -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine);
                //                DuelOperate.getInstance().sendMsg("Disappear=" + card.duelindex + "," + cv_aim.Name, "");
                //            }

                //            #endregion
                //        }
                //        else
                //        {
                            

                //            //当卡片数大于0时，则为叠放素材或添加
                //            if (cv_aim.Children.Count > 0)
                //            {
                //                if (cv.Name.Equals("card_1_Graveyard") && card.sCardType.Equals("XYZ怪兽"))
                //                {
                //                    cv_aim.Children.Add(card);
                //                    CardOperate.sort_Canvas(cv_aim);
                //                    DuelOperate.getInstance().sendMsg("Summon=" + card.duelindex + "," + cv_aim.Name, "将什么添加为素材");
                                   
                //                    //card.ContextMenu = CardMenu.cm_XYZ;
                //                    return;
                //                }

                //                Card card2 = cv_aim.Children[cv_aim.Children.Count - 1] as Card;

                //                if (card.isDef)
                //                {
                //                    card_FrontAtk(card);
                //                }

                //                //最上一张防守且为XYZ怪兽则为添加素材
                //                if (card2.isDef == true && card2.sCardType.Equals("XYZ怪兽"))
                //                {

                //                    cv_aim.Children.Insert(0, card);
                //                    sort_Canvas(cv_aim);

                //                    //OpponentOperate.ActionAnalyze("Summon=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                    DuelOperate.getInstance().sendMsg("Summon=" + card.duelindex + "," + cv_aim.Name, "将什么添加为素材");
                                    
                //                    //战报
                //                    mainwindow.report.Text += ("将 " + DuelReportOperate.from(cv.Name) + " [" + card.name + "] 添加为 ["+ card2.name +"] 的素材 " + Environment.NewLine);
                //                }
                //                //如果第一张为XYZ表攻
                //                else if (card2.sCardType.Equals("XYZ怪兽"))
                //                {
                //                    cv_aim.Children.Insert(0, card);
                //                    sort_Canvas(cv_aim);
                //                    //动作
                //                    //OpponentOperate.ActionAnalyze("Summon=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                    DuelOperate.getInstance().sendMsg("Summon=" + card.duelindex + "," + cv_aim.Name, "XYZ将什么添加为素材");
                //                    //战报
                //                    mainwindow.report.Text += ("将 " + DuelReportOperate.from(cv.Name) + " [" + card.name + "] 添加为 [" + card2.name + "] 的素材 " + Environment.NewLine);
                //                }
                //                else
                //                {

                //                    //最上一张防守且背面
                //                    if (card2.isDef == true && card2.isBack == true)
                //                    {
                //                        //card_FrontAtk(card2);
                                        
                //                        CardAnimation.Rotate2Atk(card2);
                //                        card2.isBack = false;
                //                        card.isDef = false;
                //                        //CardAnimation.RotateOut(card2, -90, 0);

                //                    }
                //                    //最上一张只是防守
                //                    else if (card2.isDef == true)
                //                    {
                //                        CardAnimation.Def_or_Atk(card2);
                //                        card2.isDef = false;

                //                    }
                //                    cv_aim.Children.Add(card);
                //                    //cv_aim.Children.Insert(0, card);
                //                    sort_Canvas(cv_aim);

                //                    //战报
                //                    if (card2.sCardType.Equals("XYZ怪兽"))
                //                    {

                //                        mainwindow.report.Text += ("将 " + DuelReportOperate.from(cv.Name) + " [" + card.name + "] 添加为 [" + card2.name + "] 的素材 -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine);
                //                    }
                //                    else
                //                    {
                //                        //mainwindow.report.Text += ("从 " + DuelReportOperate.from(mainwindow.MySpace.Children.IndexOf(cv)) + " 把 [" + card.cardName + "] 作为素材叠加 -> " + DuelReportOperate.from(mainwindow.MySpace.Children.IndexOf(cv_aim)) + Environment.NewLine);
                //                    }

                //                }
                //                //card.ContextMenu = CardMenu.cm_XYZmeterial;

                //                //当素材增加至两张后，应该把原来第一张的卡片菜单项更换为素材菜单
                //                if (cv_aim.Children.Count == 2 && !card2.sCardType.Equals("XYZ怪兽"))
                //                {
                //                    card2 = cv_aim.Children[0] as Card;
                //                    //card2.ContextMenu = CardMenu.cm_XYZmeterial;
                //                    //动作
                //                    if (card.isDef)
                //                    {
                                        
                //                    }
                //                    else if (cv_num > 24 && cv_num < 30)
                //                    {
                //                        //OpponentOperate.ActionAnalyze("Move=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                        DuelOperate.getInstance().sendMsg("Move=" + card.duelindex + "," + cv_aim.Name, "XYZ将什么添加为素材");
                //                    }
                //                    else if (cv.Name.Equals("card_1_hand"))
                //                    {
                //                        //OpponentOperate.ActionAnalyze("Summon=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                        DuelOperate.getInstance().sendMsg("Summon=" + card.duelindex + "," + cv_aim.Name, "");
                //                    }
                //                    else
                //                    {
                //                        //OpponentOperate.ActionAnalyze("Move=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                        DuelOperate.getInstance().sendMsg("Move=" + card.duelindex + "," + cv_aim.Name, "XYZ将什么添加为素材");
                //                    }
                                    

                //                    //战报
                //                    mainwindow.report.Text += ("将 " + DuelReportOperate.from(cv_aim.Name) + " [" + card2.name + "] 作为素材叠放 -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine +
                //                                               "将 " + DuelReportOperate.from(cv.Name) + " [" + card.name + "] 作为素材叠放 -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine);
                //                }
                //                else if (!card2.sCardType.Equals("XYZ怪兽"))
                //                {
                //                    //动作
                //                    if (cv.Name.Equals("card_1_hand") || cv.Name.Equals("card_1_Deck"))
                //                    {
                //                        //OpponentOperate.ActionAnalyze("Summon=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                        DuelOperate.getInstance().sendMsg("Summon=" + card.duelindex + "," + cv_aim.Name, "");
                //                    }
                //                    else if (card.isDef)
                //                    {

                //                    }
                //                    else
                //                    {
                //                        //OpponentOperate.ActionAnalyze("Move=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                        DuelOperate.getInstance().sendMsg("Move=" + card.duelindex + "," + cv_aim.Name, "");
                //                    }
                //                    //战报
                //                    mainwindow.report.Text += ("将 " + DuelReportOperate.from(cv.Name) + " [" + card.name + "] 作为素材叠放 -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine);
                //                }
                //            }
                //            //场上无卡时，普通召唤
                //            else
                //            {
                //                //Console.WriteLine(e.RoutedEvent.);

                //                //Console.WriteLine(mousebutton);
                //                //如果来自魔陷区
                //                if (cv_num > 24 && cv_num < 30)
                //                {
                //                    if (card.isBack)
                //                    {
                //                        mainwindow.report.Text += ("将 " + DuelReportOperate.from(cv.Name) + " [?] 移动 -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine);
                //                    }
                //                    else
                //                    {
                //                        //动作
                //                        //OpponentOperate.ActionAnalyze("Move=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                        DuelOperate.getInstance().sendMsg("Move=" + card.duelindex + "," + cv_aim.Name, "");
                //                        mainwindow.report.Text += ("从 " + DuelReportOperate.from(cv.Name) + " 特殊召唤 [" + card.name + "] -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine);
                //                    }
                //                    cv_aim.Children.Add(card);
                //                    card.ContextMenu = AllMenu.cm_monster;
                //                    sort_Canvas(cv_aim);
                //                    return;
                //                }

                //                //如果来自怪物区
                //                if (cv_num > 29 && cv_num < 35)
                //                {
                //                    if (card.isBack)
                //                    {
                //                        //OpponentOperate.ActionAnalyze("Move=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                        DuelOperate.getInstance().sendMsg("Move=" + card.duelindex + "," + cv_aim.Name, "");
                //                        mainwindow.report.Text += ("将 " + DuelReportOperate.from(cv.Name) + " [?] 移动 -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine);
                //                    }
                //                    else
                //                    {
                //                        //OpponentOperate.ActionAnalyze("Move=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                        DuelOperate.getInstance().sendMsg("Move=" + card.duelindex + "," + cv_aim.Name, "");
                //                        mainwindow.report.Text += ("将 " + DuelReportOperate.from(cv.Name) + " [" + card.name + "] 移动 -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine);
                //                    }
                                    
                //                    cv_aim.Children.Add(card);
 
                //                }
                //                //如果来自墓地或者除外
                //                else if (cv.Name.Equals("card_1_Graveyard") || cv.Name.Equals("card_1_Outside"))
                //                {
                //                    mainwindow.report.Text += ("从 " + DuelReportOperate.from(cv.Name) + " 特殊召唤 [" + card.name + "] -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine);
                //                    cv_aim.Children.Add(card);
                //                    //OpponentOperate.ActionAnalyze("Move=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                    DuelOperate.getInstance().sendMsg("Move=" + card.duelindex + "," + cv_aim.Name, "");
                //                }
                //                //如果是右键
                //                else if (mb_right.Equals("Pressed"))
                //                {
                //                    card_BackDef(card);
                //                    cv_aim.Children.Add(card);
                                    
                //                    //动作
                //                    //OpponentOperate.ActionAnalyze("Cover=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                    DuelOperate.getInstance().sendMsg("Cover=" + card.duelindex + "," + cv_aim.Name, "");
                //                    //战报
                //                    mainwindow.report.Text += ("从 " + DuelReportOperate.from(cv.Name) + " 取一张 [怪物卡] 盖放 -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine);
                //                    //CardSortsingle(cv_aim, card, 56, 81);
                //                }
                //                else
                //                { 
                //                    //如果按住shift
                //                    if (e.KeyStates == DragDropKeyStates.ShiftKey)
                //                    {
                //                        //card_BackDef(card);
                //                        //cv_aim.Children.Add(card);
                //                        //CardSortsingle(cv_aim, card, 56, 81);

                //                        //动作
                //                        //OpponentOperate.ActionAnalyze("Summon=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                        DuelOperate.getInstance().sendMsg("Summon=" + card.duelindex + "," + cv_aim.Name, "");
                //                        //战报
                //                        mainwindow.report.Text += ("从 " + DuelReportOperate.from(cv.Name) + " 特殊召唤 [" + card.name + "] -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine);
                //                    }
                //                    //如果按住alt
                //                    else if (e.KeyStates == DragDropKeyStates.AltKey)
                //                    {
                //                        //动作
                //                        //OpponentOperate.ActionAnalyze("Summon=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                        DuelOperate.getInstance().sendMsg("Summon=" + card.duelindex + "," + cv_aim.Name, "");
                //                        //战报
                //                        mainwindow.report.Text += ("从 " + DuelReportOperate.from(cv.Name) + " P召唤 [" + card.name + "] -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine);
                //                    }
                //                    else
                //                    {              
                //                        //动作
                //                        //OpponentOperate.ActionAnalyze("Summon=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                        DuelOperate.getInstance().sendMsg("Summon=" + card.duelindex + "," + cv_aim.Name, "");
                //                        //战报
                //                        mainwindow.report.Text += ("从 " + DuelReportOperate.from(cv.Name) + " 召唤 [" + card.name + "] -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine);
                                    
                //                    }
                //                    cv_aim.Children.Insert(0, card);
                //                }

                //                sort_Canvas(cv_aim);
                //                card.ContextMenu = AllMenu.cm_monster;
                               
                //            }
                //        }

                //        #endregion
                //    }
                //    else if (cv_aim_num > 24 && cv_aim_num < 30)
                //    {
                //        #region 魔陷区放置
                //        card.ContextMenu = AllMenu.cm_magictrap;
                //        if (cv_num > 24 && cv_num < 30)
                //        {
                //            if (card.isBack)
                //            {
                //                string report = ("将 " + DuelReportOperate.from(cv.Name) + " [魔陷卡] 移动 -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine);
                //                //OpponentOperate.ActionAnalyze("Move=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                DuelOperate.getInstance().sendMsg("Move" + card.duelindex + "," + cv_aim.Name, report);
                //            }
                //            else
                //            {
                //                string report = ("将 " + DuelReportOperate.from(cv.Name) + " [" + card.name + "] 移动 -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine);
                //                DuelOperate.getInstance().sendMsg("Move=" + card.duelindex + "," + cv_aim.Name, report);
                //                //动作
                //                //OpponentOperate.ActionAnalyze("Move=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                //战报
                                
                //            }                        
                //            cv_aim.Children.Add(card);
                //            sort_SingleCard(card);
                //            card.ContextMenu = AllMenu.cm_magictrap;
                //            return;
                //        }

                //        if (cv_num > 29 && cv_num < 35)
                //        {
                //            if (card.isBack)
                //            {
                //                //战报
                //                mainwindow.report.Text += ("将 " + DuelReportOperate.from(cv.Name) + "  [?] 移动  -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine);
                //            }
                //            else
                //            {

                //                string report = ("将 " + DuelReportOperate.from(cv.Name) + " [" + card.name + "] 作为装备卡 -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine);
                //                DuelOperate.getInstance().sendMsg("Mover=" + card.duelindex + "," + cv_aim.Name,report);

                //                //动作
                //                //OpponentOperate.ActionAnalyze("Move=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                //战报
                                
                //            }
                //            cv_aim.Children.Add(card);
                //            sort_SingleCard(card);
                //            card.ContextMenu = AllMenu.cm_magictrap;
                //            return;
                //        }

                //        if (mb_right.Equals("Pressed"))
                //        {
                //            card_BackAtk(card);
                //            cv_aim.Children.Add(card);
                //            sort_SingleCard(card);

                //            string report = ("从 " + DuelReportOperate.from(cv.Name) + " 取一张 [魔陷卡] 盖放 -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine);
                //            DuelOperate.getInstance().sendMsg("Cover2=" + card.duelindex + "," + cv_aim.Name ,report);
                //            //动作
                //            //OpponentOperate.ActionAnalyze("Cover2=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //            //战报
                            
                //        }
                //        else
                //        {
                //            cv_aim.Children.Add(card);
                //            sort_Canvas(cv_aim);

                //            string report = ("从 " + DuelReportOperate.from(cv.Name) + " 发动 [" + card.name + "] -> " + DuelReportOperate.from(cv_aim.Name) + Environment.NewLine);
                //            //动作
                //            if( cv.Name.Equals("card_1_hand") || cv.Name.Equals("card_1_Deck"))
                //            {
                //                //OpponentOperate.ActionAnalyze("Summon=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                DuelOperate.getInstance().sendMsg("Summon=" + card.duelindex + "," + cv_aim.Name,report);
                //            }
                //            else
                //            {
                //                //OpponentOperate.ActionAnalyze("Move=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                DuelOperate.getInstance().sendMsg("Move=" + card.duelindex + "," + cv_aim.Name, report);
                //            }
                            
                //            //战报
                            
                //        }
                //        card.ContextMenu = AllMenu.cm_magictrap;

                //        #endregion
                //    }
                //    else if (cv_aim.Name.Equals("card_1_Outside") || cv_aim.Name.Equals("card_1_Graveyard") || cv_aim.Name.Equals("card_1_Right") || cv_aim.Name.Equals("card_1_Left") || cv_aim.Name.Equals("card_1_Area"))
                //    {
                //        #region 除外，墓地，左摇摆，右摇摆，场地区放置

                //        card_FrontAtk(card);
                //        cv_aim.Children.Add(card);
                //        if (cv_aim.Name.Contains("Outside")) card.ContextMenu = AllMenu.cm_outside;
                //        else if (cv_aim.Name.Contains("Graveyard")) card.ContextMenu = AllMenu.cm_graveyard;
                //        else if (cv_aim.Name.Contains("Right") || cv_aim.Name.Contains("Left")) card.ContextMenu = AllMenu.cm_pendulum;
                //        //else if (cv_aim.Name.Contains("Area")) card.ContextMenu = CardMenu.cm;
                //        CardOperate.sort_SingleCard(card);

                //        //战报
                //        switch (cv_aim.Name)
                //        {
                //            case "card_1_Deck":
                //                {
                //                    string report = ("将 " + DuelReportOperate.from(cv.Name) + " [" + card.name + "] 放回卡组顶端" + Environment.NewLine);
                //                    DuelOperate.getInstance().sendMsg("Move=" + card.duelindex + "," + cv_aim.Name, report);
                //                }
                //                //动作
                //                //OpponentOperate.ActionAnalyze("Move=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                                
                //                break;
                //            case "card_1_Graveyard":
                //                {
                //                    string report = ("将 " + DuelReportOperate.from(cv.Name) + " [" + card.name + "] 送入墓地" + Environment.NewLine);
                //                    DuelOperate.getInstance().sendMsg("Move=" + card.duelindex + "," + cv_aim.Name, report);
                //                }
                //                break;
                //            case "card_1_Extra":
                //                {
                //                    string report = (DuelReportOperate.from(cv.Name) + " [" + card.name + "] 返回额外" + Environment.NewLine);
                //                    DuelOperate.getInstance().sendMsg("Move=" + card.duelindex + "," + cv_aim.Name, report);
                //                }
                                
                //                //动作
                //                //OpponentOperate.ActionAnalyze("Move=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //                break;

                //            case "card_1_Outside":
                //                {
                //                    #region

                //                    string report = ("将 " + DuelReportOperate.from(cv.Name) + " [" + card.name + "] 除外" + Environment.NewLine);
                //                    DuelOperate.getInstance().sendMsg("Disappear=" + card.duelindex + "," + cv_aim.Name, report);
                                    

                //                    #endregion
                //                }                      
                //                break;
                //            case "card_1_Right":
                //                {
                //                    string report = ("将 " + DuelReportOperate.from(cv.Name) + " [" + card.name + "] 设置为 <右刻度>" + Environment.NewLine);
                //                    DuelOperate.getInstance().sendMsg("Move=" + card.duelindex + "," + cv_aim.Name, report);
                //                }
                //                //动作
                //                //OpponentOperate.ActionAnalyze("Move=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                                
                //                break;
                //            case "card_1_Left":
                //                {
                //                    string report = ("将 " + DuelReportOperate.from(cv.Name) + " [" + card.name + "] 设置为 <左刻度>" + Environment.NewLine);
                //                    DuelOperate.getInstance().sendMsg("Move=" + card.duelindex + "," + cv_aim.Name, report);
                //                }
                //                //动作
                //                //OpponentOperate.ActionAnalyze("Move=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                                
                //                break;
                //        }

                        

                //        #endregion
                //    }
                //    else if (cv_aim.Name.Contains("hand"))
                //    {
                //        #region 手卡区放置

                //        card_FrontAtk(card);
                //        mainwindow.card_1_hand.Children.Add(card);
                //        sort_HandCard(cv_aim);
                //        //card.Margin = new Thickness(0);
                //        card.ContextMenu = AllMenu.cm_hand;
                //        //mainwindow.MySpace.Children.IndexOf(cv);

                //        if (cv.Name.Equals("materials"))
                //        {
                //            //XYZmaterialView xyz_view = XYZmaterialView.getInstance();
                //            DuelOperate.getInstance().sendMsg("Back2Hand=" + card.duelindex + "," + cv_aim.Name, "");
                //            //.ActionAnalyze("Back2Hand=" + card.duelindex + "," + cv_aim.Name, true);
                //            return;
                //        }
                //        //else
                //        //{
                //        //    OpponentOperate.ActionAnalyze("Move=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num + "," + DuelReportOperate.Analyze_canvas(cv_aim.Name), true);
                //        //}

                //        if ( cv.Name.Equals("card_1_Deck") && !(cv.Parent is Grid) )
                //        {
                            
                //            //OpponentOperate.ActionAnalyze("Draw=1,0,36", true);
                //            string report = "抽牌" + Environment.NewLine;
                //            DuelOperate.getInstance().sendMsg("Draw=1" , report);
                            

                //        }
                //        else
                //        {
                            
                //            //OpponentOperate.ActionAnalyze("Back2Hand=" + card.duelindex + "," + cv_aim.Name, true);
                           
                //            string report = ("将 " + DuelReportOperate.from(cv.Name) + " [" + card.name + "] 取回 <手卡>" + Environment.NewLine);
                //            DuelOperate.getInstance().sendMsg("Back2Hand=" + card.duelindex + "," + cv_aim.Name, report);

                //        }

                //        #endregion
                //    }

                //    #region 放置到对方场上

                //    if (cv_aim_num > 17 && cv_aim_num < 23  )
                //    {
                //        if (e.KeyStates == DragDropKeyStates.None)
                //        {
                //            DuelOperate.getInstance().sendMsg("Atk="+ card.duelindex + "," + cv_aim.Name, "攻击宣言");
                //            return;
                //        }


                //        if (cv_aim.Children.Count > 0)
                //        {
                //            if (card.isDef)
                //            {
                //                card_FrontAtk(card);
                //            }
                //            cv_aim.Children.Insert(0, card);
                //        }
                //        else
                //        {
                //            cv_aim.Children.Add(card);
                //        }
                //        CardOperate.sort(cv_aim,card);
                //        if (!card.isMine)
                //        {
                //            DuelOperate.getInstance().sendMsg("ControlChange=" + "1," + card.duelindex + "," + cv_aim.Name, "转移控制权");
                //            card.isMine = true;
                //        }                           
                //        else
                //        {
                //            DuelOperate.getInstance().sendMsg("ControlChange=" + "2," + card.duelindex + "," + cv_aim.Name, "转移控制权");
                //            card.isMine = false;
                //        }
                //        return;
                //    }

                //    #endregion

                //    if (cv_aim.Name.Equals("card_2_Graveyard") || cv_aim.Name.Equals("card_2_Out"))
                //    {
                //        cv_aim.Children.Add(card);
                //        sort(cv_aim, card);
                //        DuelOperate.getInstance().sendMsg("Send2OpGraveyard=" + card.duelindex + "," + cv_aim.Name, "转移控制权");
                //        return;
                //    }


                //}
                
                //rect.SetResourceReference(Image.StyleProperty, "x:null");
            }

        }

        #endregion



        #region <-- 拖拽完成 -->

        /// <summary>
        /// 拖放完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(BitmapImage)) && !e.Data.GetDataPresent(typeof(System.Windows.Shapes.Ellipse)))
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                mb_right = null;
            }
        }

        #endregion

        #endregion

        #region <-- 预览区的Tab切换 -->

        /// <summary>
        /// 卡组，额外，墓地等查看时的TAB切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_View_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cardview = CardView.getInstance(mainwindow);

            int choose = cardview.tb_View.SelectedIndex;
            //Card card;

            //当卡片预览的选择项改变的时候，非选择项区位中的卡片的第一张都要回到场地上
            get_Card2Battle(choose);

            //Console.WriteLine("TAB切换后卡组剩余："+mainwindow.card_1_Deck.Children.Count);
                         
            switch (choose)
            {
                case 0:
                    //对方墓地
                    
                    break;
                case 1:
                    //对方除外
                    
                    break;
                case 2: 
                    //对方额外
                    //DuelOperate.getInstance().sendMsg("None=", "查看<对手额外>");
                    view_Extra2();
                    break;
                case 3: 
                    //额外                   
                    //DuelOperate.getInstance().sendMsg("None=", "查看<额外>");
                    //Console.WriteLine("查看额外");
                    mainwindow.card_1_Extra.AllowDrop = false;
                    //cardview.Show();
                    //cardview.tb_View.Visibility = Visibility.Visible;
                    if (mainwindow.card_1_Extra.Children.Count == 0) return;
                    view_or_set(mainwindow.card_1_Extra, cardview.card_1_Extra, mainwindow.card_1_Extra.Children.Count, "正面");
                    sort_CardView(cardview.card_1_Extra,10);
                    
                    break;
                    
                case 4:
                    //除外
                    //DuelOperate.getInstance().sendMsg("None=", "查看<除外>");
                    //Console.WriteLine("查看除外");
                    mainwindow.card_1_Outside.AllowDrop = false;
                    if (mainwindow.card_1_Outside.Children.Count == 0) return;
                    view_or_set(mainwindow.card_1_Outside, cardview.card_1_Outside, mainwindow.card_1_Outside.Children.Count, "正面");
                    sort_CardView(cardview.card_1_Outside,10);
                    break;
                case 5:
                    //墓地
                    //DuelOperate.getInstance().sendMsg("None=", "查看<墓地>");
                    //Console.WriteLine("查看墓地");
                    mainwindow.card_1_Graveyard.AllowDrop = false;
                    if (mainwindow.card_1_Graveyard.Children.Count == 0) return;
                    view_or_set(mainwindow.card_1_Graveyard, cardview.card_1_Graveyard, mainwindow.card_1_Graveyard.Children.Count, "正面");                   
                    sort_CardView(cardview.card_1_Graveyard,10);               
                    break;
                case 6:
                    //卡组
                    mainwindow.card_1_Deck.AllowDrop = false;
                    //显示卡组数量
                    cardview.card_1_DeckNum.Text = cardview.card_1_Deck.Children.Count + mainwindow.card_1_Deck.Children.Count + "";
                    //聚焦查询框
                    cardview.card_1_DeckNum.TabIndex = 0;                
                    cardview.card_1_DeckNum.SelectionStart = cardview.card_1_DeckNum.Text.Length+1;
                    cardview.card_1_DeckNum.SelectAll();
                    break;

                default:
                    //cardview.Hide();
                    break;
            }
            mainwindow.report.ScrollToEnd();
        }

        #endregion

        #region <-- 把预览区的卡放回场上 -->

        /// <summary>
        /// 把预览区的卡放回场上放置到场上
        /// </summary>
        /// <param name="selectindex"></param>
        public static void get_Card2Battle(int selectindex)
        {
            cardview = CardView.getInstance(mainwindow);



            #region 对方除外

            if (selectindex != 1)
            {

                if (mainwindow.card_2_Outside.Children.Count < 1)
                {
                    view_or_set(cardview.card_2_Outside, mainwindow.card_2_Outside, cardview.card_2_Outside.Children.Count, "正面");
                }


            }

            #endregion

            #region 对方额外

            if (selectindex != 2)
            {

                if (mainwindow.card_2_Outside.Children.Count < 1)
                {
                    view_or_set(cardview.card_2_Extra, mainwindow.card_2_Extra, cardview.card_2_Extra.Children.Count, "正面");
                }


            }

            #endregion


            //额外
            if (selectindex != 3)
            {
                
                if (mainwindow.card_1_Extra.Children.Count < 1)
                {
                    view_or_set(cardview.card_1_Extra, mainwindow.card_1_Extra, cardview.card_1_Extra.Children.Count, "背面");
                }
                mainwindow.card_1_Extra.AllowDrop = true;
                
            }

            //除外
            if (selectindex != 4)
            {
                if (mainwindow.card_1_Outside.Children.Count < 1 && cardview.card_1_Outside.Children.Count > 0)
                {
                    view_or_set(cardview.card_1_Outside, mainwindow.card_1_Outside, cardview.card_1_Outside.Children.Count, "正面");
                }
                mainwindow.card_1_Outside.AllowDrop = true;
                
            }

            //墓地
            if (selectindex != 5)
            {
                if (mainwindow.card_1_Graveyard.Children.Count < 1 && cardview.card_1_Graveyard.Children.Count > 0)
                {
                    view_or_set(cardview.card_1_Graveyard, mainwindow.card_1_Graveyard, cardview.card_1_Graveyard.Children.Count, "正面");
                }
                mainwindow.card_1_Graveyard.AllowDrop = true;
            }

            
            //卡组
            if (selectindex != 6)
            {

                //当使用过查看卡组上方X张后的情况
                if (mainwindow.card_1_Deck.Children.Count > 0 && cardview.card_1_Deck.Children.Count > 0)
                {
                    CardUI card;
                    int x = cardview.card_1_Deck.Children.Count;
                    for (int i = 0; i < x; i++)
                    {
                        card = cardview.card_1_Deck.Children[x - i - 1] as CardUI;
                        cardview.card_1_Deck.Children.Remove(card);
                        mainwindow.card_1_Deck.Children.Add(card);
                        card_BackAtk(card);
                        card.SetValue(Canvas.TopProperty, (mainwindow.card_1_Deck.ActualHeight - card.ActualHeight) / 2.0);
                        card.SetValue(Canvas.LeftProperty, (mainwindow.card_1_Deck.ActualWidth - card.ActualWidth) / 2.0);
                    }
                }

                if (mainwindow.card_1_Deck.Children.Count < 1 && cardview.card_1_Deck.Children.Count > 0)
                {
                    view_or_set(cardview.card_1_Deck, mainwindow.card_1_Deck, cardview.card_1_Deck.Children.Count, "背面");
                }
                mainwindow.card_1_Deck.AllowDrop = true;
                //foreach (Card c in cardview.card_1_Deck.Children)
                //{
                //    c.Visibility = System.Windows.Visibility.Hidden;
                //}

            }
 
        }

        #endregion

        #region <-- 查看除外 -->

        /// <summary>
        /// 查看除外
        /// </summary>
        public static void view_Outside()
        {
            cardview = CardView.getInstance(mainwindow);
            view_or_set(mainwindow.card_1_Outside, cardview.card_1_Outside, mainwindow.card_1_Outside.Children.Count, "正面");
            sort_CardView(cardview.card_1_Outside, 10);
            mainwindow.view_Outside.IsEnabled = false;

            //原本是-1，改变会触发Tabchange事件
            cardview.tb_View.SelectedIndex = 4;
            cardview.Show();
            mainwindow.card_1_Outside.AllowDrop = false;
        }

        #endregion

        #region <-- 查看墓地 -->

        /// <summary>
        /// 查看墓地
        /// </summary>
        public static void view_Graveyard()
        {
            CardsViewWin csv = new CardsViewWin(mainwindow.card_1_Graveyard);
            csv.tb_title.Text = "己方墓地";
            Point p = mainwindow.card_1_8.PointToScreen(new Point(0, 0));
            csv.Top = p.Y - csv.Height;
            csv.Left = p.X - ((csv.Width - mainwindow.card_1_8.ActualWidth) / 2);
            csv.ShowDialog();

            //cardview = CardView.getInstance(mainwindow);
            //view_or_set(mainwindow.card_1_Graveyard, cardview.card_1_Graveyard, mainwindow.card_1_Graveyard.Children.Count, "正面");
            //sort_CardView(cardview.card_1_Graveyard, 10);
            //mainwindow.view_Graveyard.IsEnabled = false;

            ////原本是-1，改变会触发Tabchange事件
            //cardview.tb_View.SelectedIndex = 5;
            //cardview.Show();
            //mainwindow.card_1_Graveyard.AllowDrop = false;
        }

        #endregion

        #region <-- 查看卡组 -->

        

        public static void excuete_viewCards(object sender, ExecutedRoutedEventArgs e)
        {
            //CardUI card = e.OriginalSource as CardUI;
            //ModifyAtkOrDefWin mad = new ModifyAtkOrDefWin(card);
            //mad.Owner = Application.Current.MainWindow;
            //Point p = card.PointToScreen(new Point(0, 0));
            //mad.Top = p.Y - mad.Height;
            //mad.Left = p.X - ((mad.Width - card.ActualWidth) / 2);
            //mad.ShowDialog();
            MyCanvas mcv = sender as MyCanvas;
            if (mcv.Children.Count == 0)
            {
                return;
            }
            CardsViewWin csv = new CardsViewWin(mcv);
            
            Point p = mainwindow.card_1_8.PointToScreen(new Point(0, 0));
            csv.Top = p.Y - csv.Height;
            csv.Left = p.X - ((csv.Width - mainwindow.card_1_8.ActualWidth) / 2);
            mcv.AllowDrop = false;
            csv.ShowDialog();
            //csv


        }

        /// <summary>
        /// 查看卡组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void view_Main()
        {
            CardsViewWin csv = new CardsViewWin(mainwindow.card_1_Deck);
            csv.tb_title.Text = "己方卡组";
            Point p = mainwindow.card_1_8.PointToScreen(new Point(0, 0));
            csv.Top = p.Y - csv.Height;
            csv.Left = p.X - ((csv.Width - mainwindow.card_1_8.ActualWidth) / 2);
            csv.ShowDialog();

            //cardview = CardView.getInstance(mainwindow);
            //Console.WriteLine(mainwindow.card_1_Deck.Children.Count);
            //view_or_set(mainwindow.card_1_Deck, cardview.card_1_Deck,mainwindow.card_1_Deck.Children.Count,"正面");
            ////foreach (Card  cd in cardview.card_1_Deck.Children)
            ////{
            ////    Console.WriteLine(cd.isBack);
            ////}
            //sort_CardView(cardview.card_1_Deck,10);
            //mainwindow.view_Deck.IsEnabled = false;

            ////原本是-1，改变会触发Tabchange事件
            //cardview.tb_View.SelectedIndex = 6;
            //cardview.Show();
            //mainwindow.card_1_Deck.AllowDrop = false;
            ////Console.WriteLine("查看卡组");
            ////mainwindow.report.Text += ("查看 <卡组>" + Environment.NewLine);

        }

        #endregion

        #region <-- 查看额外 -->

        /// <summary>
        /// 查看额外
        /// </summary>
        public static void view_Extra_Click()
        {
            CardsViewWin csv = new CardsViewWin(mainwindow.card_1_Extra);
            csv.tb_title.Text = "己方额外";
            Point p = mainwindow.card_1_8.PointToScreen(new Point(0, 0));
            csv.Top = p.Y - csv.Height;
            csv.Left = p.X - ((csv.Width - mainwindow.card_1_8.ActualWidth) / 2);
            csv.ShowDialog();

            //cardview = CardView.getInstance(mainwindow);
            //view_or_set(mainwindow.card_1_Extra, cardview.card_1_Extra, mainwindow.card_1_Extra.Children.Count, "正面");
            //sort_CardView(cardview.card_1_Extra,10);
            //cardview.tb_View.SelectedIndex = 3;
            ////cardview.tb_View.UpdateLayout();

            //cardview.Show();
            //mainwindow.card_1_Extra.AllowDrop = false;

            ////Console.WriteLine("查看卡组");
            ////mainwindow.report.Text += ("查看 <额外>" + Environment.NewLine);

        }

        #endregion

        #region <-- 查看对方除外 -->

        public static void view_Outside2()
        {
            cardview = CardView.getInstance(mainwindow);
            view_or_set(mainwindow.card_2_Outside, cardview.card_2_Outside, mainwindow.card_2_Outside.Children.Count, "正面");
            sort_CardView(cardview.card_2_Outside, 10);
            mainwindow.view_Outside2.IsEnabled = false;

            //原本是-1，改变会触发Tabchange事件
            cardview.tb_View.SelectedIndex = 1;
            cardview.Show();
            mainwindow.card_1_Outside.AllowDrop = false;
        }

        #endregion

        #region  <-- 查看对方墓地 -->

        public static void view_Graveyard2()
        {
            cardview = CardView.getInstance(mainwindow);
            view_or_set(mainwindow.card_2_Graveyard, cardview.card_2_Graveyard, mainwindow.card_2_Graveyard.Children.Count, "正面");
            sort_CardView(cardview.card_2_Graveyard, 10);
            mainwindow.view_Graveyard2.IsEnabled = false;

            //原本是-1，改变会触发Tabchange事件
            cardview.tb_View.SelectedIndex = 0;
            cardview.Show();
            mainwindow.card_1_Outside.AllowDrop = false;
        }

        #endregion

        #region <-- 查看对方额外 -->

        /// <summary>
        /// 查看对方额外
        /// </summary>
        public static void view_Extra2()
        {
            cardview = CardView.getInstance(mainwindow);

            //Card card;
            //int num = mainwindow.card_2_Extra.Children.Count;
            //for (int i = 0; i < num; i++)
            //{
            //    card = mainwindow.card_2_Extra.Children[i] as Card;
            //    if (card.CardDType.Contains("灵摆"))
            //    {
            //        Base.getawayParerent(card);
            //        cardview.card_2_Extra.Children.Insert(0, card);
            //        if (card.isBack)
            //        {
            //            card.isBack = false;
            //            card.SetPic();
            //        }
            //        i--;
            //        num--;
            //    }
                
                              
            //}
            //sort_CardView(cardview.card_2_Extra, 10);
            //cardview.tb_View.SelectedIndex = 2;
            //cardview.Show();
        }

        #endregion

        #region <-- 查看卡组上方X张 -->

        /// <summary>
        /// 查看卡组上方X张
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_1_Deckviewx_Click(object sender, RoutedEventArgs e)
        {
            cardview = CardView.getInstance(mainwindow);

            Card card;

            if( mainwindow.card_1_Deck.Children.Count > 0 && cardview.card_1_Deck.Children.Count > 0)
            {
                int x = cardview.card_1_Deck.Children.Count;
                for (int i = 0; i < x; i++)
                {
                    card = cardview.card_1_Deck.Children[x - i - 1] as Card;
                    cardview.card_1_Deck.Children.Remove(card);
                    mainwindow.card_1_Deck.Children.Add(card);
                }
            }

            //获得要查看的卡片数量
            int y = Convert.ToInt32( cardview.card_1_DeckNum.Text);

            //Card card;

            for (int i = 0; i < y; i++)
            {
                card = mainwindow.card_1_Deck.Children[mainwindow.card_1_Deck.Children.Count - y + i] as Card;
                mainwindow.card_1_Deck.Children.Remove(card);
                cardview.card_1_Deck.Children.Insert(0,card);
                //card_FrontAtk(card);
            }


            //view_or_set(mainwindow.card_1_Deck, cardview.card_1_Deck, x, "正面");

            //整理卡组
            sort_CardView(cardview.card_1_Deck,10);

            
        }

        #endregion

        #region <-- 卡片在控件中的转移 -->

        /// <summary>
        /// 把卡片从一个控件中放到另外的控件中
        /// </summary>
        /// <param name="cv">原控件</param>
        /// <param name="cv2">目标控件</param>
        /// <param name="num">要转换的卡片数</param>
        /// <param name="isback">是否背面转换？</param>
        public static void view_or_set(Canvas cv, Canvas cv2, int num, string isback)
        {
            CardUI temp;
            for (int i = 0; i < num; i++)
            {
                temp = cv.Children[0] as CardUI;
                cv.Children.Remove(temp);
                cv2.Children.Insert(0, temp);
                //cv2.Children.Insert(0, temp);
                if (cv2.Equals(mainwindow.card_1_Deck) || cv2.Equals(mainwindow.card_1_Extra) || cv2.Equals(mainwindow.card_2_Extra))
                {
                    card_BackAtk(temp);
                }
                else
                {
                    card_FrontAtk(temp);
                }
                //if (isback.Equals("背面")) card_BackAtk(temp);
                //if (isback.Equals("正面")) card_FrontAtk(temp);
                //if (!(cv.Parent is System.Windows.Controls.Grid)) CardSortsingle(cv, temp, 56, 81);
                if (cv_others_1.Contains(cv2) || cv_others_2.Contains(cv2))
                {
                    Canvas.SetTop(temp, (cv2.ActualHeight - temp.ActualHeight) / 2.0);
                    Canvas.SetLeft(temp, (cv2.ActualWidth - temp.ActualWidth) / 2.0);

                }
                //sort_SingleCard(temp);
                //if (cv2.Name.Contains("Deck")) temp.ContextMenu = CardMenu.cm_deck;
                //if (cv2.Name.Contains("hand")) temp.ContextMenu = AllMenu.cm_hand;
                //else if (cv2.Name.Contains("Graveyard")) temp.ContextMenu = AllMenu.cm_graveyard;
                //else if (cv2.Name.Contains("Outside")) temp.ContextMenu = AllMenu.cm_outside;
                //else { temp.ContextMenu = null; }
            }
        }

        #endregion

        #region <-- 抽X张卡片 -->

        /// <summary>
        /// 抽X张卡片
        /// </summary>
        /// <param name="draw_num">抽取数量</param>
        /// <param name="time">起手动画时间（毫秒）</param>
        public static List<CardUI> card_Draw(int draw_num ,double time)
        {

            //view_or_set(mainwindow.card_1_Deck, mainwindow.card_hand, num,"正面");

            //Card temp;
            //for (int i = 0; i < num; i++)
            //{
            //    temp = mainwindow.card_1_Deck.Children[mainwindow.card_1_Deck.Children.Count - 1] as Card;
            //    mainwindow.card_1_Deck.Children.Remove(temp);
            //    mainwindow.card_1_hand.Children.Add(temp);
            //    card_FrontAtk(temp);
            //    temp.ContextMenu = AllMenu.cm_hand;
            //}
            ////sort_HandCard("1");

            MyCanvas cv = mainwindow.card_1_Deck;
            MyCanvas cv_aim = mainwindow.card_1_hand;
            cv_aim.WhenAddChildren -= CardAreaEvent.add2Hand;
            List<CardUI> cards = new List<CardUI>();

            if (!(cv.Children.Count < draw_num))
            {
                

                TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();
                //List<FrameworkElement> cards = new List<FrameworkElement>();
                for (int i = 0; i < draw_num; i++)
                {
                    CardUI card_draw = cv.Children[cv.Children.Count - 1] as CardUI;

                    card_draw.ContextMenu = AllMenu.Instance.cm_hand;
                    //CardAnimation.setTransformGroup(card_draw);
                    //1.获取卡片相对于目的地的距离
                    Point start = card_draw.TranslatePoint(new Point(), cv_aim);
                    //2.获取卡片在卡框中的相对距离
                    //Card card_handlast = cv_aim.Children[cv_aim.Children.Count - 1] as Card;
                    Point end;
                    if (cv_aim.Children.Count == 0 || draw_num > 1)
                    {
                        end = new Point(((cv_aim.ActualWidth - card_draw.ActualWidth) / 2), ((cv_aim.ActualHeight - card_draw.ActualHeight) / 2));
                    }
                    else
                    {
                        Card card_hand_R1 = cv_aim.Children[cv_aim.Children.Count - 1] as Card;
                        end = card_hand_R1.TranslatePoint(new Point(), cv_aim);
                    }

                    //脱离原控件
                    card_draw.getAwayFromParents();
                    //利用1设置初始位置
                    Canvas.SetTop(card_draw, start.Y);
                    Canvas.SetLeft(card_draw, start.X);
                    //加入目的地控件
                    cv_aim.Children.Add(card_draw);

                    MyStoryboard msb = CardAnimation.CanvasXY(start, end, time);
                    msb.card = card_draw;
                    msb.Completed += (object c, EventArgs d) =>
                    {

                        msb.card.BeginAnimation(Canvas.LeftProperty, null);
                        msb.card.BeginAnimation(Canvas.TopProperty, null);

                        Canvas.SetTop(msb.card, end.Y);
                        Canvas.SetLeft(msb.card, end.X);

                    };
                    //CardOperate.sort_HandCard("2");
                    animator.Animates.Add(msb);
                    cards.Add(card_draw);
                    //msb.Begin(card_main);
                }
                animator.Animates[animator.Animates.Count - 1].Completed += (object c, EventArgs d) =>
                {
                    CardOperate.sort_HandCard(cv_aim);
                    foreach (CardUI card in cards)
                    {
                        card.set2FrontAtk2();
                        CardAnimation.turn(card);

                    }
                    cv_aim.WhenAddChildren += new CollectionChangeDelegate(CardAreaEvent.add2Hand);

                };
                animator.Begin(cards.ToList<FrameworkElement>());

                //mainwindow.report.Text += ("从 " + DuelReportOperate.from("card_1_Deck") + " 抽<" + num + ">张卡" + Environment.NewLine);

                return cards;
            }

            return null;


            

        }

        #endregion <-- 抽X张卡片 -->

        #region <-- 关闭卡片预览区 -->

        /// <summary>
        /// 关闭卡片预览区
        /// </summary>
        public static void cardview_close()
        {
            cardview = CardView.getInstance(mainwindow);
            if (mainwindow.card_1_Deck.Children.Count > 0 && cardview.card_1_Deck.Children.Count > 0)
            {
                CardUI card;
                int x = cardview.card_1_Deck.Children.Count;
                for (int i = 0; i < x; i++)
                {
                    card = cardview.card_1_Deck.Children[x - i - 1] as CardUI;
                    cardview.card_1_Deck.Children.Remove(card);
                    mainwindow.card_1_Deck.Children.Add(card);
                    card.SetValue(Canvas.TopProperty, (mainwindow.card_1_Deck.ActualHeight - card.ActualHeight) / 2.0);
                    card.SetValue(Canvas.LeftProperty, (mainwindow.card_1_Deck.ActualWidth - card.ActualWidth) / 2.0);
                   // Canvas.SetBottom(card, (mainwindow.card_1_Deck.ActualHeight - card.ActualHeight) / 2.0);
                    //Canvas.SetLeft(card, );
                    //CardOperate.sort_SingleCard(card);
                    CardOperate.card_BackAtk(card);

                }
            }
            mainwindow.view_Deck.IsEnabled = true;
            mainwindow.view_Graveyard.IsEnabled = true;
            mainwindow.view_Outside.IsEnabled = true;
            mainwindow.view_Outside2.IsEnabled = true;
            mainwindow.view_Graveyard2.IsEnabled = true;
            //cardview.tb_View.SelectedIndex = -1;
            //Thread.Sleep(100);
            //cardview.Hide();
            cardview.Close();
            //DuelOperate.getInstance().sendMsg("None=", "关闭<卡片预览区>");

            //mainwindow.report.Text += ("查看结束" + Environment.NewLine);
            //Console.WriteLine(this.tb_View.SelectedIndex);
        }


        #endregion

        #region <-- 卡片状态设置，表攻，里攻，里防 -->

        /// <summary>
        /// 表侧攻击表示
        /// </summary>
        /// <param name="card">要修改的卡片</param>
        public static void card_FrontAtk(CardUI card)
        {
            //RotateTransform rotateTransform = new RotateTransform(0);
            //card.RenderTransform = rotateTransform;  
            //card.isDef = false;
            //card.isBack = false;
            //card.SetPic();
            //card.set2FrontAtk();
            //card.showImg();
            //CardAnimation.setTransformGroup(card);
 
        }

        /// <summary>
        /// 里侧攻击表示
        /// </summary>
        /// <param name="card">要修改的卡片</param>
        public static void card_BackAtk(CardUI card)
        {
            //card.Visibility = Visibility.Visible;
            //RotateTransform rotateTransform = new RotateTransform(0);
            //card.RenderTransform = rotateTransform;
            //card.set2BackAtk();
            //card.showImg();
            

        }

        /// <summary>
        /// 里侧防守表示
        /// </summary>
        /// <param name="card"></param>
        public static void card_BackDef(CardUI card)
        {
            card.Visibility = Visibility.Visible;
            RotateTransform rotateTransform = new RotateTransform(-90);
            card.RenderTransform = rotateTransform;
            card.set2BackDef();

        }


        #endregion

        #region <-- 卡区整理（相同靠一起） -->

        /// <summary>
        /// 把放入的卡片与相同的放在一起
        /// </summary>
        /// <param name="card">卡片</param>
        /// <param name="cv_aim">目标控件</param>
        public static void card_ExtraSort(Card card,Canvas cv_aim)
        {
            int num = cv_aim.Children.Count;
            for (int i = 0; card.Parent == null && i < num; i++)
            {
                string same = (cv_aim.Children[i] as Card).name;
                if (card.name.Equals(same))
                {
                    cv_aim.Children.Capacity = num + 1;
                    //cv_aim.Children.RemoveAt(i);

                    List<Card> temp = new List<Card>();
                    for (int j = i; j < num; j++)
                    {

                        Card card2 = cv_aim.Children[i] as Card;
                        cv_aim.Children.RemoveAt(i);
                        temp.Add(card2);
                        card2 = null;
                        //cv_aim.Children.Insert(num, card2);
                    }
                    cv_aim.Children.Insert(i, card);
                    for (int k = 0; k < temp.Count; k++)
                    {

                        cv_aim.Children.Add(temp[k]);
                    }

                    temp = null;
                }
                
            }
            if (card.Parent == null)
            {
                cv_aim.Children.Add(card);
            }
            sort_CardView(cardview.card_1_Extra,10);

        }

        #endregion

        #region <-- 调整卡片位置，整理卡区 -->

        #region 自适应：墓地，额外，魔陷，场地，P卡位，卡组

        //public static void sort_centerInParents

        #endregion

        #region <-- 卡片位置自适应 -->
        public static void sort(Canvas cv,CardUI card)
        {
            FrameworkElement cv_par = cv.Parent as FrameworkElement;
            if (cv_par.Parent is System.Windows.Controls.TabItem)
            {
                sort_CardView(cv,10);
            }
            else 
            {
                if (cv.Name.Equals("card_1_hand")) { sort_HandCard(mainwindow.card_1_hand); return; }
                if (cv.Name.Equals("card_2_hand")) { sort_HandCard(mainwindow.card_2_hand); return; }

                if (cv.Name.Equals("cv_main")) { sort_main(cv); return; }
                if (cv.Name.Equals("cv_extra")) { sort_extra(cv); return; }
                if (cv.Name.Equals("cv_side")) { sort_extra(cv); return; }

                if (cv_monsters_1.Contains(cv) || cv_monsters_2.Contains(cv))
                {
                    sort_Canvas(cv);
                }
                else
                {
                    if (card != null)
                    {
                        sort_SingleCard(card);
                    }
                    else
                    {
                        sort_SingleCard(cv); 
                    }                 
                    return;
                }
             
                
                
            }
        }

        #endregion

        /// <summary>
        /// 卡区整理（预览区，额外，卡组，墓地，除外，素材
        /// </summary>
        /// <param name="cv"></param>
        /// <param name="perrow"></param>
        public static void sort_CardView(Canvas cv, double perrow)
        {
            for (int i = 0; i < Math.Ceiling(cv.Children.Count / perrow); i++)
            {
                //for (int j = 0; j < ((i == Math.Ceiling(cv.Children.Count / 9.0) - 1) ? cv.Children.Count % 9 : 9); j++)
                for (int j = 0; perrow * i + j < cv.Children.Count; j++)
                {
                    CardUI card = cv.Children[(int)perrow * i + j] as CardUI;
                    Canvas.SetLeft(card, 5 * (j + 1) + 56 * j);
                    Canvas.SetTop(card, 5 * (i + 1) + 81 * i);
                }
            }
        }

        /// <summary>
        /// Canvas卡区整理
        /// </summary>
        /// <param name="cv"></param>
        public static void sort_Canvas(Canvas cv)
        {
            int hn = cv.Children.Count;

            if (hn == 0) return;

            if (hn >1)
            {
                CardUI top = cv.Children[hn - 1] as CardUI;
                if (top.info.sCardType.Equals("XYZ怪兽") && (top.Status == Status.BACK_DEF || top.Status == Status.FRONT_DEF))
                {
                    sort_XYZ_def(cv);
                }
                else
                {
                    sort_XYZ_atk(cv);
                }
            }
            else
            {
                sort_SingleCard(cv);
                //sort_XYZ_atk(cv);
            }
        }

        /// <summary>
        /// 卡区整理-1
        /// </summary>
        /// <param name="cv"></param>
        public static void sort_XYZ_atk(Canvas cv)
        {
            double card_height = 81;
            double card_width = 56;
            

            //获得控件中的卡片数量
            int hn = cv.Children.Count;

            if (hn == 0) return;

            
            //计算卡片数于控件中的平均距离
            double average = cv.ActualWidth / hn;

            //计算卡片的上下距离
            double average2 = (cv.ActualHeight - card_height) / 2 + 1;

            int i = 1;

            //重置卡片距离
            if (average < card_width || average == card_width) //空间不足或刚好
            {


                foreach (CardUI card in cv.Children)
                {
                    //CardAnimation.setTransformGroup(card);
                    //1.获取卡片相对于目的地的距离
                    Point start = card.TranslatePoint(new Point(), cv);
                    //2.获取卡片在卡框中的相对距离
                    //Card card_handlast = cv_aim.Children[cv_aim.Children.Count - 1] as Card;
                    double endX = (card_width - ((card_width * hn - cv.ActualWidth) / (hn - 1))) * (i - 1);
                    Point end = new Point(endX,average2);
                    //Canvas.SetTop(card, average2);
                    //Canvas.SetLeft(card, endX);
                    MyStoryboard msb = CardAnimation.CanvasXY(end);
                    msb.card = card;
                    msb.Completed += (object c, EventArgs d) =>
                    {
                        msb.card.BeginAnimation(Canvas.LeftProperty, null);
                        msb.card.BeginAnimation(Canvas.TopProperty, null);

                        Canvas.SetTop(msb.card, end.Y);
                        Canvas.SetLeft(msb.card, end.X);
                    };
                    msb.Begin(card);
                    i++;
                }
            }
            else
            {
                CardUI card = cv.Children[0] as CardUI;
                Point start = card.TranslatePoint(new Point(), cv);
                double endX = (cv.ActualHeight - card.Width)/2;
                Point end = new Point(endX, average2);
                MyStoryboard msb = CardAnimation.CanvasXY(end);
                msb.card = card;
                msb.Completed += (object c, EventArgs d) =>
                {
                    msb.card.BeginAnimation(Canvas.LeftProperty, null);
                    msb.card.BeginAnimation(Canvas.TopProperty, null);

                    Canvas.SetTop(msb.card, end.Y);
                    Canvas.SetLeft(msb.card, end.X);
                };
                msb.Begin(card);
            }



        }
     

        /// <summary>
        /// 卡区整理-2
        /// </summary>
        /// <param name="cv">要整理的Canvas</param>
        public static void sort_XYZ_def(Canvas cv)
        {
            double card_height = 81;
            double card_width = 56;

            //获得控件中的卡片数量
            int card_num = cv.Children.Count;

            if (card_num < 1) return;

            //获取上侧距离
            double canvas_top = (cv.ActualHeight - card_height) / 2.0;

            //单卡时左侧距离
            double canvas_left_single = (cv.ActualWidth - card_width) / 2.0;
            

            CardUI card_top = cv.Children[card_num - 1] as CardUI;

            
            if (card_num == 1 )
            {
                /* 1.当卡片数量为1时，也就是从两张取出剩下一张的情况
                 * 2.当且仅当剩余的一张为攻击形式时才需要移动
                 */
                if (card_top.Status == Status.BACK_ATK || card_top.Status == Status.FRONT_ATK)
                {
                    Point end = new Point(canvas_left_single, canvas_top);
                    MyStoryboard msb = CardAnimation.CanvasXY(end);
                    msb.card = card_top;
                    msb.Completed += (object c, EventArgs d) =>
                    {
                        msb.card.BeginAnimation(Canvas.LeftProperty, null);
                        msb.card.BeginAnimation(Canvas.TopProperty, null);

                        Canvas.SetTop(msb.card, canvas_top);
                        Canvas.SetLeft(msb.card, canvas_left_single);
                    };
                    msb.Begin(card_top);
                }          
                
            }
            else if (card_num > 1)
            {
                /* 
                 * 1.卡片数量两张以上
                 * 2.当第一张为防守时，不处理第一张卡片
                 * 3.但应考虑第一张由攻转防的情况，这种情况要单独处理第一张
                 * 4.并把要处理的卡片数量 - 1
                 */
                if (card_top.Status == Status.BACK_DEF || card_top.Status == Status.FRONT_DEF)
                {
                    if (Canvas.GetLeft(card_top) != canvas_left_single || Canvas.GetTop(card_top) != canvas_top)
                    {
                        Point end = new Point(canvas_left_single, canvas_top);
                        MyStoryboard msb = CardAnimation.CanvasXY(end);
                        msb.card = card_top;
                        msb.Completed += (object c, EventArgs d) =>
                        {
                            msb.card.BeginAnimation(Canvas.LeftProperty, null);
                            msb.card.BeginAnimation(Canvas.TopProperty, null);

                            Canvas.SetTop(msb.card, canvas_top);
                            Canvas.SetLeft(msb.card, canvas_left_single);
                        };
                        msb.Begin(card_top);
                    }    

                    card_num -= 1;
                }

                /* 
                 * 1.当需要批量处理的卡片仅为一张时，单独进行处理 
                 */
                if (card_num == 1)
                {
                    CardUI card_top2 = cv.Children[card_num - 1] as CardUI;
                    Point end = new Point(canvas_left_single, canvas_top);
                    MyStoryboard msb = CardAnimation.CanvasXY(end);
                    msb.card = card_top2;
                    msb.Completed += (object c, EventArgs d) =>
                    {
                        msb.card.BeginAnimation(Canvas.LeftProperty, null);
                        msb.card.BeginAnimation(Canvas.TopProperty, null);

                        Canvas.SetTop(msb.card, canvas_top);
                        Canvas.SetLeft(msb.card, canvas_left_single);
                    };
                    msb.Begin(card_top2);
                }
                else
                {
                    for (int i = 0; i < card_num; i++)
                    {
                        CardUI card = cv.Children[i] as CardUI;
                        Point start = card.TranslatePoint(new Point(), cv);
                        //2.获取卡片在卡框中的相对距离
                        //Card card_handlast = cv_aim.Children[cv_aim.Children.Count - 1] as Card;
                        double endX = (cv.ActualWidth - card_width) / (card_num - 1) * i;
                        Point end = new Point(endX, canvas_top);

                        //Canvas.SetTop(card, average2);
                        //Canvas.SetLeft(card, endX);
                        MyStoryboard msb = CardAnimation.CanvasXY(end);
                        msb.card = card;
                        msb.Completed += (object c, EventArgs d) =>
                        {
                            msb.card.BeginAnimation(Canvas.LeftProperty, null);
                            msb.card.BeginAnimation(Canvas.TopProperty, null);

                            Canvas.SetTop(msb.card, end.Y);
                            Canvas.SetLeft(msb.card, end.X);
                        };
                        msb.Begin(card);

                    }
                }

                

            }

        }

        /// <summary>
        /// 卡区整理-3
        /// </summary>
        /// <param name="cv"></param>
        public static void sort_XYZ_def2(Canvas cv)
        {
            

            double card_height = 81;
            double card_width = 56;

            //获得控件中的卡片数量
            double hn = cv.Children.Count - 1;

            

            //计算卡片的上下距离
            double average2 = (cv.ActualHeight - card_height) / 2.0;

            if (hn == 1)
            {
                double endX = (cv.ActualWidth - card_width) / 2.0;
                Point end = new Point(endX, average2);
                CardUI card = cv.Children[0] as CardUI;
                MyStoryboard msb = CardAnimation.CanvasXY(end);
                msb.Completed += (object c, EventArgs d) =>
                {
                    card.BeginAnimation(Canvas.LeftProperty, null);
                    card.BeginAnimation(Canvas.TopProperty, null);

                    Canvas.SetTop(card, end.Y);
                    Canvas.SetLeft(card, end.X);
                };
                msb.Begin(card);
            }
            else
            {
                //计算卡片数于控件中的平均距离
                double average = cv.ActualWidth / hn;



                //int i = 1;

                //重置卡片距离

                for (int i = 0; i < hn; i++)
                {
                    CardUI card = cv.Children[i] as CardUI;
                    Point start = card.TranslatePoint(new Point(), cv);
                    //2.获取卡片在卡框中的相对距离
                    //Card card_handlast = cv_aim.Children[cv_aim.Children.Count - 1] as Card;
                    double endX = (card_width - ((card_width * hn - cv.ActualWidth) / (hn - 1))) * i;
                    Point end = new Point(endX, average2);
                    if (i == hn)
                    {
                        end.X = (cv.ActualWidth - card_width) / 2.0;
                        end.Y = average2;
                    }
                    //Canvas.SetTop(card, average2);
                    //Canvas.SetLeft(card, endX);
                    MyStoryboard msb = CardAnimation.CanvasXY(end);
                    msb.card = card;
                    msb.Completed += (object c, EventArgs d) =>
                    {
                        msb.card.BeginAnimation(Canvas.LeftProperty, null);
                        msb.card.BeginAnimation(Canvas.TopProperty, null);

                        Canvas.SetTop(msb.card, end.Y);
                        Canvas.SetLeft(msb.card, end.X);
                    };
                    msb.Begin(card);

                }
            }
            //if (hn < 1) return;

            

        }

        ///<summary>
        /// 整理手卡
        /// </summary>
        /// <param name="field"></param>
        public static void sort_HandCard(Canvas cv)
        {
            //Canvas cv = mainwindow.card_1_hand;
            //if (field.Equals("2"))
            //{
            //    cv = mainwindow.card_2_hand;
            //}
            

            double card_height = 81;
            double card_width = 56;
            double cards_range = 2;

            //获得控件中的卡片数量
            int hn = cv.Children.Count;

            if (hn == 0) return;


            //计算卡片数于控件中的平均距离
            double average = cv.ActualWidth / hn;

            //计算卡片的上下距离
            double average2 = (cv.ActualHeight - card_height) / 2 + 1;

            int i = 1;

            //重置卡片距离
            if (average > card_width) //空间足够
            {
                double range;
                //range = (average - card_width) / 2;

                //第一张和最后一张左右距离
                range = (cv.ActualWidth - (card_width * hn + cards_range * (hn - 1))) / 2;

                
                foreach (CardUI card in cv.Children)
                {
                    //设置上下距离
                    //Canvas.SetTop(card, average2);

                   // Point start = new Point(Canvas.GetLeft(card), Canvas.GetTop(card));
                    //Point start = new Point(Canvas.GetLeft(card),average2);
                    Point end = new Point();
                    
                    if (i == 1)
                    {
                        end = new Point(range,average2);
                        //Canvas.SetLeft(card, range);
                    }
                    else if (i > 1 || i < hn + 1)
                    {
                        double end_ = range + card_width * (i - 1) + (cards_range * (i - 1));
                        end = new Point(end_, average2);
                        //Canvas.SetLeft(card, end);
                    }
                    MyStoryboard msb = CardAnimation.CanvasXY(end);
                    msb.card = card;
                    msb.Completed += (object c, EventArgs d) =>
                    {

                        card.BeginAnimation(Canvas.LeftProperty, null);
                        card.BeginAnimation(Canvas.TopProperty, null);

                        Canvas.SetTop(msb.card, end.Y);
                        Canvas.SetLeft(msb.card, end.X);

                    };
                    msb.Begin(card);
                   // Animations.Add(card, msb);
                    //tls.Animates.Add(msb);
                    //move.Add(card);
                    i++;
                }
                
                //tls.Begin(move);

            }
            else if (average < card_width || average == card_width) //空间不足或刚好
            {
                

                foreach (CardUI card in cv.Children)
                {
                    Point start = new Point(Canvas.GetLeft(card), average2);
                    double end_ = (card_width - ((card_width * hn - cv.ActualWidth) / (hn - 1))) * (i - 1);
                    Point end = new Point(end_, average2);
                
                    //Canvas.SetTop(card, average2);
                    //Canvas.SetLeft(card, end_);

                    MyStoryboard msb = CardAnimation.CanvasXY(start, end, 200);
                    msb.Completed += (object c, EventArgs d) =>
                    {

                        card.BeginAnimation(Canvas.LeftProperty, null);
                        card.BeginAnimation(Canvas.TopProperty, null);

                        Canvas.SetTop(card, end.Y);
                        Canvas.SetLeft(card, end.X);

                    };
                    msb.Begin(card);
                    i++;
                }
            }
            

        }

        /// <summary>
        /// 卡区单卡居中
        /// </summary>
        /// <param name="cv"></param>
        public static void sort_SingleCard(Canvas cv)
        {
            if (cv.Children.Count < 1)
            {
                return;
            }
            Card card = cv.Children[0] as Card;
            if (card != null)
            {
                double top = card.ActualHeight == double.NaN ? (cv.ActualHeight - card.Height) / 2.0 : (cv.ActualHeight - card.ActualHeight) / 2.0;
                double left = card.ActualWidth == double.NaN ? (cv.ActualWidth - card.Width) / 2.0 : (cv.ActualWidth - card.ActualWidth) / 2.0;

                Point start = new Point(Canvas.GetLeft(card), top);

                Point end = new Point(left, top);

                Canvas.SetTop(card, top);
                Canvas.SetLeft(card, left);

                MyStoryboard msb = CardAnimation.CanvasXY(start, end, 200);
                msb.Completed += (object c, EventArgs d) =>
                {

                    card.BeginAnimation(Canvas.LeftProperty, null);
                    card.BeginAnimation(Canvas.TopProperty, null);

                    Canvas.SetTop(card, end.Y);
                    Canvas.SetLeft(card, end.X);

                };
                msb.Begin(card);
            }
            
        }

        /// <summary>
        /// 单卡居中
        /// </summary>
        /// <param name="card"></param>
        public static void sort_SingleCard(CardUI card)
        {
            Canvas cv = Base.getParerent(card);
            if (cv != null)
            {
                double top = card.ActualHeight == double.NaN || card.ActualHeight == 0 ? (cv.ActualHeight - card.Height) / 2.0 : (cv.ActualHeight - card.ActualHeight) / 2.0;
                double left = card.ActualWidth == double.NaN || card.ActualHeight == 0 ? (cv.ActualWidth - card.Width) / 2.0 : (cv.ActualWidth - card.ActualWidth) / 2.0;
                Canvas.SetTop(card, top);
                Canvas.SetLeft(card, left);

                Point start = new Point(Canvas.GetLeft(card), top);
                
                Point end = new Point(left, top);

                Canvas.SetTop(card, top);
                Canvas.SetLeft(card, left);

                MyStoryboard msb = CardAnimation.CanvasXY(start, end, 200);
                msb.Completed += (object c, EventArgs d) =>
                {

                    card.BeginAnimation(Canvas.LeftProperty, null);
                    card.BeginAnimation(Canvas.TopProperty, null);

                    Canvas.SetTop(card, end.Y);
                    Canvas.SetLeft(card, end.X);

                };
                msb.Begin(card);
            }
            
        }

        /// <summary>
        /// 整理卡组管理区
        /// </summary>
        /// <param name="cv"></param>
        public static void sort_main(Canvas cv)
        {
            int num = cv.Children.Count;
            int col_num = 10 + (int) Math.Ceiling((num-40)/4.0);
            int col_lastnum = col_num;
            double range = (cv.ActualHeight - 81 * 4) / 5.0;

            for (int i = 0; i < 4; i++)
            {
                if (i==3)
                {
                    col_lastnum = num - col_num * 3;
                }
                for (int j = 0; j < col_lastnum; j++)
                {
                    CardUI card = cv.Children[i * col_num + j] as CardUI;
                    card.set2FrontAtk();
                    card.showImg();
                    card.SetValue(Canvas.TopProperty, card.Height * i + range * (i + 1));
                    card.SetValue(Canvas.LeftProperty, (card.Width - ((card.Width * col_num - cv.ActualWidth) / (col_num - 1))) * j);
                    //Canvas.SetTop(card, );
                    //Canvas.SetLeft(card, );
                }

            }
            
            
        }

        public static void sort_extra(Canvas cv)
        {
            int num = cv.Children.Count;
            if (num < 11)
            {
                
                for (int i = 0; i < num; i++)
                {
                    CardUI card = cv.Children[i] as CardUI;
                    card.set2FrontAtk();
                    card.showImg();
                    Canvas.SetTop(card, (cv.ActualHeight - 81) / 2);
                    Canvas.SetLeft(card, (card.Width - ((card.Width * 11 - cv.Width) / (11 - 1))) * i);
                }
            }
            else
            {
                for (int i = 0; i < num; i++)
                {
                    CardUI card = cv.Children[i] as CardUI;
                    card.set2FrontAtk();
                    card.showImg();
                    Canvas.SetTop(card, (cv.ActualHeight - 81)/2);
                    Canvas.SetLeft(card, (card.Width - ((card.Width * num - cv.ActualWidth) / (num - 1))) * i);
                }
            }
        }

        #endregion

        public static void setAtkOrDef(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                TextBlock tb = sender as TextBlock;
                if (tb.Text.Equals("?")||String.IsNullOrEmpty(tb.Text))
                {
                    return;
                }
                CardUI card = tb.GetBindingExpression(TextBlock.TextProperty).ParentBinding.Source as CardUI;
                ModifyAtkOrDefWin mad = new ModifyAtkOrDefWin(card);
                mad.Owner = Application.Current.MainWindow;
                Point p = tb.PointToScreen(new Point(0, 0));
                mad.Top = p.Y + tb.Height;
                mad.Left = p.X + (tb.ActualWidth / 2) - mad.Width /2;
                mad.ShowDialog();

                #region 指令发送

                CardMessage cardMessage = new CardMessage();
                int cardid = DuelOperate.getInstance().myself.deck.Main.IndexOf(card);
                cardMessage.cardID = cardid;
                cardMessage.curAtk = card.curAtk;
                cardMessage.curDef = card.curDef;
                cardMessage.remark = card.ToolTip == null ? null : card.ToolTip.ToString() ;
                String contentJson = JsonConvert.SerializeObject(cardMessage);

                BaseJson bj = new BaseJson();
                bj.guid = DuelOperate.getInstance().myself.userindex;
                bj.cid = "";
                bj.action = ActionCommand.CARD_MESSAGE;
                bj.json = contentJson;
                String json = JsonConvert.SerializeObject(bj);
                DuelOperate.getInstance().sendMsg(json);

                #endregion
                //MessageBox.Show(tb.Name);
            }
        }



    }
}
