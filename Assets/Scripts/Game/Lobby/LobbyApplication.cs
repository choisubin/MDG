using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LobbyElement : BaseElement
{
    public LobbyApplication app
    {
        get
        {
            return GameObject.FindObjectOfType<LobbyApplication>();
        }
    }
}
public class LobbyApplication : MonoBehaviour, IGameBasicModule
{
    public LobbyModel model;
    public LobbyView view;
    public LobbyController controller;
    public void Init(GameObject gm)
    {
        controller.Init();
    }

    public void Set()
    {
        //gameObject.SetActive(true);
        controller.Set();
    }
    public void AdvanceTime(float dt_sec)
    {
        controller.AdvanceTime(dt_sec);
    }

    public void Dispose()
    {
        //gameObject.SetActive(false);
        controller.Dispose();
    }

    public void SetActive(bool flag)
    {
        gameObject.SetActive(flag);
    }
}
