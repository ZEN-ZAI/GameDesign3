using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    public float speed;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void MoveToBack()
    {
        animator.SetTrigger("MoveToBack");
    }

    public void MoveToFront()
    {
        animator.SetTrigger("MoveToFront");
    }
}
