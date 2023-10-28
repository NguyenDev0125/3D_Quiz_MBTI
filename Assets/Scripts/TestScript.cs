using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string json = "{\r\n  \"userName\": \"string\",\r\n  \"password\": \"string\",\r\n  \"confirmPassword\": \"string\"\r\n}";
        DbRequestManager.Instance.DataSendRequest(ApiUrls.userRegisterApi , json );
    }

}
