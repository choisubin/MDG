using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UnitPopupController : MonoBehaviour
{
    [SerializeField] UnitUiItem _unit;
    [SerializeField] TextMeshProUGUI _txtUnitInfo;
    [SerializeField] TextMeshProUGUI _txtEquipBtn;
    [SerializeField] Button _equipBtn;



    private UnitWrapperDefinition _unitDef;
    private FirebaseManager.UserUnitData _userUnitData;
    private bool _isEquip = false;
    public void Set(int unitKey)
    {
        _unit.Set(unitKey);

        _unitDef = DefinitionManager.Instance.GetData<UnitWrapperDefinition>(unitKey);
        _userUnitData = FirebaseManager.Instance.userUnitDataDic[unitKey];

        _isEquip = false;
        foreach (var equip in FirebaseManager.Instance.CurrentEquipUnit)
        {
            if (unitKey == equip.key) _isEquip = true;
        }

        _equipBtn.interactable = !_isEquip;
        if (_unitDef != null)
        {
            _txtUnitInfo.text = string.Format
                ("공격력 : {0}\n공격속도 : {1}\n타겟팅 : <color=red>{2}</color>\n공격타입 : <color=red>{3}</color>\n",
                _unitDef.BaseAtk,
                _unitDef.BaseAttackSpeed,
                GetTargetStr(_unitDef.EUnitTargetingType),
                GetAttackTypeStr(_unitDef.EUnitAttackType)
                );
        }
        this.gameObject.SetActive(true);
    }


    private string GetTargetStr(EUnitTargetingType type)
    {
        switch(type)
        {
            case EUnitTargetingType.Front:
                return "가장 가까운 적";
            case EUnitTargetingType.HighHP:
                return "가장 체력이 높은 적";
            case EUnitTargetingType.LowHp:
                return "가장 체력이 낮은 적";
            case EUnitTargetingType.Random:
                return "랜덤";
            default:
                return "";
        }
    }
    private string GetAttackTypeStr(EUnitAttackType type)
    {
        switch (type)
        {
            case EUnitAttackType.MultipleAtk:
                return "다중 공격";
            case EUnitAttackType.SingleAtk:
                return "단일 공격";
            default:
                return "";
        }
    }
}
