using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ReviewQuestionController : QuestionController
{
    public ReviewQuestionList questionList;
    private ReviewQuestionContent currQuestion;
    private Attempts attempt;
    private void Awake()
    {
        attempt = new Attempts();
    }

    public override void TakeResult(int answerIndex)
    {
        if (questionList.questions.Count == 0)
        {
            CompleteQuestion();
        }
        else
        {
            AttemptDetailRequest newAttemptDetail = new AttemptDetailRequest();
            newAttemptDetail.examinationQuestionId = currQuestion.examinationQuestionId;
            newAttemptDetail.isCorrect = currQuestion.listAnswer[answerIndex].isAnswer;
            newAttemptDetail.userAnswered = currQuestion.listAnswer[answerIndex].value;
            attempt.attemptDetails.Add(newAttemptDetail);

        }
        numQuesAnswered++;
        if(numQuesAnswered < numQues)
        {
            DisplayRandomQuestion();
        }
        else
        {
            GameManager.Instance.ChangeState(GameState.Playing);
            questionPanel.HidePanel();
        }

    }

    private void CompleteQuestion()
    {
        GameManager.Instance.GameVictory();
        string json = JsonConvert.SerializeObject(attempt);
        string token = PlayerPrefs.GetString("usertoken");
        Debug.Log(json);
        DBRequestManager.Instance.DataSendRequestWithToken(APIUrls.postAttemptApi, json, token, (s) =>
        {
            Debug.Log(s);
        });
        GameManager.Instance.ChangeState(GameState.Playing);
    }
    public override void DisplayRandomQuestion()
    {
        if(questionList.questions.Count == 0)
        {
            CompleteQuestion();
        }
        currQuestion = GetRandomQuestionContent();
        if(currQuestion != null)
        {
            questionPanel.DisplayQuestion(currQuestion);
        }
    }

    private ReviewQuestionContent GetRandomQuestionContent()
    {
        if (questionList.questions.Count <= 0) return null;
        int rand = Random.Range(0, questionList.questions.Count);
        currQuestion = questionList.questions[rand];
        questionList.questions.RemoveAt(rand);
        return currQuestion;
    }

}
#region DATA STRUCT

internal class Attempts
{
    public string examDate;
    public int attempType = 0;
    public List<AttemptDetailRequest> attemptDetails;
    public Attempts()
    {
        examDate = "2023-10-31T14:06:37.577Z";
        attemptDetails = new List<AttemptDetailRequest>();
    }
}
internal class AttemptDetailRequest
{
    public string examinationQuestionId;
    public bool isCorrect;
    public string userAnswered;
}
public class MBTIResult
{
    public string idQues;
    public string nameQues;
    public string answer;
    public int group;
}


public class QuestionStruct
{
    public int IDQues;
    public string NameQues;
    public int IDAns;
    public string NameAns;
    public int Group;
}
public class AnswerStruct
{
    public int IDAns;
}

[SerializeField]
public interface IQuestion { }
public class MBTIExam
{
    public string id { get; set; }
    public string note { get; set; }
    public int numberOfQuestion { get; set; }
    public string createdBy { get; set; }
    public List<MBTIQuestionContent> mbtI_ExamQuestions { get; set; }
}
[Serializable]
public class MBTIQuestionContent
{
    public MBTIQuestion mbtI_Question { get; set; }
}
[Serializable]
public class MBTIQuestion : IQuestion
{
    public string id { get; set; }
    public string nameQuestion { get; set; }
    public string firstAnswerType { get; set; }
    public string secondAnswerType { get; set; }
    public string nameAns1 { get; set; }
    public string nameAns2 { get; set; }
    public string category { get; set; }
    public string createdBy { get; set; }
}

[Serializable]
public class Exam
{
    public string id;
    public string name { get; set; }
    public string description { get; set; }
    public int totalNumberOfQuestion { get; set; }
    public List<ExamQuestion> examinationQuestions { get; set; }
}
[Serializable]
public class ExamQuestion
{
    public string id;
    public Question question { get; set; }
}
[Serializable]
public class Question
{
    public string id { get; set; }
    public string content { get; set; }

}
[Serializable]
public class ReviewQuestionContent : IQuestion
{
    public string examinationQuestionId;
    public string question;
    public List<Answer> listAnswer;

}
[Serializable]
public class Answer
{
    public string value;
    public bool isAnswer;
}
[Serializable]
public class ExamList
{
    public int totalItemsCount;
    public int pageSize;
    public int pageIndex;
    public int totalPagesCount;
    public bool next;
    public bool previous;
    public List<Exam> items { get; set; }
}
[Serializable]
public class Respone
{
    public int statusCode { get; set; }
    public bool isSuccess { get; set; }
    public object errorMessage { get; set; }
    public ExamList result { get; set; }
}
#endregion