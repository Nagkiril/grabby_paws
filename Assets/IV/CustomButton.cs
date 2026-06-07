using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using IV.Animations.Controllers.UI;

namespace IV
{
    public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Button _wrappedButton;
        [SerializeField] private ButtonVisualController _visualController;

        public event Action OnClicked;

        public bool Interactable
        {
            get => _wrappedButton.interactable;
            set
            {
                if (_wrappedButton.interactable != value)
                {
                    _wrappedButton.interactable = value;
                    _visualController?.SetInteractable(value);
                }
            }
        }

        private void Awake()
        {
            _wrappedButton.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            OnClicked?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (Interactable && _visualController != null)
            {
                _visualController?.Release();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Interactable && _visualController != null)
            {
                _visualController.Press();
            }
        }
    }
}