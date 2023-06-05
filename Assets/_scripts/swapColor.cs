using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class swapColor : MonoBehaviour
{
    [SerializeField] public Material[] colors = new Material[15];
    int colIndex = 15; 

    public void SwapColor(string arrow)
    {
        GameObject currCar = GameObject.Find("customize").transform.GetChild(0).GetChild(StaticClass.CarsInfo).GetChild(0).gameObject;
        colIndex = arrow == "right" ? colIndex + 1 : colIndex - 1;

        StaticClass.ColorInfo = Mathf.Abs(colIndex) % StaticClass.colors.Length;

        Material[] mat = currCar.GetComponent<MeshRenderer>().materials;
        mat[StaticClass.carsColorElements[StaticClass.CarsInfo]] = colors[StaticClass.ColorInfo]; 
        currCar.GetComponent<MeshRenderer>().materials = mat;

    }
}
