using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : IngameElement
{
    public void Init()
    {
        //NotificationCenter.Instance.AddObserver(OnNotification, ENotiMessage.OnKillEnemy);
        //NotificationCenter.Instance.AddObserver(OnNotification, ENotiMessage.OnArriveEnemy);
    }

    public void Set()
    {
        NotificationCenter.Instance.AddObserver(OnNotification, ENotiMessage.OnKillEnemy);
        NotificationCenter.Instance.AddObserver(OnNotification, ENotiMessage.OnArriveEnemy);
        app.model.ResetAllModelValue();
        app.view.SetStageInfoText(app.model.StageNum, app.model.PartNum);
        app.view.InGameUI.SetUpgradeBtn();
        Debug.LogError("Set");
    }

    public void AdvanceTime(float dt_sec)
    {
        app.model.GameTime += dt_sec;
        app.view.UpdateText((int)app.model.GameTime, app.model.Money);
    }
    public void OnNotification(Notification noti)
    {
        switch(noti.msg)
        {
            case ENotiMessage.OnKillEnemy:
                app.model.Money += (int)noti.data[EDataParamKey.Integer];
                break;
            case ENotiMessage.OnArriveEnemy:
                app.model.PlayerHP -= (int)noti.data[EDataParamKey.Integer];
                break;
        }
    }
    public void Dispose()
    {
        NotificationCenter.Instance.RemoveObserver(OnNotification, ENotiMessage.OnKillEnemy);
        NotificationCenter.Instance.RemoveObserver(OnNotification, ENotiMessage.OnArriveEnemy);
    }
}
