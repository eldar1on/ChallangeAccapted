using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class Platform : MonoBehaviour
{
    public GameObject _cube;


    [Header("Cinemachine")]
    public CinemachineVirtualCamera _Vcam;
    public Cinemachine3rdPersonFollow _3thPersonFollow;
    public float cam_Height;
    float shoulderoffset_m;


    [Header("Lerp")]
    public float _futureScale;
    public float lastScale;
    public int interpolationFramesCount = 45;
    int elapsedFrames = 0;
    public bool _scale;

    [Header("Over")]
    public bool _fail;
    public CharacterMover _mover;
    public Animator _animC;

    [Header("FX")]
    public GameObject _feedback_G;
    public GameObject _feedback_B;

    public delegate void OnSuccess();
    public static event OnSuccess Success;

    public delegate void OnFail();
    public static event OnFail Fail;

    public delegate void OnStart();
    public static event OnStart StartTheGameAlready;

    void Awake()
    {
        _mover = GetComponent<CharacterMover>();
        _scale = false;

        lastScale = 1;
        _3thPersonFollow = _Vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        _3thPersonFollow.ShoulderOffset.y = 6.5f;
    }

    void OnEnable()
    {
        Platform.StartTheGameAlready += GiveStart;

    }

    void OnDisable()
    {
        Platform.StartTheGameAlready -= GiveStart;
    }

    void GiveStart()
    {
        _animC.SetTrigger("Start");
    }


    void Update()
    {
        if (_scale)
        {
            ScaleCube();
        }

        if(_cube.transform.localScale.y <= 0f && _fail == false)
        {
            FailTheRound();
        }
        CameraHeight();
    }

    void FailTheRound()
    {
        Fail();
        _fail = true;
        _mover.enabled = false;
        _animC.SetTrigger("Fall");
    }

    void CameraHeight()
    {
        shoulderoffset_m = Mathf.Clamp(lastScale / 3, 1f, 4f);
        _3thPersonFollow.ShoulderOffset.y = Mathf.Lerp(_3thPersonFollow.ShoulderOffset.y, shoulderoffset_m * 6.5f, Time.deltaTime * 10f);
    }

    void OnTriggerEnter(Collider other)
    {
        //print(other.gameObject.name + "is collided with player.");

        if(other.gameObject.tag == "Obs")
        {
            Obstacle collidedObstacle = other.gameObject.GetComponent<Obstacle>();
            collidedObstacle._shrink = true;

            
            //lastScale = _cube.transform.localScale.y;
            _futureScale = _cube.transform.localScale.y - collidedObstacle._lostScale;

            _scale = true;

            _animC.SetTrigger("React");

            GameObject _BadFeedback = Instantiate(_feedback_B, _animC.gameObject.transform, false);
            Destroy(_BadFeedback, 2f);

            //print("FutureScale from obs : " + _futureScale);

        }

        else if (other.gameObject.tag == "Collectable")
        {

            Collectable collidedCollectable = other.gameObject.GetComponent<Collectable>();
            collidedCollectable._shrink = true;


            _futureScale = _cube.transform.localScale.y + collidedCollectable._scaleValue;
            //lastScale = _cube.transform.localScale.y;
            _scale = true;

            GameObject _goodFeedback = Instantiate(_feedback_G ,_animC.gameObject.transform, false);
            Destroy(_goodFeedback, 2f);

        }

        else if(other.gameObject.tag == "Over")
        {
            //print("Successfully ended the round!");
            _animC.SetTrigger("Win");

            int _roundScore = (int)(_cube.transform.localScale.y * 10);

            print(_roundScore);
            LevelManager._manager.IncreaseCoin(_roundScore);
            Success();
        }

    }




    public void ScaleCube()
    {
        float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
        Vector3 interpolatedScale = Vector3.Lerp(new Vector3( 1f, lastScale, 1f), new Vector3(1f, _futureScale, 1f), interpolationRatio);
        elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)

        _cube.transform.localScale = interpolatedScale;

        if(_cube.transform.localScale.y == _futureScale)
        {
            _scale = false;
            lastScale = _futureScale;
        }

    }

    public GameObject _startButton;
    public void StartTheGame()
    {
        _startButton.SetActive(false);
        StartTheGameAlready();
    }


}
