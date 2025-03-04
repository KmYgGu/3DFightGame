using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AnimationTag
{
    None = 0,
    Idle = 1,
    move = 2,
    Jump = 3,
    Attack = 4
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
        tagDictionary[Animator.StringToHash("move")] = AnimationTag.move;
        tagDictionary[Animator.StringToHash("Jump")] = AnimationTag.Jump;
        tagDictionary[Animator.StringToHash("Attack")] = AnimationTag.Attack;

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
