using UnityEngine;

namespace GP.Environment.Pickups
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class PickupComponent : MonoBehaviour
    {
        [SerializeField] protected Rigidbody Rigidbody;
        [SerializeField] protected Collider Collision;
        [SerializeField] protected bool ValidateAutomatically = true;

        public bool IsPickedUp { get; private set; }

        protected virtual void Awake()
        {

        }

        public virtual void Pickup()
        {
            IsPickedUp = true;
        }

        protected virtual void OnValidate()
        {
            if (ValidateAutomatically)
            {
                if (Collision == null)
                {
                    Collision = GetComponent<Collider>();
                    if (Collision == null)
                    {
                        Debug.LogWarning($"{gameObject.name} has no collider; please manually add a required shape to track this PickupComponent.", this);
                    }
                }

                if (Collision != null && !Collision.isTrigger)
                {
                    Collision.isTrigger = true;
                }

                if (Rigidbody == null)
                {
                    Rigidbody = GetComponent<Rigidbody>();
                    //This check should be unnecessary because of RequireComponent, but just in case...
                    if (Rigidbody != null)
                    {
                        Rigidbody.isKinematic = true;
                    }
                }
            }
        }
    }
}
