using UnityEngine;

namespace GP.Environment.Levels
{
    public class TileEndPoint : MonoBehaviour
    {
        TileEndPoint _connectedEndpoint;

        public bool IsEmpty()
        {
            return _connectedEndpoint == null;
        }

        public void ConnectEndpoints(TileEndPoint otherEndPoint)
        {
            if (IsEmpty())
            {
                _connectedEndpoint = otherEndPoint;
                otherEndPoint.ConnectEndpoints(this);
            }
        }
    }
}
