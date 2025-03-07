using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTagReader : MonoBehaviour
{
    private Animator animator;
    private PlayerStat PlayerStat;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<Animator>(out animator);
        PlayerStat = GetComponentInParent<PlayerStat>();
    }

    // ���� �ִϸ��̼��� �±׸� �������� �Լ�
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

    private void idleaniDebug()// idle �̺�Ʈ ���� ȣ��
    {
        Debug.Log("idle�����Դϴ�. �������� �ʱ�ȭ");
        EventManager.Instance.TriggerEvent();//idle
        PlayerStat.collidedObjects.Clear();
    }

}
