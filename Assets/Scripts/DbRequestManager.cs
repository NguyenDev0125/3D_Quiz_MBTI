
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DbRequestManager : MonoBehaviour
{
    #region MAKE SINGLETON
    private static DbRequestManager instance;
    public static DbRequestManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindAnyObjectByType<DbRequestManager>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (DbRequestManager.instance != null && DbRequestManager.instance != this) Destroy(this);
        DontDestroyOnLoad(DbRequestManager.Instance);
    }
    #endregion
    public void DataGetRequestWithToken(string url , string token , Action<string> callback)
    {
        StartCoroutine(IE_DataGetRequestWithToken(url, token, callback));
    }
    private IEnumerator IE_DataGetRequestWithToken(string url , string token , Action<string > callBack)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        www.SetRequestHeader("Authorization", "Bearer " + token);
        yield return www.SendWebRequest();
        if (www.isDone)
        {
            callBack.Invoke(www.downloadHandler.text);
        }
        else
        {
            Debug.LogError(www.error);
        }
    }
    public void DataGetRequest(string url , Action<string> callback)
    {
        StartCoroutine(IE_DataGetRequest(url, callback));
    }

    private IEnumerator IE_DataGetRequest(string url , Action<string> callback)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if(www.isDone)
        {
            callback.Invoke(www.downloadHandler.text);
        }
        else
        {
            Debug.LogError(www.error);
        }
    }
    public void DataSendRequest(string url , string data , Action<string> callback = null)
    {
        StartCoroutine(IE_DataSendRequest(url, data , null, callback));
    }

    public void DataSendRequestWithToken(string url , string data, string token, Action<string> callBack = null)
    {
        StartCoroutine(IE_DataSendRequest(url, data, token , callBack));
        // nay no giong nhu da luong ay ong 
        // khi nay , goi ham nay 2 lan lien tuc , post va get , cai kia chua post len thi cai get da gui request len nen no chi lay dc du lieu cu hay sao ay , neen nhet cai get chay sau khi 
        // sau khi post xong la dc 
    }

    private IEnumerator IE_DataSendRequest(string url, string json , string token = null , Action<string> callback = null)
    {
        byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
        Debug.Log("Log" + json);
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        www.uploadHandler = new UploadHandlerRaw(data);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        if(token != null)
        {
            www.SetRequestHeader("Authorization", "Bearer " + token);
        }

        yield return www.SendWebRequest();

        callback?.Invoke(www.downloadHandler.text);
    }

    public void FieldDataSendRequest(string api , string data, string token ,  Action<string> callBack = null)
    {
        StartCoroutine(IE_FieldDataSendRequest(api , data, token , callBack));
    }

    public IEnumerator IE_FieldDataSendRequest(string api, string data, string token , Action<string> callBack)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(api, data))
        {
            www.SetRequestHeader("accept", "*/*");
            www.SetRequestHeader("Authorization","Bearer "+ token);

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.result);
            }
            else
            {
                callBack?.Invoke("");
            }
        }
    }
    }


