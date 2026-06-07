using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IV.PoolableList
{
    public abstract class PoolableList<TListItem, TItemInfo> : MonoBehaviour
        where TListItem : PoolableListItem<TItemInfo>
    {
        [SerializeField] protected int _itemColumns;
        [SerializeField] protected int _itemRows;
        [SerializeField] protected TListItem _itemPrefab;
        [SerializeField] protected RectTransform _itemsContainer;
        [SerializeField] protected ScrollRect _scroll;

        protected List<TItemInfo> _infos;
        protected List<TListItem> _items = new List<TListItem>();
        protected int _currentTopRow = int.MinValue;

        protected virtual void Awake()
        {
            _scroll.onValueChanged.AddListener((scrollPosition) => OnScrollChange());
        }

        private void ShowInfosInViewport()
        {
            if (_infos != null)
            {
                var topRowIndex = Mathf.FloorToInt(_itemsContainer.anchoredPosition.y / _itemPrefab.Size.y);
                topRowIndex = Mathf.Max(0, topRowIndex);

                if (_currentTopRow != topRowIndex)
                {
                    var itemsStates = new Dictionary<TListItem, bool>();
                    foreach (var item in _items)
                    {
                        itemsStates.Add(item, false);
                    }

                    for (var currentRow = topRowIndex; currentRow < topRowIndex + _itemRows; currentRow++)
                    {
                        for (var currentColumn = 0; currentColumn < _itemColumns; currentColumn++)
                        {
                            var itemIndex = (currentRow - topRowIndex) * _itemColumns + currentColumn;
                            var infoIndex = currentRow * _itemColumns + currentColumn;

                            if (infoIndex < _infos.Count)
                            {
                                itemsStates[_items[itemIndex]] = true;
                                _items[itemIndex].ShowInfo(_infos[infoIndex]);
                                _items[itemIndex].SetPosition(GetViewPosition(currentRow, currentColumn));
                            }
                        }
                    }
                    _currentTopRow = topRowIndex;

                    foreach (var itemState in itemsStates)
                    {
                        if (itemState.Key.gameObject.activeSelf != itemState.Value)
                            itemState.Key.gameObject.SetActive(itemState.Value);
                    }
                }
            }
        }

        protected void ShowItems(List<TItemInfo> items)
        {
            _infos = new List<TItemInfo>(items);
            var contaierHeight = Mathf.CeilToInt((float)_infos.Count / _itemColumns) * _itemPrefab.Size.y;
            _itemsContainer.sizeDelta = new Vector2(_itemsContainer.sizeDelta.x, contaierHeight);
            CreateViews();
            ShowInfosInViewport();
        }

        protected virtual void OnScrollChange()
        {
            ShowInfosInViewport();
        }

        protected void CreateViews()
        {
            var itemCount = (_itemColumns + 1) * _itemRows;

            for (var i = 0; i < itemCount && _items.Count < itemCount; i++)
            {
                var newItem = Instantiate(_itemPrefab, _itemsContainer);
                _items.Add(newItem);
            }
        }

        protected Vector2 GetViewPosition(int rowIndex, int columnIndex)
        {
            var size = _itemPrefab.Size;
            return new Vector2(columnIndex * size.x, -1f * rowIndex * size.y);
        }
    }
}