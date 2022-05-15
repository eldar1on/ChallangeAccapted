using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePoolManager : MonoBehaviour
{
    public Collectable collectablePrefab;

    public List<Collectable> collectablePool;

    public int collectableAmounthInitial;

    public List<Transform> collectableCoordiantes;
    public GameObject coordinateParent;

    public int _openCollectableCount;

    public GameObject _Player;
    public bool checkPosition;
    public float distanceTreshold;
    public float distanceToNext;

    private void Awake()
    {
        //Oyun henüz başlamadan, belirttiğimiz kadar collectable spawnlıyoruz.

        _openCollectableCount = 0;

        collectablePool = new List<Collectable>();

        for (int i = 0; i < collectableAmounthInitial; i++)
        {
            Collectable newCollectable = Instantiate(collectablePrefab);

            newCollectable.gameObject.SetActive(false);
            newCollectable.transform.parent = transform;
            collectablePool.Add(newCollectable);
        }

        GetCoordinates();
    }

    void GetCoordinates()
    {
        //Collectableların yerleşeceği bütün pozisyonları listeliyoruz.

        collectableCoordiantes = new List<Transform>();

        int childCount = 0;
        childCount = coordinateParent.transform.childCount;
        //print("Coordiantes count : " + childCount);

        for (int i = 0; i < childCount; i++)
        {
            collectableCoordiantes.Add(coordinateParent.transform.GetChild(i));
        }
    }



    Collectable GetCollectableFromPool()
    {
        //Boştaki collectablelardan almaya çalışıyoruz.

        for (int i = 0; i < collectablePool.Count; i++)
        {
            if (!collectablePool[i].gameObject.activeSelf)
            {
                return collectablePool[i];
            }
        }

        return null;
    }


    void PositionCollectable()
    {
        Collectable newCollectable = GetCollectableFromPool();

        //print(collectableCoordiantes[_openCollectableCount].position);

        if(newCollectable == null)
        {
            return;
        }

        newCollectable.gameObject.SetActive(true);
        newCollectable.Init(collectableCoordiantes[_openCollectableCount].position);

        _openCollectableCount++;



    }



    void Update()
    {
        if (checkPosition)
        {
            FurtherSpawn();
        }

    }

    void FurtherSpawn()
    {
        distanceToNext = Vector3.Distance(_Player.transform.position, collectableCoordiantes[ _openCollectableCount ].position);

        if(distanceToNext < distanceTreshold)
        {
            if(_openCollectableCount + 1 < collectableCoordiantes.Count)

            PositionCollectable();
        }
    }
}
