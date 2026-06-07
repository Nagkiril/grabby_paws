#if FEEL_RELEASE
using MoreMountains.Feedbacks;
#endif
using UnityEngine;

namespace GP.Environment.Pickups
{
    public class PickupTreasure : PickupComponent
    {
#if FEEL_RELEASE
        [SerializeField] private MMF_Player _disappearAnimation;
#endif

        protected override void Awake()
        {
            base.Awake();
#if FEEL_RELEASE
            _disappearAnimation.Events.OnComplete.AddListener(OnDisappearFinished);
#endif
        }

        private void OnDisappearFinished()
        {
            Destroy(gameObject);
        }

        public override void Pickup()
        {
            if (!IsPickedUp)
            {
#if FEEL_RELEASE
                _disappearAnimation.PlayFeedbacks();
#else
                OnDisappearFinished();
#endif
                base.Pickup();
            }
        }
    }
}
