using DG.Tweening;
using Feature.UI;
using UnityEngine;

public class WinUI : BaseUIWindow
{
    [SerializeField] private CanvasGroup canvasGroup;

    public override void Show()
    {
        base.Show();
        canvasGroup.DOFade(1f, 0.5f).SetEase(Ease.InOutQuart);
    }

    public override void Hide()
    {
        base.Hide();
        canvasGroup.DOFade(0f, 0.5f).SetEase(Ease.InOutQuart);
    }
}
