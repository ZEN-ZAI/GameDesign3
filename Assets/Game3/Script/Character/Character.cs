using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character",menuName = "New Character/Character")]
public class Character : ScriptableObject
{
    public int itemID;
    public string characterID = "000";
    new public string name = "New Character";
    public Sprite icon = null;
    public GameObject prefab = null;

    public string rank;

    public float currentHp;
    public float maxHp;

    public int attack;
    public int defense;

    public string special = "Not have special.";

    //
    public bool canAttack;
}
