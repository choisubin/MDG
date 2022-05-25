using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameApplication : BaseApplication
{
    public InGameModel model;
    public InGameView view;
    public InGameController controller;

    public override void Init()
    {
        controller.Init(this, 0, 0);
    }

    public void Init(int stageNum, int subStage)
    {
        //Init();
    }

    public override void Set()
    {
        //gameObject.SetActive(true);
        controller.Set();
    }

    public override void AdvanceTime(float dt_sec)
    {
        controller.AdvanceTime(dt_sec);
    }

    public override void Dispose()
    {
        //gameObject.SetActive(false);
        controller.Dispose();
    }

}
