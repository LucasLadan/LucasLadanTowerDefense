using UnityEngine;
using UnityEngine.UI;

public class Tiles : MonoBehaviour
{
    private TowerManager _towerManager;
    private MoneyManager _moneyManager;

    private void Start()
    {
        _towerManager = FindFirstObjectByType<TowerManager>();
        _moneyManager = FindFirstObjectByType<MoneyManager>();
    }

    private void OnMouseDown()
    {
        Debug.Log("Ok");
        GameObject tower = _towerManager.GetPickedTower();
        if (tower != null)
        {
            ITowerFunctions _towerFunctions = tower.GetComponent<ITowerFunctions>();
            if (_towerFunctions != null)
            {
                TowerStats towerStats = _towerFunctions.GetTowerStats();
                if (towerStats != null)
                {
                    if (_moneyManager.DoTransaction(towerStats._cost))
                    {
                        Debug.Log("Ok");
                        GameObject _placedTower = Instantiate(tower, transform);
                        _placedTower.GetComponent<ITowerFunctions>().TileOn(gameObject.GetComponent<Tiles>());
                    }
                }

            }
        }
    }

}
