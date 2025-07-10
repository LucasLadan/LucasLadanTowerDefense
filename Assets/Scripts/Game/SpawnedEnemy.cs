using UnityEngine;
using static UnityEngine.CullingGroup;

public class SpawnedEnemy : Enemy
{
    public override void OnBulletHit(Bullet bullet)
    {
        ChangeHealth(bullet.GetDamage());
        if (bullet.GetDamage() > 0)
        {
            if (GetHealth() < 0)
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}
