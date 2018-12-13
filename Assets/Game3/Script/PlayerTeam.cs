using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    #region Singleton
    public static PlayerTeam instance;

    void Awake()
    {
        instance = this;
        SlotsTeamMemberLists.instance.SetMaxInventory(maxMember);
    }
    #endregion

    public Character[] teamLists;
    [SerializeField] private int maxMember;
    public int Count
    {
        get { return teamLists.Length; }
    }

    public void UpdateMaxMember(int n)
    {
        maxMember += n;
    }

    public bool HaveMember(int index)
    {
        return teamLists[index];
    }

    public void AddMember(int memberNumber, Character character)
    {
        Debug.Log("Add PlayerTeam characterID[" + character.itemID + "] in Slot["+memberNumber+"]");
        teamLists[memberNumber] = character;
    }

    public void Remove(Character character)
    {
        Debug.Log("Remove PlayerTeam characterID[" + character.itemID+"]");

        for (int i = 0; i < teamLists.Length; i++)
        {
            if (teamLists[i] != null && teamLists[i].itemID == character.itemID)
            {
                teamLists[i] = null;
            }
        }
    }


    void Start()
    {
        teamLists = new Character[5];
    }

    void Update()
    {
        //teamLists.Capacity = maxMember;
        SlotsTeamMemberLists.instance.SetMaxInventory(maxMember);
    }
}
