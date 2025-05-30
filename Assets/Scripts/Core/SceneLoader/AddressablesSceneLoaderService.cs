using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class AddressablesSceneLoaderService : IAddressablesSceneLoaderService
{
    private readonly Dictionary<string, SceneInstance> _loadedScenes = new();
    private readonly Dictionary<string, AsyncOperationHandle<SceneInstance>> _loadingOperations = new();

    public int LoadedScenesCount => _loadedScenes.Count;

    public async UniTask<SceneInstance> LoadSceneAsync(string address, LoadSceneMode loadMode = LoadSceneMode.Single)
    {
        try
        {
            if (_loadingOperations.ContainsKey(address))
            {
                await _loadingOperations[address].Task;
                return _loadedScenes.TryGetValue(address, out var existingScene) ? existingScene : default;
            }
            
            if (_loadedScenes.TryGetValue(address, out var cachedScene))
            {
                Debug.LogWarning($"Scene '{address}' is already loaded.");
                return cachedScene;
            }

            var operation = Addressables.LoadSceneAsync(address, loadMode);
            _loadingOperations[address] = operation;
            
            await operation.Task;

            if (operation.Status == AsyncOperationStatus.Succeeded)
            {
                var sceneInstance = operation.Result;
                _loadedScenes[address] = sceneInstance;
                _loadingOperations.Remove(address);
                
                Debug.Log($"Successfully loaded scene: {address}");
                return sceneInstance;
            }
            else
            {
                Debug.LogError($"Failed to load scene at address: {address}. Error: {operation.OperationException?.Message}");
                _loadingOperations.Remove(address);
                return default;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error loading scene at address {address}: {ex.Message}");
            _loadingOperations.Remove(address);
            return default;
        }
    }

    public async UniTask<SceneInstance> LoadSceneAdditiveAsync(string address)
    {
        return await LoadSceneAsync(address, LoadSceneMode.Additive);
    }

    public async UniTask UnloadSceneAsync(string address)
    {
        try
        {
            if (!_loadedScenes.TryGetValue(address, out var sceneInstance))
            {
                Debug.LogWarning($"Scene '{address}' is not loaded or not found in cache.");
                return;
            }

            var operation = Addressables.UnloadSceneAsync(sceneInstance);
            await operation.Task;

            if (operation.Status == AsyncOperationStatus.Succeeded)
            {
                _loadedScenes.Remove(address);
                Debug.Log($"Successfully unloaded scene: {address}");
            }
            else
            {
                Debug.LogError($"Failed to unload scene: {address}. Error: {operation.OperationException?.Message}");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error unloading scene {address}: {ex.Message}");
        }
    }

    public async UniTask UnloadSceneAsync(SceneInstance sceneInstance)
    {
        try
        {
            string address = null;
            foreach (var pair in _loadedScenes)
            {
                if (pair.Value.Scene == sceneInstance.Scene)
                {
                    address = pair.Key;
                    break;
                }
            }

            if (address == null)
            {
                Debug.LogWarning("SceneInstance not found in loaded scenes cache.");
                return;
            }

            await UnloadSceneAsync(address);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error unloading scene instance: {ex.Message}");
        }
    }

    public bool UnloadScene(string address)
    {
        try
        {
            if (_loadedScenes.TryGetValue(address, out var sceneInstance))
            {
                var operation = Addressables.UnloadSceneAsync(sceneInstance);
                operation.WaitForCompletion();
                
                if (operation.Status == AsyncOperationStatus.Succeeded)
                {
                    _loadedScenes.Remove(address);
                    Debug.Log($"Successfully unloaded scene: {address}");
                    return true;
                }
                else
                {
                    Debug.LogError($"Failed to unload scene: {address}");
                    return false;
                }
            }
            
            Debug.LogWarning($"Scene '{address}' is not loaded.");
            return false;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error unloading scene {address}: {ex.Message}");
            return false;
        }
    }

    public bool UnloadScene(SceneInstance sceneInstance)
    {
        try
        {
            string address = null;
            foreach (var pair in _loadedScenes)
            {
                if (pair.Value.Scene == sceneInstance.Scene)
                {
                    address = pair.Key;
                    break;
                }
            }

            if (address != null)
            {
                return UnloadScene(address);
            }
            
            Debug.LogWarning("SceneInstance not found in loaded scenes cache.");
            return false;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error unloading scene instance: {ex.Message}");
            return false;
        }
    }

    public void UnloadAllScenes()
    {
        int count = _loadedScenes.Count;
        var addressesToUnload = new List<string>(_loadedScenes.Keys);
        
        foreach (var address in addressesToUnload)
        {
            UnloadScene(address);
        }
        
        _loadedScenes.Clear();
        _loadingOperations.Clear();
        
        Debug.Log($"Unloaded {count} scenes.");
    }

    public bool IsSceneLoaded(string address)
    {
        return _loadedScenes.ContainsKey(address);
    }

    public SceneInstance GetLoadedScene(string address)
    {
        return _loadedScenes.TryGetValue(address, out var sceneInstance) ? sceneInstance : default;
    }
}
