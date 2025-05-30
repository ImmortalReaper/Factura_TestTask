using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IObjectPoolService
{
    public GameObject Get(GameObject prefab);
    public T Get<T>(T prefab) where T : Component;
    public GameObject GetTimed(GameObject prefab, float timeInSeconds);
    public T GetTimed<T>(T prefab, float timeInSeconds) where T : Component;
    public void Release(GameObject instance);
    public void ReleaseTimed(GameObject instance);
    public void ReleaseTimed<T>(T component) where T : Component;
    public void Release<T>(T component) where T : Component;
}
