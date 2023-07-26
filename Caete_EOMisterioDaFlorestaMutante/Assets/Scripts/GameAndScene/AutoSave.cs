using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSave : MonoBehaviour
{
    public SceneController _sceneController;

    public AudioClip save;
    public AudioSource audioControl;
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
        if (other.gameObject.CompareTag("Player"))
        {
            if (audioControl.clip != save)
            {
                audioControl.clip = save;
                audioControl.Play();
            }
            other.gameObject.GetComponent<PlayerController>().vida = other.gameObject.GetComponent<PlayerController>().vidaMax;
            _sceneController.SaveGame();
        }
    }
}
