using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : InGameElement
{
    public void Init()
    {
    }

    public void Init(int stageNum,int subStageNum)
    {
        app.model.StageNum = stageNum;
        app.model.SubStageNum = subStageNum;
        app.model.MapKey = 0;
        app.view.TileMap.Init(app.model.MapDef); //나중에 key값으로 대체해야함
    }

    public void Set()
    {
    }

    public void AdvanceTime(float dt_sec)
    {
        app.model.time += dt_sec;
    }

    public void Dispose()
    {
    }

}
