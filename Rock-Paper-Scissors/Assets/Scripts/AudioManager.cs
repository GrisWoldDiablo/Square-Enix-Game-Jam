using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region AudioSingleton
    private static AudioManager _instance = null;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioManager>();
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private List<AudioClip> musicFiles;
    [SerializeField] private List<AudioClip> sfxFiles;

    /// <summary>
    /// Preset audio files to loop or play on game start
    /// </summary>
    public void Awake()
    {
        musicSource.playOnAwake = true;
        musicSource.loop = true;

        sfxSource.playOnAwake = false;
        sfxSource.loop = false;
    }

    public void Update()
    {
        //Test if audio works
        if(Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D))
        {
            PlayMainMenuMusic();
        }

        if(Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.S))
        {
            PlayMenuSelectableSFX();
        }
    }

    public void PlayConnectedSFX()
    {
        sfxSource.PlayOneShot(sfxFiles[0]);
    }

    public void PlayMainMenuMusic()
    {
        musicSource.clip = musicFiles[0];
        musicSource.Play();
    }

    public void PlayMenuSelectableSFX()
    {
        sfxSource.PlayOneShot(sfxFiles[1]);
    }

    public void PlayAcceptButtonSFX()
    {
        sfxSource.PlayOneShot(sfxFiles[2]);
    }

    public void PlayCancelButtonSFX()
    {
        sfxSource.PlayOneShot(sfxFiles[3]);
    }

    public void PlayGameInterfaceMusic()
    {
        musicSource.clip = musicFiles[1];
        musicSource.Play();
    }

    public void PlayTimerMusic()
    {
        sfxSource.loop = true;
        sfxSource.clip = sfxFiles[5];
        sfxSource.Play();
    }

    public void StopTimerMusic()
    {
        sfxSource.loop = false;
        sfxSource.PlayOneShot(sfxFiles[4]);
    }
}
