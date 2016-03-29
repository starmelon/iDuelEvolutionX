using iDuel_EvolutionX;
using iDuel_EvolutionX.Model;
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

namespace iDuel_EvolutionX.Service
{
    class MenuItemOperate
    {
        public static MainWindow mainwindow;
        public static CardView cardview;


        /// <summary>
        /// 修改卡片备注
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void execute_setCardRemark(object sender, ExecutedRoutedEventArgs e)
        {
            CardControl card = e.OriginalSource as CardControl;
            EditRemark er = new EditRemark();
            er.sendResult += (result) => {
                card.ToolTip = result;
            };
            er.Owner = Application.Current.MainWindow;
            Point p = card.PointToScreen(new Point(0, 0));
            er.Top = p.Y - er.Height;
            er.Left = p.X - ((er.Width - card.ActualWidth) / 2);
            er.ShowDialog();
        }

        /// <summary>
        /// 卡片菜单：攻守处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void excuete_set2AtkOrDef(object sender, ExecutedRoutedEventArgs e)
        {
            CardControl card = e.OriginalSource as CardControl;

            switch (card.Status)
            {
                case Status.FRONT_ATK:
                    CardAnimation.Rotate2FrontDef(card);
                    break;
                case Status.FRONT_DEF:
                    CardAnimation.Rotate2FrontAtk(card);
                    break;
                case Status.BACK_ATK:
                    CardAnimation.Rotate2BackDef(card);
                    break;
                case Status.BACK_DEF:
                    CardAnimation.Rotate2FrontAtk2(card);
                    break;
                default:
                    break;
            }

            
        }

        /// <summary>
        /// 卡片菜单：攻守处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void excuete_set2FrontOrBack(object sender, ExecutedRoutedEventArgs e)
        {
            CardControl card = e.OriginalSource as CardControl;
            //Canvas cv = card.Parent as Canvas;

            switch (card.Status)
            {
                case Status.FRONT_ATK:
                    
                case Status.FRONT_DEF:
                    CardAnimation.turn2Back(card);
                    break;
                case Status.BACK_ATK:
                case Status.BACK_DEF:
                    CardAnimation.turn2Front(card);
                    break;
                default:
                    break;
            }


        }

        public static void excuete_set2BackDef(object sender, ExecutedRoutedEventArgs e)
        {
            CardControl card = e.OriginalSource as CardControl;
            //Canvas cv = card.Parent as Canvas;
            if (card.Status == Status.BACK_DEF)
            {
                return;
            }

            CardAnimation.turn2BackDef(card);

        }

        public static void excuete_release2Graveyard(object sender, ExecutedRoutedEventArgs e)
        {
            //MainWindow main = sender as MainWindow;
            CardControl card = e.OriginalSource as CardControl;

            CardAnimation.move2Graveyard(card);

        }

