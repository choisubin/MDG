using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StagePopupController : BaseController
{
    [SerializeField] private TextMeshProUGUI _txtStageInfo;
    [SerializeField] private Image _StageTitleImg;
    private Dictionary<int, List<StageWrapperDefinition>> _stageDefinition;
    public override void Init()
    {
        _stageDefinition = DefinitionManager.Instance.GetDatas<List<StageWrapperDefinition>>();
    }

    private int _curStageNum;
    public int CurStageNum
    {
        get
        {
            return _curStageNum;
        }
        set
        {
            if(value > 0 && value <= _stageDefinition.Count)
            {
                _curStageNum = value;
                _curPartNum = 1;
                _curStageDef = _stageDefinition[_curStageNum][_curPartNum - 1];
                UpdatePopup(_curStageDef);
            }
        }
    }
    private int _curPartNum;
    public int CurPartNum
    {
        get
        {
            return _curPartNum;
        }
        set
        {
            if(value>0 && value <= _stageDefinition[_curStageNum].Count)
            {
                _curPartNum = value;
                _curStageDef = _stageDefinition[_curStageNum][_curPartNum - 1];
                UpdatePopup(_curStageDef);
            }
        }
    }
    private StageWrapperDefinition _curStageDef;
    public override void Set()
    {
        _curStageNum = 1;
        _curPartNum = 1;
        _curStageDef = _stageDefinition[_curStageNum][_curPartNum-1];
        UpdatePopup(_curStageDef);
    }
    public override void AdvanceTime(float dt_sec)
    {
    }
    public override void Dispose()
    {
    }

    private void UpdatePopup(StageWrapperDefinition stageDef)
    {
        _txtStageInfo.text = string.Format("{0} - {1}", stageDef.key, stageDef.partNum);
        _StageTitleImg.sprite = Resources.Load<Sprite>("Sprites/Unit/"+stageDef.titleUnitImgStr);
    }

    public void OnClick_RightBtn()
    {
        CurStageNum++;
    }
    public void OnClick_LeftBtn()
    {
        CurStageNum--;
    }
    public void OnClick_UpBtn()
    {
        CurPartNum++;
    }
    public void OnClick_DownBtn()
    {
        CurPartNum--;
    }
    public void OnClick_Exit()
    {
        Dispose();
        gameObject.SetActive(false);
    }
}
