using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectQuestionMenu : MonoBehaviour
{
    public QuestionLoader questionsLoader;
    public HomeUIController HomeUIController;
    public SelectExamPanel SelectExamPanel;
    public Button mbtiBtn;
    public Button reviewBtn;

    ConfirmPanel confirmPanel;

    private void Awake()
    {
        mbtiBtn.onClick.AddListener(MBTIBtnClick);
        reviewBtn.onClick.AddListener(ReviewBtnClick);
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

}
