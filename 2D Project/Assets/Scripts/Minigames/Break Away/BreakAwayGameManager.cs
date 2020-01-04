using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakAwayGameManager : MonoBehaviour
{
    #region Singleton
    private static BreakAwayGameManager _instance = null;
    public static BreakAwayGameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<BreakAwayGameManager>();
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField] private List<BABlock> _bABlocks;
    [SerializeField] private Text _lifeText;
    private int _lifes = 2;
    private Color _colorGood = Color.green;
    private Color _colorBad = Color.red;
    private Color _colorNeutral = Color.yellow;

    private List<BABlock> _goodBlocks = new List<BABlock>();
    private List<BABlock> _badBlocks = new List<BABlock>();

    private void Start()
    {
        _lifeText.text = $"Chances Left: {_lifes}";
        var chosenBlockIndex = new List<int>();
        for (int i = 0; i < _bABlocks.Count; i++)
        {
            chosenBlockIndex.Add(i);
        }

        for (int i = 0; i < 4; i++)
        {
            int randomIndex = chosenBlockIndex[Random.Range(0, chosenBlockIndex.Count)];
            _bABlocks[randomIndex].InitBlock(_colorGood,BABlockType.Good);
            _goodBlocks.Add(_bABlocks[randomIndex]);
            chosenBlockIndex.Remove(randomIndex);
        }

        for (int i = 0; i < 4; i++)
        {
            int randomIndex = chosenBlockIndex[Random.Range(0, chosenBlockIndex.Count)];
            _bABlocks[randomIndex].InitBlock(_colorBad, BABlockType.Bad);
            _badBlocks.Add(_bABlocks[randomIndex]);
            chosenBlockIndex.Remove(randomIndex);
        }

        foreach (var index in chosenBlockIndex)
        {
            _bABlocks[index].InitBlock(_colorNeutral);
        }
    }

    public void RemoveBlock(BABlock block)
    {
        _goodBlocks.Remove(block);
        if (_badBlocks.Remove(block))
        {
            _lifes--;
        }
       
        if (_lifes == 0)
        {
            Debug.Log("Player Lost!");
            GameManager.Instance.MinigameEnd(false, -2);
        }
        else if (_goodBlocks.Count == 0)
        {
            Debug.Log("Player Win!");
            GameManager.Instance.MinigameEnd(true, 2);
        }
        _lifeText.text = $"Chances Left: {_lifes}";
    }
}
