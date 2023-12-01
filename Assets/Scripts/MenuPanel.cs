using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] Button backHomeBtn;
    [SerializeField] Button backMenuBtn;
    private void Awake()
    {
        backHomeBtn.onClick.AddListener(BackHome);
        backMenuBtn.onClick.AddListener(BackMenu);
    }

    private void BackHome()
    {
        PlayerPrefs.SetInt("mt", 0);
        SceneManager.LoadScene("Home");
    }

    private void BackMenu()
    {
        PlayerPrefs.SetInt("mt", 1);
        SceneManager.LoadScene("Home");
    }
}
