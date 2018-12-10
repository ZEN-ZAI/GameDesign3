using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotInventory : MonoBehaviour, IDropHandler ,IPointerEnterHandler
{

    public Item itemInSlot;

    void Start()
    {
        if (transform.childCount == 1)
        {
            itemInSlot = transform.GetChild(0).GetComponent<Item>();
        }
        else if (transform.childCount > 1)
        {
            Debug.LogError("[Inventory Slot] Item over in 1 slot.");
        }
    }

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

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && transform.childCount == 0)
        {
            Debug.Log(eventData.pointerDrag.name + " Drop in " + gameObject.name);
            eventData.pointerDrag.GetComponent<Item>().nowSlot.GetComponent<RectTransform>().GetComponent<CanvasGroup>().blocksRaycasts = true;
            eventData.pointerDrag.GetComponent<Item>().nowSlot = transform;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && transform.childCount == 0)
        {
            Debug.Log("Item ["+ eventData.pointerDrag.name+ "] to new slot "+ gameObject.name);
        }
    }
}
