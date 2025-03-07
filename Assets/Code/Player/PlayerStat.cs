using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    //�ɷ�ġ�� ��ȭ�� ���� ������ EventManager�� ȣ��
    [SerializeField]private AnimationTag AniState;
    public AnimationTag aniState => AniState;// ���� ������ ���� �ʴ� �Լ�

    private int PlayerHp = 1000;
    public int playerHp => PlayerHp;

    private int PlayerAttackPower = 10;
    public int playerAttackPower => PlayerAttackPower;

    private AnimationTagReader tagReader;

    // �浹�� ������Ʈ(����)�� ������ HashSet
    public HashSet<GameObject> collidedObjects = new HashSet<GameObject>();

    private void Start()
    {
        tagReader = gameObject.GetComponentInChildren<AnimationTagReader>();

        AniState = AnimationTag.Idle; // ó������ idle������ �ʱ�ȭ
        
    }

    private void OnEnable()
    {
        
        EventManager.Instance.OnCustomEvent3 += OnEventReceived3;
    }

    private void OnDisable()
    {
        
        EventManager.Instance.OnCustomEvent3 -= OnEventReceived3;
    }

    /*private void OnEventReceived(int value)
    {
        //���⿡ ���� �̺�Ʈ�� ���� ��ġ�� ����
    }*/

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


}
