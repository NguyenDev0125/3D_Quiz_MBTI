using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionsPush : MonoBehaviour
{
    public List<NewQuestionContentRequest> Questions;

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0,0,200,120) , "Click to push questions list"))
        {
            string json = JsonConvert.SerializeObject(Questions);
            DbRequestManager.Instance.DataSendRequest(ApiUrls.postQuestionApi , json);
        }
    }
    [Serializable]
    public class Answer
    {
        public string value;
        public bool isAnswer;
    }
    [Serializable]
    public class NewQuestionContentRequest
    {
        public string question;
        public List<Answer> listAnswer;
    }

}
