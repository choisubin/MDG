using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : LobbyElement
{
    public void Init()
    {
    }
    public void Set()
    {
        app.view.LobbyUI.Set();
    }
    public void AdvanceTime(float dt_sec)
    {
        app.view.LobbyUI.AdvaceTime(dt_sec);
    }

    public void Dispose()
    {
        app.view.LobbyUI.Dispose();
    }
}
