using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI chatText;
    [SerializeField] TextMeshProUGUI answer, answer2;
    [SerializeField] Button btn1, btn2 , openChat;
    [SerializeField] GameObject chatPanel;
    [SerializeField] Chat currChat;
    Action finishCallback;
    private void Start()
    {
        btn1.onClick.AddListener(Btn1Click);
        btn2.onClick.AddListener(Btn2Click);
        openChat.onClick.AddListener(DisplayChat);
    }
    public void ShowPanel(Chat root ,Action callback = null)
    {
        currChat = root;
        finishCallback = callback;
        this.gameObject.SetActive(true);
        openChat.gameObject.SetActive(true);
    }
    private void DisplayChat()
    {
        chatText.text = currChat.quest;
        answer.text = currChat.answer1;
        answer2.text = currChat.answer2;
        chatPanel.gameObject.SetActive (true);
        openChat.gameObject.SetActive(false);
    }
    public void HidePanel()
    {
        this.gameObject.SetActive (false);
        chatPanel.gameObject.SetActive(false);
        
    }

    private void Btn1Click()
    {
        if (currChat.branch1 == null)
        {
            HidePanel();
            finishCallback?.Invoke();
            return;
        }
        currChat = currChat.branch1;
        DisplayChat();
 
    }

    private void Btn2Click()
    {
        if (currChat.branch2 == null)
        {
            finishCallback?.Invoke();
            HidePanel();
            return;
        }
        currChat = currChat.branch2;
        DisplayChat();
    }
}



