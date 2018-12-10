using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsInventoryListsSystem : MonoBehaviour
{
    #region Singleton
    public static SlotsInventoryListsSystem instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public List<RectTransform> slots = new List<RectTransform>();

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            slots.Add(transform.GetChild(i).GetComponent<RectTransform>());
        }

        SortingInventory();
    }

    public void SortingInventory()
    {
        slots = slots.OrderBy(e => e.childCount).Reverse().ToList();
        foreach (var item in slots)
        {
            int index = slots.IndexOf(item);
            item.SetSiblingIndex(index);
        }
    }


}
