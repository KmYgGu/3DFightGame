using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;


public class RoundMessageChanged : MonoBehaviour
{
    [SerializeField]private GameObject MessageImage;
    [SerializeField]private TextMeshProUGUI RoundText;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(RoundFight(99));
        
        //StartCoroutine(WhoWin("Win"));
    }


    IEnumerator RoundFight(int number)
    {
        MessageImage.SetActive(true);
        RoundText.text = ($"Round {number}");
        //yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(2);
        RoundText.text = ($"Fight!");
        yield return new WaitForSeconds(1);
        MessageImage.SetActive(false);
    }

    IEnumerator WhoWin(string result)
    {
        MessageImage.SetActive(true);
        RoundText.text = ($"K.O");
        //yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(2);
        RoundText.text = ($"You {result}!");
        yield return new WaitForSeconds(1);
        MessageImage.SetActive(false);
    }
}
