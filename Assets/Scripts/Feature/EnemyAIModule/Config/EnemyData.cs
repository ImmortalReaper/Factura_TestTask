using System;
using UnityEngine;

namespace ShootingCar.Feature.EnemyAIModule.Config
{
    [Serializable]
    public class EnemyData
    {
        public EnemyType EnemyType;
        public GameObject EnemyPrefab;
        public float EnemySpeed;
        public float EnemyMaxHealth;
        public float EnemyDamage;
    }
}
