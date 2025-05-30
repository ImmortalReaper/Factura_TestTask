using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(TurretConfig), menuName = "Configurations/Turret Config/" + nameof(TurretConfig))]
public class TurretConfig : ScriptableObject
{
    public LayerMask TargetLayer;
    public List<TurretData> TurretsConfig;
    public TurretData DefaultTurretConfig;

    public TurretData GetTurretConfig(TurretType turretType) => 
        TurretsConfig.FirstOrDefault(turretData => turretData.TurretType == turretType) ?? DefaultTurretConfig;
}
