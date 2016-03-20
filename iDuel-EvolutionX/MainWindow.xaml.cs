using iDuel_EvolutionX.ADO;
using iDuel_EvolutionX.Model;
using iDuel_EvolutionX.Net;
using iDuel_EvolutionX.Service;
using iDuel_EvolutionX.UI;
using NBX3.Service;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace iDuel_EvolutionX
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        

        List<Card> card_deck = new List<Card>();  //己方主卡组
        List<Card> card_extra = new List<Card>(); //己方额外卡组
        List<Card> card_side = new List<Card>();

        List<Card> card_deck2 = new List<Card>();  //对方主卡组
        List<Card> card_extra2 = new List<Card>(); //对方额外卡组
        List<Card> card_side2 = new List<Card>();

        Card card_temp = new Card();

        public static double card_width = 56;           //卡片宽度
        public static double card_height = 81;          //卡片高度
        public static double card_hand_range = 4;       //手卡间距
        public static double card_hand_width = 435;     //手卡区宽度
        public static double card_hand_height = 100;    //手卡区高度
        CardView cardview;
        //CardView cardview2 = new CardView();
             
        public MainWindow()
        {
            InitializeComponent();

            #region 设置攻守显示的数据绑定

            /*
             * 1.因为在Xaml定义中，无法对转换器参数传送对象，顾以代码形式定义
             * 2.且如果Xaml定义中在转换器中使用MainWindow对象时，比如通过App获得运行窗体的对象引用，
             * 会导致Xaml页面报空指针错误导致页面无法编辑，尽管不影响代码
             */
            for (int i = 1; i < 3; i++)
            {
                for (int j = 6; j < 11; j++)
                {
                    //定义并初始化一个绑定
                    Binding textBinding = new Binding();
                    //设置要绑定源控件
                    textBinding.ElementName = "card_" + i + "_" + j;
                    //设置要绑定属性
                    textBinding.Path = new PropertyPath("Children.Count");
                    //设置转换器
                    if (i==1)
                    {
                        textBinding.Converter = this.TryFindResource("CountToDimensionConverter2") as IValueConverter;
                    }
                    else if (i == 2)
                    {
                        textBinding.Converter = this.TryFindResource("CountToDimensionConverter3") as IValueConverter;
                    }
                    
                    //给转换器传送参数
                    textBinding.ConverterParameter = this.FindName("card_"+ i +"_" + j);
                    //设置绑定到要绑定的控件
                    TextBlock tb = this.FindName("atk_" + i + "_" + j) as TextBlock;
                    tb.SetBinding(TextBlock.TextProperty, textBinding);
                }
            }
            

            

            #endregion

            #region <-- 初始化卡片预览框 -->
            cardview = CardView.getInstance(this);
            #endregion

            #region <-- 传递主窗口引用 -->
            CardAnimation.mainwindow = this;
            MenuItemOperate.mainwindow = this;
            CardOperate.mainwindow = this;
            OpponentOperate.mainwindow = this;
            Base.mainwindow = this;
            UIAnimation.mainwindow = this;

            #endregion

            #region <-- 传递控件组引用 -->

            for (int i = 1; i < 3; i++)
            {
                for (int j = 1; j < 6; j++)
                {
                    if (i == 1 ) CardOperate.cv_magictraps_1.Add(this.battle_zone_middle.FindName("card_" + i + "_" + j) as FrameworkElement);
                    else if (i == 2) CardOperate.cv_magictraps_2.Add(this.battle_zone_middle.FindName("card_" + i + "_" + j) as FrameworkElement);
                }
                for (int k = 6; k < 11; k++)
                {
                    if (i == 1) CardOperate.cv_monsters_1.Add(this.battle_zone_middle.FindName("card_" + i + "_" + k) as FrameworkElement);
                    else if (i == 2) CardOperate.cv_monsters_2.Add(this.battle_zone_middle.FindName("card_" + i + "_" + k) as FrameworkElement);
                }
            }
            CardOperate.cv_others_1.Add(this.card_1_Deck);
            CardOperate.cv_others_1.Add(this.card_1_Extra);
            CardOperate.cv_others_1.Add(this.card_1_Outside);
            CardOperate.cv_others_1.Add(this.card_1_Graveyard);
            CardOperate.cv_others_1.Add(this.card_1_Left);
            CardOperate.cv_others_1.Add(this.card_1_Right);
            CardOperate.cv_others_1.Add(this.card_1_Area);

            CardOperate.cv_others_2.Add(this.card_2_Deck);
            CardOperate.cv_others_2.Add(this.card_2_Extra);
            CardOperate.cv_others_2.Add(this.card_2_Outside);
            CardOperate.cv_others_2.Add(this.card_2_Graveyard);
            CardOperate.cv_others_2.Add(this.card_2_Left);
            CardOperate.cv_others_2.Add(this.card_2_Right);
            CardOperate.cv_others_2.Add(this.card_2_Area);

            #endregion

            #region <-- 注册场地控件的拖放事件 -->

            card_1_1.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_1_2.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_1_3.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_1_4.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_1_5.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_1_1.Drop += new DragEventHandler(DuelEvent.card_Drop_Magic);
            card_1_2.Drop += new DragEventHandler(DuelEvent.card_Drop_Magic);
            card_1_3.Drop += new DragEventHandler(DuelEvent.card_Drop_Magic);
            card_1_4.Drop += new DragEventHandler(DuelEvent.card_Drop_Magic);
            card_1_5.Drop += new DragEventHandler(DuelEvent.card_Drop_Magic);

            card_1_6.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_1_7.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_1_8.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_1_9.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_1_10.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_1_6.Drop += new DragEventHandler(DuelEvent.card_Drop_Monster);
            card_1_7.Drop += new DragEventHandler(DuelEvent.card_Drop_Monster);
            card_1_8.Drop += new DragEventHandler(DuelEvent.card_Drop_Monster);
            card_1_9.Drop += new DragEventHandler(DuelEvent.card_Drop_Monster);
            card_1_10.Drop += new DragEventHandler(DuelEvent.card_Drop_Monster);

            card_1_Deck.ContextMenu = AllMenu.cm_deck;
            card_1_Deck.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_1_Deck.Drop += new DragEventHandler(DuelEvent.card_Drop_Main);
            card_1_Extra.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_1_Extra.Drop += new DragEventHandler(DuelEvent.card_Drop_Extra);
            card_1_hand.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_1_hand.Drop += new DragEventHandler(DuelEvent.card_Drop_Hand);
            card_1_Left.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_1_Left.Drop += new DragEventHandler(DuelEvent.card_Drop_Pendulum);
            card_1_Right.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_1_Right.Drop += new DragEventHandler(DuelEvent.card_Drop_Pendulum);
            card_1_Graveyard.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            
            
            card_1_Graveyard.Drop += new DragEventHandler(DuelEvent.card_Drop_Graveyard);
            card_1_Outside.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_1_Outside.Drop += new DragEventHandler(DuelEvent.card_Drop_Outside);

            card_2_1.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_2_2.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_2_3.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_2_4.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_2_5.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_2_1.Drop += new DragEventHandler(DuelEvent.card_Drop_OpMagic);
            card_2_2.Drop += new DragEventHandler(DuelEvent.card_Drop_OpMagic);
            card_2_3.Drop += new DragEventHandler(DuelEvent.card_Drop_OpMagic);
            card_2_4.Drop += new DragEventHandler(DuelEvent.card_Drop_OpMagic);
            card_2_5.Drop += new DragEventHandler(DuelEvent.card_Drop_OpMagic);

            card_2_6.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_2_7.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_2_8.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_2_9.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_2_10.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_2_6.Drop += new DragEventHandler(DuelEvent.card_Drop_OpMonster);
            card_2_7.Drop += new DragEventHandler(DuelEvent.card_Drop_OpMonster);
            card_2_8.Drop += new DragEventHandler(DuelEvent.card_Drop_OpMonster);
            card_2_9.Drop += new DragEventHandler(DuelEvent.card_Drop_OpMonster);
            card_2_10.Drop += new DragEventHandler(DuelEvent.card_Drop_OpMonster);

            card_2_hand.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            card_2_hand.Drop += new DragEventHandler(DuelEvent.card_Drop_Hand2);
            
            cv_main.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            cv_main.Drop += new DragEventHandler(DuelEvent.sideMode_Drop);
            cv_extra.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            cv_extra.Drop += new DragEventHandler(DuelEvent.sideMode_Drop);
            cv_side.DragOver += new DragEventHandler(DuelEvent.card_DragOver);
            cv_side.Drop += new DragEventHandler(DuelEvent.sideMode_Drop);

            TriggerAction ta = this.Resources["hide_menu"] as TriggerAction;
            this.bsb_menu_hide.Actions.Add(ta);

            #endregion

            #region 初始化控件的卡片处理事件

            #region 怪物区

            card_1_6.WhenAddChildren += new CollectionChangeDelegate(CardAreaEvent.add2Monster);
            card_1_7.WhenAddChildren += new CollectionChangeDelegate(CardAreaEvent.add2Monster);
            card_1_8.WhenAddChildren += new CollectionChangeDelegate(CardAreaEvent.add2Monster);
            card_1_9.WhenAddChildren += new CollectionChangeDelegate(CardAreaEvent.add2Monster);
            card_1_10.WhenAddChildren += new CollectionChangeDelegate(CardAreaEvent.add2Monster);
            card_1_6.WhenInsertChildren += new CollectionChangeDelegate(CardAreaEvent.insert2Monster);
            card_1_7.WhenInsertChildren += new CollectionChangeDelegate(CardAreaEvent.insert2Monster);
            card_1_8.WhenInsertChildren += new CollectionChangeDelegate(CardAreaEvent.insert2Monster);
            card_1_9.WhenInsertChildren += new CollectionChangeDelegate(CardAreaEvent.insert2Monster);
            card_1_10.WhenInsertChildren += new CollectionChangeDelegate(CardAreaEvent.insert2Monster);
            card_1_6.WhenRemoveChildren += new CollectionChangeDelegate(CardAreaEvent.removeFromMonster);
            card_1_7.WhenRemoveChildren += new CollectionChangeDelegate(CardAreaEvent.removeFromMonster);
            card_1_8.WhenRemoveChildren += new CollectionChangeDelegate(CardAreaEvent.removeFromMonster);
            card_1_9.WhenRemoveChildren += new CollectionChangeDelegate(CardAreaEvent.removeFromMonster);
            card_1_10.WhenRemoveChildren += new CollectionChangeDelegate(CardAreaEvent.removeFromMonster);

            #endregion

            #region 魔陷区

            card_1_1.WhenAddChildren += new CollectionChangeDelegate(CardAreaEvent.add2MagicTrap);
            card_1_2.WhenAddChildren += new CollectionChangeDelegate(CardAreaEvent.add2MagicTrap);
            card_1_3.WhenAddChildren += new CollectionChangeDelegate(CardAreaEvent.add2MagicTrap);
            card_1_4.WhenAddChildren += new CollectionChangeDelegate(CardAreaEvent.add2MagicTrap);
            card_1_5.WhenAddChildren += new CollectionChangeDelegate(CardAreaEvent.add2MagicTrap);
            card_1_1.WhenRemoveChildren += new CollectionChangeDelegate(CardAreaEvent.removeFromMagicTrap);
            card_1_2.WhenRemoveChildren += new CollectionChangeDelegate(CardAreaEvent.removeFromMagicTrap);
            card_1_3.WhenRemoveChildren += new CollectionChangeDelegate(CardAreaEvent.removeFromMagicTrap);
            card_1_4.WhenRemoveChildren += new CollectionChangeDelegate(CardAreaEvent.removeFromMagicTrap);
            card_1_5.WhenRemoveChildren += new CollectionChangeDelegate(CardAreaEvent.removeFromMagicTrap);

            #endregion 墓地

            card_1_Graveyard.WhenRemoveChildren = new CollectionChangeDelegate(CardAreaEvent.romoveFromGraveyard);
            card_1_Graveyard.WhenAddChildren = new CollectionChangeDelegate(CardAreaEvent.add2Graveyrad);

            #endregion

            #region 手牌
            card_1_hand.WhenRemoveChildren += new CollectionChangeDelegate(CardAreaEvent.removeFromHand);
            #endregion

            #region <-- 注册其他控件事件 -->

            view_Deck.Click += new RoutedEventHandler(DuelEvent.view_Deck_Click);             //查看卡组
            view_Extra.Click += new RoutedEventHandler(DuelEvent.view_Extra_Click);           //查看额外
            view_Graveyard.Click += new RoutedEventHandler(DuelEvent.view_Graveyard_Click);   //查看墓地
            view_Outside.Click += new RoutedEventHandler(DuelEvent.view_Outside_Click);       //查看除外
            view_Extra2.Click += new RoutedEventHandler(DuelEvent.view_Extra2_Click);         //查看额外（对手）
            view_Outside2.Click += new RoutedEventHandler(DuelEvent.view_Outside2_Click);     //查看除外（对手）
            view_Graveyard2.Click += new RoutedEventHandler(DuelEvent.view_Graveyard2_Click); //查看墓地（对手）
         


            btn_choosezone.Click += new RoutedEventHandler(DuelEvent.btn_choosezone); //场地选择
            btn_deck.Click += new RoutedEventHandler(DuelEvent.btn_deck);             //卡组管理
            btn_choosedeckCancel.Click += new RoutedEventHandler(DuelEvent.btn_choosedeckCancel); //取消选择
            lb_firstdocument.SelectionChanged += new SelectionChangedEventHandler(DuelEvent.ListBox_SelectionChanged);  //卡组管理第一个选项卡
            lb_firstdocument2.SelectionChanged += new SelectionChangedEventHandler(DuelEvent.ListBox_SelectionChanged); //卡组管理第二个选项卡
            lb_firstdocument3.SelectionChanged += new SelectionChangedEventHandler(DuelEvent.ListBox_SelectionChanged); //卡组管理第三个选项卡
            btn_start.Click += new RoutedEventHandler(DuelEvent.btn_start);             //开局按钮
            btn_firstAtk.Click += new RoutedEventHandler(DuelEvent.btn_firstAtk);       //先攻按钮
            btn_secondAtk.Click += new RoutedEventHandler(DuelEvent.btn_secondAtk);     //后攻按钮
            btn_sideMode.Click += new RoutedEventHandler(DuelEvent.btn_sideMode);
            btn_sideModeCancel.Click += new RoutedEventHandler(DuelEvent.btn_sideModeCancel);
            btn_viewreport.Click += new RoutedEventHandler(DuelEvent.btn_viewreport);   //R按钮（查看战报）
            tb_chat_send.KeyDown += new KeyEventHandler(DuelEvent.tb_chatsend_KeyDown); //聊天发送框
            btn_roll.Click += new RoutedEventHandler(DuelEvent.btn_roll);
            btn_coin.Click += new RoutedEventHandler(DuelEvent.btn_coin);

            rta_dp.MouseDown += new MouseButtonEventHandler(DuelEvent.Rectangle_MouseDown); //阶段按钮DP
            rta_sp.MouseDown += new MouseButtonEventHandler(DuelEvent.Rectangle_MouseDown); //阶段按钮SP
            rta_m1.MouseDown += new MouseButtonEventHandler(DuelEvent.Rectangle_MouseDown); //阶段按钮M1
            rta_bp.MouseDown += new MouseButtonEventHandler(DuelEvent.Rectangle_MouseDown); //阶段按钮BP
            rta_m2.MouseDown += new MouseButtonEventHandler(DuelEvent.Rectangle_MouseDown); //阶段按钮M2
            rta_ep.MouseDown += new MouseButtonEventHandler(DuelEvent.Rectangle_MouseDown); //阶段按钮EP

            #endregion

            #region <-- 批量操作菜单的事件注册 -->

            mi_monster_2Graveyard.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            mi_monster_2Outside.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            mi_monster_2Outside2.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            mi_monster_2hand.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            mi_monster_2hand2.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            mi_monster_2defUp.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            mi_monster_2atkUp.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            mi_monster_2defDown.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            mi_monster_2Main.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            mi_monster_2Main2.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            mi_monster_shuffle.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

            mi_magictrap_2Graveyard.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            mi_magictrap_2hand.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            mi_magictrap_2Outside.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            mi_magictrap_2Main.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            mi_magictrap_shuffle.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

            mi_hand_2Graveyard.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            mi_hand_2Outside.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            mi_hand_2Main.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

            mi_field_2Graveyard.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);        
            mi_field_2hand.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            mi_field_2hand2.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            mi_field_2Outside.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            mi_field_2Outside2.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            mi_field_shuffle.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
         
            #endregion

            #region <-- 加载全卡信息 -->
            
            CardDataOperate.GetAllCard();

            #endregion

            #region <-- 初始化用户配置信息 -->

            AppConfigOperate.getInstance();

            #endregion

            #region <-- 初始化决斗准备 -->

            DuelOperate.getInstance(this);

            #endregion

            #region <-- 初始卡组管理器 -->

            //Binding testbinding = new Binding();
            //testbinding.Mode = BindingMode.TwoWay;
            //testbinding.Path = new PropertyPath("first");
            DecksManergerOperate.getInstance(this);
            //设置数据banding
            this.tbc_DeckDocument.DataContext = DecksManergerOperate.getInstance();

            #endregion

            

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //if (lb_firstdocument.SelectedItem == null)
            //{
            //    lb_firstdocument.SelectedItem = lb_firstdocument.Items[0];
            //}
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = e.Source as Image;
            if (e.LeftButton == MouseButtonState.Pressed && img != null && img.Name.Equals("img_head_op"))
            {
                this.DragMove();
            }
            
        }



        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Server iduel_Server = Server.getInstance(this);
            iduel_Server.startlisten(tb_ip.Text, tb_socket.Text);
            mi_listen.IsEnabled = false;
            mi_connect.IsEnabled = false;
            mi_stopconnect.IsEnabled = false;
            DuelOperate.getInstance().myself.userindex = "1";
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Client iduel_Client = Client.getInstance(this);
            iduel_Client.startConnect(tb_ip.Text, tb_socket.Text);
            mi_connect.IsEnabled = false;
            mi_listen.IsEnabled = false;
            DuelOperate.getInstance().myself.userindex = "2";
        }
   

        private void mi_stopconnect_Click(object sender, RoutedEventArgs e)
        {
            if (Client.check())
            {
                Client cl = Client.getInstance();
                cl.disConnect();
            }
            mi_connect.IsEnabled = true;
            mi_listen.IsEnabled = true;
        }

        private void mi_stoplisten_Click(object sender, RoutedEventArgs e)
        {
            if (Server.check())
            {
                Server sr = Server.getInstance();
                sr.stoptlisten();
            }
            mi_listen.IsEnabled = true;
            mi_connect.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void report_TextChanged(object sender, TextChangedEventArgs e)
        {
            report.ScrollToEnd();
        }

        private void tb_setDuelistName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine("tb_setDuelistName_TextChanged");
            tbk_myname.Text = tb_Duelist.Text;
            if (DuelOperate.isExist())
            {
                DuelOperate.getInstance().myself.name = tb_Duelist.Text;
            }
            
        }

        /// <summary>
        /// F5快捷键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                DuelOperate.getInstance().DuelStart();
            }
        }

        private void tb_chat_view_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        
    
        

        

        
        
    }

    

    

    
}
