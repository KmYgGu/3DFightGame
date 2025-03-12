using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyTail : MonoBehaviour
{
    private PlayerAttack playerAttack;
    [SerializeField] private AnimationClip attackAni;

    [SerializeField] private GameObject[] TailObj; //0 : 왼발, 1 : 오른발, 2 : 왼손..

    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent<PlayerAttack>(out playerAttack); // 같은 오브젝트에 있는 스크립트 가져오기
    }

    public void SetTail(int aniNumber)
    {
        foreach (GameObject obj in TailObj)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }


        switch (aniNumber)
        {
            case 0:
                TailObj[1].SetActive(true);
                break;
            case 1:
                TailObj[1].SetActive(true);
                break;
            case 2:
                TailObj[0].SetActive(true);
                break;
            case 3:
                TailObj[1].SetActive(true);
                break;
            case 4:
                TailObj[2].SetActive(true);
                break;
            case 5:
                TailObj[2].SetActive(true);
                break;
            case 6:
                //TailObj[1].SetActive(true);
                break;
            case 7:
                TailObj[1].SetActive(true);
                break;

            default:
                break;

        }
    }
}
