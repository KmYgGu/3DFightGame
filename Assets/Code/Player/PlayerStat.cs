using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    //능력치에 변화가 있을 때마다 EventManager를 호출
    [SerializeField]private AnimationTag AniState;
    public AnimationTag aniState => AniState;// 값을 가지고 있지 않는 함수

    private int PlayerHp = 1000;
    public int playerHp => PlayerHp;

    private int PlayerAttackPower = 10;
    public int playerAttackPower => PlayerAttackPower;

    private AnimationTagReader tagReader;

    // 충돌한 오브젝트(공격)를 저장할 HashSet
    public HashSet<GameObject> collidedObjects = new HashSet<GameObject>();

    private void Start()
    {
        tagReader = gameObject.GetComponentInChildren<AnimationTagReader>();

        AniState = AnimationTag.Idle; // 처음에는 idle값으로 초기화
        
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
        //여기에 여러 이벤트를 받은 수치를 정리
    }*/

    private void OnEventReceived3()
    {
        StartCoroutine("GetAnimationTag");
    }

    private IEnumerator GetAnimationTag()
    {
        
        yield return new WaitForEndOfFrame(); // 한 프레임 대기 후 실행

        AniState = tagReader.GetCurrentAnimationTag();
        
        tagReader.GetCurrentAnimationTag();
                
        //Debug.Log("플레이어 현재 애니메이션 태그: " + AniState);

    }


}
