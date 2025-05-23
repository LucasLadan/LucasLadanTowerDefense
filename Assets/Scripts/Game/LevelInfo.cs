using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "Scriptable Objects/LevelInfo")]
public class LevelInfo : ScriptableObject
{
    public List<Enemy> enemies;
    public int totalToSpawn;
    public List<int> waveSpawns;
}
