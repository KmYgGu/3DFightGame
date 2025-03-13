using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySwordWind;

public class PlayerStat : MonoBehaviour
{
    //능력치에 변화가 있을 때마다 EventManager를 호출
    [SerializeField]private AnimationTag AniState;
    public AnimationTag aniState => AniState;// 값을 가지고 있지 않는 함수

    [SerializeField]private float PlayerHp = 1000;
    //public float playerHp => PlayerHp;
    public float playerHp
    {
        get => PlayerHp;
        set
        {

            PlayerHp = value;

            if (PlayerHpCha != null)
            {

                PlayerHpCha.Invoke();
            }


        }

    }
    public delegate void PlayerHpChanged();
    public event PlayerHpChanged PlayerHpCha;

    private int PlayerAttackPower = 100;
    public int playerAttackPower => PlayerAttackPower;

    private AnimationTagReader tagReader;

    [SerializeField] private EnemyStat enemyStat;

    [SerializeField] private bool isGuarding = false;
    public bool ISGuarding
    {
        get => isGuarding;
        set => isGuarding = value;
    }



    [SerializeField] private bool isAttack = false;
     

    public bool isattack => isAttack;

    public void ChangeisAttacktrue()
    {
        isAttack = true;
    }
    public void ChangeisAttackfalse()
    {
        isAttack = false;
    }

    private void Awake()
    {
        tagReader = gameObject.GetComponentInChildren<AnimationTagReader>();

        AniState = AnimationTag.Idle; // 처음에는 idle값으로 초기화
        
    }

    private void OnEnable()
    {
        
        EventManager.Instance.OnCustomEvent3 += OnEventReceived3;

        EnemyAttackBox.PlayerDam += HpDamage;

        EnemySwordWind.PlayerSWDam += ESWDamage;
    }

    private void OnDisable()
    {
        
        EventManager.Instance.OnCustomEvent3 -= OnEventReceived3;

        EnemyAttackBox.PlayerDam -= HpDamage;

        EnemySwordWind.PlayerSWDam -= ESWDamage;
    }

    private void OnEventReceived3()
    {
        StartCoroutine("GetAnimationTag");
    }

    private IEnumerator GetAnimationTag()
    {
        
        yield return new WaitForEndOfFrame(); // 한 프레임 대기 후 실행

        AniState = tagReader.GetCurrentAnimationTag();
        
        tagReader.GetCurrentAnimationTag();
                
        //Debug.Log("플레이어 현재 애니메이션 태그: " + AniState);

    }

    private void HpDamage(EnemyAttackBox PlayerDam)
    {
        switch (enemyStat.aniState)
        {
            case AnimationTag.Attack1:
                playerHp -= enemyStat.enemyAttackPower; //10
                break;
            case AnimationTag.Attack2:
                playerHp -= enemyStat.enemyAttackPower + 2;
                break;
            case AnimationTag.Attack3:
                playerHp -= enemyStat.enemyAttackPower + 5;
                break;
            case AnimationTag.Attack4:
                playerHp -= enemyStat.enemyAttackPower * 2;
                break;
            case AnimationTag.Attack5:
                playerHp -= enemyStat.enemyAttackPower;
                break;
            case AnimationTag.Attack6:
                playerHp -= enemyStat.enemyAttackPower + 2;
                break;
            case AnimationTag.Attack7:
                playerHp -= enemyStat.enemyAttackPower;
                break;
            case AnimationTag.Attack8:
                playerHp -= enemyStat.enemyAttackPower * 3;
                break;
            case AnimationTag.JumpAttack:
                playerHp -= enemyStat.enemyAttackPower;
                break;
            case AnimationTag.CounterAttack:
                playerHp -= enemyStat.enemyAttackPower;
                break;
            default:
                //Debug.Log("EnemyStatcs. 공격상태가 맞는지 확인");
                break;

        }
        // 체력이 0 이하로 떨어졌을 때 디버그 로그 출력
        if (playerHp <= 0)
        {
            //Debug.Log("플레이어 체력이 0 이하가 되었습니다!");
            EventManager.Instance.PlayerDieEvent();

        }


    }

    private void ESWDamage(EnemySwordWind SWD)
    {
        playerHp -= enemyStat.enemyAttackPower * 3;
        if (playerHp <= 0)
        {
            //Debug.Log("플레이어 체력이 0 이하가 되었습니다!");
            EventManager.Instance.PlayerDieEvent();
        }
    }

}
