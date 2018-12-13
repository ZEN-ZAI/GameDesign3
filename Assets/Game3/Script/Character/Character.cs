using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character",menuName = "New Character/Character")]
public class Character : ScriptableObject
{
    public int itemID;
    public string monsterID = "001";
    new public string name = "New Character";
    public Sprite icon = null;
    public GameObject prefab = null;

    public string rank; 
    public int hp;
    public int atk;
    
}
