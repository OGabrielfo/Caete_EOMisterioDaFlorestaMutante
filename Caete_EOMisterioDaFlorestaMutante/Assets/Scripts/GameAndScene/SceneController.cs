using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameController gameController;

    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        if(PlayerPrefs.HasKey("posX"))
        {
            gameController.LoadTest();
        }
        else
        {
            gameController._posX = _player.transform.position.x;
            gameController._posY = _player.transform.position.y;
            gameController._posZ = _player.transform.position.z;
            gameController.SaveTest();
        }

        _player.transform.position = new Vector3(gameController._posX, gameController._posY, gameController._posZ);
        /*
        gameController = GetComponent<GameController>();
        gameController.LoadConfigs();
        gameController.LoadGame();
        */
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            gameController._posX = _player.transform.position.x;
            gameController._posY = _player.transform.position.y;
            gameController._posZ = _player.transform.position.z;
            gameController.SaveTest();
            Debug.Log("Jogo Salvo" + " " + gameController._posX + " " + gameController._posY + " " + gameController._posZ);
        }
    }
}