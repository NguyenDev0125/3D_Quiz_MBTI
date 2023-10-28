
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
        StartCoroutine(IE_SendDataRequest(url, data , callback));
    }

    private IEnumerator IE_SendDataRequest(string url, string json, Action<string> callback)
    {
        byte[] data = System.Text.Encoding.UTF8.GetBytes(json);

        UnityWebRequest www = new UnityWebRequest(url, "POST");
        www.uploadHandler = new UploadHandlerRaw(data);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        callback?.Invoke(www.downloadHandler.text);
    }

}
