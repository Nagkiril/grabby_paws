using UnityEngine;

namespace GP.Actors
{
    public class ActorController : MonoBehaviour
    {
        [SerializeField] Rigidbody _ownBody;
        [SerializeField] float _speed;

        public void Move(Vector2 direction)
        {
            if (_ownBody != null)
            {
                var delta = new Vector3(direction.x, 0f, direction.y) * _speed * Time.deltaTime;
                _ownBody.MovePosition(_ownBody.position + delta);
            }
        }
    }
}
