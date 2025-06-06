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
    private float _speedMult = 1;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private bool isStalling = true;
    [SerializeField] private float cost;

    private float _stallTimer = 60f;

    private void Awake()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            
            _health -= bullet.GetDamage();
            if (bullet.GetDamage() > 0)
            {
                _stallTimer = 60f;
                if (_health < 0)
                {
                    _stateManager.UpdateGameState.RemoveListener(StateChanged);
                    _gameplayState.EnemyKilled(cost);
                    Destroy(gameObject);
                    return;
                }
            }
        }
        ITowerFunctions towerFunctions = collision.gameObject.GetComponent<ITowerFunctions>();
        if (towerFunctions != null )
        {
            _speedMult = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ITowerFunctions towerFunctions = collision.gameObject.GetComponent<ITowerFunctions>();
        if (towerFunctions != null)
        {
            _speedMult = 1;
        }
    }

    IEnumerator StallAvoidance()
    {
        yield return new WaitForSeconds(_stallTimer);
        if (isStalling)
        {
            _speed *= 2;
            _attackSpeed /= 5;
        }
    }

    private void Update()
    {
        _rigidbody.linearVelocity = new Vector2(_speed * _speedMult * -1,0);
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
}
