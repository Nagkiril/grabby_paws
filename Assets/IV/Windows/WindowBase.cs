using System;
using System.Collections;
using System.Collections.Generic;
using IV.Animations.Controllers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace IV.Windows
{
    public class WindowBase : MonoBehaviour
    {
        [field: SerializeField] public WindowOpenType OpenType { get; private set; }
        [field: SerializeField] public bool PrepareAllowed { get; protected set; }
        [SerializeField] protected ShowHideActiveController _showController;
        [SerializeField] protected CustomButton _closeButton;
        protected bool _isControlAllowed;

        protected string _customWindowKey;

        public bool ControlAllowed
        {
            get => _isControlAllowed;
            set
            {
                _isControlAllowed = value;

            }
        }
        public bool IsShown { get; protected set; }
        public event Action<WindowBase> OnWindowClosed;

        protected virtual void Awake()
        {
            if (OpenType == WindowOpenType.Default)
            {
                Debug.LogWarning($"{gameObject} window has not been assigned a definitive WindowOpenType!");
            }
            if (_closeButton != null)
                _closeButton.OnClicked += Close;
            _showController.OnAnimationFinish += OnAnimationFinished;
        }

        protected virtual void Start()
        {
            if (!IsShown)
                SetHidden();
        }

        protected void ChangeShow(bool isShown)
        {
            IsShown = isShown;
            if (isShown)
                _showController.Show();
            else
                _showController.Hide();
        }

        protected void NotifyWindowClosed()
        {
            OnWindowClosed?.Invoke(this);
        }

        protected virtual void OnAnimationFinished(AnimationController controller)
        {
            if (!_showController.IsShown)
                NotifyWindowClosed();
        }

        public virtual void Reactivate()
        {
            if (!IsShown)
                Show();
        }

        public virtual void Show() 
        {
            ChangeShow(true);
        }

        public virtual void Close()
        {
            if (IsShown)
                ChangeShow(false);
        }

        public virtual void SetHidden()
        {
            IsShown = false;
            _showController.Hide(false);
        }

        public virtual void SetCustomWindowKey(string windowKey)
        {
            _customWindowKey = windowKey;
        }

        //We're not doing Prepare() method as an interface (such as IWindowPreparable) in order to avoid having to cast windows multiple times (cast prefab to check interface 1st, then cast new instance to call the method on it)
        public virtual void Prepare()
        {

        }

        public virtual string GetWindowKey()
        {
            return string.IsNullOrEmpty(_customWindowKey) ? GetType().Name : _customWindowKey;
        }

        public class Factory : PrefabFactory<WindowBase>
        {

        }
    }

    public enum WindowOpenType
    {
        Default,
        MainWindow,
        SubWindow
    }
}