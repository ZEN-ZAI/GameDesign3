using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    #region Singleton
    public static PlayerMotor instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public Transform rootModel;
    public GameObject model;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        

        if (PanelTarget.instance.targetCharacter != null && agent.velocity != Vector3.zero)
        {
            Debug.Log(GetDistanceToPoint());

            if (GetDistanceToPoint() <= 7 && !attack)
            {
                StopToMove();
                tempAnimator.SetTrigger("attack");
                PanelTarget.instance.targetCharacter.ReceiveDamage(tempAtk);
                PanelTarget.instance.UpdateUI();
                agent.SetDestination(tempStartTranform);
                attack = true;
            }
        }

        if (attack && transform.position == tempStartTranform)
        {
            //StopToMove();
            transform.localRotation = Quaternion.identity;
            model.transform.localRotation = Quaternion.identity;
        }
    }
    private Animator tempAnimator;
    private int tempAtk;
    private Vector3 tempStartTranform;
    private bool attack;


    public float GetDistanceToPoint()
    {
        return agent.remainingDistance;
    }

    public void MoveToTarget(Vector3 point, Animator animator,int tempAtk, Vector3 tempStartTranform)
    {
        tempAnimator = animator;
        this.tempAtk = tempAtk;
        this.tempStartTranform = tempStartTranform;
        agent.SetDestination(point);
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void StopToMove()
    {
        agent.velocity = Vector3.zero;
        agent.ResetPath();

    }

    public Vector3 GetVelocity()
    {
        return agent.velocity;
    }
}