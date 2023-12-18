using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SelectQuestionMenu : MonoBehaviour
{
    public QuestionLoader questionsLoader;
    public HomeUIController HomeUIController;
    public SelectExamPanel SelectExamPanel;
    public Button backBtn;
    public Button mbtiBtn;
    public Button reviewBtn;
    public Button showHistoryBtn;
    public Button showMBTIBtn;
    public ScrollRect scrollrect;
    public Item itemPrb;
    public Item itemPrb2;
    public TextMeshProUGUI text;
    public GameObject historyPanel;
    ConfirmPanel confirmPanel;
    private bool isOpenned = false;
    [SerializeField] TextMeshProUGUI mbtiText , mbtiDesText;
    [SerializeField] ScrollRect scrollview2;
    [SerializeField] GameObject mbtiPanel;
    private void Awake()
    {
        mbtiBtn.onClick.AddListener(MBTIBtnClick);
        showMBTIBtn.onClick.AddListener(ShowMBTIReuslt);
        reviewBtn.onClick.AddListener(ReviewBtnClick);
        backBtn.onClick.AddListener(OnBackBtnClick);
        showHistoryBtn.onClick.AddListener(ShowHistory);
        confirmPanel = FindObjectOfType<ConfirmPanel>(true);
    }
    private void MBTIBtnClick()
    {
        confirmPanel.OnResult = (result) =>
        {
            if(result == ConfirmResult.OK)
            {
                HomeUIController.OpenLoadingPanel();
                questionsLoader.LoadMBTIQuestions();
                PlayerPrefs.SetInt("gamemode", 0);
                SceneManager.LoadScene("GamePlay");
            }

        };
        confirmPanel.Display();

    }
    private void ReviewBtnClick()
    {
        SelectExamPanel.Display();
    }
    private void OnBackBtnClick()
    {
        this.gameObject.SetActive(false);
        HomeUIController.homeMenu.gameObject.SetActive(true);
    }
    private void ShowHistory()
    {
        historyPanel.gameObject.SetActive(!historyPanel.activeInHierarchy);
        DbRequestManager.Instance.DataGetRequestWithToken(APIUrls.getResultReview, PlayerPrefs.GetString("usertoken"), (s) =>
        {
            Debug.Log(s);
            List<R3> listTest = JsonConvert.DeserializeObject<R>(s).result.items;
            if(listTest.Count > 0)
            {
                
                Debug.Log(listTest.Count);
                
                for (int i = 0; i < scrollrect.content.childCount; i++)
                {
                    scrollrect.content.GetChild(i).gameObject.SetActive(false);
                }
                foreach (R3 test in listTest)
                {
                    Item itemClone = GameObject.Instantiate(itemPrb, scrollrect.content);
                    itemClone.gameObject.SetActive(true);
                    itemClone.GetComponent<RectTransform>().sizeDelta = new Vector2(700, 100);
                    itemClone.GetComponentInChildren<Image>().enabled = true;
                    itemClone.SetItem(test.name, test.score);
                }
                text.gameObject.SetActive(false);
                scrollrect.gameObject.SetActive(true);
            }
            else
            {
                text.gameObject.SetActive(true);
                scrollrect.gameObject.SetActive(false);
            }

        });

        
    }
    public void ShowMBTIReuslt()
    {
        mbtiPanel.SetActive(!mbtiPanel.activeInHierarchy);
        DbRequestManager.Instance.DataGetRequestWithToken(APIUrls.getUserRecord, PlayerPrefs.GetString("usertoken"), (s) =>
        {
            A1 a1 = JsonConvert.DeserializeObject<A1>(s);
            if(a1.result.Length > 0)
            {
                A2 a2 = a1.result[0];
                string mbti = a2.result;
                int mbti_id = a2.mbtiId;
                Debug.Log(APIUrls.getMBTIDes + mbti);
                // lay des 
                DbRequestManager.Instance.DataGetRequestWithToken(APIUrls.getMBTIDes + mbti, PlayerPrefs.GetString("usertoken"), (s) =>
                {
                    MBTIResult2 result = JsonConvert.DeserializeObject<MBTIRespone>(s).result;
                    mbtiText.text = mbti;
                    mbtiDesText.text = result.description;
                });
                string getMBTIDepartment = APIUrls.getMBTIDepartment;


                getMBTIDepartment = String.Format(getMBTIDepartment, mbti_id);
                Debug.Log(getMBTIDepartment);
                DbRequestManager.Instance.DataGetRequestWithToken(getMBTIDepartment, PlayerPrefs.GetString("usertoken"), (s) =>
                {
                    Debug.Log(s);
                    List<Item2> listItem = JsonConvert.DeserializeObject<Respone2>(s).result.items;
                    Debug.Log(scrollview2.content.childCount);
                    ClearList();
                    for (int i = 0; i < listItem.Count; i++)
                    {
                        Item itemClone = Instantiate(itemPrb2, scrollview2.content);
                        itemClone.SetItem(listItem[i].department.name, listItem[i].department.description);
                        itemClone.gameObject.SetActive(true);
                    }
                });
                mbtiDesText.gameObject.SetActive(true);
            }
            else
            {
                ClearList();
                mbtiText.text = "Please play first ";
                mbtiDesText.gameObject.SetActive(false);
            }

        });


    }
    private void ClearList()
    {
        for (int i = 0; i < scrollview2.content.childCount; i++)
        {
            scrollview2.content.GetChild(i).gameObject.SetActive(false);
        }
    }
}
public class A1
{
    public A2[] result;
}

public class A2
{
    public int id;
    public string result;
    public int mbtiId;
}
public class Department
{
    public string id;
    public string name;
    public string code;
    public string description;
    public List<string> majors;
}
public class Item2
{
    public Department department;
}
public class Result2
{
    public int totalItemsCount;
    public int pageSize;
    public int pageIndex;
    public int totalPagesCount;
    public bool next;
    public bool previous;
    public List<Item2> items;
}
public class Respone2
{
    public int statusCode;
    public bool isSuccess;
    public string errorMessage;
    public Result2 result;
}