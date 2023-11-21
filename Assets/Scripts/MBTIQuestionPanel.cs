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
        MBTIQuestion question = (MBTIQuestion) ques;
        quesTxt.text = question.nameQuestion;
        A.SetText(question.nameAns1);
        B.SetText(question.nameAns2);
        C.SetText("Next question");
        A.btn.interactable = true; 
        B.btn.interactable = true;
        C.btn.interactable = true;
        group = 0;
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
