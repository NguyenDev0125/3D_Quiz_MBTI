using DG.Tweening;
using TMPro;
using UnityEngine;

public class QuestionPanel : MonoBehaviour
{
    public TextMeshProUGUI quesTxt;
    public AnswerButton A;
    public AnswerButton B;
    public AnswerButton C;
    public virtual void TakeResult(int result) { }
    public void HidePanel()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void DisplayQuestion(IQuestion ques)
    {
        RectTransform quesTextTransform = GetComponent<RectTransform>();
        quesTextTransform.localScale = Vector3.one * 0.8f;
        quesTextTransform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBounce).OnComplete(() =>
        {

        });
    }
}
