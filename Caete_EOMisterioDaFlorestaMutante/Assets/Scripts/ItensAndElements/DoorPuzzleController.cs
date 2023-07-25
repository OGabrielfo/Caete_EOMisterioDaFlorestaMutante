using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzleController : MonoBehaviour
{
    public GameObject[] keys;
    public GameObject door;

    private int keysActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        keysActive = 0;

        foreach (GameObject keys in keys)
        {
            KeysPlayerIdentifier key = keys.GetComponent<KeysPlayerIdentifier>();
            if (key.playerTouched == true)
            {
                keysActive++;
            }
        }

        if (keysActive == keys.Length)
        {
            Destroy(door);
            //Destroy(gameObject);
        }
    }
}
