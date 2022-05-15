using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweeper : MonoBehaviour
{

    public void OnCollisionEnter(Collision collision)
    {
        //print("Collision");

        if (collision.gameObject.tag == "Obs")
        {
            Obstacle collidedObstacle = collision.gameObject.GetComponent<Obstacle>();
            collidedObstacle._shrink = true;
        }
        else if (collision.gameObject.tag == "Collectable")
        {
            //print("Collectable is triggered!");

            Collectable collidedCollectable = collision.gameObject.GetComponent<Collectable>();
            collidedCollectable._shrink = true;
        }
    }
}
