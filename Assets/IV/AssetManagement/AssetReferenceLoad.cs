using System;
using System.Collections.Generic;

namespace IV.AssetManagement
{
    [Serializable]
    public class AssetReferenceLoad
    {
        public string AssetKey { get; private set; }
        public List<Action<AssetReferenceData>> LoadFinishCallbacks { get; private set; }

        public AssetReferenceLoad(string assetKey)
        {
            AssetKey = assetKey;
            LoadFinishCallbacks = new List<Action<AssetReferenceData>>();
        }
    }
}