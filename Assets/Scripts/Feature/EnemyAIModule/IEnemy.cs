using ShootingCar.Feature.EnemyAIModule.Config;
using UnityEngine;

namespace ShootingCar.Feature.EnemyAIModule
{
    public interface IEnemy
    {
        public EnemyHealth EnemyHealth { get; }
        public Animator StickmanAnimator { get; }
        public EnemyData EnemyData { get; }
        public void SetDefaultState();
        public void SpawnBlood(Vector3 position, Vector3 normal);
    }
}
