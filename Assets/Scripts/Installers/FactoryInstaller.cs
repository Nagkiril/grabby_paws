using GP.Environment.Levels;
using IV.AssetManagement;
using UnityEngine;
using Zenject;

namespace GP.DI
{
    public class FactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelTile.Factory>().AsSingle();
        }
    }
}