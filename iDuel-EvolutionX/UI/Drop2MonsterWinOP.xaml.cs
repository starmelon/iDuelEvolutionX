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
    public enum Drop2MonsterWinOPResult
    {
        INSERT,
        ATK

    }

    public delegate void Drop2MonsterOPDelegate(Drop2MonsterWinOPResult result);

    /// <summary>
    /// OverOrInsert.xaml 的交互逻辑
    /// </summary>
    public partial class Drop2MonsterWinOP : Window
    {
        

        public event Drop2MonsterOPDelegate sendResult;


        public Drop2MonsterWinOP()
        {
            InitializeComponent();

            btn_insert.Click += new RoutedEventHandler(insertClick);
            btn_atk.Click += new RoutedEventHandler(atkClick);
        }

        private void insertClick(object sender, RoutedEventArgs e)
        {
            sendResult(Drop2MonsterWinOPResult.INSERT);
            this.Close();
        }

        private void atkClick(object sender, RoutedEventArgs e)
        {
            sendResult(Drop2MonsterWinOPResult.ATK);
            this.Close();
        }
    }
}
