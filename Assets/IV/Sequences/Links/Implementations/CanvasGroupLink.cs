using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IV.Sequences.Links.Implementations
{
    public class CanvasGroupLink : DotweenSequenceLink
    {
        [SerializeField] private CanvasGroup _targetGroup;
        [SerializeField] private float _startAlpha;
        [SerializeField] private float _endAlpha;

        protected override Tween GetTransitionTween()
        {
            return DOTween.To(() => { return _targetGroup.alpha; }, (x) => { _targetGroup.alpha = x; }, _endAlpha, _duration);
        }

        protected override void SetTransitionStart()
        {
            _targetGroup.alpha = _startAlpha;
        }

    }
}