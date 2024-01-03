using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnswerDetailItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questText;
    [SerializeField] AnswerDetail ans1, ans2, ans3;
    public void SetItem(string quest , string ans1, string ans2, string ans3 , int trueIndex)
    {
        questText.text = quest;
        this.ans1.SetAnswer(ans1);
        this.ans2.SetAnswer(ans2);
        this.ans3.SetAnswer(ans3);
        if (trueIndex == 0)
        {
            this.ans1.SetType(0);
        }else if(trueIndex == 1)
        {
            this.ans2.SetType(0);
        }
        else
        {
            this.ans3.SetType(0);
        }
    }

    public void SetItem(string quest, string ans1, string ans2, string ans3, int trueIndex , int falseIndex) 
    {
        this.gameObject.SetActive(true);
        questText.text = quest;
        this.ans1.SetAnswer(ans1);
        this.ans2.SetAnswer(ans2);
        this.ans3.SetAnswer(ans3);
        if (trueIndex == 0)
        {
            this.ans1.SetType(1);
        }
        else if (trueIndex == 1)
        {
            this.ans2.SetType(1);
        }
        else
        {
            this.ans3.SetType(1);
        }

        if(falseIndex == 0)
        {
            this.ans1.SetType(2);
        }else if(falseIndex == 1)
        {
            this.ans2.SetType(2);
        }else 
        { 
            this.ans3.SetType(2); 
        }

    }
}
