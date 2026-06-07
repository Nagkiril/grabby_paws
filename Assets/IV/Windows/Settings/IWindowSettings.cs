using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IV.Windows.Settings
{
    public interface IWindowSettings
    {
        public T GetWindowPrefab<T>() where T : WindowBase;

        public void GetAsyncWindowPrefab<T>(string windowKey, Action<T> onLoadCompleted) where T : WindowBase;

        public IEnumerable<WindowBase> GetAllWindowPrefabs();

        public void NotifyRelease(string windowKey);
    }
}