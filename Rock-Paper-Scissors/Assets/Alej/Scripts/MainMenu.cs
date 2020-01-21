using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Fields
    private AsyncOperation async;

    [SerializeField] private GameObject[] panels;
    [SerializeField] private Selectable[] defaultOptions;

    void Start()
    {
        PanelToggle(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitBtn()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void PanelToggle(int position)
    {
        Input.ResetInputAxes();

        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(position == i);

            if (position == i)
                defaultOptions[i].Select();
        }
    }
}
