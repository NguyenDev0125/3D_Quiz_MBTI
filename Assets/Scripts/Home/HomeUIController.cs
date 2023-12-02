using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUIController : MonoBehaviour
{
    public GameObject homeMenu;
    public GameObject selectQuesMenu;
    public LoginPanel loginPanel;
    public GameObject loadingPanel;

    public Button startBtn;
    public Button settingBtn;
    public Button quitBtn;
    public Button openLoginBtn;
    [SerializeField] List<Button> listBtns;
    [SerializeField] Ease ease;

    private void Awake()
    {
        foreach (Button btn in listBtns)
        {
            btn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundName.ButtonClick);
                RectTransform rect = btn.GetComponent<RectTransform>();
                Vector3 normalScale = rect.localScale;
                rect.transform.localScale = rect.transform.localScale * 0.8f;
                rect.DOScale(normalScale, 0.3f).SetDelay(0.2f).SetEase(ease);
            });
        }
        startBtn.onClick.AddListener(OpenSelectQuesMenu);
        settingBtn.onClick.AddListener(OpenSettingMenu);
        quitBtn.onClick.AddListener(QuitGame);
        openLoginBtn.onClick.AddListener(OpenLoginMenu);
        int gameMode = PlayerPrefs.GetInt("mt", -1);
        if(PlayerPrefs.GetInt("mt",0) == 0)
        {
            selectQuesMenu.SetActive(false);
            
        }
        else if(gameMode == 1)
        {
            selectQuesMenu.SetActive(true);
            loginPanel.gameObject.SetActive(false);
            
        }
        PlayerPrefs.SetInt("mt", -1);


    }
    private void Start()
    {
        loginPanel.AutoLogin();
    }
    private void OpenSelectQuesMenu()
    {
        selectQuesMenu.SetActive(true);
        RectTransform rect = selectQuesMenu.GetComponent<RectTransform>();
        Vector3 normalScale = rect.localScale;
        rect.transform.localScale *= 0.8f;
        rect.DOScale(normalScale, 0.3f).SetEase(Ease.OutBounce);
        homeMenu.SetActive(false);
    }

    private void OpenSettingMenu()
    {
        SoundSettingPanel.Instance.Togle();
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void OpenLoginMenu()
    {
        loginPanel.gameObject.SetActive(true);
        loginPanel.transform.localScale *= 0.8f;
        loginPanel.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBounce);
    }

    public void OpenLoadingPanel()
    {
        loadingPanel.gameObject.SetActive(true);
    }

}
