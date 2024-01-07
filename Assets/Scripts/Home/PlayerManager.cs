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
    public string allowMbti;
}
