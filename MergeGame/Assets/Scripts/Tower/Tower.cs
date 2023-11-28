using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TowerType
{
    Empty, Wood, Stone, Ice, Coal
};

public class Tower : MonoBehaviour
{
    [Range(1f, 8f)]
    [SerializeField] protected float attackRange;

    public TowerType towerType;
    public int towerLevel;


    public bool IsEquals(Tower tower)
    {
        if (tower.towerType != towerType)
            return false;

        if (tower.towerLevel != towerLevel)
            return false;

        return true;
    }


    //public virtual void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //    GetComponent<SphereCollider>().radius = attackRange;
    //}


}


