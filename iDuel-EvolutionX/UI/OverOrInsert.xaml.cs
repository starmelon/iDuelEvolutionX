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
    public enum CardAction
    {
        INSERT,
        OVERLAY

    }

    public delegate void MyDelegate(CardAction action);

    /// <summary>
    /// OverOrInsert.xaml 的交互逻辑
    /// </summary>
    public partial class OverOrInsert : Window
    {
        

        public event MyDelegate sendResult;


        public OverOrInsert()
        {
            InitializeComponent();

            btn_insert.Click += new RoutedEventHandler(insertClick);
            btn_add.Click += new RoutedEventHandler(addClick);
        }

        private void insertClick(object sender, RoutedEventArgs e)
        {
            sendResult(CardAction.INSERT);
            this.Close();
        }

        private void addClick(object sender, RoutedEventArgs e)
        {
            sendResult(CardAction.OVERLAY);
            this.Close();
        }
    }
}
