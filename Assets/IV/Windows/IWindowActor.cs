using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IV.Windows
{
    public interface IWindowActor
    {
        void OpenWindow<T>(Action<T> windowOpenCallback, string exactWindowKey = "", WindowOpenType openType = WindowOpenType.Default) where T : WindowBase;
    }
}