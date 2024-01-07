using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfilePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI usernameTxt, tokenTxt;
    private void Awake()
    {
        UpdateStatus();
    }
    public void UpdateStatus()
    {
        usernameTxt.text = "Loading...";
        tokenTxt.text = "0";
        PlayerManager.Instance.GetUserProfile((user) =>
        {
            usernameTxt.text = user.username;
            tokenTxt.text = user.gameToken.ToString();
            Debug.Log(user.username + " " + user.gameToken);
        });
    }
}
