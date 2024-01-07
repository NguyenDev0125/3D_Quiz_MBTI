using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new PlayerManager();
            }
            return instance;
        }
    }
    private UserProfile _userProfile;
    public UserProfile UserProfile => _userProfile;
    private PurchaseList _purchaseList;
    public PurchaseList PurchaseList => _purchaseList;
   public void GetUserProfile(Action<UserProfile> callback = null)
    {
        DbRequestManager.Instance.DataGetRequestWithToken(APIUrls.getUserProfile, PlayerPrefs.GetString("usertoken"), (s) =>
        {
            Debug.Log(s);
            UserProfile  user = JsonConvert.DeserializeObject<UserProfile>(s);
            _userProfile = user;
            callback?.Invoke(user);
        });
    }
    public int GetNumberToken()
    {
        if (_userProfile == null)
        {
            GetUserProfile();
            return -1;
        }
        return _userProfile.gameToken;
    }

    public void GetPurchaseList(Action<PurchaseList> callBack)
    {
        DbRequestManager.Instance.DataGetRequestWithToken(APIUrls.getStudentPurchases, PlayerPrefs.GetString("usertoken"), (s) =>
        {
            _purchaseList = JsonConvert.DeserializeObject<PurchaseList>(s);
            callBack?.Invoke(_purchaseList);
        });
    }

}

public class UserProfile
{
    public string id;
    public string username;
    public string firstName;
    public string lastName;
    public string email;
    public string role;
    public string createdDate;
    public string highestScore;
    public string address;
    public int gameToken;
    public bool allowMbti;
}

public class PurchaseList
{
    public int statusCode;
    public bool isSuccess;
    public string errorMessage;
    public List<PurchaseItem> result;
}
public class PurchaseItem
{
    public string id;
    public string name;
    public string description;
    public int totalNumberOfQuestion;
}
