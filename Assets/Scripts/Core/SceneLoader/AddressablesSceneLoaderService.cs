using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace ShootingCar.Core.SceneLoader
{
    public class AddressablesSceneLoaderService : IAddressablesSceneLoaderService
    {
        private Dictionary<string, AsyncOperationHandle<SceneInstance>> _activeScenes { get; } = new();

        public int LoadedScenesCount => _activeScenes.Count;

        public async UniTask LoadAsync(string sceneId, bool clearOthers) {
            if (_activeScenes.ContainsKey(sceneId))
                return;

            var mode = clearOthers ? LoadSceneMode.Single : LoadSceneMode.Additive;
            var handle = Addressables.LoadSceneAsync(sceneId, mode);

            _activeScenes[sceneId] = handle;
            await handle.Task;
        }

        public async UniTask LoadMultipleAsync(List<string> sceneIds, bool clearOthers) {
            if (clearOthers) {
                var redundantScenes = _activeScenes.Keys.Except(sceneIds).ToList();
                await ReleaseScenesAsync(redundantScenes);
            }

            foreach (var id in sceneIds)
                await LoadAsync(id, false);
        }

        public async UniTask ReleaseSceneAsync(string sceneId) {
            if (!_activeScenes.TryGetValue(sceneId, out var handle)) {
                Debug.LogWarning($"Scene '{sceneId}' not found in loaded scenes.");
                return;
            }

            var unloadHandle = Addressables.UnloadSceneAsync(handle);
            await unloadHandle.Task;
            _activeScenes.Remove(sceneId);
        }

        public async UniTask ReleaseScenesAsync(List<string> sceneIds) {
            foreach (var id in sceneIds)
                await ReleaseSceneAsync(id);
        }
    }
}
