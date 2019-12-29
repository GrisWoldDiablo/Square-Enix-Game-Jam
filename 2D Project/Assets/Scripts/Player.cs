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

    public int Position { get => _position; }
    public string Name { get => _name; set => _name = value; }
    public Tile CurrentTile { get => BoardManager.Instance.Tiles[_position]; }
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
        this.transform.position = BoardManager.Instance.Tiles[_position].transform.position;    
    }

    public void MovePosition(int value)
    {
        _position += value;
        if (_position >= BoardManager.Instance.Tiles.Count)
        {
            _position = BoardManager.Instance.Tiles.Count - 1;
        }
        this.transform.position = BoardManager.Instance.Tiles[_position].transform.position;
        PlayerManager.Instance.CurrentPlayer.CurrentTile.Action();
    }

    public void SetPosition(int value)
    {
        _position = value;
        this.transform.position = BoardManager.Instance.Tiles[_position].transform.position;
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
}
