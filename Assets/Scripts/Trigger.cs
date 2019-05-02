using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Trigger : MonoBehaviour
{
    
    [SerializeField]
    protected Text locationText;

    protected string location;
    protected bool isPlayerHere = false;

    protected void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            locationText.text = location;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) {
            locationText.text = "";
        }
    }

    public void moveToScene() {
        // figure out how to de-abstract this
    }

    public bool within() {
        return isPlayerHere;
    }
}
