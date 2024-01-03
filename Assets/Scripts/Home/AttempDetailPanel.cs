using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttempDetailPanel : MonoBehaviour
{
    [SerializeField] AnswerDetailItem itemPrb;
    [SerializeField] ScrollRect listView;
    [SerializeField] Button closeBtn;
    private void Awake()
    {
        closeBtn.onClick.AddListener(() =>
        {
            foreach(Transform t in listView.content)
            {
                t.gameObject.SetActive(false);
            }
            this.gameObject.SetActive(false);    
        });
    }
    public void Open(List<E2> listE2)
    {
        this.gameObject.SetActive(true);
        for(int i = 0; i < listE2.Count; i++)
        {
            E3 e3 = listE2[i].examinationQuestion;
            E4 e4 = e3.question;
            E5 e5 = JsonConvert.DeserializeObject<E5>(e4.content);
            List<E6> listAnswer = e5.ListAnswer;
            bool isTrue = e3.isCorrect;
            AnswerDetailItem clone = Instantiate(itemPrb,listView.content);

            if(isTrue)
            {
                string userAnswer = e3.userAnswered;
                int trueAnsIndex = 0;
                for(int  j = 0; j < listAnswer.Count; j++)
                {
                    if (listAnswer[j].Value == userAnswer)
                    {
                        trueAnsIndex = j;
                        break;
                    }
                }
                clone.SetItem(e5.Question, listAnswer[0].Value, listAnswer[1].Value, listAnswer[2].Value , trueAnsIndex);
            }
            else
            {
                string userAnswer = e3.userAnswered;
                int falseIndex = 0;
                int trueIndex = 0;
                for (int j = 0; j < listAnswer.Count; j++)
                {
                    if (listAnswer[j].Value == userAnswer)
                    {
                        falseIndex = j;
                    }else if (listAnswer[j].IsAnswer )
                    {
                        trueIndex = j;
                    }
                }
                clone.SetItem(e5.Question, listAnswer[0].Value, listAnswer[1].Value, listAnswer[2].Value, trueIndex,falseIndex);
            }

        }
    }
}
