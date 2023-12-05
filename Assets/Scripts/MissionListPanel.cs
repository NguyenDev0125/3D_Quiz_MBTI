using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionListPanel : MonoBehaviour
{
    public ReviewQuestionController controller;
    public MissionItemUI itemPrb;
    public ScrollRect scrollView;
    public List<Misstion> listMission;
    public ArrowDirection arrowDirection;
    private List<MissionItemUI> listMisstionUI;

    private void Start()
    {
        listMisstionUI = new List<MissionItemUI>();
        for(int i = 0; i < listMission.Count; i++)
        {
            listMission[i].teacher.id = i;
            MissionItemUI clone = Instantiate<MissionItemUI>(itemPrb, scrollView.content);
            clone.Init(i,false , listMission[i].mission , CompleteMisstion);
            
            listMisstionUI.Add(clone);
        }
        Debug.Log("Total mission :"+listMisstionUI.Count);
    }
    int currMisstionIdx = 0;
    public void UnLockMission(int id)
    {
        for (int i = 0; i < listMission.Count; i++)
        {
            if (id == i)
            {
                listMisstionUI[id].Unlock();
                arrowDirection.Hide();
            }
        }
    }

    private void CompleteMisstion(int id)
    {
        for(int i = 0; i < listMission.Count; i++)
        {
            if(id == i)
            {
                arrowDirection.SetTarget(listMission[i].teacher.transform);
                this.gameObject.SetActive(false);
            }
        }
    }
}
[Serializable]
public class Misstion
{
    [SerializeField] public Teacher teacher;
    [SerializeField] public string mission;
}
