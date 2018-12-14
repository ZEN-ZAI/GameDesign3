using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCharacter : MonoBehaviour
{
    [SerializeField]
    private Character character;
    public GameObject model;

    public void AddCharacter(Character character)
    {
        this.character = character;
        model = Instantiate(character.prefab, transform);
        GetComponent<PlayerMotor>().rootModel = transform;
        GetComponent<PlayerMotor>().model = model;
        character.currentHp = character.maxHp;
    }

    public Character GetAttribute
    {
        get { return character; }
    }

    public void IdleAnimation(bool boolean)
    {
        model.GetComponent<Animator>().SetBool("idle", boolean);
    }

    public void WalkAnimation()
    {
        model.GetComponent<Animator>().SetBool("idle", false);
    }

    public void AttackAnimation()
    {
        model.GetComponent<Animator>().SetTrigger("attack");
    }

    public void Attack()
    {
        Debug.LogWarning(character.name + "[ID:" + character.itemID +"] Attack to "+ PanelTarget.instance.targetCharacter.character.name + "[ID:" + PanelTarget.instance.targetCharacter.character.itemID + "]");
        model.transform.LookAt(PanelTarget.instance.targetCharacter.transform);
        //GetComponent<PlayerMotor>().MoveToPoint(PanelTarget.instance.targetCharacter.transform.position);

        PanelTarget.instance.targetCharacter.ReceiveDamage(this.character.attack);
    }

    public void UseSkill()
    {
        Debug.LogWarning(character.name + "[ID:" + character.itemID + "] Use Skill to " + PanelTarget.instance.targetCharacter.character.name + "[ID:" + PanelTarget.instance.targetCharacter.character.itemID + "]");

        model.transform.LookAt(PanelTarget.instance.targetCharacter.transform);
        //GetComponent<PlayerMotor>().MoveToPoint(PanelTarget.instance.targetCharacter.transform.position);

        PanelTarget.instance.targetCharacter.ReceiveDamage(this.character.attack);

    }

    public void ReceiveDamage(int damage)
    {
        int temp = character.defense - damage;
        this.character.currentHp -= temp;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.character != null && character.currentHp <= 0)
        {
            if (PanelTarget.instance.targetCharacter == this.character)
            {
                PanelTarget.instance.targetCharacter = null;
            }
        }
    }

    void OnMouseDown()
    {
        Debug.LogWarning("enter"+gameObject.name);
        PanelTarget.instance.targetCharacter = gameObject.GetComponent<DummyCharacter>();
    }
}
