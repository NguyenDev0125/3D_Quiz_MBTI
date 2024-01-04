using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryListItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText, scoreText, resultText , dateText;

    public void SetItem(string name , string score , string result , string date)
    {
        nameText.text = name;
        scoreText.text = score;
        resultText.text = result;
        dateText.text = date;
    }
}
