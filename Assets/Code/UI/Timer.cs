using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI textMeshProUGUI;

    private float timer = 99;
    // Start is called before the first frame update
    void Start()
    {
       // textMeshProUGUI.text = timer.ToString();
       /*while(timer > 0)
        {
            ChangeTime();
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        //ChangeTime();
    }

    void ChangeTime()
    {
        //timer -= Time.time;
        timer = timer - 1f;// (Time.deltaTime);
        //timer = (int)timer;
        //timer = Mathf.FloorToInt(timer);

        textMeshProUGUI.text = timer.ToString();
    }
}
