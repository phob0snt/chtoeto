using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapWheels : MonoBehaviour
{
    int currWheel = 2;
    public void SwapWheels(string arrow)
    {
        GameObject[] wh = GameObject.FindGameObjectsWithTag("wheel");
        currWheel = arrow == "right" ? currWheel + 1 : currWheel - 1;

        StaticClass.WheelsInfo = Mathf.Abs(currWheel) % StaticClass.wheels.Length;

        switch (StaticClass.wheels[StaticClass.WheelsInfo])
        {
            case "wheel1":
                foreach (var whel in wh)
                {
                    whel.transform.GetChild(0).gameObject.SetActive(true);
                    whel.transform.GetChild(1).gameObject.SetActive(false);
                }
                break;
            case "wheel2":
                foreach (var whel in wh)
                {
                    whel.transform.GetChild(0).gameObject.SetActive(false);
                    whel.transform.GetChild(1).gameObject.SetActive(true);
                }
                break;
        }
    }
}
