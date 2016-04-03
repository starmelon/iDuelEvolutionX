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
    /// CardView.xaml 的交互逻辑
    /// </summary>
    public partial class CardView : Window ,IDisposable
    {
        private static MainWindow mainwindow;
        private static CardView cv;

        private CardView()
        {
            InitializeComponent();
            //card_View.SelectedIndex = 1;
            //判断行数，确定
            //5*9+56*8=496
            //5*10+56*9=554
            //5*9+81*5
            this.card_1_Deck.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            this.card_1_Deck.Drop += new DragEventHandler(DuelEvent.card_Drop);
            this.card_1_Extra.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            this.card_1_Extra.Drop += new DragEventHandler(DuelEvent.card_Drop);
            this.card_1_Graveyard.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            this.card_1_Graveyard.Drop += new DragEventHandler(DuelEvent.card_Drop);
            this.card_1_Outside.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            this.card_1_Outside.Drop += new DragEventHandler(DuelEvent.card_Drop);
            this.tb_View.SelectionChanged += new SelectionChangedEventHandler(DuelEvent.card_View_SelectionChanged);
            this.card_1_Deckviewx.Click += new RoutedEventHandler(DuelEvent.btn_card_1_Deckviewx);

            this.btn_cardviewClose_Deck_1.Click += new RoutedEventHandler(DuelEvent.btn_cardview_close);
            this.btn_cardviewClose_Extra_1.Click += new RoutedEventHandler(DuelEvent.btn_cardview_close);
            this.btn_cardviewClose_Graveyard_1.Click += new RoutedEventHandler(DuelEvent.btn_cardview_close);
            this.btn_cardviewClose_Outside_1.Click += new RoutedEventHandler(DuelEvent.btn_cardview_close);
            this.btn_cardviewClose_Extra_2.Click += new RoutedEventHandler(DuelEvent.btn_cardview_close);
            this.btn_cardviewClose_Graveyard_2.Click += new RoutedEventHandler(DuelEvent.btn_cardview_close);
            this.btn_cardviewClose_Outside_2.Click += new RoutedEventHandler(DuelEvent.btn_cardview_close);

            //this.btn_cardviewClose.Click += new RoutedEventHandler(CardEvent.btn_cardview_close);
            //this.deck_Draw.Click += new RoutedEventHandler(CardEvent.btn_deck_Draw);
            //this.card_2_Graveyard.DragOver += new DragEventHandler(CardEvent.card_DragOver);
            //this.card_2_Graveyard.Drop += new DragEventHandler(CardEvent.card_Drop);
            //this.card_2_Outside.DragOver += new DragEventHandler(CardEvent.card_DragOver);
            //this.card_2_Outside.Drop += new DragEventHandler(CardEvent.card_Drop);

            
            //for (int i = 0; i < 15/8; i++)
            //{
            //    for (int j = 0; j < 8; j++)
            //    {
            //        Canvas.SetLeft(this.card_view, 5 * (j + 1) + 56 * j);
            //        Canvas.SetTop(this.card_view, 5 * (i + 1) + 81 * i);
            //    }
            //}
        }

        public static CardView getInstance(MainWindow mw)
        {
            if (cv == null)
            {
                cv = new CardView();
                mainwindow = mw;
            }
            return cv;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !e.Source.GetType().Name.Equals("Card"))
            {
                this.DragMove();
            }

        }

        private void TabItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("你选中了" + sender.ToString());
        }

        private void card_View_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("你选中了" + sender.ToString());
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            if (mainwindow.card_1_Deck.Children.Count > 0 && card_1_Deck.Children.Count > 0)
            {
                CardUI card;
                int x = card_1_Deck.Children.Count;
                for (int i = 0; i < x; i++)
                {
                    card = card_1_Deck.Children[x - i - 1] as CardUI;
                    card_1_Deck.Children.Remove(card);
                    mainwindow.card_1_Deck.Children.Add(card);
                    CardOperate.sort_SingleCard(card);
                    CardOperate.card_BackAtk(card);
                }
            }
            //foreach (Card card in card_1_Deck.Children)
            //{
            //    card.Visibility = System.Windows.Visibility.Hidden;
            //}
            mainwindow.view_Deck.IsEnabled = true;
            this.tb_View.SelectedIndex = -1;
            mainwindow.report.Text += ("查看结束" + Environment.NewLine);
            //Console.WriteLine(this.tb_View.SelectedIndex);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //e.Cancel = true;
            ////this.InvalidateArrange();
            
            
            this.tb_View.SelectedIndex = -1;
            this.Dispose();
            //this.tb_View.UpdateLayout();
            //this.tb_View.Visibility = System.Windows.Visibility.Collapsed;
            //this.Visibility = System.Windows.Visibility.Collapsed;
            //this.Hide();
        }



        public void Dispose()
        {
            cv = null;
            //throw new NotImplementedException();
        }
    }
}
