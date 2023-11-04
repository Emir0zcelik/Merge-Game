using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalTower : Tower
{
    [SerializeField] Transform leftPoint;
    [SerializeField] Transform rightPoint;
    [SerializeField] GameObject coalPrefab;

    [SerializeField] float fireInterval = 3f;
    private float currentFireTime;
    private void Start()
    {
        currentFireTime = fireInterval;
    }
    private void Update()
    {
        currentFireTime -= Time.deltaTime;

        if(currentFireTime <= 0f)
        {
            Instantiate(coalPrefab, leftPoint.position, leftPoint.localRotation);
            Instantiate(coalPrefab, rightPoint.position, rightPoint.localRotation);
            currentFireTime = fireInterval;
        }
            
       
    }
}
