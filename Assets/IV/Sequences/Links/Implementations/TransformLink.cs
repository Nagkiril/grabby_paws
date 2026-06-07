using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IV.Sequences.Links.Implementations
{
    public class TransformLink : DotweenSequenceLink
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Vector3 _endPosition;
        [SerializeField] private Vector3 _startScale;
        [SerializeField] private Vector3 _endScale;

        private Sequence _transformSequence;

        protected override void Start()
        {
            base.Start();
            if (_target == null)
                _target = transform;
        }

        protected override Tween GetTransitionTween()
        {
            if (_transformSequence != null)
                _transformSequence.Kill();
            _transformSequence = DOTween.Sequence();
            if (_startPosition != _endPosition)
                _transformSequence.Insert(0, _target.DOLocalMove(_endPosition, _duration));
            if (_startScale != _endScale)
                _transformSequence.Insert(0, _target.DOScale(_endScale, _duration));
            return _transformSequence;
        }

        protected override void SetTransitionStart()
        {
            if (_startPosition != _endPosition)
                _target.position = _startPosition;
            if (_startScale != _endScale)
                _target.localScale = _startScale;
        }
    }
}