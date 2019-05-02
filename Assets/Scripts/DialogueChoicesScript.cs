using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueChoicesScript : TypeWriterScript
{


    [SerializeField]
    private string choiceScript;

    [SerializeField]
    private Button choice1;
    [SerializeField]
    private Button choice2;
    [SerializeField]
    private Button choice3;

    // Start is called before the first frame update
    void Start()
    {

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
            else if (index == fullScriptInChunks.Count - 1)
            {

            }
        }
    }
}
