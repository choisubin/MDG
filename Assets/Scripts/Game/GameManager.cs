using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, System.IDisposable
{
    // Start is called before the first frame update
    public InGameApplication inGameApplication;
    public LobbyApplication lobbyApplication;
    void Awake()
    {
        Screen.SetResolution(1080, 1920, true);
    }
    void Start()
    {
        NotificationCenter.Instance.AddObserver(OnNotification, ENotiMessage.ChangeSceneState);
        InitHandlers();
        ChangeState(EGameState.LOBBY);

    }

    // Update is called once per frame
    void Update()
    {
        if (_currentState != EGameState.UNKNOWN)
        {
            GetStateHandler(_currentState).AdvanceTime(Time.deltaTime);
        }
    }


    public void Dispose()
    {
        GetStateHandler(_currentState).Dispose();
        NotificationCenter.Instance.RemoveObserver(OnNotification, ENotiMessage.ChangeSceneState);
    }

    public void OnNotification(Notification noti)
    {
        EGameState state = (EGameState)noti.data[EDataParamKey.Integer];
        switch(state)
        {
            case EGameState.INGAME:
                int[] stageInfo = (int[])noti.data[EDataParamKey.IntegerArr];
                Debug.LogError(stageInfo[0]);
                Debug.LogError(stageInfo[1]);
                inGameApplication.SetStageNum(stageInfo[0], stageInfo[1], (int)noti.data[EDataParamKey.StageEnemySpawnKey]);
                ChangeState(state);
                break;
            default:
                ChangeState(state);
                break;
        }
    }

    #region State Handlers
    private Dictionary<EGameState, IGameBasicModule> _handlers = new Dictionary<EGameState, IGameBasicModule>();
    private EGameState _currentState = EGameState.UNKNOWN;

    private void InitHandlers()
    {
        _handlers.Clear();

        _handlers.Add(EGameState.LOADING, new LoadingApplication());
        _handlers.Add(EGameState.INGAME, inGameApplication);
        _handlers.Add(EGameState.LOBBY, lobbyApplication);
        foreach (EGameState state in _handlers.Keys)
        {
            _handlers[state].Init(this.gameObject);
            _handlers[state].SetActive(false);
        }
    }


    private void ChangeState(EGameState nextState)
    {
        if (nextState != EGameState.UNKNOWN && nextState != _currentState)
        {
            EGameState prevState = _currentState;
            _currentState = nextState;
            IGameBasicModule leaveHandler = GetStateHandler(prevState);
            if (leaveHandler != null)
            {
                leaveHandler.Dispose();
                leaveHandler.SetActive(false);
            }
            IGameBasicModule enterHandler = GetStateHandler(_currentState);
            if (enterHandler != null)
            {
                enterHandler.SetActive(true);
                enterHandler.Set();
            }
        }
    }

    private IGameBasicModule GetStateHandler(EGameState state)
    {
        if (_handlers.ContainsKey(state))
        {
            return _handlers[state];
        }
        return null;
    }
    #endregion
    public class LoadingApplication : IGameBasicModule
    {
        GameManager gm;
        public void Init(GameObject gm)
        {
            this.gm = gm.GetComponent<GameManager>();
        }

        public void Set()
        {
        }
        float _currentTime = 0;
        public void AdvanceTime(float dt_sec)
        {
            _currentTime += dt_sec;
            if (_currentTime > 2f)
            {
                gm.ChangeState(EGameState.LOBBY);
            }
        }

        public void Dispose()
        {
        }

        public void SetActive(bool flag)
        {
        }
    }
}
public interface IGameBasicModule
{
    void Init(GameObject gm);
    void Set();
    void AdvanceTime(float dt_sec);
    void Dispose();
    void SetActive(bool flag);
}

public enum EGameState
{
    UNKNOWN,
    LOADING,
    INGAME,
    LOBBY,
}

