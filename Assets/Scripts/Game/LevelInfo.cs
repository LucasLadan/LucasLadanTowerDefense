using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "Scriptable Objects/LevelInfo")]
public class LevelInfo : ScriptableObject
{
    public List<Enemy> enemies;
    public int totalToSpawn;
    public List<int> waveSpawns;//The int is how many kills to spawn it
    public SpawnType spawnType;
    public float timeBetweenSpawns;
    public float timeBeforeSpawns;
    
    public enum SpawnType
    {
        spam,weight,cost
    }
}
