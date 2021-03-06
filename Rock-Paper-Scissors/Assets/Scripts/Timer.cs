﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    //Animation
    Animation anim;
    [SerializeField] AnimationClip timerStart;
    [SerializeField] AnimationClip timerEnd;

    [SerializeField] float seconds;
    [SerializeField] TextMeshProUGUI timerText;

    bool timerStarted;

    #region Singleton
    private static Timer _instance = null;
    public static Timer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Timer>();
            }
            return _instance;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update()
    {
        if (timerStarted && seconds > 0)
        {
            seconds -= Time.deltaTime;
            timerText.text = seconds.ToString("F0");
        }
        else if (timerStarted && seconds <= 0)
        {
            timerStarted = false;
            TimerComplete(true);
        }
    }

    public void TimerActivate()
    {
        seconds = 5;
        anim.clip = timerStart;
        anim.Play();
        timerText.text = seconds.ToString();
        timerText.enabled = true;
        timerStarted = true;
        AudioManager.Instance.PlayTimerMusic();
    }

    public void TimerComplete(bool expired = false)
    {
        if(!expired)
        {
            seconds = 0;
            anim.clip = timerEnd;
            anim.Play();
            timerStarted = false;

        }

        else
        {
            seconds = 0;
            anim.clip = timerEnd;
            anim.Play();
            timerStarted = false;
            GameLogic.Instance.TimerExpire(); //Will damage player that didn't throw move
        }
        AudioManager.Instance.StopTimerMusic();
        ACManager.Instance.RoundReady();
        //Add Next player turn logic here
    }
}
