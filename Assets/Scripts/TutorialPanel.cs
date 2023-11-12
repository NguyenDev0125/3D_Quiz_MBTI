using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] Transform panel;
    [SerializeField] TextMeshProUGUI textUI;
    [SerializeField] Button skipBtn;
    [SerializeField] List<string> listText = new List<string>();
    [SerializeField] float startDelayTime;
    [SerializeField] float delayShowChar;

    Coroutine show;
    string currText;
    int textIndex = 0;

    private void Awake()
    {
        skipBtn.onClick.AddListener(Skip);
    }
    private void Start()
    {
        ShowPanel();
    }

    private void ShowPanel()
    {
        panel.transform.localScale = Vector3.zero;
        panel.gameObject.SetActive(true);
        panel.DOScale(Vector3.one, 1f).SetDelay(startDelayTime).SetEase(Ease.OutBounce).OnComplete(ShowNextText);
        
    }
    private void ShowNextText()
    {
        if(textIndex < listText.Count)
        {
            ShowText(listText[textIndex++]);
        }
        else
        {
            HidePanel();
        }

    }
    private void ShowText(string txt)
    {
        show = StartCoroutine(IE_DisplayString(txt));
    }

    private IEnumerator IE_DisplayString(string txt)
    {
        textUI.text = "";
        currText = txt;
        for(int i = 0; i < txt.Length; i++)
        {
            textUI.text += txt[i];
            yield return new WaitForSeconds(delayShowChar);
        }
    }

    private bool CheckShowComplete()
    {
        return textUI.text.Length == currText.Length;
    }

    private void SkipShow()
    {
        if(show != null)
        {
            StopCoroutine(show);
            show = null;
        }
        textUI.text = currText;
    }
    private void Skip()
    {
        if(CheckShowComplete())
        {
            ShowNextText();
        }
        else
        {
            SkipShow();
        }
    }

    private void HidePanel()
    {
        panel.DOScale(Vector3.one / 2, 0.3f).OnComplete(() => panel.gameObject.SetActive(false));
        SoundManager.Instance.PlaySound(SoundName.Slide);
    }
}
