using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BABlockType
{
    Neutral,
    Good,
    Bad
}
public class BABlock : MonoBehaviour
{
    private Color _colorChoosen;
    private Renderer _renderer;
    private BABlockType _type;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            BreakAwayGameManager.Instance.RemoveBlock(this);
            Destroy(gameObject);
        }
    }

    public void InitBlock(Color color, BABlockType type = BABlockType.Neutral)
    {
        _type = type;
        _colorChoosen = color;
        _renderer.material.SetColor("_Color", _colorChoosen);
    }
}
