using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private StateManager _stateManager;
    private Rigidbody2D _rigidbody;
    private GameplayState _gameplayState;

    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    private bool _isAttacking;
    private bool _isWaveZombie = false;
    private float _speedMult = 1;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float cost;


    virtual public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _stateManager = FindFirstObjectByType<StateManager>();
        _gameplayState = FindFirstObjectByType<GameplayState>();
        if ( _stateManager == null )
        {
            Debug.Log("StateManager doesn't exist");
            Destroy(gameObject);
        }
        else
        {
            _stateManager.UpdateGameState.AddListener(StateChanged);
        }
    }

    virtual public void OnBulletHit(Bullet bullet)
    {
        _health -= bullet.GetDamage();
        if (bullet.GetDamage() > 0)
        {
            if (_health < 0)
            {
                _gameplayState.EnemyKilled(cost,_isWaveZombie);
                Destroy(gameObject);
                return;
            }
        }
    }

    virtual public void OnTowerTriggered(ITowerFunctions towerFunctions)
    {
        _speedMult = 0;
        StartCoroutine(DoDamage(towerFunctions));
    }

    virtual public IEnumerator DoDamage(ITowerFunctions towerFunctions)
    {
        while (_isAttacking)
        {
            yield return new WaitForSeconds(_attackSpeed);
            towerFunctions.TakeDamage(_damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            OnBulletHit(bullet);            
        }
        ITowerFunctions towerFunctions = collision.gameObject.GetComponent<ITowerFunctions>();
        if (towerFunctions != null)
        {
            _isAttacking = true;
            OnTowerTriggered(towerFunctions);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ITowerFunctions towerFunctions = collision.gameObject.GetComponent<ITowerFunctions>();
        if (towerFunctions != null)
        {
            _speedMult = 1;
            _isAttacking = false;
        }
    }

    private void OnDestroy()
    {
        _stateManager.UpdateGameState.RemoveListener(StateChanged);
    }

    private void Update()
    {
        _rigidbody.linearVelocity = new Vector2(_speed/10 * _speedMult * -1,0);
    }

    private void StateChanged(StateManager.GameState gameState)
    {
        if (gameState == StateManager.GameState.menu || gameState == StateManager.GameState.picking)
        {
            _stateManager.UpdateGameState.RemoveListener(StateChanged);
        }
    }

    public float GetCost()
    { return cost; }

    public void SetIsWaveZombie(bool newBool)
    { _isWaveZombie = newBool; }

    public void SetSpeedMult(float speedMult)
    {  _speedMult = speedMult; }

    public void ChangeHealth(int damage)
    { _health -= damage; }

    public int GetHealth()
    { return _health; }
}
