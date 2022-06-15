using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LobbyUI : MonoBehaviour
{
    [SerializeField] private RectTransform _bottomToggleGroup;
    [SerializeField] private RectTransform _anchor;
    [SerializeField] private RectTransform _infoPanelTr;
    [SerializeField] private RectTransform _infoImgTr;
    [SerializeField] private RectTransform _infoTextTr;
    [SerializeField] private GameObject[] _middleContents;

    [SerializeField] private StagePopupController _stagePopupController;

    [SerializeField] private TextMeshProUGUI _userNickName;

    [SerializeField] private Inventory _inventory;

    const float rectNum = 0.46f;

    public void Set()
    {
        this.gameObject.SetActive(true);
        _stagePopupController.Init();
        SetUiSize();
        _userNickName.text = FirebaseManager.Instance.dic["username"] as string;
    }

    private void SetUiSize()
    {
        _bottomToggleGroup.sizeDelta = new Vector2(Mathf.Min(_anchor.rect.width, 1040), 320);
        //_infoImgTr.sizeDelta = new Vector2(_infoPanelTr.rect.width * rectNum, _infoPanelTr.rect.width * rectNum);
        //_infoTextTr.sizeDelta = new Vector2(_infoPanelTr.rect.width - _infoImgTr.rect.width - 10f, _infoTextTr.rect.height);
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
        if(contents==((int)EMiddleContents.Inventory))
        {
            _inventory.Set();
        }
    }

    public void OnClick_SinglePlay()
    {
        _stagePopupController.Set();
        _stagePopupController.gameObject.SetActive(true);
    }

    public void Dispose()
    {
        gameObject.SetActive(false);
    }
}
[SerializeField]
public enum EMiddleContents
{
    Home = 0,
    Inventory,
    Shop
}