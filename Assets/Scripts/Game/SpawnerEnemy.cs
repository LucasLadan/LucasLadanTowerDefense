using System.Collections;
using UnityEngine;

public class SpawnerEnemy : Enemy
{
    [SerializeField] private Enemy _spawnedEnemy;
    [SerializeField] private float _spawnTime;
    [SerializeField] private float _animationTime;

    public override void Awake()
    {
        StartCoroutine(SpawnEnemyTimer());
        base.Awake();
    }

    private void SpawnEnemy()
    {
        Instantiate(_spawnedEnemy,transform.position + Vector3.left,new Quaternion(0,0,0,0));
    }

    IEnumerator SpawnEnemyTimer()
    {
        while (transform.position.x > 2)
        {
            yield return new WaitForSeconds(_spawnTime);
            SetSpeedMult(0);
            SpawnEnemy();
            yield return new WaitForSeconds(_animationTime);
            SetSpeedMult(1);
        }
    }
}
