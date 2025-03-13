using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] EnemyManager enemyManager;

    [SerializeField] RoundMessageChanged roundMessage;
    [SerializeField] Timer timer;

    int NowRoundis = 0;
    bool canmove;

    private void Start()
    {
        EventManager.Instance.PlayerDied += PlayerLose;
        EventManager.Instance.EnemyDied += PlayerWin;


        StartCoroutine(RoundStart());
    }

    IEnumerator RoundStart()
    {
        NowRoundis++;
        canmove = false;
        playerManager.PlayerSet();
        enemyManager.EnemySet();
        yield return StartCoroutine(roundMessage.RoundFight(NowRoundis));

        StartCoroutine(timer.CountdownTimer());
        Debug.Log("fight!");

        canmove = true;
        //playerManager.StartJumpCoroutine();

        enemyManager.StartEnemyCoroutine();
    }

    private void Update()
    {
        if (canmove)
        {
            playerManager.PlayerAllMove(canmove);
        }
    }

    private void OnEnable()
    {
        
        //EventManager.Instance.PlayerDied += PlayerLose;
        //EventManager.Instance.EnemyDied += PlayerWin;
    }

    private void OnDisable()
    {
        EventManager.Instance.PlayerDied -= PlayerLose;
        EventManager.Instance.EnemyDied -= PlayerWin;
    }

    void PlayerLose()
    {
        canmove = false;
        StopCoroutine();
        StartCoroutine(Reslult("lose"));
    }

    void PlayerWin()
    {
        canmove = false;
        StopCoroutine();
        StartCoroutine(Reslult("win"));
    }

    IEnumerator Reslult(string win)
    {
        yield return StartCoroutine(roundMessage.WhoWin(win));

        yield return new WaitForSeconds(1);
        StartCoroutine(RoundStart());
    }

    void StopCoroutine()
    {
        //playerManager.STopJumpCoroutine();

        enemyManager.StopEnemyCoroutine();
        //StopAllCoroutines();
    }
}
