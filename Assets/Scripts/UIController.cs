using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button OpenMenuBtn;
    public Button closeVictoryBtn;
    public Button settingBtn;
    public Button menuBtn;
    public Button closeMenuBtn;
    public GameObject questionList;
    public GameObject victoryPanel;
    public GameObject menu;
    public ReviewQuestionPanel reviewQuestionPanel;
    public MBTIQuestionPanel mbtiQuestionPanel;
    public Ease ease = Ease.OutBounce;
    [SerializeField] List<Button> listBtns;
    private void Awake()
    {
        OpenMenuBtn.onClick.AddListener(() =>
        {
            questionList.SetActive(!questionList.activeInHierarchy);
            RectTransform rect = questionList.GetComponent<RectTransform>();
            Vector3 normalScale = rect.localScale;
            rect.transform.localScale = rect.transform.localScale * 0.8f;
            rect.DOScale(normalScale, 0.1f).SetEase(ease);

        });
        settingBtn.onClick.AddListener(() =>
        {
            SoundSettingPanel.Instance.Togle();
        });
        closeVictoryBtn.onClick.AddListener(ShowVictory);
        menuBtn.onClick.AddListener(() => menu.SetActive(true));
        closeMenuBtn.onClick.AddListener(() => menu.SetActive(false));
        foreach(UnityEngine.UI.Button btn in listBtns)
        {
            btn.onClick.AddListener(() =>
            {
                // Sound manager null vi no khoi tao o home scene, nhung mk load thang game play nen sound manager chua dc khoi tao .
                SoundManager.Instance.PlaySound(SoundName.ButtonClick);
                RectTransform rect = btn.GetComponent<RectTransform>();
                Vector3 normalScale = rect.localScale;
                rect.transform.localScale = rect.transform.localScale * 0.8f;
                rect.DOScale(normalScale, 0.1f).SetEase(ease);
            });
        }

    }

    public void HideUI()
    {
        reviewQuestionPanel.gameObject.SetActive(false);
        mbtiQuestionPanel.gameObject.SetActive(false);
    }

    internal void ShowVictory()
    {
        RectTransform rect = victoryPanel.GetComponent<RectTransform>();
        if (!victoryPanel.activeInHierarchy)
        {
            victoryPanel.gameObject.SetActive(true);
            rect.transform.localScale *= 0.8f;
            rect.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBounce);
        }
        else
        {
            rect.DOScale(Vector3.one * 0.7f, 0.3f).OnComplete(() => victoryPanel.SetActive(false));
        }

    }
}
