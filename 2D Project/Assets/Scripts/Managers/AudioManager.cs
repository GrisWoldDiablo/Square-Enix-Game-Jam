using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioListener),typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    #region Singleton
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

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private List<AudioClip> _clipBMGs;

    private AudioSource _sourceBMG;
    public AudioSource SourceBMG { get => _sourceBMG; }

    void Awake()
    {
        #region Dont Destroy On Load
        var objects = GameObject.FindObjectsOfType(this.GetType());
        if (objects.Length > 1)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            _sourceBMG = GetComponent<AudioSource>();
            DontDestroyOnLoad(this.gameObject);
        }
        #endregion
    }

    private void Start()
    {
        _sourceBMG.clip = _clipBMGs[Random.Range(0, _clipBMGs.Count)];
        _sourceBMG.Play();
    }

    public void SetVolMaster(float sliderValue)
    {
        _audioMixer.SetFloat("VolumeMaster", sliderValue);
    }

    public bool ToggleMute()
    {
        _sourceBMG.mute = !_sourceBMG.mute;
        return _sourceBMG.mute;
    }
}
