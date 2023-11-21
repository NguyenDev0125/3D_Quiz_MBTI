using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class MBTIQuestionController : QuestionController
{
    private int E = 0, I = 0, S = 0, N = 0, T = 0, F = 0, J = 0, P = 0;

    [SerializeField] MBTIQuestionsList MBTIquestionList;

    private MBTIQuestion currQuestion;
    private List<MBTIResult> results;

    private void Awake()
    {
        results = new List<MBTIResult>();
    }
    public override void DisplayRandomQuestion()
    {
        currQuestion = GetRandomQuestion();
        questionPanel.DisplayQuestion(currQuestion);
    }
    public MBTIQuestion GetRandomQuestion()
    {
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
        string s = "";
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
        if (MBTIquestionList.questions.Count == 0)
        {
            string mbti = GetMBTIString();
            Result rs = new Result();
            rs.result = mbti;
            rs.recordDetails = new List<ResultDetail>();
            for(int i = 0; i < results.Count; i++)
            {
                ResultDetail dt = new ResultDetail();
                dt.mbtI_ExamQuestionId = results[i].idQues;
                dt.userChoice = results[i].answer;
                rs.recordDetails.Add(dt);
            }
            string json = JsonConvert.SerializeObject(rs);
            DBRequestManager.Instance.DataSendRequestWithToken(APIUrls.postRecord, json, PlayerPrefs.GetString("usertoken"), (s) =>
            {
                Debug.Log(json);
                Debug.Log(s);
            });
            GameManager.Instance.GameVictory();
            questionPanel.HidePanel();
            return;

        }
        else
        {
            if (numQuesAnswered < numQues)
            {

                MBTIResult mbti = new MBTIResult();
                mbti.idQues = currQuestion.id.ToString();
                mbti.nameQues = currQuestion.nameQuestion;
                mbti.answer = result == 0 ? currQuestion.nameAns1 : currQuestion.nameAns2;
                results.Add(mbti);

                DisplayRandomQuestion();
            }
            else
            {
                GameManager.Instance.ChangeState(GameState.Playing);
                questionPanel.HidePanel();
            }
        }

    }

}

public class Result
{
    public string result = "";
    public List<ResultDetail> recordDetails;
}

public class ResultDetail
{
    public string mbtI_ExamQuestionId = "";
    public string userChoice = "";
}