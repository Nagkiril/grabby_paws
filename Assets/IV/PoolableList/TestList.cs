using IV.PoolableList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class TestList : PoolableList<TestListItem, TestListInfo>
    {
        private void Start()
        {
            var testInfos = new List<TestListInfo>();
            for (var i = 0; i < 15; i++)
            {
                testInfos.Add(new TestListInfo(i.ToString()));
            }
            ShowItems(testInfos);
        }
    }
}