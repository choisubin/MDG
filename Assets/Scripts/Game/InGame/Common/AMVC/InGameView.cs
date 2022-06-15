using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameView : IngameElement
{
    [SerializeField] private InGameUI _inGameUi;
    public InGameUI InGameUI
    {
        get
        {
            return _inGameUi;
        }
    }
    public void SetStageInfoText(int stage,int part)
    {
        _inGameUi.SetStageText(stage, part);
    }
    public void UpdateText(int time,int money)
    {
        _inGameUi.SetTimeText(time);
        _inGameUi.SetMoneyText(money);
    }
}
