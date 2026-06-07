using IV.PoolableList;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Test
{
    public class TestListInfo
    {
        public string Name;

        public TestListInfo(string name)
        {
            Name = name;
        }
    }

    public class TestListItem : PoolableListItem<TestListInfo>
    {
        [SerializeField] private TMP_Text _title;

        public override void ShowInfo(TestListInfo info)
        {
            _title.text = info.Name;
        }
    }
}