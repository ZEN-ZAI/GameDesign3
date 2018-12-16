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

    [SerializeField] private List<Character> teamLists = new List<Character>();
    [SerializeField] private int maxMember;
    public int Count
    {
        get { return teamLists.Count; }
    }

    public void AddMaxMember()
    {
        maxMember++;
    }

    public void AddMember(int memberNumber, Character character)
    {
        Debug.Log("Add PlayerTeam characterID[" + character.itemID + "] in Slot[" + memberNumber + "]");
        teamLists[memberNumber] = character;
    }

    public void Remove(Character character)
    {
        Debug.Log("Remove PlayerTeam characterID[" + character.itemID + "]");

        for (int i = 0; i < teamLists.Count; i++)
        {
            if (teamLists[i] != null && teamLists[i].itemID == character.itemID)
            {
                teamLists[i] = null;
            }
        }
    }

    public Character GetCharacter(int index)
    {
        return teamLists[index];
    }

    public bool GetCharacterCanAttack(int index)
    {
        return teamLists[index].canAttack;
    }

    public void SetCharacterCanAttack(int index, bool boolean)
    {
         teamLists[index].canAttack = boolean;
    }

    public bool NullCharacter(int index)
    {
        if (teamLists[index] == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Start()
    {
        for (int i = 0; i < maxMember; i++)
        {
            teamLists.Add(null);
        }
    }

    void Update()
    {
        if (teamLists.Count < maxMember)
        {
            teamLists.Add(null);
        }
        else if (teamLists.Count > maxMember)
        {
            teamLists.Remove(null);
        }
        SlotsTeamMemberLists.instance.SetMaxInventory(maxMember);
    }
}
