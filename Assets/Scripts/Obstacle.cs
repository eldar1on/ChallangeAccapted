using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Obstacle : Collectable
{
    //[SerializeField]
    [Header("Obstacle")]
    public float _lostScale;

    public Material[] _materials;
    public MeshRenderer _renderer;
    public TextMeshProUGUI _text;
    public bool forcedToBe;
    public bool _x50;

    virtual public void Start()
    {
        //print("Hello i'm an obstacle!");
        SetType();
    }

    void OnEnable()
    {
        SetType();

    }

    void SetType()
    {
        if (!forcedToBe)
        {
            int _random = Random.Range(0, 2);

            switch (_random)
            {
                case 1:
                    //print("0.5f");
                    _text.text = "-0.5";
                    _lostScale = 0.5f;
                    _renderer.material = _materials[1];
                    break;
                case 0:
                    //print("0.25f");
                    _text.text = "-0.25";
                    _lostScale = 0.25f;
                    _renderer.material = _materials[0];
                    break;
                default:
                    print("FAOOOOOOOOOOOOOOOOOO");
                    break;
            }
        }
        else
        {
            if (_x50)
            {
                print("0.5f");
                _text.text = "-0.5";
                _lostScale = 0.5f;
                _renderer.material = _materials[1];
            }
            else
            {
                print("0.25f");
                _text.text = "-0.25";
                _lostScale = 0.25f;
                _renderer.material = _materials[0];
            }
        }


    }

    float GetLostScale()
    {
        return _lostScale;
    }

}
