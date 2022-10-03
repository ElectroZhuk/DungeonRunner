using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMoney : MonoBehaviour
{
    private int _moneyAmount;

    public int CoinsAmount => _moneyAmount;

    public event UnityAction<int> CoinsChanged;

    public void Add(Money money)
    {
        if (money == null)
            throw new UnityException("Element can't be null");

        _moneyAmount += money.Amount;
        CoinsChanged?.Invoke(_moneyAmount);
    }

    public bool CanAdd(Money coin) => true;
}
