using iDuel_EvolutionX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace iDuel_EvolutionX.UI
{
    class UIAnimation
    {
        private static UIAnimation uiAnimation;
        public static MainWindow mainwindow;
        public Storyboard opacity21;
        public Storyboard stateOpertion;
        public Storyboard opactiy20;
        public Storyboard rotateAnimation;
        public Storyboard show_serchP2;
        public Storyboard hide_serchP2;
        
        //public static MyStoryboard 

        private UIAnimation()
        {
            opacity21 = opacityChange(1);
            stateOpertion = colorChange();
            opactiy20 = opacityChange(0);
            rotateAnimation = angleChange();
        }

        public static UIAnimation getInstance()
        {
            if (uiAnimation == null)
            {
                uiAnimation = new UIAnimation();

            }
            return uiAnimation;
        }

        /// <summary>
        /// 改变阶段宣言按钮的边框亮度
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public static MyStoryboard ChangePhase(int to)
        {
            MyStoryboard msb = new MyStoryboard();
            DoubleAnimation da = new DoubleAnimation(to, TimeSpan.FromMilliseconds(300));

            msb.Children.Add(da);

            DependencyProperty[] propertyChainX = new DependencyProperty[]
            {
                Rectangle.EffectProperty,
                System.Windows.Media.Effects.DropShadowEffect.OpacityProperty,
            };
            Storyboard.SetTargetProperty(da, new PropertyPath("Opacity"));

            return msb;
        }


        #region <-- 状态显示 -->

        private static Storyboard colorChange()
        {
            Storyboard sb = new Storyboard();
            ColorAnimation ca = new ColorAnimation()
            {
                To = Colors.Blue,
                Duration = TimeSpan.FromMilliseconds(800)
            };
            DependencyProperty[] propertyChain2color = new DependencyProperty[]
            {
                TextBlock.EffectProperty,
                System.Windows.Media.Effects.DropShadowEffect.ColorProperty
            };
            Storyboard.SetTargetProperty(ca, new PropertyPath("(0).(1)",propertyChain2color));
            Storyboard.SetTarget(ca, mainwindow.tb_opOperation);

            sb.AutoReverse = true;
            sb.RepeatBehavior = RepeatBehavior.Forever;
            sb.Children.Add(ca);
            
            return sb;
        }

        #endregion




        #region
        private static Storyboard opacityChange(double end)
        {
            Storyboard sb = new Storyboard();
            DoubleAnimation da = new DoubleAnimation()
            {
                To = end,
                Duration = TimeSpan.FromMilliseconds(800)
            };
            Storyboard.SetTargetProperty(da, new PropertyPath("Opacity"));
            //Storyboard.SetTarget(da, mainwindow.tb_opOperation);
            sb.Children.Add(da);
            return sb;
        }

        private static Storyboard angleChange()
        {
            Storyboard sb = new Storyboard();
            DoubleAnimation da = new DoubleAnimation()
            {
                To = 360,
                Duration = TimeSpan.FromMilliseconds(1500)
            };
            Storyboard.SetTargetProperty(da, new PropertyPath("RenderTransform.Angle"));
            //mainwindow.img_scan_op.RenderTransform = new RotateTransform(0);
            //Storyboard.SetTarget(da, mainwindow.img_scan_op);
            sb.RepeatBehavior = RepeatBehavior.Forever;
            sb.Children.Add(da);
            return sb;
        }

        #endregion


        /// <summary>
        /// main,extra,side的移动
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="star"></param>
        /// <param name="end"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static MyStoryboard SPmove(StackPanel sp,Thickness star,Thickness end,double time)
        {
            MyStoryboard msb = new MyStoryboard();

            ThicknessAnimation ta = new ThicknessAnimation(star, end, TimeSpan.FromMilliseconds(time));
            EasingFunctionBase easing = new QuadraticEase()
            {

                EasingMode = EasingMode.EaseOut,       //公式
                //Oscillations =1,                           //滑过动画目标的次数
                //Springiness = 2                             //弹簧刚度
            };
            ta.EasingFunction = easing;


            msb.Children.Add(ta);
            Storyboard.SetTarget(ta, sp);
            //StackPanel.MarginProperty
            Storyboard.SetTargetProperty(ta, new PropertyPath("Margin"));
         
            return msb;
        }

        /// <summary>
        /// decksDocument的移动
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="star"></param>
        /// <param name="end"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static MyStoryboard SPmove(TabControl sp, Thickness start, Thickness end, double time)
        {
            MyStoryboard msb = new MyStoryboard();

            ThicknessAnimation ta = new ThicknessAnimation(start,end, TimeSpan.FromMilliseconds(time));
            EasingFunctionBase easing = new QuadraticEase()
            {

                EasingMode = EasingMode.EaseOut,       //公式
                //Oscillations =1,                           //滑过动画目标的次数
                //Springiness = 2                             //弹簧刚度
            };
            ta.EasingFunction = easing;


            msb.Children.Add(ta);
            Storyboard.SetTarget(ta, sp);
            //StackPanel.MarginProperty
            Storyboard.SetTargetProperty(ta, new PropertyPath("Margin"));

            return msb;
        }

        public static MyStoryboard showDecksManerger(Grid gd,double star,double end,double time)
        {
            MyStoryboard msb = new MyStoryboard();
            DoubleAnimation da = new DoubleAnimation(star, end, TimeSpan.FromMilliseconds(time));
            EasingFunctionBase easing = new QuadraticEase()
            {

                EasingMode = EasingMode.EaseOut,       //公式
                //Oscillations =1,                           //滑过动画目标的次数
                //Springiness = 2                             //弹簧刚度
            };
            da.EasingFunction = easing;
            msb.Children.Add(da);
            Storyboard.SetTarget(da, gd);
            Storyboard.SetTargetProperty(da, new PropertyPath("Opacity"));

            return msb;
        }
    }
}
