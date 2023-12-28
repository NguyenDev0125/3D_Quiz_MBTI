using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public Button btn;
    public int result = 0;
    public QuestionPanel questionPanel;
    private void Awake()
    {

        btn.onClick.AddListener(() =>
        {
            btn.interactable = false;
            SendResult();
            
        });
    }

    private void SendResult()
    {
        questionPanel.TakeResult(result);
    }
    public void SetText(string text)
    {
        txt.text = text;
        btn.image.color = btn.colors.normalColor;
    }
}
