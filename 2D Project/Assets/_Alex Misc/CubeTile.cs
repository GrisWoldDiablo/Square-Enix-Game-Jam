using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTile : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<CubeTile> _connectedCubeTiles;
    [SerializeField] private GameObject _takenTile;
    private bool _isTaken;
    public List<CubeTile> ConnectedCubeTiles { get => _connectedCubeTiles; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (CubeMazeManager.Instance.CurrentCubeTile.ConnectedCubeTiles.Contains(this) /*&& !_isTaken*/)
        {
            TakeTile();
            CubeMazeManager.Instance.MovePlayer();
        }
    }

    public void TakeTile()
    {
        //var takenTile = Instantiate(_takenTile, CubeMazeManager.Instance.CurrentCubeTile.transform);
        //takenTile.transform.position += takenTile.transform.forward * -0.01f;
        //_spriteRenderer.material.SetColor("_Color", Color.red);
        CubeMazeManager.Instance.CurrentCubeTile = this;
        //_isTaken = true;
    }
}
