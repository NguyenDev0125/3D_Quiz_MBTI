using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUIController : MonoBehaviour
{
    public GameObject homeMenu;
    public GameObject selectQuesMenu;
    public LoginPanel loginPanel;

    public Button startBtn;
    public Button settingBtn;
    public Button quitBtn;


    private void Awake()
    {
        startBtn.onClick.AddListener(OpenSelectQuesMenu);
        settingBtn.onClick.AddListener(OpenSettingMenu);
    }
    private void Start()
    {
        loginPanel.AutoLogin();
    }
    private void OpenSelectQuesMenu()
    {
        selectQuesMenu.SetActive(true);
        homeMenu.SetActive(false);
    }

    private void OpenSettingMenu()
    {
        // TODO : Setting
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
