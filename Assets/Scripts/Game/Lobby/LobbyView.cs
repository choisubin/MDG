using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyView : LobbyElement
{
    [SerializeField]private LobbyUI _lobbyUi;
    public LobbyUI LobbyUI
    {
        get
        {
            return _lobbyUi;
        }
    }

}
