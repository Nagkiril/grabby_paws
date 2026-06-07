using System;
using UnityEngine;

namespace GP.Environment.Pickups
{
    public class PickupActor : PickupComponent
    {
        public event Action<PickupTreasure> OnTreasurePickup;

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PickupTreasure>(out var pickedTreasure))
            {
                OnTreasurePickup?.Invoke(pickedTreasure);
            }
        }
    }
}
