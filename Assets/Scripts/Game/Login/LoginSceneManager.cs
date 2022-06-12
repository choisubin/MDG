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
    public TMP_InputField _loginEmail;
    public TMP_InputField _loginPassWord;

    public GameObject _registerPopup;
    public GameObject _loginPopup;

    FirebaseAuth auth;
    private bool _isLoginFinish = false;
    void Awake()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isLoginFinish)
        {
            _isLoginFinish = false;
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }    
    }

    public void OnClick_Register()
    {
        auth.CreateUserWithEmailAndPasswordAsync(_registerEmail.text, _registerPassWord.text).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
        _registerPopup.SetActive(false);
    }

    public void OnClick_Login()
    {
        auth.SignInWithEmailAndPasswordAsync(_loginEmail.text, _loginPassWord.text).ContinueWith(task => {
            if(task.IsCompleted&&!task.IsCanceled&&!task.IsFaulted)
            {
                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
                _isLoginFinish = true;
            }

            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

        });
    }
}
