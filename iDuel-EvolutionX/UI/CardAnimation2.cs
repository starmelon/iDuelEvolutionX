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
    class CardAnimation2
    {
        public static MainWindow mainwindow;
        //public static CardView cardview;


        public static void ControlChange(Card card, Point start, Point end,double start_angle,double end_engle ,int time)
        {
            
        }

        /// <summary>
        /// 翻转动画
        /// </summary>
        /// <param name="card">卡片对象</param>
        /// <param name="angle_star">起始角度</param>
        /// <param name="angle_end">终止角度</param>
        public static void RotateOut(Card card, double angle_star, double angle_end)
        {

            RotateTransform rtf = new RotateTransform();
            card.RenderTransform = rtf;

            //da.From = 0;    //起始值
            //da.To = 1;      //结束值
            //da.Duration = TimeSpan.FromSeconds(3);         //动画持续时间
            //this.textBlock1.BeginAnimation(card.OpacityProperty, da);//开始动画

            Storyboard storyboard = new Storyboard();

            DoubleAnimation rotate = new DoubleAnimation(angle_star, angle_end, new Duration(TimeSpan.FromMilliseconds(300)));
            storyboard.Children.Add(rotate);
            Storyboard.SetTarget(rotate, card);
            Storyboard.SetTargetProperty(rotate, new PropertyPath("RenderTransform.Angle"));


            DoubleAnimation da = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(300)));
            storyboard.Children.Add(da);
            Storyboard.SetTarget(da, card);
            Storyboard.SetTargetProperty(da, new PropertyPath("Opacity"));

            storyboard.Completed += (object c, EventArgs d) =>
            {
                card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
                card.RenderTransform.SetValue(RotateTransform.AngleProperty, angle_end);
                storyboard = null;
            };

            storyboard.Begin();
        }

        /// <summary>
        /// 卡片移动动画
        /// </summary>
        /// <param name="card">卡片实例</param>
        /// <param name="start">起始位置</param>
        /// <param name="end">终止位置</param>
        /// <param name="time">运行时间</param>
        public static void MoveAnimation(CardControl card, Point start, Point end, string to, int time)
        {
            //新建动画故事版
            Storyboard sb = new Storyboard();

            //设定X和Y坐标的方向动画
            DoubleAnimation xA = new DoubleAnimation(start.X, end.X, TimeSpan.FromMilliseconds(time));
            DoubleAnimation yA = new DoubleAnimation(start.Y, end.Y, TimeSpan.FromMilliseconds(time));

            //把方向动画加入故事版
            sb.Children.Add(xA);
            sb.Children.Add(yA);

            //关联操作的卡片和方向动画
            Storyboard.SetTarget(xA, card);
            Storyboard.SetTarget(yA, card);

            //关联具体要执行动画的依赖属性
            Storyboard.SetTargetProperty(xA, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTargetProperty(yA, new PropertyPath("(Canvas.Top)"));

            //该部分为动画执行完成后调用
            //1.从移动面板中取出
            //2.确定目的地
            //3.取消动画对移动对线依赖属性的影响，即置空
            sb.Completed += (object c, EventArgs d) =>
            {
                //从移动面板Canvas分离
                mainwindow.MySpace.Children.Remove(card);

                //清空属性和动画的关联绑定
                card.BeginAnimation(Canvas.LeftProperty, null);
                card.BeginAnimation(Canvas.TopProperty, null);
                //card.Margin = new Thickness(3, 3, 3, 3);

                //对被操作过的属性置0
                if (!to.Equals("手卡") && !to.Equals("对手前场"))
                {
                    Canvas.SetTop(card, 0);
                    Canvas.SetLeft(card, 0);
                }

                switch (to)
                {
                    case "手卡":
                        card.Margin = new Thickness(0);                   
                        mainwindow.card_1_hand.Children.Add(card);
                        CardOperate.sort_HandCard(mainwindow.card_1_hand);
                        break;
                    case "墓地":                            
                        mainwindow.card_1_Graveyard.Children.Add(card);
                        CardOperate.card_FrontAtk(card);
                        CardOperate.sort_SingleCard(card);
                        
                        break;
                    case "除外":
                        mainwindow.card_1_Outside.Children.Add(card);
                        break;
                    case "额外":
                        //CardOperate.card_ExtraSort(card, mainwindow.card_1_Extra);
                        CardOperate.card_BackAtk(card);
                        //CardOperate.CardSortsingle(mainwindow.card_1_Extra, card, 56, 81);
                        
                        break;
                    case "怪物":

                        break;
                    case "魔陷": break;
                    case "场地":
                        //CardOperate.get_Firstcard2Battle(6);
                        mainwindow.card_1_Area.Children.Add(card);
                        break;
                    case "卡组":
                        
                        mainwindow.card_1_Deck.Children.Add(card);
                        CardOperate.card_BackAtk(card);
                        CardOperate.sort_SingleCard(card);
                        break;
                    case "对手墓地":
                        //mainwindow.card_1_Deck.Children.Insert(0, card);
                        mainwindow.card_2_Graveyard.Children.Add(card);
                        CardOperate.card_FrontAtk(card);
                        //RotateTransform rtf = new RotateTransform(180);
                        CardOperate.sort_SingleCard(card);
                        // CardOperate.HandCardSort();
                        break;
                    case "对手前场":
                        //mainwindow.card_1_Deck.Children.Insert(0, card);
                        //mainwindow.card_2_Graveyard.Children.Add(card);
                        if (mainwindow.card_2_6.Children.Count == 0)
                        {
                            mainwindow.card_2_6.Children.Add(card);
                        }
                        else if (mainwindow.card_2_7.Children.Count == 0)
                        {
                            mainwindow.card_2_7.Children.Add(card);
                        }
                        else if (mainwindow.card_2_8.Children.Count == 0)
                        {
                            mainwindow.card_2_8.Children.Add(card);
                        }
                        else if (mainwindow.card_2_9.Children.Count == 0)
                        {
                            mainwindow.card_2_9.Children.Add(card);
                        }
                        else if (mainwindow.card_2_10.Children.Count == 0)
                        {
                            mainwindow.card_2_10.Children.Add(card);
                        }
                        Canvas cv = card.Parent as Canvas;
                        CardOperate.sort_Canvas(cv);

                        //Canvas.SetLeft(card, 12.5);
                        //Canvas.SetTop(card, 0);
                        //Canvas.SetBottom(card, 3);


                        card.ContextMenu = null;//取消菜单命令
                        //card.PreviewMouseMove -= CardEvent.CardDragStart;
                        //card.MouseDown -= CardEvent.ClikDouble;
                        
                        //card.Margin = new Thickness(0);
                        break;

                       
                }

                


                //清理动画
                sb.Remove(card);
                sb = null;
                GC.Collect();
                Console.WriteLine("清理内存");
            };

            //动画开始
            sb.Begin();
        }

        public static MyStoryboard FadeOut(Card card,double time)
        {
            MyStoryboard msb = new MyStoryboard();
            //msb.card = card;
            //DoubleAnimationUsingKeyFrames keyFramesAnimation = new DoubleAnimationUsingKeyFrames();
            //keyFramesAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(time+50));
            //Storyboard.SetTarget(keyFramesAnimation, card);
            //Storyboard.SetTargetProperty(keyFramesAnimation, new PropertyPath("Opacity"));

            //LinearDoubleKeyFrame keyFram = new LinearDoubleKeyFrame();
            //keyFram.Value = 0;
            //keyFram.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(time));
            //keyFramesAnimation.KeyFrames.Add(keyFram);

            //msb.Children.Add(keyFramesAnimation);

            return msb;
        }

        public static MyStoryboard FadeIn(Card card, double time)
        {
            MyStoryboard msb = new MyStoryboard();
            //msb.card = card;
            //DoubleAnimationUsingKeyFrames keyFramesAnimation = new DoubleAnimationUsingKeyFrames();
            //keyFramesAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(time));
            //Storyboard.SetTarget(keyFramesAnimation, card);
            //Storyboard.SetTargetProperty(keyFramesAnimation, new PropertyPath("Opacity"));

            ////0秒位置是0
            //LinearDoubleKeyFrame keyFram = new LinearDoubleKeyFrame();
            //keyFram.Value = 0;
            //keyFram.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0));
            //keyFramesAnimation.KeyFrames.Add(keyFram);

            ////1秒位置是1
            //LinearDoubleKeyFrame keyFram2 = new LinearDoubleKeyFrame();
            //keyFram2.Value = 1;
            //keyFram2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(time));
            //keyFramesAnimation.KeyFrames.Add(keyFram2);

            //msb.Children.Add(keyFramesAnimation);

            return msb;
        }

        /// <summary>
        /// 卡片淡出淡入
        /// </summary>
        /// <param name="card">卡片对象</param>
        /// <param name="aim_controls">目标位置控件</param>
        public static void FadeOut(CardControl card,object aim_controls,bool isback) 
        {
            Storyboard sb = new Storyboard();
            DoubleAnimationUsingKeyFrames keyFramesAnimation = new DoubleAnimationUsingKeyFrames();
            keyFramesAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(300));
            Storyboard.SetTarget(keyFramesAnimation, card);
            Storyboard.SetTargetProperty(keyFramesAnimation, new PropertyPath("Opacity"));

            LinearDoubleKeyFrame keyFram = new LinearDoubleKeyFrame();
            keyFram.Value = 0;
            keyFram.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(250));
            keyFramesAnimation.KeyFrames.Add(keyFram);

            sb.Children.Add(keyFramesAnimation);

            sb.Completed += (object c, EventArgs d) =>
            {
                card.BeginAnimation(Card.OpacityProperty, null);
                
                //card.Opacity = 0;
                //card.SetValue(Card.OpacityProperty, 1.0);
                Canvas cv_par = card.Parent as Canvas;
                Base.getawayParerent(card);
                Canvas cv = aim_controls as Canvas;

                CardOperate.sort_Canvas(cv_par);
                
                CardOperate.sort_HandCard(mainwindow.card_1_hand);
                CardOperate.sort_HandCard(mainwindow.card_2_hand);
                cv.Children.Add(card);
                CardOperate.sort_SingleCard(card);
                if (!isback) CardOperate.card_FrontAtk(card);
                //Console.WriteLine(cv.Name);
                ////card.Margin = new Thickness(3,3,3,3);
                //RotateTransform rotateTransform = new RotateTransform(0);
                //card.RenderTransform = rotateTransform;
                //CardOperate.HandCardSort();
                //CardOperate.card_lay(cv_par, cv, card, cardview,isback);
                //CardOperate.get_Firstcard2Battle(6);
                //if(cv.Name.Equals("card_1_Extra")) CardAnimation.RotateOut(card,)
                //cv.Children.Add(card);
                FadeIn(card,aim_controls);
                
                sb = null;
            };

            sb.Begin();
        }

        public static void FadeIn(CardControl card,object aim_controls) 
        {
            Storyboard sb = new Storyboard();
            DoubleAnimationUsingKeyFrames keyFramesAnimation = new DoubleAnimationUsingKeyFrames();
            keyFramesAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
            Storyboard.SetTarget(keyFramesAnimation, card);
            Storyboard.SetTargetProperty(keyFramesAnimation, new PropertyPath("Opacity"));

            //0秒位置是0
            LinearDoubleKeyFrame keyFram = new LinearDoubleKeyFrame();
            keyFram.Value = 0;
            keyFram.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0));
            keyFramesAnimation.KeyFrames.Add(keyFram);

            //1秒位置是1
            LinearDoubleKeyFrame keyFram2 = new LinearDoubleKeyFrame();
            keyFram2.Value = 1;
            keyFram2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1));
            keyFramesAnimation.KeyFrames.Add(keyFram2);

            sb.Children.Add(keyFramesAnimation);

            sb.Completed += (object c, EventArgs d) =>
            {
                card.BeginAnimation(Card.OpacityProperty, null);
                
                //card.Opacity = 1;
                
                sb = null;
            };

            sb.Begin();
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
        /// 普通召唤
        /// </summary>
        /// <param name="card"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static MyStoryboard MoveAnimation2Summon(Card card, Point start, Point end, int time)
        {
            MyStoryboard sb = new MyStoryboard();

            ////设定X和Y坐标的方向动画
            //DoubleAnimation xA = new DoubleAnimation(start.X, end.X, TimeSpan.FromMilliseconds(time));
            //DoubleAnimation yA = new DoubleAnimation(start.Y, end.Y, TimeSpan.FromMilliseconds(time));
            //DoubleAnimation dA = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(300));

            ////关联操作的卡片和方向动画
            //Storyboard.SetTarget(xA, card);
            //Storyboard.SetTarget(yA, card);
            //Storyboard.SetTarget(dA, card);

            //Storyboard.SetTargetProperty(xA, new PropertyPath("(Canvas.Left)"));
            //Storyboard.SetTargetProperty(yA, new PropertyPath("(Canvas.Top)"));
            //Storyboard.SetTargetProperty(dA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));

            ////把方向动画加入故事版
            //sb.Children.Add(xA);
            //sb.Children.Add(yA);
            //sb.Children.Add(dA);

            //sb.card = card;

            return sb;
 
        }
        
        /// <summary>
        /// 防守盖放
        /// </summary>
        /// <param name="card"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static MyStoryboard MoveAnimation2Def(Card card, Point start, Point end, int time)
        {

            MyStoryboard sb = new MyStoryboard();

           // //设定X和Y坐标的方向动画
           // DoubleAnimation xA = new DoubleAnimation(start.X, end.X, TimeSpan.FromMilliseconds(time));
           // DoubleAnimation yA = new DoubleAnimation(start.Y, end.Y, TimeSpan.FromMilliseconds(time));
           // //DoubleAnimation dA = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(300));
           // DoubleAnimation rA = new DoubleAnimation(0, -90, new Duration(TimeSpan.FromMilliseconds(200)));
            
            

           // //关联操作的卡片和方向动画
           // Storyboard.SetTarget(xA, card);
           // Storyboard.SetTarget(yA, card);
           // //Storyboard.SetTarget(dA, card);
           // Storyboard.SetTarget(rA, card);


           // //关联具体要执行动画的依赖属性
           // Storyboard.SetTargetProperty(xA, new PropertyPath("(Canvas.Left)"));
           // Storyboard.SetTargetProperty(yA, new PropertyPath("(Canvas.Top)"));
           //// Storyboard.SetTargetProperty(xA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(Canvas.Left)"));
           // //Storyboard.SetTargetProperty(yA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(Canvas.Top)"));
           // //Storyboard.SetTargetProperty(dA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
           // Storyboard.SetTargetProperty(rA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(RotateTransform.Angle)"));


            

           // //把方向动画加入故事版
           // sb.Children.Add(xA);
           // sb.Children.Add(yA);
           // //sb.Children.Add(dA);
           // sb.Children.Add(rA);


           // sb.card = card;

            return sb;

        }

        public static MyStoryboard MoveAnimation2Atk(Card card, Point start, Point end, int time)
        {

            MyStoryboard sb = new MyStoryboard();

            ////设定X和Y坐标的方向动画
            //DoubleAnimation xA = new DoubleAnimation(start.X, end.X, TimeSpan.FromMilliseconds(time));
            //DoubleAnimation yA = new DoubleAnimation(start.Y, end.Y, TimeSpan.FromMilliseconds(time));
            //DoubleAnimation rA = new DoubleAnimation(-90,0, new Duration(TimeSpan.FromMilliseconds(300)));

            ////关联操作的卡片和方向动画
            //Storyboard.SetTarget(xA, card);
            //Storyboard.SetTarget(yA, card);
            ////Storyboard.SetTarget(dA, card);
            //Storyboard.SetTarget(rA, card);


            ////关联具体要执行动画的依赖属性
            //Storyboard.SetTargetProperty(xA, new PropertyPath("(Canvas.Left)"));
            //Storyboard.SetTargetProperty(yA, new PropertyPath("(Canvas.Top)"));
            //Storyboard.SetTargetProperty(rA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)"));

            ////把方向动画加入故事版
            //sb.Children.Add(xA);
            //sb.Children.Add(yA);
            //sb.Children.Add(rA);


            //sb.card = card;

            return sb;

        }

        /// <summary>
        /// 攻←→守
        /// </summary>
        /// <param name="card"></param>
        public static void Def_or_Atk(Card card)
        {
            double start = (double)card.RenderTransform.GetValue(RotateTransform.AngleProperty);
            double end = start == 0 ? -90 : 0;

            RotateTransform rotate = new RotateTransform();
            ScaleTransform scale = new ScaleTransform();
            TransformGroup group = new TransformGroup();
            group.Children.Add(scale);
            group.Children.Add(rotate);
            card.RenderTransform = group;

            TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();
            

            MyStoryboard msb = Rotate(card, start,end);
            //msb.Completed += (object sender, EventArgs e) =>
            //{
            //    msb.card.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            //    msb.card.RenderTransform.SetValue(RotateTransform.AngleProperty, 0.0);
            //    //storyboard = null;
            //};

            animator.Animates.Add(msb);
            animator.Begin();

        }


        /// <summary>
        /// 里守→表攻
        /// </summary>
        /// <param name="card"></param>
        public static void Rotate2Atk(Card card)
        {
            //double angle = (double)card.RenderTransform.GetValue(RotateTransform.AngleProperty);
            //Console.WriteLine(angle);
            //ScaleTransform scaleTransform = new ScaleTransform();
            
            //card.RenderTransform.SetValue(RotateTransform.AngleProperty, angle);

            RotateTransform rotate = new RotateTransform();
            ScaleTransform scale = new ScaleTransform();
            TransformGroup group = new TransformGroup();
            group.Children.Add(scale);
            group.Children.Add(rotate);      
            card.RenderTransform = group;

            TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();

            //MyStoryboard msb1 = Rotate(card, 90.0, 0.0);
            //animator.Animates.Add(msb1);

            MyStoryboard msb2 = scalX_120_rotate_9020(card);
            msb2.Completed += (object c, EventArgs d) =>
            {
                //msb2.card.SetPic();
            };
            animator.Animates.Add(msb2);
            

    
            MyStoryboard msb3 = scalX_021(card);

            animator.Animates.Add(msb3);

            animator.Begin();

        }

        /// <summary>
        /// 表攻→里守
        /// </summary>
        /// <param name="card"></param>
        public static void Rotate2Def(Card card)
        {
            RotateTransform rotate = new RotateTransform();
            ScaleTransform scale = new ScaleTransform();
            TransformGroup group = new TransformGroup();
            group.Children.Add(scale);
            group.Children.Add(rotate);
            card.RenderTransform = group;

            TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();

            //MyStoryboard msb1 = Rotate(card, 90.0, 0.0);
            //animator.Animates.Add(msb1);

            MyStoryboard msb2 = scalX_120_rotate_0290(card);
            msb2.Completed += (object c, EventArgs d) =>
            {
                //msb2.card.SetPic();
            };
            animator.Animates.Add(msb2);



            MyStoryboard msb3 = scalX_021(card);

            animator.Animates.Add(msb3);

            animator.Begin();

        }

        /// <summary>
        /// 里侧←→表侧
        /// </summary>
        /// <param name="card"></param>
        public static void Rotate_card(Card card)
        {
            //double angle = (double) card.RenderTransform.GetValue(RotateTransform.AngleProperty);

            RotateTransform rotate = new RotateTransform();
            if (card.isDef)
            {
                 rotate = new RotateTransform(-90);
            }
            ScaleTransform scale = new ScaleTransform();
            TransformGroup group = new TransformGroup();
            group.Children.Add(scale);
            group.Children.Add(rotate);
            card.RenderTransform = group;

            TransLibrary.StoryboardChain animator = new TransLibrary.StoryboardChain();
            MyStoryboard msb1 = scalX_120(card,300);
            msb1.Completed += (object c, EventArgs d) =>
            {
                //msb1.card.RenderTransform.SetValue(RotateTransform.AngleProperty, angle);
                //msb1.card.SetPic();
                //CardOperate.card_FrontAtk(msb1.card);
            };
            animator.Animates.Add(msb1);
            
            MyStoryboard msb2 = scalX_021(card);
            msb2.Completed += (object c, EventArgs d) =>
            {
                msb2.card.BeginAnimation(ScaleTransform.ScaleXProperty, null);
                msb2.card.RenderTransform.SetValue(ScaleTransform.ScaleXProperty, (double)1);
                //msb2.
                //card.RenderTransform = null;
            };

            animator.Animates.Add(msb2);

            animator.Begin();
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
        public static MyStoryboard scalX_120_rotate_9020(Card card)
        {
            //ScaleTransform scaleTransform = new ScaleTransform();
            //scaleTransform.ScaleX = 1;
            //card.RenderTransform = scaleTransform;

            //新建动画故事版
            MyStoryboard sb = new MyStoryboard();

            ////设定X和Y坐标的方向动画
            //DoubleAnimation xA = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(150));
            //DoubleAnimation yA = new DoubleAnimation(-90, 0, TimeSpan.FromMilliseconds(150));

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
        /// 卡片逆时针翻旋-P1
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static MyStoryboard scalX_120_rotate_0290(Card card)
        {
            //ScaleTransform scaleTransform = new ScaleTransform();
            //scaleTransform.ScaleX = 1;
            //card.RenderTransform = scaleTransform;

            //新建动画故事版
            MyStoryboard sb = new MyStoryboard();

            ////设定X和Y坐标的方向动画
            //DoubleAnimation xA = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(150));
            //DoubleAnimation yA = new DoubleAnimation(0, -90, TimeSpan.FromMilliseconds(150));

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
        /// 卡片翻转-P1
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static MyStoryboard scalX_120(Card card,double time)
        {
            //ScaleTransform scaleTransform = new ScaleTransform();
            //scaleTransform.ScaleX = 1;
            //card.RenderTransform = scaleTransform;

            //新建动画故事版
            MyStoryboard sb = new MyStoryboard();

            ////设定X和Y坐标的方向动画
            //DoubleAnimation xA = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(time));

            //sb.Children.Add(xA);
            ////sb.Children.Add(yA);
            ////sb.GetCurrentTime();

            ////关联操作的卡片和方向动画
            //Storyboard.SetTarget(xA, card);
            ////Storyboard.SetTarget(yA, btn);

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
            ////Storyboard.SetTargetProperty(xA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(RotateTransform.Angle)"));

            //sb.card = card;

            return sb;
        }

        /// <summary>
        /// 卡片翻转-P2
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static MyStoryboard scalX_021(Card card)
        {
            //ScaleTransform scaleTransform = new ScaleTransform();
            //scaleTransform.ScaleX = 1;
            //card.RenderTransform = scaleTransform;

            //新建动画故事版
            MyStoryboard sb = new MyStoryboard();

            ////设定X和Y坐标的方向动画
            //DoubleAnimation xA = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(300));

            //sb.Children.Add(xA);
            ////sb.Children.Add(yA);
            ////sb.GetCurrentTime();

            ////关联操作的卡片和方向动画
            //Storyboard.SetTarget(xA, card);
            ////Storyboard.SetTarget(yA, btn);

            //DependencyProperty[] propertyChain = new DependencyProperty[]
            //{
            //    Card.RenderTransformProperty,
            //    //TransformGroup.ChildrenProperty,
            //    ScaleTransform.ScaleXProperty
            //};

            ////关联具体要执行动画的依赖属性
            ////Storyboard.SetTargetProperty(xA, new PropertyPath("(0).(1)", propertyChain));
            //Storyboard.SetTargetProperty(xA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            ////Storyboard.SetTargetProperty(xA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.X)"));

            //sb.card = card;

            return sb;
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

        


        #region 处理对手场和观战

        

        #region 单卡片操作

        

      

        #region 初始化动画对象

        //private static RotateTransform rotate = new RotateTransform(0);              //创建旋转类，初始角度为0
        //private static RotateTransform rotate_def = new RotateTransform(-90);        //创建旋转类，初始角度为-90
        //private static ScaleTransform scale = new ScaleTransform();                  //创建翻转类
        //private static TranslateTransform translate = new TranslateTransform();      //创建位置变换类

        /// <summary>
        /// 设定操作对象的动画属性组
        /// </summary>
        /// <param name="card">设定的卡片</param>
        public static void setTransformGroup(Card card)
        {
            TransformGroup group = new TransformGroup();
            if (card.isDef) group.Children.Add(new RotateTransform(-90));
            if (!card.isDef) group.Children.Add(new RotateTransform(0));
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
            Storyboard.SetTargetProperty(da, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(RotateTransform.Angle)"));

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
        public static DoubleAnimation ScaleX(Double startAngle, Double endAngle, double time)
        {
            DoubleAnimation xA = new DoubleAnimation(startAngle, endAngle, TimeSpan.FromMilliseconds(time));
            EasingFunctionBase easing = new CubicEase()
            {
                EasingMode = EasingMode.EaseOut
            };
            xA.EasingFunction = easing;
            Storyboard.SetTargetProperty(xA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(ScaleTransform.ScaleX)"));
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
            Storyboard.SetTargetProperty(yA, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(ScaleTransform.ScaleY)"));
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
        public static MyStoryboard CanvasXY(Point end, double time)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(CanvasX(end.X, time));
            msb.Children.Add(CanvasY(end.Y, time));

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
        public static MyStoryboard Rotate_A2D(double time)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(Rotate(0, -90, time));
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
        public static MyStoryboard Rotate_D2A(double time)
        {
            MyStoryboard msb = new MyStoryboard();
            msb.Children.Add(Rotate(-90, 0, time));
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
            msb.Children.Add(ScaleX(-1, 0, time));
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
            msb.Children.Add(ScaleX(0, 1, time));
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
            msb.Children.Add(ScaleX(0, 1, time1));
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
            msb.Children.Add(ScaleX(0, 1, time1));
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
            msb.Children.Add(ScaleX(1, 0, time1));
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
            msb.Children.Add(ScaleX(0, 1, time1));
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
            msb.Children.Add(CanvasX(start.X, end.X, time2));
            msb.Children.Add(CanvasY(start.Y, end.Y, time2));
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
            msb.Children.Add(ScaleX(0, 1, time1));
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
            msb.Children.Add(ScaleX(0, 1, time1));
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
        public static MyStoryboard Cards_move(List<CardControl> cards,Point end, double time,string field)
        {
            MyStoryboard msb = new MyStoryboard();
            //msb.cards = cards;

            //foreach (CardControl card in cards)
            //{
            //    //Point start = card.TranslatePoint(new Point(), mainwindow.MySpace);
            //    Point start = card.TranslatePoint(new Point(), mainwindow.MySpace);
            //    if (field.Equals("2"))
            //    {
            //        start = card.TranslatePoint(new Point(), mainwindow.OpSpace);
            //    }

            //    //if (end != mainwindow.card_1_Graveyard.TranslatePoint(new Point(), mainwindow.MySpace))
            //    //{
            //    //    Canvas cv = card.Parent as Canvas;
            //    //    if (card == cv.Children[cv.Children.Count - 1])
            //    //    {
            //    //        end = mainwindow.card_1_Extra.TranslatePoint(new Point(), mainwindow.MySpace);
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
            //        mainwindow.OpSpace.Children.Add(card);
            //    }
            //    else if (field.Equals("1"))
            //    {
            //        mainwindow.MySpace.Children.Add(card);
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
        public static MyStoryboard Cards_move2(List<CardControl> cards, Point end, double time, string field)
        {
            MyStoryboard msb = new MyStoryboard();
            //msb.cards = cards;

            //foreach (CardControl card in cards)
            //{
            //    //Point start = card.TranslatePoint(new Point(), mainwindow.MySpace);
            //    Point start = card.TranslatePoint(new Point(), mainwindow.MySpace);
            //    if (field.Equals("2"))
            //    {
            //        start = card.TranslatePoint(new Point(), mainwindow.OpSpace);
            //    }
                

            //    //if (end != mainwindow.card_1_Graveyard.TranslatePoint(new Point(), mainwindow.MySpace))
            //    //{
            //    //    Canvas cv = card.Parent as Canvas;
            //    //    if (card == cv.Children[cv.Children.Count - 1])
            //    //    {
            //    //        end = mainwindow.card_1_Extra.TranslatePoint(new Point(), mainwindow.MySpace);
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
            //    //mainwindow.MySpace.Children.Add(card);
            //    if (field.Equals("2"))
            //    {
            //        mainwindow.OpSpace.Children.Add(card);
            //    }
            //    else if (field.Equals("1"))
            //    {
            //        mainwindow.MySpace.Children.Add(card);
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
            

            double p1x = start.X / mainwindow.OpSpace.ActualWidth;
            double p1y = start.Y / mainwindow.OpSpace.ActualHeight;

            double p2x = end.X / mainwindow.OpSpace.ActualWidth;
            double p2y = end.Y / mainwindow.OpSpace.ActualHeight;

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

            mainwindow.OpSpace.Children.Add(line);

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

            //double p1x = start.X / mainwindow.OpSpace.ActualWidth;
            //double p1y = start.Y / mainwindow.OpSpace.ActualHeight;

            //double p2x = end.X / mainwindow.OpSpace.ActualWidth;
            //double p2y = end.Y / mainwindow.OpSpace.ActualHeight;


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
            
            //mainwindow.OpSpace.Children.Add(line);

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
            mainwindow.OpSpace.Children.Add(sword);

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
