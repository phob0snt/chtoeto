using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handCollides : MonoBehaviour
{
    [SerializeField] private Collider steeringWheel;
    [SerializeField] private Collider rButt;
    [SerializeField] private Collider bibi;
    public bool sCollides;
    public bool rCollides;
    public bool bibiCollides;

    private void OnTriggerEnter(Collider other)
    {
        if (other == steeringWheel)
        {
            sCollides = true;
        }
        else if (other == rButt)
        {
            rCollides = true;
        }
        else if (other == bibi)
        {
            bibiCollides = true;
        }
        GameObject.Find("Loading").GetComponent<LoadingParams>().currentCar.GetComponent<carScript>().Sounds();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == steeringWheel)
        {
            sCollides = false;
        }
        else if (other == rButt)
        {
            rCollides = false;
        }
        else if (other == bibi)
        {
            bibiCollides = false;
        }
    }

}
