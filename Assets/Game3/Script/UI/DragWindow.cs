using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        UILink.instance.MoveInventoryUI();
    }
}
