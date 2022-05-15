using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{

    public MoveSwipe _moveSwipe;


    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, 
            new Vector3(_moveSwipe.SumValue, transform.position.y, transform.position.z),
            10f * Time.deltaTime);
    }
}
