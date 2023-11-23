using Ebac.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : Singleton<VFXManager>
{
    public enum VFXType
    {
        Jump
    }

    public List<VFXManagerSetup> vfxSetup;

    public void PlayVFXByType(VFXType vfxType, Vector3 position) {
        foreach (var item in vfxSetup)
        {
            if (item.vfxType == vfxType) {
                var prefabItem = Instantiate(item.prefab);
                prefabItem.transform.position = position;
                Destroy(prefabItem.gameObject, 5f);
                break;
            }
        }
    }

}

[System.Serializable]
public class VFXManagerSetup 
{
    public VFXManager.VFXType vfxType;
    public GameObject prefab;
}
