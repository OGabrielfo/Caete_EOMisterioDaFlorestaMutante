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
    
    public GameObject _pauseMenu, _habilidadeMenu;
    public GameObject _jaguatirica, _macaco, _peixe, _tatu;
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

        if (_habilidadeMenu.active == true)
        {
            if (_gameController._mula > 0)
            {
                _jaguatirica.SetActive(true);
            }
            else
            {
                _jaguatirica.SetActive(false);
            }

            if (_gameController._mapinguari > 0)
            {
                _macaco.SetActive(true);
            }
            else
            {
                _macaco.SetActive(false);
            }

            if (_gameController._iara > 0)
            {
                _peixe.SetActive(true);
            }
            else
            {
                _peixe.SetActive(false);
            }

            if (_gameController._boitata > 0)
            {
                _tatu.SetActive(true);
            }
            else
            {
                _tatu.SetActive(false);
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
        _habilidadeMenu.SetActive(true);
    }

    public void CloseHabilidades()
    {
        _habilidadeMenu.SetActive(false);
    }

    public void Recomecar()
    {
        /*_sceneController.LoadGame();

        if (_pauseMenu.active == true)
        {
            _pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        if (_sceneController.deathScreen.active == true)
        {
            _sceneController.deathScreen.SetActive(false);
        }*/

        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("GameStages");
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
        if (_sceneController.deathScreen.active == true)
        {
            _sceneController.deathScreen.SetActive(false);
        }
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
