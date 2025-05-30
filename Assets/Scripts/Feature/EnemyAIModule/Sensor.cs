using System;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    [SerializeField] Collider _collider;
    
    public event Action<Collider> OnTriggerSensorEnter;
    public event Action<Collider> OnTriggerSensorExit;
    public event Action<Collider> OnTriggerSensorStay;
    public event Action<Collision> OnCollisionSensorEnter;
    public event Action<Collision> OnCollisionSensorExit;
    public event Action<Collision> OnCollisionSensorStay;
    
    private void OnTriggerEnter(Collider other) => OnTriggerSensorEnter?.Invoke(other);
    private void OnTriggerStay(Collider other) => OnTriggerSensorStay?.Invoke(other);
    private void OnTriggerExit(Collider other) => OnTriggerSensorExit?.Invoke(other);
    private void OnCollisionEnter(Collision other) => OnCollisionSensorEnter?.Invoke(other);
    private void OnCollisionExit(Collision other) => OnCollisionSensorExit?.Invoke(other);
    private void OnCollisionStay(Collision other) => OnCollisionSensorStay?.Invoke(other);
}
