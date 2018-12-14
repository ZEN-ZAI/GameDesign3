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
    public PanelMember[] panelMember;
    public Transform[] teamPosition;
    public Transform[] enemyPosition;
    public Queue<Character> queueCharacter = new Queue<Character>();

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

            SpawnPlayerTeam();
            SpawnEnemyTeam();
            SetInstanceMemberPanel();


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

    public void Enqueue()
    {

    }

    public void Dequeue()
    {

    }

    public void RandomEnemy()
    {

    }

    public void SetInstanceMemberPanel()
    {
        for (int i = 0; i < panelMember.Length; i++)
        {
            if (panelMember[i] != null && PlayerTeam.instance.teamLists[i] != null)
            {
                panelMember[i].targetCharacter = teamPosition[i].GetComponent<DummyCharacter>();
                panelMember[i].UpdateInstance();
            }
        }
    }

    public void SpawnPlayerTeam()
    {
        for (int i = 0; i < PlayerTeam.instance.teamLists.Length; i++)
        {
            if (PlayerTeam.instance.teamLists[i] != null)
            {
                //teamPosition[i].GetComponent<DummyCharacter>().AddCharacter(Instantiate(PlayerTeam.instance.teamLists[i], teamPosition[i]));
                teamPosition[i].GetComponent<DummyCharacter>().AddCharacter(PlayerTeam.instance.teamLists[i]);
            }
        }
    }

    public void SpawnEnemyTeam()
    {
        /*for (int i = 0; i < PlayerTeam.instance.teamLists.Length; i++)
        {
            if (PlayerTeam.instance.teamLists[i] != null)
            {
                teamPosition[i].GetComponent<DummyCharacter>().AddCharacter(Instantiate(PlayerTeam.instance.teamLists[i], teamPosition[i]));
            }
        }*/
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
