using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IV.AssetManagement
{
    public interface IAssetUser
    {
        public event Action<IAssetUser> OnAllAssetsRelease;
        public event Action<IAssetUser, string> OnAssetRelease;
    }
}