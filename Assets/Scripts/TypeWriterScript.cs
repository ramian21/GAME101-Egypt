using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterScript : MonoBehaviour
{
    [SerializeField]
    protected float delay = 0.05f;
    [SerializeField]
    protected string fullScript;

    protected string currentText = "";

    protected string[] lines;
    protected List<string> lineChunks;

    protected List<string> fullScriptInChunks;

    protected static int index;

    protected bool currentlyDisplaying;

    [SerializeField]
    protected int dialogueBoxCharLength = 280;








    // Start is called before the first frame update
    protected void Start()
    {
        // parse full script

        index = -1;
        currentlyDisplaying = false;
        
        
        lineChunks = new List<string>();
        fullScriptInChunks = new List<string>();

        parseAndAdd(fullScript);

        StartCoroutine("ShowText", fullScriptInChunks[++index]);
    }

    protected void parseAndAdd(string script) {

        
        lines = script.Split('#');

        string[] linesRef = lines;
        List<string> lineChunksRef = lineChunks;
        string currentTextRef = currentText;




        for (int k = 0; k < linesRef.Length; k++)
        {
            lineChunksRef.Clear();
            int len = linesRef[k].Length;
            for (int i = 0; i < ((linesRef[k].Length / dialogueBoxCharLength) + 1); i++)
            {
                int startRange = i * dialogueBoxCharLength;
                int remainder = (len - (i * dialogueBoxCharLength));
                int substringLength = (remainder >= dialogueBoxCharLength ? dialogueBoxCharLength : remainder);
                string chunk = linesRef[k].Substring(startRange, substringLength);
                lineChunksRef.Add(chunk);
            }

            string[] currentLineChunksRef = lineChunksRef.ToArray();

            for (int i = 0; i < currentLineChunksRef.Length; i++)
            {
                fullScriptInChunks.Add(currentLineChunksRef[i]);
            }
        }
    }

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
        }
    }


    IEnumerator ShowText(string toBeDisplayed)
    {
        currentlyDisplaying = true;
        for (int k = 0; k <= toBeDisplayed.Length; k++)
        {
            currentText = toBeDisplayed.Substring(0, k);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }

    }
}
