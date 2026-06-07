using GP.Players;
using UnityEngine;

namespace GP.Players.ControlSources
{
    public abstract class PlayerControlSource : MonoBehaviour
    {
        [SerializeField] PlayerController _controller;

        void Start()
        {
            _controller.RegisterSource(this);
        }

        public abstract Vector2 GetMoveVector();
    }
}