using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

enum TileType
{
    None,
    Wall,
    Path,
    Minigame,
    Start,
    End,
    SnakeHead,
    SnakeTail,
    Ladder
}

#if (UNITY_EDITOR)
[CustomEditor(typeof(Tile))]
[CanEditMultipleObjects]
public class ObjectBuilderEditor : Editor
{
    private TileType _tileType;
    private Tile _myScript;

    private void OnEnable()
    {
        _myScript = (Tile)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        UpdateObject();
    }

    public void UpdateObject()
    {
        _tileType = _myScript.Type;
    }
}
#endif

public class Tile : MonoBehaviour
{
    [SerializeField] private TileType _type;
    private int _position;

    internal TileType Type { get => _type; set => _type = value; }
    public int Position { get => _position; set => _position = value; }
    public Tile gotoTile;

    private void Start()
    {
        if (Type != TileType.None || Type != TileType.Wall)
        {
            _position = BoardManager.Instance.Tiles.IndexOf(this);
        }
    }

    public void Action()
    {
        switch (_type)
        {
            case TileType.None:
                break;
            case TileType.Wall:
                break;
            case TileType.Path:
                Debug.Log("Path Tile");
                break;
            case TileType.Minigame:
                Debug.Log("Path Minigame");
                break;
            case TileType.Start:
                break;
            case TileType.End:
                Debug.Log("Path End");
                break;
            case TileType.SnakeHead:
                Debug.Log("Path Snake Head");
                if (gotoTile != null)
                {
                    PlayerManager.Instance.CurrentPlayer.SetPosition(gotoTile.Position);
                }
                break;
            case TileType.SnakeTail:
                Debug.Log("Path Snake Tail");
                break;
            case TileType.Ladder:
                Debug.Log("Path Ladder");
                if (gotoTile != null)
                {
                    PlayerManager.Instance.CurrentPlayer.SetPosition(gotoTile.Position);
                }
                break;
            default:
                break;
        }
    }
}
