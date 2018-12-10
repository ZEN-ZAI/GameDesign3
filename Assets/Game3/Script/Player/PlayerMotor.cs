using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void StopToMove()
    {
        //agent.velocity = Vector3.zero;
        agent.velocity = Vector3.zero;
        agent.ResetPath();

    }

    public Vector3 GetVelocity()
    {
        return agent.velocity;
    }


}