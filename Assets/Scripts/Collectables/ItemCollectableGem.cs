using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableGem : ItemCollectableBase
{
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddGems();
    }

}
