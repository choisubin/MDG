using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : IngameElement
{
    public void Init()
    {
        NotificationCenter.Instance.AddObserver(OnNotification, ENotiMessage.OnKillEnemy);
    }

    public void Set()
    {
        app.view.SetStageInfoText(app.model.StageNum, app.model.PartNum);
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
        }
    }
    public void Dispose()
    {
    }
}
