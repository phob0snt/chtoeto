using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapBody : MonoBehaviour
{
    int currCar = 3;
    Transform cust;
    private void Start()
    {
        cust = GameObject.Find("customize").transform;
    }
    public void SwapCar(string arrow)
    {
        currCar = arrow == "right" ? currCar + 1 : currCar - 1;

        StaticClass.CarsInfo = Mathf.Abs(currCar) % StaticClass.cars.Length;

        Transform first = cust.GetChild(0).GetChild(0);
        Transform jiga = cust.GetChild(0).GetChild(1);
        Transform carvet = cust.GetChild(0).GetChild(2);
        switch (StaticClass.cars[StaticClass.CarsInfo])
        {
            case "first":
                first.position = cust.GetChild(0).position;
                first.gameObject.SetActive(true);
                carvet.gameObject.SetActive(false);
                jiga.gameObject.SetActive(false);
                break;
            case "jiga":
                jiga.position = cust.GetChild(0).position;
                jiga.gameObject.SetActive(true);
                first.gameObject.SetActive(false);
                carvet.gameObject.SetActive(false);
                break;
            case "carvet":
                carvet.position = cust.GetChild(0).position;
                carvet.gameObject.SetActive(true);
                first.gameObject.SetActive(false);
                jiga.gameObject.SetActive(false);
                break;
        }
    }
}
