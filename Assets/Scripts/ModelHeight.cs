using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelHeight : MonoBehaviour
{

    public LayerMask _layerMask;
    public float distance;
    public float ray_distance;


    public GameObject _model;
    void Update()
    {

        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down * ray_distance);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, ray_distance, _layerMask))
        {
            _model.transform.position = new Vector3(_model.transform.position.x, hit.point.y + distance, _model.transform.position.z);

        }
    }
}
