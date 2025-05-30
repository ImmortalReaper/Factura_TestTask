using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace ShootingCar.Core.SceneLoader
{
    public interface IAddressablesSceneLoaderService
    {
        int LoadedScenesCount { get; }
        public UniTask LoadAsync(string sceneId, bool clearOthers);
        public UniTask LoadMultipleAsync(List<string> sceneIds, bool clearOthers);
        public UniTask ReleaseSceneAsync(string sceneId);
        public UniTask ReleaseScenesAsync(List<string> sceneIds);
    }
}
