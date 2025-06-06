using TMPro;
using UnityEngine;

public class MoneyText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private MoneyManager _moneyManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _moneyManager.moneyChanged.AddListener(MoneyUpdate);
    }

    private void MoneyUpdate(int money, int change)
    {
        _text.text = money.ToString()+"$";
    }
}
