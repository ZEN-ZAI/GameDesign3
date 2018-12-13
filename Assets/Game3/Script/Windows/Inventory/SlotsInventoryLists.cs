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

    public ItemSocket ItemSocketDummy;
    public Transform inventoryContent;
    public GameObject slotInventoryPrefeb;

    [SerializeField]private List<GameObject> slotsInventoryLists = new List<GameObject>();
    private int maxInventory;

    public int Count
    {
        get { return slotsInventoryLists.Count; }
    }

    public int GetMaxInventory()
    {
        return maxInventory;
    }

    public void SetMaxInventory(int n)
    {
        maxInventory = n;
    }

    public void AddSlot()
    {
        if (slotsInventoryLists.Count < maxInventory)
        {
            Debug.LogWarning("Add Slot.");

            GameObject slotInventory = Instantiate(slotInventoryPrefeb, inventoryContent);
            slotInventory.transform.tag = "InventorySlot";
            slotsInventoryLists.Add(slotInventory);
        }
    }

    public void RemoveSlot()
    {
        if (slotsInventoryLists.Count >= 1)
        {
            Debug.LogWarning("Remove Slot.");

            slotsInventoryLists.Remove(slotsInventoryLists[slotsInventoryLists.Count - 1]);
            slotsInventoryLists.Capacity--;
            Destroy(inventoryContent.GetChild(slotsInventoryLists.Count).gameObject);
        }
    }

    public void ClearSlot()
    {
        Debug.LogWarning("Clear Slot.");

        for (int i = slotsInventoryLists.Count; i >= 1; i--)
        {
            slotsInventoryLists.Remove(slotsInventoryLists[slotsInventoryLists.Count - 1]);
            slotsInventoryLists.Capacity--;
            Destroy(inventoryContent.GetChild(slotsInventoryLists.Count).gameObject);
        }

    }

    public void AddItemSocketInSlot(Character character)
    {
        if (slotsInventoryLists.Count >= 1)
        {
            int index = slotsInventoryLists.FindIndex(e => e.transform.childCount == 0);
            ItemSocket newItemSocket = Instantiate(ItemSocketDummy, slotsInventoryLists[index].transform);
            newItemSocket.AddInstance(character);

            Debug.LogWarning("Add item[" + character.name + "] in slot: " + index);
        }

    }

    //use Destroy(gameObject); in a in ItemSocket.cs For RemoveItemInSlot 
    public void RemoveItemInSlot(Character character)
    {
        /*
        int index = slotsInventoryLists.FindIndex(e => e.GetComponent<ItemSocket>().character == character);

        Debug.LogWarning("Remove idex slot :" + index);
        return slotsInventoryLists[index].transform;
        */
    }

    // for Play
    void Start()
    {
        if (inventoryContent.childCount != 0)
        {
            for (int i = 0; i < inventoryContent.childCount; i++)
            {
                Destroy(inventoryContent.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < maxInventory; i++)
        {
            AddSlot();
        }
    }

    void Update()
    {
        if (Count < maxInventory)
        {
            AddSlot();
        }
        else if (Count > maxInventory)
        {
            RemoveSlot();
        }
    }

    public void SortingInventory()
    {
        slotsInventoryLists = slotsInventoryLists.OrderBy(e => e.transform.childCount).Reverse().ToList();
        foreach (var item in slotsInventoryLists)
        {
            int index = slotsInventoryLists.IndexOf(item);
            item.transform.SetSiblingIndex(index);
        }
    }

}
