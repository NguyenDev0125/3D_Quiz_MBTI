using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ReviewQuestionController : QuestionController
{
    public ReviewQuestionList questionList;
    public UIController uiController;
    private ReviewQuestionContent currQuestion;
    private Attempts attempt;
    private bool isWin = false;
    private void Awake()
    {
        attempt = new Attempts();
        attempt.name = questionList.examName;
        totalQues = questionList.questions.Count;

    }
    private void Start()
    {
        questionPanel.UpdateCounterText($"{counter}/{totalQues}");
    }
    public override void TakeResult(int answerIndex)
    {
        AttemptDetailRequest newAttemptDetail = new AttemptDetailRequest();
        newAttemptDetail.examinationQuestionId = currQuestion.examinationQuestionId;
        newAttemptDetail.isCorrect = currQuestion.listAnswer[answerIndex].isAnswer;
        newAttemptDetail.userAnswered = currQuestion.listAnswer[answerIndex].value;
        attempt.attemptDetails.Add(newAttemptDetail);

        if (questionList.questions.Count == 0)
        {
            
            CompleteQuestion();
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
        if (isWin) return;
        isWin = true;
        GameManager.Instance.GameVictory();
        string json = JsonConvert.SerializeObject(attempt);
        string token = PlayerPrefs.GetString("usertoken");
        Debug.Log(json);
        DbRequestManager.Instance.DataSendRequestWithToken(APIUrls.postAttemptApi, json, token, (s) =>
        {
            Debug.Log(  s);
            // con tai sao co 2 de li thi, toi cx kh bt :)) ua co ha
            DbRequestManager.Instance.DataGetRequestWithToken(APIUrls.getResultReview, PlayerPrefs.GetString("usertoken"), (s) =>
            {
                Debug.Log("Test");
                Debug.Log("API Tra ve :  " + s);
                List<R3> listR3 = JsonConvert.DeserializeObject<R>(s).result.items;
                uiController.victoryPanel.ShowListResult(listR3);
                Debug.Log(listR3.Count);
                int score = 0;
                foreach (R3 r3 in listR3)
                {
                    score += r3.score;
                }

                List<TestHistory2> listTest;
                string data = PlayerPrefs.GetString("test_history", "");
                if (data == "")
                {
                    listTest = new List<TestHistory2>();
                }
                else
                {
                    listTest = JsonConvert.DeserializeObject<List<TestHistory2>>(data);
                }
                TestHistory2 test = new TestHistory2();
                test.name = attempt.name;
                test.examDate = listR3[0].examDate;
                test.totalScore = score;
                bool check = true;
                for (int i = 0; i < listTest.Count; i++)
                {
                    if (listTest[i].name == test.name)
                    {
                        check = false;
                        listTest[i] = test; break;
                    }
                }
                if (check) listTest.Add(test);
                string js = JsonConvert.SerializeObject(listTest);
                Debug.Log(js);
                PlayerPrefs.SetString("test_history", js);

            });

            GameManager.Instance.ChangeState(GameState.Playing);
        });

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
            questionPanel.UpdateCounterText($"{++counter}/{totalQues}");
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
    public string name;
    //public string examDate;
    public string examDate;
    public int attempType = 0;
    public List<AttemptDetailRequest> attemptDetails;

    public Attempts()
    {
        //examDate = "2023-10-31T14:06:37.577Z";
        DateTime curDate = DateTime.UtcNow;
        examDate = curDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        //examDate = DateTime.Now.ToString("dd-mm-yyyy"); // output 07-12-2023 
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
    public int id;
    public MBTIQuestion mbtI_Question { get; set; }
}
[Serializable]
public class MBTIQuestion : IQuestion
{
    public string id { get; set; }
    public string id2 { get; set; }
    public string nameQuestion { get; set; }
    public string firstAnswerType { get; set; }
    public string secondAnswerType { get; set; }
    public string nameAns1 { get; set; }
    public string nameAns2 { get; set; }
    public string category { get; set; }
    public string createdBy { get; set; }
}

[Serializable]
public class TestHistory // cai nay chui dau ra 
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
    public List<TestHistory> items { get; set; }
}
[Serializable]
public class Respone
{
    public int statusCode { get; set; }
    public bool isSuccess { get; set; }
    public object errorMessage { get; set; }
    public ExamList result { get; set; }
}

public class R
{
    public int statusCode;
    public bool isSuccess;
    public string errorMessage;
    public R2 result;
}

public class R2
{
    public int totalItemsCount;
    public int pageSize;
    public int pageIndex;
    public int totalPagesCount;
    public bool next;
    public bool previous;
    public List<R3> items;
}
public class R3
{
    public string id;
    public string name;
    public string examDate;
    public int attempType;
    public int score;
    public string result;
    public string doneBy;
    public string userId;

}

#endregion