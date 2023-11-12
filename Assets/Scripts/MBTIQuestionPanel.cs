using DG.Tweening;
using TMPro;
using UnityEngine;

public class MBTIQuestionPanel : QuestionPanel
{
    public MBTIQuestionController quesController;
    int group = 0;
    public override void DisplayQuestion(IQuestion ques)
    {
        base.DisplayQuestion(ques);
        MBTIQuestionContent question = (MBTIQuestionContent) ques;
        quesTxt.text = question.NameQues;
        A.SetText(question.Ans1);
        B.SetText(question.Ans2);
        C.SetText("Next question");
        A.btn.interactable = true; 
        B.btn.interactable = true;
        C.btn.interactable = true;
        group = question.Group;
        this.gameObject.SetActive(true);
    }
    public override void TakeResult(int result)
    {
        A.btn.interactable = false;
        B.btn.interactable = false;
        C.btn.interactable = false;
        quesController.TakeResult(result , group);
    }
}
