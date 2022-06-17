using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InGameUI : IngameElement
{
    public TextMeshProUGUI _txtTime;
    public TextMeshProUGUI _txtMoney;
    public TextMeshProUGUI _txtStage;
    public List<UpgradeBtn> _upgradeBtns;
    public GameObject _goStageResultPopup;
    public TextMeshProUGUI _txtStageResult;
    public void SetUpgradeBtn()
    {
        int count = 0;
        var datas = FirebaseManager.Instance.CurrentEquipUnit;
        foreach(var btn in _upgradeBtns)
        {
            UnitWrapperDefinition def = DefinitionManager.Instance.GetData<UnitWrapperDefinition>(datas[count].key);
            btn.Set(def, app.model.SlotLevel[count]);
            count++;
        }
    }
    public void SetTimeText(int time)
    {
        _txtTime.text = string.Format("{0:00}:{1:00}", time / 60, time % 60);
    }

    public void SetMoneyText(int money)
    {
        _txtMoney.text = string.Format("{0}", money);
    }

    public void SetStageText(int stagenum,int partnum)
    {
        _txtStage.text = string.Format("stage {0:00}-{1:00}", stagenum,partnum);
    }

    public void EnableStageResultPopup(bool isClear)
    {
        _txtStageResult.text = isClear ?  string.Format("<color=yellow>Stage\nClear!</color>"): string.Format("<color=red>Stage\nFail..</color>");
        _goStageResultPopup.SetActive(true);
    }

    public void OnClick_Upgrade(int slot)
    {
        if(_upgradeBtns[slot].needCoin!=0)
        {
            if(_upgradeBtns[slot].needCoin<= app.model.Money)
            {
                app.model.Money -= _upgradeBtns[slot].needCoin;
                app.model.SlotLevel[slot]++;
                SetUpgradeBtn();
            }
        }
    }

    public void OnClickHomeBtn()
    {
        _goStageResultPopup.SetActive(false);
        Hashtable sendData = new Hashtable();
        sendData.Add(EDataParamKey.Integer, EGameState.LOBBY);
        NotificationCenter.Instance.PostNotification(ENotiMessage.ChangeSceneState, sendData);
    }
}
