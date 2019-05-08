using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManagerScript : MonoBehaviour
{

    public static Vector3 playerPosition = new Vector3(11.3f, 2f, 31.6f);
    public static int phase = 0;

    [SerializeField]
    public static AudioSource bgm;

    void Awake()
    {
        if(bgm == null) {
            bgm = GetComponent<AudioSource>();
        }
        DontDestroyOnLoad(this.gameObject);

        if(!bgm.isPlaying) {
            bgm.loop = true;
            bgm.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
