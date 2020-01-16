using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMazeManager : MonoBehaviour
{
    #region Singleton
    private static CubeMazeManager _instance = null;
    public static CubeMazeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CubeMazeManager>();
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _mazePlayer;
    [SerializeField] private CubeTile _currentCubeTile;
    public CubeTile CurrentCubeTile { get => _currentCubeTile; set => _currentCubeTile = value; }
    public GameObject MazePlayer { get => _mazePlayer; set => _mazePlayer = value; }
    public Camera MainCamera { get => _mainCamera; }

    public void Start()
    {
        //CurrentCubeTile.TakeTile();
        _mazePlayer = Instantiate(_mazePlayer, CurrentCubeTile.transform);
        MovePlayer();
    }
    
    private void Update()
    {
        _mazePlayer.transform.rotation = _mainCamera.transform.rotation;
    }

    public void MovePlayer()
    {
        _mazePlayer.transform.parent = CurrentCubeTile.transform;
        _mazePlayer.transform.SetPositionAndRotation(CurrentCubeTile.transform.position, _mainCamera.transform.rotation);
        _mazePlayer.transform.position += _mazePlayer.transform.forward * -0.1f;
    }
}
