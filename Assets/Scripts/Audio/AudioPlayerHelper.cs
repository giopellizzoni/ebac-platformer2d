using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerHelper : MonoBehaviour
{
    public KeyCode keycode = KeyCode.Space;
    public AudioSource audioSource;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keycode))
        {
            Play();
        }
    }
    private void Play()
    {
        audioSource.Play();
    }
}
