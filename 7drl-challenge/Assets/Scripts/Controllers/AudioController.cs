using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    public AudioSource levelMusic;
    public AudioSource gameOverMusic;
    public AudioSource winMusic;

    public AudioSource[] sfx;

    private void Awake()
    {
        instance = this;
    }

    public void PlayGameOver()
    {
        levelMusic.Stop();
        gameOverMusic.Play();
    }

    public void PlayWinLevel()
    {
        levelMusic.Stop();
        winMusic.Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }
}
