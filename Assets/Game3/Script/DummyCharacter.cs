using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCharacter : MonoBehaviour
{
    [SerializeField]
    private Character character;
    public GameObject model;

    public bool CanAttack
    {
        set { character.canAttack = value; }
        get { return character.canAttack; }
    }

    public void AddCharacter(Character character)
    {
        this.character = character;
        model = Instantiate(character.prefab, transform);
        GetComponent<PlayerMotor>().rootModel = transform;
        GetComponent<PlayerMotor>().model = model;
    }

    public void ClearCharacter()
    {
        this.character = null;
        Destroy(model);
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

        Vector3 tempStartTranform = transform.position;

        model.transform.LookAt(PanelTarget.instance.targetCharacter.transform);

        
        GetComponent<PlayerMotor>().MoveToTarget(PanelTarget.instance.targetCharacter.transform.position, 
            model.GetComponent<Animator>(),
            this.character.attack,
            tempStartTranform,
            character
            );

        //CanAttack = false;
    }

    /*private IEnumerator CheckStop(System.Action<bool> done, PlayerMotor playerMotor)
    {
        if (playerMotor.GetDistanceToPoint() == )
        {

        }
    }*/

    private IEnumerator Delay(System.Action<bool> done)
    {
        yield return new WaitForSeconds(0.5f);
        done(true);
    }

    public void UseSkill()
    {
        Debug.LogWarning(character.name + "[ID:" + character.itemID + "] Use Skill to " + PanelTarget.instance.targetCharacter.character.name + "[ID:" + PanelTarget.instance.targetCharacter.character.itemID + "]");

        model.transform.LookAt(PanelTarget.instance.targetCharacter.transform);
        PanelTarget.instance.targetCharacter.ReceiveDamage(this.character.attack);

        CanAttack = false;

    }

    public void ReceiveDamage(int damage)
    {
        Debug.LogWarning(character.name + "[ID:" + character.itemID + "] ReceiveDamage: "+ damage);
        int temp = Mathf.Abs(character.defense - damage);
        Debug.LogWarning("temp damage: " + temp);
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
        if (character != null)
        {
            Debug.LogWarning("Click: " + gameObject.name);
            PanelTarget.instance.targetCharacter = gameObject.GetComponent<DummyCharacter>();
            PanelTarget.instance.UpdateUI();
        }
    }
}
