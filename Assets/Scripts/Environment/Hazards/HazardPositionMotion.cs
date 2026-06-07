using DG.Tweening;
using System;
using UnityEngine;

namespace GP.Environment.Hazards
{
    //For this, I would normally prefer to use either an in-house sequencer, FEEL package, or at least utilize my own sequencer (IV.Sequences, which often uses DOTween anyway).
    //But for this script only, I'll be assuming I can't do none of this, so I'll try to make it a self-contained script using DOTween directly, as an experiment.
    public class HazardPositionMotion : HazardSummitMotion
    {
        [SerializeField] PositionCurveSet _motionTrajectory;
        [SerializeField] Transform _motionTarget;
        [SerializeField] float _motionDuration;
        [SerializeField] float _motionDelay;
        [SerializeField] float _summitFromRatio;
        [SerializeField] float _summitUntilRatio;
        [SerializeField] bool _playOnStart;
        [SerializeField] bool _playInfinitely;

        private Sequence _motionSequence;

        void Start()
        {
            if (_playOnStart)
            {
                StartMotion();
            }
        }

        public void StartMotion()
        {
            _motionSequence?.Kill();
            _motionSequence = DOTween.Sequence();

            _motionSequence.AppendInterval(_motionDelay);
            _motionSequence.Append(_motionTarget.DOLocalMoveX(1, _motionDuration).SetEase(_motionTrajectory.PositionX));
            _motionSequence.Join(_motionTarget.DOLocalMoveY(1, _motionDuration).SetEase(_motionTrajectory.PositionY));
            _motionSequence.Join(_motionTarget.DOLocalMoveZ(1, _motionDuration).SetEase(_motionTrajectory.PositionZ));
            _motionSequence.InsertCallback(_motionDelay + _motionDuration * _summitFromRatio, () => { SetMotionSummit(true); });
            _motionSequence.InsertCallback(_motionDelay + _motionDuration * _summitUntilRatio, () => { SetMotionSummit(false); });

            if (_playInfinitely)
            {
                _motionSequence.SetLoops(-1);
            }
        }

        [Serializable]
        class PositionCurveSet
        {
            public AnimationCurve PositionX;
            public AnimationCurve PositionY;
            public AnimationCurve PositionZ;
        }
    }
}