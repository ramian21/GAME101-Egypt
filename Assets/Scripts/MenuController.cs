﻿// To use this example, attach this script to an empty GameObject.
// Create three buttons (Create>UI>Button). Next, select your
// empty GameObject in the Hierarchy and click and drag each of your
// Buttons from the Hierarchy to the Your First Button, Your Second Button
// and Your Third Button fields in the Inspector.
// Click each Button in Play Mode to output their message to the console.
// Note that click means press down and then release.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector

    [SerializeField]
    public Button playButton;

    [SerializeField]

    private Button quitButton;

    [SerializeField]
    private Button infoButton;

    private GameObject[] infoObjects;

    private bool infoShowing;

    void Start()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        playButton.onClick.AddListener(Play);
        quitButton.onClick.AddListener(Quit);
        infoButton.onClick.AddListener(Info);

        infoObjects = GameObject.FindGameObjectsWithTag("Info");
        infoShowing = false;
        foreach (GameObject g in infoObjects)
        {
            g.SetActive(false);
        }
    }

    void TaskOnClick()
    {
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");
    }

    void Play()
    {
        SceneManager.LoadScene("OverWorldScene");
    }

    void Quit()
    {
        Application.Quit();
    }

    void Info()
    {

        infoShowing = !infoShowing;
        foreach (GameObject g in infoObjects)
        {
            g.SetActive(!infoShowing);
        }

    }

    void TaskWithParameters(string message)
    {
        //Output this to console when the Button2 is clicked
        Debug.Log(message);
    }

    void ButtonClicked(int buttonNo)
    {
        //Output this to console when the Button3 is clicked
        Debug.Log("Button clicked = " + buttonNo);
    }
}
