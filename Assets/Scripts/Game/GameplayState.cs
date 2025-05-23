using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class GameplayState : MonoBehaviour
{
    [SerializeField] private StateManager _stateManager;
    [SerializeField] private LevelInfo _levelInfo;
    private int _points = 0;
    private int _totalKills = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _stateManager.UpdateGameState.AddListener(UpdateState);
    }


    public void EnemyKilled(int cost)
    {
        _totalKills += 1;
        _points += (int)(math.round(cost / 2)+1);
    }

    public void LevelPicked(LevelInfo _newInfo)
    {
        _levelInfo = _newInfo;
        _stateManager.ChangeState(StateManager.GameState.picking);
    }

    private void UpdateState(StateManager.GameState gameState)
    {
        if (gameState == StateManager.GameState.gameplay)
        {
            _points = 0;
            _totalKills = 0;
        }
    }

    private void SpawnEnemy()
    {
        Enemy SelectedEnemy = _levelInfo.enemies[0];
        for (int i = 0; i < _levelInfo.enemies.Count; i++)
        {
            if (_levelInfo.enemies[i].GetCost() <= _points)
            {
                if (SelectedEnemy.GetCost() < _levelInfo.enemies[i].GetCost())
                {
                    SelectedEnemy = _levelInfo.enemies[i];
                }
                else if (SelectedEnemy.GetCost() == _levelInfo.enemies[i].GetCost() && UnityEngine.Random.Range(1,2) == 2)
                {
                    SelectedEnemy = _levelInfo.enemies[i];
                }
            }
        }
    }
}
