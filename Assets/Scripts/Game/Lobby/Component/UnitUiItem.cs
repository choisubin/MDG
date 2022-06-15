using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UnitUiItem : MonoBehaviour
{
    [SerializeField] private Image _unitImg;
    [SerializeField] private TextMeshProUGUI _levelText;

    private UnitWrapperDefinition _unitDef;
    private FirebaseManager.UserUnitData _userUnitData;
    public void Set(int unitkey)
    {
        _unitDef = DefinitionManager.Instance.GetData<UnitWrapperDefinition>(unitkey);
        _userUnitData = FirebaseManager.Instance.userUnitDataDic[unitkey];
        if(_unitDef!=null)
        {
            _unitImg.sprite = Resources.Load<Sprite>("Sprites/Unit/" + _unitDef.UnitImageStr);
            _levelText.text = string.Format("lv. {0}", _userUnitData.level);
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
