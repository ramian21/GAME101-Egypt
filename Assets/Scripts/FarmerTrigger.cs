using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FarmerTrigger : Trigger
{
    
    void Start() {
        location = "Farmer's Town";
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            locationText.text = location;
            isPlayerHere = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) {
            locationText.text = "";
            isPlayerHere = false;
        }
    }

    public void moveToScene() {
        SceneManager.LoadScene("FarmerScene");
    }
}
