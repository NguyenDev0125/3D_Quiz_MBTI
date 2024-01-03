using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerDetail : MonoBehaviour
{
    [SerializeField] Text aws;
    [SerializeField] Image statusImage;
    [SerializeField] Sprite normal, trueAws, falseAws;

    public void SetAnswer(string answer)
    {
        aws.text = answer;

    }

    public void SetType(int type)
    {
        if (type == 1)
        {
            statusImage.gameObject.SetActive(true);
            statusImage.sprite = normal;
        }
        else if (type == 2)
        {
            statusImage.sprite = trueAws;
            statusImage.gameObject.SetActive(true);
        }

    }
}
