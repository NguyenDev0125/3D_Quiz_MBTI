
using System.Collections.Generic;
using UnityEngine;

public class QuestionController : MonoBehaviour
{
    public QuestionPanel questionPanel;

    protected int numQues;
    protected int numQuesAnswered;
    public virtual void StartAnswering(int numQues)
    {
        Debug.Log("Start answers : " + numQues);
        this.numQues = numQues;
        numQuesAnswered = 0;
        DisplayRandomQuestion();
        GameManager.Instance.ChangeState(GameState.Answering);
        SoundManager.Instance.PlaySound(SoundName.Slide);
    }
    public virtual void DisplayRandomQuestion() { }
    public virtual void TakeResult(int result, int group) { }
    public virtual void TakeResult(int result) { }

}
