using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Global Variables
    //Player
    [HideInInspector]
    public int _vidaMax, _vidaAtual, _limitePulo, _pontos;
    [HideInInspector]
    public float _posX, _posY, _posZ;

    //Bosses
    [HideInInspector]
    public int _mula, _mapinguari, _iara, _boitata, _homem;

    //Items
    [HideInInspector]
    public int _hpUp01, _hpUp02, _hpUp03, _hpUp04, _hpUp05;
    #endregion

    #region Variáveis Gerais
    public int _gamePlayed;
    public float _volumeMusic, _volumeSFX;
    #endregion

    private void Awake()
    {
        if (PlayerPrefs.HasKey("gamePlayed"))
        {
            _gamePlayed = PlayerPrefs.GetInt("gamePlayed");
        }
        else
        {
            _gamePlayed = 0;
        }

        LoadConfigs();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void NewGame() {
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("gamePlayed", 1);

        //Player
        PlayerPrefs.SetInt("vidaMax", 3);
        PlayerPrefs.SetInt("vidaAtual", 3);
        PlayerPrefs.SetInt("limitePulo", 1);
        PlayerPrefs.SetInt("pontos", 0);

        //Bosses
        PlayerPrefs.SetInt("mula", 0);
        PlayerPrefs.SetInt("mapinguari", 0);
        PlayerPrefs.SetInt("iara", 0);
        PlayerPrefs.SetInt("boitata", 0);
        PlayerPrefs.SetInt("homem", 0);

        //Items
        PlayerPrefs.SetInt("hpUp01", 0);
        PlayerPrefs.SetInt("hpUp02", 0);
        PlayerPrefs.SetInt("hpUp03", 0);
        PlayerPrefs.SetInt("hpUp04", 0);
        PlayerPrefs.SetInt("hpUp05", 0);

        PlayerPrefs.Save();

    }

    public void SaveGame() {
        //Player
        PlayerPrefs.SetInt("vidaMax", _vidaMax);
        PlayerPrefs.SetInt("vidaAtual", _vidaAtual);
        PlayerPrefs.SetInt("limitePulo", _limitePulo);
        PlayerPrefs.SetInt("pontos", _pontos);
        PlayerPrefs.SetFloat("posX", _posX);
        PlayerPrefs.SetFloat("posY", _posY);
        PlayerPrefs.SetFloat("posZ", _posZ);

        //Bosses
        PlayerPrefs.SetInt("mula", _mula);
        PlayerPrefs.SetInt("mapinguari", _mapinguari);
        PlayerPrefs.SetInt("iara", _iara);
        PlayerPrefs.SetInt("boitata", _boitata);
        PlayerPrefs.SetInt("homem", _homem);

        //Items
        PlayerPrefs.SetInt("hpUp01", _hpUp01);
        PlayerPrefs.SetInt("hpUp02", _hpUp02);
        PlayerPrefs.SetInt("hpUp03", _hpUp03);
        PlayerPrefs.SetInt("hpUp04", _hpUp04);
        PlayerPrefs.SetInt("hpUp05", _hpUp05);

        PlayerPrefs.Save();
    }

    public void LoadGame() {
        //Player
        _vidaMax = PlayerPrefs.GetInt("vidaMax");
        _vidaAtual = PlayerPrefs.GetInt("vidaAtual");
        _limitePulo = PlayerPrefs.GetInt("limitePulo");
        _pontos = PlayerPrefs.GetInt("pontos");
        _posX = PlayerPrefs.GetFloat("posX");
        _posY = PlayerPrefs.GetFloat("posY");
        _posZ = PlayerPrefs.GetFloat("posZ");

        //Bosses
        _mula = PlayerPrefs.GetInt("mula");
        _mapinguari = PlayerPrefs.GetInt("mapinguari");
        _iara = PlayerPrefs.GetInt("iara");
        _boitata = PlayerPrefs.GetInt("boitata");
        _homem = PlayerPrefs.GetInt("homem");

        //Items
        _hpUp01 = PlayerPrefs.GetInt("hpUp01");
        _hpUp02 = PlayerPrefs.GetInt("hpUp02");
        _hpUp03 = PlayerPrefs.GetInt("hpUp03");
        _hpUp04 = PlayerPrefs.GetInt("hpUp04");
        _hpUp05 = PlayerPrefs.GetInt("hpUp05");

    }

    public void SaveConfigs()
    {
        PlayerPrefs.SetFloat("volumeMusic", _volumeMusic);
        PlayerPrefs.SetFloat("volumeSFX", _volumeSFX);
        PlayerPrefs.Save();
    }

    public void LoadConfigs()
    {
        if (PlayerPrefs.HasKey("volumeMusic"))
        {
            _volumeMusic = PlayerPrefs.GetFloat("volumeMusic");
        }
        else
        {
            _volumeMusic = 0;
            PlayerPrefs.SetFloat("volumeMusic", _volumeMusic);
        }

        if (PlayerPrefs.HasKey("volumeSFX"))
        {
            _volumeSFX = PlayerPrefs.GetFloat("volumeSFX");
        }
        else
        {
            _volumeSFX = 0;
            PlayerPrefs.SetFloat("volumeSFX", _volumeSFX);
        }
    }

    public void LoadTest()
    {
        _posX = PlayerPrefs.GetFloat("posX");
        _posY = PlayerPrefs.GetFloat("posY");
        _posZ = PlayerPrefs.GetFloat("posZ");
    }

    public void SaveTest()
    {
        PlayerPrefs.SetFloat("posX", _posX);
        PlayerPrefs.SetFloat("posY", _posY);
        PlayerPrefs.SetFloat("posZ", _posZ);
        PlayerPrefs.Save();
    }
}
