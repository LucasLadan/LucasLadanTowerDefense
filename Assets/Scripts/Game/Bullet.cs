using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _pierce;
    [SerializeField] private int _lifespan;
    [SerializeField] private float _speed;
    private void Awake()
    {
        
    }

    IEnumerator DoLifeSpan()
    {
        yield return new WaitForSeconds(_lifespan);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy _enemy = collision.gameObject.GetComponent<Enemy>();
        if (_enemy != null)
        {
            _pierce -= 1;
            if (_pierce < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public int GetDamage()
    { return _damage; }
}
