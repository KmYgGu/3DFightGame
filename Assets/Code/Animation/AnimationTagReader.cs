using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTagReader : MonoBehaviour
{
    private Animator animator;
    //private PlayerJump playerJump;
    private ChangeFaceMaterial changeFace;

    private int animHash_isGround = Animator.StringToHash("isGround");// �����ϴ� ���� �����ϱ� ����

    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent<Animator>(out animator);
        //playerJump = GetComponentInParent<PlayerJump>();
        changeFace = GetComponentInChildren<ChangeFaceMaterial>();
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
        //Debug.Log("idle�����Դϴ�. �������� �ʱ�ȭ");

        changeFace.ChangeFace(0);
        EventManager.Instance.TriggerEvent();//idle

        
    }

    private void EnemyidleaniDebug()//�� idle �̺�Ʈ ���� ȣ��
    {
        
        EventManager.Instance.EnemyaniEvent();//Enemyidle


    }
    private void FDamageisGround()// ���� �ǰ����� �������� ���� ����� �� ���� �׶��� Ʈ����
    {
        //animator.SetTrigger(animHash_isGround);
        //playerJump.GroundCheck();
    }

}
