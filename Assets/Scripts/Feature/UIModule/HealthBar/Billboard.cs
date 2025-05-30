using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private bool freezeXRotation = false;
    [SerializeField] private bool freezeZRotation = false;
    [SerializeField] private bool reverseDirection = false;

    private Vector3 directionToCamera;
    private Camera _camera;
    
    void Start() => _camera = Camera.main;
    
    void LateUpdate()
    {
        directionToCamera = _camera.transform.position - transform.position;
        
        if (reverseDirection)
            directionToCamera = -directionToCamera;
        
        if (freezeXRotation)
            directionToCamera.x = 0f;
        if (freezeZRotation)
            directionToCamera.z = 0f;
        
        if (directionToCamera != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(directionToCamera);
        }
    }
}
