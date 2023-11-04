using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenTower : Tower
{

    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform _firePoint;


    private void Start()
    {
        InvokeRepeating("ShootArrow", .5f, .8f);
    }

    void ShootArrow()
    {
        Instantiate(arrowPrefab, _firePoint.position, Quaternion.identity);
    }



}
