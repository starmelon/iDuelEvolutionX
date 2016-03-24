using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iDuel_EvolutionX.UI
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class SignTextBlock : UserControl
    {

        public SignTextBlock()
        {
            InitializeComponent();
        }

        private double GetFontSize(string text, Size availableSize, Typeface typeFace)
        {
            FormattedText formtxt = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeFace, 10, Brushes.Black);

            double ratio = Math.Min(availableSize.Width / formtxt.Width, availableSize.Height / formtxt.Height);

            return 8 * ratio;
        }

        private void ellipse_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double size = GetFontSize(this.Content.ToString(), new Size(this.ActualWidth, this.ActualHeight), new Typeface(this.FontFamily, this.FontStyle, this.FontWeight, this.FontStretch));
            this.FontSize = size <= 0 ? 1 : size;
            double num = this.ActualWidth > this.ActualHeight ? this.ActualHeight : this.ActualWidth;
            //this.Width = num;
            //this.Height = num;
            
        }

        private void content_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta>0)
            {
                this.Content = (Convert.ToInt32(this.Content) + 5).ToString();
            }
            else
            {
                int temp = Convert.ToInt32(this.Content) - 5;
                this.Content = (temp < 0 ? 0 : temp ).ToString();
            }
            
        }

        private void content_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Content = (Convert.ToInt32(this.Content) + 1).ToString();
            }
            if (e.RightButton == MouseButtonState.Pressed)
            {
                int temp = Convert.ToInt32(this.Content) - 1;
                this.Content = (temp < 0 ? 0 : temp).ToString();
            }
        }
    }
}
