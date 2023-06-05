using UnityEngine;

public class engineSound : MonoBehaviour
{
    [SerializeField] float lowerPitch = 0.6f;
    [SerializeField] WheelCollider wheels;
    [SerializeField] GameObject car;
    public float enginePitch = 0.6f;


    void Update()
    {
        int currGear = car.GetComponent<carScript>().gearNum;
        float maxRpm = car.GetComponent<carScript>().maxRpm;
        float enginePitchCoef = car.GetComponent<carScript>().enginePitchCoef;
        float engineRpm = Mathf.Abs(-wheels.rpm) <= maxRpm ? Mathf.Abs(-wheels.rpm) : maxRpm;
        //Debug.Log(-wheels.rpm);
        enginePitch = lowerPitch;
        if (currGear > 1)
            enginePitch += (engineRpm / maxRpm) * enginePitchCoef - (currGear * 0.1f);
        else
            enginePitch += (engineRpm / maxRpm) * enginePitchCoef;
        if (enginePitch <= 1.6f)
            car.GetComponent<AudioSource>().pitch = enginePitch;
        else
        {
            enginePitch = 1.6f;
            car.GetComponent<AudioSource>().pitch = enginePitch;
        }
    }
}
