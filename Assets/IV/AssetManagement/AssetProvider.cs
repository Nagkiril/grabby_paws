using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace IV.AssetManagement
{
    public class AssetProvider : MonoBehaviour
    {
        [SerializeField] private float _lazyUsersCheckTime = 600;

        private List<AssetReferenceData> _assetReferences = new List<AssetReferenceData>();
        private List<AssetReferenceLoad> _loadsInProgress = new List<AssetReferenceLoad>();
        private Coroutine _lazyUsersCheckCoroutine;

        private void Awake()
        {
            _lazyUsersCheckCoroutine = StartCoroutine(CheckLazyUsers());
        }

        private void OnDestroy()
        {
            StopCoroutine(_lazyUsersCheckCoroutine);
            foreach (var asset in _assetReferences)
            {
                if (asset.MemoryHandle.IsValid())
                {
                    Addressables.Release(asset.MemoryHandle);
                }
            }
        }

        private void ReleaseFreeAsset(AssetReferenceData asset)
        {
            if (asset.ReferenceCount == 0)
            {
                if (asset.MemoryHandle.IsValid())
                {
                    Addressables.Release(asset.MemoryHandle);
                }
                _assetReferences.Remove(asset);
            }
        }

        private IEnumerator CheckLazyUsers()
        {
            while (true)
            {
                yield return new WaitForSeconds(_lazyUsersCheckTime);
                for (var i = _assetReferences.Count - 1; i >= 0; i--)
                {
                    var asset = _assetReferences[i];
                    if (asset.DirectUsers?.Count == 0)
                    {
                        for (var j = asset.LazyUsers.Count - 1; j >= 0; j--)
                        {
                            if (asset.LazyUsers[j] == null)
                                asset.LazyUsers.RemoveAt(j);
                            else
                                break;
                        }
                        ReleaseFreeAsset(asset);
                    }
                }
            }
        }

        private AssetReferenceData MakeAssetReference<T>(string assetKey, AsyncOperationHandle<T> loadProcess) where T : UnityEngine.Object
        {
            var loadedReference = new AssetReferenceData(assetKey, loadProcess, loadProcess.Result);
            _assetReferences.Add(loadedReference);
            return loadedReference; 
        }

        private void ReleaseDirectUser(IAssetUser user, string targetKey = null)
        {
            for (var i = _assetReferences.Count - 1; i >= 0; i--)
            {
                var asset = _assetReferences[i];
                if (string.IsNullOrEmpty(targetKey) || asset.AssetKey == targetKey)
                    asset.DirectUsers.Remove(user);
                ReleaseFreeAsset(asset);
            }
        }

        private void LoadAsset<T>(string assetKey, Action<T> onLoadCompleted, Action<AssetReferenceData> referenceAccount) where T : UnityEngine.Object
        {
            assetKey = string.IsNullOrEmpty(assetKey) ? typeof(T).Name : assetKey;

            var existingReference = _assetReferences.FirstOrDefault(asset => asset.AssetKey == assetKey);
            if (existingReference != null)
            {
                referenceAccount?.Invoke(existingReference);
                onLoadCompleted?.Invoke(existingReference.LoadedAsset as T);
            }
            else
            {
                var existingLoad = _loadsInProgress.FirstOrDefault(load => load.AssetKey == assetKey);
                if (existingLoad == null)
                {
                    existingLoad = new AssetReferenceLoad(assetKey);
                    _loadsInProgress.Add(existingLoad);
                    Addressables.LoadAssetAsync<T>(assetKey).Completed += (handle) =>
                    {
                        var newReference = MakeAssetReference(assetKey, handle);
                        referenceAccount.Invoke(newReference);
                        _loadsInProgress.Remove(existingLoad);
                        existingLoad.LoadFinishCallbacks.ForEach(callback => callback?.Invoke(newReference));
                    };
                }
                existingLoad.LoadFinishCallbacks.Add((newReference) => { referenceAccount.Invoke(newReference); onLoadCompleted?.Invoke(newReference.LoadedAsset as T); });
            }
        }

        public void Get<T>(string assetKey, GameObject lazyUser, Action<T> onLoadCompleted) where T : UnityEngine.Object
        {
            LoadAsset(assetKey, onLoadCompleted, reference => reference.AddUser(lazyUser));
        }

        public void Get<T>(string assetKey, IAssetUser directUser, Action<T> onLoadCompleted) where T : UnityEngine.Object
        {
            LoadAsset(assetKey, onLoadCompleted, reference => reference.AddUser(directUser));
            directUser.OnAllAssetsRelease += (user) => ReleaseDirectUser(user);
            directUser.OnAssetRelease += (user, assetKey) => ReleaseDirectUser(user, assetKey);
        }
    }
}