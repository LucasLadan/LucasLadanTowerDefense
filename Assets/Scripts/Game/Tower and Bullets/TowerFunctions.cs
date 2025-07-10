using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public interface ITowerFunctions
{
    public void TileOn(Tiles _tile);
    public void Shoot();

    public bool CheckForEnemy();
    
    public TowerStats GetTowerStats();

    public void TakeDamage(int damage);

    IEnumerator ReloadTime();
}
