using iDuel_EvolutionX.ADO;
using iDuel_EvolutionX.Service;
using iDuel_EvolutionX.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace iDuel_EvolutionX.Model
{


    class DecksManergerOperate
    {
        private static DecksManergerOperate MyDecks;
        private static MainWindow mainwindow;

        private Dictionary<string, string> codesForm;


        #region 卡组文件夹及对应的卡组

        private string firstdocument;       //第一个文件夹名字

        public string Firstdocument
        {
            get { return firstdocument; }
            private set { firstdocument = value; }
        }
        private List<Deck> first;

        public List<Deck> First
        {
            get { return first; }
            set { first = value; }
        }

        private string seconddocument;      //第二个文件夹名字

        public string Seconddocument
        {
            get { return seconddocument; }
            set { seconddocument = value; }
        }
        private List<Deck> second;

        public List<Deck> Second
        {
            get { return second; }
            set { second = value; }
        }
        private string thirddocument;      //第三个文件夹名字

        public string Thirddocument
        {
            get { return thirddocument; }
            set { thirddocument = value; }
        }
        private List<Deck> third;

        public List<Deck> Third
        {
            get { return third; }
            set { third = value; }
        }

        #endregion


        public DecksManergerOperate()
        {
            codesForm = new Dictionary<string, string>();
            first = new List<Deck>();
            second = new List<Deck>();
            third = new List<Deck>();
        }

        public static DecksManergerOperate getInstance(MainWindow mw)
        {
            if (MyDecks == null)
            {
                MyDecks = new DecksManergerOperate();
                mainwindow = mw;
                MyDecks.getCodes();
                MyDecks.set();

            }
            return MyDecks;
        }

        public static DecksManergerOperate getInstance()
        {
            if (MyDecks == null)
            {
                MyDecks = new DecksManergerOperate();
            }
            return MyDecks;
        }

        #region <-- 初始化卡组管理器 -->

        private void set()
        {
            Firstdocument = AppConfigOperate.getInstance().Deck_first;   //设定第一个文件夹的名字
            Seconddocument = AppConfigOperate.getInstance().Deck_second; //设定第二个文件夹的名字
            Thirddocument = AppConfigOperate.getInstance().Deck_third;   //设定第三个文件夹的名字
            getDocumentDecks(First, AppConfigOperate.getInstance().Deck_first_path);
            getDocumentDecks(Second, AppConfigOperate.getInstance().Deck_second_path);
            getDocumentDecks(Third, AppConfigOperate.getInstance().Deck_third_path);
        }

        #endregion

        #region <-- 读取并初始化文件夹中的卡组 -->


        private void getDocumentDecks(List<Deck> decksDocuementName, string path)
        {
            string appPath = System.IO.Directory.GetCurrentDirectory();
            DirectoryInfo di = new DirectoryInfo(path);
            di.Create();
            foreach (var file in di.GetFiles())
            {
                List<string> deck = new List<string>();
                deck = readDeckByfile(di.FullName + @"/" + file);
                Deck _deck = new Deck();
                if (checkDeck(deck, _deck))
                {

                    _deck.Name = file.Name;
                    decksDocuementName.Add(_deck);
                }
                else
                {
                    _deck = null;                  
                }
                            
            }
        }

        #endregion

        #region <-- 读入卡组文件文本信息 -->
        public static List<string> readDeckByfile(string deck_path)
        {
            List<string> Deck = new List<string>();

            string path = System.IO.Directory.GetCurrentDirectory();
            //FileStream mydeck = new FileStream(path + @"\Data\deck\" + deck_name + ".deck", FileMode.Open, FileAccess.Read);
            FileStream mydeck = new FileStream(deck_path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(mydeck, Encoding.Default);

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (!line.Trim().Equals("") && !line.Trim().Equals("$$$$"))
                {
                    Deck.Add(line.Trim());
                }
                
            }

            return Deck;
        }

        #endregion

        #region <-- 卡组读入检查并初始化 -->

        public static bool checkDeck(List<string> deck, Deck _deck)
        {


            if (deck[1].Equals("#main"))
            {
                #region core

                int num = 0;

                #region main

                for (int i = 2; i < deck.Count; i++)
                {
                    if (_deck.Main.Count > 60)
                    {
                        MessageBox.Show("主卡组不能超过60张！");
                        return false;
                    }


                    if (deck[i].Equals("#extra"))
                    {
                        num = i + 1;
                        break;
                    }

                    
                    if (deck[i].Length > 8 || deck[i].Length == 0)
                    {
                        MessageBox.Show("八位密码在1到8之间，第" + i + "：" + deck[i]);
                    }


                    deck[i] = MyDecks.add0(deck[i]);
                    if (deck[i] != null)
                    {
                        CardControl card = CardDataOperate.getCardWithInfoByCheatcode(deck[i]);
                        if (card == null)
                        {
                            string cardName;
                            if (MyDecks.codesForm.TryGetValue(deck[i], out cardName))
                            {
                                //card = CardDataOperate.getCardByName(cardName);

                                #region List.Find方法的使用范例

                                //Card temp2 = card_all.Find(cc =>
                                //{
                                //    return cc.name.Equals(cardName);
                                //});
                                //if (temp2 != null)
                                //{
                                //    temp = new Card(temp2);
                                //}

                                #endregion
                            }
                            else
                            {

                                LogHelper.Wrtie("卡组出错", "第" + i + "行[" + _deck.Path + "]");
                                return false;
                            }

                        }

                        if (card != null)
                        {
                            _deck.Main.Add(card);
                        }
                        else
                        {
                            LogHelper.Wrtie("卡组出错", "第" + i + "行[" + _deck.Path + "]");
                        }

                    }
                    else
                    {
                        LogHelper.Wrtie("卡组出错", "第" + i + "行[" + _deck.Path + "]");
                    }




                }

                if (_deck.Main.Count < 40)
                {
                    MessageBox.Show("主卡组不满40张！");
                    return false;
                }

                    
                #endregion

                #region extra

                for (int i = num; i < deck.Count; i++)
                {
                    if (_deck.Extra.Count > 15)
                    {
                        MessageBox.Show("额外不能超过15张！");
                        return false;
                    }

                    if (deck[i].Equals("!side"))
                    {
                        num = i + 1;
                        break;
                    }

                    

                    if (deck[i].Length > 8 || deck[i].Length == 0)
                    {
                        MessageBox.Show("八位密码在1到8之间，第" + i + "：" + deck[i]);
                    }



                    deck[i] = MyDecks.add0(deck[i]);
                    if (deck[i] != null)
                    {
                        CardControl card = CardDataOperate.getCardWithInfoByCheatcode(deck[i]);
                        if (card == null)
                        {
                            string cardName;
                            if (MyDecks.codesForm.TryGetValue(deck[i], out cardName))
                            {
                                //card = CardDataOperate.getCardByName(cardName);
                            }
                            else
                            {

                                LogHelper.Wrtie("卡组出错", "第" + i + "行[" + _deck.Path + "]");
                                return false;
                            }

                        }

                        if (card != null)
                        {
                            _deck.Extra.Add(card);
                        }
                        else
                        {
                            LogHelper.Wrtie("卡组出错", "第" + i + "行[" + _deck.Path + "]");
                        }

                    }
                    else
                    {
                        LogHelper.Wrtie("卡组出错", "第" + i + "行[" + _deck.Path + "]");
                    }
                }


                #endregion

                #region side

                for (int i = num; i < deck.Count; i++)
                {
                    if ( _deck.Side.Count > 15)
                    {
                        MessageBox.Show("副卡组不能超过15张！");
                        return false;
                    }

                    if (deck[i].Length > 8 || deck[i].Length == 0)
                    {
                        MessageBox.Show("八位密码在1到8之间，第" + i + "：" + deck[i]);
                    }

                    deck[i] = MyDecks.add0(deck[i]);
                    if (deck[i] != null)
                    {
                        CardControl card = CardDataOperate.getCardWithInfoByCheatcode(deck[i]);
                        if (card == null)
                        {
                            string cardName;
                            if (MyDecks.codesForm.TryGetValue(deck[i], out cardName))
                            {
                                //card = CardDataOperate.getCardByName(cardName);
                            }
                            else
                            {

                                LogHelper.Wrtie("卡组出错", "第" + i + "行[" + _deck.Path + "]");
                                return false;
                            }

                        }

                        if (card != null)
                        {
                            _deck.Side.Add(card);
                        }
                        else
                        {
                            LogHelper.Wrtie("卡组出错", "第" + i + "行[" + _deck.Path + "]");
                        }

                    }
                    else
                    {
                        LogHelper.Wrtie("卡组出错", "第" + i + "行[" + _deck.Path + "]");
                    }

                }

                

                #endregion

                #endregion
            }
            else
            {
                #region iduel

                List<string> main_ = new List<string>();      //主卡组
                List<string> extra_ = new List<string>();     //额外卡组
                List<string> side_ = new List<string>();      //side

                List<CardControl> templay = _deck.Main;

                for (int i = 0; i < deck.Count; i++)
                {
                    if (deck[i].Equals("===="))
                    {
                        templay = _deck.Extra;
                        i ++;
                    }
                    if(deck[i].Equals("####"))
                    {
                        templay = _deck.Side;
                        i++;
                    }
                    if (deck[i].Equals("===="))
                    {
                        templay = _deck.Extra;
                        i++;
                    }
                    if (deck[i].Equals("####"))
                    {
                        templay = _deck.Side;
                        i++;
                    }

                    string cardName = deck[i].Split('[')[1].Split(']')[0];

                    //CardControl card = CardDataOperate.getCardByName(cardName);
                    //if (card != null)
                    //{
                    //    templay.Add(card);
                    //}
                    //else
                    //{
                    //    Card emty = new Card();
                    //    templay.Add(emty);
                    //    LogHelper.Wrtie("卡组出错", "[" + deck[i] + "][" + _deck.Path + "]");
                        
                    //}
                    

                }

                if (_deck.Main.Count < 40 || _deck.Main.Count > 60)
                {
                    
                    
                }
                if (_deck.Extra.Count > 15)
                {
                    
                }
                if (_deck.Side.Count > 15)
                {
                                
                }


                #region

                //for (int i = 0; ;)
                //{
                //    try
                //    {
                //        if (Deck.Count < 3)
                //        {
                //            break;
                //        }

                        

                //        if (Deck[i].Equals("====") || Deck[i].Equals("####"))
                //        {
                            
                //            if (Deck[i + 1].Equals("====") || Deck[i + 1].Equals("####"))
                //            {
                //                i = i + 2;
                //            }
                //            else
                //            {
                //                i = i + 1;
                //            }
                            
                //        }
                        
                //        if (i == 0)
                //        {

                //            string cardName = Deck[i].Split('[')[1].Split(']')[0];

                //            Card temp = MyDecks.getCard(card_all,cardName);

                //            if (temp != null)
                //            {
                //                main.Add(temp);
                //                Deck.RemoveAt(i);
                //            }            
                //            else
                //            {
                                
                //            }
                //        }
                //        else
                //        {
                            

                //            if ((i == 1 && Deck[0].Equals("====")) || (i == 2 && Deck[1].Equals("====")))
                //            {
                //                string cardName = Deck[i].Split('[')[1].Split(']')[0];
                //                Card temp = MyDecks.getCard(card_all,cardName);

                //                if (temp != null)
                //                {
                //                    extra.Add(temp);
                //                    Deck.RemoveAt(i);
                //                }
                                
                //            }
                //            if ((i == 1 && Deck[0].Equals("####")) || (i == 2 && Deck[1].Equals("####")))
                //            {
                //                string cardName = Deck[i].Split('[')[1].Split(']')[0];
                //                Card temp = MyDecks.getCard(card_all, cardName);

                //                if (temp != null)
                //                {
                //                    side.Add(temp);
                //                    Deck.RemoveAt(i);
                //                }
                //            }
                //            if (i == 1 && Deck[1].Equals("===="))
                //            {

                //            }
                //            if (i == 1 && Deck[1].Equals("####"))
                //            {

                //            }
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        main.Clear();
                //        extra.Clear();
                //        side.Clear();
                //        ex.Data.Add("出错的数据", Deck[i]);                     
                //        throw ;
                //    }

                    
                    
                    
                //    //string cardName = Deck[i]
                //}

                /*

                #region 阶段1：验证卡片数量和格式

                #region main

                for (int i = 0; !Deck[0].Equals("===="); i++)
                {
                    if (!Deck[0].StartsWith("[") || !Deck[0].EndsWith("]"))
                    {
                        MessageBox.Show("格式错误，请检查主卡组第" + i + "行：" + Deck[i]);
                        return false;
                    }
                    if (i > 60)
                    {
                        MessageBox.Show("主卡组不能超过60张！");
                        return false;
                    }

                    main_.Add(Deck[0]);
                    Deck.RemoveAt(0);
                }

                if (main_.Count < 40)
                {
                    MessageBox.Show("主卡组不满40张！");
                    return false;
                }

                #endregion

                #region Extra

                for (int i = 0; !Deck[1].Equals("####"); i++)
                {
                    if (!Deck[1].StartsWith("[") || !Deck[1].EndsWith("]"))
                    {
                        MessageBox.Show("格式错误，请检查额外第" + (i + 1) + "行：" + Deck[i + 1]);
                        return false;
                    }
                    if (i > 15)
                    {
                        MessageBox.Show("额外不能超过15张！");
                        return false;
                    }

                    extra_.Add(Deck[1]);
                    Deck.RemoveAt(1);
                }

                #endregion

                #region side

                int n = Deck.Count - 2;

                for (int i = 0; i < n; i++)
                {
                    if (!Deck[2].StartsWith("[") || !Deck[2].EndsWith("]"))
                    {
                        MessageBox.Show("格式错误，请检查side第" + (i + 2) + "行：" + Deck[i + 2]);
                        return false;
                    }
                    if (i > 15)
                    {
                        MessageBox.Show("side不能超过15张！");
                        return false;
                    }

                    side_.Add(Deck[2]);
                    Deck.RemoveAt(2);
                }

                #endregion

                #endregion

                #region 阶段2：校验禁限卡表



                #endregion

                #region 阶段3：载入卡组

                #region main

                foreach (string c in main_)
                {
                    //Console.WriteLine(c);
                    Card temp = new Card(
                        card_all.Find(cc =>
                        {
                            //cc为card_all中的对象，此处是lamda表达式写的委托
                            return cc.cardName.Equals(c.TrimStart('[').TrimEnd(']').Trim('#'));
                        }));
                    if (temp != null)
                    {
                        main.Add(temp);
                    }
                    else
                    {
                        MessageBox.Show("卡片名称错误：" + c);
                    }

                }

                #endregion

                #region Extra

                foreach (string c in extra_)
                {
                    //Console.WriteLine(c);
                    Card temp = new Card(
                        card_all.Find(cc =>
                        {
                            //cc为card_all中的对象，此处是lamda表达式写的委托
                            return cc.cardName.Equals(c.TrimStart('[').TrimEnd(']').Trim('#'));
                        }));
                    if (temp != null)
                    {
                        extra.Add(temp);
                    }
                    else
                    {
                        MessageBox.Show("卡片名称错误：" + c);
                        return false;
                    }

                }

                #endregion

                #region side

                foreach (string c in side_)
                {
                    //Console.WriteLine(c);
                    Card temp = new Card(
                        card_all.Find(cc =>
                        {
                            //cc为card_all中的对象，此处是lamda表达式写的委托
                            return cc.cardName.Equals(c.TrimStart('[').TrimEnd(']').Trim('#'));
                        }));
                    if (temp != null)
                    {
                        side.Add(temp);
                    }
                    else
                    {
                        MessageBox.Show("卡片名称错误：" + c);
                        return false;
                    }

                }

                #endregion

                #endregion

                */

                #endregion

                #endregion
            }


            return true;
        }

        #endregion

        #region <-- 获取Core卡组的密码对照表 -->

        private void getCodes()
        {
            XmlDocument Codes = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;//忽略文档里面的注释
            string configUrl = System.IO.Directory.GetCurrentDirectory() + @"\Data\SetCode.xml";
            if (System.IO.File.Exists(configUrl))
            {
                XmlReader reader = XmlReader.Create(configUrl, settings);
                Codes.Load(reader);
                XmlNode xn = Codes.SelectSingleNode("CodesForm");
                XmlNodeList xnl = xn.ChildNodes;
                foreach (XmlNode xn0 in xnl)
                {
                    XmlNodeList xnl0 = xn0.ChildNodes;
                    codesForm.Add(xnl0.Item(0).InnerText, xnl0.Item(1).InnerText);
                }
                reader.Close();
            }
        }

        #endregion

        #region <-- Core卡组读入时的补0操作 -->

        private string add0(string cheatcode)
        {
            int temp;
            if (int.TryParse(cheatcode, out temp))
            {
                if (cheatcode.Length < 8)
                {
                    switch (8 - cheatcode.Length)
                    {
                        case 1:
                            return cheatcode.Insert(0, "0");
                        case 2:
                            return cheatcode.Insert(0, "00");
                        case 3:
                            return cheatcode.Insert(0, "000");
                        case 4:
                            return cheatcode.Insert(0, "0000");
                        case 5:
                            return cheatcode.Insert(0, "00000");
                        case 6:
                            return cheatcode.Insert(0, "000000");
                        case 7:
                            return cheatcode.Insert(0, "0000000");
                        default:
                            MessageBox.Show("不明原因出错!");
                            break;
                    }
                    
                }
                return cheatcode;
            }
            return null;
        }

        #endregion

    }

}
