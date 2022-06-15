using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity;
using UnityEngine.SceneManagement;
using UnityEngine;
using Firebase.Auth;
using System;

public class FirebaseManager : MonoBehaviour
{
    private static FirebaseManager _instance;
    public static FirebaseManager Instance
    {
        get
        {

            if (_instance == null)
            {
                _instance = FindObjectOfType<FirebaseManager>();
                if (FindObjectsOfType<FirebaseManager>().Length > 1)
                {
                    Debug.LogError("[Singleton] Something went really wrong " +
                        " - there should never be more than 1 singleton!" +
                        " Reopening the scene might fix it.");
                    return _instance;
                }

                if (_instance == null)
                {
                    GameObject go = new GameObject("FirebaseManager");
                    _instance = go.AddComponent<FirebaseManager>();
                }
            }

            return _instance;
        }
    }

    public FirebaseAuth auth { get; private set; }
    public Firebase.FirebaseApp app { get; private set; }
    public DatabaseReference reference;
    private bool _isLogin = false;
    public bool IsLogin
    {
        get
        {
            return _isLogin;
        }
    }
    public IDictionary<string, object> dic { get; private set; }
    private Dictionary<object, IDictionary> _userUnitDataDic = new Dictionary<object, IDictionary>();
    public Dictionary<int, UserUnitData> userUnitDataDic = new Dictionary<int, UserUnitData>();

    public UserUnitData[] CurrentEquipUnit = new UserUnitData[5];

    private void Awake()
    {
        // 객체 초기화
        DontDestroyOnLoad(gameObject);
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    private Firebase.Auth.FirebaseUser user;
    public void Login(string email,string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(
               task => {
                   if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
                   {
                       _isLogin = true;
                       user = task.Result;
                       LoadDatabase();
                   }
                   else
                   {
                       Debug.Log("로그인에 실패하셨습니다.");
                   }
               }
           );
    }

    public void Register(string username,string email,string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(
               task => {
                   if (!task.IsCanceled && !task.IsFaulted)
                   {
                       Debug.Log(email + "로 회원가입\n");

                        //회원가입한 사용자의 고유 번호를 통한 사용자 기본 생성
                        Firebase.Auth.FirebaseUser newUser = task.Result;
                        writeNewUser(newUser.UserId,username);
                   }
                   else
                       Debug.Log("회원가입 실패\n");
               });
    }

    [Serializable]
    public class User // 사용자 클래스 생성
    {
        public string username;
        public int coin;
        public int crystal;
        public List<UserUnitData> unitData;
        public User(string username)
        {
            this.username = username;
            coin = 0;
            crystal = 0;
            unitData = new List<UserUnitData>();
            unitData.Add(new UserUnitData(1, 1,1));
            unitData.Add(new UserUnitData(2, 1,2));
            unitData.Add(new UserUnitData(3, 1,3));
            unitData.Add(new UserUnitData(4, 1, 4));
            unitData.Add(new UserUnitData(5, 1,5));
            unitData.Add(new UserUnitData(6, 1, 0));
            unitData.Add(new UserUnitData(7, 1, 0));
            unitData.Add(new UserUnitData(8, 1, 0));
        }
    }

    [Serializable]
    public class UserUnitData
    {
        public int key;
        public int level;
        public int equipSlot;
        public UserUnitData(int key, int level,int equipSlot)
        {
            this.key = key;
            this.level = level;
            this.equipSlot = equipSlot;
        }
        public UserUnitData()
        { }

    }


    void writeNewUser(string userid,string username) // 가입한 회원 고유 번호에 대한 사용자 기본값 설정
    {
        User user = new User(username);
        string json = JsonUtility.ToJson(user); // 생성한 사용자에 대한 정보 json 형식으로 저장
        reference.Child(userid).SetRawJsonValueAsync(json); // 데이터베이스에 json 파일 업로드
    }

    public void LoadDatabase()
    {
        reference.Child(user.UserId).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error Database");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                object value = snapshot.Value;
                if (null != (value as IDictionary))
                {
                    dic = (IDictionary<string, object>)snapshot.Value;
                }
            }
        });

        DatabaseReference unitdata = FirebaseDatabase.DefaultInstance.GetReference(user.UserId);
        unitdata.Child("unitData").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary unitInfo = (IDictionary)data.Value;
                    _userUnitDataDic.Add(unitInfo["key"], unitInfo);
                    //Debug.LogErrorFormat("{0} / {1} / {2}", unitInfo["key"], unitInfo["level"], unitInfo["equipSlot"]);
                }
            }
        });
    }
    public void LoadDataToDatabase()
    {
        UserUnitData[] equipDatas = new UserUnitData[5];
        foreach(var data in _userUnitDataDic)
        {
            IDictionary d = (IDictionary)data.Value;
            int key = Convert.ToInt32(d["key"]);
            int level = Convert.ToInt32(d["level"]);
            int equipSlot = Convert.ToInt32(d["equipSlot"]);
            UserUnitData unitdata = new UserUnitData(key, level, equipSlot);
            userUnitDataDic.Add(key, unitdata);

            if (equipSlot > 0 && equipSlot < 6)
            {
                equipDatas[equipSlot - 1] = unitdata;
            }
        }
        CurrentEquipUnit = equipDatas;
    }
}
