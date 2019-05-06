using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameControllerScript : MonoBehaviour
{

    [SerializeField]
    private Button contButton;

    [SerializeField]
    private Button quitButton;
    private GameObject[] pauseObjects;

    [SerializeField]
    private GameObject player;

    private Trigger[] triggers;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] trigs = GameObject.FindGameObjectsWithTag("Trigger");
        triggers = new Trigger[(trigs).Length];

        for (int i = 0; i < triggers.Length; i++)
        {
            triggers[i] = trigs[i].GetComponent<Trigger>();
        }

        contButton.onClick.AddListener(unPause);

        player.transform.position = StaticClass.playerPosition;

        Time.timeScale = 1;
 
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        unPause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                showPauseScreen();
            }
            else if (Time.timeScale == 0)
            {
                unPause();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            print("enter hit");
            foreach (Trigger t in triggers)
            {
                if (t.within())
                {
                    StaticClass.playerPosition = player.transform.position;
                    t.moveToScene();
                }
            }
        }

    }

    void showPauseScreen()
    {
        Debug.Log(player.transform.position);
        Time.timeScale = 0;
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    void unPause()
    {
        Time.timeScale = 1;
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }


    public static class StaticClass
    {
        public static Vector3 playerPosition = new Vector3(11.3f, 2f, 31.6f);
        public static int phase = 0;
    }
}
