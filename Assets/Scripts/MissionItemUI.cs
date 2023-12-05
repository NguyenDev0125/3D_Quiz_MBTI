using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionItemUI : MonoBehaviour
{
    public Sprite lockSpr;
    public Sprite unLockSpr;

    public Image icon;
    public TextMeshProUGUI text;
    public Button completeBtn;

    public void Init(int id,bool isUnlocked , string txt , Action<int> callback)
    {
        icon.sprite = isUnlocked ? unLockSpr : lockSpr;
        text.text = txt;
        completeBtn.onClick.AddListener(() => callback(id));

    }

    public void Unlock()
    {
        icon.sprite = unLockSpr;
        completeBtn.gameObject.SetActive(false);
    }
}
