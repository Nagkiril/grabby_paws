using UnityEngine;

namespace GP.Environment.Levels.Slots
{
    public class LevelSlotCompound : LevelSlot
    {
        [SerializeField] LevelSlot[] _spawnerSlots;

        public override void Populate(LevelGenerationContext context)
        {
            foreach (var slot in _spawnerSlots)
            {
                slot.Populate(context);
            }
        }
    }
}