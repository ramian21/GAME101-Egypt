using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DialogueChoicesScript : TypeWriterScript
{


    [SerializeField]
    private string choiceScript1;

    [SerializeField]
    private string choiceScript2;

    [SerializeField]
    private string choiceScript3;

    [SerializeField]
    private Button choice1;
    [SerializeField]
    private Button choice2;
    [SerializeField]
    private Button choice3;

    private GameObject[] choiceBox;
    private bool showingChoices;

    private bool choiceSelected;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        showingChoices = false;
        choiceSelected = false;
        choice1.onClick.AddListener(delegate { parseAndAdd(choiceScript1); });
        choice2.onClick.AddListener(delegate { parseAndAdd(choiceScript2); });
        choice3.onClick.AddListener(delegate { parseAndAdd(choiceScript3); });
        choiceBox = GameObject.FindGameObjectsWithTag("ChoiceBox");
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
                if (!showingChoices) { showChoiceBox(); }
            }
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
}
