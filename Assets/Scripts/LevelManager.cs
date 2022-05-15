using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager _manager;

    public float _Time;
    public float _hasteCooldown;

    [Header("UI Elements")]
    public Animator _m_UI_animCon;
    public GameObject _hasteUI;
    public TextMeshProUGUI _coinText;
    public GameObject _coinSprite;
    public GameObject coinParent;
    public GameObject playerModel;
    public TextMeshProUGUI _LevelText;

    public TextMeshProUGUI _lastCoin;
    public TextMeshProUGUI _Score;


    public int coinCount;

    void Awake()
    {
        Application.targetFrameRate = 60;
        _manager = this;
        _coinText.text = PlayerPrefs.GetInt("CoinCollected").ToString();
        _hasteUI.SetActive(false);
        _Time = 0;

        _LevelText.text = "Level : " + (PlayerPrefs.GetInt("ActiveLevel")).ToString();
    }

    void OnEnable()
    {
        Platform.Success += Win;
        Platform.Fail += Failed;
    }

    void OnDisable()
    {
        Platform.Success -= Win;
        Platform.Fail -= Failed;
    }

    void Failed()
    {
        _m_UI_animCon.SetTrigger("Fail");
        //StartCoroutine("GameEnded", true);
    }

    void Win()
    {
        StartCoroutine("GameEnded", false);
    }



    void Update()
    {
        _Time += Time.deltaTime;

        if(_Time > _hasteCooldown)
        {
            _hasteUI.SetActive(true);
            _Time = -13f;
        }
    }

    private int score;

    [Range(1, 2)]
    public int levelID;

    public void IncreaseCoin(int amount)
    {
        score = amount;
        //print(amount);
        PlayerPrefs.SetInt("CoinCollected", PlayerPrefs.GetInt("CoinCollected") + (amount/2));
    }

    IEnumerator GameEnded(bool _isFailed)
    {
        Camera _cam = Camera.main;

        for (int i = 0; i < 9; i++)
        {
            //print("Diamond");
            Vector3 screenPos = _cam.WorldToScreenPoint(playerModel.gameObject.transform.position);
            Instantiate(_coinSprite, screenPos, Quaternion.identity, coinParent.transform);
            yield return new WaitForSeconds(.1f);
        }
        _coinText.text = PlayerPrefs.GetInt("CoinCollected").ToString();

        _Score.text = "Score : " + score.ToString();
        _lastCoin.text = PlayerPrefs.GetInt("CoinCollected").ToString();

        PlayerPrefs.SetInt("LastLevel", levelID);

        yield return new WaitForSeconds(3f);

        _m_UI_animCon.SetTrigger("Win");

    }

    public void Continue()
    {
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
