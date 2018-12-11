using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler ,IPointerEnterHandler
{

    public ItemSocket ItemSocketInSlot;

    void Start()
    {
        if (transform.childCount == 1)
        {
            ItemSocketInSlot = transform.GetChild(0).GetComponent<ItemSocket>();
        }
        else if (transform.childCount > 1)
        {
            Debug.LogError("[Inventory Slot] ItemSocket over in 1 slot.");
        }
    }

    void Update()
    {
        if (transform.childCount == 1)
        {
            ItemSocketInSlot = transform.GetChild(0).GetComponent<ItemSocket>();
        }
        else
        {
            ItemSocketInSlot = null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<ItemSocket>() != null && transform.childCount == 0)
        {
            Debug.Log(eventData.pointerDrag.name + " Drop in " + gameObject.name);
            eventData.pointerDrag.GetComponent<ItemSocket>().nowSlot.GetComponent<RectTransform>().GetComponent<CanvasGroup>().blocksRaycasts = true;
            eventData.pointerDrag.GetComponent<ItemSocket>().nowSlot = transform;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<ItemSocket>() != null &&  transform.childCount == 0)
        {
            Debug.Log("["+ eventData.pointerDrag.name+ "] to new slot "+ gameObject.name);
        }
    }
}
