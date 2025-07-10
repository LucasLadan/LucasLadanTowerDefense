using System.Collections;
using UnityEngine;

public class MoneyPickup : Bullet
{

    [SerializeField] private int moneyToGive;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        //Nothing, this doesn't harm
    }

    private void OnMouseDown()
    {
        if (FindFirstObjectByType<MoneyManager>().DoTransaction(moneyToGive))
        {
            Destroy(gameObject);
        }
    }
}
