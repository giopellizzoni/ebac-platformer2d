using Ebac.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public SOInt coins;
    public SOInt gems;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins.value = 0;
        gems.value = 0;
    }

    public void AddCoins(int amount = 1)
    {
        coins.value += amount;
    }

    public void AddGems(int amount = 1)
    {
        gems.value += amount;
    }
}
