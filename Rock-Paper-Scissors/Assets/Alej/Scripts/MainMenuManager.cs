﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    #region Singleton
    private static MainMenuManager instance = null;
    public static MainMenuManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<MainMenuManager>();

            return instance;
        }
    }

    #endregion

    //Fields
    private int panelIndex = 0;
    public bool inMenu = true;

    private MenuRotator menuRotator;

    [SerializeField] private GameObject[] panels;
    [SerializeField] private GameObject[] mainMenuObjects;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private AudioMixer audioMixer;
    private void Start()
    {
        menuRotator = FindObjectOfType<MenuRotator>();
        PanelToggle(panelIndex);
    }

    private void Update()
    {
        if (panelIndex == 0 && inMenu)
        {
            if (Input.GetKey(KeyCode.RightArrow))
                menuRotator.RotateRight();
            else if (Input.GetKey(KeyCode.LeftArrow))
                menuRotator.RotateLeft();

            if (Input.GetKeyDown(KeyCode.Return))
                MenuRotatorIndex(menuRotator.MenuRotatorIndex);
        }
        else if (panelIndex == 1 && inMenu)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
                soundSlider.value += 10;
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                soundSlider.value -= 10;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && inMenu)
            PanelToggle(0);

        if (Input.GetKeyDown(KeyCode.K))
            InMenu(!inMenu);

        audioMixer.SetFloat("MasterVolume", soundSlider.value);
    }

    private void MenuRotatorIndex(int index)
    {
        switch (index)
        {
            case 0:
                InMenu(false);
                InterfaceManager.Instance.InGame(true);
                GameLogic.Instance.CanPlay = true;
                break;
            case 1:
                PanelToggle(1);
                break;
            case 2:
                Quit();
                break;
            case 3:
                PanelToggle(2);
                break;
        }
    }

    public void InMenu(bool isInMenu)
    {
        if (isInMenu)
        {
            ACManager.Instance.InMenu();
        }
        else
        {
            ACManager.Instance.InGame();
        }

        inMenu = isInMenu;

        for (int i = 0; i < mainMenuObjects.Length; i++)
            mainMenuObjects[i].SetActive(isInMenu);
    }

    public void Action(string action)
    {
        if (!inMenu)
        {
            return;
        }
        switch (action)
        {
            case "LEFT":
                if (panelIndex == 0)
                {
                    menuRotator.RotateLeft();
                    AudioManager.Instance.PlayMenuSelectableSFX();
                }
                else if (panelIndex == 1)
                {
                    soundSlider.value -= 10;
                }
                break;
            case "RIGHT":
                if (panelIndex == 0)
                {
                    menuRotator.RotateRight();
                    AudioManager.Instance.PlayMenuSelectableSFX();
                }
                else if (panelIndex == 1)
                {
                    soundSlider.value += 10;
                }
                break;
            case "ENTER":
                MenuRotatorIndex(menuRotator.MenuRotatorIndex);
                AudioManager.Instance.PlayAcceptButtonSFX();
                break;
            case "EXIT":
                PanelToggle(0);
                AudioManager.Instance.PlayCancelButtonSFX();
                break;
        }
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void PanelToggle(int position)
    {
        panelIndex = position;

        Input.ResetInputAxes();

        for (int i = 0; i < panels.Length; i++)
            panels[i].SetActive(panelIndex == i);
    }
}
