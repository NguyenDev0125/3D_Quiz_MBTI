using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] Button backHomeBtn;
    [SerializeField] Button backMenuBtn;
    ConfirmPanel confirmPanel;
    private void Awake()
    {
        backHomeBtn.onClick.AddListener(BackHome);
        backMenuBtn.onClick.AddListener(BackMenu);
        confirmPanel = FindAnyObjectByType<ConfirmPanel>();
    }

    private void BackHome()
    {
        confirmPanel.OnResult = (result) =>
        {
            if(result == ConfirmResult.OK)
            {
                PlayerPrefs.SetInt("mt", 0);
                SceneManager.LoadScene("Home");
            }
        };

    }

    private void BackMenu()
    {
        confirmPanel.OnResult = (result) =>
        {
            if (result == ConfirmResult.OK)
            {
                PlayerPrefs.SetInt("mt", 1);
                SceneManager.LoadScene("Home");
            }
        };
    }
}
