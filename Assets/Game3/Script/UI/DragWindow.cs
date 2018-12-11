using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragWindow : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        if (transform.tag == "InventoryWindow")
        {
            UILink.instance.MoveInventoryWindow();
        }
        else if (transform.tag == "TeamWindow")
        {
            UILink.instance.MoveTeamWindow();
        }
    }
}
