using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{

    public string compareTag = "Player";
    public ParticleSystem particleSystem;
    public float timeToHide = 3;
    public GameObject graphicItem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }


    protected virtual void Collect() 
    {
        if(graphicItem != null) graphicItem.SetActive(false);
        Invoke(nameof(HideObject),timeToHide); 
        OnCollect();
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect() 
    {
        if(particleSystem != null)
        {
            particleSystem.Play();
        }
    }
}
