using UnityEngine;
using System.Collections;

public class MatchBoardController : IngameElement
{
    bool m_bInit;
    Stage m_Stage;
    InputManager m_InputManager;
    ActionManager m_ActionManager;

    //Event Members
    bool m_bTouchDown;          //입력상태 처리 플래그, 유효한 블럭을 클릭한 경우 true
    BlockPos m_BlockDownPos;    //블럭 인덱스 (보드에 저장된 위치)
    Vector3 m_ClickPos;         //DOWN 위치(보드 기준 Local 좌표)

    [SerializeField] Transform m_Container;
    [SerializeField] Camera m_BoardCamera;

    public void Init()
    {
        //InitStage();
    }

    public void Set()
    {
        int[] level = { 0, 0, 0, 0, 0 };
        app.model.SlotLevel = level;
        InitStage();
    }

    public void AdvanceTime(float dt_sec)
    {
        if (!m_bInit)
            return;

        OnInputHandler();
    }

    public void Dispose()
    {
    }

    private void InitStage()
    {
        if (m_bInit)
            return;

        m_bInit = true;
        m_InputManager = new InputManager(m_BoardCamera, m_Container);

        BuildStage(2);

        //m_Stage.PrintAll();
    }

    /*
     * 스테이지를 구성한다.
     * Stage 객체를 할당받고, Stage 구성을 요청한다.
     */
    private void BuildStage(int stageKey)
    {
        //1. Stage를 구성한다.
        m_Stage = StageBuilder.BuildStage(stageKey);
        m_ActionManager = new ActionManager(m_Container, m_Stage);

        //2. 생성한 stage 정보를 이용하여 씬을 구성한.
        m_Stage.ComposeStage("Cell", "Block", m_Container);
    }

    private void OnInputHandler()
    {
        //1. Touch Down 
        if (!m_bTouchDown && m_InputManager.isTouchDown)
        {
            //1.1 보드 기준 Local 좌표를 구한다.
            Vector2 point = m_InputManager.touch2BoardPosition;

            //1.2 Play 영역(보드)에서 클릭하지 않는 경우는 무시
            if (!m_Stage.IsInsideBoard(point))
                return;

            //1.3 클릭한 위치이 블럭을 구한다.
            BlockPos blockPos;
            if (m_Stage.IsOnValideBlock(point, out blockPos))
            {
                //1.3.1 유효한(스와이프 가능한) 블럭에서 클릭한 경우
                m_bTouchDown = true;        //클릭 상태 플래그 ON
                m_BlockDownPos = blockPos;  //클릭한 블럭의 위치(row, col) 저장
                m_ClickPos = point;         //클릭한 Local 좌표 저장
                                            //Debug.Log($"Mouse Down In Board : (blockPos})");
            }
        }
        //2. Touch UP : 유효한 블럭 위에서 Down 후에만 UP 이벤트 처리
        else if (m_bTouchDown && m_InputManager.isTouchUp)
        {
            //2.1 보드 기준 Local 좌표를 구한다.
            Vector2 point = m_InputManager.touch2BoardPosition;

            //2.2 스와이프 방향을 구한다.
            Swipe swipeDir = m_InputManager.EvalSwipeDir(m_ClickPos, point);

            //Debug.Log($"Swipe : {swipeDir} , Block = {m_BlockDownPos}");

            if (swipeDir != Swipe.NA)
                m_ActionManager.DoSwipeAction(m_BlockDownPos.row, m_BlockDownPos.col, swipeDir);

            m_bTouchDown = false;   //클릭 상태 플래그 OFF
        }
    }
}