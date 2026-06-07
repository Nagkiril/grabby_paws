using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IV.Sequences.Links.Implementations
{
    public class AnchorPositionLink : DotweenSequenceLink
    {
        [SerializeField] private RectTransform _targetRect;
        [SerializeField] private Vector2 _startPosition;
        [SerializeField] private Vector2 _endPosition;

        protected override Tween GetTransitionTween()
        {
            return _targetRect.DOAnchorPos(_endPosition, _duration);
        }

        protected override void SetTransitionStart()
        {
            _targetRect.anchoredPosition = _startPosition;
        }
    }
}