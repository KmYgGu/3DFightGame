using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTagReader : MonoBehaviour
{
    private Animator animator;
    //private PlayerJump playerJump;
    private ChangeFaceMaterial changeFace;

    private int animHash_isGround = Animator.StringToHash("isGround");// 점프하는 상태 해제하기 위해

    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent<Animator>(out animator);
        //playerJump = GetComponentInParent<PlayerJump>();
        changeFace = GetComponentInChildren<ChangeFaceMaterial>();
    }

    // 현재 애니메이션의 태그를 가져오는 함수
    public AnimationTag GetCurrentAnimationTag()
    {
        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            int tagHash = stateInfo.tagHash;

            return AnimationTagManager.Instance.GetAnimationTag(tagHash);
            
        }
        return AnimationTag.None;;
    }

    private void idleaniDebug()// idle 이벤트 상태 호출
    {
        //Debug.Log("idle상태입니다. 맞은공격 초기화");

        changeFace.ChangeFace(0);
        EventManager.Instance.TriggerEvent();//idle

        
    }

    private void EnemyidleaniDebug()//적 idle 이벤트 상태 호출
    {
        
        EventManager.Instance.EnemyaniEvent();//Enemyidle


    }
    private void FDamageisGround()// 공중 피격으로 떨어지면 땅에 닿았을 때 이즈 그라운드 트리거
    {
        //animator.SetTrigger(animHash_isGround);
        //playerJump.GroundCheck();
    }

}
