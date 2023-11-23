using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioChangeVolume : MonoBehaviour
{
    public AudioMixer group;
    public string paramName;

    public void ChangeVolume(float level) {
        group.SetFloat(paramName, level);
    }
}
