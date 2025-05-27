using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Object = UnityEngine.Object;

public class ObjectPoolSystem : IObjectPoolService, IDisposable
{
    private readonly DiContainer _container;
    private readonly Dictionary<GameObject, ObjectPool<GameObject>> _pools = new();
    private readonly GameObject _root;

    public ObjectPoolSystem(DiContainer container)
    {
        _container = container;
        _root = new GameObject("ObjectPoolSystemRoot");
        Object.DontDestroyOnLoad(_root);
    }

    public void Dispose()
    {
        Object.Destroy(_root);
        _pools.Clear();
    }
    
    public GameObject Get(GameObject prefab)
    {
        var go = GetInternal(prefab);
        return go;
    }
    
    public T Get<T>(T prefab) where T : Component
    {
        var go = GetInternal(prefab.gameObject);
        var component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }
    
    public void Release(GameObject instance)
    {
        if (!TryReleaseInternal(instance))
        {
            Object.Destroy(instance);
        }
    }
    
    public void Release<T>(T component) where T : Component
    {
        if (component == null || component.gameObject == null || !TryReleaseInternal(component.gameObject))
        {
            Object.Destroy(component.gameObject);
        }
    }

    private GameObject GetInternal(GameObject prefab)
    {
        if (!_pools.TryGetValue(prefab, out var pool))
        {
            var container = CreateContainer(prefab.name + "Pool");
            pool = CreatePool(prefab, container);
            _pools[prefab] = pool;
        }

        var instance = pool.Get();
        var pooledObj = instance.GetComponent<PooledObject>() ?? instance.AddComponent<PooledObject>();
        pooledObj.PrefabKey = prefab;
        return instance;
    }

    private bool TryReleaseInternal(GameObject instance)
    {
        var pooledObj = instance.GetComponent<PooledObject>();
        if (pooledObj == null || pooledObj.PrefabKey == null)
        {
            Debug.LogWarning("Releasing object not managed by ObjectPoolSystem.");
            return false;
        }

        if (_pools.TryGetValue(pooledObj.PrefabKey, out var pool))
        {
            pool.Release(instance);
            return true;
        }
        else
        {
            Debug.LogWarning($"No pool found for prefab: {pooledObj.PrefabKey.name}");
            return false;
        }
    }

    private Transform CreateContainer(string name)
    {
        var go = new GameObject(name);
        go.transform.SetParent(_root.transform);
        return go.transform;
    }

    private ObjectPool<GameObject> CreatePool(GameObject prefab, Transform parent)
    {
        return new ObjectPool<GameObject>(
            createFunc: () =>
            {
                var go = _container.InstantiatePrefab(prefab, parent);
                go.SetActive(false);
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
                Object.Destroy(obj);
            },
            collectionCheck: true,
            defaultCapacity: 10,
            maxSize: 100
        );
    }
}
