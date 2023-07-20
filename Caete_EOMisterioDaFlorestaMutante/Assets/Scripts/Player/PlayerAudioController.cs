using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip fall, climb, attack, death, isDamage, gainPower;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AudioFall()
    {
        audio.clip = fall;
        audio.Play();
    }
    public void AudioAttack()
    {
        audio.clip = attack;
        audio.Play();
    }
    public void AudioDeath()
    {
        audio.clip = death;
        audio.Play();
    }
    public void AudioIsDamage()
    {
        audio.clip = isDamage;
        audio.Play();
    }
}
