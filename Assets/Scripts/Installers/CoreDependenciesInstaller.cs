using IV.AssetManagement;
using UnityEngine;
using Zenject;

namespace GP.DI
{
    public class CoreDependenciesInstaller : MonoInstaller
    {
        [SerializeField] private AssetProvider _assetProvider;

        public override void InstallBindings()
        {
            Container.Bind<AssetProvider>().To<AssetProvider>().FromInstance(_assetProvider);
        }
    }
}