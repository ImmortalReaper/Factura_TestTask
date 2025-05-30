using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ShootingCar.Feature.EnemyAIModule
{
    public class FlashEffect : MonoBehaviour
    {
        [SerializeField] private Renderer targetRenderer;
        [SerializeField] private float flashDuration = 0.1f;

        private static readonly int FlashEffectId = Shader.PropertyToID("_Flash_Effect");
        private MaterialPropertyBlock _propBlock;

        private void Awake()
        {
            _propBlock = new MaterialPropertyBlock();
            targetRenderer.GetPropertyBlock(_propBlock);
        }

        public void PlayFlashEffect() => PlayFlashEffectAsync().Forget();
    
        private async UniTask PlayFlashEffectAsync()
        {
            _propBlock.SetFloat(FlashEffectId, 1f);
            targetRenderer.SetPropertyBlock(_propBlock);
            await UniTask.Delay(TimeSpan.FromSeconds(flashDuration), DelayType.DeltaTime);
            _propBlock.SetFloat(FlashEffectId, 0f);
            targetRenderer.SetPropertyBlock(_propBlock);
        }
    }
}
