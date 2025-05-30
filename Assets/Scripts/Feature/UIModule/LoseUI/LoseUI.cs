using DG.Tweening;
using UnityEngine;

namespace ShootingCar.Feature.UIModule.LoseUI
{
    public class LoseUI : BaseUIWindow
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
}
