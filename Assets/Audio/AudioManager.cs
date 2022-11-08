using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip p_hit;
    public AudioClip p_shoot;
    public AudioClip p_laser;
    public AudioClip p_missle;

    public SpaceRoom currentRoom;
    public SpaceMap spaceMap;
    public List<SpaceRoom> spaceRooms;
    public AudioClip bgm1;
    public AudioClip bgm2;
    public AudioClip bgm3;
    public AudioClip bgm4;
    public int roomNum = 0;
    
    public AudioSource bgmPlayer;
    public AudioSource sfxPlayer;
    //use smth to get the current room the player is in

    // Start is called before the first frame update
    void Start()
    {
        roomNum = 0;

        // audioPlayer = this.GetComponent<AudioSource>();
        spaceRooms = spaceMap.spaceRooms;
        currentRoom = this.GetComponentInParent<Player>().nowRoom;

        // bgmPlayer = this.GetComponentInChildren<AudioSource>();
        bgmPlayer.clip = bgm1;
        bgmPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
       
        

        // currentRoom = this.GetComponentInParent<Player>().nowRoom;
        // //check which room the player is in
        //     //for room 0 & 1, use smooth

        //     //for room 2 & 3, use fast
        // if(currentRoom == spaceRooms[2]){
        //     bgmPlayer.clip = bgmFast;
        // }
            
    }

    public void CheckRoom(){
        if(roomNum == 0){
            bgmPlayer.clip = bgm1;
            // bgmPlayer.Play();
        }else if(roomNum == 1){
            bgmPlayer.clip = bgm2;
            // bgmPlayer.Play();
        }else if(roomNum == 2){
            bgmPlayer.clip = bgm3;
            // bgmPlayer.Play();
        }else if(roomNum == 3){
            bgmPlayer.clip = bgm4;
            // bgmPlayer.Play();
        }
    }

}
