using iDuel_EvolutionX.Model;
using iDuel_EvolutionX.Service;
using iDuel_EvolutionX.UI;
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

namespace iDuel_EvolutionX
{
    /// <summary>
    /// XYZmaterialView.xaml 的交互逻辑
    /// </summary>
    public partial class XYZmaterialView : Window,IDisposable
    {
        private static MainWindow mainwindow;
        private static XYZmaterialView xyz_material;
        public static Canvas xyz_cv;

        public XYZmaterialView()
        {
            InitializeComponent();
        }

        public static XYZmaterialView getInstance()
        {
            if (xyz_material == null)
            {
                xyz_material = new XYZmaterialView();
            }
            return xyz_material;
        }

        public static XYZmaterialView getInstance(MainWindow mw,Canvas cv)
        {
            if (xyz_material == null)
            {
                xyz_material = new XYZmaterialView();
                mainwindow = mw;
                xyz_cv = cv;
            }
            return xyz_material;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !e.Source.GetType().Name.Equals("Card"))
            {

                this.DragMove();
                
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int n = materials.Children.Count;

            for (int i = 0; i < n; i++)
            {
                Card temp = materials.Children[0] as Card;
                materials.Children.RemoveAt(0);
                Canvas.SetTop(temp, (xyz_cv.ActualHeight - temp.ActualHeight) / 2.0);
                Canvas.SetLeft(temp, (xyz_cv.ActualWidth - temp.ActualWidth) / 2.0);
                xyz_cv.Children.Insert(0, temp);
            }


            Card top = xyz_cv.Children[xyz_cv.Children.Count - 1] as Card;
            if (top.sCardType.Equals("XYZ怪兽") && top.isDef)
            {
                CardOperate.sort_Canvas(xyz_cv);
            }
            else
            {
                CardOperate.sort_Canvas(xyz_cv);
            }

            this.Close();
            this.Dispose();
            
        }

        public void Dispose()
        {
            xyz_cv = null;
            xyz_material = null;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // CardEvent.doubleclick = false;
        }
    }
}
