using Newtonsoft.Json;
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
    public ScrollRect scollrect;
    public Item itemPrb;
    public TextMeshProUGUI text;
    public GameObject historyPanel;
    ConfirmPanel confirmPanel;
    private bool isOpenned = false;
    private void Awake()
    {
        mbtiBtn.onClick.AddListener(MBTIBtnClick);
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
        if (isOpenned) return;
        isOpenned = true;
        string data = PlayerPrefs.GetString("test_history", "");
        if(data == "")
        {
            text.gameObject.SetActive(true);
            scollrect.gameObject.SetActive(false);
            return;
        }
        text.gameObject.SetActive(false);
        scollrect.gameObject.SetActive(true);
        
        List<TestHistory2> listTest  = JsonConvert.DeserializeObject<List<TestHistory2>>(data);
        for(int i = 0; i < scollrect.content.childCount; i++)
        {
            scollrect.content.transform.GetChild(0).gameObject.SetActive(false);
            Destroy(scollrect.content.transform.GetChild(0).gameObject);
        }
        foreach(TestHistory2 test in listTest)
        {
            Item itemClone = GameObject.Instantiate(itemPrb, scollrect.content);
            itemClone.gameObject.SetActive(true);
            itemClone.GetComponent<RectTransform>().sizeDelta = new Vector2(700, 100);
            itemClone.GetComponentInChildren<Image>().enabled = true;
            itemClone.SetItem(test.name , test.totalScore);
        }
        
    }
}