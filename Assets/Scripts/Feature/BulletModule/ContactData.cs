using UnityEngine;

namespace ShootingCar.Feature.BulletModule
{
    public class ContactData
    {
        public Vector3 Point { get; set; }
        public Vector3 Normal { get; set; }
    
        public ContactData(Vector3 point, Vector3 normal)
        {
            Point = point;
            Normal = normal;
        }
    }
}
