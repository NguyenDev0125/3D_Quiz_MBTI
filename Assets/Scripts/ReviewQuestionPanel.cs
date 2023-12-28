using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewQuestionPanel : QuestionPanel
{
    public ReviewQuestionController questionController;
    public override void DisplayQuestion(IQuestion question)
    {
        base.DisplayQuestion(question);
        A.btn.interactable = true;
        B.btn.interactable = true;
        C.btn.interactable = true;
        ReviewQuestionContent ques = (ReviewQuestionContent)question;
        quesTxt.text = ques.question;
        if(ques.listAnswer.Count == 3)
        {
            A.SetText(ques.listAnswer[0].value);
            B.SetText(ques.listAnswer[1].value);
            C.SetText(ques.listAnswer[2].value);
        }

        this.gameObject.SetActive(true);
    }

    public override void TakeResult(int result)
    {
        questionController.TakeResult(result);
    }
}
