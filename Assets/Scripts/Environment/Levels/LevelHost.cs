using System.Collections.Generic;
using UnityEngine;

namespace GP.Environment.Levels
{
    public class LevelHost : MonoBehaviour
    {
        [SerializeField] List<LevelTile> _currentTiles;

        void Awake()
        {
            _currentTiles = new List<LevelTile>();
        }

        public void PlaceTile(LevelTile placedTile, TileEndPoint placeOnEndpoint = null)
        {
            if (placedTile != null) 
            {
                _currentTiles.Add(placedTile);
                placedTile.transform.SetParent(transform);

                TileEndPoint placedTileEndpoint = null;
                if (placeOnEndpoint != null)
                {
                    var placedTileEmptyEndpoints = placedTile.FindEmptyEndpoints();
                    if (placedTileEmptyEndpoints.Count > 0)
                    {
                        var randomIndex = Random.Range(0, placedTileEmptyEndpoints.Count);
                        placedTileEndpoint = placedTileEmptyEndpoints[randomIndex];
                    }
                }

                if (placedTileEndpoint != null)
                {
                    placedTile.PlaceWithEndpoints(placedTileEndpoint, placeOnEndpoint);
                }
                else
                {
                    placedTile.PlaceOnPosition(placeOnEndpoint?.transform.position ?? Vector3.zero);
                }
            }
        }

        public List<TileEndPoint> GetEmptyTileEndpoints()
        {
            var emptyEndpoints = new List<TileEndPoint>();

            foreach (var tile in _currentTiles)
            {
                emptyEndpoints.AddRange(tile.FindEmptyEndpoints());
            }

            return emptyEndpoints;
        }
    }
}
