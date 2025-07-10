using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DefaultTower : MonoBehaviour, ITowerFunctions
{
    [SerializeField] private TowerStats _stats;
    private int _health = 0;

    private Tiles _onTile;

    void Awake()
    {
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer != null)
        {
            _spriteRenderer.sprite = _stats._sprite;
        }
        _health = _stats._health;
        StartCoroutine(ReloadTime());
    }

    public bool CheckForEnemy()
    { 
        return Physics2D.Raycast(transform.position, Vector2.right, _stats._range, LayerMask.GetMask("Enemy"));
    }

    public IEnumerator ReloadTime()
    {
        while (true)
        {
            if (CheckForEnemy())
            {
                Shoot();
                yield return new WaitForSeconds(_stats._fireRate);
            }
            else
            {
                yield return new WaitForSeconds(_stats._checkTime);
            }

        }
        
    }

    public void Shoot()
    {
        Instantiate(_stats._bullet,gameObject.transform.position,new Quaternion(0,0,0,0));
    }

    public TowerStats GetTowerStats()
    {
        return _stats;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            _onTile.TowerDestroyed();
        }
    }

    public void TileOn(Tiles _tile)
    {
        _onTile = _tile;
    }
}
