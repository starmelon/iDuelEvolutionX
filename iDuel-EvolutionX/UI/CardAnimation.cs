using iDuel_EvolutionX;
using iDuel_EvolutionX.Model;
using iDuel_EvolutionX.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace iDuel_EvolutionX.UI
{
    class CardAnimation
    {
        public static MainWindow mainwindow;
        //public static CardView cardview;


        public static void ControlChange(Card card, Point start, Point end,double start_angle,double end_engle ,int time)
        {
            
        }

        

        

        public static MyStoryboard FadeOut(double time)
        {
            MyStoryboard msb = new MyStoryboard();
            DoubleAnimationUsingKeyFrames keyFramesAnimation = new DoubleAnimationUsingKeyFrames();
            keyFramesAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(time));

            Storyboard.SetTargetProperty(keyFramesAnimation, new PropertyPath("Opacity"));

            LinearDoubleKeyFrame keyFram = new LinearDoubleKeyFrame();
            keyFram.Value = 0;
            keyFram.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(time));
            keyFramesAnimation.KeyFrames.Add(keyFram);

            msb.Children.Add(keyFramesAnimation);

            return msb;
        }

        public static MyStoryboard FadeIn(double time)
        {
            MyStoryboard msb = new MyStoryboard();

            DoubleAnimationUsingKeyFrames keyFramesAnimation = new DoubleAnimationUsingKeyFrames();
            keyFramesAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(time));
            Storyboard.SetTargetProperty(keyFramesAnimation, new PropertyPath("Opacity"));

            //0秒位置是0
            LinearDoubleKeyFrame keyFram = new LinearDoubleKeyFrame();
            keyFram.Value = 0;
            keyFram.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0));
            keyFramesAnimation.KeyFrames.Add(keyFram);

            //1秒位置是1
            LinearDoubleKeyFrame keyFram2 = new LinearDoubleKeyFrame();
            keyFram2.Value = 1;
            keyFram2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(time));
            keyFramesAnimation.KeyFrames.Add(keyFram2);

            msb.Children.Add(keyFramesAnimation);

            return msb;
        }

        

        


        /// <summary>
        /// 卡片移动动画2(观战，对手场)
        /// </summary>
        /// <param name="card">卡片实例</param>
        /// <param name="start">起始位置</param>
        /// <param name="end">终止位置</param>
        /// <param name="time">运行时间</param>
        public static MyStoryboard MoveAnimation2(Card card, Point start, Point end, int time)
        {

            //新建动画故事版
            MyStoryboard sb = new MyStoryboard();
            //sb.card = card;

            ////TranslateTransform tlt = new TranslateTransform();
            ////card.RenderTransform = tlt;

            ////设定X和Y坐标的方向动画
            //DoubleAnimation xA = new DoubleAnimation(start.X, end.X, TimeSpan.FromMilliseconds(time));
            //DoubleAnimation yA = new DoubleAnimation(start.Y, end.Y, TimeSpan.FromMilliseconds(time));

            ////把方向动画加入故事版
            //sb.Children.Add(xA);
            //sb.Children.Add(yA);

            ////关联操作的卡片和方向动画
            //Storyboard.SetTarget(xA, card);
            //Storyboard.SetTarget(yA, card);

            ////DependencyProperty[] propertyChainX = new DependencyProperty[]
            ////{
            ////    Card.RenderTransformProperty,
            ////    TranslateTransform.XProperty
            ////};

            ////DependencyProperty[] propertyChainY = new DependencyProperty[]
            ////{
            ////    Card.RenderTransformProperty,
            ////    TranslateTransform.YProperty
            ////};

            ////关联具体要执行动画的依赖属性
            ////Storyboard.SetTargetProperty(xA, new PropertyPath("(0).(1)",propertyChainX));
            ////Storyboard.SetTargetProperty(yA, new PropertyPath("(0).(1)",propertyChainY));
            //Storyboard.SetTargetProperty(xA, new PropertyPath("(Canvas.Left)"));
            //Storyboard.SetTargetProperty(yA, new PropertyPath("(Canvas.Top)"));

           
            return sb;
        }



        #region



        /// <summary>
        /// 表示形式变更
        /// 攻→守
        /// </summary>
        /// <param name="card"></param>
        public static void Rotate2FrontDef(CardUI card)
        {
            //setAnimePrepare(card);

            MyStoryboard msb = Rotate_A2D();
            msb.card = card;
            msb.Completed +=(sender, e) => {
                msb.card.Status = Status.FRONT_DEF;
                msb.card.set2FrontDef();
                MyCanvas mcv = card.Parent as MyCanvas;
                CardOperate.sort_XYZ_def(mcv);
            };
            msb.FillBehavior = FillBehavior.Stop;
            TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();
            animator.Animates.Add(msb);
            animator.Begin(card);

        }

        /// <summary>
        /// 表示形式变更
        /// 守→攻
        /// </summary>
        /// <param name="card"></param>
        public static void Rotate2FrontAtk(CardUI card)
        {
            MyStoryboard msb = Rotate_D2A();
            msb.card = card;
            msb.Completed += (sender, e) => {
                msb.card.Status = Status.FRONT_ATK;
                msb.card.set2FrontAtk();
                MyCanvas mcv = card.Parent as MyCanvas;
                CardOperate.sort_XYZ_def(mcv);
            };
            msb.FillBehavior = FillBehavior.Stop;
            TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();
            animator.Animates.Add(msb);
            animator.Begin(card);

        }

        /// <summary>
        /// 表示形式变更
        /// 旋转→背面防守
        /// </summary>
        /// <param name="card"></param>
        public static void Rotate2BackDef(CardUI card)
        {
            MyStoryboard msb = Rotate_A2D();
            msb.card = card;
            msb.Completed += (sender, e) => {
                msb.card.Status = Status.BACK_DEF;
                msb.card.set2BackDef();
                MyCanvas mcv = card.Parent as MyCanvas;
                CardOperate.sort_XYZ_def(mcv);
            };
            msb.FillBehavior = FillBehavior.Stop;
            TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();
            animator.Animates.Add(msb);
            animator.Begin(card);

        }

        /// <summary>
        /// 表示形式变更
        /// 里守→表攻
        /// </summary>
        /// <param name="card"></param>
        public static void Rotate2FrontAtk2(CardUI card)
        {
            //setAnimePrepare(card);

            MyStoryboard msb0 = scalX_120_rotate_9020();
            msb0.card = card;
            msb0.Completed += (object c, EventArgs d) =>
            {
                msb0.card.Status = Status.FRONT_ATK;
            };
            MyStoryboard msb1 = scalX_021();
            msb1.Completed += (object c, EventArgs d) =>
            {
                MyCanvas mcv = card.Parent as MyCanvas;
                CardOperate.sort_XYZ_def(mcv);
            };

            TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();
            animator.addAnime(msb0).addAnime(msb1).Begin(card);

        }

        /// <summary>
        /// 表示形式变更
        /// →背面防守
        /// </summary>
        /// <param name="card"></param>
        public static void turn2BackDef(CardUI card)
        {
            TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();

            MyStoryboard msb0 = null;
            switch (card.Status)
            {
                case Status.FRONT_ATK:
                    msb0 = scalX_120_rotate_0290();

                    break;
                case Status.FRONT_DEF:
                    msb0 = scalX_120();
                    break;
                case Status.BACK_ATK:
                    msb0 = Rotate_A2D();
                    msb0.Completed += (object c, EventArgs d) =>
                    {
                        MyCanvas mcv = card.Parent as MyCanvas;
                        CardOperate.sort_XYZ_def(mcv);
                    };
                    break;
            }

            msb0.card = card;
            msb0.FillBehavior = FillBehavior.Stop;
            msb0.Completed += (object c, EventArgs d) =>
            {
                msb0.card.set2BackDef();
                
            };
            animator.addAnime(msb0);

            if (card.Status != Status.BACK_ATK)
            {
                MyStoryboard msb1 = scalX_021();
                msb1.Completed += (object c, EventArgs d) =>
                {
                    MyCanvas mcv = card.Parent as MyCanvas;
                    CardOperate.sort_XYZ_def(mcv);
                };
                animator.addAnime(msb1);

            }




            animator.Begin(card);

        }

        public static void Rotate_Scale_FadeInAndOut(FrameworkElement uie)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(Rotate(0, 180, 1100));
            msb.Children.Add(scaleX(0, 1.5, 900));
            msb.Children.Add(ScaleY(0, 1.5, 900));
            msb.Children.Add(Opacity(1,0, 1100));

            msb.FillBehavior = FillBehavior.Stop;

            msb.Begin(uie); 
        }


        /// <summary>
        /// 表示形式改变
        /// 正面→背面
        /// </summary>
        /// <param name="card"></param>
        public static void turn2Back(CardUI card)
        {
            //setAnimePrepare(card);

            MyStoryboard msb0 =  scalX_120();
            msb0.card = card;
            msb0.Completed += (object c, EventArgs d) =>
            {
                switch (msb0.card.Status)
                {
                    case Status.FRONT_ATK:
                        msb0.card.Status = Status.BACK_ATK;
                        break;
                    case Status.FRONT_DEF:
                        msb0.card.Status = Status.BACK_DEF;
                        break;
                    default:
                        break;
                }
            };
            MyStoryboard msb1 = scalX_021();

            TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();
            animator.addAnime(msb0).addAnime(msb1).Begin(card);

        }

        /// <summary>
        /// 表示形式变更
        /// 背面→正面
        /// </summary>
        /// <param name="card"></param>
        public static void turn2Front(CardUI card)
        {
            //setAnimePrepare(card);

            MyStoryboard msb0 = scalX_120();
            msb0.card = card;
            msb0.Completed += (object c, EventArgs d) =>
            {
                switch (msb0.card.Status)
                {
                    case Status.BACK_ATK:
                        msb0.card.Status = Status.FRONT_ATK;
                        break;
                    case Status.BACK_DEF:
                        msb0.card.Status = Status.FRONT_DEF;
                        break;
                    default:
                        break;
                }
            };
            MyStoryboard msb1 = scalX_021();

            TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();
            animator.addAnime(msb0).addAnime(msb1).Begin(card);

        }


        



        public static void move2Graveyard(CardUI card)
        {

            TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();

            MainWindow main = Application.Current.MainWindow as MainWindow;
            MyCanvas mcv = card.Parent as MyCanvas;
            Point start = card.TranslatePoint(new Point(), main.Battle);
            Point end = main.card_1_Graveyard.TranslatePoint(new Point(), main.Battle);
            end.X += (main.card_1_Graveyard.ActualWidth - card.Width) / 2;
            end.Y += (main.card_1_Graveyard.ActualHeight - card.Height) / 2;

            if (card.Status == Status.BACK_DEF || card.Status == Status.FRONT_DEF)
            {


                start.X += (mcv.ActualHeight - card.Width) / 2 - (mcv.ActualWidth - card.Height) / 2;
                start.Y += -card.Width - (mcv.ActualHeight - card.Width) / 2 + (mcv.ActualWidth - card.Height) / 2;


                card.getAwayFromParents();
                Canvas.SetLeft(card, start.X);
                Canvas.SetTop(card, start.Y);
                main.Battle.Children.Add(card);
                MyStoryboard msb = Rotate_CanvasXY(-90, 0, start, end, 300, 300);
                //MyStoryboard msb = CanvasXY(start, end, 500);
                msb.card = card;
                msb.Completed += (object c, EventArgs d) =>
                {
                    msb.card.BeginAnimation(Canvas.LeftProperty, null);
                    msb.card.BeginAnimation(Canvas.TopProperty, null);
                    msb.card.getAwayFromParents();
                    switch (card.Status)
                    {
                        case Status.FRONT_DEF:
                            msb.card.set2FrontAtk();
                            break;
                        case Status.BACK_DEF:
                            msb.card.set2BackAtk();
                            break;
                        default:
                            break;
                    }
                    
                    main.card_1_Graveyard.Children.Add(msb.card);
                };
                msb.Begin(card);

                //MyStoryboard msb = Rotate_CanvasXY(-90,0,)
            }
            else
            {

                card.getAwayFromParents();
                Canvas.SetLeft(card, start.X);
                Canvas.SetTop(card, start.Y);
                main.Battle.Children.Add(card);
                MyStoryboard msb = CanvasXY(start, end, 500);
                msb.card = card;
                msb.Completed += (object c, EventArgs d) =>
                {
                    msb.card.BeginAnimation(Canvas.LeftProperty, null);
                    msb.card.BeginAnimation(Canvas.TopProperty, null);
                    msb.card.getAwayFromParents();
                    main.card_1_Graveyard.Children.Add(msb.card);
                };
                msb.Begin(card);
            }


            if (card.Status != Status.FRONT_ATK)
            {
                
            }
            

        }

        

        public static void move2MainDeck(CardUI card)
        {

            TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();

            MainWindow main = Application.Current.MainWindow as MainWindow;
            MyCanvas mcv = card.Parent as MyCanvas;
            Point start = card.TranslatePoint(new Point(), main.Battle);
            Point end = main.card_1_Deck.TranslatePoint(new Point(), main.Battle);
            end.X += (main.card_1_Deck.ActualWidth - card.Width) / 2;
            end.Y += (main.card_1_Deck.ActualHeight - card.Height) / 2;

            if (card.Status == Status.BACK_DEF || card.Status == Status.FRONT_DEF)
            {


                start.X += (mcv.ActualHeight - card.Width) / 2 - (mcv.ActualWidth - card.Height) / 2;
                start.Y += -card.Width - (mcv.ActualHeight - card.Width) / 2 + (mcv.ActualWidth - card.Height) / 2;


                card.getAwayFromParents();
                Canvas.SetLeft(card, start.X);
                Canvas.SetTop(card, start.Y);
                main.Battle.Children.Add(card);
                MyStoryboard msb = Rotate_CanvasXY(-90, 0, start, end, 300, 300);
                //MyStoryboard msb = CanvasXY(start, end, 500);
                msb.card = card;
                msb.Completed += (object c, EventArgs d) =>
                {
                    msb.card.BeginAnimation(Canvas.LeftProperty, null);
                    msb.card.BeginAnimation(Canvas.TopProperty, null);
                    msb.card.getAwayFromParents();
                    switch (card.Status)
                    {
                        case Status.FRONT_DEF:
                            msb.card.set2FrontAtk();
                            break;
                        case Status.BACK_DEF:
                            msb.card.set2BackAtk();
                            break;
                        default:
                            break;
                    }

                    main.card_1_Deck.Children.Add(msb.card);
                };
                msb.Begin(card);

                //MyStoryboard msb = Rotate_CanvasXY(-90,0,)
            }
            else
            {

                card.getAwayFromParents();
                Canvas.SetLeft(card, start.X);
                Canvas.SetTop(card, start.Y);
                main.Battle.Children.Add(card);
                MyStoryboard msb = CanvasXY(start, end, 500);
                msb.card = card;
                msb.Completed += (object c, EventArgs d) =>
                {
                    msb.card.BeginAnimation(Canvas.LeftProperty, null);
                    msb.card.BeginAnimation(Canvas.TopProperty, null);
                    msb.card.getAwayFromParents();
                    main.card_1_Deck.Children.Add(msb.card);
                };
                msb.Begin(card);
            }


            if (card.Status != Status.FRONT_ATK)
            {

            }


        }

        private static void frontAtk2Graveyard(CardUI card)
        {
            MainWindow main = Application.Current.MainWindow as MainWindow;
            Point start = card.TranslatePoint(new Point(), main.Battle);
            Point end = main.card_1_Graveyard.TranslatePoint(new Point(), main.Battle);
            end.X += (main.card_1_Graveyard.ActualWidth - card.Width) / 2;
            end.Y += (main.card_1_Graveyard.ActualHeight - card.Height) / 2;
            card.getAwayFromParents();
            Canvas.SetLeft(card, start.X  );
            Canvas.SetTop(card, start.Y  );
            main.Battle.Children.Add(card);
            MyStoryboard msb = CanvasXY(start, end, 500);
            msb.card = card;
            msb.Completed += (object c, EventArgs d) =>
            {
                card.BeginAnimation(Canvas.LeftProperty, null);
                card.BeginAnimation(Canvas.TopProperty, null);
                card.getAwayFromParents();
                main.card_1_Graveyard.Children.Add(msb.card);
            };

            msb.Begin(card);
        }


        public static void fadeOut2FadeIn (CardUI card)
        {
            MyStoryboard msb0 = FadeOut(300);
            msb0.card = card;
            msb0.Completed += (object c, EventArgs d) =>
            {

                card.getAwayFromParents();
                card.set2FrontAtk();
                mainwindow.card_1_Outside.Children.Add(msb0.card);
            };
            MyStoryboard msb1 = FadeIn(300);

            TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();
            animator.addAnime(msb0).addAnime(msb1).Begin(card);
        }

        #region 多重卡片操作 2016.04.04

        public static MyStoryboard scalXY_021(UIElementCollection uic)
        {
            return scalXY(uic, 0, 1);
        }

        public static MyStoryboard scalXY_120(UIElementCollection uic)
        {
            return scalXY(uic, 1, 0);
        }

        public static MyStoryboard scalXY(UIElementCollection uic,double start,double end)
        {
            //新建动画故事版
            MyStoryboard msb = new MyStoryboard();

            foreach (CardUI card in uic)
            {
                //设定X和Y坐标的方向动画
                DoubleAnimation xA = new DoubleAnimation(start, end, TimeSpan.FromMilliseconds(150));
                DoubleAnimation yA = new DoubleAnimation(start, end, TimeSpan.FromMilliseconds(150));

                //关联具体要执行动画的依赖属性
                Storyboard.SetTargetProperty(xA, new PropertyPath("RenderTransform.Children[0].ScaleX"));
                Storyboard.SetTargetProperty(yA, new PropertyPath("RenderTransform.Children[0].ScaleY"));

                Storyboard.SetTarget(xA, card);
                Storyboard.SetTarget(yA, card);

                msb.Children.Add(xA);
                msb.Children.Add(yA);
            }

            return msb;
        }



        //public static MyStoryboard scalXY_021(UIElementCollection uic)
        //{
        //    //新建动画故事版
        //    MyStoryboard msb = new MyStoryboard();

        //    foreach (CardUI card in uic)
        //    {
        //        //设定X和Y坐标的方向动画
        //        DoubleAnimation xA = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(150));
        //        DoubleAnimation yA = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(150));

        //        //关联具体要执行动画的依赖属性
        //        Storyboard.SetTargetProperty(xA, new PropertyPath("RenderTransform.Children[0].ScaleX"));
        //        Storyboard.SetTargetProperty(yA, new PropertyPath("RenderTransform.Children[0].ScaleY"));

        //        Storyboard.SetTarget(xA, card);
        //        Storyboard.SetTarget(yA, card);

        //        msb.Children.Add(xA);
        //        msb.Children.Add(yA);
        //    }

        //    return msb;
        //}

        #endregion

        public static MyStoryboard scalXY_120()
        {
            //新建动画故事版
            MyStoryboard sb = new MyStoryboard();

            //设定X和Y坐标的方向动画
            DoubleAnimation xA = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(150));
            DoubleAnimation yA = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(150));

            //关联具体要执行动画的依赖属性
            Storyboard.SetTargetProperty(xA, new PropertyPath("RenderTransform.Children[0].ScaleX"));
            Storyboard.SetTargetProperty(yA, new PropertyPath("RenderTransform.Children[0].ScaleY"));

            sb.Children.Add(xA);
            sb.Children.Add(yA);

            return sb;
        }

        /// <summary>
        /// 卡片顺时针翻旋-P1
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static MyStoryboard scalX_120_rotate_2702180(Card card)
        {
            //ScaleTransform scaleTransform = new ScaleTransform();
            //scaleTransform.ScaleX = 1;
            //card.RenderTransform = scaleTransform;

            //新建动画故事版
            MyStoryboard sb = new MyStoryboard();

            ////设定X和Y坐标的方向动画
            //DoubleAnimation xA = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(150));
            //DoubleAnimation yA = new DoubleAnimation(-270, -180, TimeSpan.FromMilliseconds(150));

            //sb.Children.Add(xA);
            //sb.Children.Add(yA);
            ////sb.Children.Add(yA);
            ////sb.GetCurrentTime();

            ////关联操作的卡片和方向动画
            //Storyboard.SetTarget(xA, card);
            //Storyboard.SetTarget(yA, card);

            //DependencyProperty[] propertyChain = new DependencyProperty[]
            //{
            //    Card.RenderTransformProperty,
            //    //TransformGroup.ChildrenProperty,
                
            //    ScaleTransform.ScaleXProperty
            //   // TranslateTransform.XProperty
            //};


            ////关联具体要执行动画的依赖属性
            ////Storyboard.SetTargetProperty(xA, new PropertyPath("(0).(1)",propertyChain));
            //Storyboard.SetTargetProperty(xA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            //Storyboard.SetTargetProperty(yA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)"));
            ////Storyboard.SetTargetProperty(xA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(RotateTransform.Angle)"));

            //sb.card = card;

            return sb;
        }

        /// <summary>
        /// 卡片顺时针翻旋-P1
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static MyStoryboard scalX_120_rotate_9020()
        {
            //新建动画故事版
            MyStoryboard sb = new MyStoryboard();

            //设定X和Y坐标的方向动画
            DoubleAnimation xA = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(150));
            DoubleAnimation yA = new DoubleAnimation(-90, 0, TimeSpan.FromMilliseconds(150));

            //关联具体要执行动画的依赖属性
            Storyboard.SetTargetProperty(xA, new PropertyPath("RenderTransform.Children[0].ScaleX"));
            Storyboard.SetTargetProperty(yA, new PropertyPath("RenderTransform.Children[1].Angle"));

            sb.Children.Add(xA);
            sb.Children.Add(yA);

            return sb;
        }

        /// <summary>
        /// 卡片逆时针翻旋-P1
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static MyStoryboard scalX_120_rotate_0290()
        {

            //新建动画故事版
            MyStoryboard sb = new MyStoryboard();

            //设定X和Y坐标的方向动画
            DoubleAnimation xA = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(150));
            DoubleAnimation yA = new DoubleAnimation(0, -90, TimeSpan.FromMilliseconds(150));

            //关联具体要执行动画的依赖属性
            Storyboard.SetTargetProperty(xA, new PropertyPath("RenderTransform.Children[0].ScaleX"));
            Storyboard.SetTargetProperty(yA, new PropertyPath("RenderTransform.Children[1].Angle"));

            sb.Children.Add(xA);
            sb.Children.Add(yA);

            return sb;
        }

        /// <summary>
        /// 卡片翻转-P1
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static MyStoryboard scalX_120()
        {

            //新建动画故事版
            MyStoryboard sb = new MyStoryboard();

            //设定X和Y坐标的方向动画
            DoubleAnimation xA = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(150));

            EasingFunctionBase easing = new CubicEase()
            {
                EasingMode = EasingMode.EaseOut
            };
            xA.EasingFunction = easing;

            //关联具体要执行动画的依赖属性
            Storyboard.SetTargetProperty(xA, new PropertyPath("RenderTransform.Children[0].ScaleX"));

            sb.Children.Add(xA);

            return sb;
        }

        /// <summary>
        /// 卡片翻转-P2
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static MyStoryboard scalX_021()
        {
            //新建动画故事版
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(scaleX(0, 1, 150));

            return msb;
        }

        /// <summary>
        /// 旋转      
        /// </summary>
        /// <param name="card">旋转对象</param>
        /// <param name="angle_star">起始角度</param>
        /// <param name="angle_end">终止角度</param>
        public static MyStoryboard Rotate(Card card, double angle_star, double angle_end)
        {

            DoubleAnimation dbAscending = new DoubleAnimation(angle_star, angle_end, new Duration(TimeSpan.FromMilliseconds(300)));
            MyStoryboard storyboard = new MyStoryboard();

            //storyboard.Children.Add(dbAscending);
            //Storyboard.SetTarget(dbAscending, card);

            //DependencyProperty[] propertyChain = new DependencyProperty[]
            //{
            //    Card.RenderTransformProperty,
            //    TransformGroup.ChildrenProperty,
            //    RotateTransform.AngleProperty
            //    //ScaleTransform.ScaleXProperty
            //   // TranslateTransform.XProperty
            //};

            ////Storyboard.SetTargetProperty(dbAscending, new PropertyPath("(0).(1).(2)",propertyChain));
            //Storyboard.SetTargetProperty(dbAscending, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)"));

            //storyboard.Completed += (object sender, EventArgs e) =>
            //{
            //    card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);

            //    //ScaleTransform scaleTransform = new ScaleTransform();
            //    //scaleTransform.ScaleX = 1;
            //    //card.RenderTransform = scaleTransform;
            //    card.RenderTransform.SetValue(RotateTransform.AngleProperty, angle_end);
            //    //storyboard = null;
            //};

            //storyboard.card = card;

            return storyboard;
            //storyboard.Begin();
        }

        /// <summary>
        /// 里侧防守送往对方场地
        /// </summary>
        /// <param name="card"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static MyStoryboard Send2OpGraveyard1(Card card, Point start, Point end,double start_angle,double end_engle ,int time)
        {
            MyStoryboard sb = new MyStoryboard();
            

            ////TranslateTransform tlt = new TranslateTransform();
            ////card.RenderTransform = tlt;

            ////设定X和Y坐标的方向动画
            //DoubleAnimation xA = new DoubleAnimation(start.X, end.X, TimeSpan.FromMilliseconds(time));
            //DoubleAnimation yA = new DoubleAnimation(start.Y, end.Y, TimeSpan.FromMilliseconds(time));
            //DoubleAnimation sA = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(300));
            //DoubleAnimation rA = new DoubleAnimation(start_angle, end_engle, new Duration(TimeSpan.FromMilliseconds(300)));

            ////把方向动画加入故事版
            //sb.Children.Add(xA);
            //sb.Children.Add(yA);
            //sb.Children.Add(sA);
            //sb.Children.Add(rA);

            ////关联操作的卡片和方向动画
            //Storyboard.SetTarget(xA, card);
            //Storyboard.SetTarget(yA, card);
            //Storyboard.SetTarget(sA, card);
            //Storyboard.SetTarget(rA, card);

            ////关联具体要执行动画的依赖属性
            //Storyboard.SetTargetProperty(xA, new PropertyPath("(Canvas.Left)"));
            //Storyboard.SetTargetProperty(yA, new PropertyPath("(Canvas.Top)"));
            //Storyboard.SetTargetProperty(sA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            //Storyboard.SetTargetProperty(rA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)"));

            //sb.card = card;
            return sb;
        }

        /// <summary>
        /// 送往对方场地
        /// </summary>
        /// <param name="card"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static MyStoryboard Send2OpGraveyard2(Card card, Point start, Point end,double angele_star,double angle_end, int time)
        {
            MyStoryboard sb = new MyStoryboard();
            //sb.card = card;

            ////TranslateTransform tlt = new TranslateTransform();
            ////card.RenderTransform = tlt;

            ////设定X和Y坐标的方向动画
            //DoubleAnimation xA = new DoubleAnimation(start.X, end.X, TimeSpan.FromMilliseconds(time));
            //DoubleAnimation yA = new DoubleAnimation(start.Y, end.Y, TimeSpan.FromMilliseconds(time));
            //DoubleAnimation rA = new DoubleAnimation(angele_star, angle_end, new Duration(TimeSpan.FromMilliseconds(300)));

            ////把方向动画加入故事版
            //sb.Children.Add(xA);
            //sb.Children.Add(yA);
            //sb.Children.Add(rA);

            ////关联操作的卡片和方向动画
            //Storyboard.SetTarget(xA, card);
            //Storyboard.SetTarget(yA, card);
            //Storyboard.SetTarget(rA, card);

            ////关联具体要执行动画的依赖属性
            //Storyboard.SetTargetProperty(xA, new PropertyPath("(Canvas.Left)"));
            //Storyboard.SetTargetProperty(yA, new PropertyPath("(Canvas.Top)"));
            //Storyboard.SetTargetProperty(rA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)"));

            return sb;
        }

        #endregion

        public static MyStoryboard LifeChange(Rectangle life, double life_afterchange,double time)
        {

            MyStoryboard sb = new MyStoryboard();
            //sb.card = card;

            //TranslateTransform tlt = new TranslateTransform();
            //card.RenderTransform = tlt;

            EasingFunctionBase easing = new QuadraticEase()
            {
                
                EasingMode = EasingMode.EaseOut,       //公式
                //Oscillations =1,                           //滑过动画目标的次数
                //Springiness = 2                             //弹簧刚度
            };

            //设定X和Y坐标的方向动画
            DoubleAnimation xA = new DoubleAnimation(life.Width, life_afterchange, TimeSpan.FromMilliseconds(time));
            xA.EasingFunction = easing;
            //把方向动画加入故事版
            sb.Children.Add(xA);
            
            //关联操作的卡片和方向动画
            Storyboard.SetTarget(xA, life);


            //关联具体要执行动画的依赖属性
            Storyboard.SetTargetProperty(xA, new PropertyPath("(Rectangle.Width)"));

            return sb;
        }



        #region 处理对手场和观战

        #region 其他操作

        public static MyStoryboard EffectAim()
        {
            MyStoryboard msb = new MyStoryboard();
            ColorAnimationUsingKeyFrames keyFramesAnimation = new ColorAnimationUsingKeyFrames();
            keyFramesAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(2000));

            LinearColorKeyFrame keyFram = new LinearColorKeyFrame();
            keyFram.Value = Colors.Red;
            keyFram.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500));
            keyFramesAnimation.KeyFrames.Add(keyFram);

            LinearColorKeyFrame keyFram2 = new LinearColorKeyFrame();
            keyFram2.Value = Colors.Red;
            keyFram2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1500));
            keyFramesAnimation.KeyFrames.Add(keyFram2);

            LinearColorKeyFrame keyFram3 = new LinearColorKeyFrame();
            keyFram3.Value = Colors.Transparent;
            keyFram3.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1900));
            keyFramesAnimation.KeyFrames.Add(keyFram3);

            Storyboard.SetTargetProperty(keyFramesAnimation, new PropertyPath("BorderBrush.Color"));

            msb.Children.Add(keyFramesAnimation);

            return msb;
        }



        public static MyStoryboard EffectOrigin()
        {
            MyStoryboard msb = new MyStoryboard();
            ColorAnimationUsingKeyFrames keyFramesAnimation = new ColorAnimationUsingKeyFrames();
            keyFramesAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(230));

            LinearColorKeyFrame keyFram = new LinearColorKeyFrame();
            keyFram.Value = Colors.Blue;
            keyFram.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(50));
            keyFramesAnimation.KeyFrames.Add(keyFram);

            LinearColorKeyFrame keyFram2 = new LinearColorKeyFrame();
            keyFram2.Value = Colors.Blue;
            keyFram2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(80));
            keyFramesAnimation.KeyFrames.Add(keyFram2);

            LinearColorKeyFrame keyFram3 = new LinearColorKeyFrame();
            keyFram3.Value = Colors.LightBlue;
            keyFram3.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(100));
            keyFramesAnimation.KeyFrames.Add(keyFram3);


            System.Windows.Media.Animation.Storyboard.SetTargetProperty(keyFramesAnimation, new PropertyPath("BorderBrush.Color"));
            msb.RepeatBehavior = new RepeatBehavior(2);
            msb.Children.Add(keyFramesAnimation);

            return msb;
        }

        #endregion

        #region 单卡片操作

        /// <summary>
        /// 转移控制权
        /// </summary>
        /// <param name="card"></param>
        /// <param name="cv"></param>
        /// <param name="time"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static MyStoryboard Card_2Opponent(CardUI card, Canvas cv,double time,string field)
        {
            MyStoryboard msb = new MyStoryboard();
            //msb.card = card;

            //RotateTransform rotate = new RotateTransform();
            //ScaleTransform scale = new ScaleTransform();
            //TranslateTransform translate = new TranslateTransform();
            //TransformGroup group = new TransformGroup();

            //Point start = card.TranslatePoint(new Point(), mainwindow.OpBattle);
            //Point end = cv.TranslatePoint(new Point(), mainwindow.OpBattle);

            //end.Y = end.Y - card.ActualHeight - ((cv.ActualHeight - card.ActualHeight) / 2);
            //end.X = end.X - card.ActualWidth -((cv.ActualHeight - card.ActualWidth) / 2);

            //Base.getawayParerent(card);
            //mainwindow.OpBattle.Children.Add(card);

            //double star_angle = 0;
            //double end_angle = 180;
            //if (card.isDef)
            //{
            //    rotate = new RotateTransform(-90);
            //    star_angle = -90;
            //    end_angle = 90;
            //    start.X = start.X + ((cv.ActualHeight - card.ActualWidth) / 2) - ((cv.ActualHeight - card.ActualHeight) / 2);
            //    start.Y = start.Y - card.ActualHeight + ((card.ActualHeight - card.ActualWidth)/2);
            //    //start.X = start.X + ((card.ActualHeight - card.ActualWidth) / 2);
            //    //start.Y = start.Y - card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2);
            //}

            

            //if (field.Equals("1"))
            //{
            //    start = card.TranslatePoint(new Point(), mainwindow.MyBattle);
            //    end = card.TranslatePoint(new Point(), mainwindow.MyBattle);
            //}

            //Canvas.SetTop(card, start.Y);
            //Canvas.SetLeft(card, start.X);

            

            //group.Children.Add(scale);
            //group.Children.Add(rotate);
            //group.Children.Add(translate);
            //card.RenderTransform = group;


            //DoubleAnimation xA = new DoubleAnimation(start.X, end.X, TimeSpan.FromMilliseconds(time));
            //DoubleAnimation yA = new DoubleAnimation(start.Y, end.Y, TimeSpan.FromMilliseconds(time));

            //msb.Children.Add(xA);
            //msb.Children.Add(yA);


            ////关联操作的卡片和方向动画
            //Storyboard.SetTarget(xA, card);
            //Storyboard.SetTarget(yA, card);


            ////关联具体要执行动画的依赖属性
            //Storyboard.SetTargetProperty(xA, new PropertyPath("(Canvas.Left)"));
            //Storyboard.SetTargetProperty(yA, new PropertyPath("(Canvas.Top)"));     


            //DoubleAnimation rA = new DoubleAnimation(star_angle, end_angle, TimeSpan.FromMilliseconds(time));
            //msb.Children.Add(rA);
            //Storyboard.SetTarget(rA, card);
            //Storyboard.SetTargetProperty(rA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)"));


            return msb;
        }

        public static MyStoryboard Card_2OpponentXYZmaterial(CardUI card, Canvas cv, double time, string field)
        {
            MyStoryboard msb = new MyStoryboard();
            //msb.card = card;

            //RotateTransform rotate = new RotateTransform();
            //ScaleTransform scale = new ScaleTransform();
            //TranslateTransform translate = new TranslateTransform();
            //TransformGroup group = new TransformGroup();

            //Point start = card.TranslatePoint(new Point(), mainwindow.OpBattle);
            //Point end = cv.TranslatePoint(new Point(), mainwindow.OpBattle);

            //end.Y = end.Y - card.ActualHeight - ((cv.ActualHeight - card.ActualHeight) / 2);
            //end.X = end.X - card.ActualWidth - ((cv.ActualHeight - card.ActualWidth) / 2);

            //Base.getawayParerent(card);
            //mainwindow.OpBattle.Children.Add(card);

            //double star_angle = 0;
            //double end_angle = 180;
            //if (card.isDef)
            //{
            //    rotate = new RotateTransform(-90);
            //    star_angle = -90;
            //    end_angle = -180;
            //    start.X = start.X + ((cv.ActualHeight - card.ActualWidth) / 2) - ((cv.ActualHeight - card.ActualHeight) / 2);
            //    start.Y = start.Y - card.ActualHeight + ((card.ActualHeight - card.ActualWidth) / 2);
            //    //start.X = start.X + ((card.ActualHeight - card.ActualWidth) / 2);
            //    //start.Y = start.Y - card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2);
            //}

            //if (card.isBack)
            //{
            //    DoubleAnimation sA = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(time));
            //    msb.Children.Add(sA);
            //    Storyboard.SetTarget(sA, card);
            //    Storyboard.SetTargetProperty(sA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            //}

            //if (field.Equals("1"))
            //{
            //    start = card.TranslatePoint(new Point(), mainwindow.MyBattle);
            //    end = card.TranslatePoint(new Point(), mainwindow.MyBattle);
            //}

            //Canvas.SetTop(card, start.Y);
            //Canvas.SetLeft(card, start.X);



            //group.Children.Add(scale);
            //group.Children.Add(rotate);
            //group.Children.Add(translate);
            //card.RenderTransform = group;


            //DoubleAnimation xA = new DoubleAnimation(start.X, end.X, TimeSpan.FromMilliseconds(time));
            //DoubleAnimation yA = new DoubleAnimation(start.Y, end.Y, TimeSpan.FromMilliseconds(time));

            //msb.Children.Add(xA);
            //msb.Children.Add(yA);


            ////关联操作的卡片和方向动画
            //Storyboard.SetTarget(xA, card);
            //Storyboard.SetTarget(yA, card);


            ////关联具体要执行动画的依赖属性
            //Storyboard.SetTargetProperty(xA, new PropertyPath("(Canvas.Left)"));
            //Storyboard.SetTargetProperty(yA, new PropertyPath("(Canvas.Top)"));


            //DoubleAnimation rA = new DoubleAnimation(star_angle, end_angle, TimeSpan.FromMilliseconds(time));
            //msb.Children.Add(rA);
            //Storyboard.SetTarget(rA, card);
            //Storyboard.SetTargetProperty(rA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)"));


            return msb;
        }

        #region 初始化动画对象

        //private static RotateTransform rotate = new RotateTransform(0);              //创建旋转类，初始角度为0
        //private static RotateTransform rotate_def = new RotateTransform(-90);        //创建旋转类，初始角度为-90
        //private static ScaleTransform scale = new ScaleTransform();                  //创建翻转类
        //private static TranslateTransform translate = new TranslateTransform();      //创建位置变换类

        /// <summary>
        /// 设定操作对象的动画属性组
        /// </summary>
        /// <param name="card">设定的卡片</param>
        public static void setTransformGroup(CardUI card)
        {
            TransformGroup group = new TransformGroup();
            if (card.Status == Status.BACK_DEF || card.Status == Status.FRONT_DEF) group.Children.Add(new RotateTransform(-90));
            if (card.Status == Status.BACK_ATK || card.Status == Status.FRONT_ATK) group.Children.Add(new RotateTransform(0));
            group.Children.Add(new ScaleTransform());
            group.Children.Add(new TranslateTransform());
            card.RenderTransform = group;
            
        }

        #endregion

        #region 基本单步动画


        #region 1.旋转动画

        /// <summary>
        /// 旋转动画
        /// </summary>
        /// <param name="startAngle"></param>
        /// <param name="endAngle"></param>
        /// <param name="time">耗时（毫秒）</param>
        /// <returns></returns>
        public static DoubleAnimation Rotate(Double startAngle,Double endAngle,double time)
        {
            DoubleAnimation da = new DoubleAnimation(startAngle, endAngle, new Duration(TimeSpan.FromMilliseconds(time)));
            EasingFunctionBase easing = new CubicEase()
            {
                EasingMode = EasingMode.EaseOut
            };
            da.EasingFunction = easing;
            //Storyboard.SetTargetProperty(da, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(RotateTransform.Angle)"));
            Storyboard.SetTargetProperty(da, new PropertyPath("RenderTransform.Children[1].Angle"));
            da.Completed += (sender, e) =>
            {
                
            };
            return da;
        }

        #endregion

        #region 2.X轴翻转动画

        /// <summary>
        /// X轴翻转动画
        /// </summary>
        /// <param name="startAngle">初始位置（0到1）</param>
        /// <param name="endAngle">结束位置（0到1）</param>
        /// <param name="time">耗时（毫秒）</param>
        /// <returns></returns>
        public static DoubleAnimation scaleX(Double scalestart, Double scaleend, double time)
        {
            DoubleAnimation xA = new DoubleAnimation(scalestart, scaleend, TimeSpan.FromMilliseconds(time));
            EasingFunctionBase easing = new CubicEase()
            {
                EasingMode = EasingMode.EaseOut
            };
            xA.EasingFunction = easing;
            Storyboard.SetTargetProperty(xA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            return xA;
        }

        #endregion

        #region 3.Y轴旋转动画

        /// <summary>
        /// Y轴翻转动画
        /// </summary>
        /// <param name="startAngle">初始位置（0到1）</param>
        /// <param name="endAngle">结束位置（0到1）</param>
        /// <param name="time">耗时（毫秒）</param>
        /// <returns></returns>
        public static DoubleAnimation ScaleY(Double startAngle, Double endAngle, double time)
        {
            DoubleAnimation yA = new DoubleAnimation(startAngle, endAngle, TimeSpan.FromMilliseconds(time));
            EasingFunctionBase easing = new CubicEase()
            {
                EasingMode = EasingMode.EaseOut
            };
            yA.EasingFunction = easing;
            Storyboard.SetTargetProperty(yA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
            return yA;
        }

        #endregion

        #region 4.X轴移动动画

        /// <summary>
        /// X轴移动动画
        /// </summary>
        /// <param name="startX">初始位置</param>
        /// <param name="endX">结束位置</param>
        /// <param name="time">耗时（毫秒）</param>
        /// <returns></returns>
        public static DoubleAnimation CanvasX(double startX,double endX,double time)
        {
            DoubleAnimation xA = new DoubleAnimation(startX, endX, TimeSpan.FromMilliseconds(time));
            EasingFunctionBase easing = new CubicEase()
            {
                EasingMode = EasingMode.EaseOut
            };
            xA.EasingFunction = easing;
            Storyboard.SetTargetProperty(xA, new PropertyPath("(Canvas.Left)"));
            return xA;
        }

        #endregion

        #region 4.X轴移动动画2

        /// <summary>
        /// X轴移动动画
        /// </summary>
        /// <param name="startX">初始位置</param>
        /// <param name="endX">结束位置</param>
        /// <param name="time">耗时（毫秒）</param>
        /// <returns></returns>
        public static DoubleAnimation CanvasX(double endX, double time)
        {
            DoubleAnimation xA = new DoubleAnimation(endX, TimeSpan.FromMilliseconds(time));
            EasingFunctionBase easing = new CubicEase()
            {
                EasingMode = EasingMode.EaseOut
            };
            xA.EasingFunction = easing;
            Storyboard.SetTargetProperty(xA, new PropertyPath("(Canvas.Left)"));
            return xA;
        }
        
        #endregion

        #region 5.Y轴移动动画

        /// <summary>
        /// Y轴移动动画
        /// </summary>
        /// <param name="startY">初始位置</param>
        /// <param name="endY">结束为止</param>
        /// <param name="time">耗时（毫秒）</param>
        /// <returns></returns>
        public static DoubleAnimation CanvasY(double startY, double endY, double time)
        {
            DoubleAnimation yA = new DoubleAnimation(startY, endY, TimeSpan.FromMilliseconds(time));
            EasingFunctionBase easing = new CubicEase()
            {
                EasingMode = EasingMode.EaseOut
            };
            yA.EasingFunction = easing;
            Storyboard.SetTargetProperty(yA, new PropertyPath("(Canvas.Top)"));
            return yA;
        }

        #endregion

        #region 5.Y轴移动动画2

        /// <summary>
        /// Y轴移动动画
        /// </summary>
        /// <param name="startY">初始位置</param>
        /// <param name="endY">结束为止</param>
        /// <param name="time">耗时（毫秒）</param>
        /// <returns></returns>
        public static DoubleAnimation CanvasY(double endY, double time)
        {
            DoubleAnimation yA = new DoubleAnimation(endY, TimeSpan.FromMilliseconds(time));
            EasingFunctionBase easing = new CubicEase()
            {
                EasingMode = EasingMode.EaseOut
            };
            yA.EasingFunction = easing;
            Storyboard.SetTargetProperty(yA, new PropertyPath("(Canvas.Top)"));
            return yA;
        }

        #endregion

        #region 6.淡出元素

        /// <summary>
        /// 变更元素的透明度
        /// </summary>
        /// <param name="to">目标值</param>
        /// <param name="time">耗时（毫秒）</param>
        /// <returns></returns>
        public static DoubleAnimation Opacity2(Double to, double time)
        {
            DoubleAnimation da = new DoubleAnimation(to, new Duration(TimeSpan.FromMilliseconds(time)));
            EasingFunctionBase easing = new CubicEase()
            {
                EasingMode = EasingMode.EaseOut
            };
            da.EasingFunction = easing;
            Storyboard.SetTargetProperty(da, new PropertyPath("Opacity"));

            return da;
        }

        public static DoubleAnimationUsingKeyFrames Opacity(double from, double to, double time)
        {
            DoubleAnimationUsingKeyFrames keyFramesAnimation = new DoubleAnimationUsingKeyFrames();
            keyFramesAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(time));

            LinearDoubleKeyFrame keyFram = new LinearDoubleKeyFrame();
            keyFram.Value = 0;
            keyFram.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0));
            keyFramesAnimation.KeyFrames.Add(keyFram);

            LinearDoubleKeyFrame keyFram2 = new LinearDoubleKeyFrame();
            keyFram2.Value = 1;
            keyFram2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(time*0.23d));
            keyFramesAnimation.KeyFrames.Add(keyFram2);

            LinearDoubleKeyFrame keyFram3 = new LinearDoubleKeyFrame();
            keyFram3.Value = 1;
            keyFram3.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(time*0.77d));
            keyFramesAnimation.KeyFrames.Add(keyFram3);

            LinearDoubleKeyFrame keyFram4 = new LinearDoubleKeyFrame();
            keyFram3.Value = 0;
            keyFram3.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(time));
            keyFramesAnimation.KeyFrames.Add(keyFram4);


            Storyboard.SetTargetProperty(keyFramesAnimation, new PropertyPath("Opacity"));

            return keyFramesAnimation;
        }

        #endregion


        #endregion

        #region 组合动画组

        #region 1.移动动画

        /// <summary>
        /// 移动动画
        /// </summary>
        /// <param name="start">移动起始点</param>
        /// <param name="end">移动结束点</param>
        /// <param name="time">移动耗时（毫秒）</param>
        /// <returns></returns>
        public static MyStoryboard CanvasXY(Point start, Point end, double time)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(CanvasX(start.X, end.X, time));
            msb.Children.Add(CanvasY(start.Y, end.Y, time));
            
            return msb;
        }

        #endregion

        #region 1.移动动画2

        /// <summary>
        /// 移动动画
        /// </summary>
        /// <param name="end">移动结束点</param>
        /// <param name="time">移动耗时（毫秒）</param>
        /// <returns></returns>
        public static MyStoryboard CanvasXY(Point end)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(CanvasX(end.X, 150));
            msb.Children.Add(CanvasY(end.Y, 150));

            return msb;
        }

        public static MyStoryboard CanvasXY_Scale120(Point end) {

            
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(CanvasX(end.X, 200));
            msb.Children.Add(CanvasY(end.Y, 200));
            msb.Children.Add(scaleX(1, 0, 200));
           
            return msb;
        }

        public static MyStoryboard CanvasXY_scale120_rotate9020(Point end)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(CanvasX(end.X, 200));
            msb.Children.Add(CanvasY(end.Y, 200));
            msb.Children.Add(scaleX(1, 0, 200));
            msb.Children.Add(Rotate(-90, 0, 200));

            return msb;
        }

        public static void setStoryboardChainTarget(CardUI card,TransLibrary.StoryboardChain animator)
        {
            foreach (MyStoryboard msb in animator.Animates)
            {
                msb.card = card;
            }
        }


        public static MyStoryboard CanvasXY_Rotate_0290(Point end)
        {
            return CanvasXY_Rotate(end, 200, 0, -90, 200);
        }

        public static MyStoryboard CanvasXY_Rotate_9020(Point end)
        {
            return CanvasXY_Rotate(end, 200, -90, 0, 200);
        }

        /// <summary>
        /// 移动动画
        /// </summary>
        /// <param name="end">移动结束点</param>
        /// <param name="time">移动耗时（毫秒）</param>
        /// <returns></returns>
        public static MyStoryboard CanvasXY_Rotate(Point end, double movetime,double startAngle,double endAngle,double rotatetime)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(CanvasX(end.X, movetime));
            msb.Children.Add(CanvasY(end.Y, movetime));
            msb.Children.Add(Rotate(startAngle, endAngle, rotatetime));
            return msb;
        }

        

        public static MyStoryboard CanvasXY(UIElementCollection uic, Point end, double time)
        {
            MyStoryboard msb = new MyStoryboard();
            foreach (CardUI card in uic)
            {
                DoubleAnimation xa = CanvasX(end.X, time);
                Storyboard.SetTarget(xa,card);
                DoubleAnimation ya = CanvasY(end.Y, time);
                Storyboard.SetTarget(ya,card);
                msb.Children.Add(xa);
                msb.Children.Add(ya);
            }
            return msb;
        }

        #endregion

        #region 2.旋转动画(0到-90度)

        /// <summary>
        /// 旋转动画(0到-90度)
        /// </summary>
        /// <param name="startAngle">初始角度</param>
        /// <param name="endAngle">终止角度</param>
        /// <param name="time">旋转耗时（毫秒）</param>
        /// <returns></returns>
        public static MyStoryboard Rotate_A2D()
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(Rotate(0, -90, 300));
            return msb;
        }

        #endregion

        #region 3.旋转动画(-90度到0)

        /// <summary>
        /// 旋转动画(-90度到0)
        /// </summary>
        /// <param name="startAngle">初始角度</param>
        /// <param name="endAngle">终止角度</param>
        /// <param name="time">旋转耗时（毫秒）</param>
        /// <returns></returns>
        public static MyStoryboard Rotate_D2A()
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(Rotate(-90, 0, 300));
            return msb;
        }

        #endregion

        #region 4.翻转动画（沿X轴从1变换到0（消失））

        /// <summary>
        /// 翻转动画（沿X轴从1变换到0（消失））
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public static MyStoryboard ScaleX_120(double time)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(scaleX(-1, 0, time));
            return msb;
        }

        #endregion

        #region 5.翻转动画（沿X轴从0变换到1（显现））

        /// <summary>
        /// 翻转动画（沿X轴从0变换到1（显现））
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public static MyStoryboard ScaleX_021(double time)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(scaleX(0, 1, time));
            return msb;
        }

        #endregion

        #region 6.翻转动画（沿Y轴从1变换到0（消失））

        /// <summary>
        /// 翻转动画（沿Y轴从1变换到0（消失））
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public static MyStoryboard ScaleY_120(double time)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(ScaleY(1, 0, time));
            return msb;
        }

        #endregion

        #region 7.翻转动画（沿Y轴从0变换到1（显现））

        /// <summary>
        /// 翻转动画（沿Y轴从0变换到1（显现））
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public static MyStoryboard ScaleY_021(double time)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(ScaleY(0, -1, time));
            return msb;
        }

        #endregion

        #region 8.翻转动画（沿X轴从0变换到1（显现））+移动动画

        /// <summary>
        /// 翻转动画（沿X轴从0变换到1（显现））+移动动画
        /// </summary>
        /// <param name="start">移动起始点</param>
        /// <param name="end">移动结束点</param>
        /// <param name="time1">显现耗时（毫秒）</param>
        /// <param name="time2">移动耗时（毫秒）</param>
        /// <returns></returns>
        public static MyStoryboard ScaleX_021_CanvasXY(Point start,Point end,double time1,double time2)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(scaleX(0, 1, time1));
            msb.Children.Add(CanvasX(start.X, end.X, time2));
            msb.Children.Add(CanvasY(start.Y, end.Y, time2));          
            return msb;

        }

        #endregion

        #region 8.翻转动画（沿X轴从0变换到1（显现））+移动动画2

        /// <summary>
        /// 翻转动画（沿X轴从0变换到1（显现））+移动动画
        /// </summary>
        /// <param name="end">移动结束点</param>
        /// <param name="time1">显现耗时（毫秒）</param>
        /// <param name="time2">移动耗时（毫秒）</param>
        /// <returns></returns>
        public static MyStoryboard ScaleX_021_CanvasXY(Point end, double time1, double time2)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(scaleX(0, 1, time1));
            msb.Children.Add(CanvasX(end.X, time2));
            msb.Children.Add(CanvasY(end.Y, time2));
            return msb;

        }

        #endregion

        #region 9.翻转动画（沿X轴从1变换到0（消失））+旋转动画

        /// <summary>
        /// 翻转动画（沿X轴从1变换到0（消失））+旋转动画
        /// </summary>
        /// <param name="startAngle">初始角度</param>
        /// <param name="endAngle">终止角度</param>
        /// <param name="time1">显现耗时（毫秒）</param>
        /// <param name="time2">旋转耗时（毫秒）</param>
        /// <returns></returns>
        public static MyStoryboard ScaleX_120_Rotate(double startAngle, double endAngle, double time1, double time2)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(scaleX(1, 0, time1));
            msb.Children.Add(Rotate(startAngle, endAngle, time2));
            return msb;
        }

        #endregion

        #region 10.翻转动画（沿Y轴从0变换到（消失））+旋转动画

        /// <summary>
        /// 翻转动画（沿Y轴从0变换到（消失））+旋转动画
        /// </summary>
        /// <param name="startAngle">初始角度</param>
        /// <param name="endAngle">终止角度</param>
        /// <param name="time1">显现耗时（毫秒）</param>
        /// <param name="time2">旋转耗时（毫秒）</param>
        /// <returns></returns>
        public static MyStoryboard ScaleY_120_Rotate(double startAngle, double endAngle, double time1, double time2)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(ScaleY(1, 0, time1));
            msb.Children.Add(Rotate(startAngle, endAngle, time2));
            return msb;
        }

        #endregion

        #region 11.翻转动画（沿X轴从0变换到1（显现））+旋转动画

        /// <summary>
        /// 翻转动画（沿X轴从0变换到1（显现））+旋转动画
        /// </summary>
        /// <param name="startAngle">初始角度</param>
        /// <param name="endAngle">终止角度</param>
        /// <param name="time1">显现耗时（毫秒）</param>
        /// <param name="time2">旋转耗时（毫秒）</param>
        /// <returns></returns>
        public static MyStoryboard ScaleX_021_Rotate(double startAngle,double endAngle,double time1,double time2)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(scaleX(0, 1, time1));
            msb.Children.Add(Rotate(startAngle,endAngle,time2));
            return msb;
        }

        #endregion

        #region 12.旋转动画+移动动画

        /// <summary>
        /// 旋转动画+移动动画
        /// </summary>
        /// <param name="startAngle">起始角度</param>
        /// <param name="endAngle">终止角度</param>
        /// <param name="start">起点坐标</param>
        /// <param name="end">终点坐标</param>
        /// <param name="time1">旋转耗时（毫秒）</param>
        /// <param name="time2">移动耗时（毫秒）</param>
        /// <returns></returns>
        public static MyStoryboard Rotate_CanvasXY(double startAngle, double endAngle, Point start, Point end, double time1, double time2)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(Rotate(startAngle, endAngle, time1));
            msb.Children.Add(CanvasXY(start, end, time2));
            //msb.Children.Add(CanvasX(start.X, end.X, time2));
            //msb.Children.Add(CanvasY(start.Y, end.Y, time2));
            return msb;
        }

        #endregion

        #region 12.旋转动画+移动动画2

        /// <summary>
        /// 旋转动画+移动动画
        /// </summary>
        /// <param name="startAngle">起始角度</param>
        /// <param name="endAngle">终止角度</param>
        /// <param name="end">终点坐标</param>
        /// <param name="time1">旋转耗时（毫秒）</param>
        /// <param name="time2">移动耗时（毫秒）</param>
        /// <returns></returns>
        public static MyStoryboard Rotate_CanvasXY(double startAngle, double endAngle,Point end, double time1, double time2)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(Rotate(startAngle, endAngle, time1));
            msb.Children.Add(CanvasX(end.X, time2));
            msb.Children.Add(CanvasY(end.Y, time2));
            return msb;
        }

        #endregion

        #region 13.翻转动画（沿X轴从0变换到1（显现））+旋转动画+移动动画

        /// <summary>
        /// 翻转动画（沿X轴从0变换到1（显现））+旋转动画+移动动画
        /// </summary>
        /// <param name="startAngle">初始角度</param>
        /// <param name="endAngle">终止角度</param>
        /// <param name="start">移动起始点</param>
        /// <param name="end">移动结束点</param>
        /// <param name="time1">显现耗时（毫秒）</param>
        /// <param name="time2">旋转耗时（毫秒）</param>
        /// <param name="time3">移动耗时（毫秒）</param>
        /// <returns></returns>
        public static MyStoryboard ScaleX_021_Rotate_CanvasXY(double startAngle, double endAngle, Point start, Point end, double time1, double time2, double time3)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(scaleX(0, 1, time1));
            msb.Children.Add(Rotate(startAngle, endAngle, time2));
            msb.Children.Add(CanvasX(start.X, end.X, time3));
            msb.Children.Add(CanvasY(start.Y, end.Y, time3));
            return msb;
        }

        #endregion

        #region 13.翻转动画（沿X轴从0变换到1（显现））+旋转动画+移动动画2

        /// <summary>
        /// 翻转动画（沿X轴从0变换到1（显现））+旋转动画+移动动画
        /// </summary>
        /// <param name="startAngle">初始角度</param>
        /// <param name="endAngle">终止角度</param>
        /// <param name="end">移动结束点</param>
        /// <param name="time1">显现耗时（毫秒）</param>
        /// <param name="time2">旋转耗时（毫秒）</param>
        /// <param name="time3">移动耗时（毫秒）</param>
        /// <returns></returns>
        public static MyStoryboard ScaleX_021_Rotate_CanvasXY(double startAngle, double endAngle,Point end, double time1, double time2, double time3)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(scaleX(0, 1, time1));
            msb.Children.Add(Rotate(startAngle, endAngle, time2));
            msb.Children.Add(CanvasX(end.X, time3));
            msb.Children.Add(CanvasY(end.Y, time3));
            return msb;
        }

        #endregion

        #region 14.多重移动动画

        /// <summary>
        /// 多重移动动画(终点相同)
        /// </summary>
        /// <param name="end">移动结束点</param>
        /// <param name="time">移动耗时（毫秒）</param>
        /// <returns></returns>
        public static MyStoryboard CanvasXY(Point end, List<Card> cards,double time)
        {
            MyStoryboard msb = new MyStoryboard();
            foreach (var item in cards)
            {
                msb.Children.Add(CanvasX(end.X, time));
                msb.Children.Add(CanvasY(end.Y, time));
            }
     
            return msb;
        }

        #endregion

        #region 15.淡出

        /// <summary>
        /// 淡出
        /// </summary>
        /// <param name="time">耗时（毫秒）</param>
        /// <returns></returns>
        public static MyStoryboard Opacity20(double time)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(Opacity2(0, time));
            return msb;
        }

        #endregion

        #region 16.淡入

        /// <summary>
        /// 淡入
        /// </summary>
        /// <param name="time">耗时（毫秒）</param>
        /// <returns></returns>
        public static MyStoryboard Opacity21(double time)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(Opacity2(1, time));
            return msb;
        }

        #endregion

        #endregion

        #endregion

        #region 多重卡片操作

        /// <summary>
        /// 多重卡片翻转1
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static MyStoryboard Cards_scalX_120(List<Card> cards,double time)
        {
            MyStoryboard msb = new MyStoryboard();
            //msb.cards = cards;

            //foreach (Card card in cards)
            //{
            //    RotateTransform rotate = new RotateTransform();
            //    ScaleTransform scale = new ScaleTransform();
            //    TranslateTransform translate = new TranslateTransform();
            //    TransformGroup group = new TransformGroup();

            //    if (card.isDef)
            //    {
            //        rotate = new RotateTransform(-90);
            //    }
                

            //    group.Children.Add(scale);
            //    group.Children.Add(rotate);
            //    group.Children.Add(translate);
            //    card.RenderTransform = group;

            //    DoubleAnimation sA = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(time));
            //    msb.Children.Add(sA);
            //    Storyboard.SetTarget(sA, card);           
            //    Storyboard.SetTargetProperty(sA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            //}

            return msb;
        }


        /// <summary>
        /// 多重卡片翻转2，旋转，移动
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="cv"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static MyStoryboard Cards_move(List<Card> cards,Point end, double time,string field)
        {
            MyStoryboard msb = new MyStoryboard();
            //msb.cards = cards;

            //foreach (CardUI card in cards)
            //{
            //    //Point start = card.TranslatePoint(new Point(), mainwindow.MyBattle);
            //    Point start = card.TranslatePoint(new Point(), mainwindow.MyBattle);
            //    if (field.Equals("2"))
            //    {
            //        start = card.TranslatePoint(new Point(), mainwindow.OpBattle);
            //    }

            //    //if (end != mainwindow.card_1_Graveyard.TranslatePoint(new Point(), mainwindow.MyBattle))
            //    //{
            //    //    Canvas cv = card.Parent as Canvas;
            //    //    if (card == cv.Children[cv.Children.Count - 1])
            //    //    {
            //    //        end = mainwindow.card_1_Extra.TranslatePoint(new Point(), mainwindow.MyBattle);
            //    //    }
            //    //}

            //    RotateTransform rotate = new RotateTransform();
            //    ScaleTransform scale = new ScaleTransform();
            //    TranslateTransform translate = new TranslateTransform();
            //    TransformGroup group = new TransformGroup();

            //    if (card.isDef)
            //    {
            //        rotate = new RotateTransform(-90);
            //        start.X = start.X + ((card.ActualHeight - card.ActualWidth) / 2);
            //        start.Y = start.Y - card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2);
            //    }


            //    group.Children.Add(scale);
            //    group.Children.Add(rotate);
            //    group.Children.Add(translate);
            //    card.RenderTransform = group;

            //    Base.getawayParerent(card);
            //    if (field.Equals("2"))
            //    {
            //        mainwindow.OpBattle.Children.Add(card);
            //    }
            //    else if (field.Equals("1"))
            //    {
            //        mainwindow.MyBattle.Children.Add(card);
            //    }
            //    //
                

            //    Canvas.SetTop(card, start.Y);
            //    Canvas.SetLeft(card, start.X);

            //    //设定X和Y坐标的方向动画
            //    DoubleAnimation xA = new DoubleAnimation(start.X, end.X, TimeSpan.FromMilliseconds(time));
            //    DoubleAnimation yA = new DoubleAnimation(start.Y, end.Y, TimeSpan.FromMilliseconds(time));

            //    if (card.isBack)
            //    {
            //        DoubleAnimation sA = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(time + 50.0));
            //        msb.Children.Add(sA);
            //        Storyboard.SetTarget(sA, card);
            //        Storyboard.SetTargetProperty(sA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            //    }

            //    if(card.isDef)
            //    {
            //        DoubleAnimation rA = new DoubleAnimation(-90, 0, TimeSpan.FromMilliseconds(time));
            //        msb.Children.Add(rA);
            //        Storyboard.SetTarget(rA, card);
            //        Storyboard.SetTargetProperty(rA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)"));
            //    }
                

            //    //把方向动画加入故事版
            //    msb.Children.Add(xA);
            //    msb.Children.Add(yA);
               

            //    //关联操作的卡片和方向动画
            //    Storyboard.SetTarget(xA, card);
            //    Storyboard.SetTarget(yA, card);
                

            //    //关联具体要执行动画的依赖属性
            //    Storyboard.SetTargetProperty(xA, new PropertyPath("(Canvas.Left)"));
            //    Storyboard.SetTargetProperty(yA, new PropertyPath("(Canvas.Top)"));               
                
            //}

            return msb;
        
        }

        /// <summary>
        /// 多重卡片翻转2，旋转，移动(主要用于返回额外时)
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="cv"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static MyStoryboard Cards_move2(List<CardUI> cards, Point end, double time, string field)
        {
            MyStoryboard msb = new MyStoryboard();
            //msb.cards = cards;

            //foreach (CardUI card in cards)
            //{
            //    //Point start = card.TranslatePoint(new Point(), mainwindow.MyBattle);
            //    Point start = card.TranslatePoint(new Point(), mainwindow.MyBattle);
            //    if (field.Equals("2"))
            //    {
            //        start = card.TranslatePoint(new Point(), mainwindow.OpBattle);
            //    }
                

            //    //if (end != mainwindow.card_1_Graveyard.TranslatePoint(new Point(), mainwindow.MyBattle))
            //    //{
            //    //    Canvas cv = card.Parent as Canvas;
            //    //    if (card == cv.Children[cv.Children.Count - 1])
            //    //    {
            //    //        end = mainwindow.card_1_Extra.TranslatePoint(new Point(), mainwindow.MyBattle);
            //    //    }
            //    //}

            //    RotateTransform rotate = new RotateTransform();
            //    ScaleTransform scale = new ScaleTransform();
            //    TranslateTransform translate = new TranslateTransform();
            //    TransformGroup group = new TransformGroup();

            //    if (card.isDef)
            //    {
            //        rotate = new RotateTransform(-90);
            //        start.X = start.X + ((card.ActualHeight - card.ActualWidth) / 2);
            //        start.Y = start.Y - card.ActualWidth - ((card.ActualHeight - card.ActualWidth) / 2);
            //    }


            //    group.Children.Add(scale);
            //    group.Children.Add(rotate);
            //    group.Children.Add(translate);
            //    card.RenderTransform = group;

            //    Base.getawayParerent(card);
            //    //mainwindow.MyBattle.Children.Add(card);
            //    if (field.Equals("2"))
            //    {
            //        mainwindow.OpBattle.Children.Add(card);
            //    }
            //    else if (field.Equals("1"))
            //    {
            //        mainwindow.MyBattle.Children.Add(card);
            //    }
                

            //    Canvas.SetTop(card, start.Y);
            //    Canvas.SetLeft(card, start.X);

            //    //设定X和Y坐标的方向动画
            //    DoubleAnimation xA = new DoubleAnimation(start.X, end.X, TimeSpan.FromMilliseconds(time));
            //    DoubleAnimation yA = new DoubleAnimation(start.Y, end.Y, TimeSpan.FromMilliseconds(time));

            //    if (!card.isBack)
            //    {
            //        DoubleAnimation sA = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(time + 50.0));
            //        msb.Children.Add(sA);
            //        Storyboard.SetTarget(sA, card);
            //        Storyboard.SetTargetProperty(sA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            //    }

            //    if (card.isDef)
            //    {
            //        DoubleAnimation rA = new DoubleAnimation(-90, 0, TimeSpan.FromMilliseconds(time));
            //        msb.Children.Add(rA);
            //        Storyboard.SetTarget(rA, card);
            //        Storyboard.SetTargetProperty(rA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)"));
            //    }


            //    //把方向动画加入故事版
            //    msb.Children.Add(xA);
            //    msb.Children.Add(yA);


            //    //关联操作的卡片和方向动画
            //    Storyboard.SetTarget(xA, card);
            //    Storyboard.SetTarget(yA, card);


            //    //关联具体要执行动画的依赖属性
            //    Storyboard.SetTargetProperty(xA, new PropertyPath("(Canvas.Left)"));
            //    Storyboard.SetTargetProperty(yA, new PropertyPath("(Canvas.Top)"));

            //}

            return msb;

        }


        /// <summary>
        /// 多重卡片淡出
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static MyStoryboard Cards_disappear(List<Card> cards, double time)
        {
            MyStoryboard msb = new MyStoryboard();
            //msb.cards = cards;

            //foreach (Card card in cards)
            //{
            //    DoubleAnimationUsingKeyFrames keyFramesAnimation = new DoubleAnimationUsingKeyFrames();
            //    keyFramesAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(time));
            //    Storyboard.SetTarget(keyFramesAnimation, card);
            //    Storyboard.SetTargetProperty(keyFramesAnimation, new PropertyPath("Opacity"));

            //    LinearDoubleKeyFrame keyFram = new LinearDoubleKeyFrame();
            //    keyFram.Value = 0;
            //    keyFram.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(time - 20.0));
            //    keyFramesAnimation.KeyFrames.Add(keyFram);

            //    msb.Children.Add(keyFramesAnimation);
            //}

            return msb;
            
        }

        /// <summary>
        /// 多重卡片淡入
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static MyStoryboard Cards_appear(List<Card> cards, double time)
        {
            MyStoryboard msb = new MyStoryboard();
            //msb.cards = cards;

            //foreach (Card card in cards)
            //{
            //    DoubleAnimationUsingKeyFrames keyFramesAnimation = new DoubleAnimationUsingKeyFrames();
            //    keyFramesAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(time));
            //    Storyboard.SetTarget(keyFramesAnimation, card);
            //    Storyboard.SetTargetProperty(keyFramesAnimation, new PropertyPath("Opacity"));

            //    //0秒位置是0
            //    LinearDoubleKeyFrame keyFram = new LinearDoubleKeyFrame();
            //    keyFram.Value = 0;
            //    keyFram.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0));
            //    keyFramesAnimation.KeyFrames.Add(keyFram);

            //    //1秒位置是1
            //    LinearDoubleKeyFrame keyFram2 = new LinearDoubleKeyFrame();
            //    keyFram2.Value = 1;
            //    keyFram2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(time));
            //    keyFramesAnimation.KeyFrames.Add(keyFram2);

            //    msb.Children.Add(keyFramesAnimation);
            //}

            return msb;
        }

        /// <summary>
        /// 多重卡片翻转2，旋转
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static MyStoryboard Cards_formchange2DefUp(List<Card> cards,double time)
        {
            MyStoryboard msb = new MyStoryboard();
            //msb.cards = cards;

            //foreach (Card card in cards)
            //{

            //    RotateTransform rotate = new RotateTransform();
            //    ScaleTransform scale = new ScaleTransform();
            //    TranslateTransform translate = new TranslateTransform();
            //    TransformGroup group = new TransformGroup();

            //    if (card.isDef)
            //    {
            //        rotate = new RotateTransform(-90);
            //    }

            //    group.Children.Add(scale);
            //    group.Children.Add(rotate);
            //    group.Children.Add(translate);
            //    card.RenderTransform = group;
             
            //    if (card.isBack)
            //    {
            //        DoubleAnimation sA = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(time + 50.0));
            //        msb.Children.Add(sA);
            //        Storyboard.SetTarget(sA, card);
            //        Storyboard.SetTargetProperty(sA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            //    }

            //    if (!card.isDef)
            //    {
            //        DoubleAnimation rA = new DoubleAnimation(0, -90, TimeSpan.FromMilliseconds(time));
            //        msb.Children.Add(rA);
            //        Storyboard.SetTarget(rA, card);
            //        Storyboard.SetTargetProperty(rA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)"));
            //    }

            //}

            return msb;
        }

        /// <summary>
        /// 多重卡片翻转2，旋转
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static MyStoryboard Cards_formchange2DefDown(List<Card> cards, double time)
        {
            MyStoryboard msb = new MyStoryboard();
            //msb.cards = cards;

            //foreach (Card card in cards)
            //{

            //    RotateTransform rotate = new RotateTransform();
            //    ScaleTransform scale = new ScaleTransform();
            //    TranslateTransform translate = new TranslateTransform();
            //    TransformGroup group = new TransformGroup();

            //    if (card.isDef)
            //    {
            //        rotate = new RotateTransform(-90);
            //    }

            //    group.Children.Add(scale);
            //    group.Children.Add(rotate);
            //    group.Children.Add(translate);
            //    card.RenderTransform = group;

            //    if (!card.isBack)
            //    {
            //        DoubleAnimation sA = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(time + 50.0));
            //        msb.Children.Add(sA);
            //        Storyboard.SetTarget(sA, card);
            //        Storyboard.SetTargetProperty(sA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            //    }

            //    if (!card.isDef)
            //    {
            //        DoubleAnimation rA = new DoubleAnimation(0, -90, TimeSpan.FromMilliseconds(time));
            //        msb.Children.Add(rA);
            //        Storyboard.SetTarget(rA, card);
            //        Storyboard.SetTargetProperty(rA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)"));
            //    }

            //}

            return msb;
        }

        /// <summary>
        /// 多重卡片翻转2
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static MyStoryboard Cards_scalX_021(List<Card> cards, double time)
        {
            MyStoryboard msb = new MyStoryboard();
            //msb.cards = cards;

            //foreach (Card card in cards)
            //{
            //    RotateTransform rotate = new RotateTransform();
            //    ScaleTransform scale = new ScaleTransform();
            //    TranslateTransform translate = new TranslateTransform();
            //    TransformGroup group = new TransformGroup();

            //    if (card.isDef)
            //    {
            //        rotate = new RotateTransform(-90);
            //    }


            //    group.Children.Add(scale);
            //    group.Children.Add(rotate);
            //    group.Children.Add(translate);
            //    card.RenderTransform = group;

            //    DoubleAnimation sA = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(time));
            //    msb.Children.Add(sA);
            //    Storyboard.SetTarget(sA, card);
            //    Storyboard.SetTargetProperty(sA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            //}

            return msb;
        }

        /// <summary>
        /// 多重卡片翻转3，旋转
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static MyStoryboard Cards_formchange2AtkUp(List<Card> cards, double time)
        {
            MyStoryboard msb = new MyStoryboard();
            //msb.cards = cards;

            //foreach (Card card in cards)
            //{

            //    RotateTransform rotate = new RotateTransform();
            //    ScaleTransform scale = new ScaleTransform();
            //    TranslateTransform translate = new TranslateTransform();
            //    TransformGroup group = new TransformGroup();

            //    if (card.isDef)
            //    {
            //        rotate = new RotateTransform(-90);
            //    }

            //    group.Children.Add(scale);
            //    group.Children.Add(rotate);
            //    group.Children.Add(translate);
            //    card.RenderTransform = group;

            //    if (card.isBack)
            //    {
            //        DoubleAnimation sA = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(time + 50.0));
            //        msb.Children.Add(sA);
            //        Storyboard.SetTarget(sA, card);
            //        Storyboard.SetTargetProperty(sA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            //    }

            //    if (card.isDef)
            //    {
            //        DoubleAnimation rA = new DoubleAnimation(-90, 0, TimeSpan.FromMilliseconds(time));
            //        msb.Children.Add(rA);
            //        Storyboard.SetTarget(rA, card);
            //        Storyboard.SetTargetProperty(rA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)"));
            //    }

            //}

            return msb;
        }

        public static MyStoryboard Atkline(Point start, Point end, double time)
        {
            MyStoryboard msb = new MyStoryboard();
            

            double p1x = start.X / mainwindow.OpBattle.ActualWidth;
            double p1y = start.Y / mainwindow.OpBattle.ActualHeight;

            double p2x = end.X / mainwindow.OpBattle.ActualWidth;
            double p2y = end.Y / mainwindow.OpBattle.ActualHeight;

            Line line = new Line();
            line.Stroke = new SolidColorBrush();
            line.StrokeThickness = 10; 
            //LinearGradientBrush lgb = new LinearGradientBrush(Colors.Blue, Colors.Transparent, new Point(1, 0.5), new Point(0, 0.5));
            LinearGradientBrush lgb = new LinearGradientBrush(Colors.Red, Colors.Blue, new Point(p1x, p1y), new Point(p2x, p2y));
            line.Stroke = lgb;
            line.StrokeEndLineCap = PenLineCap.Triangle;
            line.X1 = start.X;
            line.Y1 = start.Y;
            line.X2 = end.X;
            line.Y2 = end.Y;
            //添加阴影
            DropShadowEffect dse = new DropShadowEffect();
            dse.BlurRadius = 5;
            dse.Color = Colors.Black;
            dse.Opacity = 0.7;
            dse.RenderingBias = RenderingBias.Performance;
            dse.ShadowDepth = 0.1;
            line.Effect = dse;
            //line.SetValue(Line.OpacityProperty, 0.3);
            line.Opacity = 0.8;

            mainwindow.OpBattle.Children.Add(line);

            DoubleAnimation da = new DoubleAnimation
            {
                From = start.X,
                To = end.X,
                Duration = new Duration(TimeSpan.FromMilliseconds(time - 200))
            };

            DoubleAnimation da2 = new DoubleAnimation
            {
                From = start.Y,
                To = end.Y,
                Duration = new Duration(TimeSpan.FromMilliseconds(time - 200))
            };


            EasingFunctionBase easing = new QuadraticEase()
            {

                EasingMode = EasingMode.EaseOut,       //公式
                //Oscillations =1,                           //滑过动画目标的次数
                //Springiness = 2                             //弹簧刚度
            };


            msb.Children.Add(da);
            msb.Children.Add(da2);

            Storyboard.SetTarget(da, line);
            Storyboard.SetTarget(da2, line);

            Storyboard.SetTargetProperty(da, new PropertyPath("(Line.X2)"));
            Storyboard.SetTargetProperty(da2, new PropertyPath("(Line.Y2)"));

            DoubleAnimationUsingKeyFrames keyFramesAnimation = new DoubleAnimationUsingKeyFrames();
            keyFramesAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(time));
            Storyboard.SetTarget(keyFramesAnimation, line);
            Storyboard.SetTargetProperty(keyFramesAnimation, new PropertyPath("Opacity"));

            LinearDoubleKeyFrame keyFram = new LinearDoubleKeyFrame();
            keyFram.Value = 1;
            keyFram.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(time - 200));

            LinearDoubleKeyFrame keyFram2 = new LinearDoubleKeyFrame();
            keyFram2.Value = 0;
            keyFram2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(time));

            keyFramesAnimation.KeyFrames.Add(keyFram2);

            msb.Children.Add(keyFramesAnimation);

            msb.sword = line;

            //msb.sword = line;
            
            return msb;
        }

        public static MyStoryboard Atk(Point start, Point end, double time)
        {
            double angle = Math.Atan2(end.Y - start.Y, end.X - start.X) * (180 / Math.PI) + 90;

            //double p1x = start.X / mainwindow.OpBattle.ActualWidth;
            //double p1y = start.Y / mainwindow.OpBattle.ActualHeight;

            //double p2x = end.X / mainwindow.OpBattle.ActualWidth;
            //double p2y = end.Y / mainwindow.OpBattle.ActualHeight;


            //Line line = new Line();
            //line.Stroke = new SolidColorBrush();
            //line.StrokeThickness = 10;
            ////LinearGradientBrush lgb = new LinearGradientBrush(Colors.Blue, Colors.Transparent, new Point(1, 0.5), new Point(0, 0.5));
            //LinearGradientBrush lgb = new LinearGradientBrush(Colors.Red, Colors.Blue, new Point(p1x, p1y), new Point(p2x, p2y));
            //line.Stroke = lgb;
            //line.StrokeEndLineCap = PenLineCap.Triangle;
            //line.X1 = start.X;
            //line.Y1 = start.Y;
            //line.X2 = end.X;
            //line.Y2 = end.Y;
            ////添加阴影
            //DropShadowEffect dse = new DropShadowEffect();
            //dse.BlurRadius = 5;
            //dse.Color = Colors.Black;
            //dse.Opacity = 0.7;
            //dse.RenderingBias = RenderingBias.Performance;
            //dse.ShadowDepth = 0.1;
            //line.Effect = dse;
            ////line.SetValue(Line.OpacityProperty, 0.3);
            //line.Opacity = 0.4;
            
            //mainwindow.OpBattle.Children.Add(line);

            MyStoryboard msb = new MyStoryboard();
            Image sword = new Image();
            msb.sword = sword;
            sword.Width = 56;
            sword.Height = 56;
            sword.RenderTransformOrigin = new Point(0.5, 0.5);
            //sword.Source = "/Image/attack.png";
            sword.Source = new BitmapImage(new Uri("/Image/attack.png", UriKind.RelativeOrAbsolute));
            Canvas.SetLeft(sword, start.X - (sword.Width/2));
            Canvas.SetTop(sword, start.Y - (sword.Height/2));
            mainwindow.OpBattle.Children.Add(sword);

            //DoubleAnimationUsingKeyFrames keyFramesAnimation = new DoubleAnimationUsingKeyFrames();
            //keyFramesAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(time));
            //Storyboard.SetTarget(keyFramesAnimation, line);
            //Storyboard.SetTargetProperty(keyFramesAnimation, new PropertyPath("Opacity"));

            //LinearDoubleKeyFrame keyFram = new LinearDoubleKeyFrame();
            //keyFram.Value = 1;
            //keyFram.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(time-100));

            //LinearDoubleKeyFrame keyFram2 = new LinearDoubleKeyFrame();
            //keyFram2.Value = 0;
            //keyFram2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(time));

            //keyFramesAnimation.KeyFrames.Add(keyFram2);

            //msb.Children.Add(keyFramesAnimation);


            
            RotateTransform rotate = new RotateTransform(angle);
            ScaleTransform scale = new ScaleTransform();
            TranslateTransform translate = new TranslateTransform();
            TransformGroup group = new TransformGroup();

            group.Children.Add(scale);
            group.Children.Add(rotate);
            group.Children.Add(translate);
            sword.RenderTransform = group;

            
            //DoubleAnimation rA = new DoubleAnimation(0, angle, TimeSpan.FromMilliseconds(50)); ;
            DoubleAnimation xA = new DoubleAnimation(start.X - (sword.Width / 2), end.X - (sword.Width / 2), TimeSpan.FromMilliseconds(time));
            DoubleAnimation yA = new DoubleAnimation(start.Y - (sword.Height / 2), end.Y - (sword.Height / 2), TimeSpan.FromMilliseconds(time));
            //DoubleAnimation da = new DoubleAnimation
            //{
            //    From = start.X,
            //    To = end.X,
            //    Duration = new Duration(TimeSpan.FromMilliseconds(1000))
            //};

            //DoubleAnimation da2 = new DoubleAnimation
            //{
            //    From = start.Y,
            //    To = end.Y,
            //    Duration = new Duration(TimeSpan.FromMilliseconds(1000))
            //};


            EasingFunctionBase easing = new QuadraticEase()
            {

                EasingMode = EasingMode.EaseOut,       //公式
                //Oscillations =1,                           //滑过动画目标的次数
                //Springiness = 2                             //弹簧刚度
            };
            yA.EasingFunction = easing;
            xA.EasingFunction = easing;
            //da.EasingFunction = easing;
            //da2.EasingFunction = easing;

            //msb.Children.Add(rA);
            msb.Children.Add(xA);
            msb.Children.Add(yA);
            //msb.Children.Add(da);
            //msb.Children.Add(da2);

            //Storyboard.SetTarget(rA, sword);
            Storyboard.SetTarget(xA, sword);
            Storyboard.SetTarget(yA, sword);
            //Storyboard.SetTarget(da, line);
            //Storyboard.SetTarget(da2, line);


            //Storyboard.SetTargetProperty(rA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)"));
            Storyboard.SetTargetProperty(xA, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTargetProperty(yA, new PropertyPath("(Canvas.Top)"));
            //Storyboard.SetTargetProperty(da, new PropertyPath("(Line.X2)"));
            //Storyboard.SetTargetProperty(da2, new PropertyPath("(Line.Y2)"));

            //RepeatBehavior reCount = new RepeatBehavior(3);
            //msb.RepeatBehavior = reCount;
            

            return msb;
        }


        #endregion

        #endregion
    }
}
