using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [Header("Values")]
    public float _scaleValue;

    [Header("Lerp")]
    public int interpolationFramesCount = 20; // Number of frames to completely interpolate between the 2 positions
    public int elapsedFrames = 0;
    public bool _shrink;
    public bool _inflate;

    void Start()
    {
        
    }

    void Update()
    {
        if (_shrink)
        {
            Shrink();
        }

        if (_inflate)
        {
            Inflate();
        }
    }

    void Shrink()
    {
        float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;

        Vector3 interpolatedScale = Vector3.Lerp(Vector3.one, Vector3.zero, interpolationRatio);

        if (elapsedFrames == interpolationFramesCount)
        {
            _shrink = false;
            gameObject.SetActive(false);
        }

        elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);

        transform.localScale = interpolatedScale;
    }

    void Inflate()
    {
        float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;

        Vector3 interpolatedScale = Vector3.Lerp(Vector3.zero, Vector3.one, interpolationRatio);

        if (elapsedFrames == interpolationFramesCount)
        {
            _inflate = false;
        }

        elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);

        transform.localScale = interpolatedScale;
    }

    public Vector3 _worldPos;

    public void Init(Vector3 _coordiate)
    {
        transform.position = _coordiate;
        _inflate = true;
    }

    public void KillSelf()
    {
        _shrink = true;
    }
}
