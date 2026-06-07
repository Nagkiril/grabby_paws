using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IV.Windows.Settings
{
    public interface IWindowInstanceProvider
    {
        public void GetWindowInstance<T>(Transform newInstanceRoot, string windowKey = "", Action<T> newInstanceCallback = null) where T : WindowBase;
    }
}