using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundEdit : MonoBehaviour
{
    public AudioMixer ambianceAudioMixer;
    public AudioMixer SFXMixer;
    public void SetVolume(float volume)
    {
        ambianceAudioMixer.SetFloat("ambiance", volume);
    }
    public void SetVolumeSFX(float volumeSFX)
    {
        SFXMixer.SetFloat("SFX", volumeSFX);
    }
}
