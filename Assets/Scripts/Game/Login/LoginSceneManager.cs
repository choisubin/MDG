using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class LoginSceneManager : MonoBehaviour
{
    public TMP_InputField _registerEmail;
    public TMP_InputField _registerPassWord;
    public TMP_InputField _registerNickName;
    public TMP_InputField _loginEmail;
    public TMP_InputField _loginPassWord;

    public GameObject _registerPopup;
    public GameObject _loginPopup;

    private float _curTime = 0;
    private bool _isLogin = false;
    void Update()
    {
        if(_isLogin)
        {
            SceneManager.LoadScene(1);
            //SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
        //{
        //    _curTime += Time.deltaTime;
        //    if (_curTime > 2f)
        //    {
        //        FirebaseManager.Instance.LoadData(()=>
        //        {
        //            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        //        });
        //    }
        //}    
    }

    public void OnClick_Register()
    {
        FirebaseManager.Instance.Register(_registerNickName.text, _registerEmail.text, _registerPassWord.text);
        _registerPopup.SetActive(false);
    }

    public void OnClick_Login()
    {
        FirebaseManager.Instance.Login(_loginEmail.text, _loginPassWord.text,()=>
        {
            _isLogin = true;
        });
    }
}
