using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class menuLoadP : MonoBehaviour
{
    private void Start()
    {
        if (StaticClass.loadState == "menu")
        {
        }
        else if (StaticClass.loadState == "afterTR")
        {
            GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.GetChild(4).gameObject.SetActive(true);
            Debug.Log(StaticClass.lastTRTry);
            GameObject.Find("result").GetComponent<TMP_Text>().text = StaticClass.lastTRTry;
        }
        else if (StaticClass.loadState == "afterAIR")
        {   
            GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.GetChild(5).gameObject.SetActive(true);
            GameObject.Find("result").GetComponent<TMP_Text>().text = Convert.ToString(StaticClass.place);
        }    
    }
}
