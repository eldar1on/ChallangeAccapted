using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{

    public int _totalCoin;
    public int _maxLevel;
    public bool _reset;

    public TextMeshProUGUI _currentLevel;
    public TextMeshProUGUI _currentScore;

    private Animator _animator;

    void Awake()
    {
        Application.targetFrameRate = 60;
        _animator = GetComponent<Animator>();

        PlayerPrefs.SetInt("ActiveLevel", PlayerPrefs.GetInt("ActiveLevel") + 1 );

        _currentLevel.text = "Level : " + PlayerPrefs.GetInt("ActiveLevel").ToString();
        _currentScore.text = PlayerPrefs.GetInt("CoinCollected").ToString();

        if (_reset)
        {
            PlayerPrefs.DeleteAll();    
        }

        /*
        int levelToLoad = PlayerPrefs.GetInt("ActiveLevel") % 2;

        print("ActiveLevel : " + PlayerPrefs.GetInt("ActiveLevel") + "Modulo : " + levelToLoad);
        */
    }

    public void Charge()
    {
        //int levelToLoad = PlayerPrefs.GetInt("ActiveLevel") % 2;

        print("Last Level : " + PlayerPrefs.GetInt("LastLevel").ToString());
        if(PlayerPrefs.GetInt("LastLevel") == 1 )
        {
            SceneManager.LoadScene(2);
        }
        else if (PlayerPrefs.GetInt("LastLevel") == 2)
        {
            SceneManager.LoadScene(1);
        }
        else
            SceneManager.LoadScene(1);

    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _animator.SetTrigger("Charge");
        }
    }
}
