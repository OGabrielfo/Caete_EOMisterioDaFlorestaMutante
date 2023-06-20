using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameController gameController;

    private void Awake()
    {
        gameController = GetComponent<GameController>();
        gameController.LoadConfigs();
        gameController.LoadGame();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
