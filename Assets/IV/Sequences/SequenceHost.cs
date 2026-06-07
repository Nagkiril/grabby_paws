using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IV.Sequences.Links;
using System;

namespace IV.Sequences
{
    public class SequenceHost : MonoBehaviour
    {
        [SerializeField] private SequenceLink[] _links;

        public bool IsRunning => _activeLink != null;
        public event Action OnSequenceFinished;

        private SequenceLink _activeLink;
        private int _activeIndex;

        void Start()
        {
            foreach (var link in _links)
            {
                link.OnLinkFinished += OnLinkFinished;
            }
        }

        public void Begin()
        {
            if (!IsRunning)
            {
                ProgressLink();
            }
        }

        public void Halt()
        {
            if (_activeLink != null)
            {
                _activeLink.Halt();
                _activeLink = null;
                _activeIndex = -1;
            }
        }

        private void OnLinkFinished(SequenceLink finishedLink)
        {
            if (_activeLink == finishedLink && finishedLink != null)
            {
                ProgressLink();
            }
        }
        
        private void ProgressLink()
        {
            SelectNextSequenceLink();
            if (_activeLink != null)
            {
                _activeLink.Begin();
            }
            else 
            {
                OnSequenceFinished?.Invoke();
            }
        }

        private void SelectNextSequenceLink()
        {
            if (_activeLink == null)
            {
                _activeLink = _links[0];
                _activeIndex = 0;
            }
            else if (_activeIndex < _links.Length - 1)
            {
                _activeLink = _links[++_activeIndex];
            }
            else
            {
                _activeLink = null;
                _activeIndex = -1;
            }
        }
    }
}