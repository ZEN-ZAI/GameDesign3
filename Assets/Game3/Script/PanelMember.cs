using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PanelMember : MonoBehaviour, IPointerEnterHandler ,IPointerExitHandler 
{
    public GameObject memberPanelButton;

    public Image imageMember;
    public Image hpBarMember;
    public TextMeshProUGUI hpMember;
    public TextMeshProUGUI nameMember;
    public TextMeshProUGUI atkMember;
    public TextMeshProUGUI defMember;

    public DummyCharacter targetCharacter;

    public void UpdateInstance()
    {
        imageMember.sprite = targetCharacter.GetAttribute.icon;
        hpBarMember.fillAmount = targetCharacter.GetAttribute.currentHp / targetCharacter.GetAttribute.maxHp;
        hpMember.text = targetCharacter.GetAttribute.currentHp + "/" + targetCharacter.GetAttribute.maxHp;
        nameMember.text = targetCharacter.GetAttribute.name;
        atkMember.text = "Atk: " + targetCharacter.GetAttribute.attack.ToString();
        defMember.text = "Def: " + targetCharacter.GetAttribute.defense.ToString();
    }

    void Update()
    {

        if (GameSystem.instance.inFight && targetCharacter != null)
        {
            hpBarMember.fillAmount = targetCharacter.GetAttribute.currentHp / targetCharacter.GetAttribute.maxHp;
            hpMember.text = targetCharacter.GetAttribute.currentHp + "/" + targetCharacter.GetAttribute.maxHp;
        }
    }

    public void Attack()
    {
        targetCharacter.Attack();
        memberPanelButton.SetActive(false);
    }

    public void UseSkill()
    {
        targetCharacter.UseSkill();
        memberPanelButton.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetCharacter.CanAttack)
        {
            memberPanelButton.SetActive(true);
        }
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
