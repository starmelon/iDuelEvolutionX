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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace iDuel_EvolutionX.UI
{
    /// <summary>
    /// CardsView.xaml 的交互逻辑
    /// </summary>
    public partial class CardsView : Window
    {
        MyCanvas mcv_from;

        public CardsView(MyCanvas mcv_from)
        {
            InitializeComponent();
            this.mcv_from = mcv_from;
            mcv.WhenRemoveChildren += removeFormMcv;
        }

        public void removeFormMcv(MyCanvas cv, CardUI card)
        {  
            sort2();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mcv.WhenRemoveChildren -= removeFormMcv;
            while (mcv.Children.Count!=0)
            {
                Random r = new Random(40);
                int which = r.Next(mcv.Children.Count);
                CardUI card = mcv.Children[which] as CardUI;
                card.getAwayFromParents();
                card.set2BackAtk();
                mcv_from.Children.Add(card);
                card.centerAtVerticalInParent();
            }
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int num = mcv_from.Children.Count;
            while (mcv_from.Children.Count != 0)
            {
                CardUI card = mcv_from.Children[0] as CardUI;
                card.getAwayFromParents();
                mcv.Children.Add(card);
                card.set2FrontAtk();

            }

            
            sort();
        }

        private void sort()
        {
            double perrow = 10;
            Point start = new Point((this.ActualWidth - 56)/2, (this.ActualHeight-80 - 80)/2);
            for (int i = 0; i < Math.Ceiling(mcv.Children.Count / perrow); i++)
            {
                //for (int j = 0; j < ((i == Math.Ceiling(cv.Children.Count / 9.0) - 1) ? cv.Children.Count % 9 : 9); j++)
                for (int j = 0; perrow * i + j < mcv.Children.Count && perrow * i + j < perrow * (i + 1); j++)
                {

                    CardUI card = mcv.Children[(int)perrow * i + j] as CardUI;
                    if (card != null)
                    {
                        Point end = new Point(5 * (j + 1) + 56 * j, 5 * (i + 1) + 81 * i);

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

                    //Canvas.SetLeft(card, 5 * (j + 1) + 56 * j);
                    //Canvas.SetTop(card, 5 * (i + 1) + 81 * i);
                }
            }
        }

        private void sort2()
        {
            double perrow = 10;
            for (int i = 0; i < 60; i++)
            {
                //Console.WriteLine((int)Math.Ceiling(i / perrow) + " "+ i);
            }
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
                            msb.Begin(card);
                        }

                    }
                }
            }

            //for (int i = 0; i < Math.Ceiling(mcv.Children.Count / perrow); i++)
            //{

            //    //for (int j = 0; j < ((i == Math.Ceiling(cv.Children.Count / 9.0) - 1) ? cv.Children.Count % 9 : 9); j++)
            //    for (int j = 0; perrow * i + j < perrow * (i+1); j++)
            //    {

            //        CardUI card = mcv.Children[(int)perrow * i + j] as CardUI;
            //        if (card != null)
            //        {
            //            Point start = new Point(Canvas.GetLeft(card), Canvas.GetTop(card));
            //            Point end = new Point(5 * (j + 1) + 56 * j, 5 * (i + 1) + 81 * i);
            //            if (start.X != end.X || start.Y != end.Y )
            //            {
            //                card.Tag = end;
            //                MyStoryboard msb = CardAnimation.CanvasXY(start, end, 300);
            //                msb.card = card;
            //                msb.Completed += (object c, EventArgs d) =>
            //                {
            //                    msb.card.BeginAnimation(Canvas.LeftProperty, null);
            //                    msb.card.BeginAnimation(Canvas.TopProperty, null);
            //                    Point set = (Point)msb.card.Tag;
            //                    Canvas.SetLeft(msb.card, set.X);
            //                    Canvas.SetTop(msb.card, set.Y);
            //                };
            //                msb.Begin(card);
            //            }
                        
            //        }

            //        //Canvas.SetLeft(card, 5 * (j + 1) + 56 * j);
            //        //Canvas.SetTop(card, 5 * (i + 1) + 81 * i);
            //    }
            //}
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is CardUI)
            {
                return;
            }
            
            this.DragMove();
        }

        private void mcv_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
