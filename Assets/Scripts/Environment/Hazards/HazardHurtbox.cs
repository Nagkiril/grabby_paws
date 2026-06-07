using GP.Actors.Damage;
using UnityEngine;

namespace GP.Environment.Hazards
{
    public class HazardHurtbox : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ActorHitbox>(out var hitbox))
            {
                hitbox.Hit();
            }
        }
    }
}