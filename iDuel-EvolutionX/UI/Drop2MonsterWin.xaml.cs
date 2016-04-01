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
    public enum Drop2MonsterWinResult
    {
        INSERT,
        OVERLAY

    }

    public delegate void Drop2MonsterDelegate(Drop2MonsterWinResult action);

    /// <summary>
    /// OverOrInsert.xaml 的交互逻辑
    /// </summary>
    public partial class Drop2MonsterWin : Window
    {
        

        public event Drop2MonsterDelegate sendResult;


        public Drop2MonsterWin()
        {
            InitializeComponent();

            btn_insert.Click += new RoutedEventHandler(insertClick);
            btn_add.Click += new RoutedEventHandler(addClick);
        }

        private void insertClick(object sender, RoutedEventArgs e)
        {
            sendResult(Drop2MonsterWinResult.INSERT);
            this.Close();
        }

        private void addClick(object sender, RoutedEventArgs e)
        {
            sendResult(Drop2MonsterWinResult.OVERLAY);
            this.Close();
        }
    }
}
