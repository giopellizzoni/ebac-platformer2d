using Ebac.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInGameManager : Singleton<UIInGameManager>
{
    public TextMeshProUGUI uiTextMeshCoins;

    public void UpdateTextCoints(string coinsStr)
    { 
        uiTextMeshCoins.text = coinsStr;
    }
}
