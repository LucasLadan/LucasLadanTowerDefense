using UnityEngine;

public class ChallengeParticleEvent : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    void Start()
    {
        if (FindFirstObjectByType<HealthController>())
        {
            FindFirstObjectByType<HealthController>().takenDamage.AddListener(EmitDamage);
        }
    }

    private void EmitDamage(float health, float maxHealth, float damage)
    {
        _particleSystem.Emit((int) damage);
    }
}
