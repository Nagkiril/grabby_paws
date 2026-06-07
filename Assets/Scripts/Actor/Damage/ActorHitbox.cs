using GP.Damage;
using UnityEngine;

namespace GP.Actors.Damage
{
    public class ActorHitbox : MonoBehaviour, IHitbox
    {
        public void Hit()
        {
            Debug.Log("An Actor has been hit!");
        }
    }
}