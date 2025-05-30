using System;
using UnityEngine;

namespace ShootingCar.Feature.TurretModule
{
    [Serializable]
    public class TurretData
    {
        public TurretType TurretType;
        public GameObject TurretPrefab;
        public float TurretFireRateInSec;
    }
}
