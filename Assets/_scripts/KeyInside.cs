using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyInside : MonoBehaviour
{
    [SerializeField] Collider key;
    public bool canStart;


    private void OnTriggerEnter(Collider other)
    {
        GameObject curr = GameObject.Find("Loading").GetComponent<LoadingParams>().currentCar;
        Debug.Log(curr.name);
        if (other == key)
        {
            canStart = true; 
            key.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
            key.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
            switch (curr.name)
            {
                case "CarFirst":
                    key.transform.localPosition = new Vector3(-0.00293099997f, -0.0103540001f, 0.00328000006f);
                    key.transform.localRotation = Quaternion.Euler(12, 90, 90);
                    key.transform.localScale = new Vector3(0.00005f, 0.00005f, 0.00005f);
                    break;
                case "CarJiga":
                    key.transform.localPosition = new Vector3(0.412600011f, -0.0130000003f, -1.50039995f);
                    key.transform.localRotation = Quaternion.Euler(0, 258, 0);
                    key.transform.localScale = new Vector3(0.00762319798f, 0.00750000076f, 0.010226801f);
                    break;
            }
                
            key.gameObject.GetComponent<Rigidbody>().useGravity = false;
            key.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        canStart = false;
        key.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        key.gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
}
