using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsTeamMemberLists : MonoBehaviour
{
    #region Singleton
    public static SlotsTeamMemberLists instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public Transform teamContent;
    public GameObject slotInventoryPrefeb;
    public List<GameObject> slotsTeamLists = new List<GameObject>();
    private int maxMember;

    public int Count
    {
        get { return slotsTeamLists.Count; }
    }

    public int GetMaxMember()
    {
        return maxMember;
    }

    public void SetMaxInventory(int n)
    {
        maxMember = n;
    }

    public void AddSlot()
    {
        if (slotsTeamLists.Count < maxMember)
        {
            Debug.LogWarning("Add Slot.");

            GameObject slotInventory = Instantiate(slotInventoryPrefeb, teamContent);
            slotsTeamLists.Add(slotInventory);
        }
    }

    public void RemoveSlot()
    {
        if (slotsTeamLists.Count > 1)
        {
            Debug.LogWarning("Remove Slot.");

            slotsTeamLists.Remove(slotsTeamLists[slotsTeamLists.Count - 1]);
            slotsTeamLists.Capacity--;
            Destroy(teamContent.GetChild(slotsTeamLists.Count).gameObject);
        }
    }

    void Start()
    {
        if (teamContent.childCount != 0)
        {
            for (int i = 0; i < teamContent.childCount; i++)
            {
                Destroy(teamContent.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < maxMember; i++)
        {
            AddSlot();
        }
    }

    public void UpdateTeamMember()
    {


        /*int index = slotsTeamLists.FindIndex(e => e.transform.childCount == 1);
        PlayerTeam.instance.AddMember(index,
        slotsTeamLists[index].transform.GetChild(0).GetComponent<ItemSocket>().character
        );*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Count < maxMember)
        {
            AddSlot();
        }
        else if (Count > maxMember)
        {
            RemoveSlot();
        }

        for (int i = 0; i < teamContent.childCount; i++)
        {
            if (teamContent.GetChild(i).childCount == 1 && !PlayerTeam.instance.HaveMember(i))
            {
                Debug.Log(" Child: " + i + " " + teamContent.GetChild(i).GetComponent<ItemSocket>());
                PlayerTeam.instance.AddMember(i, teamContent.GetChild(i).GetChild(0).GetComponent<ItemSocket>().character);
            }
        }
    }
}
