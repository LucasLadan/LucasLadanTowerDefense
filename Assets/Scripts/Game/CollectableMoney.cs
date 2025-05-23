using UnityEngine;
using UnityEngine.UIElements;

public class CollectableMoney : MonoBehaviour
{
    private int _givenMoney;
    [SerializeField] private Button _button;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectMoney()
    {
        MoneyManager _moneymanager = FindFirstObjectByType<MoneyManager>();
        if (_moneymanager != null)
        {
            _moneymanager.DoTransaction(_givenMoney);
            Destroy(gameObject);
        }
    }

    
}
