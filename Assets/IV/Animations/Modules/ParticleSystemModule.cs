using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace IV.Animations.Modules
{
    public class ParticleSystemModule : VisualModule
    {
        [SerializeField] private ParticleSystem _targetSystem;


        public override void Activate(bool animate = true)
        {
            _targetSystem.Play();
        }


        public override void Deactivate(bool animate = true)
        {
            _targetSystem.Stop();
        }

    }
}