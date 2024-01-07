using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectExamPanel : MonoBehaviour
{
    [SerializeField] QuestionLoader questionLoader;
    [SerializeField] ScrollRect scrollview;
    [SerializeField] ExamItemUI examUIPrb;
    [SerializeField] Button closeBtn;
    [SerializeField] ProfilePanel profilePanel;

    List<TestHistory> listExams;
    private void Awake()
    {
        closeBtn.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
        });
    }
    private bool isReviewExamLoaded = false;
    public void Display()
    {
        gameObject.SetActive(true);
        if (isReviewExamLoaded) return;
        isReviewExamLoaded = true;

        questionLoader.LoadReviewExam((respone) =>
        {
            listExams = respone.result.items;
            PlayerManager.Instance.GetPurchaseList((list) =>
            {
                for (int i = 0; i < listExams.Count; i++)
                {
                    TestHistory ex = listExams[i];

                    bool isUnlocked = false;
                    foreach(var it in list.result)
                    {
                        if(it.id == ex.id)
                        {
                            isUnlocked = true;
                            break;
                        }
                    }
                        ExamItemUI item = Instantiate(examUIPrb);
                    item.SetItem(ex.id, ex.name, ex.description, ex.examPrice, isUnlocked, SelectExam);
                    item.transform.SetParent(scrollview.content, false);
                }
            });

        });
        
    }

    private void SelectExam(string id)
    {
        foreach(TestHistory ex in listExams)
        {
            if(ex.id == id)
            questionLoader.SaveExam(ex);
        }
        FindObjectOfType<HomeUIController>().OpenLoadingPanel();
        PlayerPrefs.SetInt("gamemode", 1);
        SceneManager.LoadScene("GamePlay");
    }
}
