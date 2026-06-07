using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace IV.Sequences.Links.Implementations
{
    public class FloatPropertyLink : MaterialPropertyLink
    {
        [SerializeField] private float _startFloat;
        [SerializeField] private float _endFloat;

        protected override void SetTransitionStart()
        {
            GetTargetMaterial().SetFloat(_propertyName, _startFloat);
        }

        protected override Tween GetTransitionTween()
        {
            return GetTargetMaterial().DOFloat(_endFloat, _propertyName, _duration);
        }
    }
}