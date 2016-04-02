using iDuel_EvolutionX.Model;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;


namespace iDuel_EvolutionX.UI
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class SignTextBlock : UserControl,IDisposable
    {

        public SignTextBlock()
        {
            
            InitializeComponent();

            ContextMenu cm = new ContextMenu();
            MenuItem remark = new MenuItem { Header = "编辑备注" };
            remark.Click += Remark_Click;
            cm.Items.Add(remark);
            this.ContextMenu = cm;
        }

        private void Remark_Click(object sender, RoutedEventArgs e)
        {
            EditRemark er = new EditRemark();
            er.sendResult += (result) => {
                this.ToolTip = result;
            };
            er.Owner = Application.Current.MainWindow;
            Point p = this.PointToScreen(new Point(0, 0));
            er.Top = p.Y - er.Height;
            er.Left = p.X - ((er.Width - this.ActualWidth) / 2);
            er.ShowDialog();


        }

        
        /// <summary>
        /// 自适应文字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ellipse_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double size = GetFontSize(this.Content.ToString(), new Size(this.ActualWidth, this.ActualHeight), new Typeface(this.FontFamily, this.FontStyle, this.FontWeight, this.FontStretch));
            this.FontSize = size <= 0 ? 1 : size;
            double num = this.ActualWidth > this.ActualHeight ? this.ActualHeight : this.ActualWidth;
            //this.Width = num;
            //this.Height = num;
            
        }

        private double GetFontSize(string text, Size availableSize, Typeface typeFace)
        {
            FormattedText formtxt = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeFace, 10, Brushes.Black);

            double ratio = Math.Min(availableSize.Width / formtxt.Width, availableSize.Height / formtxt.Height);

            return 8 * ratio;
        }

        /// <summary>
        /// 中键滚动操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void content_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Console.WriteLine(e.Delta);
            if (e.Delta>0)
            {
                this.Content = (Convert.ToInt32(this.Content) + 3).ToString();
            }
            else
            {
                int temp = Convert.ToInt32(this.Content) - 1;
                if (temp < 1)
                {
                    clearSelf();
                }
                else
                {
                    this.Content = temp.ToString(); ;
                }
                //this.Content = (temp < 0 ? 0 : temp ).ToString();
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void content_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Content = (Convert.ToInt32(this.Content) + 1).ToString();
            }

            
            
        }

        public void clearSelf()
        {
            CardControl card = this.Tag as CardControl;
            if (card == null)
            {
                return;
            }
            card.signs.Remove(this);
            Tag = null;
            (this.Parent as StackPanel).Children.Remove(this);
            Dispose();
        }

        public void Dispose()
        {
            BindingOperations.ClearBinding(this, SignTextBlock.ContentProperty);
        }


    }
}
