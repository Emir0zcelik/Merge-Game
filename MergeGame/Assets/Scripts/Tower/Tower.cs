using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Range(1f, 8f)]
    [SerializeField] protected float attackRange;








    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        GetComponent<SphereCollider>().radius = attackRange;
    }


}
