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

    public Item itemDummy;
    public Transform parentInventory;
    public Item[] itemInInventory;

    public List<Character> team = new List<Character>();
    public List<Character> inventory = new List<Character>();
    private int maxInventory = 50;

    public void Add(Character character)
    {
        if (inventory.Count >= maxInventory)
        {
            Debug.Log("Your inventory is full.");
            return;
        }
        else
        {
            inventory.Add(character);
            Item newItem = Instantiate(itemDummy, parentInventory);
            newItem.AddItem(character);
        }
    }

    public void Remove(Character character)
    {
        if (inventory.Count >= 1)
        {
            inventory.Remove(character);
        }
        else
        {
            Debug.Log("Not have character in your inventory");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        itemInInventory = parentInventory.GetComponentsInChildren<Item>();
        Debug.Log(itemInInventory.Length);
        foreach (var item in itemInInventory)
        {
            inventory.Add(item.character);
        }
    }

}
