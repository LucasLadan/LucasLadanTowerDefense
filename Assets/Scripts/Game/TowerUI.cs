using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    [SerializeField] private GameObject _tower;
    private Toggle _toggle;
    private TowerManager _towerManager;

    [SerializeField] private Image _sprite;
    [SerializeField] private TextMeshProUGUI _price;

    private void Start()
    {
        
        _toggle = GetComponent<Toggle>();
        _towerManager = FindFirstObjectByType<TowerManager>();
    }

    public void Toggled(bool isOn)
    {
        if (_towerManager.PickedTower(_tower, isOn))
        {
            //_toggle.isOn = false;
        }
    }

    public bool SetTower(GameObject _newTower)
    {
        ITowerFunctions _towerFunctions = _newTower.GetComponent<ITowerFunctions>();
        if (_towerFunctions != null)
        {
            TowerStats _towerStats = _towerFunctions.GetTowerStats();
            if (_towerStats != null)
            {
                _sprite.sprite = _towerStats._sprite;
                _price.text = _towerStats._cost.ToString();
                _tower = _newTower;
                return true;
            }
        }
        return false;
    }


    

}
