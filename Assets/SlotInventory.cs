using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotInventory : MonoBehaviour, IDropHandler ,IPointerEnterHandler ,IPointerUpHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Debug.Log(eventData.pointerDrag.name + " Drop in " + gameObject.name);
            eventData.pointerDrag.GetComponent<Item>().nowSlot.GetComponent<RectTransform>().GetComponent<CanvasGroup>().blocksRaycasts = true;
            eventData.pointerDrag.GetComponent<Item>().nowSlot = transform;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Debug.Log("Item ["+ eventData.pointerDrag.name+ "] to new slot "+ gameObject.name);
            //eventData.pointerDrag.GetComponent<Item>().SetParentToInventoryUILayer();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //eventData.pointerDrag.GetComponent<Item>().SetParentToInventoryUILayer();
    }
}
