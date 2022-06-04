using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : BaseElement
{
    private InGameApplication _app = new InGameApplication();
    public void Init()
    {
    }

    public void Init(InGameApplication app, int stageNum, int subStageNum)
    {
        _app = app;
        app.model.StageNum = stageNum;
        app.model.SubStageNum = subStageNum;
        app.model.MapKey = 0;

        app.view.PuzzleMap.Init(app.model.MapDef);
    }

    public void Set()
    {
    }

    public void AdvanceTime(float dt_sec)
    {
        _app.model.time += dt_sec;
        _app.view.PuzzleMap.AdvanceTime(dt_sec);
    }

    public void Dispose()
    {
    }

}
