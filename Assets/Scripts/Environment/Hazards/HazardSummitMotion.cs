using System;
using UnityEngine;

namespace GP.Environment.Hazards
{
    //Ideally, it should be an interface IHazardMotion or IHazardSummitMotion
    //However, as I expect all the implementations to be components, I make it an abstract class, inheriting from MonoBehaviour.
    //This way, we can use default Unity inspector without breaking it, while an interface would require some extra work.
    public abstract class HazardSummitMotion : MonoBehaviour
    {
        public event Action<bool> OnMotionSummit;

        protected void SetMotionSummit(bool isSummit)
        {
            OnMotionSummit?.Invoke(isSummit);
        }
    }
}