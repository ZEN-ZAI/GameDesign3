﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotTeam : SlotInventory
{

    void Update()
    {
        if (transform.childCount == 1)
        {
            itemInSlot = transform.GetChild(0).GetComponent<Item>();

            
        }
        else
        {
            itemInSlot = null;
        }
    }

}