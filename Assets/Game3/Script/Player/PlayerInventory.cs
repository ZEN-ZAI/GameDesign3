using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    #region Singleton
    public static PlayerInventory instance;

    void Awake()
    {
        instance = this;
        SlotsInventoryLists.instance.SetMaxInventory(maxInventory);
    }
    #endregion

    public ItemSocket ItemSocketDummy;
    [SerializeField] private List<Character> InventoryLists = new List<Character>();
    [SerializeField] private int maxInventory;

    public void UpdateMaxInventory(int n)
    {
        maxInventory += n;
    }

    public void AddItem(Character character)
    {
        if (InventoryLists.Count >= maxInventory)
        {
            Debug.Log("Your inventory is full.");
            return;
        }
        else if (SlotsInventoryLists.instance.Count > 0)
        {
            InventoryLists.Add(character);
            SlotsInventoryLists.instance.AddItemSocketInSlot(character);
        }
    }

    public void Add(Character character)
    {
        if (InventoryLists.Count >= maxInventory)
        {
            Debug.Log("Your inventory is full.");
            return;
        }
        else if(SlotsInventoryLists.instance.Count > 0)
        {
            InventoryLists.Add(character);
        }
    }

    // use for itemSocket
    public void Remove(Character character)
    {
        if (InventoryLists.Count >= 1)
        {
            InventoryLists.Remove(character);
        }
        else
        {
            Debug.Log("Not have character in your inventory");
        }
    }

    
    public void ReloadItemInPlayerInventorylists()
    {
        SlotsInventoryLists.instance.ClearSlot();

        foreach (var item in PlayerInventory.instance.InventoryLists)
        {
            SlotsInventoryLists.instance.AddSlot();
            SlotsInventoryLists.instance.AddItemSocketInSlot(item);
        }
    }

    void Update()
    {
        SlotsInventoryLists.instance.SetMaxInventory(maxInventory);
    }


    // for Debug
    /*
    void Start()
    {


        ItemSocketInInventory = parentInventory.GetComponentsInChildren<ItemSocket>();
        Debug.Log(ItemSocketInInventory.Length);
        foreach (var ItemSocket in ItemSocketInInventory)
        {
            InventoryLists.Add(ItemSocket.character);
        }
    }
    */

}
