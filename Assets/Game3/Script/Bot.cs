using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{

    public delegate void state();
    public state active;

    public int tempTarget;
    public string stating;
    public bool cooldown;

    // Start is called before the first frame update
    void Start()
    {
        active = (state)(StopState);
        active();
    }

    public int tempCountAttack;

    // Update is called once per frame
    void Update()
    {
        stating = active.Method.ToString();

        if (active == StopState && GameSystem.instance.inFight && GameSystem.instance.whoPlayNow.Who != "bot")
        {
            active = (state)(StopState);
            active();
        }
        else if (active == StopState && GameSystem.instance.inFight && GameSystem.instance.whoPlayNow.Who == "bot")
        {
            active = (state)(CalculateState);
            active();
        }
        else if (active == CalculateState && tempCountAttack != 0)
        {
            active = (state)(SelectState);
            active();
        }
        /*else if (active == SelectState && tempCountAttack == -1)
        {
            GameSystem.instance.NextQueue();
            active = (state)(StopState);
            active();
        }*/

    }

    public void CalculateState()
    {
        tempCountAttack = EnemyTeam.instance.Count;
        tempCountAttack--;
    }

    public void StopState()
    {

    }

    public void SelectState()
    {
        tempTarget = Random.Range(0, GameSystem.instance.teamPosition.Length);

        while (GameSystem.instance.teamPosition[tempTarget].GetComponent<DummyCharacter>().GetAttribute == null)
        {
            tempTarget = Random.Range(0, GameSystem.instance.teamPosition.Length);
        }

        PanelTarget.instance.targetCharacter = GameSystem.instance.teamPosition[tempTarget].GetComponent<DummyCharacter>();

        GameSystem.instance.enemyPosition[tempCountAttack].GetComponent<DummyCharacter>().Attack();
        tempCountAttack--;

        if (tempCountAttack != -1)
        {
            StartCoroutine(Delay(done =>
            {
                if (done)
                {
                    active = (state)(SelectState);
                    active();
                }
            }));
        }
        else
        {
            GameSystem.instance.NextQueue();
            active = (state)(StopState);
            active();
        }
    }



    private IEnumerator Delay(System.Action<bool> done)
    {
 
        yield return new WaitForSeconds(5f);
        done(true);
    }
}
