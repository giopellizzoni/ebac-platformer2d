using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuButtonsManager : MonoBehaviour
{
    public List<GameObject> buttons;
    [Header("Animation")]
    public float duration = .5f;
    public float delay = .1f;
    public Ease ease = Ease.OutBack;

    private void Awake()
    {
        HideAllButtons();
        ShowButtons();
    }

    private void HideAllButtons()
    {
        foreach (var button in buttons)
        {
            button.transform.localScale = Vector3.zero;
            button.SetActive(false);
        }
    }

    private void ShowButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            var button = buttons[i];
            button.SetActive(true);
            button.transform.DOScale(1, duration).SetDelay(i * delay).SetEase(ease);
        }
    }


}
