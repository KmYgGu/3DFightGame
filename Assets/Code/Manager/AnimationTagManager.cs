using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AnimationTag
{
    None = 0,
    Idle = 1,
    walk = 2,
    Jump = 3,
    Attack = 4,
    Guard = 5,
    JumpAttack = 6,
    Run = 7,
    GuardSucess = 8,
    CounterAttack = 9,
    sDamage = 10,
    mDamage = 11,
}
public class AnimationTagManager : MonoBehaviour
{
    
    
    public static AnimationTagManager Instance { get; private set; }

    //�ִϸ��̼� ���¸� ������ �������
    private Dictionary<int, AnimationTag> tagDictionary = new Dictionary<int, AnimationTag>();

    private void Awake()
    {
        // �̱��� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeTagDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeTagDictionary()
    {
        // Animator�� �±� �̸��� �ؽð����� ��ȯ�Ͽ� Dictionary�� ����
        tagDictionary[Animator.StringToHash("Idle")] = AnimationTag.Idle;
        tagDictionary[Animator.StringToHash("walk")] = AnimationTag.walk;
        tagDictionary[Animator.StringToHash("Jump")] = AnimationTag.Jump;
        tagDictionary[Animator.StringToHash("Attack")] = AnimationTag.Attack;
        tagDictionary[Animator.StringToHash("Guard")] = AnimationTag.Guard;
        tagDictionary[Animator.StringToHash("JumpAttack")] = AnimationTag.JumpAttack;
        tagDictionary[Animator.StringToHash("run")] = AnimationTag.Run;
        tagDictionary[Animator.StringToHash("GuardSucess")] = AnimationTag.GuardSucess;
        tagDictionary[Animator.StringToHash("CounterAttack")] = AnimationTag.CounterAttack;
        tagDictionary[Animator.StringToHash("sDamage")] = AnimationTag.sDamage;
        tagDictionary[Animator.StringToHash("mDamage")] = AnimationTag.mDamage;


    }

    // �ִϸ��̼� �±� ��ȸ
    public AnimationTag GetAnimationTag(int tagHash)
    {
        if (tagDictionary.TryGetValue(tagHash, out AnimationTag tag))
        {
            return tag;
        }
        return AnimationTag.None;
    }

    
}
