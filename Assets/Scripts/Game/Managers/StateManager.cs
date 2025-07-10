using UnityEngine;
using UnityEngine.Events;

public class StateManager : MonoBehaviour
{
    private GameState _currentState;
    public UnityEvent<GameState> UpdateGameState;

    void Start()
    {
        _currentState = GameState.menu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(GameState newState)
    {
        if (newState != _currentState)
        {
            _currentState = newState;
            UpdateGameState.Invoke(_currentState);
        }
    }

    public enum GameState
    {
        menu,picking,gameplay,win,lose
    }
}
