using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NumberOfPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSinglePlayer()
    {
        SceneManager.LoadScene("SinglePlayer");
    }

    public void LoadMultiPlayer()
    {
        SceneManager.LoadScene("MultiPlayer");
    }
}
