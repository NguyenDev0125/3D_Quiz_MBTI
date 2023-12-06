using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherManager : MonoBehaviour
{
    public List<Teacher> Teachers;
    public MBTIQuestionsList mbtiList;
    public ReviewQuestionList reviewQuestionList;

    int totalQues;
    private void Start()
    {

        totalQues = mbtiList.questions.Count != 0? mbtiList.questions.Count : reviewQuestionList.questions.Count;
        Debug.Log(totalQues);
        int avgNumQues = totalQues / Teachers.Count;
        for(int i = 0; i < Teachers.Count-1; i++)
        {
            Teachers[i].SetNumQues(avgNumQues);
        }
        Teachers[Teachers.Count-1].SetNumQues((totalQues - avgNumQues *  (Teachers.Count-1)));
    }
}
