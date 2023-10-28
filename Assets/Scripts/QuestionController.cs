using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestionController : MonoBehaviour
{
    [SerializeField] QuestionPanel questionPanel;
    [SerializeField] QuestionList questionList;

    public int E = 0, I = 0; 
    public int S = 0, N = 0; 
    public int T = 0, F = 0; 
    public int J = 0, P = 0; 
    [SerializeField] List<Question> listQuesNotAnswered;
    private Question currQuestion;

    private int numQues;
    private int numQuesAnswered;
    private List<Question> listAnswered;

    public List<Question> ListAnswered { get => listAnswered;  }
    public List<Question> ListQuesNotAnswered { get => listQuesNotAnswered; }
    private void Awake()
    {
        listAnswered = new List<Question>();
    }
    public void LoadQuestion()
    {
        listQuesNotAnswered = questionList.questions;
    }
    public void StartAnswering(int count)
    {
        numQues = count;
        numQuesAnswered = 0;
        DisplayRandomQuestion();
        GameManager.Instance.ChangeState(GameState.Answering);
    }
    public void DisplayRandomQuestion()
    {
        if(ListQuesNotAnswered.Count <= 0)
        {
            GameManager.Instance.GameVictory();
            questionPanel.HidePanel();
            questionPanel.DisplayVictoryPanel();
            Debug.Log(GetMBTIString());
        }
        if(numQuesAnswered < numQues)
        {
            Question question = GetRandomQuestion();
            currQuestion = question;
            questionPanel.DisplayQuestion(question);
        }
        else
        {
            GameManager.Instance.ChangeState(GameState.Playing);
            questionPanel.HidePanel();
        }
    }
    public virtual void TakeResult(int result, int group)
    {
        numQuesAnswered++;
        if (result == 2)
        {
            DisplayRandomQuestion();
            return;
        }
        else
        {
            listAnswered.Add(currQuestion);
            int trueAns = currQuestion.TrueAnswer;
            switch (group)
            {
                case 0: if (result == trueAns) E++; else  I++; break;
                case 1: if (result == trueAns) T++; else  F++; break;
                case 2: if (result == trueAns) S++; else  N++; break;
                case 3: if (result == trueAns) J++; else  P++; break;
            }
            DisplayRandomQuestion();
        }
    }

    public Question GetRandomQuestion()
    {
        if (ListQuesNotAnswered == null) return null;
        int randInt = Random.Range(0,ListQuesNotAnswered.Count);
        Question ques = ListQuesNotAnswered[randInt];
        ListQuesNotAnswered.RemoveAt(randInt);
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
[Serializable]
public class Question
{
    public int IDQues;
    public int TrueAnswer;
    public string NameQues;
    public string Ans1;
    public string Ans2;
    public int Group;
}


