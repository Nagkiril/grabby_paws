using GP.Environment.Pickups;
using UnityEngine;

namespace GP.Players
{
    public class PlayerPickup : MonoBehaviour
    {
        [SerializeField] PickupActor _pickupActor;

        void OnEnable()
        {
            if (_pickupActor != null)
            {
                _pickupActor.OnTreasurePickup += OnTreasurePickup;
            }
        }

        void OnDisable()
        {
            if (_pickupActor != null)
            {
                _pickupActor.OnTreasurePickup -= OnTreasurePickup;
            }
        }

        void OnTreasurePickup(PickupTreasure treasure)
        {
            if (treasure != null && !treasure.IsPickedUp)
            {
                Debug.Log("A treasure is picked up!");
                treasure.Pickup();
            }
        }
    }
}
