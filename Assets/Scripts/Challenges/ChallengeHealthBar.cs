using UnityEngine;
using UnityEngine.UI;

public class ChallengeHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    void Start()
    {
        if (FindFirstObjectByType<HealthController>())
        {
            FindFirstObjectByType<HealthController>().takenDamage.AddListener(UpdateBar);
        }    
        
    }

    private void UpdateBar(float health, float maxHealth,float damage)
    {
        healthBar.fillAmount = health/maxHealth;
    }
}
