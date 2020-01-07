using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagersList : MonoBehaviour
{
    #region Singleton
    private static ManagersList _instance = null;
    public static ManagersList Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ManagersList>();
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField] private List<GameObject> _managers;
    public List<GameObject> Managers { get => _managers; set => _managers = value; }

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
            _managers = new List<GameObject>();
            DontDestroyOnLoad(this.gameObject); 
        }
        #endregion
    }

    public void DestroyAllManagers()
    {
        foreach (var manager in _managers)
        {
            Destroy(manager);
        }
        _managers.Clear();
    }
}
