using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] EnemyStat enemyStat;
    [SerializeField] GameObject EnemyControl;

    [SerializeField] AIEnemy aIEnemy;
    [SerializeField] EnemyDefence enemyDefence;

    public void EnemySet()
    {
        enemyDefence.GameEnd = false;
        enemyStat.enemyHp = 1000;
        EnemyControl.transform.position = new Vector3(1.5f, 0, 0);
        EnemyControl.transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    public void StartEnemyCoroutine()
    {
        StartCoroutine(aIEnemy.AIStart());
    }

    public void StopEnemyCoroutine()
    {
        //StopCoroutine(aIEnemy.AIStart());
        //StartCoroutine(aIEnemy.StopMove());
        aIEnemy.StopMove();
    }
}
