using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private RectTransform _bottomToggleGroup;
    [SerializeField] private RectTransform _anchor;
    [SerializeField] private RectTransform _infoPanelTr;
    [SerializeField] private RectTransform _infoImgTr;
    [SerializeField] private RectTransform _infoTextTr;
    [SerializeField] private GameObject[] _middleContents;

    [SerializeField] private StagePopupController _stagePopupController;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        _stagePopupController.Init();
        _stagePopupController.Set();
    }

    // Update is called once per frame
    void Update()
    {
        Init();
    }

    const float rectNum = 0.46f;
    public void Init()
    {
        _bottomToggleGroup.sizeDelta = new Vector2(Mathf.Min(_anchor.rect.width, 1040), 320);
        _infoImgTr.sizeDelta = new Vector2(_infoPanelTr.rect.width * rectNum, _infoPanelTr.rect.width * rectNum);
        _infoTextTr.sizeDelta = new Vector2(_infoPanelTr.rect.width - _infoImgTr.rect.width-10f, _infoTextTr.rect.height);
    }

    private void AllMiddleContentsOff()
    {
        foreach(var middle in _middleContents)
        {
            middle.SetActive(false);
        }
    }

    public void OnClick_Tab(int contents)
    {
        AllMiddleContentsOff();
        _middleContents[(int)contents].SetActive(true);
    }


}
[SerializeField]
public enum EMiddleContents
{
    Home = 0,
    Inventory,
    Shop
}