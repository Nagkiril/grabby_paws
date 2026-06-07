using GP.Environment.Pickups;
using UnityEngine;

namespace GP.Environment.Levels.Slots
{
    public class LevelPrefabSpawnSlot : LevelSlot
    {
        [SerializeField] string _treasurePrefabKey;

        GameObject _spawnedObject;

        public override void Populate(LevelGenerationContext context)
        {
            if (_spawnedObject == null)
            {
                //TODO
                //We could also create a service of sorts that will be actually loading all level generation assets
                //If we do, we could both utilize innate caching (like here) during generation, but also release assets immediately after generation (or wherever we choose).
                //I'll consider adding this later.
                Get<GameObject>(_treasurePrefabKey, (prefab) =>
                {
                    _spawnedObject = Instantiate(prefab, transform);
                });
            }
        }
    }
}