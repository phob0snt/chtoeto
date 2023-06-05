using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endTimeRace : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (StaticClass.gameMode == "AIRace")
        {
            if (other.gameObject.name == "fpol")
                StaticClass.place++;
            else if (other.gameObject.name == "pol")
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                string finishTime = GameObject.Find("gmUIText").GetComponent<timer>().fcurrTime;
                StaticClass.lastTRTry = finishTime;
                StaticClass.loadState = "afterAIR";
                SceneManager.LoadScene("menu");
            }
            Debug.Log("FASHFUASUGHUGIA" + StaticClass.place);
        }
        else
        {
            if (other.gameObject.name == "pol")
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                string finishTime = GameObject.Find("gmUIText").GetComponent<timer>().fcurrTime;
                StaticClass.lastTRTry = finishTime;
                StaticClass.loadState = "afterTR";
                SceneManager.LoadScene("menu");
            }
        }
        
    }

}
