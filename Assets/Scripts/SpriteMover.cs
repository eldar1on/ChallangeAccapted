using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMover : MonoBehaviour
{
    public Transform targetPos;
    public int elapsedFrames;
    public int interpolationFramesCount;
    public float threshold;

    

    void Update()
    {
        float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;

        Vector3 interpolatedPosition = Vector3.Lerp(transform.position, LevelManager._manager._coinText.transform.position , interpolationRatio);

        elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)

        float firstDistance = (transform.position - LevelManager._manager._coinText.transform.position).magnitude;
        //print(firstDistance);

        if (firstDistance < threshold)
        {

            //print("Increase diamond count!");
            Destroy(gameObject);
        }

        transform.position = interpolatedPosition;
    }
}
