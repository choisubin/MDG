using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMapComponent : MonoBehaviour
{
    [SerializeField]
    private List<UnitBase> _mapUnits = new List<UnitBase>();
    [SerializeField]
    private TileMap _tileMap;
    [SerializeField]
    private Transform _trMapUnits;

    InputManager m_InputManager;
    ActionManager m_ActionManager;

    //private UnitBase _curTouchUnit;
    //private UnitBase _curCollisionUnit;
    //private Vector2 _touchDownPos;


    //Event Members
    bool m_bTouchDown;          //입력상태 처리 플래그, 유효한 블럭을 클릭한 경우 true
    TilePos m_TileDownPos;    //블럭 인덱스 (보드에 저장된 위치)
    Vector3 m_ClickPos;         //DOWN 위치(보드 기준 Local 좌표)

    /*
    public void Init(StageDetailMapDefinition def)
    {
        if (def != null)
        {
            m_InputManager = new InputManager(transform);
            InitTileMap(def);
            InitPuzzleUnits();
        }
    }

    public void AdvanceTime(float dt_sec)
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    RaycastHit2D hit = Physics2D.Raycast(mousePos, Camera.main.transform.forward);

        //    if (hit.collider != null)
        //    {
        //        UnitBase unit;
        //        if (hit.collider.gameObject.TryGetComponent<UnitBase>(out unit))
        //        {
        //            _curTouchUnit = unit;
        //            //unit.SetPosition(hit.point);
        //            _touchDownPos = hit.point;
        //        }
        //    }
        //}
        //else if(Input.GetMouseButtonUp(0))
        //{
        //    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    RaycastHit2D hit = Physics2D.Raycast(mousePos, Camera.main.transform.forward);

        //    Vector2 vec = hit.point - _touchDownPos;
        //    Debug.LogError("차이: " + vec);
        //}
    }
    private void InitTileMap(StageDetailMapDefinition def)
    {
        _tileMap.Init(def);
    }

    private void InitPuzzleUnits()
    {
        List<TileMapItem> tileItems;
        GameObject goTemp;
        UnitBase unit;

        for (int h = 0; h < _tileMap.Lines.Count; h++)
        {
            tileItems = _tileMap.Lines[h].Items;
            for (int w = 0; w < tileItems.Count; w++)
            {
                goTemp = PoolManager.Instance.GrabPrefabs(EPrefabsType.Unit, "Unit", _trMapUnits);
                unit = goTemp.GetComponent<UnitBase>();
                unit.SetPosition(tileItems[w].transform.position);
                unit.Init(new MapUnitWrapper(EUnitType.TileMapUnit, w, h));
                _mapUnits.Add(unit);
            }
        }
    }

    private void OnInputHandler()
    {
        //1. Touch Down 
        if (!m_bTouchDown && m_InputManager.isTouchDown)
        {
            //1.1 보드 기준 Local 좌표를 구한다.
            Vector2 point = m_InputManager.touch2BoardPosition;

            //1.2 Play 영역(보드)에서 클릭하지 않는 경우는 무시
            //if (!m_Stage.IsInsideBoard(point))
            //    return;

            //1.3 클릭한 위치의 블럭을 구한다.
            TilePos blockPos;
            //if (m_Stage.IsOnValideBlock(point, out blockPos))
            {
                //1.3.1 유효한(스와이프 가능한) 블럭에서 클릭한 경우
                m_bTouchDown = true;        //클릭 상태 플래그 ON
                m_TileDownPos = blockPos;  //클릭한 블럭의 위치(row, col) 저장
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
    }*/
}
