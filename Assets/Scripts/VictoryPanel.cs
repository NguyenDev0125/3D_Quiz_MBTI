using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resultTxt;
    [SerializeField] ScrollRect scroll;
    [SerializeField] VictoryListItem itemPrb;
    [SerializeField] Button closeBtn;
    private void Awake()
    {
        closeBtn.onClick.AddListener(ClosePanel);
    }
    public void ShowResult(string result)
    {
        resultTxt.text = result;
        this.gameObject.SetActive(true);
        scroll.gameObject.SetActive(false);
    }

    public void ShowListResult(List<R3> listResults)
    {
        foreach (R3 r in listResults)
        {
            VictoryListItem obj = Instantiate(itemPrb , scroll.content);
            obj.SetItem(r.name, r.score.ToString(), r.result);
        }
        resultTxt.gameObject.SetActive(false);
        this.gameObject.SetActive(true);
    }
    private void ClosePanel()
    {
        this.gameObject.SetActive(false);
    }
}
