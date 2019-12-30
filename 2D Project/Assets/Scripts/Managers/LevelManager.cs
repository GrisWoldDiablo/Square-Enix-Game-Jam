using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public struct Minigame
{
    public string name;
    public string sceneName;
    public Sprite minigameIcon;
}

public class LevelManager : MonoBehaviour
{
    #region Singleton
    private static LevelManager _instance = null;
    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LevelManager>();
            }
            return _instance;
        }
    }
    #endregion

    
    [SerializeField] private List<Minigame> _minigames;
    
    public List<Minigame> Minigames { get => _minigames; set => _minigames = value; }

    private AsyncOperation async;
    void Awake()
    {
        #region Dont Destroy On Load
        var objects = GameObject.FindObjectsOfType(this.GetType());
        if (objects.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion
    }

    public void LoadMinigameScene(string sceneName)
    {
        if (sceneName == "random")
        {
            sceneName = _minigames[UnityEngine.Random.Range(0, _minigames.Count)].sceneName;
        }

        async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        if (async != null)
        {
            async.allowSceneActivation = true;
            UIManager.Instance.MainCanvas.gameObject.SetActive(false);
            BoardManager.Instance.TileMap.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("No Scene found");
        }
    }

    public void UnloadLastScene()
    {
        var currentScene = SceneManager.GetSceneAt(SceneManager.sceneCount-1);
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
