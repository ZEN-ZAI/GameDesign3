using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTeamSystem : MonoBehaviour
{

    public Transform[] team;

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < PlayerTeam.instance.Count; i++)
        {
            if (!PlayerTeam.instance.NullCharacter(i) && team[i].childCount == 0)
            {
                Instantiate(PlayerTeam.instance.GetCharacter(i).prefab, team[i]);
            }
            else if (PlayerTeam.instance.NullCharacter(i) && team[i].childCount == 1)
            {
                Destroy(team[i].GetChild(0).gameObject);
            }
        }
        
    }
}
