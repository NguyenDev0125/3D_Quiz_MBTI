using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "QuestionListSO" , menuName = "new QuestionList")]
public class QuestionList : ScriptableObject
{
    public List<Question> questions;
}
