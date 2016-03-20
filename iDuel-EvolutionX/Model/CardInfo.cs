using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iDuel_EvolutionX.Model
{
    public class CardInfo
    {
        public string id { get; set; }              //卡片id
        public string name { get; set; }            //卡片名字
        public string cardCamp { get; set; }        //OCG、TCG
        public string sCardType { get; set; }       //卡片类型
        public string CardDType { get; set; }       //副卡类型
        public string tribe { get; set; }           //种族
        public string element { get; set; }         //属性
        public string level { get; set; }           //星数
        public string atk { get; set; }             //攻击力
        public string def { get; set; }             //防守力
        public string effect { get; set; }          //卡片描述
        public string cheatcode { get; set; }       //八位密码
        public string adjust { get; set; }          //卡片调整
        public string oldName { get; set; }         //曾用名

    }
}
