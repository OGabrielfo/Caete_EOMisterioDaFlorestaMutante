using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameController gameController;

    private void Awake()
    {
        gameController = GetComponent<GameController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        gameController.NewGame();
        SceneManager.LoadSceneAsync("GameStages");
    }

    public void LoadGame()
    {
        gameController.LoadGame();
        SceneManager.LoadSceneAsync("GameStages");
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void Config()
    {

    }
}
