using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource BGMsound;
    public override void Init()
    {
        BGMsound = Camera.main.transform.GetChild(0).GetComponent<AudioSource>();
    }

    public void PlayBGM(string name)
    {
        Resources.UnloadAsset(BGMsound.clip);
        BGMsound.clip = null;
        AudioClip clip = Resources.Load<AudioClip>($"Sound/{name}");
        BGMsound.clip = clip;
        BGMsound.Play();
    }
}
