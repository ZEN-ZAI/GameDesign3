using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    
    #region Singleton
    public static DataBase instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public List<Character> allCharacter = new List<Character>();
    private List<Character> normalCharacter = new List<Character>();
    private List<Character> rareCharacter = new List<Character>();
    private List<Character> srCharacter = new List<Character>();
    private List<Character> ssrCharacter = new List<Character>();
    private List<Character> zenCharacter = new List<Character>();

    private int itemId;

    void Start()
    {
        foreach (var item in allCharacter)
        {
            if (item.rank == "Normal")
            {
                normalCharacter.Add(item);
            }
            else if (item.rank == "Rare")
            {
                rareCharacter.Add(item);
            }
            else if(item.rank == "SR")
            {
                srCharacter.Add(item);
            }
            else if(item.rank == "SSR")
            {
                ssrCharacter.Add(item);
            }
            else if (item.rank == "ZEN")
            {
                zenCharacter.Add(item);
            }
        }
    }

    public Character GiftItem(int index)
    {
        Character newItem = Instantiate(allCharacter[index]);
        newItem.itemID = ++itemId;

        return newItem;
    }

    void Update()
    {
        
    }
}
