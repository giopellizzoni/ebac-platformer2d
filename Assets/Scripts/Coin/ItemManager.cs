using Ebac.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public int coins;

    public TMP_Text textMesh;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins = 0;
        AddCoins(0);
    }

    public void AddCoins(int amount = 1)
    {

        coins += amount;
        textMesh.SetText($"x {coins.ToString()}");
    }

}
