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
    public Button mbtiBtn;
    public Button reviewBtn;

    ConfirmPanel confirmPanel;

    private void Awake()
    {
        mbtiBtn.onClick.AddListener(LoadQuestionFromLocal);
        reviewBtn.onClick.AddListener(LoadQuestionFormUrl);
        confirmPanel = FindObjectOfType<ConfirmPanel>(true);
    }
    private void LoadQuestionFromLocal()
    {
        confirmPanel.OnResult = (result) =>
        {
            if(result == ConfirmResult.OK)
            {
                HomeUIController.OpenLoadingPanel();
                questionsLoader.LoadReviewQuestions();
                PlayerPrefs.SetInt("gamemode", 0);
                SceneManager.LoadScene("GamePlay");
            }

        };
        confirmPanel.Display();

    }
    private void LoadQuestionFormUrl()
    {
        confirmPanel.OnResult = (result) =>
        {
            if(result == ConfirmResult.OK)
            {
                HomeUIController.OpenLoadingPanel();
                questionsLoader.LoadQuestionFormAPI();
                PlayerPrefs.SetInt("gamemode", 1);
                SceneManager.LoadScene("GamePlay");
            }

        };
        confirmPanel.Display();
    }

}
