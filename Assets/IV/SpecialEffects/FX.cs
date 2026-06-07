using IV.Animations.Modules;
using IV.Sequences;
#if FEEL_RELEASE
using MoreMountains.Feedbacks;
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IV.SpecialEffects
{
    public class FX : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _controlledParticles;
        [SerializeField] private Animator _controlAnim;
        [SerializeField] private List<VisualModule> _visualModules;
#if FEEL_RELEASE
        [SerializeField] private List<MMF_Player> _feelPlayers;
#endif
        [SerializeField] private float _autoExpiration;
        [SerializeField] private float _autoDestruction;

        private float _expirationTimer;
        private float _destructionTimer;

        public event Action<FX> OnDestruction;

        private void Awake()
        {
            _expirationTimer = _autoExpiration;
        }

        private void FixedUpdate()
        {
            if (_expirationTimer > 0)
            {
                _expirationTimer -= Time.deltaTime;
                if (_expirationTimer <= 0)
                {
                    Hide();
                }
            }
            if (_destructionTimer > 0)
            {
                _destructionTimer -= Time.deltaTime;
                if (_destructionTimer <= 0)
                {
                    OnDestruction?.Invoke(this);
                    Destroy(gameObject);
                }
            }
        }

        public void Hide()
        {
            _destructionTimer = _autoDestruction;
            foreach (var particle in _controlledParticles)
            {
                if (particle != null)
                    particle.Stop();
            }
            if (_controlAnim != null)
                _controlAnim.SetBool("Show", false);

            foreach (var module in _visualModules)
            {
                module.Deactivate();
            }
        }

        public void Show(float expirationTimer = 0)
        {
            if (expirationTimer > 0)
                _expirationTimer = expirationTimer;
            foreach (var particle in _controlledParticles)
            {
                particle.Play();
            }
            if (_controlAnim != null)
                _controlAnim.SetBool("Show", true);

            foreach (var module in _visualModules)
            {
                module.Activate();
            }

#if FEEL_RELEASE
            foreach (var player in _feelPlayers)
            {
                player.PlayFeedbacks();
            }
#endif
        }

        public void ApplyEvent(string eventName)
        {
            //This can be expanded to support a whole lot of event handling - not just animator triggers - via interfaces and bonus components; I just decide to keep it simple because that's more than enough for now
            //We can (and perhaps should) replace eventName with an enum; and convert it to trigger hash later; we can do it seamlessly by adding another signature for ApplyEvent; this only makes sense if we'll expand Fx events later
            if (_controlAnim != null)
            {
                _controlAnim.SetTrigger(eventName);
            }
        }
    }
}