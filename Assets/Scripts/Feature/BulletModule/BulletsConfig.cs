using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(BulletsConfig), menuName = "Configurations/Bullet Config/" + nameof(BulletsConfig))]
public class BulletsConfig : ScriptableObject
{
    public List<BulletData> bulletsConfig;
    public BulletData DeafultConfig;
    
    public BulletData GetBulletConfig(BulletType bulletType) => 
        bulletsConfig.FirstOrDefault(b => b.BulletType == bulletType) ?? DeafultConfig;
}
