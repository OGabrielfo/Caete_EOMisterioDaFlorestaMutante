using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPlus : MonoBehaviour
{
    public string objectName;
    public GameController gameController;
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
                case "spUp01":
                    gameController._spUp01 = 1;
                    break;
                case "spUp02":
                    gameController._spUp02 = 1;
                    break;
                default: break;
            }
            gameController.SaveGame();
            Destroy(gameObject);
        }
    }
}
