using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragInventory : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        UILink.instance.MoveInventoryUI();
    }
}
