using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IV.Windows.Settings;
using Zenject;
using System;

namespace IV.Windows
{
    public class WindowController : MonoBehaviour, IWindowActor
    {
        [SerializeField] private Image _backgroundImage;
        private WindowBase _activeWindow;
        private Stack<WindowBase> _openWindows;
        private Queue<WindowBase> _queuedWindows;

        [Inject] private IWindowInstanceProvider _windowProvider;

        private void Awake()
        {
            _openWindows = new Stack<WindowBase>();
            _queuedWindows = new Queue<WindowBase>();
            CheckBackgroundActivity();
        }

        private void CreateNewWindow<T>(Action<T> onNewWindowProvided, string exactWindowKey, WindowOpenType openType) where T : WindowBase
        {
            _windowProvider.GetWindowInstance<T>(transform, exactWindowKey, (newWindow) =>
            {
                if (newWindow != null)
                {
                    var appliedOpenType = (openType == WindowOpenType.Default ? newWindow.OpenType : openType);

                    if (appliedOpenType == WindowOpenType.MainWindow)
                        QueueWindow(newWindow);
                    else
                        ActivateWindow(newWindow);

                    onNewWindowProvided?.Invoke(newWindow);
                }
                else
                {
                    Debug.LogWarning($"Could not make a window prefab of type {typeof(T).Name}!");
                }
            });
        }

        private void QueueWindow(WindowBase targetWindow)
        {
            _queuedWindows.Enqueue(targetWindow);
            targetWindow.SetHidden();
            if (!HasActiveWindow())
                ActivateNextWindow();
        }

        private void ActivateWindow(WindowBase targetWindow)
        {
            _activeWindow = targetWindow;
            _activeWindow.OnWindowClosed += OnWindowClosed;
            if (!_openWindows.Contains(targetWindow))
                _openWindows.Push(targetWindow);
            targetWindow.Reactivate();
        }

        private void OnWindowClosed(WindowBase targetWindow)
        {
            targetWindow.OnWindowClosed -= OnWindowClosed;
            ActivateNextWindow();
        }

        private void ActivateNextWindow()
        {
            if (_openWindows.Count > 0)
                _openWindows.Pop();
            if (_openWindows.Count > 0)
            {
                ActivateWindow(_openWindows.Peek());
            } else
            {
                if (_queuedWindows.Count > 0)
                {
                    ActivateWindow(_queuedWindows.Dequeue());
                }
            }
            CheckBackgroundActivity();
        }

        private void CheckBackgroundActivity()
        {
            _backgroundImage.gameObject.SetActive(HasActiveWindow());
        }

        public bool HasActiveWindow()
        {
            return _activeWindow != null && _activeWindow.IsShown;
        }

        public void OpenWindow<T>(Action<T> windowOpenCallback, string exactWindowKey = "", WindowOpenType openType = WindowOpenType.Default) where T : WindowBase
        {
            CreateNewWindow(windowOpenCallback, exactWindowKey, openType);
        }
    }
}