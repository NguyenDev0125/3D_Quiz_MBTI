using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestionLoader : MonoBehaviour
{
    public MBTIQuestionsList MBTIquestionList;
    public ReviewQuestionList ReviewQuestionList;
    public void LoadMBTIQuestions()
    {

        MBTIquestionList.questions = new List<MBTIQuestion>();
        DbRequestManager.Instance.DataGetRequestWithToken(APIUrls.getMBTIExam,PlayerPrefs.GetString("usertoken"), s =>
        {
            Debug.Log("MBITI "+s);
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

    public void LoadReviewExam(Action<Respone> callback)
    {
        DbRequestManager.Instance.DataGetRequestWithToken(APIUrls.getExaminationsApi, PlayerPrefs.GetString("usertoken"), (json) =>
        {
            Debug.Log("Review " + json);
            Respone root = JsonConvert.DeserializeObject<Respone>(json);
            callback.Invoke(root);
            //ReviewQuestionList.questions = new List<ReviewQuestionContent>();
            //Debug.Log(root.result.items.Count);
            //ReviewQuestionList.examId = root.result.items[0].id;
            //Debug.Log(ReviewQuestionList.examId);
            //List<Exam> items = root.result.items;
            //foreach (var item in items)
            //{
            //    foreach (var examQuestion in item.examinationQuestions)
            //    {       
            //        ReviewQuestionContent ques = JsonConvert.DeserializeObject<ReviewQuestionContent>(examQuestion.question.content);
            //        ques.examinationQuestionId = examQuestion.id;
            //        ReviewQuestionList.questions.Add(ques);
            //    }
            //}
        });
    }

    public void SaveExam(Exam ex)
    {
        ReviewQuestionList.examId = ex.id;
        foreach (var examQuestion in ex.examinationQuestions)
        {
            ReviewQuestionContent ques = JsonConvert.DeserializeObject<ReviewQuestionContent>(examQuestion.question.content);
            ques.examinationQuestionId = examQuestion.id;
            ReviewQuestionList.questions.Add(ques);
        }
    }
}
