using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    #region Singleton
    public static PlayerController instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public Animator animator;
    public LayerMask movementMask;      // The ground
    private PlayerMotor motor;      // Reference to our motor
    private Camera cam;				// Reference to our camera

    public delegate void state();
    public state active;

    public bool randomFight;
    public Vector3 velocity;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        MainCharacter();
        active = (state)(Idle);
        active();
    }

    void Update()
    {

        velocity = motor.GetVelocity();
        PlayerFSM();
        MainCharacter();

    }

    void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerInventory.instance.AddItem(DataBase.instance.RandomItem());
        }

        if (Input.GetMouseButtonDown(0) && active != Fight && !UILink.instance.haveWindowInOpen && !PlayerTeam.instance.NullCharacter(0))
        {
            // Shoot out a ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If we hit
            if (Physics.Raycast(ray, out hit, movementMask))
            {
                motor.MoveToPoint(hit.point);
            }
        }

    }

    private void MainCharacter()
    {
        if (!PlayerTeam.instance.NullCharacter(0) && motor.rootModel.childCount == 0)
        {
            motor.model = Instantiate(PlayerTeam.instance.GetCharacter(0).prefab, motor.rootModel);
            animator = motor.model.GetComponent<Animator>();
        }

        if (PlayerTeam.instance.NullCharacter(0) && motor.rootModel.childCount == 1)
        {
            Destroy(motor.model.gameObject);
        }
    }

    private void PlayerFSM()
    {
        // State Idle to ...
        if (active == Idle && motor.GetVelocity() != Vector3.zero &&! PlayerTeam.instance.NullCharacter(0))
        {
            active = (state)(Walk);
            active();
        }
        else if (active == Idle && GameSystem.instance.inFight && !PlayerTeam.instance.NullCharacter(0))
        {
            active = (state)(Fight);
            active();
        }
        /*else if (active == Idle)
        {
            active = (state)(Idle);
            active();
        }*/

        // State Walk to ...
        else if (active == Walk && motor.GetVelocity() == Vector3.zero)
        {
            active = (state)(Idle);
            active();
        }
        else if (active == Walk && GameSystem.instance.inFight)
        {
            motor.StopToMove();

            active = (state)(Fight);
            active();
        }
        else if (active == Walk)
        {
            active = (state)(Walk);
            active();
        }

        // State Fight to ...
        else if (active == Fight && !GameSystem.instance.inFight)
        {
            active = (state)(Idle);
            active();
        }
        /*else if (active == Fight)
        {
            active = (state)(Fight);
            active();
        }*/
    }

    public void StopWalk()
    {
        motor.StopToMove();
    }

    private void Idle()
    {
        if (!PlayerTeam.instance.NullCharacter(0))
        {
            animator.SetBool("idle", true);
        }
        
    }

    private void Walk()
    {
        if (!PlayerTeam.instance.NullCharacter(0))
        {
            animator.SetBool("idle", false);
        }
        if (randomFight)
        {
            GameSystem.instance.RandomFight();
        }
    }

    private void Fight()
    {

    }
}
