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
        if(PlayerPrefs.GetInt("gamemode", 0)  == 0)
        {
            totalQues = mbtiList.questions.Count;
        }
        else
        {
            totalQues = reviewQuestionList.questions.Count;
        }
        
        Debug.Log(totalQues);
        int avgNumQues = totalQues / Teachers.Count;
        for(int i = 0; i < Teachers.Count; i++)
        {
            Teachers[i].SetNumQues(avgNumQues);
        }
        Teachers[Teachers.Count-1].SetNumQues(avgNumQues + (totalQues - (Teachers.Count * avgNumQues)));
    }
}
