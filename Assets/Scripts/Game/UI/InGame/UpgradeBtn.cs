using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeBtn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtLevel;
    [SerializeField] private TextMeshProUGUI _needUpgradeCoin;
    [SerializeField] private GameObject _goUpgradeCoin;
    [SerializeField] private Image _unitImage;

    public int needCoin;
    public void Set(UnitWrapperDefinition unit,int currentLevel)
    {
        var unitDef = DefinitionManager.Instance.GetData<List<InGameUpgradeUnitDefinition>>(unit.key);

        needCoin = unitDef[currentLevel].nextUpgradeCoin;
        if (needCoin == 0)
        {
            _goUpgradeCoin.SetActive(false);
        }
        else
        {
            _goUpgradeCoin.SetActive(true);
            _needUpgradeCoin.text = needCoin.ToString();
        }

        int level = unitDef[currentLevel].Level;
        _txtLevel.text = string.Format("Lv. {0}", level);

        _unitImage.sprite = Resources.Load<Sprite>("Sprites/Unit/" + unit.UnitImageStr);
    }
}
