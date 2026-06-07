using GP.Environment.Levels.Slots;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace GP.Environment.Levels
{
    public class LevelTile : MonoBehaviour
    {
        [SerializeField] TileEndPoint[] _endPoints;
        [SerializeField] TileSlotsGenerator _slots;
        bool _isPopulated;

        public void Populate(LevelGenerationContext context)
        {
            if (!_isPopulated)
            {
                _isPopulated = true;
                _slots.Populate(context);
            }
            else
            {
                Debug.LogWarning($"LevelTile is being populated an extra time on tile '{gameObject.name}'.");
            }
        }

        public void Place(Vector3 worldPosition)
        {
            transform.position = worldPosition;
        }

        public void PlaceOnPosition(Vector3 worldPosition)
        {
            transform.position = worldPosition;
        }

        public void PlaceWithEndpoints(TileEndPoint thisTileEndpoint, TileEndPoint otherEndpoint)
        {
            if (thisTileEndpoint != null && otherEndpoint != null)
            {
                var foundEndpointOnTile = _endPoints.Contains(thisTileEndpoint);

                if (foundEndpointOnTile)
                {
                    thisTileEndpoint.ConnectEndpoints(otherEndpoint);

                    var mirroredYRotation = otherEndpoint.transform.rotation * Quaternion.Euler(0f, 180f, 0f);
                    var rotationDelta = mirroredYRotation * Quaternion.Inverse(thisTileEndpoint.transform.rotation);
                    transform.rotation = rotationDelta * transform.rotation;

                    var positionDelta = otherEndpoint.transform.position - thisTileEndpoint.transform.position;
                    transform.position += positionDelta;
                }
                else
                {
                    Debug.LogWarning($"Cannot place tile '{gameObject.name}' because provided endpoint '{thisTileEndpoint.gameObject.name}' is not part of its endPoints.");
                }
            }
        }

        public List<TileEndPoint> FindEmptyEndpoints()
        {
            var emptyEndpoints = new List<TileEndPoint>();

            if (_endPoints != null && _endPoints.Length > 0)
            {
                foreach (var endPoint in _endPoints)
                {
                    if (endPoint.IsEmpty())
                    {
                        emptyEndpoints.Add(endPoint);
                    }
                }
            }

            return emptyEndpoints;
        }

        public class Factory : PrefabFactory<LevelTile>
        {

        }
    }
}
