using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip bgmSmooth;
    public AudioClip bgmFast;
    public float currentStage;//which room the player is in now(to change to corresponding bgm)
    public AudioSource audioPlayer;
    //use smth to get the current room the player is in

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //check which room the player is in
            //for room 0 & 1, use smooth

            //for room 2 & 3, use fast
    }
}
