using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    private bool _rotating = false;
    [SerializeField] private float _speed = 5.0f;

    private Vector2 _touchDownPosition;
    private Vector2 _touchUpPosition;
    [Header("Swipe Control")]
    [SerializeField] private float _minDistanceForSwipe = 50.0f;

    // Update is called once per frame
    void Update()
    {
        if (!_rotating)
        {
            if (Input.GetButtonDown("Left"))
            {
                StartCoroutine(Rotate(Vector3.up * -1.0f));
            }
            if (Input.GetButtonDown("Right"))
            {
                StartCoroutine(Rotate(Vector3.up));
            }
            if (Input.GetButtonDown("Up"))
            {
                StartCoroutine(Rotate(Vector3.right * -1.0f));
            }
            if (Input.GetButtonDown("Down"))
            {
                StartCoroutine(Rotate(Vector3.right));
            }
            
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    _touchDownPosition = touch.position;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    _touchUpPosition = touch.position;
                    SwipeControl();
                }
            }
        }

    }

    private void SwipeControl()
    {
        if (SwipedEnough())
        {
            if (IsVerticalSwipe())
            {
                if (_touchDownPosition.y - _touchUpPosition.y > 0)
                { //up
                    StartCoroutine(Rotate(Vector3.right * -1.0f));
                }
                else
                { //down
                    StartCoroutine(Rotate(Vector3.right));
                }
            }
            else
            {
                if (_touchDownPosition.x - _touchUpPosition.x > 0)
                { //right
                    StartCoroutine(Rotate(Vector3.up));
                }
                else
                { //left
                    StartCoroutine(Rotate(Vector3.up * -1.0f)); 
                }
            }
        }
    }

    private bool IsVerticalSwipe()
    {
        return VMDistance() > HMDistance();
    }

    private bool SwipedEnough()
    {
        return VMDistance() > _minDistanceForSwipe || HMDistance() > _minDistanceForSwipe;
    }

    private float VMDistance()
    {
        return Mathf.Abs(_touchDownPosition.y - _touchUpPosition.y);
    }

    private float HMDistance()
    {
        return Mathf.Abs(_touchDownPosition.x - _touchUpPosition.x);
    }

    IEnumerator Rotate(Vector3 direction)
    {
        _rotating = true;
        float lerpValue = 1.0f;
        var oldRot = transform.rotation;
        var newRot = transform.rotation * Quaternion.Euler(transform.InverseTransformDirection(direction) * 90.0f);
        while (lerpValue > 0)
        {
            lerpValue -= Time.deltaTime * _speed;
            transform.rotation = Quaternion.Slerp(newRot, oldRot, lerpValue);
            yield return null;
        }
        _rotating = false;
    }
}
