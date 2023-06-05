using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class gameMng : MonoBehaviour
{
    public bool canStart;
    public bool go;
    private bool red = false;
    private bool yellow = false;
    private string gameMode = StaticClass.gameMode;
    [SerializeField] GameObject sema;
    [SerializeField] Material[] semCols = new Material[3];
    [SerializeField] animations anims;
    [SerializeField] Transform avatar;
    private float lightsDelay = 240;
    bool rotated = false;

    void FixedUpdate()
    {
        canStart = GameObject.Find("startEngine").GetComponent<KeyInside>().canStart;
        if (avatar.position.x > 229)
            anims.isGoing(true);
        else if (avatar.position.x < 226 && avatar.position.x > 225.9f)
        {
            if (!rotated)
            {
                avatar.eulerAngles = new Vector3(0, -20, 0);
                rotated = true;
            }
            anims.isGoing(true);
        }
        else
            anims.isGoing(false);
       
        if (!go)
            semafor();
        else
        {
            if (StaticClass.gameMode == "AIRace")
            {
                GameObject.Find("firstEnemy").transform.GetChild(0).gameObject.GetComponent<AnyCarAI>().enabled = true;
                GameObject.Find("secondEnemy").transform.GetChild(0).gameObject.GetComponent<AnyCarAI>().enabled = true;
            }
        }
    }
    private void semafor()
    {
        if (canStart)
        {
            anims.isStarting(true);
            if (lightsDelay > 120)
            {
                lightsDelay -= 1;
                if (!red)
                {
                    sema.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = semCols[0];
                    sema.GetComponent<AudioSource>().Play();
                    red = true;
                }
            }
            else if (lightsDelay > 0)
            {
                lightsDelay -= 1;
                if (!yellow)
                {
                    sema.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = semCols[1];
                    sema.GetComponent<AudioSource>().Play();
                    yellow = true;
                }
            }
            else
            {
                sema.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().material = semCols[2];
                sema.GetComponent<AudioSource>().pitch = 1.4f;
                sema.GetComponent<AudioSource>().Play();
                go = true;
            }
        }
    }
}