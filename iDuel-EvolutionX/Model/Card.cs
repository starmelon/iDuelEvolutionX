using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using iDuel_EvolutionX.UI;
using System.Windows;
using iDuel_EvolutionX.Service;
using NBX3.Service;

namespace iDuel_EvolutionX.Model
{
    public class Card:Image
    {
        
        public string duelindex { get; set; }

        public bool isMine { get; set; }

        public BitmapImage img { get; set; } 

        public string name { get; set; }            //卡片名字
        public string cardCamp { get; set; }        //OCG、TCG
        public string sCardType { get; set; }       //卡片类型
        public string CardDType { get; set; }       //副卡类型
        public string tribe { get;set; }            //种族
        public string element { get; set; }         //属性
        public string level { get; set; }           //星数
        public string atk { get; set; }             //攻击力
        public string def { get; set; }             //防守力
        public string effect { get; set; }          //卡片描述
        public string cheatcode { get; set; }       //八位密码
        public string adjust { get; set; }          //卡片调整
        public bool isBack { get; set; }            //正背面
        public bool isDef { get; set; }             //攻守
        public string oldName { get; set; }         //曾用名
        public byte material { get; set; }          //叠放素材数量
        public byte cardPointer1 { get; set; }      //指示物1
        public byte cardPointer2 { get; set; }      //指示物2
        public byte cardPointer3 { get; set; }      //指示物3   

        

        public Card() {
            name = "";
            sCardType = "";
            def = "";
            atk = "";
            RenderTransformOrigin = new Point(0.5, 0.5);
            material = 0;
            cardPointer1 = 0;
            cardPointer2 = 0;
            cardPointer3 = 0;

            Width = 56;//设置卡片宽高
            Height = 81;

            this.MouseEnter += new MouseEventHandler(DuelEvent.card_picture_MouseEnter);
        }

        

        public Card(Card card)
        {
            

            /* 初始化卡片 */
            Uid = card.Uid;

            isMine = true;
            name = card.name;               //卡片名字
            cardCamp = card.cardCamp;               //OCG、TCG
            sCardType = card.sCardType;           //卡片类型
            CardDType = card.CardDType;         //卡片类型
            tribe = card.tribe;           //种族
            element = card.element; //属性
            level = card.level;         //星数
            atk = card.atk;                 //攻击力
            def = card.def;                 //防守力
            effect = card.effect;           //卡片描述
            cheatcode = card.cheatcode;               //八位密码
            adjust = card.adjust;           //卡片调整
            isBack = false;                          //正背面
            isDef = false;                          //攻守
            
            material = 0;                           //叠放素材数量
            cardPointer1 = 0;                       //指示物1
            cardPointer2 = 0;                       //指示物2
            cardPointer3 = 0;                       //指示物3  

            Width = 56;//设置卡片宽高
            Height = 81;
            //Margin = new Thickness(3, 3, 3, 3);
            RenderTransformOrigin = new Point(0.5, 0.5);

            /* 注册卡片 */
            
            ////this.pre
            ////this.MouseMove += new MouseEventHandler(CardEvent.CardDragStart);
            ////this.PreviewMouseDown += new MouseButtonEventHandler(CardEvent.ClikDouble);
            //this.PreviewMouseMove += new MouseEventHandler(DuelEvent.CardDragStart); 
            //this.MouseDown += new MouseButtonEventHandler(DuelEvent.ClikDouble);
                
            //this.QueryContinueDrag += new QueryContinueDragEventHandler(DuelEvent.card_DragContinue);
            //this.MouseEnter += new MouseEventHandler(DuelEvent.card_picture_MouseEnter);
            ////this.Style = FindResource("OuterGlowStyle") as Style;
            ////this.SetResourceReference(Card.StyleProperty, "OuterGlowStyle");
            
            
        }

