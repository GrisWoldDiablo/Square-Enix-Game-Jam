using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [SerializeField] private Image _imageMute;

    private void Start()
    {
        _imageMute.enabled = AudioManager.Instance.SourceBMG.mute;
    }

    public void PressMute()
    {
        _imageMute.enabled = AudioManager.Instance.ToggleMute();
    }
}
