using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectQuestionMenu : MonoBehaviour
{
    public Button mbtiBtn;
    public Button reviewBtn;
    public string url;
    public QuestionList questionList;

    private void Awake()
    {
        mbtiBtn.onClick.AddListener(LoadQuestionFromLocal);
        reviewBtn.onClick.AddListener(LoadQuestionFormUrl);
    }
    private void LoadQuestionFromLocal()
    {
        TextAsset quesTxt = Resources.Load<TextAsset>("CauHoi");
        TextAsset ansTxt = Resources.Load<TextAsset>("DapAn");
        QuestionStruct[] arrQuesStruct = JsonConvert.DeserializeObject<QuestionStruct[]>(quesTxt.text);
        AnswerStruct[] arrAnswerStruct = JsonConvert.DeserializeObject<AnswerStruct[]>(ansTxt.text);
        for (int i = 0; i < arrQuesStruct.Length; i += 2)
        {
            Question newQues = new Question();
            newQues.IDQues = arrQuesStruct[i].IDQues;
            newQues.NameQues = arrQuesStruct[i].NameQues;
            newQues.Ans1 = arrQuesStruct[i].NameAns;
            newQues.Ans2 = arrQuesStruct[i + 1].NameAns;
            newQues.Group = arrQuesStruct[i].Group;
            newQues.TrueAnswer = (arrQuesStruct[i].IDAns == arrAnswerStruct[i / 2].IDAns) ? 0 : 1;
            questionList.questions.Add(newQues);
        }
        SceneManager.LoadScene("GamePlay");
    }
    
    private void LoadQuestionFormUrl()
    {
        DbRequestManager.Instance.DataGetRequest(url, (json) =>
        {
            questionList.questions = JsonConvert.DeserializeObject<List<Question>>(json);
            SceneManager.LoadScene("GamePlay");
        });
    }


}
