using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IV.PoolableList
{
    public abstract class PoolableListItem<TInfo> : MonoBehaviour
    {

        private RectTransform _selfContainerRect;

        public virtual Vector2 Size => GetRectTransform().sizeDelta;

        public abstract void ShowInfo(TInfo info);

        public virtual void SetPosition(Vector2 position)
        {
            GetRectTransform().anchoredPosition = position;
        }

        protected RectTransform GetRectTransform()
        {
            if (_selfContainerRect == null)
            {
                _selfContainerRect = (RectTransform)transform;
            }

            return _selfContainerRect;
        }
    }
}