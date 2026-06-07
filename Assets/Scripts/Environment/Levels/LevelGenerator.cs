using Cysharp.Threading.Tasks;
using IV.AssetManagement;
using System.Linq;
using UnityEngine;
using Zenject;

namespace GP.Environment.Levels
{
    public class LevelGenerator : AssetUserMonoBehaviour
    {
        [SerializeField] bool _autoGenerateOnStart;
        [SerializeField] LevelHost _generatedLevelHost;
        [SerializeField] string _testStartTileKey;
        [SerializeField] string _testLoopTileKey;

        [Inject] LevelTile.Factory _tileFactory;

        void Start()
        {
            if (_autoGenerateOnStart)
            {
                Generate().Forget();
            }
        }

        TileEndPoint FindEmptyEndPoint()
        {
            var emptyEndpoints = _generatedLevelHost.GetEmptyTileEndpoints();
            if (emptyEndpoints?.Any() ?? false)
            {
                var randomIndex = Random.Range(0, emptyEndpoints.Count);
                return emptyEndpoints[randomIndex];
            }
            else 
            { 
                return null;
            }
        }

        string GetTilePrefab(LevelGenerationContext context)
        {
            return _testLoopTileKey;
        }

        async UniTask SpawnTilePrefab(LevelGenerationContext context, string prefabKey, TileEndPoint endPoint = null)
        {
            var spawnTask = new UniTaskCompletionSource();

            Get<GameObject>(prefabKey, (tileAsset) =>
            {
                var newTile = _tileFactory.Create(tileAsset.GetComponent<LevelTile>());
                _generatedLevelHost.PlaceTile(newTile, endPoint);
                newTile.Populate(context);
                spawnTask.TrySetResult();
            });

            await spawnTask.Task;
        }

        public async UniTaskVoid Generate()
        {
            var context = new LevelGenerationContext(1, 3, 2);
            var numberOfTiles = Random.Range(context.MinTiles, context.MaxTiles + 1);

            await SpawnTilePrefab(context, _testStartTileKey, null);

            for (var tileNumber = 1; tileNumber <= numberOfTiles; tileNumber++)
            {
                var endPoint = FindEmptyEndPoint();
                if (endPoint != null)
                {
                    var tileKey = GetTilePrefab(context);
                    if (tileKey != null)
                    {
                        await SpawnTilePrefab(context, tileKey, endPoint);
                    }
                    else
                    {
                        Debug.LogWarning("LevelGeneration failed: cannot find a fitting next tile prefab.");
                    }
                }
                else
                {
                    Debug.LogWarning("LevelGeneration failed: cannot find another connector.");
                }
            }
        }
    }
}
