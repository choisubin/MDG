using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{

}

public enum EMapTile
{
    STAGE1,
    STAGE2,
}

public enum EUnitType
{
    TileMapUnit,
    EnemyUnit,
}

public enum EUnitTargetingType
{
    Front,          //가장 앞 부터
    HighHP,         //체력 높은 애 
    LowHp,          //체력 낮은 애 
    Random,         //랜덤
}

public enum EUnitAttackType
{
    SingleAtk,      //단일 공격
    MultipleAtk,    //다중 공격
}

public enum EUnitAttackEffect
{
    None,           //없음    
    Burn,           //화상
    Corroded,       //부식
    Frostbite       //동상
}