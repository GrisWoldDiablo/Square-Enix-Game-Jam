using System.Collections;
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
        //TimerActivate();
        //Invoke("TimerComplete", 3f);

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
            TimerComplete();
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
    }

    public void TimerComplete()
    {
        seconds = 0;
        anim.clip = timerEnd;
        anim.Play();
        timerStarted = false;

        //Add Next player turn logic here
    }
}
