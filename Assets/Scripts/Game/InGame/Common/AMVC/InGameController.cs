using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : BaseController
{
    private InGameApplication _app = new InGameApplication();
    public override void Init()
    {
    }

    public void Init(InGameApplication app, int stageNum, int subStageNum)
    {
        //_app = app;
        //app.model.StageNum = stageNum;
        //app.model.SubStageNum = subStageNum;
        //app.model.MapKey = 0;

        //app.view.PuzzleMap.Init(app.model.MapDef);
    }

    public override void AdvanceTime(float dt_sec)
    {
        //_app.model.time += dt_sec;
        //_app.view.PuzzleMap.AdvanceTime(dt_sec);
    }

    public override void Set()
    {
    }

    public override void Dispose()
    {
    }
}
