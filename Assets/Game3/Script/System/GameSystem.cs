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

    public Transform[] teamPosition;

    public Transform[] enemyPosition;

    public Queue<WhoPlay> queuePlay = new Queue<WhoPlay>();
    public WhoPlay whoPlayNow;

    void Start()
    {
        NormalCameraEnable();
        active = (state)(NormalState);
        active();
    }

    void Update()
    {

        if (active == NormalState && inFight) //  normal to fight
        {
            mainUI.SetActive(false);
            FightCameraEnable();

            RandomEnemy();
            SpawnPlayerTeam();
            UILink.instance.SetInstanceMemberPanel(teamPosition);
            SpawnEnemyTeam();
            RandomFightFirst();
            whoPlayNow = queuePlay.Peek();

            if (whoPlayNow.Who == "player") //ถึงคืว
            {
                PlayerTeamCanAttack(true);
                BotTeamCanAttack(false);
            }
            else if (whoPlayNow.Who == "bot")
            {
                BotTeamCanAttack(true);
                PlayerTeamCanAttack(false);
            }

            CameraAnimation.instance.MoveToBack();

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
            NormalCameraEnable();
            CameraAnimation.instance.MoveToFront();
            ClearFightState();

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

    int tempPlayerCantAttack;
    int tempBotCantAttack;
    private void FightState()
    {

        if (whoPlayNow.Who == "player")
        {
            for (int i = 0; i < PlayerTeam.instance.Count; i++)
            {
                if (!PlayerTeam.instance.NullCharacter(i) && PlayerTeam.instance.GetCharacterCanAttack(i) == false)
                {
                    tempPlayerCantAttack++;

                    if (tempPlayerCantAttack == PlayerTeam.instance.Count)
                    {
                        NextQueue();
                    }
                }
            }
        }
        else if (whoPlayNow.Who == "bot")
        {
            for (int i = 0; i < EnemyTeam.instance.teamLists.Count; i++)
            {
                if (EnemyTeam.instance.teamLists[i].canAttack == false)
                {
                    tempPlayerCantAttack++;

                    if (tempPlayerCantAttack == EnemyTeam.instance.teamLists.Count)
                    {
                        NextQueue();
                    }
                }
            }
        }
    }

    private void BotTeamCanAttack(bool boolean)
    {
        for (int i = 0; i < EnemyTeam.instance.teamLists.Count; i++)
        {
            if (EnemyTeam.instance.teamLists[i] != null)
            {
                EnemyTeam.instance.teamLists[i].canAttack = boolean;
            }
        }
    }

    private void PlayerTeamCanAttack(bool boolean)
    {
        for (int i = 0; i < PlayerTeam.instance.Count; i++)
        {
            if (!PlayerTeam.instance.NullCharacter(i))
            {
                PlayerTeam.instance.SetCharacterCanAttack(i, boolean);

            }
        }
    }

    private void RandomFightFirst()
    {
        int temp = Random.Range(0,2);

        if (temp == 0)
        {
            Debug.Log("Player First");
            queuePlay.Enqueue(new WhoPlay("player"));
            queuePlay.Enqueue(new WhoPlay("bot"));
        }
        else
        {
            Debug.Log("Bot First");
            queuePlay.Enqueue(new WhoPlay("bot"));
            queuePlay.Enqueue(new WhoPlay("player"));

        }
        
    }

    private void NormalState()
    {

    }

    private IEnumerator Delay(System.Action<bool> done)
    {
        yield return new WaitForSeconds(0.5f);
        done(true);
    }

    public void NextQueue()
    {
        queuePlay.Dequeue();
        queuePlay.Enqueue(whoPlayNow);
        whoPlayNow = queuePlay.Peek();

        Debug.Log("Queue: "+ whoPlayNow.Who);

        if (whoPlayNow.Who == "player")
        {
            PlayerTeamCanAttack(true);
            BotTeamCanAttack(false);
        }
        else if (whoPlayNow.Who == "bot")
        {
            BotTeamCanAttack(true);
            PlayerTeamCanAttack(false);
        }
        else
        {
            Debug.LogError("Queue Error: " + whoPlayNow.Who);
        }
    }

    public void ClearFightState()
    {
        ClearBotDummyTeam();
        ClearPlayerDummyTeam();
        EnemyTeam.instance.teamLists.Clear();
        queuePlay.Clear();
    }

    public void RandomEnemy()
    {
        int temp = Random.Range(2, 8);

        for (int i = 0; i < temp; i++)
        {
            EnemyTeam.instance.AddMember(DataBase.instance.RandomItem());
        }
    }

    public void SpawnPlayerTeam()
    {
        for (int i = 0; i < PlayerTeam.instance.Count; i++)
        {
            if (!PlayerTeam.instance.NullCharacter(i) && teamPosition[i].GetComponent<DummyCharacter>() != null)
            {
                teamPosition[i].GetComponent<DummyCharacter>().AddCharacter(PlayerTeam.instance.GetCharacter(i));
            }
        }
    }

    public void ClearPlayerDummyTeam()
    {
        for (int i = 0; i < PlayerTeam.instance.Count; i++)
        {
            if (!PlayerTeam.instance.NullCharacter(i))
            {
                teamPosition[i].GetComponent<DummyCharacter>().ClearCharacter();
            }
        }
    }

    public void ClearBotDummyTeam()
    {
        for (int i = 0; i < EnemyTeam.instance.teamLists.Count; i++)
        {
            if (EnemyTeam.instance.teamLists[i] != null)
            {
                enemyPosition[i].GetComponent<DummyCharacter>().ClearCharacter();
            }
        }
    }

    public void SpawnEnemyTeam()
    {
        for (int i = 0; i < EnemyTeam.instance.teamLists.Count; i++)
        {
            if (EnemyTeam.instance.teamLists[i] != null && enemyPosition[i].GetComponent<DummyCharacter>() != null)
            {
                enemyPosition[i].GetComponent<DummyCharacter>().AddCharacter(EnemyTeam.instance.teamLists[i]);
            }
        }
    }

    public void SetFight()
    {
        inFight = true;
    }

    public void SetNormal()
    {
        inFight = false;
    }

    private void FightCameraEnable()
    {
        normalStateCamera.enabled = false;
        fightStateCamera.enabled = true;
    }

    private void NormalCameraEnable()
    {
        normalStateCamera.enabled = true;
        fightStateCamera.enabled = false;
    }
}
[System.Serializable]
public class WhoPlay 
{
    public string namePlayer;

    public WhoPlay(string namePlayer)
    {
        this.namePlayer = namePlayer;
    }

    public string Who
    {
       get { return this.namePlayer; }
    }
}

