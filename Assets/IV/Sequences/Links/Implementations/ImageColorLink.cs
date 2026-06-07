using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IV.Sequences.Links.Implementations
{
    public class ImageColorLink : DotweenSequenceLink
    {
        [SerializeField] private Image _targetImage;
        [SerializeField] private Color _startColor;
        [SerializeField] private Color _endColor;

        protected override Tween GetTransitionTween()
        {
            return _targetImage.DOColor(_endColor, _duration);
        }

        protected override void SetTransitionStart()
        {
            _targetImage.color = _startColor;
        }

    }
}