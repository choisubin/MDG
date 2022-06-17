using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IngameElement : BaseElement
{
    public InGameApplication app
    {
        get
        {
            return GameObject.FindObjectOfType<InGameApplication>();
        }
    }
}
public class InGameApplication : MonoBehaviour, IGameBasicModule
{
    public InGameModel model;
    public InGameView view;
    //public InGameController controller;
    public InGameController inGameController;
    public DefenseMapController defenseMapController;
    public MatchBoardController matchBoardController;

    public void Init(GameObject gm)
    {
        //controller.Init(this, 0, 0);
        inGameController.Init();
        defenseMapController.Init();
        matchBoardController.Init();
    }

    public void Init(int stageNum, int subStage)
    {
        //Init();
    }

    public void Set()
    {
        //gameObject.SetActive(true);
        //controller.Set();

        inGameController.Set();
        defenseMapController.Set();
        matchBoardController.Set();
    }

    public void AdvanceTime(float dt_sec)
    {
        //controller.AdvanceTime(dt_sec);
        if(!model.IsAlive)
        {
            view.InGameUI.EnableStageResultPopup(false);
        }
        else if (model.IsAlive||!model.IsClear)
        {
            inGameController.AdvanceTime(dt_sec);
            defenseMapController.AdvanceTime(dt_sec);
            matchBoardController.AdvanceTime(dt_sec);
        }
    }

    public void Dispose()
    {
        //gameObject.SetActive(false);
        //controller.Dispose();

        StopAllCoroutines();
        inGameController.Dispose();
        defenseMapController.Dispose();
        matchBoardController.Dispose();
    }

    public void SetActive(bool flag)
    {
        gameObject.SetActive(flag);
    }

    public void SetStageNum(int stage,int part,int spawnKey)
    {
        model.SetStageInfo(stage, part, spawnKey);
    }
}