        //判断要执行的命令
        public static void Command_judge(object sender, string command)
        {
            CardControl card = CardOperate.getCard(sender) as CardControl;
            ModifyAtkOrDef mad = new ModifyAtkOrDef(card);
            mad.Owner = mainwindow;
            mad.ShowDialog();
            //MenuItem mi = sender as MenuItem;
            //MenuItem mi_par = mi.Parent as MenuItem;

            //#region 指示物

            //if ( mi_par!=null && mi_par.Header.Equals("指示物"))
            //{

            //    Card card = CardOperate.getCard((sender as MenuItem).Parent) as Card;
            //    if (card.isBack) return;
            //    Canvas cv = card.Parent as Canvas;
            //    if (cv.Children.Count > 1 && card.sCardType.Contains("XYZ"))
            //    {
            //        return;
            //    }

            //    StackPanel sp = mainwindow.FindName(cv.Name.Replace("card", "sp_sign")) as StackPanel;
            //    if (mi.Header.ToString().Equals("清除"))
            //    {
            //        string report = "清除 " + "[" + card.Name + "] 的所有指示物";
            //        DuelOperate.getInstance().sendMsg("Clearsign=" + sp.Name , report);
            //        sp.Children.Clear();
            //        return;
            //    }
            //    foreach (FrameworkElement fe in sp.Children)
            //    {
            //        if (fe.Name.Equals("Black") && mi.Header.ToString().Equals("黑"))
            //        {
            //            string report = "清除 " + "[" + card.Name + "] 的[黑色指示物]";
            //            DuelOperate.getInstance().sendMsg("Setsign=" + sp.Name + ",black", report);          
            //            sp.Children.Remove(fe); return; 
            //        }
            //        if (fe.Name.Equals("Blue") && mi.Header.ToString().Equals("蓝"))
            //        {
            //            string report = "清除 " + "[" + card.Name + "] 的[蓝色指示物]";
            //            DuelOperate.getInstance().sendMsg("Setsign=" + sp.Name + ",blue", report);
            //            sp.Children.Remove(fe); return;
            //        }
            //        if (fe.Name.Equals("Red") && mi.Header.ToString().Equals("红"))
            //        {
            //            string report = "清除 " + "[" + card.Name + "] 的[红色指示物]";
            //            DuelOperate.getInstance().sendMsg("Setsign=" + sp.Name + ",red", "清除红色指示物");
            //            sp.Children.Remove(fe); return;
            //        }

            //    }



            //    Grid gd = new Grid();
            //    gd.Width = 25;
            //    gd.Height = 25;


            //    TextBlock tb = new TextBlock();
            //    tb.Foreground = Brushes.White;
            //    tb.IsHitTestVisible = true;
            //    tb.Height = 21;
            //    tb.Style = Application.Current.Resources["tb_AtkDefStyle"] as Style;
            //    tb.Text = "1";
            //    tb.MouseDown += new MouseButtonEventHandler(DuelEvent.ClikDouble2);


            //    /* tooltip
            //    StackPanel sp_tooltip = new StackPanel();
            //    System.Windows.Shapes.Ellipse a = new System.Windows.Shapes.Ellipse();
            //    a.Stroke = Brushes.Black;
            //    a.StrokeThickness = 2;
            //    a.Width = 50;
            //    a.Height = 50;
            //    ToolTip tt = new ToolTip();
            //    tt.Content = a;
            //    tt.StaysOpen = true;
            //    tt.Style = null;
            //    ToolTipService.SetInitialShowDelay(tt, 300);
            //    ToolTipService.SetShowDuration(tt, 7000);
            //    ToolTipService.SetBetweenShowDelay(tt, 2000);
            //    ToolTipService.SetPlacement(tt, System.Windows.Controls.Primitives.PlacementMode.Top);
            //    ToolTipService.SetPlacementRectangle(tt, new Rect(50, 0, 0, 0));
            //    ToolTipService.SetHorizontalOffset(tt, 0);
            //    ToolTipService.SetVerticalOffset(tt, 0);
            //    ToolTipService.SetHasDropShadow(tt, false);
            //    ToolTipService.SetShowOnDisabled(tt, true);
            //    ToolTipService.SetIsEnabled(tt, true);
            //    tb.ToolTip = tt;*/

            //    Border bd = new Border();
            //    bd.Style = Application.Current.Resources["bd_style"] as Style;
            //    switch (mi.Header.ToString())
            //    {
            //        case "黑":
            //            {
            //                gd.Name = "Black";
            //                tb.Name = sp.Name.Replace("sp", "tb") + "_black";
            //                bd.BorderBrush = Brushes.Black;
            //                string report = "给 " + "[" + card.name + "] 放置[黑色指示物] -> 现为[1]";
            //                DuelOperate.getInstance().sendMsg("Setsign=" + sp.Name + ",black", report);
            //            }
            //            break;
            //        case "蓝":
            //            {
            //                gd.Name = "Blue";
            //                tb.Name = sp.Name.Replace("sp", "tb") + "_blue";
            //                bd.BorderBrush = Brushes.Blue;
            //                string report = "给 " + "[" + card.name + "] 放置[蓝色指示物] -> 现为[1]";
            //                DuelOperate.getInstance().sendMsg("Setsign=" + sp.Name + ",blue", report);
            //            }                     
            //            break;
            //        case "红":
            //            {
            //                gd.Name = "Red";
            //                tb.Name = sp.Name.Replace("sp", "tb") + "_red";
            //                bd.BorderBrush = Brushes.Red;
            //                string report = "给 " + "[" + card.name + "] 放置[红色指示物] -> 现为[1]";
            //                DuelOperate.getInstance().sendMsg("Setsign=" + sp.Name + ",red", report);
            //            }
            //            break;  

            //    }

            //    gd.Children.Add(bd);              
            //    gd.Children.Add(tb);
            //    sp.Children.Add(gd);


            //    return;
            //}


            //#endregion

            //#region 怪物卡位切洗

            //if (mi.Header.Equals("全部洗切"))
            //{

            //    int m = 1;
            //    int n = 11;

            //    if (mi_par != null  )
            //    {
            //        if (mi_par.Header.Equals("怪物"))
            //        {
            //            m = 5;
            //            n = 11;
            //        }
            //        else if (mi_par.Header.Equals("魔陷"))
            //        {
            //            m = 1;
            //            n = 6;
            //        }

            //    }



            //    //获取需要切洗的Canvas
            //    List<Canvas> cvs = new List<Canvas>();
            //    if (mainwindow.btn_choosezone.Content.Equals("C-ON"))
            //    {
            //        for (int i = m; i < n; i++)
            //        {
            //            CheckBox cb = mainwindow.battle_zone_middle.FindName("cb_1_" + i) as CheckBox;
            //            if (cb != null && cb.IsChecked == true)
            //            {
            //                Canvas cv_shuffle = mainwindow.battle_zone_middle.FindName("card_1_" + i) as Canvas;
            //                if (cv_shuffle.Children.Count == 1)
            //                {
            //                    Card card_first = cv_shuffle.Children[0] as Card;
            //                    if (!card_first.isBack || !card_first.isDef)
            //                    {
            //                        cvs.Clear();
            //                        MessageBox.Show("请确定切洗区域均为仅有一张set卡");
            //                        return;
            //                    }
            //                    cvs.Add(cv_shuffle);
            //                }
            //                else
            //                {
            //                    cvs.Clear();
            //                    MessageBox.Show("请确定切洗区域均为仅有一张set卡");
            //                    return;

            //                }

            //            }
            //        }
            //    }
            //    else if (mainwindow.btn_choosezone.Content.Equals("C-OFF"))
            //    {
            //        for (int i = m; i < n; i++)
            //        {
            //            Canvas cv_shuffle = mainwindow.battle_zone_middle.FindName("card_1_" + i) as Canvas;
            //            if (cv_shuffle.Children.Count == 1)
            //            {
            //                Card card_first = cv_shuffle.Children[0] as Card;
            //                if (!card_first.isBack || !card_first.isDef)
            //                {
            //                    cvs.Clear();
            //                    MessageBox.Show("请确定切洗区域均为仅有一张set卡");
            //                    return;
            //                }
            //                cvs.Add(cv_shuffle);
            //            }
            //        }
            //    }

            //    if (cvs.Count < 2) return;

            //    TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();
            //    List<FrameworkElement> shuffle_cards = new List<FrameworkElement>();
            //    for (int j = 0; j < cvs.Count; j++)
            //    {
            //        if (cvs[j].Children.Count == 1)
            //        {
            //            CardControl card = cvs[j].Children[0] as CardControl;       
            //            Grid gd = Base.getParerent(card).Parent as Grid;
            //            if (gd != null)
            //            {
            //                Panel.SetZIndex(gd, 1);
            //            }

            //            //1.获取卡片相对于目的地的距离
            //            Point start = card.TranslatePoint(new Point(), cvs[0]);
            //            if (card.isDef)
            //            {
            //                start.X = start.X + ((card.ActualHeight - card.ActualWidth) / 2.0);
            //                start.Y = start.Y - card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2.0);
            //            }

            //            //2.获取卡片在卡框中的相对距离
            //            Point end = new Point((cvs[0].ActualWidth - card.ActualWidth) / 2.0, (cvs[0].ActualHeight - card.ActualHeight) / 2.0);
            //            //脱离原控件
            //            Base.getawayParerent(card);
            //            //利用1设置初始位置
            //            Canvas.SetTop(card, start.Y);
            //            Canvas.SetLeft(card, start.X);
            //            //加入目的地控件
            //            cvs[0].Children.Add(card);
            //            shuffle_cards.Add(card);
            //            MyStoryboard msb = CardAnimation.CanvasXY(end, 200);
            //            msb.card = card;
            //            msb.Completed += (object sender_, EventArgs e_) =>
            //            {
            //                //清空属性和动画的关联绑定
            //                msb.card.BeginAnimation(Canvas.LeftProperty, null);
            //                msb.card.BeginAnimation(Canvas.TopProperty, null);

            //                Canvas.SetTop(msb.card, end.Y);
            //                Canvas.SetLeft(msb.card, end.X);

            //                if (shuffle_cards.IndexOf(card) == shuffle_cards.Count - 1)
            //                {
            //                    string commandsend = "Shuffle_zone=" + cvs[0].Name;
            //                    shuffle<FrameworkElement>(shuffle_cards);
            //                    shuffle<FrameworkElement>(shuffle_cards);

            //                    //DuelOperate.getInstance().sendMsg("shuffle_monster="+cvs[0])
            //                    for (int i = 0; i < cvs.Count; i++)
            //                    {
            //                        commandsend += "," + ((Card)shuffle_cards[i]).duelindex + ","+ cvs[i].Name;
            //                        Panel.SetZIndex(cvs[i], i);
            //                        Point start2 = shuffle_cards[i].TranslatePoint(new Point(), cvs[i]);
            //                        if (card.isDef)
            //                        {
            //                            start2.X = start2.X + ((shuffle_cards[i].ActualHeight - shuffle_cards[i].ActualWidth) / 2.0);
            //                            start2.Y = start2.Y - shuffle_cards[i].ActualWidth - ((shuffle_cards[i].ActualHeight - shuffle_cards[i].ActualWidth) / 2.0);
            //                        }

            //                        //2.获取卡片在卡框中的相对距离
            //                        Point end2 = new Point((cvs[i].ActualWidth - shuffle_cards[i].ActualWidth) / 2.0, (cvs[i].ActualHeight - shuffle_cards[i].ActualHeight) / 2.0);
            //                        //脱离原控件
            //                        Base.getawayParerent(shuffle_cards[i] as CardControl);
            //                        //利用1设置初始位置
            //                        Canvas.SetTop(shuffle_cards[i], start2.Y);
            //                        Canvas.SetLeft(shuffle_cards[i], start2.X);
            //                        //加入目的地控件
            //                        cvs[i].Children.Add(shuffle_cards[i]);
            //                        MyStoryboard msb2 = CardAnimation.CanvasXY(end2, 200);
            //                        msb2.card = shuffle_cards[i] as CardControl;
            //                        msb2.Completed += (object a, EventArgs b) =>
            //                        {
            //                            msb2.card.BeginAnimation(Canvas.LeftProperty, null);
            //                            msb2.card.BeginAnimation(Canvas.TopProperty, null);

            //                            Canvas.SetTop(msb2.card, end.Y);
            //                            Canvas.SetLeft(msb2.card, end.X);

            //                            if (i == cvs.Count)
            //                            {
            //                                foreach (var item in cvs)
            //                                {
            //                                    Panel.SetZIndex(item, 0);
            //                                }
            //                            }
            //                            //Panel.SetZIndex(cvs[i], 0);
            //                        };
            //                        msb2.Begin(shuffle_cards[i]);
            //                    }
            //                    DuelOperate.getInstance().sendMsg(commandsend, "");

            //                }

            //                //storyboard = null;
            //            };
            //            msb.Begin(card);
            //            //animator.Animates.Add(msb);
            //        }

            //    }




            //    //animator.Begin(shuffle_cards);
            //    return;
            //}

            //#endregion

            //if (command.Equals("全部送往墓地") || command.Contains("全部除外") || command.Equals("全部变为表侧 · 防守表示") || command.Equals("全部变为里侧 · 防守表示") || command.Equals("全部变为表侧 · 攻击表示") || command.Contains("全部返回手卡") || command.Contains("全部返回卡组顶端") || command.Contains("全部洗切"))
            //{
            //    #region 多重卡片操作

            //    //MenuItem mi = sender as MenuItem;
            //    //MenuItem mi_par = mi.Parent as MenuItem;

            //    //OpponentOperate.ActionAnalyze(, true);
            //    DuelOperate.getInstance().sendMsg("Cards=" + mi_par.Header.ToString() +","+ mi.Header.ToString(),"");
            //    //怪物
            //    int n = 6;
            //    int m = 11;

            //    if (mi_par.Header.Equals("魔陷"))
            //    {
            //        n = 1;
            //        m = 6;
            //    }
            //    if (mi_par.Header.Equals("场上"))
            //    {
            //        n = 1;
            //        m = 11;
            //    }
            //    if (mi_par.Header.Equals("手卡"))
            //    {
            //        n = 35;
            //        m = 36;
            //    }

            //    List<CardControl> cards_1 = new List<CardControl>();
            //    List<CardControl> cards_2 = new List<CardControl>();
            //    List<CardControl> cards_3 = new List<CardControl>();//返回手卡时每个卡位的第一张XYZ怪
            //    List<CardControl> cards_4 = new List<CardControl>();//返回手卡时每个卡位的卡片素材

            //    for (int i = n; i < m; i++)
            //    {
            //        Canvas cv = mainwindow.battle_zone_middle.FindName("card_1_"+i) as Canvas;
            //        //Canvas cv = mainwindow.MySpace.Children[i] as Canvas;

            //        if (cv.Children.Count>0)
            //        {
            //            if (command.Equals("全部洗切"))
            //            {


            //            }
            //            if (command.Equals("全部变为表侧 · 防守表示") || command.Equals("全部变为表侧 · 攻击表示"))
            //            {
            //                #region

            //                CardControl top = cv.Children[cv.Children.Count - 1] as CardControl;
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

            //                if (cv.Children.Count<2)
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
            //                            if (card.isBack)
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
            //            else if (command.Contains("全部返回卡组顶端") )
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

            //                    if (command.Equals("全部送往墓地") )
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

            //    if (cards_1.Count>0)
            //    {
            //        if (command.Equals("全部送往墓地") || command.Equals("全部变为表侧 · 防守表示") || command.Equals("全部变为表侧 · 攻击表示") )
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

            //    if (cards_2.Count>0)
            //    {

            //        if (command.Equals("全部送往墓地"))
            //        {
            //            #region

            //            Point end = mainwindow.card_1_Graveyard.TranslatePoint(new Point(), mainwindow.MySpace);
            //            MyStoryboard msb2 = CardAnimation.Cards_move(cards_2, end, 150, "1");
            //            msb2.Completed += (object sender_, EventArgs e_) =>
            //            {
            //                foreach (CardControl card in msb2.cards)
            //                {
            //                    card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //                    card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);

            //                    mainwindow.MySpace.Children.Remove(card);

            //                    //清空属性和动画的关联绑定
            //                    card.BeginAnimation(Canvas.LeftProperty, null);
            //                    card.BeginAnimation(Canvas.TopProperty, null);

            //                    Canvas.SetTop(card, 0);
            //                    Canvas.SetLeft(card, 0);

            //                    //card.isDef = false;
            //                    mainwindow.card_1_Graveyard.Children.Add(card);
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
            //                foreach (CardControl card in msb3.cards)
            //                {
            //                    Base.getawayParerent(card);
            //                    card.RenderTransform = new RotateTransform();
            //                    //card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);

            //                    card.set2FrontAtk();
            //                    card.showImg();
            //                    mainwindow.card_1_Outside.Children.Add(card);
            //                    CardOperate.sort_SingleCard(card);
            //                }

            //                foreach (CardControl card in cards_4)
            //                {
            //                    card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //                    card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);

            //                    mainwindow.MySpace.Children.Remove(card);

            //                    //清空属性和动画的关联绑定
            //                    card.BeginAnimation(Canvas.LeftProperty, null);
            //                    card.BeginAnimation(Canvas.TopProperty, null);

            //                    Canvas.SetTop(card, 0);
            //                    Canvas.SetLeft(card, 0);

            //                    card.isBack = false;
            //                    card.isDef = false;
            //                    mainwindow.card_1_Graveyard.Children.Add(card);
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

            //            Point end = mainwindow.card_1_hand.TranslatePoint(new Point(), mainwindow.MySpace);
            //            if (mainwindow.card_1_hand.Children.Count < 1)
            //            {
            //                end.X = end.X + ((mainwindow.card_1_hand.Width - 56) / 2);
            //            }
            //            else
            //            {
            //                end.X = end.X + Canvas.GetLeft(mainwindow.card_1_hand.Children[mainwindow.card_1_hand.Children.Count - 1]);
            //            }
            //            MyStoryboard msb10 = CardAnimation.Cards_move(cards_2, end, 150, "1");
            //            msb10.Completed += (object sender_, EventArgs e_) =>
            //            {
            //                foreach (Card card in msb10.cards)
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
            //                    mainwindow.card_1_hand.Children.Add(card);

            //                }

            //                CardOperate.sort_HandCard(mainwindow.card_1_hand);

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
            //                    mainwindow.card_1_Extra.Children.Add(card);
            //                    CardOperate.sort_SingleCard(card);
            //                }

            //                foreach (Card card in cards_4)
            //                {
            //                    if (command.Substring(command.Length - 4, 4).Equals("素材送墓"))
            //                    {
            //                        card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //                        card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);

            //                        mainwindow.MySpace.Children.Remove(card);

            //                        //清空属性和动画的关联绑定
            //                        card.BeginAnimation(Canvas.LeftProperty, null);
            //                        card.BeginAnimation(Canvas.TopProperty, null);

            //                        Canvas.SetTop(card, 0);
            //                        Canvas.SetLeft(card, 0);

            //                        card.isDef = false;
            //                        mainwindow.card_1_Graveyard.Children.Add(card);
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

            //            MyStoryboard msb16 = CardAnimation.Cards_move2(cards_2, end, 150,"1");
            //            msb16.Completed += (object sender_, EventArgs e_) =>
            //            {
            //                foreach (Card card in cards_2)
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
            //                    mainwindow.card_1_Deck.Children.Add(card);
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
            //                    mainwindow.card_1_Extra.Children.Add(card);
            //                    CardOperate.sort_SingleCard(card);
            //                }

            //                foreach (Card card in cards_4)
            //                {
            //                    if (command.Substring(command.Length - 4, 4).Equals("素材送墓"))
            //                    {
            //                        card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //                        card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);

            //                        mainwindow.MySpace.Children.Remove(card);

            //                        //清空属性和动画的关联绑定
            //                        card.BeginAnimation(Canvas.LeftProperty, null);
            //                        card.BeginAnimation(Canvas.TopProperty, null);

            //                        Canvas.SetTop(card, 0);
            //                        Canvas.SetLeft(card, 0);

            //                        card.isDef = false;
            //                        mainwindow.card_1_Graveyard.Children.Add(card);
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
            //            msb16.Name = "msb16";
            //            tls.Animates.Add(msb16);

            //            #endregion
            //        }



            //    }

            //    #endregion

            //    #region 当处理回手卡,回卡组命令时，XYZ需回到额外

            //    if (cards_3.Count > 0 )
            //    {


            //        Point end = mainwindow.card_1_Extra.TranslatePoint(new Point(), mainwindow.MySpace);

            //        //设置XYZ的动画
            //        MyStoryboard msb11 = CardAnimation.Cards_move2(cards_3, end, 150,"1");

            //        if (cards_2.Count < 1)
            //        {
            //            //若msb10不存在，则不存在回手卡的卡，需要启动自身动画，即msb11
            //            msb11.Completed += (object sender_, EventArgs e_) =>
            //            {
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
            //                    mainwindow.card_1_Extra.Children.Add(card);
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
            //                        msb.Children.Add(dba);
            //                    }
            //                    else if (msb.Name.Equals("msb16"))
            //                    {
            //                        foreach (DoubleAnimation dba in msb11.Children)
            //                        msb.Children.Add(dba);
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

            //            Point end = mainwindow.card_1_Graveyard.TranslatePoint(new Point(), mainwindow.MySpace);
            //            MyStoryboard msb12 = CardAnimation.Cards_move2(cards_4, end, 150, "1");
            //            if (cards_2.Count < 1)
            //            {
            //                //若msb10不存在，则不存在回手卡的卡，需要启动自身动画，即msb11
            //                msb12.Completed += (object sender_, EventArgs e_) =>
            //                {
            //                    foreach (Card card in cards_4)
            //                    {
            //                        card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //                        card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);

            //                        mainwindow.MySpace.Children.Remove(card);

            //                        //清空属性和动画的关联绑定
            //                        card.BeginAnimation(Canvas.LeftProperty, null);
            //                        card.BeginAnimation(Canvas.TopProperty, null);

            //                        Canvas.SetTop(card, 0);
            //                        Canvas.SetLeft(card, 0);

            //                        card.isDef = false;
            //                        mainwindow.card_1_Graveyard.Children.Add(card);
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
            //                        mainwindow.card_1_Outside.Children.Add(card);
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

            //#endregion

            //}
            //else
            //{
            //    cardview = CardView.getInstance(mainwindow);

            //    if (cardview.tb_View.SelectedIndex != -1)
            //    {

            //        MessageBoxResult MsgBoxResult = MessageBox.Show("关闭卡片区浏览窗口后再进行菜单操作(是否关闭？)", "提示", MessageBoxButton.YesNo);
            //        if (MsgBoxResult == MessageBoxResult.Yes)
            //        {
            //            //DuelOperate.CardviewExit();
            //            //cardview.tb_View.SelectedIndex = -1;
            //            //cardview.Close();
            //            CardOperate.cardview_close();
            //            //mainwindow.
            //            return;
            //        }
            //        if (MsgBoxResult == MessageBoxResult.No)
            //        {
            //            return;
            //        }

            //    }

            //    Card card = CardOperate.getCard(sender) as Card;
            //    Canvas cv = new Canvas();
            //    if (card == null) cv = CardOperate.getCard(sender) as Canvas;
            //    else cv = card.Parent as Canvas;

            //    int cv_childre_num = cv.Children.IndexOf(card);

            //    switch (command)
            //    {
            //        #region

            //        case "效果发动":
            //            {
            //                MyStoryboard msb = CardAnimation.EffectOrigin();
            //                msb.Begin((mainwindow.FindName(cv.Name.Replace("card", "bd")) as FrameworkElement));
            //                DuelOperate.getInstance().sendMsg("StartEffect=" + cv.Name, "效果发动");
            //            }
            //            break;
            //        case "攻/守形式转换":
            //            {
                            //def_or_Atk(card);

            //                string report = "将 " + (DuelReportOperate.from(cv.Name) + " [" + card.name + "] 变更为 <" + DuelReportOperate.analyze_state(card) + ">" + Environment.NewLine);
            //                DuelOperate.getInstance().sendMsg("FormChange=" + card.duelindex, report);

            //            }

            //            //动作
            //            //OpponentOperate.ActionAnalyze("FormChange=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num, true);
            //            //OpponentOperate.ActionAnalyze("FormChange=" +card.duelindex ,true);

            //            break;
            //        case "里侧/表侧转换":
            //            {
            //                in_or_out(card);
            //                string report = "将 " + (DuelReportOperate.from(cv.Name) + " [" + card.name + "] 变更为 <" + DuelReportOperate.analyze_state(card) + ">" + Environment.NewLine);                        
            //                DuelOperate.getInstance().sendMsg("FormChange2=" + card.duelindex, report);
            //            }

            //            break;
            //        case "转为里侧守备":
            //            {
            //                to_inDef(card);
            //                string report = "将 " + (DuelReportOperate.from(cv.Name) + " [" + card.name + "] 变更为 <" + DuelReportOperate.analyze_state(card) + ">" + Environment.NewLine);
            //                DuelOperate.getInstance().sendMsg("FormChange3=" + card.duelindex, report);                           
            //            }

            //            break;
            //        case "放回卡组顶端":
            //            {
            //                send2where_move(card, "卡组", 600);
            //                card.ContextMenu = AllMenu.cm_deck;
            //                string report = "将 " + (DuelReportOperate.from(cv.Name) + " [" + card.name + "] 放回 <卡组>顶端" + Environment.NewLine);
            //                DuelOperate.getInstance().sendMsg("Back2Hand=" + card.duelindex + "," +"card_1_Deck",report);

            //            }
            //            break;
            //        case "送入墓地":
            //            {
            //                send2where_move(card, "墓地", 150);
            //                card.ContextMenu = AllMenu.cm_graveyard;
            //                CardOperate.sort_HandCard(mainwindow.card_1_hand);
            //                string report = "将 " + (DuelReportOperate.from(cv.Name) + " [" + card.name + "] 送入 <墓地>" + Environment.NewLine);
            //                DuelOperate.getInstance().sendMsg("Move=" + card.duelindex + "," + "card_1_Graveyard", report);

            //            }

            //            break;
            //        case "从游戏中除外":
            //            {
            //                send2where_gone(card, mainwindow.card_1_Outside, false);
            //                card.ContextMenu = AllMenu.cm_outside;
            //                string report = "将 " + (DuelReportOperate.from(cv.Name) + " [" + card.name + "] 从游戏中 <除外>" + Environment.NewLine);
            //                DuelOperate.getInstance().sendMsg("Disappear=" + card.duelindex + "," + "card_1_Outside", report);

            //            }

            //            break;
            //        case "解放":
            //            {
            //                send2where_gone(card, mainwindow.card_1_Graveyard, false);
            //                card.ContextMenu = AllMenu.cm_graveyard;
            //                string report = "将 " + (DuelReportOperate.from(cv.Name) + " [" + card.name + "] 解放" + Environment.NewLine);
            //                DuelOperate.getInstance().sendMsg("Disappear=" + card.duelindex + "," + "card_1_Graveyard", report);
            //            }


            //            break;
            //        case "加入手卡":
            //            {
            //                send2where_move(card, "手卡", 600);
            //                card.ContextMenu = AllMenu.cm_hand;
            //                string report = "将 " + (DuelReportOperate.from(cv.Name) + " [" + card.name + "] 取回 <手卡>" + Environment.NewLine);
            //                DuelOperate.getInstance().sendMsg("Back2Hand=" + card.duelindex + "," + "card_1_hand", report);

            //            }
            //            break;
            //        case "打开/盖伏":
            //            {
            //                string report = null;
            //                if (card.isBack) report = "将 " + (DuelReportOperate.from(cv.Name) + " [" + card.name + "] 打开" + Environment.NewLine);
            //                if (!card.isBack) report = "将 " + (DuelReportOperate.from(cv.Name) + " [" + card.name + "] 盖伏" + Environment.NewLine);
            //                in_or_out(card);
            //                DuelOperate.getInstance().sendMsg("FormChange2="+card.duelindex, report);

            //            }
            //            break;
            //        case "将顶牌送往墓地":
            //            {
            //                send2where_move(card, "墓地", 600);
            //                card.ContextMenu = AllMenu.cm_graveyard;
            //                string report = "将 " + (DuelReportOperate.from(cv.Name) + " [" + card.name + "] 送入 <墓地>" + Environment.NewLine);
            //                DuelOperate.getInstance().sendMsg("Move=" + card.duelindex + "," + "card_1_Graveyard", report);                          
            //            }
            //            break;
            //        case "将顶牌从游戏中除外":
            //            {
            //                send2where_gone(card, mainwindow.card_1_Outside, false);
            //                card.ContextMenu = AllMenu.cm_outside;
            //                string report = "将 " + (DuelReportOperate.from(cv.Name) + " [" + card.name + "] 从游戏中 <除外>" + Environment.NewLine);
            //                DuelOperate.getInstance().sendMsg("Disappear=" + card.duelindex + "," + "card_1_Outside",report);

            //            }
            //            break;
            //        case "将顶牌背面除外":
            //            {
            //                send2where_gone(card, mainwindow.card_1_Outside, true);
            //                card.ContextMenu = AllMenu.cm_outside;

            //                mainwindow.report.Text += ("将顶牌从游戏中背面 <除外>" + Environment.NewLine);
            //            }
            //            break;
            //        case "切洗卡组":
            //            {
            //                Console.WriteLine("切洗");
            //                shuffle_card("卡组");
            //                mainwindow.report.Text += ("切洗 <卡组>" + Environment.NewLine);
            //            }
            //            break;
            //        case "将顶牌放回卡组底部":
            //            {
            //                top2buttom();
            //                string report = "将顶牌放回 <卡组> 底部" + Environment.NewLine;
            //                DuelOperate.getInstance().sendMsg("Top2bottom=",report);                                                     
            //            }

            //            break;
            //        case "返回额外区":
            //            {
            //                send2where_move(card, "额外", 600);
            //                string report = (DuelReportOperate.from(cv.Name) + " [" + card.name + "] 返回 <额外>" + Environment.NewLine);
            //                DuelOperate.getInstance().sendMsg("Back2Hand=" + card.duelindex + "," + "card_1_Extra", report);

            //            }

            //            break;
            //        case "送入对手墓地":
            //            {
            //                send2OpGraveyard(card, "对手墓地", 300);
            //                OpponentOperate.ActionAnalyze("Send2OpGraveyard=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num, true);
            //                string report = ("将 " + DuelReportOperate.from(cv.Name) + " [" + card.name + "] 送入 <对手墓地>" + Environment.NewLine);
            //                DuelOperate.getInstance().sendMsg("Send2OpGraveyard=" + card.duelindex , report);
            //            }
            //            //send2where_move(card, "对手墓地", 600);

            //            break;
            //        case "转移控制权":
            //            {
            //                control_Change(card, 200);

            //               // DuelOperate.getInstance().sendMsg("ControlChange=" + card.duelindex, report);
            //            }
            //            break;


            //        #endregion
            //    }
            //}


        }


