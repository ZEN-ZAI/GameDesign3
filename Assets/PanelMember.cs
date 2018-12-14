using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PanelMember : MonoBehaviour, IPointerEnterHandler ,IPointerExitHandler 
{
    public DummyCharacter targetCharacter;

    public GameObject memberPanelButton;

    public Image imageMember;
    public Slider hpBarMember;
    public TextMeshProUGUI hpMember;
    public TextMeshProUGUI nameMember;
    public TextMeshProUGUI atkMember;
    public TextMeshProUGUI defMember;

    public void UpdateInstance()
    {
        imageMember.sprite = targetCharacter.GetAttribute.icon;
        hpBarMember.value = targetCharacter.GetAttribute.currentHp / targetCharacter.GetAttribute.maxHp;
        hpMember.text = targetCharacter.GetAttribute.currentHp + "/" + targetCharacter.GetAttribute.maxHp;
        nameMember.text = targetCharacter.GetAttribute.name;
        atkMember.text = "Atk: " + targetCharacter.GetAttribute.attack.ToString();
        defMember.text = "Def: " + targetCharacter.GetAttribute.defense.ToString();
    }

    public void Attack()
    {
        targetCharacter.Attack();
    }

    public void UseSkill()
    {
        targetCharacter.UseSkill();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        memberPanelButton.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        memberPanelButton.SetActive(false);
    }

    public void Start()
    {
        memberPanelButton.SetActive(false);
    }
}
