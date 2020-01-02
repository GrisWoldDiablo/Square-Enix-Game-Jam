using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private AudioSource _sourceBMG;
    [SerializeField] private List<AudioClip> _clipBMGs;
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
            DontDestroyOnLoad(this.gameObject);
        }
        #endregion
    }

    private void Start()
    {
        _sourceBMG.clip = _clipBMGs[Random.Range(0, _clipBMGs.Count)];
        _sourceBMG.Play();
    }
}
