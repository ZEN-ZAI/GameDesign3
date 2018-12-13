using System;
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

    public FormSlot formSlot = new FormSlot();
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

    void Start()
    {
        nowSlot = transform.parent;
        formSlot.oldSlot = nowSlot;
        formSlot.character = character;
    }

    void Update()
    {
        if (gameObject.transform.parent.tag == "InventorySlot")
        {
            removeButton.image.enabled = true;
            removeButton.enabled = true;
        }
        else
        {
            removeButton.image.enabled = false;
            removeButton.enabled = false;
        }
    }

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

        if (formSlot.oldSlot.tag == "InventorySlot" && nowSlot.tag == "TeamMemberSlot")
        {
            SlotsTeamMemberLists.instance.UpdateTeamMember();
            PlayerInventory.instance.Remove(character);
        }
        else if (formSlot.oldSlot.tag == "TeamMemberSlot" && nowSlot.tag == "InventorySlot")
        {
            PlayerTeam.instance.Remove(character);
            PlayerInventory.instance.Add(character);
        }
        else if (formSlot.oldSlot.tag == "TeamMemberSlot" && nowSlot.tag == "TeamMemberSlot")
        {
            PlayerTeam.instance.Remove(character);
            SlotsTeamMemberLists.instance.UpdateTeamMember();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Show information.");
    }
}
public class FormSlot
{
   public Transform oldSlot;
   public Character character;
}
