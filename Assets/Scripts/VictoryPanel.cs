using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resultTxt;
    [SerializeField] ScrollRect scroll;
    [SerializeField] VictoryListItem itemPrb;
    public void ShowResult(string result)
    {
        resultTxt.text = result;
        this.gameObject.SetActive(true);
    }

    public void ShowListResult(List<R3> listResults)
    {
        foreach (R3 r in listResults)
        {
            VictoryListItem obj = Instantiate(itemPrb , scroll.content);
            obj.SetItem(r.name, r.score.ToString(), r.result);
        }
    }
}
