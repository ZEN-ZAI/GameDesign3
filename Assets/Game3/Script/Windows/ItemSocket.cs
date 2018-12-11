using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSocket : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler
{
    public Image icon;
    public Button removeButton;
    public Character character;

    public Transform nowSlot;

    public void removeItemInInventory()
    {
        PlayerInventory.instance.Remove(character);
        Destroy(gameObject); // Destroy ItemSocket
    }

    public void AddInstance(Character character)
    {

        icon.sprite = character.icon;
        icon.enabled = true;
        this.character = character;
        removeButton.image.enabled = true;
        removeButton.enabled = true;
    }

    /*public void RemoveInstance(Character character)
    {

        icon.sprite = null;
        icon.enabled = false;
        character = null;
        removeButton.image.enabled = false;
        removeButton.enabled = false;
    }*/

    public void OnBeginDrag(PointerEventData eventData)
    {

        nowSlot = transform.parent;
        transform.SetParent(transform.parent/*.parent.parent.parent.parent.parent*/); // is UI canvas
        nowSlot.GetComponent<RectTransform>().GetComponent<CanvasGroup>().blocksRaycasts = false;

        DummyItem.instance.SetEnable(icon);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

        DummyItem.instance.SetPosition(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DummyItem.instance.SetDisable();

        transform.SetParent(nowSlot);

        if (transform.GetComponent<RectTransform>().position.x != 50) // ออกนอกช่องให้ reset
        {
            nowSlot.GetComponent<RectTransform>().GetComponent<CanvasGroup>().blocksRaycasts = true;
            nowSlot.GetComponent<RectTransform>().GetComponent<GridLayoutGroup>().enabled = false;
            nowSlot.GetComponent<RectTransform>().GetComponent<GridLayoutGroup>().enabled = true;
        }

        //Update to TeamLists
       // SlotsTeamMemberLists.instance.UpdateTeamMember();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Show information.");
    }
}
