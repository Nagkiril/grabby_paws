using IV.ListExtensions;
using System.Linq;
using UnityEngine;

namespace GP.Environment.Levels.Slots
{
    public class TileSlotsGenerator : MonoBehaviour
    {
        //I expect that Treasures would spawn using a separate logic presented in PopulateTreasures() method below
        //When I'll have ideas for a proper tile\treasure generation algorithm, I expect to not have this set listed here; even now it could be modulized into "SlotTreasuresGenerator" or something.
        //But current logic is utterly basic and TileSlotsGenerator doesn't suffer from bloat, so I'll keep it as is for the time being.
        [SerializeField] LevelSlot[] _treasureSlots;
        //Although technically a violation of YAGNI, I decided to leave this stub;
        //Special slots could provide self-contained logic for oneshot content like pickable quest items or even spawning story NPCs when they're available on a tile.
        //I'm most likely going to use private\protected implementation for those slots, and have them fetch required info via DI\context and communicate (when necessary) via context.
        [SerializeField] LevelSlot[] _specialSlots;

        public void Populate(LevelGenerationContext context)
        {
            PopulateTreasures(context);
            PopulateSpecials(context);
        }

        private void PopulateTreasures(LevelGenerationContext context)
        {
            if (_treasureSlots != null)
            {
                var unusedSlots = _treasureSlots.ToList();

                for (var i = 0; i < context.TreasurePerTile && unusedSlots.Count > 0; i++)
                {
                    var randomSlot = unusedSlots.GetRandom();
                    randomSlot.Populate(context);
                    unusedSlots.Remove(randomSlot);
                }
            }
        }

        private void PopulateSpecials(LevelGenerationContext context)
        {
            if (_specialSlots != null)
            {
                foreach (var slot in _specialSlots)
                {
                    slot.Populate(context);
                }
            }
        }
    }
}