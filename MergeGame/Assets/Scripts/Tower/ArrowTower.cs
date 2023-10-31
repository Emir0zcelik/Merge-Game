using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : Tower
{
    [SerializeField] Transform[] attackPoints;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] float attackInterval;
    private float currentAttackTime;

    private void Start()
    {
        currentAttackTime = attackInterval;
    }

    private void Update()
    {
        currentAttackTime -= Time.deltaTime;

        if(currentAttackTime <= 0f) 
        {
            for (int i = 0; i < attackPoints.Length; i++)
            {   // Objects are move in the same direction
                //Instantiate(arrowPrefab, attackPoints[i].position, Quaternion.identity);
            }
            currentAttackTime = attackInterval;
        }

       
    }








}
