using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iDuel_EvolutionX.Model;
using iDuel_EvolutionX.UI;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using iDuel_EvolutionX.Tools;
using NBX3.Service;
using System.Windows;

namespace iDuel_EvolutionX.ADO
{
    class CardDataOperate
    {
        private static DataTable allCards;

        

        /// <summary>
        /// 读取所有卡片
        /// </summary>
        /// <returns></returns>
        public static bool GetAllCard()
        {
            //List<Card> allcards = new List<Card>();
            DataTable dt = SqliteHelper.ExecuteDataTable(@"select * from YGODATA");
            allCards = dt;
            if (allCards != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 通过八位密码获取一张卡片
        /// </summary>
        /// <param name="cheatcode">八位密码</param>
        /// <returns>卡片</returns>
        public static CardUI getCardWithInfoByCheatcode(string cheatcode)
        {
            CardInfo info = getCardInfoByCheatcode(cheatcode);
            if (info == null)
            {
                MessageBox.Show("初始化获取卡片信息失败");
                return null;
            }
            CardUI card = new CardUI(DuelOperate.getInstance().myself.cardback);
            card.initCardInfo(info);
            //card.info = info;
            return card;
        }

        /// <summary>
        /// 通过八位密码获取一组卡片
        /// </summary>
        /// <param name="card_codes">八位密码</param>
        /// <returns>卡堆</returns>
        public static List<CardUI> getCardsWithInfoByCheatcode(List<string> card_codes)
        {
            List<CardUI> cards = new List<CardUI>();
            for (int i = 0; i < card_codes.Count; i++)
            {
                CardUI card = getCardWithInfoByCheatcode(card_codes[i]);
                if (card != null)
                {
                    
                    cards.Add(card);
                }
                else
                {
                    LogHelper.Wrtie("卡片不存在？：", card_codes[i]);
                }
                
            }
            return cards;
        }


        /// <summary>
        /// 通过八位密码获取一张卡片信息
        /// </summary>
        /// <param name="cheatcode">八位密码</param>
        /// <returns>卡片信息</returns>
        public static CardInfo getCardInfoByCheatcode(string cheatcode)
        {
            if (allCards != null)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                DataRow[] dr = allCards.Select("cheatcode='" + cheatcode + "'"); // 查询语句 "  列名 = '变量'  "
                watch.Stop();
                Console.WriteLine(watch.ElapsedMilliseconds);
                if (dr.Length == 1)
                {
                    
                    CardInfo info = new CardInfo();
                    info.id = dr[0]["id"].ToString();
                    info.cardCamp = (string)dr[0]["CardCamp"];
                    info.name = (string)dr[0]["name"];
                    info.sCardType = (string)dr[0]["sCardType"];
                    info.CardDType = (string)SqliteHelper.FromDbValue(dr[0]["CardDType"]);
                    info.tribe = (string)SqliteHelper.FromDbValue(dr[0]["tribe"]);
                    info.element = (string)SqliteHelper.FromDbValue(dr[0]["element"]);
                    info.level = Convert.ToString(SqliteHelper.FromDbValue(dr[0]["level"]));
                    info.atk = (string)SqliteHelper.FromDbValue(dr[0]["atk"]);
                    info.def = (string)SqliteHelper.FromDbValue(dr[0]["def"]);
                    info.effect = (string)dr[0]["effect"];
                    info.cheatcode = ((string)dr[0]["cheatcode"]).TrimEnd();
                    info.adjust = (string)SqliteHelper.FromDbValue(dr[0]["adjust"]);
                    info.oldName = (string)SqliteHelper.FromDbValue(dr[0]["oldName"]);

                    //Card card = new Card();
                    //card.Uid = dr[0]["id"].ToString();
                    //card.cardCamp = (string)dr[0]["CardCamp"];
                    //card.name = (string)dr[0]["name"];
                    //card.sCardType = (string)dr[0]["sCardType"];
                    //card.CardDType = (string)SqliteHelper.FromDbValue(dr[0]["CardDType"]);
                    //card.tribe = (string)SqliteHelper.FromDbValue(dr[0]["tribe"]);
                    //card.element = (string)SqliteHelper.FromDbValue(dr[0]["element"]);
                    //card.level = Convert.ToString(SqliteHelper.FromDbValue(dr[0]["level"]));
                    //card.atk = (string)SqliteHelper.FromDbValue(dr[0]["atk"]);
                    //card.def = (string)SqliteHelper.FromDbValue(dr[0]["def"]);
                    //card.effect = (string)dr[0]["effect"];
                    //card.cheatcode = ((string)dr[0]["cheatcode"]).TrimEnd();
                    //card.adjust = (string)SqliteHelper.FromDbValue(dr[0]["adjust"]);
                    //card.oldName = (string)SqliteHelper.FromDbValue(dr[0]["oldName"]);
                    return info;
                }
                else
                {


                    LogHelper.Wrtie("卡片不存在？：", cheatcode);
                    return null;
                }


            }

            return null;
 
        }

        public static Card getCardByName(string name)
        {
            if (allCards != null)
            {
                //Stopwatch watch = new Stopwatch();
                //watch.Start();watch.Stop();Console.WriteLine(watch.ElapsedMilliseconds);
                DataRow[] dr;
                dr = allCards.Select("name='" + name + "'"); // 查询语句 "  列名 = '变量'  "
                if (dr.Length < 1)
                {
                    for (int i = 0; i < allCards.Rows.Count; i++)
                    {
                        string oldname = (string)SqliteHelper.FromDbValue(allCards.Rows[i]["oldName"]);
                        if (oldname.Trim().Equals("")) continue;
                        string[] oldnames = oldname.Split('，', ',');
                        for (int j = 0; j < oldname.Length; j++)
                        {
                            if (oldname[j].Equals(name))
                            {
                                //DataRow dr0 = allCards.Rows[i];
                                dr = new DataRow[] { allCards.Rows[i] };
                                //allCards.Rows[i];
                            }

                        }
                    }
                }
                //dr = allCards.Select("oldName like '%" + name + "%'");

                #region linq查询

                //watch.Start();
                //List<DataRow> a = (from d in allCards.AsEnumerable()
                //              where d["cheatcode"].ToString().TrimEnd() == cheatcode
                //              select d).ToList<DataRow>();
                //watch.Stop();
                //Console.WriteLine(watch.ElapsedMilliseconds);

                #endregion

                
                if (dr.Length == 1)
                {
                    Card card = new Card();
                    card.Uid = dr[0]["id"].ToString();
                    card.cardCamp = (string)dr[0]["CardCamp"];
                    card.name = (string)dr[0]["name"];
                    card.sCardType = (string)dr[0]["sCardType"];
                    card.CardDType = (string)SqliteHelper.FromDbValue(dr[0]["CardDType"]);
                    card.tribe = (string)SqliteHelper.FromDbValue(dr[0]["tribe"]);
                    card.element = (string)SqliteHelper.FromDbValue(dr[0]["element"]);
                    card.level = Convert.ToString(SqliteHelper.FromDbValue(dr[0]["level"]));
                    card.atk = (string)SqliteHelper.FromDbValue(dr[0]["atk"]);
                    card.def = (string)SqliteHelper.FromDbValue(dr[0]["def"]);
                    card.effect = (string)dr[0]["effect"];
                    card.cheatcode = ((string)dr[0]["cheatcode"]).TrimEnd();
                    card.adjust = (string)SqliteHelper.FromDbValue(dr[0]["adjust"]);
                    card.oldName = (string)SqliteHelper.FromDbValue(dr[0]["oldName"]);
                    return card;
                }
                else
                {
                    LogHelper.Wrtie("卡片不存在？：", name);
                    return null;
                }
            }

            return null;

        }

        



    }
}
