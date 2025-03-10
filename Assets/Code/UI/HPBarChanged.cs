using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarChanged : MonoBehaviour
{
    [SerializeField] private Image PlayerhealthBarImage;
    [SerializeField] private Image PlayerHPSlowImage;

    [SerializeField] private Image EnemyhealthBarImage;
    [SerializeField] private Image EnemyHPSlowBarImage;


    [SerializeField] private PlayerStat playerStat;
    [SerializeField] private EnemyStat enemyStat;

    private float fillSpeed = 1f;

    private void OnEnable()
    {
        enemyStat.EnemyHpCha += UpdateEnemyHealthBar;
        //PlayerAttackBox.EnemyDam += UpdateEnemyHealthBar;

        //EnemyAttackBox.PlayerDam += UpdatePlayerHealthBar;
        playerStat.PlayerHpCha += UpdatePlayerHealthBar;
    }

    private void OnDisable()
    {
        enemyStat.EnemyHpCha -= UpdateEnemyHealthBar;
        //PlayerAttackBox.EnemyDam -= UpdateEnemyHealthBar;

        //EnemyAttackBox.PlayerDam -= UpdatePlayerHealthBar;
        playerStat.PlayerHpCha -= UpdatePlayerHealthBar;
    }
    /*public void TakeDamage(float damage)
    {
        TestHp -= damage;
        TestHp = Mathf.Clamp(TestHp, 0, 1000); // 체력 최소값 제한 maxHealth = 1000
        UpdateHealthBar();
    }*/

    IEnumerator SmoothHealthBarUpdate(float TargetHp, Image TargetHpImage)
    {
        float targetFill = TargetHp / 1000;
        float startFill = TargetHpImage.fillAmount;

        float elapsedTime = 0f;
        while (elapsedTime < fillSpeed)
        {
            elapsedTime += Time.deltaTime;
            TargetHpImage.fillAmount = Mathf.Lerp(startFill, targetFill, elapsedTime / fillSpeed);
            yield return null;
        }

        TargetHpImage.fillAmount = targetFill; // 최종적으로 정확한 값 설정
    }

    void UpdatePlayerHealthBar()
    {
        
        PlayerhealthBarImage.fillAmount = playerStat.playerHp / 1000;
        

        StartCoroutine(SmoothHealthBarUpdate(playerStat.playerHp, PlayerHPSlowImage));
        
    }

    void UpdateEnemyHealthBar()
    {

        //Debug.Log("호출이 되었는가?");
        EnemyhealthBarImage.fillAmount = enemyStat.enemyHp / 1000;
               
        StartCoroutine(SmoothHealthBarUpdate(enemyStat.enemyHp, EnemyHPSlowBarImage));
    }
}
