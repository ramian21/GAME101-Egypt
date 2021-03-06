﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DialogueChoicesScript : TypeWriterScript
{


    [SerializeField]
    [TextArea(1, 3)]
    private string choiceScript1;

    [SerializeField]
    [TextArea(1, 3)]
    private string choiceScript2;

    [SerializeField]
    [TextArea(1, 3)]
    private string choiceScript3;

    [SerializeField]
    [TextArea(1, 3)]
    private string choiceScript1Part2;

    [SerializeField]
    [TextArea(1, 3)]
    private string choiceScript2Part2;

    [SerializeField]
    [TextArea(1, 3)]
    private string choiceScript3Part2;

    [SerializeField]
    private Button choice1;
    [SerializeField]
    private Button choice2;
    [SerializeField]
    private Button choice3;

    [SerializeField]
    private string closingText;

    [SerializeField]
    private string closingTextPart2;

    private GameObject[] choiceBox;
    private bool showingChoices;

    private bool choiceSelected;

    private GameObject[] phase1Objects;

    private GameObject[] phase2Objects;
    private GameObject escapeText;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        if (closingText == null)
        {
            closingText = "";
        }

        showingChoices = false;
        choiceSelected = false;

        choiceBox = GameObject.FindGameObjectsWithTag("ChoiceBox");
        phase1Objects = GameObject.FindGameObjectsWithTag("Phase1");
        phase2Objects = GameObject.FindGameObjectsWithTag("Phase2");

        if (GameManagerScript.phase < 4)
        {
            choice1.onClick.AddListener(delegate { addWithClosingText(choiceScript1); });
            choice2.onClick.AddListener(delegate { addWithClosingText(choiceScript2); });
            choice3.onClick.AddListener(delegate { addWithClosingText(choiceScript3); });
            foreach (GameObject g in phase1Objects)
            {
                g.SetActive(true);
            }
            foreach (GameObject g in phase2Objects)
            {
                g.SetActive(false);
            }
        }
        else if (GameManagerScript.phase >= 4)
        {
            choice1.onClick.AddListener(delegate { addWithClosingText(choiceScript1Part2); });
            choice2.onClick.AddListener(delegate { addWithClosingText(choiceScript2Part2); });
            choice3.onClick.AddListener(delegate { addWithClosingText(choiceScript3Part2); });
            foreach (GameObject g in phase1Objects)
            {
                g.SetActive(false);
            }
            foreach (GameObject g in phase2Objects)
            {
                g.SetActive(true);
            }
        }
        choiceBox = GameObject.FindGameObjectsWithTag("ChoiceBox");
        escapeText = GameObject.FindGameObjectWithTag("EscapeText");
        escapeText.SetActive(false);
        hideChoiceBox();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (currentlyDisplaying)
            {
                StopCoroutine("ShowText");
                currentlyDisplaying = false;
                currentText = fullScriptInChunks[index];
                this.GetComponent<Text>().text = currentText;
            }
            else if (index < fullScriptInChunks.Count - 1)
            {
                StartCoroutine("ShowText", fullScriptInChunks[++index]);
            }
            else if (index == fullScriptInChunks.Count - 1 && !choiceSelected)
            {
                if (!showingChoices)
                {
                    showChoiceBox();
                }
                else
                {
                    escapeText.SetActive(true);
                }
            }
            else
            {
                escapeText.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && index >= fullScriptInChunks.Count - 1)
        {
            GameManagerScript.phase++;
            SceneManager.LoadScene("OverworldScene");
        }
    }

    protected void parseAndAdd(string script)
    {
        hideChoiceBox();
        choiceSelected = true;
        base.parseAndAdd(script);
        StartCoroutine("ShowText", fullScriptInChunks[++index]);
    }

    void showChoiceBox()
    {
        foreach (GameObject g in choiceBox)
        {
            g.SetActive(true);
        }
        showingChoices = true;
    }

    void hideChoiceBox()
    {
        foreach (GameObject g in choiceBox)
        {
            g.SetActive(false);
        }
        showingChoices = false;
    }

    void addWithClosingText(string script)
    {
        parseAndAdd(script);
        index--;
        if (GameManagerScript.phase == 0)
        {
            parseAndAdd(closingText);
        }
        else if (GameManagerScript.phase == 1)
        {
            parseAndAdd(closingTextPart2);
        }
    }
}
