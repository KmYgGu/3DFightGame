using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTagReader : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<Animator>(out animator);
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

}
