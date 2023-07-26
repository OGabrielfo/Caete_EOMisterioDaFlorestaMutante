using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VidaMaxPlus : MonoBehaviour
{
    public string objectName;
    public GameController gameController;

    public AudioClip pegando;
    public AudioSource audioControl;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(objectName))
        {
            if (PlayerPrefs.GetInt(objectName) == 1)
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (objectName)
            {
                case "hpUp01":
                    gameController._hpUp01 = 1;
                    break;
                case "hpUp02":
                    gameController._hpUp02 = 1;
                    break;
                case "hpUp03":
                    gameController._hpUp02 = 1;
                    break;
                default: break;
            }
            audioControl.clip = pegando;
            audioControl.Play();
            gameController.SaveGame();
            Destroy(gameObject);
        }
    }
}
