using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace IV.Sequences.Links.Implementations
{
    public abstract class DotweenSequenceLink : SequenceLink
    {
        [SerializeField] private Ease _ease;
        [SerializeField] protected float _duration;
        private Sequence _linkSequence;

        private void FinishSeqeuence()
        {
            ClearSequence();
            _linkSequence = null;
            FinishLink();
        }

        private void ClearSequence()
        {
            if (_linkSequence != null)
            {
                _linkSequence.Kill();
                _linkSequence = null;
            }
        }

        protected override void BeginLink()
        {
            ClearSequence();
            SetTransitionStart();
            _linkSequence = DOTween.Sequence();
            _linkSequence.Append(GetTransitionTween().SetEase(_ease));
            _linkSequence.AppendCallback(FinishSeqeuence);
        }

        protected override void HaltLink()
        {
            ClearSequence();
        }

        protected abstract Tween GetTransitionTween();

        protected abstract void SetTransitionStart();
    }
}