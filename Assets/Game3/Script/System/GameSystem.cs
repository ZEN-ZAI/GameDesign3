using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DataBase))]
public class GameSystem : MonoBehaviour
{
    #region Singleton
    public static GameSystem instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public Camera normalStateCamera;
    public Camera fightStateCamera;
    public GameObject mainUI;

    public delegate void state();
    public state active;

    public bool inFight;

    void Start()
    {
        active = (state)(NormalState);
        active();
    }

    void Update()
    {

        if (active == NormalState && inFight) //  normal to fight
        {
            mainUI.SetActive(false);
            active = (state)(FightState);
            active();
        }
        else if (active == NormalState && !inFight) //  normal to normal
        {
            active = (state)(NormalState);
            active();
        }
        else if (active == FightState && inFight) //  fight to fight
        {
            active = (state)(FightState);
            active();
        }
        else if (active == FightState && !inFight) //  fight to normal
        {
            mainUI.SetActive(true);
            active = (state)(NormalState);
            active();
        }
    }

    public void RandomFight()
    {
        StartCoroutine(Delay(done =>
        {
            if (done)
            {
                int temp = Random.Range(0, 20);
                if (temp == 0)
                {
                    SetFight();
                }
            }
        }));
    }

    private IEnumerator Delay(System.Action<bool> done)
    {
        yield return new WaitForSeconds(0.5f);
        done(true);
    }

    public void SetFight()
    {
        inFight = true;
    }

    public void SetWalk()
    {
        inFight = false;
    }

    private void FightState()
    {
        FightCameraEnable();
    }
    private void NormalState()
    {
        WalkCameraEnable();
    }

    private void FightCameraEnable()
    {
        normalStateCamera.enabled = false;
        fightStateCamera.enabled = true;
    }

    private void WalkCameraEnable()
    {
        normalStateCamera.enabled = true;
        fightStateCamera.enabled = false;
    }
}
