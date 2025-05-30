using System;
using UnityEngine;

[Serializable]
public class EnemyData
{
    public EnemyType EnemyType;
    public GameObject EnemyPrefab;
    public float EnemySpeed;
    public float EnemyMaxHealth;
    public float EnemyDamage;
}
