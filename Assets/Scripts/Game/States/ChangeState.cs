using UnityEngine;

public class ChangeState : MonoBehaviour
{
    [SerializeField] private StateManager.GameState _newState;
    private StateManager _stateManager;
    void Start()
    {
        _stateManager = FindFirstObjectByType<StateManager>();
    }

    public void ChangeTheState()
    {
        _stateManager.ChangeState(_newState);
    }
}
