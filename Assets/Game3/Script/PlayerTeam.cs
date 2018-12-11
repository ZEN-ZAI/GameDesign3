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

    [SerializeField] private /*List<Character> teamLists = new List<Character>();*/ Character[] teamLists = new Character[5];
    [SerializeField] private int maxMember;

    public void UpdateMaxMember(int n)
    {
        maxMember += n;
    }

    public bool HaveMember(int index)
    {
       /* if(teamLists[index].cg == null)
        {
            return true;
        }
        else
        {
            return false;
        }*/
    }

    public void AddMember(int memberNumber,Character character)
    {
        teamLists[memberNumber] = character;
        //teamLists[memberNumber] = character;
    }

    void Update()
    {
        SlotsTeamMemberLists.instance.SetMaxInventory(maxMember);
    }
}
