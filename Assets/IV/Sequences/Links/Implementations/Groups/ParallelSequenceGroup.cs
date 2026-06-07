using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IV.Sequences.Links.Implementations.Groups
{
    public class ParallelSequenceGroup : SequenceLink
    {
        [SerializeField] private SequenceLink[] _allLinks;

        protected override void Start()
        {
            foreach (var link in _allLinks)
            {
                link.OnLinkFinished += OnSublinkFinish;
            }
        }

        protected void OnSublinkFinish(SequenceLink link)
        {
            if (AllSublinksFinished())
                FinishLink();
        }

        private bool AllSublinksFinished()
        {
            bool isFinished = true;
            foreach(var link in _allLinks)
            {
                isFinished &= !link.IsRunning;
                if (!isFinished)
                    break;
            }
            return isFinished;
        }

        protected override void BeginLink()
        {
            foreach (var link in _allLinks)
            {
                link.Begin();
            }
        }

        protected override void HaltLink()
        {
            foreach (var link in _allLinks)
            {
                link.Halt();
            }
        }
    }
}