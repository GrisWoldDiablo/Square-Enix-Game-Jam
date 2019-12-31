using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_BounceBall : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI livesLeftUI;
    [SerializeField] TextMeshProUGUI bouncesLeftUI;

    public TextMeshProUGUI LivesLeftUI { get => livesLeftUI; set => livesLeftUI = value; }
    public TextMeshProUGUI BouncesLeftUI { get => bouncesLeftUI; set => bouncesLeftUI = value; }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
