using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iDuel_EvolutionX.Model
{
    public enum Position
    {
        //己方
        MONSTER_1,MONSTER_2,MONSTER_3,MONSTER_4, MONSTER_5,//怪物区
        MAGICTRAP_1, MAGICTRAP_2, MAGICTRAP_3, MAGICTRAP_4, MAGICTRAP_5,//魔陷区
        PENDULUM_LEFT, PENDULUM_RIGHT,//灵摆区
        HAND,//手牌
        DECK,//卡组
        GRAVEYARD,//墓地
        EXCLUTION,//除外
        EXTRA,//额外

        //敌方
        MONSTER_1_OPP, MONSTER_2_OPP, MONSTER_3_OPP, MONSTER_4_OPP, MONSTER_5_OPP,//怪物区
        MAGICTRAP_1_OPP, MAGICTRAP_2_OPP, MAGICTRAP_3_OPP, MAGICTRAP_4_OPP, MAGICTRAP_5_OPP,//魔陷区
        PENDULUM_LEFT_OPP, PENDULUM_RIGHT_OPP,//灵摆区
        HAND_OPP,//手牌
        DECK_OPP,//卡组
        GRAVEYARD_OPP,//墓地
        EXCLUTION_OPP,//除外
        EXTRA_OPP//额外


    }
}
