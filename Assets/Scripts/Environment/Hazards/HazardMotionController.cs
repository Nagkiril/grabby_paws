using System;
using UnityEngine;

namespace GP.Environment.Hazards
{
    public class HazardMotionController : MonoBehaviour
    {
        [SerializeField] private HazardSummitMotion _motion;
        [SerializeField] private HazardHurtbox _hurtbox;

        private void Awake()
        {
            _motion.OnMotionSummit += ToggleHurtbox;
        }

        private void ToggleHurtbox(bool isActive)
        {
            _hurtbox.gameObject.SetActive(isActive);
        }
    }
}