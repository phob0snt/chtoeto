using UnityEngine;

public class spawnEnding : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (StaticClass.gameMode == "timeRace")
            GameObject.Find("TimeRaceStuff").transform.GetChild(1).gameObject.SetActive(true);
        else
            GameObject.Find("AIRaceStuff").transform.GetChild(1).gameObject.SetActive(true);
    }
}
