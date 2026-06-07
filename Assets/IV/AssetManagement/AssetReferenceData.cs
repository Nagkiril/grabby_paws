using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace IV.AssetManagement
{
    [Serializable]
    public class AssetReferenceData
    {
        public string AssetKey { get; private set; }
        public int ReferenceCount => DirectUsers?.Count ?? 0 + LazyUsers?.Count ?? 0;
        public List<IAssetUser> DirectUsers { get; private set; }
        public List<GameObject> LazyUsers { get; private set; }
        public AsyncOperationHandle MemoryHandle { get; private set; }
        public UnityEngine.Object LoadedAsset { get; private set; }

        public AssetReferenceData(string className, AsyncOperationHandle memoryHandle, UnityEngine.Object baseComponent)
        {
            AssetKey = className;
            MemoryHandle = memoryHandle;
            LoadedAsset = baseComponent;
        }

        public void AddUser(GameObject gameObject)
        {
            if (LazyUsers == null)
                LazyUsers = new List<GameObject>();
            if (!LazyUsers.Contains(gameObject))
                LazyUsers.Add(gameObject);
            LazyUsers.Add(gameObject);
        }

        public void AddUser(IAssetUser user)
        {
            if (DirectUsers == null)
                DirectUsers = new List<IAssetUser>();
            if (!DirectUsers.Contains(user))
                DirectUsers.Add(user);
        }
    }
}