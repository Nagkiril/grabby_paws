using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using IV.GizmoExtension;
using UnityEngine;

namespace GP.Environment.Pickups
{
    public class PickupAttractor : PickupComponent
    {
        [SerializeField] PickupAttractorProfile _profile = new();

        readonly List<PickupTreasure> _attractedTreasures = new();
        CancellationTokenSource _attractionCancellation;

        protected override void Awake()
        {
            base.Awake();

            _profile ??= new PickupAttractorProfile();
            _profile.Initialize();
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            _profile ??= new PickupAttractorProfile();
            _profile.Validate(this);
        }

        void OnEnable()
        {
            _attractionCancellation = new CancellationTokenSource();
            RunAttractionLoop(_attractionCancellation.Token).Forget();
        }

        void OnDisable()
        {
            _attractionCancellation?.Cancel();
            _attractionCancellation?.Dispose();
            _attractionCancellation = null;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PickupTreasure pickupTreasure) && !_attractedTreasures.Contains(pickupTreasure))
            {
                _attractedTreasures.Add(pickupTreasure);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PickupTreasure pickupTreasure) && _attractedTreasures.Contains(pickupTreasure))
            {
                _attractedTreasures.Remove(pickupTreasure);
            }
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            var center = transform.position;
            var nearRadius = _profile.BaseAttractionDistance - _profile.DeltaAttractionDistance;
            var baseRadius = _profile.BaseAttractionDistance;
            var farRadius = _profile.BaseAttractionDistance + _profile.DeltaAttractionDistance;

            GizmoHelper.DrawCircleXZ(center, nearRadius, Color.green);
            GizmoHelper.DrawCircleXZ(center, baseRadius, Color.yellow);
            GizmoHelper.DrawCircleXZ(center, farRadius, Color.cyan);
        }
#endif

        async UniTaskVoid RunAttractionLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                for (var index = _attractedTreasures.Count - 1; index >= 0; index--)
                {
                    var treasure = _attractedTreasures[index];
                    if (treasure != null)
                    {
                        var targetPosition = transform.position;
                        var squaredDistance = (transform.position - treasure.transform.position).sqrMagnitude;
                        var step = _profile.AttractionSpeed * _profile.GetProximityMultiplier(squaredDistance) * Time.fixedDeltaTime;
                        treasure.transform.position = Vector3.MoveTowards(treasure.transform.position, targetPosition, step);
                    }
                    else
                    {
                        _attractedTreasures.RemoveAt(index);
                        continue;
                    }
                }

                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken);
            }
        }
    }
}
