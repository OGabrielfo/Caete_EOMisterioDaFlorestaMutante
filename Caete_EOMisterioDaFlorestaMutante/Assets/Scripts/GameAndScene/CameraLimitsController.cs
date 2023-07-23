using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimitsController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    private CinemachineConfiner confiner;
    private BoxCollider newConfiner;

    // Start is called before the first frame update
    void Start()
    {
        newConfiner = GetComponent<BoxCollider>();
        confiner = virtualCamera.GetComponent<CinemachineConfiner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            confiner.m_BoundingVolume = newConfiner;
            confiner.InvalidatePathCache();
        }
    }
}
