using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TestTower : MonoBehaviour, ITowerFunctions
{
    [SerializeField] private TowerStats _stats;

    private Tiles _onTile;

    public bool CheckForEnemy()
    {
        return true;
    }



    public IEnumerator ReloadTime()
    {
        while (true)
        {
            if (CheckForEnemy())
            {
                yield return new WaitForSeconds(_stats._fireRate);
                Shoot();
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }
        
    }

    public void Shoot()
    {
        Instantiate(_stats._bullet,gameObject.transform.position,new Quaternion(0,0,0,0));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer != null)
        {
            _spriteRenderer.sprite = _stats._sprite;
        }
        StartCoroutine(ReloadTime());
    }

    public TowerStats GetTowerStats()
    {
        return _stats;
    }

    public void TileOn(Tiles _tile)
    {
        _onTile = _tile;
    }
}
