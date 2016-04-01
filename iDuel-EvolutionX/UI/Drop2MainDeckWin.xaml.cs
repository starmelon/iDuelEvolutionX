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
    public enum Drop2MainDeckResult
    {
        UP,
        MIDDLE,
        BOTTOM
    }

    public delegate void Drop2MainDeckDelegate(Drop2MainDeckResult location);

    /// <summary>
    /// OverOrInsert.xaml 的交互逻辑
    /// </summary>
    public partial class Drag2MainDeckWin : Window
    {
        

        public event Drop2MainDeckDelegate sendResult;


        public Drag2MainDeckWin()
        {
            InitializeComponent();

            btn_up.Click += new RoutedEventHandler(upClick);
            btn_middle.Click += new RoutedEventHandler(middleClick);
            btn_bottom.Click += new RoutedEventHandler(bottomClick);
        }

        private void upClick(object sender, RoutedEventArgs e)
        {
            sendResult(Drop2MainDeckResult.UP);
            this.Close();
        }

        private void middleClick(object sender, RoutedEventArgs e)
        {
            sendResult(Drop2MainDeckResult.MIDDLE);
            this.Close();
        }

        private void bottomClick(object sender, RoutedEventArgs e)
        {
            sendResult(Drop2MainDeckResult.BOTTOM);
            this.Close();
        }
    }
}
