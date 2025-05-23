using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private StateManager _stateManager;

    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private bool isStalling = true;
    [SerializeField] private int cost;

    private void Awake()
    {
        _stateManager = GetComponent<StateManager>();
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
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            
            _health -= bullet.GetDamage();
            if (bullet.GetDamage() > 0)
            {
                isStalling = false;
                if (_health < 0)
                {
                    _stateManager.UpdateGameState.RemoveListener(StateChanged);
                    Destroy(gameObject);
                }
            }
        }
    }

    IEnumerator StallAvoidance()
    {
        yield return new WaitForSeconds(60f);
        if (isStalling)
        {
            _speed *= 2;
            _attackSpeed /= 5;
        }
    }

    private void StateChanged(StateManager.GameState gameState)
    {
        if (gameState == StateManager.GameState.menu || gameState == StateManager.GameState.picking)
        {
            _stateManager.UpdateGameState.RemoveListener(StateChanged);
        }
    }

    public int GetCost()
    { return cost; }
}
