using IV.AssetManagement;
using UnityEngine;

namespace GP.Environment.Levels.Slots
{
    public abstract class LevelSlot : AssetUserMonoBehaviour
    {
        public abstract void Populate(LevelGenerationContext context);
    }
}