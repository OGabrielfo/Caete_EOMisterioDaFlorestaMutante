using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAndAmbienceAudioController : MonoBehaviour
{
    public AudioSource musicSource, ambienceSource;
    public AudioClip musicClip, ambienceClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && musicSource.clip != musicClip && ambienceSource.clip != ambienceClip)
        {
            musicSource.clip = musicClip;
            ambienceSource.clip = ambienceClip;
            musicSource.Play();
            ambienceSource.Play();
        }
    }
}
