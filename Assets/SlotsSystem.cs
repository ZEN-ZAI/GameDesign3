using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsSystem : MonoBehaviour
{
    #region Singleton
    public static SlotsSystem instance;

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
        slots = slots.OrderBy(e => e.childCount).Reverse().ToList();
        foreach (var item in slots)
        {
            int index = slots.IndexOf(item);
            item.SetSiblingIndex(index);
        }
    }

    void Update()
    {

        if (UILink.instance.inventoryPanel.activeInHierarchy)
        {

        }
    }


}
