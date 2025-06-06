using UnityEngine;

public class DisableOnState : MonoBehaviour
{
    [SerializeField] private StateManager.GameState toDisableOn;
    private StateManager _stateManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _stateManager = FindFirstObjectByType<StateManager>();
        _stateManager.UpdateGameState.AddListener(UpdateGameState);
    }

    private void UpdateGameState(StateManager.GameState _newState)
    {
        if (_newState == toDisableOn)
        {
            _stateManager.UpdateGameState.RemoveListener(UpdateGameState);
            gameObject.SetActive(false);
        }
    }
}
