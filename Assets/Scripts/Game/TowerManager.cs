using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> _unlockedTowers = new List<GameObject>();
    [SerializeField] private TowerUI _uiTemplate;
    [SerializeField] private GridLayoutGroup _towerUnlocks;
    [SerializeField] private GridLayoutGroup _selectedLayout;
    private List<GameObject> _selectedTowers = new List<GameObject>();
    private GameObject _pickedTower;

    private StateManager.GameState _gameState;
    [SerializeField] private StateManager _stateManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _stateManager.UpdateGameState.AddListener(UpdateState);
        for (int i = 0; i < _unlockedTowers.Count; i++)
        {
            if ( _unlockedTowers[i].GetComponentInChildren<ITowerFunctions>() == null )
            {
                _unlockedTowers.RemoveAt(i);
                i--;
            }
            else
            {
                TowerUI newUI = Instantiate(_uiTemplate,_towerUnlocks.transform);
                newUI.SetTower(_unlockedTowers[i]);
            }
        }
    }

    public bool PickedTower(GameObject tower, bool equip)
    {
        Debug.Log("Selected");
        if (_gameState == StateManager.GameState.picking)
        {
            if (tower.GetComponent<ITowerFunctions>() != null)
            {
                if (equip && _selectedTowers.Count < 6)
                {
                    _selectedTowers.Add(tower);
                    return true;
                }
                else
                {
                    _selectedTowers.Remove(tower);
                }
            }
        }
        else if (_gameState == StateManager.GameState.gameplay)
        {
            _pickedTower = tower;
        }
        return false;
    }

    private void UpdateState(StateManager.GameState newGameState)
    {
        _gameState = newGameState;
        if (_gameState == StateManager.GameState.gameplay)
        {
            for (int i = 0; i < _selectedTowers.Count; i++)
            {
                TowerUI newUI = Instantiate(_uiTemplate, _selectedLayout.transform);
                newUI.SetTower(_selectedTowers[i]);
            }
        }
    }

    public GameObject GetPickedTower()
    {
        return _pickedTower;
    }

    public List<GameObject> GetSelectedTowers()
    {
        return _selectedTowers;
    }
}
