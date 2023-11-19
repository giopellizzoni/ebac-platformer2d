using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioTriggerTransition : MonoBehaviour
{

    public AudioMixerSnapshot snapshot1;
    public AudioMixerSnapshot snapshot2;

    public string tagToCompare = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(tagToCompare))
        {
            snapshot2.TransitionTo(.1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(tagToCompare))
        {
            snapshot1.TransitionTo(.1f);
        }
    }



}
