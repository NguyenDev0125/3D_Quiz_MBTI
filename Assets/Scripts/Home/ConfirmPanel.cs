
using System;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPanel : MonoBehaviour
{
    [SerializeField] Button confirm, cancel;
    public Action<ConfirmResult> OnResult;
    private void Awake()
    {
        confirm.onClick.AddListener(Confirm);
        cancel.onClick.AddListener(Cancel);
    }

    private void Confirm()
    {
        Display(false);
        OnResult?.Invoke(ConfirmResult.OK);
    }

    private void Cancel()
    {
        Display(false);
        OnResult?.Invoke(ConfirmResult.CANCEL);
    }

    public void Display(bool isDisplay = true)
    {
        this.gameObject.SetActive(isDisplay);
    }
}

public enum ConfirmResult
{
    OK,
    CANCEL
}
