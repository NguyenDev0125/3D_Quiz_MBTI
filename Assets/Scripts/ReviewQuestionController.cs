﻿using Newtonsoft.Json;
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
    public override void DisplayRandomQuestion()
    {
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
public interface IQuestion
{

}
public class MBTIQuestionContent : IQuestion
{
    public int IDQues;
    public int TrueAnswer;
    public string NameQues;
    public string Ans1;
    public string Ans2;
    public int Group;
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