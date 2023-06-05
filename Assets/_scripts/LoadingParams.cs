using Cinemachine;
using TMPro;
using UnityEngine;

public class LoadingParams : MonoBehaviour
{
    [SerializeField] public Material[] colors = new Material[15];
    [SerializeField] private Transform startpos;
    public GameObject currentCar;
    private GameObject key;

    void Start()
    {
        string gameMode = StaticClass.gameMode;
        key = GameObject.Find("key");
        GameObject keySpawn = GameObject.Find("keySpawn");
        GameObject first = GameObject.Find("Cars").transform.GetChild(0).gameObject;
        GameObject jiga = GameObject.Find("Cars").transform.GetChild(1).gameObject;
        GameObject carvet = GameObject.Find("Cars").transform.GetChild(2).gameObject;

        switch (StaticClass.cars[StaticClass.CarsInfo])
        {
            case "jiga":
                currentCar = jiga;
                first.SetActive(false);
                jiga.SetActive(true);
                break;
            case "carvet":
                currentCar = carvet;
                jiga.SetActive(false);
                carvet.SetActive(true);
                break;
            case "first":
            default:
                currentCar = first;
                first.SetActive(true);
                jiga.SetActive(false);
                carvet.SetActive(false);
                break;




        }
        currentCar.transform.position = startpos.position;
        key.transform.position = keySpawn.transform.position;

        

        GameObject[] wh = GameObject.FindGameObjectsWithTag("wheel");

        switch (StaticClass.wheels[StaticClass.WheelsInfo])
        {
            case "wheel2":
                foreach (var w in wh)
                {
                    w.transform.GetChild(0).gameObject.SetActive(false);
                    w.transform.GetChild(1).gameObject.SetActive(true);
                }
                break;
            case "wheel1":
            default:
                foreach (var w in wh)
                {
                    w.transform.GetChild(0).gameObject.SetActive(true);
                    w.transform.GetChild(1).gameObject.SetActive(false);
                }
                break;
        }
        Material[] mat = currentCar.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().materials;
        mat[StaticClass.carsColorElements[StaticClass.CarsInfo]] = colors[StaticClass.ColorInfo];
        currentCar.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().materials = mat;
        currentCar.GetComponent<AudioSource>().volume = StaticClass.GenVolume;
        
        switch(gameMode)
        {
            case "timeRace":
                this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("gmUIText").GetComponent<timer>().enabled = true;
                GameObject.Find("gmUIText").GetComponent<place>().enabled = false;
                break;
            case "AIRace":
                this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("gmUIText").GetComponent<timer>().enabled = false;
                GameObject.Find("gmUIText").GetComponent<place>().enabled = true;
                break;
        }


    }
    


}
