using GP.Actors;
using GP.Players.ControlSources;
using System.Collections.Generic;
using UnityEngine;

namespace GP.Players
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] ActorController _actorController;

        List<PlayerControlSource> _controlSources = new List<PlayerControlSource>();

        void FixedUpdate()
        {
            if (_actorController != null)
            {
                var input = Vector2.zero;
                foreach (var control in _controlSources) 
                {
                    input += control.GetMoveVector();
                }

                _actorController.Move(input.normalized);
            }
        }

        public void RegisterSource(PlayerControlSource newSource)
        {
            if (!_controlSources.Contains(newSource))
            {
                _controlSources.Add(newSource);
            }
            else
            {
                Debug.LogWarning($"Trying to register an existing control source {newSource}, possibly due to a bug.");
            }
        }
    }
}
