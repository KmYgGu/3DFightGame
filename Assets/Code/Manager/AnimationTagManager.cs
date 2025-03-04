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

    //애니메이션 상태를 저장할 백과사전
    private Dictionary<int, AnimationTag> tagDictionary = new Dictionary<int, AnimationTag>();

    private void Awake()
    {
        // 싱글톤 설정
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
        // Animator의 태그 이름을 해시값으로 변환하여 Dictionary에 저장
        tagDictionary[Animator.StringToHash("Idle")] = AnimationTag.Idle;
        tagDictionary[Animator.StringToHash("move")] = AnimationTag.move;
        tagDictionary[Animator.StringToHash("Jump")] = AnimationTag.Jump;
        tagDictionary[Animator.StringToHash("Attack")] = AnimationTag.Attack;

    }

    // 애니메이션 태그 조회
    public AnimationTag GetAnimationTag(int tagHash)
    {
        if (tagDictionary.TryGetValue(tagHash, out AnimationTag tag))
        {
            return tag;
        }
        return AnimationTag.None;
    }
}
