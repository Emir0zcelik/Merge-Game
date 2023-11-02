using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenTower : Tower
{
    public Transform target;
    [SerializeField] string _enemyTag = "Enemy";
    [SerializeField] Transform _attackPoint;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, .5f);
    }

  

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(_enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

        }
        if(nearestEnemy != null && shortestDistance <= attackRange)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
        
    }
    private void Update()
    {
        if (target == null)
            return;
    }
}
