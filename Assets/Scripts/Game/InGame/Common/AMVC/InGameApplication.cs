using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameApplication : BaseApplication
{
    public InGameModel model;
    public InGameView view;
    //public InGameController controller;
    public BaseController[] controllers;

    public override void Init()
    {
        //controller.Init(this, 0, 0);

        foreach(var c in controllers)
        {
            c.Init();
        }
    }

    public void Init(int stageNum, int subStage)
    {
        //Init();
    }

    public override void Set()
    {
        //gameObject.SetActive(true);
        //controller.Set();

        foreach (var c in controllers)
        {
            c.Set();
        }
    }

    public override void AdvanceTime(float dt_sec)
    {
        //controller.AdvanceTime(dt_sec);

        foreach (var c in controllers)
        {
            c.AdvanceTime(dt_sec);
        }
    }

    public override void Dispose()
    {
        //gameObject.SetActive(false);
        //controller.Dispose();

        foreach (var c in controllers)
        {
            c.Dispose();
        }
    }

}
