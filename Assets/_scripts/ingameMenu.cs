using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ingameMenu : MonoBehaviour
{
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    this.transform.GetChild(0).gameObject.SetActive(true);
        //    Cursor.lockState = CursorLockMode.Confined;
        //    Cursor.visible = true;
        //    Time.timeScale= 0f;
        //}
    }

    public void closeIngameMenu()
    {
        //this.transform.GetChild(0).gameObject.SetActive(false);
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        //Time.timeScale= 1.0f;
    }

    public void openMainMenu()
    {
        //SceneManager.LoadScene("menu");
        //Time.timeScale = 1.0f;
    }
}
