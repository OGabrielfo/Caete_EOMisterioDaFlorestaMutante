using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    private GameController _gameController;
    private SceneController _sceneController;
    
    public GameObject _pauseMenu;
    public AudioMixer audioMixer;

    [SerializeField] private Slider volumeMusic;
    [SerializeField] private Slider volumeSFX;


    private void Awake()
    {
        _gameController = GetComponent<GameController>();
        _sceneController = GetComponent<SceneController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        volumeMusic.value = _gameController._volumeMusic;
        volumeSFX.value = _gameController._volumeSFX;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (_pauseMenu.active == false)
            {
                _pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                _pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    public void Continuar()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Habilidades()
    {

    }

    public void Recomecar()
    {
        if (_pauseMenu.active == true)
        {
            _pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        _sceneController.LoadGame();
    }

    public void Sair()
    {
        _sceneController.SaveGame();
        SceneManager.LoadScene("MenuInicial");
        SceneManager.UnloadSceneAsync("GameStages");
        Time.timeScale = 1f;
    }

    public void SairSemSalvar()
    {
        SceneManager.LoadScene("MenuInicial");
        SceneManager.UnloadSceneAsync("GameStages");
    }

    public void SFXVolume()
    {
        audioMixer.SetFloat("SFXVolume", volumeSFX.value);
        _gameController._volumeSFX = volumeSFX.value;
        _gameController.SaveConfigs();
    }

    public void MusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", volumeMusic.value);
        _gameController._volumeMusic = volumeMusic.value;
        _gameController.SaveConfigs();
    }

}
