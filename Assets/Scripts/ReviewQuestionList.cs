using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "QuestionsListSO" , menuName = "new QuestionsListSO")]
public class ReviewQuestionList : ScriptableObject
{
    public string examId;
    public List<ReviewQuestionContent> questions;
}
