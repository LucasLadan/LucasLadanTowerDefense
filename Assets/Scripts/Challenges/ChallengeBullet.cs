using UnityEngine;

public class ChallengeBullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthController _healthController = collision.gameObject.GetComponent<HealthController>();
        if (_healthController != null)
        {
            _healthController.TakeDamage(50f);
        }
    }
}
