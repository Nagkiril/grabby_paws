using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace IV.Sequences.Links.Implementations
{
    public abstract class MaterialPropertyLink : DotweenSequenceLink
    {
        [SerializeField] private Renderer _targetRenderer;
        [SerializeField] protected string _propertyName;

        protected Material GetTargetMaterial()
        {
            return _targetRenderer.material;
        }
    }
}