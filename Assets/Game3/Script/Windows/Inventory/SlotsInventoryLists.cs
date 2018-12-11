using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsInventoryLists : MonoBehaviour
{
    #region Singleton
    public static SlotsInventoryLists instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject slotInventoryPrefeb;
    private List<GameObject> slots = new List<GameObject>();
    private int maxInventory;

    public int GetMaxInventory()
    {
        return maxInventory;
    }

    public void SetMaxInventory(int n)
    {
        maxInventory = n;
    }

    public Transform AddItemInSlot()
    {
        int index = slots.FindIndex(e => e.transform.childCount == 0);

        Debug.LogWarning("Add idex slot :"+index);
        return slots[index].transform;

    }

    /* // use  Destroy(gameObject); in a in ItemSocket.cs
    public Transform RemoveItemInSlot(Character character)
    {

        int index = slots.FindIndex(e => e.GetComponent<ItemSocket>().character == character);

        Debug.LogWarning("Remove idex slot :" + index);
        return slots[index].transform;

    }*/

    // for Play
    void Start()
    {
        //slots.Capacity = PlayerInventory.instance.maxInventory;

        if (transform.childCount !=0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < maxInventory; i++)
        {
            GameObject slotInventory = Instantiate(slotInventoryPrefeb,transform);
            slots.Add(slotInventory);
        }
    }

    public void SortingInventory()
    {
        slots = slots.OrderBy(e => e.transform.childCount).Reverse().ToList();
        foreach (var item in slots)
        {
            int index = slots.IndexOf(item);
            item.transform.SetSiblingIndex(index);
        }
    }

    // for Debug
    /*
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
    */

}
