using System;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class timer : MonoBehaviour
{
    private bool setTime = false;
    private DateTime startTime;
    private string currTime;
    private GameObject currCar;
    private string format;

    public string fcurrTime;


    void Update()
    {
        currCar = GameObject.Find("Loading").GetComponent<LoadingParams>().currentCar;
        if (GameObject.Find("Loading").GetComponent<gameMng>().go)
        {
            if (!setTime)
            {
                startTime = DateTime.Now;
                setTime = true;
            }
            currTime = DateTime.Now.Subtract(startTime).ToString();
            if (currTime.Length >= 9)
                fcurrTime = $"{currTime.Substring(3, 5)}:{currTime.Substring(9, 2)}";
            GetComponent<TMP_Text>().text = $"Текущий круг: {fcurrTime}";
        }

    }
}