        #region <-- 将顶牌放回卡组底部 -->
        private static void top2buttom()
        {
            //Console.WriteLine("卡组有：" + mainwindow.card_1_Deck.Children.Count + "卡片");
            Card card = (Card)mainwindow.card_1_Deck.Children[mainwindow.card_1_Deck.Children.Count-1];
            mainwindow.card_1_Deck.Children.Remove(card);
            mainwindow.card_1_Deck.Children.Insert(0, card);
            //CardOperate.card_BackAtk(card);
            //card = null;
            //CardOperate.get_Firstcard2Battle(6);
        }

        #endregion

        #region <-- 暂时无用 -->

        #region <-- 放回卡组顶端 -->
        private static void back_to_deck(Card card)
        {
            //if (!card.isDef)
            //{
            //    card.isDef = false;
            //    card.isBack = true;
            //    CardAnimation.RotateOut(card, -90, 0);
            //    card.SetPic();

            //    card.Margin = new Thickness(0);

            //    //设定移动起点坐标
            //    Point start = card.TranslatePoint(new Point(), mainwindow.MySpace);
            
            //    //设置移动终点坐标
            //    //Point end = new Point(647, 91);
            //    //设置移动终点坐标
            //    Point end = mainwindow.card_1_Deck.TranslatePoint(new Point(), mainwindow.MySpace);

            //    CardAnimation.MoveAnimation(card, start, end, "卡组", 600);
            //}
            ////throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region <-- 攻/守形式转换 -->
        private static void def_or_Atk(CardControl card)
        {
            

            ////before = DuelReportOperate.analyze_state(card);
            //Canvas cv = card.Parent as Canvas;

            //if (card.isDef && card.isBack)
            //{
            //    card.isDef = false;
            //    card.isBack = false;
                
            //    //TextBlock tb = mainwindow.FindName(Base.getParerent(card).Name.Replace("card","atk")) as TextBlock;
            //    //if (tb != null)
            //    //{
            //    //    tb.SetCurrentValue(TextBlock.TextProperty, card.cardAtk + "/" + card.cardDef);
            //    //}
            //    //CardAnimation.Rotate(card, 90, 0);
            //    CardAnimation.Rotate2Atk(card);           
            //    //CardAnimation.RotateOut(card, 90, 0);
            //    CardOperate.sort_Canvas(cv);
            //    //card.isDef = false;
            //    //card.isBack = false;
            //    //card.SetPic();
            //}
            //else if (card.isDef == false)
            //{
            //    //RotateTransform rt = new RotateTransform();
            //    //card.RenderTransform = rt;
            //    CardAnimation.Def_or_Atk(card);
            //    card.isDef = true;
            //    if (cv.Children.Count > 1)
            //    {
            //        CardOperate.sort_Canvas(cv);
            //    }                          
                
            //}
            //else if (card.isDef && card.isBack == false)
            //{
            //    MyStoryboard msb = CardAnimation.Rotate(card, -90, 0);
            //    msb.Completed += (object sender, EventArgs e) =>
            //    {
            //        msb.card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //        msb.card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);
                    
            //        //storyboard = null;
            //    };
            //    msb.Begin();           
            //    card.isDef = false;
            //    card.isBack = false;
            //    CardOperate.sort_Canvas(cv);
            //}
            ////after = DuelReportOperate.analyze_state(card);

            //战报
           // mainwindow.report.Text += "将 " + (DuelReportOperate.from(cv.Name) + " [" + card.cardName + "] 变更为 <" + DuelReportOperate.analyze_state(card) + ">" + Environment.NewLine);

        }

        #endregion

        #region <-- 里侧/表侧转换 -->
        private static void in_or_out(Card card)
        {
            //Canvas cv = card.Parent as Canvas;

            //card.isBack = card.isBack ? false : true;
            //StackPanel sp = mainwindow.FindName(cv.Name.Replace("card", "sp_sign")) as StackPanel;
            //if (sp != null) sp.Children.Clear();
            
            //CardAnimation.Rotate_card(card);
                      
            ////战报
            ////mainwindow.report.Text += "将 " + (DuelReportOperate.from(cv.Name) + " [" + card.cardName + "] 变更为 <" + DuelReportOperate.analyze_state(card) + ">" + Environment.NewLine);
        }

        #endregion

        #region <-- 转为里侧守备 -->
        private static void to_inDef(Card card)
        {
            //Canvas cv = card.Parent as Canvas;

            //#region 清除指示物

            //StackPanel sp = mainwindow.FindName(cv.Name.Replace("card", "sp_sign")) as StackPanel;
            //if (sp != null) sp.Children.Clear();

            //#endregion

            //if (!card.isBack)
            //{
            //    if (card.isDef)
            //    {
            //        CardAnimation.Rotate_card(card);
            //        card.isBack = true;
            //    }
            //    else if (!card.isDef)
            //    {
            //        CardAnimation.Rotate2Def(card);
            //        if (cv.Children.Count > 1)
            //        {
            //            CardOperate.sort_Canvas(cv);
            //        }
            //        card.isDef = true; 
            //        card.isBack = true;
            //    }

            //}
            //else if (card.isBack)
            //{
            //    if (card.isDef)
            //    {
            //        return;
            //    }
            //    else if (!card.isDef)
            //    {
            //        CardAnimation.Def_or_Atk(card);
            //        card.isDef = true;
            //    }
            //    card.SetPic();
            //}

            //CardOperate.sort_Canvas(cv);
            //mainwindow.report.Text += "将 " + (DuelReportOperate.from(cv.Name) + " [" + card.cardName + "] 变更为 <" + DuelReportOperate.analyze_state(card) + ">" + Environment.NewLine);
        }

        #endregion

        #region <-- 送去墓地,放回卡组顶部,返回额外区 -->

        /* 
         * 送去墓地
         * 放回卡组顶部
         * 返回额外区
         */
        private static void send2where_move(Card card, string to, int time)
        {
            ////card.Margin = new Thickness(0);

            ////设定移动起点坐标和终点
            //Point start = card.TranslatePoint(new Point(), mainwindow.MySpace);
            //Canvas cv = card.Parent as Canvas;

            

            ////设置移动终点坐标，修改卡片状态
            //Point end = new Point();

            //switch (to)
            //{
            //    case "手卡":
            //        #region
                    
            //        int num = mainwindow.card_1_hand.Children.Count;
            //        if (num == 0) { end = mainwindow.card_1_hand.TranslatePoint(new Point(), mainwindow.MySpace); }
            //        else { end = mainwindow.card_1_hand.Children[mainwindow.card_1_hand.Children.Count - 1].TranslatePoint(new Point(), mainwindow.MySpace); }                
            //        break;
            //        #endregion
            //    case "墓地":
            //        #region
                    

            //        end = mainwindow.card_1_Graveyard.TranslatePoint(new Point(), mainwindow.MySpace);
            //        break;
            //        #endregion
            //    case "除外":
            //        #region
            //        if (cardview.tb_View.SelectedIndex == 0)
            //        {
            //            MessageBox.Show("你正在查看额外区，不允许对额外进行菜单操作！");
            //            return;
            //        }
            //        //if (card.isBack) card.isBack = false;
            //        //card.SetPic();
            //        //if(card.isDef) CardAnimation.RotateOut(card, 90, 0);
            //        //if(!card.isDef)CardAnimation.RotateOut(card, 0, 0);
            //        end = mainwindow.card_1_Outside.TranslatePoint(new Point(), mainwindow.MySpace);
            //        break;
            //        #endregion
            //    case "额外":
            //        #region
            //        if (cardview.tb_View.SelectedIndex == 0)
            //        {
            //            MessageBox.Show("你正在查看额外区，不允许对额外进行菜单操作！");
            //            return;
            //        }
            //        //card.isBack = true;
            //        //CardAnimation.RotateOut(card, 0, 0);
            //        //card.SetPic();
            //        end = mainwindow.card_1_Extra.TranslatePoint(new Point(), mainwindow.MySpace);
            //        break;
            //        #endregion
            //    case "怪物":
            //        #region
            //        card.Margin = new Thickness(5, 0, 5, 0);
            //        //end = new Point(583,0);
            //        break;
            //        #endregion
            //    case "魔陷":
            //        #region
            //        card.Margin = new Thickness(5, 0, 5, 0);
            //        break;
            //        #endregion
            //    case "场地":
            //        #region
            //        end = end = mainwindow.card_1_Area.TranslatePoint(new Point(), mainwindow.MySpace);
            //        break;
            //        #endregion
            //    case "卡组":
            //        #region
            //        if (cardview.tb_View.SelectedIndex == 5 )
            //        {
            //            MessageBox.Show("你正在查看额外区，不允许对额外进行菜单操作！");
            //            return;
            //        }
                    
            //        end = mainwindow.card_1_Deck.TranslatePoint(new Point(), mainwindow.MySpace);
            //        break;
            //        #endregion
            //   /* case "对手墓地":
            //        #region
            //        end = mainwindow.card_2_Graveyard.TranslatePoint(new Point(), mainwindow.MySpace);
            //        end.X = end.X - mainwindow.card_2_Graveyard.Width;
            //        end.Y = end.Y - mainwindow.card_2_Graveyard.Height;

                    
            //        if (card.RenderTransform.GetValue(RotateTransform.AngleProperty).Equals(0.0))
            //        {
            //            RotateTransform rotate = new RotateTransform();
            //            ScaleTransform scale = new ScaleTransform();
            //            TranslateTransform translate = new TranslateTransform();
            //            TransformGroup group = new TransformGroup();
            //            group.Children.Add(scale);
            //            group.Children.Add(rotate);
            //            group.Children.Add(translate);
            //            card.RenderTransform = group;

            //            MyStoryboard msb = CardAnimation.Rotate(card, 0, 180);
            //            msb.Completed += (object sender, EventArgs e) =>
            //            {
            //                msb.card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //                msb.card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);
            //                //storyboard = null;
            //            };
            //            msb.Begin();
            //        }
            //        break;
            //        #endregion*/
            //    case "对手前场":
            //        {
            //            #region
            //            if (mainwindow.card_2_6.Children.Count == 0)
            //            {
            //                end = mainwindow.card_2_6.TranslatePoint(new Point(), mainwindow.MySpace);
            //                end.X = end.X - mainwindow.card_2_6.Width;
            //                end.Y = end.Y - mainwindow.card_2_6.Height;

                            
            //            }
            //            else if (mainwindow.card_2_7.Children.Count == 0)
            //            {
            //                end = mainwindow.card_2_7.TranslatePoint(new Point(), mainwindow.MySpace);

            //            }
            //            else if (mainwindow.card_2_8.Children.Count == 0)
            //            {
            //                end = mainwindow.card_2_8.TranslatePoint(new Point(), mainwindow.MySpace);

            //            }
            //            else if (mainwindow.card_2_9.Children.Count == 0)
            //            {
            //                end = mainwindow.card_2_9.TranslatePoint(new Point(), mainwindow.MySpace);

            //            }
            //            else if (mainwindow.card_2_10.Children.Count == 0)
            //            {
            //                end = mainwindow.card_2_10.TranslatePoint(new Point(), mainwindow.MySpace);

            //            }
            //            if (end == new Point(0, 0))
            //            {
            //                MessageBox.Show("对方场地已满");
            //                return;
            //            }
            //            else
            //            {
            //                if (card.RenderTransform.GetValue(RotateTransform.AngleProperty).Equals(0.0))
            //                {
            //                    MyStoryboard msb = CardAnimation.Rotate(card, 0, 180);
            //                    msb.Completed += (object sender, EventArgs e) =>
            //                    {
            //                        msb.card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //                        msb.card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);
            //                        //storyboard = null;
            //                    };
            //                    msb.Begin();
            //                }

            //                //CardAnimation.Rotate(card,)
            //            }
            //            //if () MessageBox.Show(对方场地已满);
            //            break;
            //            #endregion
            //        }
            //    default: break;




            //}

            ////判断是否转移控制权，是则不转换状态
            //if(!to.Equals("对手前场"))
            //{
            //    //不是背面
            //    if (!card.isBack) 
            //    {
            //        //非防守
            //        if (to.Equals("卡组") || to.Equals("额外"))
            //        {
            //            card.isBack = true;
            //            CardAnimation.RotateOut(card, 0, 0);
            //            card.SetPic();
            //        }
            //        //防守
            //        if (card.isDef)
            //        {
            //            card.isDef = false;
            //            if (to.Equals("卡组") || to.Equals("额外"))
            //            {
            //                card.isBack = true;
            //                CardAnimation.RotateOut(card, 90, 0);
            //                card.SetPic();
                    
            //            }
            //            else
            //            {
            //                MyStoryboard msb =  CardAnimation.Rotate(card, 90, 0);
            //                msb.Completed += (object sender, EventArgs e) =>
            //                {
            //                    msb.card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //                    msb.card.RenderTransform.SetValue(RotateTransform.AngleProperty, (double)0);
            //                    //storyboard = null;
            //                };
            //                msb.Begin();
            //            }
                    
            //        }
            //    }
            //    //背面
            //    else if (card.isBack) 
            //    {

            //        if (!to.Equals("卡组") || to.Equals("额外")) card.isBack = false;
            //        if (card.isDef) CardAnimation.RotateOut(card, 90, 0);
            //        else if (!card.isDef) CardAnimation.RotateOut(card, 0, 0);
            //        card.SetPic();
            //    }
            //}



            

            ////获得卡片所在父控件并解离
            //Base.getawayParerent(card);

            ////加入移动面板Canvas，并设置起点
            //mainwindow.MySpace.Children.Add(card);
            //Canvas.SetLeft(card, start.X);
            //Canvas.SetTop(card, start.Y);

            ////执行移动动画且加入目标控件
            //CardAnimation.MoveAnimation(card, start, end,to,200);

            //switch (to)
            //{
            //    case "墓地":
            //        {
            //            if (cv.Children.Count > 2)
            //            {
            //                Card temp = cv.Children[cv.Children.Count - 1] as Card;
            //                if (temp.sCardType.Equals("XYZ怪兽"))
            //                {
            //                    CardOperate.sort_Canvas(cv);
            //                }
            //                else
            //                {
                                
            //                }
            //            }
            //            else
            //            {
            //                CardOperate.sort_Canvas(cv);
            //            }
                        
            //        }
                    
            //        break;
            //    case "":

            //        break;
            //    default:
            //        break;
            //}

            //if (cv.Name.Equals("card_hand"))
            //{

            //    CardOperate.sort_HandCard(mainwindow.card_1_hand);
            //}

        }

        #endregion

        #region <-- 送入对手墓地 -->

        /*
         * 送入对手墓地
         */
        private static void send2OpGraveyard(Card card, string to, int time)
        {
            //Canvas cv = card.Parent as Canvas;

            //Point start = card.TranslatePoint(new Point(), mainwindow.MySpace);
            //Point end = mainwindow.card_2_Graveyard.TranslatePoint(new Point(),mainwindow.MySpace);

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
            //    start.X = start.X + ((cv.ActualWidth - card.ActualWidth) / 2) - ((cv.ActualWidth - card.ActualHeight) / 2); ;
            //    start.Y = start.Y - card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2);

            //    end.X = (cv.ActualWidth - card.ActualWidth) / 2;
            //    end.Y = end.Y - card.ActualHeight;

            //    Canvas.SetTop(card, start.Y);
            //    Canvas.SetLeft(card, start.X);

            //    rotate = new RotateTransform(-90);

            //    MyStoryboard msb1 = CardAnimation.scalX_120(card, 100);
            //    msb1.Completed += (object sender, EventArgs e) =>
            //    {
            //        msb1.card.isBack = false;
            //        msb1.card.isDef = false;
            //        card.SetPic();
            //    };

            //    tls.Animates.Add(msb1);

            //    MyStoryboard msb2 = CardAnimation.Send2OpGraveyard1(card, start, end,-90,-180, 200);
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

            //        mainwindow.card_2_Graveyard.Children.Add(card);

            //        CardOperate.sort_SingleCard(card);
            //    };

            //    tls.Animates.Add(msb2);
            //}
            ////表守
            //else if (card.isDef && !card.isBack)
            //{
            //    start.X = start.X + ((cv.ActualWidth - card.ActualWidth) / 2) - ((cv.ActualWidth - card.ActualHeight) / 2);
            //    start.Y = start.Y - card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2);

            //    end.X = (cv.ActualWidth - card.ActualWidth) / 2;
            //    end.Y = end.Y - card.ActualHeight;

            //    Canvas.SetTop(card, start.Y);
            //    Canvas.SetLeft(card, start.X);

            //    rotate = new RotateTransform(-90);

            //    MyStoryboard msb1 = CardAnimation.Send2OpGraveyard2(card, start, end,-90,-180, 300);
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

            //        mainwindow.card_2_Graveyard.Children.Add(msb1.card);

            //        CardOperate.sort_SingleCard(msb1.card);

                    
            //    };
            //    tls.Animates.Add(msb1);

            //}
            ////表攻
            //else if (!card.isDef && !card.isBack)
            //{
            //    start.X = start.X + ((cv.ActualWidth - card.ActualWidth) / 2) - ((cv.ActualWidth - card.ActualHeight) / 2);
            //    start.Y = start.Y - card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2);

            //    end.X = (cv.ActualWidth - card.ActualWidth) / 2;
            //    end.Y = end.Y - card.ActualHeight;

            //    Canvas.SetTop(card, start.Y);
            //    Canvas.SetLeft(card, start.X);

            //    rotate = new RotateTransform(-90);

            //    MyStoryboard msb1 = CardAnimation.Send2OpGraveyard2(card, start,end,0,-180, 150);
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

            //        mainwindow.card_2_Graveyard.Children.Add(msb1.card);

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
        }

        #endregion

        #region <-- 转移控制权 -->

        /*
         * 转移控制权
         */
        private static void control_Change(Card card, int time)
        {
            //Canvas cv = card.Parent as Canvas;

            //int cv_childre_num = cv.Children.IndexOf(card);

            //Canvas cv_aim = null;

            //Point start = card.TranslatePoint(new Point(), mainwindow.MySpace);
            


            //if (mainwindow.card_2_6.Children.Count == 0)
            //{
            //    cv_aim = mainwindow.card_2_6;
            
            //}
            //else if (mainwindow.card_2_7.Children.Count == 0)
            //{
            //    cv_aim = mainwindow.card_2_7;
     
            //}
            //else if (mainwindow.card_2_8.Children.Count == 0)
            //{
            //    cv_aim = mainwindow.card_2_8;
                   
            //}
            //else if (mainwindow.card_2_9.Children.Count == 0)
            //{
            //    cv_aim = mainwindow.card_2_9;
            //}
            //else if (mainwindow.card_2_10.Children.Count == 0)
            //{
            //    cv_aim = mainwindow.card_2_10;
                
                
            //}
            //if (cv_aim == null)
            //{
            //    MessageBox.Show("对方场地已满");
            //    //cv_aim = null;
            //    return;
            //}
            //else
            //{
            //    OpponentOperate.ActionAnalyze("ControlChange=" + DuelReportOperate.Analyze_canvas(cv.Name) + "," + cv_childre_num, true);

            //    Point end = cv_aim.TranslatePoint(new Point(), mainwindow.MySpace);
            //    end.X = end.X - cv_aim.ActualWidth + ((cv_aim.ActualWidth - card.ActualWidth) / 2);
            //    end.Y = end.Y - cv_aim.ActualHeight; 

            //    Base.getawayParerent(card);
            //    mainwindow.MySpace.Children.Add(card);

            //    RotateTransform rotate = new RotateTransform();
            //    ScaleTransform scale = new ScaleTransform();
            //    TranslateTransform translate = new TranslateTransform();
            //    TransformGroup group = new TransformGroup();

            //    //start.X = start.X + ((cv.ActualWidth - card.ActualWidth) / 2) - ((cv.ActualWidth - card.ActualHeight) / 2);
            //    //start.Y = start.Y - card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2);

            //    //end.X = (cv.ActualWidth - card.ActualWidth) / 2;
            //    //end.Y = end.Y - card.ActualHeight;

                

            //    if (card.isDef)
            //    {
            //        rotate = new RotateTransform(-90);
            //        start.X = start.X + ((cv.ActualWidth - card.ActualWidth) / 2) -((cv.ActualWidth - card.ActualHeight) / 2);
            //        start.Y = start.Y - card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2);
            //    }

            //    Canvas.SetTop(card, start.Y);
            //    Canvas.SetLeft(card, start.X);

            //    group.Children.Add(scale);
            //    group.Children.Add(rotate);
            //    group.Children.Add(translate);
            //    card.RenderTransform = group;

            //    MyStoryboard msb1 = null;
            //    if(!card.isDef) msb1 = CardAnimation.Send2OpGraveyard2(card, start, end, 0, -180, time);
            //    if (card.isDef) msb1 = CardAnimation.Send2OpGraveyard2(card, start, end, -90, -270, time);
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

            //        if (card.isDef)
            //        {
            //            card.RenderTransform = new RotateTransform(-90);
            //        }

            //        cv_aim.Children.Add(msb1.card);

            //        CardOperate.sort_Canvas(cv_aim);
            //        //CardOperate.CardSortsingle(cv_aim, card, 56, 81);

            //        mainwindow.report.Text += ("将 " + DuelReportOperate.from(cv.Name) + " [" + card.name + "] 转移控制权" + Environment.NewLine);
            //    };
            //    msb1.Begin();
            //}

            
        }

        #endregion

        #region <-- 从游戏中除外,解放,将顶牌从游戏中除外,将顶牌背面除外 -->

        /* 
         * 从游戏中除外 
         * 解放
         * 将顶牌从游戏中除外
         * 将顶牌背面除外
         */
        private static void send2where_gone(Card card,object aim_controls,bool isback)
        {
           // if (isback)
           // {
           //     CardOperate.card_BackAtk(card);
           // }
           // else
           // {
           //     CardOperate.card_FrontAtk(card);
           // }

           // //Canvas cv = aim_controls as Canvas;
           // //if (cv.Name.Equals("card_1_Extra")) CardAnimation.RotateOut(card, 0, 90);
            
           //// Base.GetawayParerent(card);
           // CardAnimation.FadeOut(card, aim_controls,isback);
            
           //// mainwindow.card_1_Outside.Children.Add(card);
           // //CardAnimation.FadeIn(card);
        }

        #endregion

        #region <-- 卡组洗切 -->

        /*
         * 卡组洗切
         * 
         */
        private static void shuffle_card(string where)
        {
            List<Card> cards = new List<Card>();

            switch(where)
            {
                case "卡组":
                    foreach (Card card in mainwindow.card_1_Deck.Children)
                    {
                        cards.Add(card);
                        //mainwindow.card_1_Deck.Children.Remove(card);
                    }
                    foreach (Card card in cards)
                    {
                        mainwindow.card_1_Deck.Children.Remove(card);
                    }

                    break;
                case "手卡": break;
            }

            //Console.WriteLine( mainwindow.card_1_Deck.Children.);
            shuffle<Card>(cards);

            switch (where)
            {
                case "卡组":
                    foreach (Card card in cards)
                    {
                        mainwindow.card_1_Deck.Children.Add(card);
                    }
                    break;
                case "手卡": break;
            }
            
        }

        #endregion

        #region <-- 洗切 -->

        private static void shuffle<T>(List<T> list)
        {
            Random random = new Random();
            for (int i = 1; i < list.Count; i++)
            {
                Swap<T>(list, i, random.Next(0, i));
            }
        }

        private static void Swap<T>(List<T> cards, int indexA, int indexB)
        {
            T temp = cards[indexA];
            cards[indexA] = cards[indexB];
            cards[indexB] = temp;
        }

        #endregion

    }
}
