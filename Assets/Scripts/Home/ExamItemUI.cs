using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExamItemUI : MonoBehaviour
{
    [SerializeField] Button selectBtn;
    [SerializeField] TextMeshProUGUI nameTxt, desTxt;
    Action<string> callBack;
    string id;
    public void SetItem(string id,string n , string des , Action<string> callback)
    {
        selectBtn.onClick.AddListener(OnSelectButtonClick);
        this.callBack = callback;
        nameTxt.text = n;
        desTxt.text = des;
        this.id = id;
    }

    private void OnSelectButtonClick()
    {
        ConfirmPanel confirmPanel = FindObjectOfType<ConfirmPanel>(true);
        confirmPanel.OnResult = (result) =>
        {
            if (result == ConfirmResult.OK)
            {
                callBack?.Invoke(id);
            }

        };
        confirmPanel.Display();
    }
}
