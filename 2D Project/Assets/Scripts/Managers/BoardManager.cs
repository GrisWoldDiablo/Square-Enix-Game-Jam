using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    #region Singleton
    private static BoardManager _instance = null;
    public static BoardManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<BoardManager>();
            }
            return _instance;
        }
    }
    #endregion

    
    [Header("Tiles' Sprites")]
    [SerializeField] private Sprite _noneSprite;
    [SerializeField] private Sprite _wallSprite;
    [SerializeField] private Sprite _pathSprite;
    [SerializeField] private Sprite _startSprite;
    [SerializeField] private Sprite _endSprite;
    [SerializeField] private Sprite _snakeHeadSprite;
    [SerializeField] private Sprite _snakeTailSprite;
    [SerializeField] private Sprite _ladderStartSprite;
    [SerializeField] private Sprite _ladderEndSprite;
    [SerializeField] private Sprite _minigameDefaultSprite;

    public Sprite NoneSprite { get => _noneSprite; }
    public Sprite WallSprite { get => _wallSprite; }
    public Sprite PathSprite { get => _pathSprite; }
    public Sprite StartSprite { get => _startSprite; }
    public Sprite EndSprite { get => _endSprite; }
    public Sprite SnakeHeadSprite { get => _snakeHeadSprite; }
    public Sprite SnakeTailSprite { get => _snakeTailSprite; }
    public Sprite LadderStartSprite { get => _ladderStartSprite; }
    public Sprite LadderEndSprite { get => _ladderEndSprite; }
    public Sprite MinigameDefaultSprite { get => _minigameDefaultSprite; }

    [Header("Grid Tilemap")]
    [SerializeField] private Tilemap _tileMap;
    [Header("Board game play tiles from start to end.")]
    [SerializeField] private List<Tile> playTiles;

    public List<Tile> PlayTiles { get => playTiles; set => playTiles = value; }
    public Tilemap TileMap { get => _tileMap; set => _tileMap = value; }

    void Awake()
    {
        #region Dont Destroy On Load
        var objects = GameObject.FindObjectsOfType(this.GetType());
        if (objects.Length > 1)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            ManagersList.Instance.Managers.Add(this.gameObject);
            DontDestroyOnLoad(this.gameObject);
        }
        #endregion
    }

    private void Start()
    {
        foreach (var player in PlayerManager.Instance.Players)
        {
            player.gameObject.transform.parent = _tileMap.transform;
            player.transform.position = playTiles[0].transform.position;
            player.gameObject.SetActive(true);
        }
    }
}
