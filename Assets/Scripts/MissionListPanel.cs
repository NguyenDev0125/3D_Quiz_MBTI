using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MissionListPanel : MonoBehaviour
{
    public ReviewQuestionController controller;
    public MissionItemUI itemPrb;
    public ScrollRect scrollView;
    public List<string> listMission;
    public List<MissionItemUI> list;

    private void Awake()
    {
        list = new List<MissionItemUI> ();
        for(int i = 0; i < listMission.Count; i++)
        {
            MissionItemUI clone = Instantiate<MissionItemUI>(itemPrb, scrollView.content);
            clone.Init(false , listMission[i]);
            list.Add(clone);
        }
    }
    int currMisstionIdx = 0;
    public void UnLockMission()
    {
        list[currMisstionIdx++].Unlock();
    }
}