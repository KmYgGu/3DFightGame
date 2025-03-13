using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySwordWind;

public class PlayerStat : MonoBehaviour
{
    //�ɷ�ġ�� ��ȭ�� ���� ������ EventManager�� ȣ��
    [SerializeField]private AnimationTag AniState;
    public AnimationTag aniState => AniState;// ���� ������ ���� �ʴ� �Լ�

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

        AniState = AnimationTag.Idle; // ó������ idle������ �ʱ�ȭ
        
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
        
        yield return new WaitForEndOfFrame(); // �� ������ ��� �� ����

        AniState = tagReader.GetCurrentAnimationTag();
        
        tagReader.GetCurrentAnimationTag();
                
        //Debug.Log("�÷��̾� ���� �ִϸ��̼� �±�: " + AniState);

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
                //Debug.Log("EnemyStatcs. ���ݻ��°� �´��� Ȯ��");
                break;

        }
        // ü���� 0 ���Ϸ� �������� �� ����� �α� ���
        if (playerHp <= 0)
        {
            //Debug.Log("�÷��̾� ü���� 0 ���ϰ� �Ǿ����ϴ�!");
            EventManager.Instance.PlayerDieEvent();

        }


    }

    private void ESWDamage(EnemySwordWind SWD)
    {
        playerHp -= enemyStat.enemyAttackPower * 3;
        if (playerHp <= 0)
        {
            //Debug.Log("�÷��̾� ü���� 0 ���ϰ� �Ǿ����ϴ�!");
            EventManager.Instance.PlayerDieEvent();
        }
    }

}
