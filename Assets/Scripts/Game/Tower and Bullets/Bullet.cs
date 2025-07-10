using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _pierce;
    [SerializeField] private float _lifespan;
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if ( _rigidbody != null )
        {
            _rigidbody.linearVelocity = new Vector2(1 * _speed, 0);
        }
        StartCoroutine(DoLifeSpan());
    }

    IEnumerator DoLifeSpan()
    {
        yield return new WaitForSeconds(_lifespan);
        Destroy(gameObject);
    }

    virtual public void OnTriggerEnter2D(Collider2D collision)
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
