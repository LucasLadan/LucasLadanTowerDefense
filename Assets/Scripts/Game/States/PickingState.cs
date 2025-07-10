using Unity.VisualScripting;
using UnityEngine;

public class PickingState : MonoBehaviour
{

    [SerializeField] private StateManager _stateManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _stateManager.UpdateGameState.AddListener(UpdateState);
    }

    private void UpdateState(StateManager.GameState gameState)
    {
        if (gameState == StateManager.GameState.picking)
        {

        }
    }
}
