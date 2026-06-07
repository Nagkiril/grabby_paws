using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace IV.Sequences.Links.Implementations
{
    public class ColorPropertyLink : MaterialPropertyLink
    {
        [SerializeField] private Color _startColor;
        [SerializeField] private Color _endColor;

        protected override void SetTransitionStart()
        {
            GetTargetMaterial().SetColor(_propertyName, _startColor);
        }

        protected override Tween GetTransitionTween()
        {
            return GetTargetMaterial().DOColor(_endColor, _propertyName, _duration);
        }
    }
}