using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    [SerializeField] private GameObject _tower;
    private Toggle _toggle;
    private TowerManager _towerManager;
    private bool doSwitch;

    [SerializeField] private Image _sprite;
    [SerializeField] private TextMeshProUGUI _price;

    private void Start()
    {
        doSwitch = true;
        _toggle = GetComponent<Toggle>();
        _towerManager = FindFirstObjectByType<TowerManager>();
        _towerManager.towerChanged.AddListener(TowerSwitch);
    }

    public void TowerSwitch()
    {
        if (doSwitch)
        {
            _sprite.color = Color.white;
            _toggle.isOn = false;
        }
        else
        {
            doSwitch = true;
        }
    }

    public void Toggled(bool isOn)
    {
        if (isOn)
        {
            _sprite.color = Color.gray;
            doSwitch = false;
            _towerManager.PickedTower(_tower, isOn);
        }
        else
        {
            _sprite.color = Color.white;
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
