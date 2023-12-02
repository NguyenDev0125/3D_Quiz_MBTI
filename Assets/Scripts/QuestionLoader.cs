using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class QuestionLoader : MonoBehaviour
{
    public MBTIQuestionsList MBTIquestionList;
    public ReviewQuestionList ReviewQuestionList;
    public void LoadQuestionsFromLocal()
    {

        MBTIquestionList.questions = new List<MBTIQuestion>();
        DbRequestManager.Instance.DataGetRequestWithToken(APIUrls.getMBTIExam,PlayerPrefs.GetString("usertoken"), s =>
        {
            Debug.Log(s);
            MBTIExam[] exams = JsonConvert.DeserializeObject<MBTIExam[]>(s);
            MBTIquestionList.questionListId = exams[0].id;
            Debug.Log(exams.Length);
            Debug.Log(exams[0].mbtI_ExamQuestions.Count);

            for (int i = 0; i < exams[0].mbtI_ExamQuestions.Count; i++)
            {
                MBTIquestionList.questions.Add(exams[0].mbtI_ExamQuestions[i].mbtI_Question);
            }
        });

    }

    public void LoadQuestionFormAPI()
    {
        DbRequestManager.Instance.DataGetRequestWithToken(APIUrls.getExaminationsApi, PlayerPrefs.GetString("usertoken"), (json) =>
        {
            Respone root = JsonConvert.DeserializeObject<Respone>(json);
            ReviewQuestionList.questions = new List<ReviewQuestionContent>();
            ReviewQuestionList.examId = root.result.items[0].id;
            Debug.Log(ReviewQuestionList.examId);
            List<Exam> items = root.result.items;
            foreach (var item in items)
            {
                foreach (var examQuestion in item.examinationQuestions)
                {       
                    ReviewQuestionContent ques = JsonConvert.DeserializeObject<ReviewQuestionContent>(examQuestion.question.content);
                    ques.examinationQuestionId = examQuestion.id;
                    ReviewQuestionList.questions.Add(ques);
                }
            }
        });
    }
}
