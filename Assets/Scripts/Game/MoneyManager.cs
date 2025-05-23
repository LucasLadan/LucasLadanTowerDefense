using UnityEngine;
using UnityEngine.Events;

public class MoneyManager : MonoBehaviour
{
    private int _money = 20;

    public UnityEvent<int, int> moneyChanged;

    void Start()
    {
        
    }

    public bool DoTransaction(int changedMoney)
    {
        if (_money + changedMoney < 0)
        {
            return false;
        }
        _money += changedMoney;
        moneyChanged.Invoke(_money, changedMoney);
        return true;
    }

    public int GetMoney()
    { return _money; }
}
