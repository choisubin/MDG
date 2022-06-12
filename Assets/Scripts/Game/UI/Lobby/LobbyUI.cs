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

    // Start is called before the first frame update
    void Start()
    {
        Init();
        _stagePopupController.Init();
        _userNickName.text = FirebaseManager.Instance.dic["username"] as string;
        foreach (var a in FirebaseManager.Instance.dic)
        {
            Debug.LogError(a.Key);
            Debug.LogError(a.Value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Init();
    }

    const float rectNum = 0.46f;
    public void Init()
    {

        //_userNickName.text = (string)FirebaseManager.Instance.dic["username"];
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

    public void OnClick_SinglePlay()
    {
        _stagePopupController.Set();
        _stagePopupController.gameObject.SetActive(true);
    }
}
[SerializeField]
public enum EMiddleContents
{
    Home = 0,
    Inventory,
    Shop
}