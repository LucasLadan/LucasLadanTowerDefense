using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class GameplayState : MonoBehaviour
{
    [SerializeField] private StateManager _stateManager;
    [SerializeField] private LevelInfo _levelInfo;
    private EnemySpawnerManager _enemySpawnerManager;
    private float _points = 0;
    private float _wavePoints;
    private float _pointsToAdd = 1;
    private int _totalKills = 0;
    private int _totalSpawned = 0;
    private List<Enemy> _tempList = new List<Enemy>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _stateManager.UpdateGameState.AddListener(UpdateState);
        _enemySpawnerManager = FindFirstObjectByType<EnemySpawnerManager>();
    }

    public void EnemyKilled(float cost, bool isWave)
    {
        _totalKills += 1;
        if (!isWave)
        {
            _pointsToAdd += 0.1f;
            _points += (cost * 0.75f);
            _wavePoints += (cost * 0.25f);
            if (cost > 1.5f)
            {
                SpawnEnemy();
            }
        }
        if (_levelInfo.waveSpawns.Count > 0 && _totalKills == _levelInfo.waveSpawns[0])
        {
            SpawnWave();
            _levelInfo.waveSpawns.RemoveAt(0);
        }
        if (_totalKills >= _levelInfo.totalToSpawn)
        {
            Debug.Log("win");
        }
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
            StartCoroutine(ZombieSpawner());
            _points = 0;
            _pointsToAdd = 1;
            _totalKills = 0;
            _totalSpawned = 0;
        }
        else
        {
            StopCoroutine(ZombieSpawner());
        }
    }

    IEnumerator ZombieSpawner()
    {
        yield return new WaitForSeconds(30f);
        while (true)
        {
            yield return new WaitForSeconds(20f);
            _points += _pointsToAdd;
            _pointsToAdd += 0.25f;
            SpawnEnemy();
        }
    }

    private void SpawnWave()
    {
        Debug.Log(_points);
        while (_wavePoints > 0 && _totalSpawned < _levelInfo.totalToSpawn)
        {
            Enemy selectedEnemy = _levelInfo.enemies[0];

            switch (_levelInfo.spawnType)
            {
                case LevelInfo.SpawnType.cost:
                    selectedEnemy = _enemySpawnerManager.SpawnBasedOnHighest(_levelInfo.enemies, _points);
                    break;
                case LevelInfo.SpawnType.spam:
                    selectedEnemy = _enemySpawnerManager.SpawnBasedOnSpam(_levelInfo.enemies, _points);
                    break;
                case LevelInfo.SpawnType.weight:
                    
                    selectedEnemy = _enemySpawnerManager.SpawnBasedOnWeights(_tempList, _points);
                    break;
            }

            GameObject newEnemy = Instantiate(selectedEnemy.gameObject, new Vector2(17, UnityEngine.Random.Range(-3, 2)), new Quaternion(0, 0, 0, 0));
            newEnemy.GetComponent<Enemy>().SetIsWaveZombie(true);
            _wavePoints -= selectedEnemy.GetCost();
            Debug.Log("Spawned");
            _totalSpawned += 1;
        }
    }

    private void SpawnEnemy()
    {
        //Debug.Log(_points);
        if (_points >= 1 && _totalSpawned < _levelInfo.totalToSpawn)
        {
            Enemy selectedEnemy = _levelInfo.enemies[0];

            switch (_levelInfo.spawnType)
            {
                case LevelInfo.SpawnType.cost:
                    selectedEnemy = _enemySpawnerManager.SpawnBasedOnHighest(_levelInfo.enemies, _points);
                    break;
                case LevelInfo.SpawnType.spam:
                    selectedEnemy = _enemySpawnerManager.SpawnBasedOnSpam(_levelInfo.enemies, _points);
                    break;
                case LevelInfo.SpawnType.weight:
                    selectedEnemy = _enemySpawnerManager.SpawnBasedOnWeights(_levelInfo.enemies, _points);
                    break;
            }

            Instantiate(selectedEnemy.gameObject, new Vector2(12, UnityEngine.Random.Range(-3, 2)), new Quaternion(0, 0, 0, 0));
            _points -= selectedEnemy.GetCost();
            Debug.Log("Spawned");
            _totalSpawned += 1;
        }
    }
}
