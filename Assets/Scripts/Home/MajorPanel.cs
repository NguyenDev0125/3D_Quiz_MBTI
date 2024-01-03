using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MajorPanel : MonoBehaviour
{
    [SerializeField] Button close;
    [SerializeField] ScrollRect listview;
    [SerializeField] MajorItem itemPrb;
    private void Awake()
    {
        close.onClick.AddListener(Close);
    }
    public void Open(string id)
    {
        DbRequestManager.Instance.DataGetRequestWithToken(APIUrls.getMajor + id, PlayerPrefs.GetString("usertoken"), (s) =>
        {
            Debug.Log(s);
            T1 t1 = JsonConvert.DeserializeObject<T1>(s);
            List<T3> listMajor = t1.result.majors;
            for(int i = 0; i < listMajor.Count; i++)
            {
                MajorItem clone = Instantiate(itemPrb, listview.content);
                clone.SetText(listMajor[i].majorName, listMajor[i].description);
            }
            this.gameObject.SetActive(true);
        });
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
        foreach(Transform t in listview.content.transform)
        {
            t.gameObject.SetActive(false);
        }
    }
}

public class T1
{
    public int statusCode;
    public bool isSuccess;
    public string errorMessage;
    public T2 result;
}

public class T2
{
    public string id;
    public string name;
    public string code;
    public string description;
    public List<T3> majors;
}

public class T3
{
    public string id;
    public string majorName;
    public string code;
    public string description;
}

