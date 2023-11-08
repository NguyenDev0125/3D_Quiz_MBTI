using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button OpenMenuBtn;
    public Button closeVictoryBtn;
    public GameObject questionList;
    public GameObject victoryPanel;
    public ReviewQuestionPanel reviewQuestionPanel;
    public MBTIQuestionPanel mbtiQuestionPanel;
    private void Awake()
    {
        OpenMenuBtn.onClick.AddListener(() =>
        {
            questionList.SetActive(!questionList.activeInHierarchy);
        });
        closeVictoryBtn.onClick.AddListener(ShowVictory);
    }

    public void HideUI()
    {
        reviewQuestionPanel.gameObject.SetActive(false);
        mbtiQuestionPanel.gameObject.SetActive(false);
    }

    internal void ShowVictory()
    {
        victoryPanel.gameObject.SetActive(!victoryPanel.activeInHierarchy);
    }
}
