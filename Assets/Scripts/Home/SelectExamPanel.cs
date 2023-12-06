using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectExamPanel : MonoBehaviour
{
    [SerializeField] QuestionLoader questionLoader;
    [SerializeField] ScrollRect scrollview;
    [SerializeField] ExamItemUI examUIPrb;

    List<Exam> listExams;
    
    public void Display()
    {
        questionLoader.LoadReviewExam((respone) =>
        {
            listExams = respone.result.items;
            for(int i = 0; i < listExams.Count;i++)
            {
                Exam ex = listExams[i];
                ExamItemUI item = Instantiate(examUIPrb);
                item.SetItem(ex.id, ex.name, ex.description, SelectExam);
                item.transform.SetParent(scrollview.content,false);
            }
        });
        gameObject.SetActive(true);
    }

    private void SelectExam(string id)
    {
        foreach(Exam ex in listExams)
        {
            if(ex.id == id)
            questionLoader.SaveExam(ex);
        }
        FindObjectOfType<HomeUIController>().OpenLoadingPanel();
        PlayerPrefs.SetInt("gamemode", 1);
        SceneManager.LoadScene("GamePlay");
    }
}
