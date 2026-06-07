using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IV.Sequences.Links
{
    public abstract class SequenceLink : MonoBehaviour
    {
        public bool IsRunning { get; private set; }
        public event Action<SequenceLink> OnLinkFinished;

        protected virtual void Start()
        {

        }

        protected abstract void BeginLink();
        protected abstract void HaltLink();

        protected void FinishLink()
        {
            IsRunning = false;
            OnLinkFinished?.Invoke(this);
        }

        public void Begin()
        {
            IsRunning = true;
            BeginLink();
        }

        public void Halt()
        {
            IsRunning = false;
            HaltLink();
        }
    }
}