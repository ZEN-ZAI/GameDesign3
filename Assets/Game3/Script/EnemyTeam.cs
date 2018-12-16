using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeam : MonoBehaviour
{
    #region Singleton
    public static EnemyTeam instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public List<Character> teamLists = new List<Character>();
    [SerializeField] private int maxMember;
    public int Count
    {
        get { return teamLists.Count; }
    }

    public void AddMember(Character character)
    {
        Debug.Log("Add EnemyTeam characterID[" + character.itemID + "]");
        teamLists.Add(character);
    }
}