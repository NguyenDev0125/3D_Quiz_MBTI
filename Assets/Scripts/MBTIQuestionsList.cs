using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MBTIQuestionsListSO" , menuName = "new MBTIQuestionsList")]

public class MBTIQuestionsList : ScriptableObject, IQuestionList
{
    public string questionListId;
    [SerializeField]public List<MBTIQuestion> questions;
}
