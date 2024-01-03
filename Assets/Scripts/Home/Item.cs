using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTxt, scoreTxt;
    [SerializeField] Button button;
    string id;
    public void SetItem(string id,string name , int score , Action<string> callback)
    {
        this.id = id;
        nameTxt.text = name;
        scoreTxt.text = score.ToString();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>callback?.Invoke(this.id));
    }
    public void SetItem(string id,string name, string message , Action <string> callback)
    {
        nameTxt.text = "";
        scoreTxt.text = message;
        this.id = id;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => callback?.Invoke(this.id));
        this.gameObject.SetActive(true);
    }
}
