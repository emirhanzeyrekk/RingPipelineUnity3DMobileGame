using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    private bool musicEnable;

    [SerializeField]
    private AudioSource music;

    private void Awake()
    {
        musicEnable = true;
    }

    public void ToggleMusic()
    {
        if (!musicEnable)
        {
            //Enable music,
            music.Play();
            musicEnable = true;
        }
        else
        {
            //Disable music
            music.Pause();
            musicEnable = false;
        }
    }
}
