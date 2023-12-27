using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class MBTIQuestionController : QuestionController
{
    private int E = 0, I = 0, S = 0, N = 0, T = 0, F = 0, J = 0, P = 0;

    [SerializeField] MBTIQuestionsList MBTIquestionList;
    [SerializeField] UIController uiController;
    private MBTIQuestion currQuestion;
    private List<MBTIResult> results;
    private bool checkingResult = false;
    private void Awake()
    {
        results = new List<MBTIResult>();
        questionPanel.UpdateCounterText($"0/{MBTIquestionList.questions.Count}");
    }
    public override void DisplayRandomQuestion()
    {
        questionPanel.UpdateCounterText($"{++counter}/{MBTIquestionList.questions.Count}");
        currQuestion = GetRandomQuestion();
        questionPanel.DisplayQuestion(currQuestion);
    }
    public MBTIQuestion GetRandomQuestion()
    {
        if (MBTIquestionList.questions.Count == 0) return null;
        int randInt = Random.Range(0, MBTIquestionList.questions.Count);
        MBTIQuestion ques = MBTIquestionList.questions[randInt];
        MBTIquestionList.questions.RemoveAt(randInt);
        return ques;
    }

    public string GetMBTIString()
    {
        string MBTI = "";
        MBTI += (E > I) ? "E" : "I";
        MBTI += (S > N) ? "S" : "N";
        MBTI += (T > F) ? "T" : "F";
        MBTI += (J > P) ? "J" : "P";
        return MBTI;
    }

    public override void TakeResult(int result, int group)
    {
        numQuesAnswered++;
        if (result != 2)
        {
            switch(currQuestion.firstAnswerType)
            {
                case "E": if (result == 0) E++; else I++; break;
                case "S": if (result == 0) S++; else N++; break;
                case "T": if (result == 0) T++; else F++; break;
                case "J": if (result == 0) J++; else P++; break;
            }
        }

        MBTIResult mbti = new MBTIResult();
        mbti.idQues = currQuestion.id;
        mbti.nameQues = currQuestion.nameQuestion;
        mbti.answer = result == 0 ? currQuestion.nameAns1 : currQuestion.nameAns2;
        results.Add(mbti);

        if (MBTIquestionList.questions.Count == 0)
        {
            string mbtiString = GetMBTIString();
            Result rs = new Result();
            rs.result = mbtiString;
            rs.recordDetails = new List<ResultDetail>();
            for(int i = 0; i < results.Count; i++)
            {
                ResultDetail dt = new ResultDetail();
              
                dt.mbtI_ExamQuestionId = int.Parse(results[i].idQues);
                dt.userChoice = results[i].answer;
                rs.recordDetails.Add(dt);
            }
            string json = JsonConvert.SerializeObject(rs);
            Debug.Log("Push json");
            DbRequestManager.Instance.DataSendRequestWithToken(APIUrls.postRecord, json, PlayerPrefs.GetString("usertoken"), (s) =>
            {
                Debug.Log(json);
                Debug.Log(s);
                RS rs = JsonConvert.DeserializeObject<RP>(s).result;
                Debug.Log(rs.id);
                PlayerPrefs.SetInt("MBTI_ID",rs.id);
                PlayerPrefs.SetString("MBTI", mbtiString);
            });
            // 

            Debug.Log(APIUrls.getMBTIDes + mbti);
            DbRequestManager.Instance.DataGetRequestWithToken(APIUrls.getMBTIDes + mbtiString, PlayerPrefs.GetString("usertoken"), (s) =>
            {
                Debug.Log(s);
                string des = JsonConvert.DeserializeObject<MBTIRespone>(s).result.description;
                Debug.Log(des);
                uiController.victoryPanel.ShowResult(mbtiString + " : " + des);
                
            });
            GameManager.Instance.GameVictory();
            questionPanel.HidePanel();
            return;

        }
        else
        {
            if (numQuesAnswered < numQues)
            {
                DisplayRandomQuestion();
            }
            else
            {
                GameManager.Instance.ChangeState(GameState.Playing);
                questionPanel.HidePanel();
            }
            
        }
        checkingResult = false;// hmm :))

    }

}

public class Result
{
    public string result = "";
    public List<ResultDetail> recordDetails;
}

public class ResultDetail
{
    public int mbtI_ExamQuestionId = 0;
    public string userChoice = "";
}

class MBTIRespone
{
    public int statusCode;
    public bool isSuccess;
    public string errorMessage;
    public MBTIResult2 result;
}

class MBTIResult2
{
    public int id;
    public int id2;
    public string name;
    public string code;
    public string description;
    public string mbtI_Departments;
}

public class RP
{
    public int statusCode;
    public bool isSuccess;
    public string errorMessage;
    public RS result;
}
public class RS
{
    public int id;
    public string name;
    public string code;
    public string description;
    //public string mbti_Departments;// list
}