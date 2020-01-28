using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

 public class MenuRotator : MonoBehaviour
{
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private float rotatingTime;

    private int menuIndex = 0;
    private bool isRotating = false;

    public int MenuRotatorIndex { get => menuIndex; set => menuIndex = value; }

    public void RotateLeft()
    {
        if (!isRotating)
        {
            isRotating = true;
            MenuIndex(-1);
            StartCoroutine(Rotate(new Vector3(0, -90, 0)));
        }
    }
    public IEnumerator Rotate(Vector3 direction)
    {
        Quaternion start = transform.rotation;
        Quaternion end = start * Quaternion.Euler(direction);

        float rate = 1.0f / rotatingTime;
        float timer = 0.0f;

        while (timer < 1.0)
        {
            timer += Time.deltaTime * rate;
            transform.rotation = Quaternion.Slerp(start, end, timer);
            yield return null;
        }

        buttons[menuIndex].GetComponent<Image>().color = new Color(0.95f, 0.95f, 0);
        buttons[menuIndex].GetComponent<Animator>().SetBool("Selected", true);
        isRotating = false;
    }

    public void RotateRight()
    {
        if (!isRotating)
        {
            isRotating = true;
            MenuIndex(1);
            StartCoroutine(Rotate(new Vector3(0, 90, 0))); 
        }
    }

    void MenuIndex(int direction) // 1 Right, -1 left
    {
        buttons[menuIndex].GetComponent<Image>().color = new Color(1, 1, 1);
        buttons[menuIndex].GetComponent<Animator>().SetBool("Selected", false);

        menuIndex += direction;

        if (menuIndex < 0)
            menuIndex = 3;
        else if (menuIndex > 3)
            menuIndex = 0;
    }
}
