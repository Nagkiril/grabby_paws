using System;
using UnityEngine;

namespace GP.Environment.Pickups
{
    [Serializable]
    public class PickupAttractorProfile
    {
        public float AttractionSpeed = 5f;
        public float BaseAttractionDistance = 3f;
        public float DeltaAttractionDistance = 1f;
        public float NearProximityMultiplier = 1.5f;
        public float FarProximityMultiplier = 0.75f;

        //We're using squared distances for optimization, but I suspect the performance payout would be minimal if at all visible.
        //Could also just use magnitudes directly for much simpler, but less performant code.
        float _baseAttractionDistanceSqr;
        float _nearThresholdDistanceSqr;
        float _farThresholdDistanceSqr;

        public void Validate(UnityEngine.Object context = null)
        {
            if (DeltaAttractionDistance < 0f)
            {
                DeltaAttractionDistance = 0f;
                Debug.LogWarning("Delta Attraction Distance cannot be negative.", context);
            }

            if (BaseAttractionDistance < 0f)
            {
                BaseAttractionDistance = 0f;
                Debug.LogWarning("Base Attraction Distance cannot be negative.", context);
            }

            if (AttractionSpeed < 0f)
            {
                AttractionSpeed = 0f;
                Debug.LogWarning("Attraction Speed cannot be negative.", context);
            }

            Initialize();
        }

        public void Initialize()
        {
            var nearDistance = Mathf.Max(BaseAttractionDistance - DeltaAttractionDistance, 0f);
            var farDistance = BaseAttractionDistance + DeltaAttractionDistance;

            _baseAttractionDistanceSqr = BaseAttractionDistance * BaseAttractionDistance;
            _nearThresholdDistanceSqr = nearDistance * nearDistance;
            _farThresholdDistanceSqr = farDistance * farDistance;
        }

        public float GetProximityMultiplier(float distanceSqr)
        {
            var deltaDistance = Mathf.Max(DeltaAttractionDistance, 0f);

            if (deltaDistance <= 0f)
            {
                if (distanceSqr > _baseAttractionDistanceSqr)
                {
                    return FarProximityMultiplier;
                }

                if (distanceSqr < _baseAttractionDistanceSqr)
                {
                    return NearProximityMultiplier;
                }

                return 1f;
            }

            if (distanceSqr > _baseAttractionDistanceSqr)
            {
                var denominator = Mathf.Max(_farThresholdDistanceSqr - _baseAttractionDistanceSqr, Mathf.Epsilon);
                var interpolation = Mathf.Clamp01((distanceSqr - _baseAttractionDistanceSqr) / denominator);
                return Mathf.Lerp(1f, FarProximityMultiplier, interpolation);
            }

            if (distanceSqr < _baseAttractionDistanceSqr)
            {
                var denominator = Mathf.Max(_baseAttractionDistanceSqr - _nearThresholdDistanceSqr, Mathf.Epsilon);
                var interpolation = Mathf.Clamp01((_baseAttractionDistanceSqr - distanceSqr) / denominator);
                return Mathf.Lerp(1f, NearProximityMultiplier, interpolation);
            }

            return 1f;
        }
    }
}
