using iDuel_EvolutionX.Model;
using iDuel_EvolutionX.Service;
using NBX3.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace iDuel_EvolutionX.UI
{
    /// <summary>
    /// 事件响应的指向
    /// </summary>
    class DuelEvent
    {
        //public static bool doubleclick = false;

        /// <summary>
        /// 阶段宣言按钮的单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {  
            Rectangle rta = sender as Rectangle;
            if (DuelOperate.isExist())
            {
                DuelOperate.getInstance().ChangePhase(rta);
            }
            else
            {
                MessageBox.Show("决斗还未开始！"); 
            }        
            
            
            
            
        }

        /// <summary>
        /// 聊天框的回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void tb_chatsend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                DuelOperate.getInstance().chatOrLife();
                
            }
            
        }


        /// <summary>
        /// 菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void MenuItem_Handle(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            MenuItemOperate.Command_judge(sender, mi.Header.ToString());

        }

        /// <summary>
        /// 卡片双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ClikDouble(object sender, MouseButtonEventArgs e)
        {
            //Console.WriteLine(e.ToString());
            //Console.WriteLine(e.ChangedButton);
            if (e.ClickCount==2 )
            {

                CardControl card = sender as CardControl;
                CardOperate.Card_DoubleClick(card,e);
                
            }
            
            //CardEvent.doubleclick = false;
            //doubleclick = false;
        }

        /// <summary>
        /// 指示物双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ClikDouble2(object sender, MouseButtonEventArgs e)
        {
            //Console.WriteLine(e.ToString());
            //Console.WriteLine(e.ChangedButton);
            if (e.ClickCount == 2)
            {
                
                TextBlock tb = sender as TextBlock;
                CardOperate.Sign_DoubleClick(tb, e.ChangedButton.ToString());

            }

            //CardEvent.doubleclick = false;
            //doubleclick = false;
        }

        public static void card_picture_MouseEnter(object sender, MouseEventArgs e)
        {
            CardOperate.Card_showpic(sender, e);
        }

        #region 拖放操作

        /// <summary>
        /// 拖拽开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void CardDragStart(object sender, MouseEventArgs e)
        {
            Console.WriteLine("拖拽："+e.Source.GetType().Name);
            if (e.LeftButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
            {
                CardOperate.CardDragStart(sender, e);

            }
            
            
        }

        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop(object sender, DragEventArgs e)
        {
            CardOperate.card_Drop(sender, e);
        }

        /// <summary>
        /// 魔陷卡区的释放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop_Magic(object sender, DragEventArgs e)
        {
            CardOperate.card_Drop_Magic(sender, e);
        }

        /// <summary>
        /// 怪物卡区的释放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop_Monster(object sender, DragEventArgs e)
        {
            CardOperate.card_Drop_Monster(sender, e);
        }

        /// <summary>
        /// 主卡组区的释放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop_Main(object sender, DragEventArgs e)
        {
            CardOperate.card_Drop_Main(sender, e);
        }

        /// <summary>
        /// 额外区的释放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop_Extra(object sender, DragEventArgs e)
        {
            CardOperate.card_Drop_Extra(sender, e);
        }

        /// <summary>
        /// 手卡区的释放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop_Hand(object sender, DragEventArgs e)
        {
            CardOperate.card_Drop_Hand(sender, e);
        }

        /// <summary>
        /// 墓地区的释放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop_Graveyard(object sender, DragEventArgs e)
        {
            CardOperate.card_Drop_Graveyard(sender, e);
        }

        /// <summary>
        /// 除外区的释放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop_Outside(object sender, DragEventArgs e)
        {
            CardOperate.card_Drop_Outside(sender, e);
        }

        /// <summary>
        /// 灵摆区的释放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop_Pendulum(object sender, DragEventArgs e)
        {
            //CardOperate.card_Drop_Pendulum(sender, e);
            CardOperate.card_Drop_Pendulum(sender, e);
        }

        /// <summary>
        /// 对手怪物区的释放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop_OpMonster(object sender, DragEventArgs e)
        {
            CardOperate.card_Drop_OpMonster(sender, e);
        }

        /// <summary>
        /// 对手魔陷区的释放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_Drop_OpMagic(object sender, DragEventArgs e)
        {
            CardOperate.card_Drop_OpMagic(sender, e);
        }

        /// <summary>
        /// 拖拽完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_DragOver(object sender, DragEventArgs e)
        {
            CardOperate.card_DragOver(sender, e);
        }

        public static void card_DragContinue(object sender, QueryContinueDragEventArgs e)
        {
            if (e.KeyStates == DragDropKeyStates.ControlKey)
            {
                e.Action = DragAction.Continue;
            }
            
        }


        #endregion

        /// <summary>
        /// 选项卡变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void card_View_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CardOperate.card_View_SelectionChanged(sender, e);
        }

        public static void view_Extra2_Click(object sender, RoutedEventArgs e)
        {
            CardOperate.view_Extra2();
        }

        /// <summary>
        /// 查看对方墓地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void view_Graveyard2_Click(object sender, RoutedEventArgs e)
        {
            CardOperate.view_Graveyard2();
        }

        /// <summary>
        /// 查看对方除外
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void view_Outside2_Click(object sender, RoutedEventArgs e)
        {
            CardOperate.view_Outside2();
        }


        /// <summary>
        /// 查看除外
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void view_Outside_Click(object sender, RoutedEventArgs e)
        {
            CardOperate.view_Outside();
        }

        /// <summary>
        /// 查看墓地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void view_Graveyard_Click(object sender, RoutedEventArgs e)
        {
            CardOperate.view_Graveyard();
        }

        /// <summary>
        /// 查看卡组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void view_Deck_Click(object sender, RoutedEventArgs e)
        {

            CardOperate.view_Main();
        }

        /// <summary>
        /// 查看额外
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void view_Extra_Click(object sender, RoutedEventArgs e)
        {
            CardOperate.view_Extra_Click();
        }

        /// <summary>
        /// 查看卡组上方X张
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void btn_card_1_Deckviewx(object sender, RoutedEventArgs e)
        {
            CardOperate.card_1_Deckviewx_Click(sender,e);
        }


        /// <summary>
        /// 卡牌预览的关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void btn_cardview_close(object sender, RoutedEventArgs e)
        {
            CardOperate.cardview_close();
        }

        #region <-- 卡组管理按钮事件 -->

        /// <summary>
        /// 卡组管理按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void btn_deck(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            if (btn.Content.Equals("卡组管理"))
            {
                DuelOperate.getInstance().ShowDeck();
            }
            else if (btn.Content.Equals("确认选择"))
            {
                DuelOperate.getInstance().SetDeck(btn);
            }
        }

        #endregion

        public static void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DuelOperate.getInstance().ChooseDeck(e.AddedItems[0]);
        }

        /// <summary>
        /// 开局按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void btn_start(object sender, RoutedEventArgs e)
        {
            
            DuelOperate duel = DuelOperate.getInstance();
            duel.Dispose();
            //duel = DuelOperate.getInstance();
            duel.DuelStart();
        }

        /// <summary>
        /// 先攻按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void btn_firstAtk(object sender, RoutedEventArgs e)
        {      
            DuelOperate.getInstance().FirstAtk();
        }

        /// <summary>
        /// 先攻按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void btn_secondAtk(object sender, RoutedEventArgs e)
        {
            DuelOperate.getInstance().SecondAtk();
        }

        #region <-- 显示区域选择控件的按钮事件 -->

        /// <summary>
        /// 显示区域选择控件的按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void btn_choosezone(object sender, RoutedEventArgs e)
        {
            DuelOperate.getInstance().ShowZone_cb();
        }

        #endregion

        #region  <-- 选择区域的事件 -->

        /// <summary>
        /// 选择区域的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal static void cb_Checked(object sender, RoutedEventArgs e)
        {
            DuelOperate.getInstance().ChooseZone(sender);
        }

        #endregion

        #region <-- 查看战报 -->

        internal static void btn_viewreport(object sender, RoutedEventArgs e)
        {
            DuelOperate.getInstance().ViewReport();
        }

        #endregion

        #region <-- 切换side按钮的事件 -->

        internal static void btn_sideMode(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            if (btn.Content.Equals("更换SIDE"))
            {
                DuelOperate.getInstance().sideMode();
            }
            else if (btn.Content.Equals("确认更换"))
            {
                DuelOperate.getInstance().sideModeEnd();
                //DuelOperate.getInstance().sideModeSure();
            }
            
        }

        #endregion

        internal static void card_Drop_Hand2(object sender, DragEventArgs e)
        {
            CardOperate.card_Drop_Hand2(sender, e);
        }

        #region 卡组管理预览窗口的放置事件

        internal static void sideMode_Drop(object sender, DragEventArgs e)
        {
            CardOperate.sideMode(sender, e);
        }

        #endregion

        #region <-- 卡组选择取消按钮的事件 -->

        internal static void btn_choosedeckCancel(object sender, RoutedEventArgs e)
        {
            DuelOperate.getInstance().DecksetCancel();
        }

        #endregion

        internal static void btn_sideModeCancel(object sender, RoutedEventArgs e)
        {
            DuelOperate.getInstance().sideModeCancel();
        }

        #region <-- 掷骰子 -->

        internal static void btn_roll(object sender, RoutedEventArgs e)
        {
            DuelOperate.getInstance().roll();
        }

        #endregion


        #region <-- -->

        internal static void btn_coin(object sender, RoutedEventArgs e)
        {
            DuelOperate.getInstance().coin();
        }

        #endregion
    }
}
