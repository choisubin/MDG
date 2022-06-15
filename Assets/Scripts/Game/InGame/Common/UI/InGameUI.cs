using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InGameUI : MonoBehaviour
{
    public TextMeshProUGUI _txtTime;
    public TextMeshProUGUI _txtMoney;
    public TextMeshProUGUI _txtStage;
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
}
