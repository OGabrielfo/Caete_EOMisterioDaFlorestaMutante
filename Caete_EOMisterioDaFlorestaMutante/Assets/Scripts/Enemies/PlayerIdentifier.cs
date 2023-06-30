using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdentifier : MonoBehaviour
{
    public bool playerInArea = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInArea = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInArea = false;
        }
    }
}
