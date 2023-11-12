using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingPanel : MonoSingleton<SoundSettingPanel>
{
    [SerializeField] Image iconSound;
    [SerializeField] Image iconMusic;
    [SerializeField] Slider sound;
    [SerializeField] Slider music;
    [SerializeField] Button saveBtn;
    [SerializeField] GameObject panel;
    protected override void Awake()
    {
        base.Awake();
        sound.value = PlayerPrefs.GetFloat("sound", 1f);
        music.value = PlayerPrefs.GetFloat("music", 1f);
        sound.onValueChanged.AddListener((f) => SoundManager.Instance.SetVolume(f));
        music.onValueChanged.AddListener((f) => SoundManager.Instance.SetVolume(f,SoundType.Music));
        saveBtn.onClick.AddListener(Save);
    }
    public void Togle()
    {
        RectTransform rect = panel.GetComponent<RectTransform>();
        if (!panel.activeInHierarchy)
        {
            panel.SetActive(true);
            rect.transform.localScale *= 0.8f;
            rect.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBounce);
        }
        else
        {
            rect.DOScale(Vector3.one * 0.7f, 0.2f).SetEase(Ease.Linear).OnComplete(()=> panel.SetActive(false));
        }
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("sound", sound.value);
        PlayerPrefs.SetFloat("music", music.value);
        Togle();
    }
}
