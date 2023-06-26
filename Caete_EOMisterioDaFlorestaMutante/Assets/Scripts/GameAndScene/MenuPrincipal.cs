using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    private GameController gameController;

    public Button continuar;

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
        if (gameController._gamePlayed == 1)
        {
            continuar.interactable = true;
        }
        else
        {
            continuar.interactable = false;
        }
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
