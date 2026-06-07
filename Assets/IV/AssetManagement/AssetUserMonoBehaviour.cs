using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IV.AssetManagement
{
    public class AssetUserMonoBehaviour : MonoBehaviour, IAssetUser
    {
        [SerializeField] protected bool _isLazyAsseting;

        [Inject] protected AssetProvider _assets;

        public event Action<IAssetUser> OnAllAssetsRelease;
        public event Action<IAssetUser, string> OnAssetRelease;

        protected virtual void OnDestroy()
        {
            OnAllAssetsRelease?.Invoke(this);
        }

        protected void ReleaseAsset(string assetKey)
        {
            OnAssetRelease?.Invoke(this, assetKey);
        }

        protected void Get<T>(string assetKey) where T : UnityEngine.Object
        {
            if (_isLazyAsseting)
                _assets.Get<T>(assetKey, gameObject, OnAssetPrepared);
            else
                _assets.Get<T>(assetKey, this, OnAssetPrepared);
        }

        protected void Get<T>(string assetKey, Action<T> assetPreparedCallback) where T : UnityEngine.Object
        {
            if (_isLazyAsseting)
                _assets.Get<T>(assetKey, gameObject, assetPreparedCallback);
            else
                _assets.Get<T>(assetKey, this, assetPreparedCallback);
        }

        protected virtual void OnAssetPrepared<T>(T asset) where T : UnityEngine.Object
        {
            Debug.LogWarning($"OnAssetPrepared called for an unhandled asset {asset} of type {asset.GetType()}!");
        }
    }
}