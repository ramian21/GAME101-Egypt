using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Trigger : MonoBehaviour
{

    [SerializeField]
    protected Text locationText;

    [SerializeField]
    protected string location;

    [SerializeField]
    protected string sceneName;
    protected bool isPlayerHere = false;

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            locationText.text = location;
            isPlayerHere = true;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            locationText.text = "";
            isPlayerHere = false;
        }
    }

    public void moveToScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public bool within()
    {
        return isPlayerHere;
    }
}
