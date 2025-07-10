using UnityEngine;
using UnityEngine.UI;

public class Tiles : MonoBehaviour
{
    private TowerManager _towerManager;
    private MoneyManager _moneyManager;
    private bool _hasTower = false;
    private GameObject _tower = null;

    private void Start()
    {
        _towerManager = FindFirstObjectByType<TowerManager>();
        _moneyManager = FindFirstObjectByType<MoneyManager>();
    }

    public void TowerDestroyed()
    {
        if (gameObject.transform.childCount > 0)
        {
            _hasTower = false;
            _tower = null;
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    private void OnMouseDown()
    {
        GameObject tower = _towerManager.GetPickedTower();

        if(!_hasTower && tower == null) return;

        ITowerFunctions _towerFunctions = tower.GetComponent<ITowerFunctions>();

        if (_towerFunctions == null) return;

        TowerStats towerStats = _towerFunctions.GetTowerStats();
        if (_towerFunctions == null) return;

        if (_hasTower) return;

        if (!_moneyManager.DoTransaction(towerStats._cost * -1)) return;

        

        GameObject _placedTower = Instantiate(tower, transform);
        _placedTower.GetComponent<ITowerFunctions>().TileOn(gameObject.GetComponent<Tiles>());
        _placedTower.transform.position += new Vector3(0, 0, 0.2f);
        _tower = _placedTower;
        _hasTower = true;
    }

}
