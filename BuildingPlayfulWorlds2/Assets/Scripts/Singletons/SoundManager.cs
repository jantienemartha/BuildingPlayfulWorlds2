using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager manager;
    public AudioSource source;

    private void Awake()
    {
        if (manager != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            manager = this;
        }
    }

    public void PlayClip(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
