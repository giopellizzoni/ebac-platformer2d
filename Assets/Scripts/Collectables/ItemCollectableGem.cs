using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableGem : ItemCollectableBase
{
    public Collider2D collider;
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddGems();
        collider.enabled = false;
    }

}
