using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public AudioSource audioControl;
    public AudioClip fall, climb, attack, death, isDamage, gainPower, steps, jump;
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
        audioControl.clip = fall;
        audioControl.Play();
    }
    public void AudioAttack()
    {
        audioControl.clip = attack;
        audioControl.Play();
    }
    public void AudioDeath()
    {
        if(audioControl.clip != death)
        {
            audioControl.clip = death;
            audioControl.Play();
        }
        
    }
    public void AudioIsDamage()
    {
        audioControl.clip = isDamage;
        audioControl.Play();
    }

    public void AudioSteps()
    {
        audioControl.clip = steps;
        audioControl.Play();
    }

    public void AudioJump()
    {
        audioControl.clip = jump;
        audioControl.Play();
    }
}
