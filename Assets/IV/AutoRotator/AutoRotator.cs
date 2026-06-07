using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace IV.AutoRotator
{
    public class AutoRotator : MonoBehaviour
    {
        [SerializeField] Vector3 _rotationAngles = new Vector3(0f, 90f, 0f);
        [SerializeField] Space _rotationSpace = Space.Self;

        public bool IsRotating = true;

        CancellationTokenSource _rotationCancellation;

        void OnEnable()
        {
            _rotationCancellation = new CancellationTokenSource();
            RunRotationLoop(_rotationCancellation.Token).Forget();
        }

        void OnDisable()
        {
            _rotationCancellation?.Cancel();
            _rotationCancellation?.Dispose();
            _rotationCancellation = null;
        }

        async UniTaskVoid RunRotationLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (IsRotating)
                {
                    var rotationStep = _rotationAngles * Time.fixedDeltaTime;
                    transform.Rotate(rotationStep, _rotationSpace);
                }

                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken);
            }
        }
    }
}
