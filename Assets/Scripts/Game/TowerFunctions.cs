using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public interface ITowerFunctions
{
    public void TileOn(Tiles _tile);
    public void Shoot();

    public bool CheckForEnemy();
    
    public TowerStats GetTowerStats();

    IEnumerator ReloadTime();
}
