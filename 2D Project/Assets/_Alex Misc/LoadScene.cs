using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private AsyncOperation _async;

    public void LoadNewScene()
    {
        if (_async == null)
        {
            Scene curScene = SceneManager.GetActiveScene();
            int nextScene = curScene.buildIndex + 1;
            if (nextScene >= SceneManager.sceneCountInBuildSettings)
            {
                nextScene = 0;
                Debug.Log("Last scene werapping arround to first scene.");
            }
            if ((_async = SceneManager.LoadSceneAsync(nextScene)) != null)
            {
                _async.allowSceneActivation = true;
            }
        }
    }
}
