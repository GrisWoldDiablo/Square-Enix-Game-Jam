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

    [SerializeField] private int _boardWidth = 15;
    [SerializeField] private int _boardHeight = 9;
    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private GameObject _tileWall;
    [SerializeField] private GameObject _tilePath;
    [SerializeField] private GameObject _tileMinigame;
    [SerializeField] private GameObject _tileStart;
    [SerializeField] private GameObject _tileEnd;
    [SerializeField] private List<Tile> tiles;
    public List<Tile> Tiles { get => tiles; set => tiles = value; }


    void Awake()
    {
        #region Dont Destroy On Load
        var objects = GameObject.FindObjectsOfType(this.GetType());
        if (objects.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion
    }


    private void Start()
    {
        var tiles = new List<GameObject>();
        var boardZeroX = ((_boardWidth - 1) / 2) * -1;
        var boardZeroY = ((_boardHeight - 1) / 2) * -1;
        for (int i = 0; i < _boardWidth; i++)
        {
            tiles.Add(Instantiate(_tileWall, new Vector3(boardZeroX + i, boardZeroY + _boardHeight-1), Quaternion.identity, _tileMap.transform));
            tiles.Add(Instantiate(_tileWall, new Vector3(boardZeroX + i, boardZeroY), Quaternion.identity, _tileMap.transform));
        }
        for (int i = 1; i < _boardHeight - 1; i++)
        {
            tiles.Add(Instantiate(_tileWall, new Vector3(boardZeroX, boardZeroY + i), Quaternion.identity, _tileMap.transform));
            tiles.Add(Instantiate(_tileWall, new Vector3(boardZeroX + _boardWidth-1, boardZeroY + i), Quaternion.identity, _tileMap.transform));
        }

        //var startTile = Instantiate(_tileStart, new Vector3(boardZeroX + 1, boardZeroY + 1), Quaternion.identity, _tileMap.transform);
        //tiles.Add(startTile);
        //var endTile = Instantiate(_tileEnd, new Vector3(boardZeroX + _boardWidth - 2, boardZeroY + _boardHeight - 2), Quaternion.identity, _tileMap.transform);
        //tiles.Add(endTile);
        //var newDir = new Vector3(1, 0);
        //var xDir = 1;
        //var yDir = 1;
        //var currentPosition = startTile.transform.position + (Vector3.right * xDir);
        //while (true)
        //{
        //    tiles.Add(Instantiate(_tilePath, currentPosition, Quaternion.identity, _tileMap.transform));

        //    if (currentPosition == endTile.transform.position)
        //    {
        //        break;
        //    }
        //    if (!tiles.Exists(e => e.transform.position == currentPosition + newDir))
        //    {
        //        currentPosition += newDir;
        //        continue;
        //    }
        //    else if (!tiles.Exists(e => e.transform.position == currentPosition + ((Vector3.up * yDir)* xDir)))
        //    {
        //        xDir *= -1;
        //        currentPosition += (Vector3.up * yDir);
        //    }

        //}

    }

}
