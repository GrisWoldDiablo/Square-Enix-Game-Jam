using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [SerializeField] private Image _imageMute;

    private void Start()
    {
        _imageMute.enabled = !AudioManager.Instance.AudioListener.isActiveAndEnabled;
    }

    public void PressMute()
    {
        AudioManager.Instance.ToggleMute(_imageMute);
    }
}
