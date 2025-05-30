using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public interface IAddressablesSceneLoaderService
{
    int LoadedScenesCount { get; }
    UniTask<SceneInstance> LoadSceneAsync(string address, LoadSceneMode loadMode = LoadSceneMode.Single);
    UniTask<SceneInstance> LoadSceneAdditiveAsync(string address);
    UniTask UnloadSceneAsync(string address);
    UniTask UnloadSceneAsync(SceneInstance sceneInstance);
    bool UnloadScene(string address);
    bool UnloadScene(SceneInstance sceneInstance);
    void UnloadAllScenes();
    bool IsSceneLoaded(string address);
    SceneInstance GetLoadedScene(string address);
}
