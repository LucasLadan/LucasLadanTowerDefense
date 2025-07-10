using System.Collections;
using UnityEngine;

public class ProducerTower : MonoBehaviour, ITowerFunctions
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
        //Not needed for this
        return true;
    }

    public TowerStats GetTowerStats()
    {
        return _stats;
    }

    public IEnumerator ReloadTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(_stats._fireRate);
            Shoot();
        }
    }

    public void Shoot()
    {
        Instantiate(_stats._bullet, gameObject.transform.position + Vector3.back, new Quaternion(0, 0, 0, 0));
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
