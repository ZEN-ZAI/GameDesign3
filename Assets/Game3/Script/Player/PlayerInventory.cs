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
    }
    #endregion

    public ItemSocket ItemSocketDummy;

    public List<Character> teamLists = new List<Character>();
    public List<Character> InventoryLists = new List<Character>();
    public int maxInventory = 50;
    
    public void Add(Character character)
    {
        if (InventoryLists.Count >= maxInventory)
        {
            Debug.Log("Your inventory is full.");
            return;
        }
        else
        {
            InventoryLists.Add(character);
            ItemSocket newItemSocket = Instantiate(ItemSocketDummy, SlotsInventoryLists.instance.AddItemInSlot());
            newItemSocket.AddInstance(character);
        }
    }
    
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

    // for Play
    void Start()
    {
        SlotsInventoryLists.instance.SetMaxInventory(maxInventory);
    }

    void Update()
    {
        if (maxInventory < SlotsInventoryLists.instance.GetMaxInventory())
        {
            return;
        }
        else
        {
            SlotsInventoryLists.instance.SetMaxInventory(maxInventory);
        }
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
