using iDuel_EvolutionX.Model;
using iDuel_EvolutionX.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace iDuel_EvolutionX.UI
{
    /// <summary>
    /// 添加菜单和注册菜单事件
    /// </summary>
    public sealed class AllMenu
    {
        private static readonly AllMenu _instance = new AllMenu();
        public CardMenuDeck cm_deck;
        public CardMenuMonster cm_monster;
        public CardMenuMagicTrap cm_magictrap;
        public CardMenuGraveyard cm_graveyard;
        public CardMenuHand cm_hand;
        public CardMenuOutside cm_outside;
        public CardMenuPendulum cm_pendulum; 

        
        //public static 
        //public static CardMenuXYZ cm_XYZ = new CardMenuXYZ();
        //public static CardMenuXYZ_material cm_XYZmeterial = new CardMenuXYZ_material();

        public static AllMenu Instance
        {
            get
            {
                return _instance;
            }
        }

        static AllMenu()
        {
            
        }

        private AllMenu()
        {
            
            cm_deck = new CardMenuDeck();
            cm_monster = new CardMenuMonster();
            cm_magictrap = new CardMenuMagicTrap();
            cm_graveyard = new CardMenuGraveyard();
            cm_hand = new CardMenuHand();
            cm_outside = new CardMenuOutside();
            cm_pendulum = new CardMenuPendulum();
    }

        /// <summary>
        /// 怪物场区的菜单
        /// </summary>
        public class CardMenuMonster : ContextMenu
        {        
            MenuItem set2AtkOrDef  = new MenuItem { Header = "攻 ↔ 守 形式转换" }; //OK
            MenuItem set2FrontOrBack  = new MenuItem { Header = "里 ↔ 表 形式转换" }; //OK
            MenuItem set2BackDef  = new MenuItem { Header = " → 里侧守备" };  //OK
            MenuItem mi_4  = new MenuItem { Header = "攻击宣言" };
            MenuItem mi_5  = new MenuItem { Header = "效果发动" };
            MenuItem mi_6  = new MenuItem { Header = "转移控制权" };
        
            MenuItem release = new MenuItem { Header = "解放" };
            MenuItem release2Graveyard = new MenuItem { Header = "解放 → 墓地" };
            MenuItem release2Banish = new MenuItem { Header = "解放 → 除外" };


            

            MenuItem setCardRemark = new MenuItem { Header = "修改备注" };



            public CardMenuMonster()
            {
                this.MouseWheel += CardMenuMonster_MouseWheel;

                this.AddChild(set2AtkOrDef);
                AllMenu.setMenuItemBind(set2AtkOrDef);


                set2AtkOrDef.Command = CardCommands.Set2AtkOrDef;

                this.AddChild(set2FrontOrBack);
                AllMenu.setMenuItemBind(set2FrontOrBack);
                set2FrontOrBack.Command = CardCommands.Set2FrontOrBack;

                this.AddChild(set2BackDef);
                AllMenu.setMenuItemBind(set2BackDef);
                set2BackDef.Command = CardCommands.Set2BackDef;

                this.AddChild(new Separator());



                mi_4.IsEnabled = false;
                //this.AddChild(mi_4);
                mi_4.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);



                this.AddChild(release2Graveyard);
                //release.Items.Add(release2Graveyard);
                AllMenu.setMenuItemBind(release2Graveyard);
                release2Graveyard.Command = CardCommands.Release2Graveyard;
                this.AddChild(release2Banish);
                //release.Items.Add(release2Banish);
                AllMenu.setMenuItemBind(release2Banish);
                release2Banish.Command = CardCommands.Release2Banish;
                //this.AddChild(release);

                this.AddChild(new Separator());

                //this.AddChild(quecksetSign);
                //AllMenu.setMenuItemBind(quecksetSign);
                //quecksetSign.Command = CardCommands.AddBlueSign;

                //AllMenu.addSignMenuItems(setSign);
                //this.AddChild(setSign);

                StackPanel sp = getSetSignItem();
                this.AddChild(sp);

                this.AddChild(new Separator());
                AllMenu.setMenuItemBind(setCardRemark);
                setCardRemark.Command = CardCommands.SetCardRemark;
                this.AddChild(setCardRemark);



            }

            

            private void SetSign_Click(object sender, RoutedEventArgs e)
            {
                MessageBox.Show(sender.ToString());
            }
        }

        

        /// <summary>
        /// 魔陷场区的菜单，场地区的菜单
        /// </summary>
        public class CardMenuMagicTrap : ContextMenu
        {
            MenuItem mi_1 = new MenuItem { Header = "效果发动" };      
            MenuItem mi_2 = new MenuItem { Header = "放回卡组顶端" };  //OK
            MenuItem mi_3 = new MenuItem { Header = "送入墓地" };      //OK
            MenuItem mi_4 = new MenuItem { Header = "从游戏中除外" };  //OK
            MenuItem mi_5 = new MenuItem { Header = "加入手卡" };      //OK
            MenuItem set2FrontOrBack = new MenuItem { Header = "打开/盖伏" };     //OK
            MenuItem mi_7 = new MenuItem { Header = "放回卡组顶端" };  //OK
            MenuItem mi_8 = new MenuItem { Header = "指示物" };      
            MenuItem setCardRemark = new MenuItem { Header = "修改备注" };

            

            public CardMenuMagicTrap()
            {
                this.MouseWheel += CardMenuMonster_MouseWheel;

                mi_1.IsEnabled = false;
                this.AddChild(mi_1);
                mi_1.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                //this.AddChild(mi_2);
                //mi_2.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                //this.AddChild(mi_3);
                //mi_3.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                //this.AddChild(mi_4);
                //mi_4.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                //this.AddChild(mi_5);
                //mi_5.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(set2FrontOrBack);
                AllMenu.setMenuItemBind(set2FrontOrBack);
                set2FrontOrBack.Command = CardCommands.Set2FrontOrBack;
                //this.AddChild(mi_6);
                //mi_6.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                //this.AddChild(mi_7);
                //mi_7.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                //this.mi_8.Items.Add(addSignBlue);
                //this.mi_8.Items.Add(addSignBlack);
                //this.mi_8.Items.Add(addSignRed);
                //this.mi_8.Items.Add(addSignGreen);
                //AllMenu.addSignMenuItems(mi_8);
                //this.AddChild(mi_8);
                this.AddChild(new Separator());
                StackPanel sp = getSetSignItem();
                this.AddChild(sp);
                this.AddChild(new Separator());
                //this.mi_8.Items.Add(mi_10);
                //mi_10.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
                //this.mi_8.Items.Add(mi_11);
                //mi_11.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
                //this.mi_8.Items.Add(mi_12);
                //mi_12.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
                //this.mi_8.Items.Add(mi_13);
                //mi_13.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
                //this.AddChild(mi_8);
                //mi_8.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
                AllMenu.setMenuItemBind(setCardRemark);
                setCardRemark.Command = CardCommands.SetCardRemark;
                this.AddChild(setCardRemark);


            }
        }

        /// <summary>
        /// 手卡的菜单
        /// </summary>
        public class CardMenuHand : ContextMenu
        {
            MenuItem mi_1 = new MenuItem { Header = "放置到场上" };
            MenuItem mi_2 = new MenuItem { Header = "召唤/发动" };  
            MenuItem mi_3 = new MenuItem { Header = "特殊召唤" };      
            MenuItem back2MainDeck = new MenuItem { Header = "放回卡组顶端" };   //OK
            MenuItem release2Graveyard = new MenuItem { Header = "送入墓地" };       //OK
            MenuItem release2Banish = new MenuItem { Header = "除外此卡" };   //OK
            MenuItem mi_7 = new MenuItem { Header = "效果发动" };  

            public CardMenuHand()
            {
                this.MouseWheel += CardMenuMonster_MouseWheel;

                mi_1.IsEnabled = false;
                this.AddChild(mi_1);
                mi_1.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                mi_2.IsEnabled = false;
                this.AddChild(mi_2);
                mi_2.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                mi_3.IsEnabled = false;
                this.AddChild(mi_3);
                mi_3.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(back2MainDeck);
                AllMenu.setMenuItemBind(back2MainDeck);
                back2MainDeck.Command = CardCommands.Back2MainDeck;   

                this.AddChild(release2Graveyard);
                AllMenu.setMenuItemBind(release2Graveyard);
                release2Graveyard.Command = CardCommands.Release2Graveyard;


                this.AddChild(release2Banish);
                AllMenu.setMenuItemBind(release2Banish);
                release2Banish.Command = CardCommands.Release2Banish;

                mi_7.IsEnabled = false;
                this.AddChild(mi_7);
                mi_7.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);


            }
  
        }

        /// <summary>
        /// 墓地的菜单
        /// </summary>
        public class CardMenuGraveyard : ContextMenu
        {
            MenuItem mi_1 = new MenuItem { Header = "特殊召唤/发动" };
            MenuItem mi_2 = new MenuItem { Header = "效果发动" };
            MenuItem mi_3 = new MenuItem { Header = "加入手卡" };       //OK
            MenuItem mi_4 = new MenuItem { Header = "放回卡组顶端" };   //OK
            MenuItem mi_5 = new MenuItem { Header = "从游戏中除外" };   //OK

            public CardMenuGraveyard()
            {
                this.MouseWheel += CardMenuMonster_MouseWheel;

                mi_1.IsEnabled = false;
                this.AddChild(mi_1);
                mi_1.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                mi_2.IsEnabled = false;
                this.AddChild(mi_2);
                mi_2.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_3);
                mi_3.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_4);
                mi_4.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_5);
                mi_5.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            }
        }

        /// <summary>
        /// 卡组的菜单
        /// </summary>
        public class CardMenuDeck : ContextMenu
        {
            //MenuItem mi_1 = new MenuItem { Header = "加入手卡" };               //OK
            MenuItem mi_2 = new MenuItem { Header = "切洗卡组" };               //OK
            MenuItem mi_3 = new MenuItem { Header = "抽X张卡片" };
            MenuItem mi_4 = new MenuItem { Header = "将顶牌放回卡组底部" };     //OK
            //MenuItem mi_5 = new MenuItem { Header = "将顶牌送往墓地" };         //OK
            //MenuItem mi_6 = new MenuItem { Header = "将顶牌从游戏中除外" };     //OK
            MenuItem mi_7 = new MenuItem { Header = "将顶牌背面除外" };         //OK
            MenuItem mi_8 = new MenuItem { Header = "查看顶牌" };
            MenuItem mi_9 = new MenuItem { Header = "查看卡组上方X张" };     

             public CardMenuDeck()
            {
                this.MouseWheel += CardMenuMonster_MouseWheel;

                //this.AddChild(mi_1);
                //mi_1.Click += new RoutedEventHandler(CardEvent.MenuItem_Handle);

                this.AddChild(mi_2);
                mi_2.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                mi_3.IsEnabled = false;
                this.AddChild(mi_3);
                mi_3.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_4);
                mi_4.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                //this.AddChild(mi_5);
                //mi_5.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                //this.AddChild(mi_6);
                //mi_6.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_7);
                mi_7.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                mi_8.IsEnabled = false;
                this.AddChild(mi_8);
                mi_8.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                mi_9.IsEnabled = false;
                this.AddChild(mi_9);
                mi_9.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);


            }

        }

        /// <summary>
        /// 除外区菜单
        /// </summary>
        public class CardMenuOutside : ContextMenu
        { 
            MenuItem mi_1 = new MenuItem { Header = "特殊召唤/发动" };
            MenuItem mi_2 = new MenuItem { Header = "效果发动" };
            MenuItem mi_3 = new MenuItem { Header = "加入手卡" };       //OK
            MenuItem mi_4 = new MenuItem { Header = "放回卡组顶端" };   //OK
            MenuItem mi_5 = new MenuItem { Header = "送入墓地" };       //OK

            public CardMenuOutside()
            {
                mi_1.IsEnabled = false; 
                this.AddChild(mi_1);
                mi_1.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                mi_2.IsEnabled = false;
                this.AddChild(mi_2);
                mi_2.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_3);
                mi_3.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_4);
                mi_4.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_5);
                mi_5.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            }
        
        }

        /// <summary>
        /// 灵摆区菜单
        /// </summary>
        public class CardMenuPendulum : ContextMenu
        {
            MenuItem mi_1 = new MenuItem { Header = "效果发动" };
            MenuItem mi_2 = new MenuItem { Header = "返回额外区" };     //OK
            MenuItem mi_3 = new MenuItem { Header = "加入手卡" };       //OK
            MenuItem sign = new MenuItem { Header = "指示物" };

            public CardMenuPendulum()
            {
                this.MouseWheel += CardMenuMonster_MouseWheel;

                mi_1.IsEnabled = false;
                this.AddChild(mi_1);
                mi_1.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_2);
                mi_2.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_3);
                mi_3.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(new Separator());
                StackPanel sp = getSetSignItem();
                this.AddChild(sp);
            }
        }

        /// <summary>
        /// 怪物素材菜单
        /// </summary>
        public class CardMenuXYZ_material : ContextMenu
        {
            MenuItem mi_1 = new MenuItem { Header = "送入墓地" };
            MenuItem mi_2 = new MenuItem { Header = "从游戏中除外" };
            MenuItem mi_3 = new MenuItem { Header = "返回额外区" };
            MenuItem mi_4 = new MenuItem { Header = "加入手卡" };  
            MenuItem mi_5 = new MenuItem { Header = "转移控制权" };
            MenuItem mi_6 = new MenuItem { Header = "送入对手墓地" };
            MenuItem mi_7 = new MenuItem { Header = "修改备注" };

            public CardMenuXYZ_material()
            {
                this.MouseWheel += CardMenuMonster_MouseWheel;

                this.AddChild(mi_1);
                mi_1.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_2);
                mi_2.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_3);
                mi_3.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                mi_4.IsEnabled = false;
                this.AddChild(mi_4);
                mi_4.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                mi_5.IsEnabled = false;
                this.AddChild(mi_5);
                mi_5.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                //mi_6.IsEnabled = false;
                this.AddChild(mi_6);
                mi_6.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_7);
                mi_7.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                
            }



        }

        /// <summary>
        /// XYZ怪物场区的菜单
        /// </summary>
        public class CardMenuXYZ : ContextMenu
        {
            MenuItem mi_1 = new MenuItem { Header = "攻/守形式转换" }; //OK
            MenuItem mi_2 = new MenuItem { Header = "里侧/表侧转换" }; //OK
            MenuItem mi_3 = new MenuItem { Header = "转为里侧守备" };  //OK
            MenuItem mi_4 = new MenuItem { Header = "攻击宣言" };
            MenuItem mi_5 = new MenuItem { Header = "效果发动" };
            MenuItem mi_6 = new MenuItem { Header = "素材操作" };
            MenuItem mi_7 = new MenuItem { Header = "转移控制权" };
            MenuItem mi_8 = new MenuItem { Header = "返回额外区" };  //OK
            MenuItem mi_9 = new MenuItem { Header = "送入墓地" };      //OK
            MenuItem mi_10 = new MenuItem { Header = "从游戏中除外" };  //OK      
            MenuItem mi_11 = new MenuItem { Header = "解放" };          //OK
            MenuItem mi_12 = new MenuItem { Header = "加入手卡" };      //OK
            MenuItem mi_13 = new MenuItem { Header = "指示物操作" };
            MenuItem mi_14 = new MenuItem { Header = "修改备注" };
            MenuItem mi_15 = new MenuItem { Header = "" };    //OK
            MenuItem mi_16 = new MenuItem { Header = "送入对手墓地" };  //OK

            MenuItem mi_17 = new MenuItem { Header = "把最下面一个素材送往墓地" };
            MenuItem mi_18 = new MenuItem { Header = "把最下面一个素材除外" };
            MenuItem mi_19 = new MenuItem { Header = "素材全部送往墓地" };
            MenuItem mi_20 = new MenuItem { Header = "素材全部除外" };


            public CardMenuXYZ()
            {
                
                
                this.AddChild(mi_1);
                mi_1.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_2);
                mi_2.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_3);
                mi_3.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                mi_4.IsEnabled = false;
                this.AddChild(mi_4);
                mi_4.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                mi_5.IsEnabled = false;
                this.AddChild(mi_5);
                mi_5.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                //mi_6.IsEnabled = false;
                this.mi_6.Items.Add(mi_17);
                this.mi_6.Items.Add(mi_18);
                this.mi_6.Items.Add(mi_19);
                this.mi_6.Items.Add(mi_20);
                this.AddChild(mi_6);
                
               // mi_6.Click += new RoutedEventHandler(CardEvent.MenuItem_Handle);

                this.AddChild(mi_7);
                mi_7.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_8);
                mi_8.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_9);
                mi_9.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_10);
                mi_10.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_11);
                mi_11.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                mi_12.IsEnabled = false;
                this.AddChild(mi_12);
                mi_12.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                mi_13.IsEnabled = false;
                this.AddChild(mi_13);
                mi_13.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_14);
                mi_14.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                //mi_15.IsEnabled = false;
                this.AddChild(mi_15);
                mi_15.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
            }



        }

        /// <summary>
        /// 绑定命令
        /// </summary>
        /// <param name="menu"></param>
        private static void setMenuItemBind(MenuItem menu)
        {
            Binding bind = new Binding("PlacementTarget");
            bind.RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(ContextMenu), 1);
            menu.SetBinding(MenuItem.CommandTargetProperty, bind);
        }

        /// <summary>
        /// 菜单的关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CardMenuMonster_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                return;
            }
            ContextMenu cm = sender as ContextMenu;
            cm.IsOpen = false;

        }

        public static StackPanel getSetSignItem()
        {
            MenuItem addSignBlue = new MenuItem { Header = "蓝" };   
            MenuItem addSignBlack = new MenuItem { Header = "黑" };  
            MenuItem addSignRed = new MenuItem { Header = "红" };    
            MenuItem addSignGreen = new MenuItem { Header = "绿" };

            
            AllMenu.setMenuItemBind(addSignBlue);
            AllMenu.setMenuItemBind(addSignBlack);
            AllMenu.setMenuItemBind(addSignRed);
            AllMenu.setMenuItemBind(addSignGreen);

            addSignBlue.Command = CardCommands.AddBlueSign;
            addSignBlack.Command = CardCommands.AddBlackSign;
            addSignRed.Command = CardCommands.AddRedSign;
            addSignGreen.Command = CardCommands.AddGreenSign;

            ControlTemplate menuItemCT = Application.Current.TryFindResource("MyMenuItem") as ControlTemplate;
            addSignBlue.Width = addSignBlack.Width = addSignRed.Width = addSignGreen.Width = 25;
            addSignBlue.Template = addSignBlack.Template = addSignRed.Template = addSignGreen.Template = menuItemCT;

            TextBlock tb = new TextBlock();
            tb.VerticalAlignment = VerticalAlignment.Center;
            tb.Text = "指示物：";

            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.Children.Add(tb);
            sp.Children.Add(addSignBlue);
            sp.Children.Add(addSignBlack);
            sp.Children.Add(addSignRed);
            sp.Children.Add(addSignGreen);

            return sp;
        }

        /// <summary>
        /// 添加指示物选项菜单
        /// </summary>
        /// <param name="menu"></param>
        private static void addSignMenuItems(MenuItem menu)
        {

            Binding bind = new Binding("PlacementTarget");
            bind.RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(ContextMenu), 1);

            MenuItem addSignBlue = new MenuItem { Header = "蓝" };   //OK
            addSignBlue.SetBinding(MenuItem.CommandTargetProperty, bind);
            MenuItem addSignBlack = new MenuItem { Header = "黑" };  //OK
            addSignBlack.SetBinding(MenuItem.CommandTargetProperty, bind);
            MenuItem addSignRed = new MenuItem { Header = "红" };    //OK
            addSignRed.SetBinding(MenuItem.CommandTargetProperty, bind);
            MenuItem addSignGreen = new MenuItem { Header = "绿" };
            addSignGreen.SetBinding(MenuItem.CommandTargetProperty, bind);

            addSignBlue.Command = CardCommands.AddBlueSign;
            addSignBlack.Command = CardCommands.AddBlackSign;
            addSignRed.Command = CardCommands.AddRedSign;
            addSignGreen.Command = CardCommands.AddGreenSign;

            menu.Items.Add(addSignBlue);
            menu.Items.Add(addSignBlack);
            menu.Items.Add(addSignRed);
            menu.Items.Add(addSignGreen);
        }

        
    }
}
