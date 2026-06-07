using IV.AssetManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace IV.Windows.Settings
{
    public class WindowSceneSettings : AssetUserMonoBehaviour, IWindowSettings
    {
        [SerializeField] private WindowBase[] _windowPrefabs;

        public T GetWindowPrefab<T>() where T : WindowBase
        {
            foreach (var window in _windowPrefabs)
            {
                if (window is T)
                    return (T)window;
            }
            return null;
        }

        public void GetAsyncWindowPrefab<T>(string windowKey, Action<T> onLoadCompleted) where T : WindowBase
        {
            if (string.IsNullOrEmpty(windowKey))
                windowKey = typeof(T).Name;
            Get<GameObject>(windowKey, loadedWindow => onLoadCompleted?.Invoke(loadedWindow.GetComponent<T>()));
        }

        public void NotifyRelease(string windowKey) => ReleaseAsset(windowKey);

        public IEnumerable<WindowBase> GetAllWindowPrefabs() => _windowPrefabs;
    }
}