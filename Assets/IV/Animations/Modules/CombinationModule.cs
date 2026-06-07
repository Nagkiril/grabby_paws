using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace IV.Animations.Modules
{
    public class CombinationModule : VisualModule
    {
        [SerializeField] private List<VisualModule> _targetModules;

        private bool _isInitialized;
        private int _activeTargetsCount;

        private void Initialize()
        {
            if (!_isInitialized)
            {
                _isInitialized = true;
                foreach (var module in _targetModules)
                {
                    module.OnTransitionComplete += OnTargetTransitionComplete;
                }
            }
        }

        private void OnTargetTransitionComplete(VisualModule target)
        {
            _activeTargetsCount--;
            if (_activeTargetsCount == 0)
                NotifyTransitionDone();
        }

        public override void Activate(bool animate = true)
        {
            Initialize();
            _activeTargetsCount = _targetModules.Count;

            foreach (var module in _targetModules)
            {
                module.Activate(animate);
            }
        }

        public override void Deactivate(bool animate = true)
        {
            Initialize();
            _activeTargetsCount = _targetModules.Count;

            foreach (var module in _targetModules)
            {
                module.Deactivate(animate);
            }
        }
    }
}