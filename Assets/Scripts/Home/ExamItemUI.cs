using Newtonsoft.Json;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExamItemUI : MonoBehaviour
{
    [SerializeField] Button selectBtn;
    [SerializeField] Image btnImage, tokenIcon;
    [SerializeField] TextMeshProUGUI nameTxt, desTxt , btnText;
    [SerializeField] Sprite unlockedSpr, lockedSpr;
    Action<string> callBack;
    string id;
    public void SetItem(string id,string n , string des , int price, bool isUnlocked , Action<string> callback)
    {
        if(isUnlocked)
        {
            selectBtn.onClick.RemoveAllListeners();
            selectBtn.onClick.AddListener(OnSelectButtonClick);
            btnImage.sprite = unlockedSpr;
            btnText.text = "Select";
        }
        else
        {
            selectBtn.onClick.RemoveAllListeners();
            selectBtn.onClick.AddListener(PurchaseExam);
            btnImage.sprite = lockedSpr;
            btnText.text = price.ToString();
            tokenIcon.gameObject.SetActive(true);

        }
        Debug.Log(id);
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
        confirmPanel.Display("Bài kiểm tra đã được mua, bạn có muốn vào trò chơi luôn chứ ?");
    }

    private void PurchaseExam()
    {
        ConfirmPanel confirmPanel = FindObjectOfType<ConfirmPanel>(true);
        confirmPanel.OnResult = (result) =>
        {
            if (result == ConfirmResult.OK)
            {
                PurchaseExam exam = new PurchaseExam();
                exam.examId = this.id;
                String js = JsonConvert.SerializeObject(exam);
                Debug.Log(js);
                DbRequestManager.Instance.DataSendRequestWithToken(APIUrls.purchaseExam, js , PlayerPrefs.GetString("usertoken"), (s) =>
                {
                    Debug.Log(s);
                    PurchaseResult rs = JsonConvert.DeserializeObject<PurchaseResult>(s);
                    if(rs.isSuccess)
                    {
                        selectBtn.onClick.RemoveAllListeners();
                        selectBtn.onClick.AddListener(OnSelectButtonClick);
                        btnImage.sprite = unlockedSpr;
                        tokenIcon.gameObject.SetActive(false);
                        btnText.text = "Select";
                        FindObjectOfType<ProfilePanel>().UpdateStatus();
                    }
                });
            }

        };
        confirmPanel.Display("Mua bài kiểm tra này với giá 10 tokens chứ ?");
    }

}
struct PurchaseExam
{
    public string examId;
}
struct PurchaseResult
{
    public int statusCode;
    public bool isSuccess;
    public string errorMessage;
    public string result;
}

