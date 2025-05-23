using TMPro;
using UnityEngine;

public class ChallengeGameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameOverScreen;
    void Start()
    {
        if (FindFirstObjectByType<HealthController>())
        {
            FindFirstObjectByType<HealthController>().takenDamage.AddListener(CheckForGameOver);
        }
    }

    // Update is called once per frame
    private void CheckForGameOver(float health, float maxHealth, float damage)
    {
        if (health <= 0)
        {
            gameOverScreen.enabled = true;
        }
    }
}
