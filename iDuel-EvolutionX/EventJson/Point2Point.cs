using iDuel_EvolutionX.Model;
using iDuel_EvolutionX.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace iDuel_EvolutionX.EventJson
{
    public enum ActionCommand
    {
        GAME_SET_DUELST_INFO,  //设置决斗者信息
        GAME_START,       //开局
        GAME_RPS,         //剪刀石头布 Rock-Paper-Scissors
        GAME_ORDER,       //先手或者后手
        GAME_SET_PHASE,   //阶段宣言
        GAME_DRAW,        //抽牌
        GAME_LIFE_CHANGE, //生命值变动

        CARD_ACTIVE,       //效果发动
        CARR_SELECT_AIM,   //选择对象
        CARD_MOVE,         //卡片移动
        CARD_DISAPPEAR,    //卡片消失
        CARD_SIGN_ACTION,  //指示物改变
        CARD_ATK,          //攻击
        CARD_STATUS_CHANGE,//卡片状态改变
        CARD_REMARK,       //修改卡片备注


    }

    public class BaseJson
    {
        public Guid guid;    //玩家GUID
        public string cid;     //开局后的卡片ID
        public int msgIndex;
        public ActionCommand action;  //
        public string json;
    }

    public class DuelistInfo
    {
        public int did;              //决斗者ID
        public string name;          //决斗者名称
        public bool hasHead;     //头像
        public bool hasCardBack; //卡背

        
    }

    public class DeckInfo
    {
        public List<string> main = new List<string>();
        public List<string> extra = new List<string>();
    }

    public class OrderInfo
    {
        public bool isFirst;
        public List<int> cardsIDs = new List<int>();

    }

    public class DrawInfo
    {
        public bool isBack = true;
        public int cardID;
    }

    public class PHASE
    {

    }

    public class MoveInfo
    {
        public int cardID;
        public bool isAdd;
        public Area aimArea;
        public Status aimStatus;
    }


}
