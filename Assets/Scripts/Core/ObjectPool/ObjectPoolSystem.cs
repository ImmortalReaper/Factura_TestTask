using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Object = UnityEngine.Object;

public class ObjectPoolSystem : IObjectPoolService, IDisposable
{
    private readonly DiContainer _container;
    private readonly Dictionary<GameObject, ObjectPool<GameObject>> _pools = new();
    private readonly Dictionary<GameObject, ObjectPool<GameObject>> _instanceToPool = new();
    private readonly Dictionary<GameObject, CancellationTokenSource> _timedObjects = new();
    private readonly GameObject _root;

    public ObjectPoolSystem(DiContainer container)
    {
        _container = container;
        _root = new GameObject("ObjectPoolSystemRoot");
    }

    public void Dispose()
    {
        Object.Destroy(_root);
        _pools.Clear();
        _instanceToPool.Clear();
    }

    public GameObject Get(GameObject prefab)
    {
        if (!_pools.TryGetValue(prefab, out var pool))
        {
            var parent = CreateContainer(prefab.name + "Pool");
            pool = CreatePool(prefab, parent);
            _pools[prefab] = pool;
        }

        return pool.Get();
    }

    public T Get<T>(T prefab) where T : Component
    {
        var go = Get(prefab.gameObject);
        var comp = go.GetComponent<T>();
        if (comp == null) comp = go.AddComponent<T>();
        return comp;
    }

    public GameObject GetTimed(GameObject prefab, float timeInSeconds)
    {
        var instance = Get(prefab);
        var cts = new CancellationTokenSource();
        _timedObjects[instance] = cts;
        ReleaseAfterDelay(instance, timeInSeconds, cts.Token).Forget();
        return instance;
    }

    public T GetTimed<T>(T prefab, float timeInSeconds) where T : Component
    {
        var instance = Get(prefab.gameObject);
        var comp = instance.GetComponent<T>();
        if (comp == null) comp = instance.AddComponent<T>();
        var cts = new CancellationTokenSource();
        _timedObjects[instance] = cts;
        ReleaseAfterDelay(instance, timeInSeconds, cts.Token).Forget();
        return comp;
    }

    private async UniTaskVoid ReleaseAfterDelay(GameObject instance, float delay, CancellationToken cancellationToken)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: cancellationToken);
        if (instance != null && _timedObjects.ContainsKey(instance))
            Release(instance);
    }

    public void ReleaseTimed(GameObject instance)
    {
        if (_timedObjects.TryGetValue(instance, out var cts))
        {
            cts?.Cancel();
            cts?.Dispose();
            if (_instanceToPool.TryGetValue(instance, out var pool))
            {
                pool.Release(instance);
            }
            else
            {
                Debug.LogWarning("ReleaseTimed: object not managed by ObjectPoolSystem.");
                Object.Destroy(instance);
            }
        }
        else
        {
            Debug.LogWarning("ReleaseTimed: no active timer found for this object.");
        }
    }

    public void ReleaseTimed<T>(T component) where T : Component
    {
        if (component == null || component.gameObject == null) return;
        ReleaseTimed(component.gameObject);
    }
    
    public void Release(GameObject instance)
    {
        if (_instanceToPool.TryGetValue(instance, out var pool))
        {
            pool.Release(instance);
        }
        else
        {
            Debug.LogWarning("Releasing object not managed by ObjectPoolSystem.");
            Object.Destroy(instance);
        }
    }

    public void Release<T>(T component) where T : Component
    {
        if (component == null || component.gameObject == null) return;
        Release(component.gameObject);
    }

    private Transform CreateContainer(string name)
    {
        var go = new GameObject(name);
        go.transform.SetParent(_root.transform);
        return go.transform;
    }

    private ObjectPool<GameObject> CreatePool(GameObject prefab, Transform parent)
    {
        ObjectPool<GameObject> pool = null;
        pool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                var go = _container.InstantiatePrefab(prefab, parent);
                go.SetActive(false);
                _instanceToPool[go] = pool;
                return go;
            },
            actionOnGet: obj =>
            {
                obj.transform.SetParent(parent);
                obj.SetActive(true);
            },
            actionOnRelease: obj =>
            {
                obj.SetActive(false);
                obj.transform.SetParent(parent);
            },
            actionOnDestroy: obj =>
            {
                _instanceToPool.Remove(obj);
                Object.Destroy(obj);
            },
            collectionCheck: true,
            defaultCapacity: 10,
            maxSize: 100
        );
        return pool;
    }
}
