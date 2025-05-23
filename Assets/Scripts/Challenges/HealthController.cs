using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    [SerializeField] private Image healthBar;


    public UnityEvent<float,float,float> takenDamage;

    private float _health;
    void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        takenDamage.Invoke(_health, _maxHealth,damage);
    }
}
