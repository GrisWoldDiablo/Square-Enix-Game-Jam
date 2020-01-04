using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputChanged : MonoBehaviour
{
    [SerializeField] private InputField _inputField;
    [SerializeField] private Button _button;
    public void SetInteractable()
    {
        _button.interactable = _inputField.text != string.Empty;
    }
}
