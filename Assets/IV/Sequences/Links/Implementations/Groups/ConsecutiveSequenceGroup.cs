using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IV.Sequences.Links.Implementations.Groups
{
    public class ConsecutiveSequenceGroup : SequenceLink
    {
        [SerializeField] private SequenceLink[] _links;

        private SequenceLink _activeLink;
        private int _activeIndex;

        protected override void Start()
        {
            base.Start();
            foreach (var link in _links)
            {
                link.OnLinkFinished += OnSublinkFinished;
            }
        }

        protected override void BeginLink()
        {
            HaltLink();
            ProgressLink();
        }

        protected override void HaltLink()
        {
            if (_activeLink != null)
            {
                _activeLink.Halt();
                _activeLink = null;
                _activeIndex = -1;
            }
        }

        private void OnSublinkFinished(SequenceLink finishedLink)
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
                FinishLink();
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