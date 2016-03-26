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
    public static class AllMenu
    {
        public static CardMenuDeck cm_deck = new CardMenuDeck();
        public static CardMenuMonster cm_monster = new CardMenuMonster();
        public static CardMenuMagicTrap cm_magictrap = new CardMenuMagicTrap();
        public static CardMenuGraveyard cm_graveyard = new CardMenuGraveyard();
        public static CardMenuHand cm_hand = new CardMenuHand();
        public static CardMenuOutside cm_outside = new CardMenuOutside();
        public static CardMenuPendulum cm_pendulum = new CardMenuPendulum();
        //public static 
        //public static CardMenuXYZ cm_XYZ = new CardMenuXYZ();
        //public static CardMenuXYZ_material cm_XYZmeterial = new CardMenuXYZ_material();



        /// <summary>
        /// 怪物场区的菜单
        /// </summary>
        public class CardMenuMonster : ContextMenu
        {        
            MenuItem mi_1  = new MenuItem { Header = "攻/守形式转换" }; //OK
            MenuItem mi_2  = new MenuItem { Header = "里侧/表侧转换" }; //OK
            MenuItem mi_3  = new MenuItem { Header = "转为里侧守备" };  //OK
            MenuItem mi_4  = new MenuItem { Header = "攻击宣言" };
            MenuItem mi_5  = new MenuItem { Header = "效果发动" };
            MenuItem mi_6  = new MenuItem { Header = "转移控制权" };
            MenuItem mi_7  = new MenuItem { Header = "放回卡组顶端" };  //OK
            MenuItem mi_8  = new MenuItem { Header = "送入墓地" };      //OK
            MenuItem mi_9  = new MenuItem { Header = "从游戏中除外" };  //OK      
            MenuItem mi_10 = new MenuItem { Header = "解放" };          //OK
            MenuItem mi_11 = new MenuItem { Header = "加入手卡" };      //OK
            MenuItem mi_12 = new MenuItem { Header = "指示物" };

            MenuItem mi_13 = new MenuItem { Header = "修改备注" };
            MenuItem mi_14 = new MenuItem { Header = "返回额外区" };    //OK
            MenuItem mi_15 = new MenuItem { Header = "送入对手墓地" };  //OK
            MenuItem mi_16 = new MenuItem { Header = "修改攻守" };  //OK

            MenuItem mi_17 = new MenuItem { Header = "黑" };  //OK
            MenuItem mi_18 = new MenuItem { Header = "蓝" };  //OK
            MenuItem mi_19 = new MenuItem { Header = "红" };  //OK
            MenuItem mi_20 = new MenuItem { Header = "清除" };

            public CardMenuMonster()
            {
                this.AddChild(mi_1);
                mi_1.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_2);
                mi_2.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_3);
                mi_3.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                mi_4.IsEnabled = false;
                //this.AddChild(mi_4);
                mi_4.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                //mi_5.IsEnabled = false;
                this.AddChild(mi_5);
                mi_5.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                //mi_6.IsEnabled = false;
                //this.AddChild(mi_6);
                mi_6.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                //this.AddChild(mi_7);
                mi_7.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                //this.AddChild(mi_8);
                mi_8.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                //this.AddChild(mi_9);
                mi_9.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_10);
                mi_10.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                //this.AddChild(mi_11);
                mi_11.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.mi_12.Items.Add(mi_17);
                CommandBinding cb = new CommandBinding(CardCommands.AddSign);
                cb.Executed += (o, target) => {

                    MessageBox.Show("测试");
                };
                //mi_17.CommandBindings.Add(cb);
                mi_17.Command = CardCommands.AddSign;
                Binding bind = new Binding("PlacementTarget");
                bind.RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(ContextMenu),1);
                mi_17.SetBinding(MenuItem.CommandTargetProperty, bind);
                //mi_17.DataContext = mi_17.FindCommonVisualAncestor(mi_12);
                //mi_17.Command.CanExecute();
                //mi_17.CommandTarget = this.mi_12;
                //mi_17.CommandTarget = mi_12;
                //Application.Current.MainWindow.CommandBindings.Add(cb);

                //mi_17.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
                this.mi_12.Items.Add(mi_18);
                mi_18.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
                this.mi_12.Items.Add(mi_19);
                mi_19.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
                this.mi_12.Items.Add(mi_20);
                mi_20.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
                this.AddChild(mi_12);
               
                //mi_13.IsEnabled = false;
                this.AddChild(mi_16);
                mi_16.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                mi_13.IsEnabled = false;
                
                this.AddChild(mi_13);
                //mi_13.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                //this.AddChild(mi_14);
                mi_14.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                //mi_15.IsEnabled = false;
                //this.AddChild(mi_15);
                mi_15.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
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
            MenuItem mi_6 = new MenuItem { Header = "打开/盖伏" };     //OK
            MenuItem mi_7 = new MenuItem { Header = "放回卡组顶端" };  //OK
            MenuItem mi_8 = new MenuItem { Header = "指示物" };      
            MenuItem mi_9 = new MenuItem { Header = "修改备注" };

            MenuItem mi_10 = new MenuItem { Header = "黑" };  //OK
            MenuItem mi_11 = new MenuItem { Header = "蓝" };  //OK
            MenuItem mi_12 = new MenuItem { Header = "红" };  //OK
            MenuItem mi_13 = new MenuItem { Header = "清除" };

            public CardMenuMagicTrap()
            {
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

                this.AddChild(mi_6);
                mi_6.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                //this.AddChild(mi_7);
                //mi_7.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.mi_8.Items.Add(mi_10);
                mi_10.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
                this.mi_8.Items.Add(mi_11);
                mi_11.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
                this.mi_8.Items.Add(mi_12);
                mi_12.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
                this.mi_8.Items.Add(mi_13);
                mi_13.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);
                this.AddChild(mi_8);
                //mi_8.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                mi_9.IsEnabled = false;
                this.AddChild(mi_9);
                mi_9.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);


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
            MenuItem mi_4 = new MenuItem { Header = "放回卡组顶端" };   //OK
            MenuItem mi_5 = new MenuItem { Header = "送入墓地" };       //OK
            MenuItem mi_6 = new MenuItem { Header = "从游戏中除外" };   //OK
            MenuItem mi_7 = new MenuItem { Header = "效果发动" };  

            public CardMenuHand()
            {
                mi_1.IsEnabled = false;
                this.AddChild(mi_1);
                mi_1.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                mi_2.IsEnabled = false;
                this.AddChild(mi_2);
                mi_2.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                mi_3.IsEnabled = false;
                this.AddChild(mi_3);
                mi_3.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_4);
                mi_4.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_5);
                mi_5.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_6);
                mi_6.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

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

        public class CardMenuPendulum : ContextMenu
        {
            MenuItem mi_1 = new MenuItem { Header = "效果发动" };
            MenuItem mi_2 = new MenuItem { Header = "返回额外区" };     //OK
            MenuItem mi_3 = new MenuItem { Header = "加入手卡" };       //OK


            public CardMenuPendulum()
            {
                mi_1.IsEnabled = false;
                this.AddChild(mi_1);
                mi_1.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_2);
                mi_2.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

                this.AddChild(mi_3);
                mi_3.Click += new RoutedEventHandler(DuelEvent.MenuItem_Handle);

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


    }
}
