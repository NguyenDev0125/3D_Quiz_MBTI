using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTxt, scoreTxt;
    public void SetItem(string name , int score)
    {
        nameTxt.text = name;
        scoreTxt.text = score.ToString();
    }
}