        public void SetPic()
        {
            if (isBack)
            {
                if(    !CardOperate.cv_monsters_1.Contains(this.Parent)
                    && !CardOperate.cv_magictraps_1.Contains(this.Parent)
                    && !CardOperate.cv_others_1.Contains(this.Parent)
                    )
                {
                    if(DuelOperate.getInstance().opponent.cardback != null)
                    {
                        Source = DuelOperate.getInstance().opponent.cardback;
                    }
                    else
                    {
                        Source = new BitmapImage(new Uri("/Image/Cardback.jpg", UriKind.Relative));
                    }
                }
                else
                {
                    //string str = System.IO.Directory.GetCurrentDirectory() + "\\Data\\Cardback\\0.jpg";
                    if (DuelOperate.getInstance().myself.cardback != null)
                    {
                        Source = DuelOperate.getInstance().myself.cardback;
                    }
                    else
                    {
                        Source = new BitmapImage(new Uri("/Image/Cardback.jpg", UriKind.Relative));
                    }

                }
            }
            else
            {
                //先从本地卡图库取图
                if (img != null)
                {
                    Source = img;
                    return;
                }
                string str = System.IO.Directory.GetCurrentDirectory() + "\\image\\" + Uid + ".jpg";
                if (System.IO.File.Exists(str))
                {
                    //BitmapImage image = new BitmapImage(new Uri(str, UriKind.Absolute));
                    img = new BitmapImage(new Uri(str, UriKind.Absolute));
                    Source = img;
                    
                }
                else
                {
                    //不存在则从使用程序自带默认
                
                    if (sCardType.Contains("怪兽"))
                    {
                        if (CardDType.Contains("灵摆"))
                        {
                            if (sCardType.Contains("通常"))
                            {
                                img = new BitmapImage(new Uri("/Image/Cardpic/GLTT.jpg", UriKind.Relative));
                                Source = img;
                                return;
                            }
                            if (sCardType.Contains("效果"))
                            {
                                img = new BitmapImage(new Uri("/Image/Cardpic/GLXG.jpg", UriKind.Relative));
                                Source = img;
                                return;
                            }
                        }
                        if (CardDType.Contains("同调"))
                        {
                            img = new BitmapImage(new Uri("/Image/Cardpic/GTT.jpg", UriKind.Relative));
                            Source = img;
                            return;                          
                        }
                        if (sCardType.Contains("XYZ"))
                        {
                            img = new BitmapImage(new Uri("/Image/Cardpic/GXYZ.jpg", UriKind.Relative));
                            Source = img;
                            return; 
                        }
                        if (sCardType.Contains("仪式"))
                        {
                            img = new BitmapImage(new Uri("/Image/Cardpic/GRH.jpg", UriKind.Relative));
                            Source = img;
                            return; 
                        }

                        if (sCardType.Contains("融合"))
                        {
                            img = new BitmapImage(new Uri("/Image/Cardpic/GY.jpg", UriKind.Relative));
                            Source = img;
                            return;
                        }

                        if (sCardType.Contains("通常"))
                        {
                            img = new BitmapImage(new Uri("/Image/Cardpic/GTC.jpg", UriKind.Relative));
                            Source = img;
                            return;
                        }
                        if (sCardType.Contains("效果"))
                        {
                            img = new BitmapImage(new Uri("/Image/Cardpic/GXG.jpg", UriKind.Relative));
                            Source = img;
                            return;
                        }

                    }
                    if (sCardType.Contains("魔法"))
                    {
                        img = new BitmapImage(new Uri("/Image/Cardpic/MF.jpg", UriKind.Relative));
                        Source = img;
                        return;
                    }
                    if (sCardType.Contains("陷阱"))
                    {
                        img = new BitmapImage(new Uri("/Image/Cardpic/XJ.jpg", UriKind.Relative));
                        Source = img;
                        return;
                    }
 
                }
                
            }
            //string str = isBack ? System.IO.Directory.GetCurrentDirectory() + "\\image\\0.jpg" : System.IO.Directory.GetCurrentDirectory() + "\\image\\" + Uid + ".jpg" ;
            //BitmapImage image = new BitmapImage(new Uri(str, UriKind.Absolute));//载入图片
            
 
        }
    }
}
