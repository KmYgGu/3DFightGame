using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    //�ɷ�ġ�� ��ȭ�� ���� ������ EventManager�� ȣ��
    [SerializeField] private AnimationTag AniState;
    public AnimationTag aniState => AniState;// ���� ������ ���� �ʴ� �Լ�

    [SerializeField] private float EnemyHp = 1000;

    //public float enemyHp => EnemyHp;
    public float enemyHp 
    {   get => EnemyHp;
        set
        {
            
            EnemyHp = value;

            if (EnemyHpCha != null)
            {
                
                EnemyHpCha.Invoke();
            }
                

        }

    }
    public delegate void EnemyHpChanged();
    public event EnemyHpChanged EnemyHpCha;

    private int EnemyAttackPower = 50;
    public int enemyAttackPower => EnemyAttackPower;

    private AnimationTagReader tagReader;

    [SerializeField] private PlayerStat playerStat;

    [SerializeField]private bool IsAttack = false;

    //public bool isattack => IsAttack;
    public bool isattack
    {
        get => IsAttack;
        set
        {

            IsAttack = value;
                      

        }

    }

    public void ChangeisAttacktrue()
    {
        isattack = true;
    }
    public void ChangeisAttackfalse()
    {
        isattack = false;
    }

    // �浹�� ������Ʈ(����)�� ������ HashSet


    private void Awake()
    {
        tagReader = gameObject.GetComponentInChildren<AnimationTagReader>();

        AniState = AnimationTag.Idle; // ó������ idle������ �ʱ�ȭ

    }


    private void OnEnable()
    {

        //EventManager.Instance.OnCustomEvent3 += OnEventReceived3;
        EventManager.Instance.EnemyAni += OnEventReceived3;

        PlayerAttackBox.EnemyDam += HpDamage;
    }

    private void OnDisable()
    {

        //EventManager.Instance.OnCustomEvent3 -= OnEventReceived3;
        EventManager.Instance.EnemyAni -= OnEventReceived3;

        PlayerAttackBox.EnemyDam -= HpDamage;
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

        

    }

    private void HpDamage(PlayerAttackBox EnemyDam)//���ݿ� ���� ������ ����
    {
        switch (playerStat.aniState)
        {
            case AnimationTag.Attack1:
                enemyHp -= playerStat.playerAttackPower; //50
                
                break;
            case AnimationTag.Attack2:
                enemyHp -= playerStat.playerAttackPower +10;
                ;
                break;
            case AnimationTag.Attack3:
                enemyHp -= playerStat.playerAttackPower + 25;
                
                break;
            case AnimationTag.Attack4:
                enemyHp -= playerStat.playerAttackPower * 2;
                
                break;
            case AnimationTag.Attack5:
                enemyHp -= playerStat.playerAttackPower;
                break;
            case AnimationTag.Attack6:
                enemyHp -= playerStat.playerAttackPower + 20;
                break;
            case AnimationTag.Attack7:
                enemyHp -= playerStat.playerAttackPower;
                break;
            case AnimationTag.Attack8:
                enemyHp -= playerStat.playerAttackPower *3;
                break;
            case AnimationTag.JumpAttack:
                enemyHp -= playerStat.playerAttackPower + 5;
                break;
            case AnimationTag.CounterAttack:
                enemyHp -= playerStat.playerAttackPower;
                break;
            default:
                Debug.Log("EnemyStatcs. ���ݻ��°� �´��� Ȯ��");
                break;

        }

        
        //enemyHp -= 100;
    }
}
