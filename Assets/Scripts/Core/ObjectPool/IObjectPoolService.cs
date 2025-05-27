using UnityEngine;

public interface IObjectPoolService
{
    public GameObject Get(GameObject prefab);
    public T Get<T>(T prefab) where T : Component;
    public void Release(GameObject instance);
    public void Release<T>(T component) where T : Component;
}
