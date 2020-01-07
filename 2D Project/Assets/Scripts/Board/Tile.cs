using System;
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
public class ObjectBuilderEditor : Editor
{
    private TileType _tileType;
    private Tile _myTile;
    SerializedProperty sceneName;
    private void OnEnable()
    {
        _myTile = (Tile)target;
        sceneName = serializedObject.FindProperty("_minigameSceneName");
    }

    public override void OnInspectorGUI()
    {
        if (!_myTile.gameObject.activeInHierarchy)
        {
            DrawDefaultInspector();
            return;
        }
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_type"), true);

        if (_myTile.Type == TileType.LadderStart || _myTile.Type == TileType.SnakeHead)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_gotoTile"), true); 
        }

        if (_myTile.Type == TileType.Minigame)
        {
            EditorGUILayout.PropertyField(sceneName);
            SetMinigameButtons();
        }

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }

        UpdateObject();
    }

    private void UpdateObject()
    {
        switch (_myTile.Type)
        {
            case TileType.None:
                _myTile.gameObject.name = "Tile None";
                _myTile.GetComponent<SpriteRenderer>().sprite = BoardManager.Instance.NoneSprite;
                break;
            case TileType.Wall:
                _myTile.gameObject.name = "Tile Wall";
                _myTile.GetComponent<SpriteRenderer>().sprite = BoardManager.Instance.WallSprite;
                break;
            case TileType.Path:
                _myTile.gameObject.name = "Tile Path";
                _myTile.GetComponent<SpriteRenderer>().sprite = BoardManager.Instance.PathSprite;
                break;
            case TileType.Start:
                _myTile.gameObject.name = "Tile Start";
                _myTile.GetComponent<SpriteRenderer>().sprite = BoardManager.Instance.StartSprite;
                break;
            case TileType.End:
                _myTile.gameObject.name = "Tile End";
                _myTile.GetComponent<SpriteRenderer>().sprite = BoardManager.Instance.EndSprite;
                break;
            case TileType.SnakeHead:
                _myTile.gameObject.name = "Tile Snake Head";
                _myTile.GetComponent<SpriteRenderer>().sprite = BoardManager.Instance.SnakeHeadSprite;
                break;
            case TileType.SnakeTail:
                _myTile.gameObject.name = "Tile Snake Tail";
                _myTile.GetComponent<SpriteRenderer>().sprite = BoardManager.Instance.SnakeTailSprite;
                break;
            case TileType.LadderStart:
                _myTile.gameObject.name = "Tile Ladder Start";
                _myTile.GetComponent<SpriteRenderer>().sprite = BoardManager.Instance.LadderStartSprite;
                break;
            case TileType.LadderEnd:
                _myTile.gameObject.name = "Tile Ladder End";
                _myTile.GetComponent<SpriteRenderer>().sprite = BoardManager.Instance.LadderEndSprite;
                break;
            default:
                break;
        }
    }

    private void SetMinigameButtons()
    {
        GUILayout.Box("Minigame choices");
        for (int i = 0; i < LevelManager.Instance.Minigames.Count; i++)
        {
            if (GUILayout.Button($"Set {LevelManager.Instance.Minigames[i].name}", GUILayout.MaxWidth(175), GUILayout.Height(25)))
            {
                ChangeMinigame(i);
            }
        }
        if (GUILayout.Button($"Set Random", GUILayout.MaxWidth(175), GUILayout.Height(25)))
        {
            ChangeMinigame(-1);
        }
    }

    private void ChangeMinigame(int value)
    {
        if (value == -1)
        {
            _myTile.GetComponent<SpriteRenderer>().sprite = BoardManager.Instance.MinigameDefaultSprite;

            sceneName.stringValue = "random";
            Debug.Log($"Minigame is set to Random.");
            _myTile.gameObject.name = "Tile Minigame Random";
            
        }
        else
        {
            Debug.Log($"Minigame : {LevelManager.Instance.Minigames[value].name} set.");
            _myTile.GetComponent<SpriteRenderer>().sprite = LevelManager.Instance.Minigames[value].minigameIcon;
            sceneName.stringValue = LevelManager.Instance.Minigames[value].sceneName;
            _myTile.gameObject.name = $"Tile Minigame ({LevelManager.Instance.Minigames[value].name})";
        }
    }
}
#endif

public class Tile : MonoBehaviour
{
    [SerializeField] private TileType _type;
    [SerializeField] private Tile _gotoTile;
    [SerializeField] private string _minigameSceneName;
    private int _position;
    
    internal TileType Type { get => _type; set => _type = value; }
    public int Position { get => _position; set => _position = value; }

    private void Start()
    {
        if (_type != TileType.None || _type != TileType.Wall)
        {
            _position = BoardManager.Instance.PlayTiles.IndexOf(this);
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
                LevelManager.Instance.LoadMinigameScene(_minigameSceneName);
                break;
            case TileType.Start:
                break;
            case TileType.End:
                Debug.Log("Tile End");
                UIManager.Instance.ShowWinnerPanel();
                break;
            case TileType.SnakeHead:
                Debug.Log("Tile Snake Head");
                if (_gotoTile != null)
                {
                    PlayerManager.Instance.CurrentPlayer.SetPosition(_gotoTile.Position);
                }
                break;
            case TileType.SnakeTail:
                Debug.Log("Tile Snake Tail");
                GameManager.Instance.RoundEnd();
                break;
            case TileType.LadderStart:
                Debug.Log("Tile Ladder");
                if (_gotoTile != null)
                {
                    PlayerManager.Instance.CurrentPlayer.SetPosition(_gotoTile.Position);
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

    public void ChangeSceneName(string name)
    {
        _minigameSceneName = name;
    }


}
