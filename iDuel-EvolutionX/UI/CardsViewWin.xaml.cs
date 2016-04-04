using iDuel_EvolutionX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace iDuel_EvolutionX.UI
{

    
     
    /// <summary>
    /// CardsView.xaml 的交互逻辑
    /// </summary>
    public partial class CardsViewWin : Window
    {
        MyCanvas mcv_from;


        public CardsViewWin(MyCanvas mcv_from)
        {
            InitializeComponent();
            this.tb_title.Text = getWinTitileByAreaEnum(mcv_from.area);
            this.mcv_from = mcv_from;
            this.mcv.WhenRemoveChildren += removeFormMcv;
        }

        public void removeFormMcv(MyCanvas cv, CardUI card)
        {  
            setCardAutoFit();
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CloseWin(object sender, RoutedEventArgs e)
        {
            closeWin();
        }

        private void closeWin()
        {
            mcv.WhenRemoveChildren -= removeFormMcv;
            mcv_from.WhenAddChildren -= CardAreaEvent.add2Deck;
            MyStoryboard msb = CardAnimation.scalXY_120();
            msb.Completed += (object c, EventArgs d) =>
            {

                while (mcv.Children.Count != 0)
                {
                    Random r = new Random(40);
                    int which = r.Next(mcv.Children.Count);
                    CardUI card = mcv.Children[which] as CardUI;
                    card.getAwayFromParents();
                    switch (mcv_from.area)
                    {
                        case Area.MAINDECK:
                            card.set2BackAtk();
                            break;
                        case Area.GRAVEYARD:
                            break;
                        case Area.BANISH:
                            break;
                        case Area.SPACE:
                            break;
                        case Area.EXTRA:
                            break;
                        case Area.MONSTER_1:
                            break;
                        case Area.MONSTER_2:
                            break;
                        case Area.MONSTER_3:
                            break;
                        case Area.MONSTER_4:
                            break;
                        case Area.MONSTER_5:
                            break;
                        default:
                            break;
                    }

                    mcv_from.Children.Add(card);
                    card.BeginAnimation(Canvas.LeftProperty, null);
                    card.BeginAnimation(Canvas.TopProperty, null);
                    card.centerAtVerticalInParent();
                }
                mcv_from.WhenAddChildren += CardAreaEvent.add2Deck;
                mcv_from.AllowDrop = true;
                MyStoryboard msb0 = CardAnimation.scalXY_021(mcv_from.Children);
                //msb0.FillBehavior = System.Windows.Media.Animation.FillBehavior.Stop;
                msb0.Completed += (object c0, EventArgs d0) =>
                {
                    this.Close();

                };
                msb0.Begin();
            };
            msb.Begin(this);
        }

        /// <summary>
        /// 初始化加载要预览的卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            MyStoryboard msb = CardAnimation.scalXY_120(mcv_from.Children);
            msb.FillBehavior = System.Windows.Media.Animation.FillBehavior.Stop;
            msb.Completed += (object c, EventArgs d) =>
            {
                Point start = new Point((this.ActualWidth - 56) / 2, (this.ActualHeight - 80 - 80) / 2);
                while (mcv_from.Children.Count != 0)
                {
                    CardUI card = mcv_from.Children[0] as CardUI;
                    card.getAwayFromParents();
                    mcv.Children.Add(card);
                    card.set2FrontAtk();
                    card.BeginAnimation(Canvas.LeftProperty, null);
                    card.BeginAnimation(Canvas.TopProperty, null);
                    Canvas.SetLeft(card, start.X);
                    Canvas.SetTop(card, start.Y);

                }
                setCardAutoFit();
            };
            msb.Begin();
     
        }

        
        /// <summary>
        /// 卡片自适应
        /// </summary>
        private void setCardAutoFit()
        {
            double perrow = 9;

            int rows = (int)Math.Ceiling(mcv.Children.Count / perrow) ;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; perrow * i + j < mcv.Children.Count && perrow * i + j < perrow * (i+1); j++)
                {
                    //Console.WriteLine(perrow * i + j);
                    CardUI card = mcv.Children[(int)perrow * i + j] as CardUI;
                    if (card != null)
                    {
                        Point start = new Point(Canvas.GetLeft(card), Canvas.GetTop(card));
                        Point end = new Point(5 * (j + 1) + 56 * j, 5 * (i + 1) + 81 * i);
                        if (start.X != end.X || start.Y != end.Y)
                        {
                            card.Tag = end;
                            MyStoryboard msb = CardAnimation.CanvasXY(start, end, 300);
                            msb.card = card;
                            msb.Completed += (object c, EventArgs d) =>
                            {
                                msb.card.BeginAnimation(Canvas.LeftProperty, null);
                                msb.card.BeginAnimation(Canvas.TopProperty, null);
                                Point set = (Point)msb.card.Tag;
                                Canvas.SetLeft(msb.card, set.X);
                                Canvas.SetTop(msb.card, set.Y);
                            };
                            msb.FillBehavior = System.Windows.Media.Animation.FillBehavior.Stop;
                            msb.Begin(card);
                        }

                    }
                }
            }

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is CardUI)
            {
                return;
            }
            
            this.DragMove();
        }

        /// <summary>
        /// 根据AreaEnum获取对应的中文标题
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        private string getWinTitileByAreaEnum(Area area)
        {
            switch (area)
            {
                case Area.GRAVEYARD:
                    return "己方墓地";
                case Area.MAINDECK:
                    return "己方卡组";
                case Area.BANISH:
                    return "己方除外";
                case Area.EXTRA:
                    return "己方额外";
                case Area.MONSTER_1:
                    return "怪物区 左1";
                case Area.MONSTER_2:
                    return "怪物区 左2";
                case Area.MONSTER_3:
                    return "怪物区 中间";
                case Area.MONSTER_4:
                    return "怪物区 右2";
                case Area.MONSTER_5:
                    return "怪物区 右1";
                default:
                    return "";
            }
        }

        //private void Window_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Escape)
        //    {
        //        closeWin();
        //    }
        //}

    }
}
