using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnCube : MonoBehaviour
{
    [SerializeField] Collider handCollider;
    private void OnTriggerEnter(Collider other)
    {
        if (other == handCollider)
        {
            Debug.Log("sad");
        }
    }
}
