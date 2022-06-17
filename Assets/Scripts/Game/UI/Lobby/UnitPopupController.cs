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

    [SerializeField] private UnitUiLine _equipSlotLines;
    [SerializeField] private GameObject _goSlotLines;


    private UnitWrapperDefinition _unitDef;
    private FirebaseManager.UserUnitData _userUnitData;
    private int _unitKey;
    bool _isEquip = false;
    public void Set(int unitKey)
    {
        this.gameObject.SetActive(true);
        _unit.Set(unitKey);
        _unitKey = unitKey;
        _unitDef = DefinitionManager.Instance.GetData<UnitWrapperDefinition>(unitKey);

        UpdateUI();
    }

    public void UpdateUI()
    {
        _goSlotLines.SetActive(false);
        _userUnitData = FirebaseManager.Instance.userUnitDataDic[_unitKey];

        bool _isEquip = false;
        foreach (var equip in FirebaseManager.Instance.CurrentEquipUnit)
        {
            if (_unitKey == equip.key)
            {
                _isEquip = true;
            }
        }
        if (_unitDef != null)
        {
            _txtUnitInfo.text = string.Format
                ("공격력 : {0}\n공격속도 : {1}\n타겟팅 : <color=black>{2}</color>\n공격타입 : <color=black>{3}</color>\n공격횟수 : {4}",
                _unitDef.BaseAtk,
                _unitDef.BaseAttackSpeed,
                GetTargetStr(_unitDef.EUnitTargetingType),
                GetAttackTypeStr(_unitDef.EUnitAttackType),
                _unitDef.UnitAttackNum
                );
        }
        _txtEquipBtn.text = _isEquip ? "장착중" : "장착하기";
        _equipBtn.interactable = !_isEquip;

    }

    public void OnClickEquip()
    {
        int[] equipUnitKeys = new int[5];
        foreach (var dataDic in FirebaseManager.Instance.userUnitDataDic)
        {
            int slot = dataDic.Value.equipSlot;
            if (slot > 0 && slot < 6)
            {
                equipUnitKeys[slot - 1] = dataDic.Key;
            }
        }
        _equipSlotLines.Set(equipUnitKeys);
        _goSlotLines.SetActive(true);
    }

    public void OnClickEquipSlot(int slotNum)
    {
        FirebaseManager.Instance.ChangeEquipUnit(_unitKey, slotNum, () =>
        {
            NotificationCenter.Instance.PostNotification(ENotiMessage.OnFireBaseDataUpdate);
        });
    }

    private string GetTargetStr(EUnitTargetingType type)
    {
        switch(type)
        {
            case EUnitTargetingType.Front:
                return "가장 가까운 적";
            case EUnitTargetingType.HighHP:
                return "HighHP";
            case EUnitTargetingType.LowHp:
                return "LowHp";
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
