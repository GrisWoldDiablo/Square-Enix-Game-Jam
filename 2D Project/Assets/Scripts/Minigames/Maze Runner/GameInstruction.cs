using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameInstruction : MonoBehaviour
{
    [SerializeField] Text textUI;
    private Image colorPanel;
    public float timer = 6.0f;
    

    // Start is called before the first frame update
    void Start()
    {
       colorPanel  = GameObject.Find("Panel").GetComponent<Image>();
       colorPanel.color = UnityEngine.Color.black;
       colorPanel.CrossFadeAlpha(0, 5.0f, true);
       textUI.CrossFadeAlpha(0, 5.0f, true);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
