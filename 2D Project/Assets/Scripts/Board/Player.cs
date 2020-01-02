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
    private float _speedPerTile;
    private Vector3 _currentPosition;

    public int Position { get => _position; }
    public string Name { get => _name; set => _name = value; }
    public Tile CurrentTile { get => BoardManager.Instance.PlayTiles[_position]; }
    public Sprite PlayerSprite { get => _playerSprite; }
    public Color PlayerColor { get => _playerColor; set => GetComponent<SpriteRenderer>().color = value; }

    public int WonCount { get => _wonCount; }
    public int LossCount { get => _lossCount; }

    public void MovePosition(int value, bool performAction = true)
    {
        var futurePosition = _position + value;
        if (futurePosition >= BoardManager.Instance.PlayTiles.Count)
        {
            futurePosition = BoardManager.Instance.PlayTiles.Count - 1;
        }
        else if (futurePosition < 0)
        {
            futurePosition = 0;
        }
        _positionToMove = futurePosition - _position;
        _speedPerTile = Mathf.Abs(_moveSpeed / _positionToMove);

        StartCoroutine(LerpMove(performAction));
    }

    public void SetPosition(int value)
    {
        StartCoroutine(LerpPosition(value));
    }

    public void AddWon()
    {
        _wonCount++;
    }

    public void AddLost()
    {
        _lossCount++;
    }

    public IEnumerator LerpMove(bool performAction)
    {
        float lerpValue;
        int modifier = 1;
        if (_positionToMove < 0)
        {
            modifier = -1;
        }
        for (int i = 1; i <= _positionToMove * modifier; i++)
        {
            lerpValue = 1.0f;
            _currentPosition = this.transform.position;
            if (_positionToMove > 0)
            {
                _position++; 
            }
            else
            {
                _position--;
            }
            while (lerpValue > 0)
            {
                lerpValue -=  Time.deltaTime / _speedPerTile;
                this.transform.position = Vector3.Lerp(BoardManager.Instance.PlayTiles[_position ].transform.position,_currentPosition,lerpValue);
                yield return null;
            }
        }
        if (performAction)
        {
            CurrentTile.Action();
        }
        else
        {
            GameManager.Instance.RoundEnd();
        }
    }

    public IEnumerator LerpPosition(int newPosition)
    {
        
        float lerpValue = 1.0f;
        _currentPosition = this.transform.position;
        _position = newPosition;
        while (lerpValue > 0)
        {
            lerpValue -= Time.deltaTime / _moveSpeed;
            this.transform.position = Vector3.Lerp(BoardManager.Instance.PlayTiles[_position].transform.position, _currentPosition, lerpValue);
            yield return null;
        }

        CurrentTile.Action();
    }

    public void Init()
    {
        _playerSprite = GetComponent<SpriteRenderer>().sprite;
        _playerColor = GetComponent<SpriteRenderer>().color;
    }
}
