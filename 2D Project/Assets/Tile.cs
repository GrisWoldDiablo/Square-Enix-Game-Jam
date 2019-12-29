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
    LadderStart,
    LadderEnd
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
    [SerializeField] private Tile gotoTile;
    [SerializeField] private List<string> _minigameSceneNames;
    private int _position;


    internal TileType Type { get => _type; set => _type = value; }
    public int Position { get => _position; set => _position = value; }

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
                Debug.Log("Tile Path");
                GameManager.Instance.RoundEnd();
                break;
            case TileType.Minigame:
                Debug.Log("Tile Minigame");
                LevelManager.Instance.LoadMinigameScene(_minigameSceneNames[Random.Range(0,_minigameSceneNames.Count)]);
                break;
            case TileType.Start:
                break;
            case TileType.End:
                Debug.Log("Tile End");
                break;
            case TileType.SnakeHead:
                Debug.Log("Tile Snake Head");
                if (gotoTile != null)
                {
                    PlayerManager.Instance.CurrentPlayer.SetPosition(gotoTile.Position);
                }
                break;
            case TileType.SnakeTail:
                Debug.Log("Tile Snake Tail");
                break;
            case TileType.LadderStart:
                Debug.Log("Tile Ladder");
                if (gotoTile != null)
                {
                    PlayerManager.Instance.CurrentPlayer.SetPosition(gotoTile.Position);
                }
                break;
            case TileType.LadderEnd:
                Debug.Log("Tile LadderEnd");
                GameManager.Instance.RoundEnd();
                break;
            default:
                break;
        }
    }
}
