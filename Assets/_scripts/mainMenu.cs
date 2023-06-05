using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{
    //[SerializeField] Camera _camera;
    //private bool moveCamTowards = false;
    //private bool moveCamBackwards = false;

    //private void FixedUpdate()
    //{
    //    if (moveCamTowards)
    //    {
    //        StartCoroutine(SmoothMoveCam(_camera.transform.position, new Vector3(-1.1f, 1.21f, -1.25f)));
    //        StopAllCoroutines();
    //    }
    //    else if (moveCamBackwards)
    //    {
    //        StartCoroutine(SmoothMoveCam(_camera.transform.position, new Vector3(0.18f, 1.78f, -4.18f)));
    //        StopAllCoroutines();
    //    }
    //}

    //IEnumerator SmoothMoveCam(Vector3 startpos, Vector3 endpos)
    //{
    //    while (startpos != endpos)
    //    {
    //        _camera.transform.position = Vector3.Lerp(startpos, endpos, 6.6f * Time.deltaTime);
    //        yield return new WaitForEndOfFrame();
    //    }
    //    moveCamTowards = false;
    //    moveCamBackwards = false;
    //}
    private Transform custMenu;
    private Transform mCanvas;
    private Transform laCanvas;
    private Transform raCanvas;
    private void Start()
    {
        custMenu = GameObject.Find("customize").transform;
    }
    public void SettingsMenu()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("sliderGVolume").GetComponent<Slider>().value = PlayerPrefs.GetFloat("GenVolume");
        GameObject.Find("volumeGValue").GetComponent<TMP_Text>().text = PlayerPrefs.GetFloat("GenVolume").ToString();
    }

    public void BackToMainMenu()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void BackToCarChoose(string menu)
    {
        mCanvas = GameObject.Find("mCanvas").transform;
        laCanvas = GameObject.Find("laCanvas").transform;
        raCanvas = GameObject.Find("raCanvas").transform;
        switch (menu)
        {
            case "wheels":
                custMenu.GetChild(1).GetChild(1).gameObject.SetActive(true);
                custMenu.GetChild(1).GetChild(2).gameObject.SetActive(true);
                mCanvas.GetChild(0).gameObject.SetActive(true);
                mCanvas.GetChild(2).gameObject.SetActive(false);
                laCanvas.GetChild(0).gameObject.SetActive(true);
                raCanvas.GetChild(0).gameObject.SetActive(true);
                laCanvas.GetChild(2).gameObject.SetActive(false);
                raCanvas.GetChild(2).gameObject.SetActive(false);
                break;
            case "colors":
                custMenu.GetChild(1).GetChild(1).gameObject.SetActive(true);
                custMenu.GetChild(1).GetChild(2).gameObject.SetActive(true);
                mCanvas.GetChild(0).gameObject.SetActive(true);
                mCanvas.GetChild(1).gameObject.SetActive(false);
                laCanvas.GetChild(0).gameObject.SetActive(true);
                raCanvas.GetChild(0).gameObject.SetActive(true);
                laCanvas.GetChild(1).gameObject.SetActive(false);
                raCanvas.GetChild(1).gameObject.SetActive(false);
                break;
        }
    }

    public void VolumeSettings(string volumeType)
    {
        switch (volumeType)
        {
            case "General":
                StaticClass.GenVolume = (float)Math.Round((decimal)GameObject.Find("sliderGVolume").GetComponent<Slider>().value, 2);
                GameObject.Find("volumeGValue").GetComponent<TMP_Text>().text = StaticClass.GenVolume.ToString();
                PlayerPrefs.SetFloat("GenVolume", StaticClass.GenVolume);
                break;
            case "Sounds":
                StaticClass.SoundsVolume = (float)Math.Round((decimal)GameObject.Find("sliderSVolume").GetComponent<Slider>().value, 2);
                GameObject.Find("volumeSValue").GetComponent<TMP_Text>().text = StaticClass.SoundsVolume.ToString();
                PlayerPrefs.SetFloat("SoundsVolume", StaticClass.SoundsVolume);
                break;
            case "Music":
                StaticClass.MusicVolume = (float)Math.Round((decimal)GameObject.Find("sliderMVolume").GetComponent<Slider>().value, 2);
                GameObject.Find("volumeMValue").GetComponent<TMP_Text>().text = StaticClass.MusicVolume.ToString();
                PlayerPrefs.SetFloat("MusicVolume", StaticClass.MusicVolume);
                break;
        }
    }

    public void ChooseCarMenu()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Screen").SetActive(false);
        this.GetComponent<Canvas>().enabled = false;
        custMenu.GetChild(0).gameObject.SetActive(true);
        custMenu.GetChild(1).gameObject.SetActive(true);
    }

    public void ChangeColorMenu()
    {
        mCanvas = GameObject.Find("mCanvas").transform;
        laCanvas = GameObject.Find("laCanvas").transform;
        raCanvas = GameObject.Find("raCanvas").transform;
        GameObject.Find("swapCol").SetActive(false);
        GameObject.Find("chCol").SetActive(false);
        mCanvas.GetChild(0).gameObject.SetActive(false);
        mCanvas.GetChild(1).gameObject.SetActive(true);
        laCanvas.GetChild(0).gameObject.SetActive(false);
        raCanvas.GetChild(0).gameObject.SetActive(false);
        laCanvas.GetChild(1).gameObject.SetActive(true);
        raCanvas.GetChild(1).gameObject.SetActive(true);
    }


    public void SwapWheelsMenu()
    {
        mCanvas = GameObject.Find("mCanvas").transform;
        laCanvas = GameObject.Find("laCanvas").transform;
        raCanvas = GameObject.Find("raCanvas").transform;
        GameObject.Find("swapCol").SetActive(false);
        GameObject.Find("chCol").SetActive(false);
        mCanvas.GetChild(0).gameObject.SetActive(false);
        mCanvas.GetChild(2).gameObject.SetActive(true);
        laCanvas.GetChild(0).gameObject.SetActive(false);
        raCanvas.GetChild(0).gameObject.SetActive(false);
        laCanvas.GetChild(2).gameObject.SetActive(true);
        raCanvas.GetChild(2).gameObject.SetActive(true);
    }

    public void ChooseGamemode()
    {
        custMenu.GetChild(0).gameObject.SetActive(false);
        custMenu.GetChild(1).gameObject.SetActive(false);
        this.gameObject.GetComponent<Canvas>().enabled = true;
        this.transform.GetChild(2).gameObject.SetActive(true);
        this.transform.GetChild(3).gameObject.SetActive(true);
    }

    public void PlayGame(string mode)
    {
        GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
        SceneManager.LoadScene("Race");
        Time.timeScale = 1.0f;
        switch (mode)
        {
            case "Time":
                StaticClass.gameMode = "timeRace";
                break;
            case "AI":
                StaticClass.gameMode = "AIRace";
                break;
        }
    }

    public void GoToMainAfterTR()
    {
        this.transform.GetChild(4).gameObject.SetActive(false);
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void GoToMainAfterAIR()
    {
        this.transform.GetChild(5).gameObject.SetActive(false);
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}