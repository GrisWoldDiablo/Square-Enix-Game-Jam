using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _position = 0;
    [SerializeField] private string _name = string.Empty;
    private Sprite _playerSprite;
    private Color _playerColor;
    private int _wonCount = 0;
    private int _lossCount = 0;

    private float _positionToMove = 0.0f;
    [SerializeField] private float _moveSpeed = 2.0f;
    public int Position { get => _position; }
    public string Name { get => _name; set => _name = value; }
    public Tile CurrentTile { get => BoardManager.Instance.PlayTiles[_position]; }
    public Sprite PlayerSprite { get => _playerSprite; }
    public Color PlayerColor { get => _playerColor; }
    public int WonCount { get => _wonCount; }
    public int LossCount { get => _lossCount; }

    private void Awake()
    {
        _playerSprite = GetComponent<SpriteRenderer>().sprite;
        _playerColor = GetComponent<SpriteRenderer>().color;
    }
    private void Start()
    {
        this.transform.position = BoardManager.Instance.PlayTiles[_position].transform.position;    
    }

    public void MovePosition(int value)
    {
        var futurePosition = _position + value;
        if (futurePosition >= BoardManager.Instance.PlayTiles.Count)
        {
            futurePosition = BoardManager.Instance.PlayTiles.Count - 1;
        }
        _positionToMove = futurePosition - _position;


        _position += value;
        if (_position < 0)
        {
            _position = 0;
        }
        if (_position >= BoardManager.Instance.PlayTiles.Count)
        {
            _position = BoardManager.Instance.PlayTiles.Count - 1;
        }
        this.transform.position = BoardManager.Instance.PlayTiles[_position].transform.position;
    }

    public void SetPosition(int value)
    {
        _position = value;
        this.transform.position = BoardManager.Instance.PlayTiles[_position].transform.position;
        PlayerManager.Instance.CurrentPlayer.CurrentTile.Action();
    }

    public void AddWon()
    {
        _wonCount++;
    }

    public void AddLost()
    {
        _lossCount++;
    }

    public void LerpPosition()
    {

    }
}
