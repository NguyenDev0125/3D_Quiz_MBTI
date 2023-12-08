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

    private void Awake()
    {
        mbtiBtn.onClick.AddListener(MBTIBtnClick);
        reviewBtn.onClick.AddListener(ReviewBtnClick);
        backBtn.onClick.AddListener(OnBackBtnClick);
        showHistoryBtn.onClick.AddListener(ShowHistory);
        // tao button tren UI , xong gan event onClick thi an cai panel do di . 
        // :)) kho giai thich qua :D 
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
        
        string data = PlayerPrefs.GetString("test_historys", "");
        if(data == "")
        {
            text.gameObject.SetActive(true);
            scollrect.gameObject.SetActive(false);
            return;
        }
        text.gameObject.SetActive(false);
        scollrect.gameObject.SetActive(true);
        List<TestHistory2> listTest  = JsonConvert.DeserializeObject<List<TestHistory2>>(data);
        foreach(TestHistory2 test in listTest)
        {
            Item itemClone = GameObject.Instantiate(itemPrb, scollrect.content);
            itemClone.SetItem(test.name , test.totalScore);
        }
        
    }
}